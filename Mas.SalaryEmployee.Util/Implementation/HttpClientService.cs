using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mas.SalaryEmployee.Util.Implementation
{
    public class HttpClientService : IHttpClientService
    {
        /// <summary>
        /// Perform a GET HTTP operation 
        /// </summary>
        /// <typeparam name="TOut">Object type to return</typeparam>
        /// <param name="uri">Target uri</param>
        /// <returns>Typed object</returns>
        public async Task<TOut> GetAsync<TOut>(string uri)
        {
            TOut result;

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<TOut>(responseBody);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
    }
}
