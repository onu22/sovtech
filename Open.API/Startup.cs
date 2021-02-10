using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Open.API.config;
using Open.API.Services;
using Open.API.Services.ResourceClient;

namespace Open.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
             .AddAuthentication(options =>
             {
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }
             ).AddJwtBearer(options => Configuration.Bind("AzureAd", options));

            services.AddControllers();
            services.Configure<UrlsConfig>(Configuration.GetSection("urls"));
            services.Configure<AdClientConfig>(Configuration.GetSection("AdClient"));
            services.AddSwagger()
            .AddApplicationServices();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenAPI.API V1");
                
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Open API",
                    Description = "Open ApI Challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "Pascal Onuorah",
                    }
                });

                // To Enable authorization using Swagger (JWT)  
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });

            });

            return services;

        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<HttpClient>();
            services.AddSingleton<IHttpClientService, HttpClientService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IChuckNorrisService, ChuckNorrisService>();
            services.AddScoped<ISwapiService, SwapiService>();
            return services;
        }


    }


}
