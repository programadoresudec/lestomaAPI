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
            services.AddScoped<IUsuarioService, LSUsuario>();
            services.AddScoped<IBuzonService, LSBuzon>();
            services.AddScoped<IUpaService, LSUpa>();
            services.AddScoped<IActividadService, LSActividad>();
            services.AddScoped<IUpasActividadesService, LSUpasActividades>();
            services.AddScoped<DAOUsuario>();
            services.AddScoped<DAOUpa>();
            services.AddScoped<DAOActividad>();
            services.AddScoped<DAOUpaActividad>();
            services.AddScoped<DAOBuzonReportes>();
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddTransient<ICamposAuditoriaHelper, CamposAuditoriaHelper>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

    }
}
