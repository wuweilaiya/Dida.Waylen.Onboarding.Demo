using Core.Plugin.Caller;
using Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;
using Dida.Waylen.Onboarding.Demo.Contract.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dida.Waylen.Onboarding.Demo.Caller;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddTagSystemCaller(this IServiceCollection services)
    {
        services.AddCaller("Demo", builder =>
        {
            //builder.CallerCmsIds = [7283084101882482688L, 7283084296208781312L, 7283084431407976448L];
        });

        services.AddSingleton<HotelCallerClient>();
        services.AddScoped<HotelServiceCaller>();
        services.AddScoped<IHotelCaller, HotelCaller>();

        return services;
    }
}
