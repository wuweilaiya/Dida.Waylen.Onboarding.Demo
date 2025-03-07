using System.Diagnostics;
using Core.Caching.Internal.DidaMember;
using Core.DataModel.Models.DidaSystem.Project.Enums;
using Core.DataModel.Models.DidaSystem.Project.Variables;
using Core.Plugin.Web.MinimalAPIs.AllInOne;
using Core.Plugin.Web.MinimalAPIs.AllInOne.Providers;
using Framework.Startup.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dida.Waylen.Onboarding.Demo.Infrastructure.AllInOne;

public static class ServiceCollectionExtensions
{
    private sealed class AllInOneProvider
    {
    }

    public static WebApplication AddDidaAllInOneDemo(this IServiceCollection services, DidaAllInOneOptions options)
    {
        if (services.Any((ServiceDescriptor service) => service.ImplementationType == typeof(AllInOneProvider)))
        {
            throw new Exception("Please do not call AddDidaAllInOne repeatedly");
        }

        StartupOptions.Set(typeof(IServiceCollection).FullName, services);
        if (options.ProjectName == null)
        {
            string text2 = (options.ProjectName = options.Project.ToString());
        }

        Projects? project = LoggingVariable.Project;
        Projects valueOrDefault = project.GetValueOrDefault();
        if (!project.HasValue)
        {
            valueOrDefault = options.Project;
            LoggingVariable.Project = valueOrDefault;
        }

        if (options.LionKingVersion == LionKingVersion.V2)
        {
            DidaCachingOptions.Version = DidaCachingVersion.V2;
        }

        options.OnConfigureAfter(Core.Plugin.Web.MinimalAPIs.AllInOne.AllInOneProvider.MinimalAPIs, delegate (WebApplication app)
        {
            app.Use(async delegate (HttpContext context, RequestDelegate next)
            {
                HttpContext context2 = context;
                context2.Response.OnStarting(delegate
                {
                    string text3 = Activity.Current?.Id ?? context2.TraceIdentifier;
                    context2.Response.Headers["X-Dida-Trace-Id"] = text3;
                    return Task.CompletedTask;
                });
                await next(context2);
            });
        });
        DidaAllInOneBuilder didaAllInOneBuilder = new DidaAllInOneBuilder(options);
        didaAllInOneBuilder.AddLoggingProvider().AddOpenTelemetryProvider().AddGlobalizationProvider()
            .AddCorsProvider()
            .AddBasicProvider()
            //.AddEFProvider()
            .AddSqliteEFProvider()
            .AddSwaggerProvider()
            .AddGlobalExceptionLoggingProvider()
            .AddJsonProvider()
            .AddDidaFrontendOptionsProvider()
            .AddDIProvider()
            .AddAuthProvider()
            .AddDidaMemberProvider()
            .AddAuditingProvider()
            .AddMinimalAPIsProvider();
        didaAllInOneBuilder.ConfigureServices(services);
        didaAllInOneBuilder.Configure();
        return didaAllInOneBuilder.App;
    }
}
