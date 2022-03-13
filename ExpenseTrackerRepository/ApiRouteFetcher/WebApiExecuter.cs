using ExpenseTrackerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpenseTrackerRepository.ApiRouteFetcher
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public WebApiExecuter(string baseUrl, HttpClient httpClient)
        {
            this.baseUrl = baseUrl;
            this.httpClient = httpClient;

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {

          return await httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<OperationResponse> InvokePost<T>(string uri, T obj)
        {
            var postResponse = await httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            var postContent = await postResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<OperationResponse>(postContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result;
        }
        public async Task<T> InvokePostObjResponse<T>(string uri, T obj)
        {
            var postResponse = await httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            await HandleWebApiExeError(postResponse);

            return await postResponse.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var putResponse = await httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            putResponse.EnsureSuccessStatusCode();
            await HandleWebApiExeError(putResponse);
        }

        public async Task InvokeDelete<T>(string uri)
        {
            var deleteResponse = await httpClient.DeleteAsync(GetUrl(uri));
            deleteResponse.EnsureSuccessStatusCode();
            await HandleWebApiExeError(deleteResponse);
        }

        private string GetUrl(string uri)
        {

            return $"{baseUrl}/{uri}";
        }

        private async Task HandleWebApiExeError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var apiError = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(apiError);
            }
        }
    }
}
