namespace Dida.Waylen.Onboarding.Demo.Caller;

public class HotelCallerClient : ISingleton
{
    public const string CallerName = "Demo";

    public HotelCallerClient(ICallerClientWrapperFactory callerClientWrapperFactory)
    {
        Client = callerClientWrapperFactory.Create(CallerName);
    }

    public ICallerClient Client { get; private set; }
}
