using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.Api.Middleware;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.Data;
using lestoma.Entidades.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace lestoma.Api
{
    public class Startup
    {
        private const string EMAILSUPERADMIN = "superadminlestoma@gmail.com";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddNewtonsoftJson();


            services.AddDbContext<Mapeo>(options =>
            {

                options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection"));
            });



            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "lestoma.Api",
                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()

                    }
                });
            });
            // JWT TOKEN
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
            var issuer = appSettings.Issuer;
            var audience = appSettings.Audience;
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(d =>
                {
                    d.RequireHttpsMetadata = false;
                    d.SaveToken = true;
                    d.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(llave),
                        //establezca clockskew en cero para que los tokens caduquen exactamente a la hora de
                        //vencimiento del token(en lugar de 5 minutos después)
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // Inyección de dependencias a partir de una inversión de control.
            IoC.AddDependency(services);
            services.AddHttpContextAccessor();
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new AutoMappersProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Mapeo db)
        {
            bool adminTabla = false;
            int id = 0;
            try
            {
                var usuario = db.TablaUsuarios.FirstOrDefault(x => x.Email.Equals(EMAILSUPERADMIN));
                if (usuario == null)
                {
                    var hash = HashHelper.Hash("superadmin1234");
                    var superadmin = new EUsuario
                    {
                        Nombre = "Super Admin",
                        Apellido = "Lestoma",
                        Clave = hash.Password,
                        Salt = hash.Salt,
                        EstadoId = (int)TipoEstadoUsuario.Activado,
                        RolId = (int)TipoRol.SuperAdministrador,
                        Email = EMAILSUPERADMIN
                    };
                    db.Add(superadmin);
                    db.SaveChanges();
                }

                else
                {
                    adminTabla = db.TablaSuperAdministradores.Any(x => x.UsuarioId == usuario.Id);
                }

                if (!adminTabla)
                {

                    if (usuario == null)
                    {
                        var existe = db.TablaUsuarios.FirstOrDefault(x => x.Email.Equals(EMAILSUPERADMIN));
                        id = existe.Id;
                    }
                    var super = new ESuperAdministrador
                    {
                        UsuarioId = (short)(id == 0 ? usuario.Id : id)
                    };
                    db.Add(super);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "lestoma.Api v1"));
            }

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
