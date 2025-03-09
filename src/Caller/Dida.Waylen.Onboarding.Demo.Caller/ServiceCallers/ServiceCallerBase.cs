
using Framework.Service.Caller.Abstractions;

namespace Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;

public class ServiceCallerBase
{
    protected ICallerClient CallerClient;

    public ServiceCallerBase(HotelCallerClient callerClient)
    {
        CallerClient = callerClient.Client;
    }

    protected static Dictionary<string, IEnumerable<string>> CreateHeaders(Guid? userId)
    {
        if (userId != null)
        {
            return new Dictionary<string, IEnumerable<string>>()
                {
                    { "UserId", new[] { userId.ToString()! } }
                };
        }

        return [];
    }
}
