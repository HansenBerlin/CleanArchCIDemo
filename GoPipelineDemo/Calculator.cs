using System.Net.Http;
using System.Threading.Tasks;

namespace GoPipelineDemo
{
    public class Calculator
    {
        private static readonly HttpClient Client = new();
        private const string RequestUrl = "https://127.0.0.1:8080/PrimeNumberChecker/";

        public int Add(int a, int b)
        {
            return a + b;
        }

        public PrimeState CheckPrime(long checkNumber)
        {
            return GetProductAsync(RequestUrl + checkNumber).Result;
        }
        
        private async Task<PrimeState> GetProductAsync(string path)
        {
            PrimeState result = PrimeState.Invalid;
            HttpResponseMessage response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<PrimeState>();
            }
            return result;
        }
    }
}