namespace PaymentApplication.Controller;

public class HttpRequestController
{
    protected readonly HttpClient _client;
    protected readonly string _requestBaseUrl;

    public HttpRequestController(HttpClient client, string requestBaseUrl)
    {
        _client = client;
        _requestBaseUrl = requestBaseUrl;
    }
}