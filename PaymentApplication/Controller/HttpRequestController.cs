namespace PaymentApplication.Controller;

public class HttpRequestController
{
    protected readonly HttpClient Client;
    protected readonly string RequestBaseUrl;

    protected HttpRequestController(HttpClient client, string requestBaseUrl)
    {
        Client = client;
        RequestBaseUrl = requestBaseUrl;
    }
}