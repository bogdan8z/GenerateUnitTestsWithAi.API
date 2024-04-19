
using GenerateUnitTestsWithAi.API.Services.Models;
using Newtonsoft.Json;
using System.Text;

namespace GenerateUnitTestsWithAi.API.Services
{
    public class RapidApiChatGptService : IAIGeneratorService
    {
        private const string RequestUri = "https://open-ai21.p.rapidapi.com/chatgpt";
        private const string Host = "open-ai21.p.rapidapi.com";
        private const string ContentType = "application/json";
        private readonly string _apiKey = "YOUR_API_KEY";
        private readonly string _role = "user";

        public async Task<string?> GetResponse(string prompt)
        {
            using var httpClient = GetHttpClient();
            var data = new
            {
                messages = new[]
                {
                    new
                    {
                        role = _role,
                        content = prompt
                    }
                },
                web_access = false
            };
            string jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, ContentType);

            var response = await httpClient.PostAsync(RequestUri, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<RapidApiChatGptResponseModel>(responseContent);
            return responseData?.Result;
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", Host);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(ContentType));
            return httpClient;
        }
    }
}
