using Hellang.Middleware.ProblemDetails;
using Kros.AspNetCore;
using Kros.AspNetCore.Authorization;
using Kros.AspNetCore.HealthChecks;
using Kros.ProblemDetails.Extensions;
using Kros.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.BuilderMiddlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Kros.CqrsTemplate
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup : BaseStartup
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="env">Environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
            : base(configuration, env)
        { }

        /// <summary>
        /// Configure IoC container.
        /// </summary>
        /// <param name="services">Service.</param>
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddControllers();
            services.AddKrosProblemDetails();

            services.AddApiJwtAuthentication(JwtAuthorizationHelper.JwtSchemeName, Configuration);

            services.AddKormDatabase(Configuration);
            services.AddMediatRDependencies();

            services.Scan(scan =>
                scan.FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());

            services
                .AddSwaggerDocumentation(Configuration, c =>
                {
                    c.AddFluentValidationRules();
                })
                .AddHealthChecks(Configuration)
                .AddApplicationInsights(Configuration);
        }

        /// <summary>
        /// Configure web api pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public override void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            base.Configure(app, loggerFactory);

            if (Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kros.CqrsTemplate API V1");
                });
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseProblemDetails();
            app.UseErrorHandling();
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponseAsync
            });

            app.UseApplicationInsights(Configuration);
            app.UseRouting();
            app.UseRoutePattern();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseKormMigrations();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseRobotsTxt(builder => builder.DenyAll());

            app.UseSwaggerDocumentation(Configuration);
        }
    }
}
