using Amazon.Runtime;
using AutoMapper;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using lestoma.Api.Core;
using lestoma.Api.Helpers;
using lestoma.Api.Middleware;
using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using lestoma.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace lestoma.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = string.Empty;
            try
            {
                services.AddSingleton<ILoggerManager, LoggerManager>();
                services.AddCors(options =>
                {
                    var frontendURL = Configuration.GetValue<string>("frontend_web_url");
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
                        .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
                    });
                });

                services.AddControllers()
                    .AddNewtonsoftJson();

                if (Environment.IsProduction())
                {
                    connectionString = Reutilizables.Decrypt(Configuration.GetConnectionString("PostgresConnection"));
                }
                else if (Environment.IsDevelopment())
                {
                    connectionString = Configuration.GetConnectionString("PostgresConnection");
                }
                services.AddDbContext<LestomaContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });

                var context = new CustomAssemblyLoadContext();
                context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

                services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new ResponseDTO
                        {
                            MensajeHttp = "Error de entrada en los atributos",
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            ErrorsEntries = context.ModelState.ModelStateErrorsToString()
                        }.ToString();
                        return new BadRequestObjectResult(result);
                    };
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
                        Description = "Copia y pega el Token en el campo 'Value:' así: Bearer " +
                        "{Token JWT}.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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

                //AWSOptions Credentials
                var awsSettings = Configuration.GetSection("AWS").Get<AwsSettings>();
                var awsOption = Configuration.GetAWSOptions();
                awsOption.Credentials = new BasicAWSCredentials(Reutilizables.Decrypt(awsSettings.AccesskeyId), Reutilizables.Decrypt(awsSettings.SecretAccessKey));
                services.AddDefaultAWSOptions(awsOption);


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
                ConfigureHangfire(connectionString, services);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                new LoggerManager().LogError(ex.Message, ex);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            new HangFireRemove().WipeJobs();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "lestoma.Api v1"));

            app.UseMiddleware<CustomExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            new LoggerManager().LogInformation("configurando dashboard");
            app.UseHangfireDashboard("/dashboard-lestoma", new DashboardOptions
            {
                //AppPath = "" //The path for the Back To Site link. Set to null in order to hide the Back To  Site link.
                DashboardTitle = "Dashboard De Lestoma APP",
                Authorization = new IDashboardAuthorizationFilter[]
                {
                     new HangfireDashboardJwtAuthorizationFilter()
                }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void ConfigureHangfire(string connection, IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UsePostgreSqlStorage(connection, new PostgreSqlStorageOptions
               {
                   DistributedLockTimeout = TimeSpan.FromMinutes(5),
                   SchemaName = "hangfire_lestoma",
                   InvisibilityTimeout = TimeSpan.FromMinutes(5),
               }));

            services.AddHangfireServer();
        }
    }
}
