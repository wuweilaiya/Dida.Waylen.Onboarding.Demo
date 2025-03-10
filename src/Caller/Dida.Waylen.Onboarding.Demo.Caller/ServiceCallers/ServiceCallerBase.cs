using Framework.Service.Caller.Abstractions.Requests;

namespace Dida.Waylen.Onboarding.Demo.Caller.ServiceCallers;

public abstract class ServiceCallerBase
{
    protected ICallerClient CallerClient;

    protected abstract string UrlPrefix { get; }

    public ServiceCallerBase(HotelCallerClient callerClient)
    {
        CallerClient = callerClient.Client;
    }

    protected Task<TResult?> GetAsync<TResult>(string? url = null)
    {
        return CallerClient.GetAsync<TResult>(GetUrl(url));
    }

    protected Task<TResult?> GetAsync<TResult>(object data)
    {
        return CallerClient.GetAsync<TResult>(GetUrl(data));
    }

    protected Task<TResult?> GetAsync<TResult>(string url, object data)
    {
        return CallerClient.GetAsync<TResult>(GetUrl(url, data));
    }

    protected Task PostAsync(string? url = null, object? data = null)
    {
        var request = new PostRequestMessage(GetUrl(url), data);

        return CallerClient.PostAsync(request);
    }

    protected Task PutAsync(string? url = null, object? data = null)
    {
        var request = new PutRequestMessage(GetUrl(url), data);

        return CallerClient.PutAsync(request);
    }

    protected Task DeleteAsync(string url)
    {
        var request = new DeleteRequestMessage(GetUrl(url));

        return CallerClient.DeleteAsync(request);
    }

    string GetUrl(string? url = null) => url is null ? UrlPrefix.TrimEnd('/') : $"{UrlPrefix.TrimEnd('/')}/{url.TrimStart('/')}";

    string GetUrl(object data) => $"{UrlPrefix.TrimEnd('/')}?{ToQueryString(data)}";

    string GetUrl(string url, object data) => $"{GetUrl(url)}?{ToQueryString(data)}";

    protected static string ToQueryString(object obj)
    {
        var properties = obj.GetType().GetProperties();

        return string.Join("&", properties.Select(p => $"{p.Name}={p.GetValue(obj)}"));
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
