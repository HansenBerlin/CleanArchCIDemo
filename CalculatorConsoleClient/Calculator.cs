using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorConsoleClient
{
    public class Calculator
    {
        private static readonly HttpClient Client = new();
        private const string RequestUrl = "http://127.0.0.1:8080/PrimeNumberChecker/";

        public int Add(int a, int b)
        {
            return a + b;
        }

        public async Task<PrimeState> CheckPrime(long checkNumber)
        {
            return await GetPrimeState(RequestUrl + checkNumber);
        }
        
        private async Task<PrimeState> GetPrimeState(string path)
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