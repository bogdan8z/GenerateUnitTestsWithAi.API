namespace GenerateUnitTestsWithAi.API.Services
{
    public interface IUnitTestService
    {
        public Task<string?> GenerateUnitTest(string method);
    }
}
