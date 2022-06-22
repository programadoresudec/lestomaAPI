using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.Data;
using lestoma.Data.DAO;
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
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IJWT, JWT>();
            services.AddTransient<ICamposAuditoriaHelper, CamposAuditoriaHelper>();
            #endregion

            #region Injection de logica de negocio
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IBuzonService, BuzonService>();
            services.AddScoped<IUpaService, UpaService>();
            services.AddScoped<IActividadService, ActividadService>();
            services.AddScoped<IDetalleUpaActividadService, DetalleUpaActividadService>();
            services.AddScoped<IComponenteService, ComponenteService>();
            #endregion

            #region Injection de repositorios
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<UpaRepository>();
            services.AddScoped<ActividadRepository>();
            services.AddScoped<UpaActividadRepository>();
            services.AddScoped<BuzonRepository>();
            services.AddScoped<ComponenteRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            return services;
        }

    }
}
