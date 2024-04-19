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
            var prompt= _askForUnitTest + method;
            var completion = await _aiGeneratorService.GetResponse(prompt);
            
            return completion;
        }
    }
}
