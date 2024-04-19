using System.Text.Json;
using GenerateUnitTestsWithAi.API.Services.Models;
using OpenAI_API.Completions;

namespace GenerateUnitTestsWithAi.API.Services
{
    public class ChatGptService : IAIGeneratorService
    {
     //   TODO
        private const string RequestUri = "https://api.openai.com/v1/chat/completions";
        private readonly string _model = "gpt-3.5-turbo-0125";
        private readonly string _apiKey = "YOUR_API_KEY";
        
        public async Task<string> GetResponseOld(string prompt)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var requestData = "{\"messages\":[{\"role\":\"user\",\"content\":\"test me?\\n\"},{\"role\":\"assistant\",\"content\":\"ddd\"}],\"temperature\":1,\"max_tokens\":256,\"top_p\":1,\"frequency_penalty\":0,\"presence_penalty\":0,\"model\":\"gpt-3.5-turbo-0125\",\"stream\":true}";

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(RequestUri, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<ChatGptResponseModel>(responseContent);

            return responseData.Choices[0].Text;
        }

        public async Task<string> GetResponse(string prompt)
        {
            var api = new OpenAI_API.OpenAIAPI(_apiKey);

            try
            {
                int maxTokens = 50;

                var completionRequest = new CompletionRequest
                {
                    Prompt = prompt,
                    Model = _model,
                    MaxTokens = maxTokens
                };

                var completionResult = await api.Completions.CreateCompletionAsync(completionRequest);
                var generatedText = completionResult.Completions[0].Text;

                return generatedText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


            return null;
        }
    }
}
