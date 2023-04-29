using Amazon.SimpleEmail;
using DinkToPdf;
using DinkToPdf.Contracts;
using lestoma.CommonUtils.Interfaces;
using lestoma.Data;
using lestoma.Data.Repositories;
using lestoma.Logica.Interfaces;
using lestoma.Logica.LogicaService;
using Microsoft.Extensions.DependencyInjection;

namespace lestoma.Api.Helpers
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {

            #region Injection de helpers
            services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IJWT, JWT>();
            services.AddScoped<IAuditoriaHelper, AuditoriaHelper>();
            services.AddScoped<IGenerateReport, GenerateReport>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            #region Injection de logica de negocio
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IBuzonService, BuzonService>();
            services.AddScoped<IUpaService, UpaService>();
            services.AddScoped<IActividadService, ActividadService>();
            services.AddScoped<IDetalleUpaActividadService, DetalleUpaActividadService>();
            services.AddScoped<IComponenteService, ComponenteService>();
            services.AddScoped<IModuloService, ModuloService>();
            services.AddScoped<IReporteService, ReporteService>();
            services.AddScoped<ILaboratorioService, LaboratorioService>();
            #endregion

            #region Injection de repositorios
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<UpaRepository>();
            services.AddScoped<ActividadRepository>();
            services.AddScoped<UpaActividadRepository>();
            services.AddScoped<BuzonRepository>();
            services.AddScoped<ComponenteRepository>();
            services.AddScoped<ProtocoloRepository>();
            services.AddScoped<ModuloRepository>();
            services.AddScoped<AplicacionRepository>();
            services.AddScoped<ReporteRepository>();
            services.AddScoped<LaboratorioRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region Injection de SDK AWS AmazonSimpleEmail
            services.AddAWSService<IAmazonSimpleEmailService>();
            #endregion


            return services;
        }

    }
}
