using System.Text;
using Newtonsoft.Json;
using PaymentCore.Emuns;
using PaymentCore.Entities;
using PaymentCore.Services;

namespace PaymentApplication.Controller;

public class PaymentExecuterController : HttpRequestController, IPaymentExecuter
{
    public PaymentExecuterController(HttpClient client, string requestBaseUrl) : base(client, requestBaseUrl) { }

    public async Task <PaymentState> MakePayment(IPayment paymentData, string url)
    {
        PaymentState result = PaymentState.Pending;
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(paymentData), Encoding.UTF8);
        httpContent.Headers.ContentType = new("application/json");
        HttpResponseMessage response = await _client.PostAsync($"{_requestBaseUrl}/{url}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<PaymentState>(responseMessage);

        }
        return result;
    }
}