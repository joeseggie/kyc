using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UgandaTelecom.Kyc.Core.Services
{
    public class UrlHandlerService : IUrlHandlerService
    {
        public Task<string> ProcessGETRequestAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ProcessPOSTRequestAsync(string url, string body, string bearerToken)
        {
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var uri = new Uri(url);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                throw new ApplicationException($"HttpPost request to {url} with body {body} failed with HttpStatusCode {response.StatusCode} content: {(await response.Content.ReadAsStringAsync())}");
            }
        }

        public async Task<string> ProcessPOSTRequestAsync(string url, string body)
        {
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(url);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }

                throw new ApplicationException($"HttpPost request to {uri} with body {body} failed with HttpStatusCode {response.StatusCode} content: {(await response.Content.ReadAsStringAsync())}");
            }
        }
    }
}
