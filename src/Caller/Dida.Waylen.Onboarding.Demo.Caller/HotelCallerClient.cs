using Framework.Service.Caller.Abstractions;

namespace Dida.Waylen.Onboarding.Demo.Caller;

public class HotelCallerClient
{
    public HotelCallerClient(ICallerClientWrapperFactory callerClientWrapperFactory)
    {
        Client = callerClientWrapperFactory.Create("Demo");
    }

    public ICallerClient Client { get; private set; }
}
