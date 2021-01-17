using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using IoTEventHub.Web.Utils;

namespace IoTEventHub.Web
{
    /// <summary>
    /// The start up class of this project
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The Cors policy name
        /// </summary>
        private readonly string MyAllowSpecificOrigins = "MyAllowSpecificOrigins";

        /// <summary>
        /// The configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The starts the configuration
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">the services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // See https://dotnetcoretutorials.com/2019/12/19/using-newtonsoft-json-in-net-core-3-projects/
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddNewtonsoftJson();

            // See https://dotnetcoretutorials.com/2020/01/31/using-swagger-in-net-core-3/
            services.AddSwaggerGenNewtonsoftSupport();

            // Ensure the ApiConfiguration (containing the api key) is read from the appsettings.json
            services.Configure<ApiConfiguration>(Configuration.GetSection("ApiConfiguration"));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "IoTEventHub.Web",
                    Description = "Web API for receiving and processing data of the IoT event hub",
                    Contact = new OpenApiContact
                    {
                        Name = "Eric van Winden",
                        Url = new Uri(Constants.OpenApiContactUrl),
                    }
                });
                setupAction.AddSecurityDefinition(Constants.ApiKey, new OpenApiSecurityScheme
                {
                    Description = "Api key needed to access the endpoints.",
                    In = ParameterLocation.Header,
                    Name = Constants.ApiKey,
                    Type = SecuritySchemeType.ApiKey
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = Constants.ApiKey,
                                Type = SecuritySchemeType.ApiKey,
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = Constants.ApiKey
                                },
                            },
                            new string[] {}
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                });
            });

            services.AddHostedService<StatisticsService>();
            services.AddControllers();
            var aikey = Configuration["APPINSIGHTS_CONNECTIONSTRING"];
            services.AddApplicationInsightsTelemetry(aikey);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();

            // See https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.1&tabs=visual-studio%2Cvisual-studio-xml
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoTEventHub.Web");
                c.RoutePrefix = string.Empty;
            });

            // See https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
