using System.Text;
using Newtonsoft.Json;
using PaymentApplication.ValueObjects;
using PaymentCore.Emuns;
using PaymentCore.Interfaces;
using PaymentCore.UseCases;

namespace PaymentApplication.Controller;

public class MakePaymentsUseCaseController : HttpRequestController, IMakePaymentsUseCase
{
    public MakePaymentsUseCaseController(HttpClient client, string requestBaseUrl) : base(client, requestBaseUrl) { }

    public async Task <PaymentState> MakePayment(IPayment paymentData)
    {
        PaymentState result = PaymentState.Pending;
        HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(paymentData), Encoding.UTF8);
        httpContent.Headers.ContentType = new("application/json");
        HttpResponseMessage response = await Client.PostAsync($"{RequestBaseUrl}/{ApiStrings.MakePayment}", httpContent);
        if (response.IsSuccessStatusCode)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<PaymentState>(responseMessage);

        }
        return result;
    }
}