using FluentValidation.AspNetCore;
using Kros.CqrsTemplate.Infrastructure;
using Kros.KORM.Extensions.Asp;
using Kros.MediatR.Extensions;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for registering services for this project to the DI container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string ApiName = "Kros.CqrsTemplate Api";
        private const string ApiVersion = "v1";
        private const string FullApiName = ApiName + " " + ApiVersion;

        /// <summary>
        /// Add KORM database.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Configuration.</param>
        public static void AddKormDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddKorm(configuration)
                .UseDatabaseConfiguration<DatabaseConfiguration>()
                .InitDatabaseForIdGenerator()
                .AddKormMigrations(o =>
                {
                    o.AddAssemblyScriptsProvider(Assembly.GetEntryAssembly(), "Kros.CqrsTemplate.SqlScripts");
                })
                .Migrate();

        /// <summary>
        /// Add MediatR.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static IServiceCollection AddMediatRDependencies(this IServiceCollection services)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            return services.AddMediatR(executingAssembly)
                .AddMediatRNullCheckPostProcessor()
                .AddFluentValidation(new[] { executingAssembly });
        }

        /// <summary>
        /// Add Health checks.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Configuration.</param>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
            => services.AddHealthChecks()
                .AddCheck($" {FullApiName}", () => HealthCheckResult.Healthy(), tags: new[] { "api" })
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    name: $" {ApiName} database",
                    tags: new[] { "db", "sql" }).Services;
    }
}
