using Core.Plugin.Caller;
using Framework.Extensions.DependencyInjection;

namespace Dida.Waylen.Onboarding.Demo.Caller;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddTagSystemCaller(this IServiceCollection services)
    {
        services.AddCaller(HotelCallerClient.CallerName, builder =>
        {
            builder.CallerCmsIds = [7283084101882482688L];
        });

        services.AddInject();

        return services;
    }
}
