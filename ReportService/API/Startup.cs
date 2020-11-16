using System;
using System.Reflection;
using API;
using API.Controllers;
using API.Infrastructure.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using KafkaFlow;
using KafkaFlow.TypedHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
              .AddCustomMvc()
              .AddCustomDbContext(Configuration)
              .AddCustomSwagger(Configuration)
              .AddCustomConfiguration(Configuration)
              .AddKafka();
            //configure autofac


            var container = new ContainerBuilder();

            // container.RegisterType<PersonRepository>()
            //                 .As<IPersonRepository>()
            //                 .InstancePerLifetimeScope();

            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddAzureWebAppDiagnostics();
            //loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Person");
                   c.OAuthClientId("orderingswaggerui");
                   c.OAuthAppName("Ordering Swagger UI");
               });

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

        }

    }
}
static class CustomExtensionsMethods
{

    public static IServiceCollection AddKafka(this IServiceCollection services)
    {

        services.AddKafka(kafka => kafka
          // .UseConsoleLog() 
          .AddCluster(cluster => cluster
              .WithBrokers(new[] { "localhost:9092" })
              // Install KafkaFlow.Admin
              // .EnableAdminMessages("kafka-flow.admin")
              .AddConsumer(consumer => consumer
                  .Topic("customerEvents")
                  .WithBufferSize(100)
                  .WithWorkersCount(10)
                  .WithAutoOffsetReset(AutoOffsetReset.Latest)
                  .AddMiddlewares(middlewares => middlewares
                      // // Install KafkaFlow.Compressor and Install KafkaFlow.Compressor.Gzip
                      // .AddCompressor<GzipMessageCompressor>() 
                      // // Install KafkaFlow.Serializer and Install KafkaFlow.Serializer.Protobuf
                      // .AddSerializer<ProtobufMessageSerializer>()
                      // // Install KafkaFlow.TypedHandler
                      .AddTypedHandlers(handlers => handlers
                          .WithHandlerLifetime(InstanceLifetime.Singleton)
                          .AddHandler<PersonCreatedHandler>())
                  )
              )
          )
      );

        return services;

    }
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        // Add framework services.
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            // Added for functional tests
            .AddApplicationPart(typeof(ReportController).Assembly)
            .AddNewtonsoftJson()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
        ;

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        return services;
    }

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEntityFrameworkNpgsql();
        //    .AddDbContext<PersonContext>(options =>
        //    {
        //        options.UseNpgsql(configuration["ConnectionString"],
        //            npgsqlOptionsAction: sqlOptions =>
        //            {
        //                sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
        //                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
        //            });
        //    },
        //        ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
        //    );

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Report HTTP API",
                Version = "v1",
                Description = "The Report Service HTTP API"
            });
        });

        return services;
    }

    public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes = { "application/problem+json", "application/problem+xml" }
                };
            };
        });

        return services;
    }


}