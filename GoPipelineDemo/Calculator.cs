using System.Net.Http;
using System.Threading.Tasks;

namespace GoPipelineDemo
{
    public class Calculator
    {
        private static HttpClient client = new HttpClient();
        private const string requestUrl = "https://localhost:8080/PrimeNumberChecker/";

        public int Add(int a, int b)
        {
            return a + b;
        }

        public PrimeState CheckPrime(long checkNumber)
        {
            return GetProductAsync(requestUrl + checkNumber).Result;
        }
        
        private async Task<PrimeState> GetProductAsync(string path)
        {
            PrimeState result = PrimeState.Invalid;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<PrimeState>();
            }
            return result;
        }
    }
}