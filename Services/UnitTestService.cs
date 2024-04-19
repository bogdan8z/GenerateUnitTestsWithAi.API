
namespace GenerateUnitTestsWithAi.API.Services
{
    public class UnitTestService : IUnitTestService
    {
        private readonly IAIGeneratorService _aiGeneratorService;
        private readonly string _askForUnitTest = "using c#, NUnit and Moq give me some unit test for the method: ";

        public UnitTestService(IAIGeneratorService aiGeneratorService)
        {
            _aiGeneratorService = aiGeneratorService;
        }
        public async Task<string?> GenerateUnitTest(string method)
        {
            var encodedText = EncodeMethodTextToBeSent(method);

            string? completion = await GetAiResponse(encodedText);

            return completion;
        }

        private string EncodeMethodTextToBeSent(string method)
        {
            return method;
        }

        private async Task<string?> GetAiResponse(string text)
        {
            var prompt = _askForUnitTest + text;
            var completion = await _aiGeneratorService.GetResponse(prompt);
            return completion;
        }
    }
}
