namespace GenerateUnitTestsWithAi.API.Services
{
    public interface IAIGeneratorService
    {
        Task<string?> GetResponse(string prompt);
    }
}
