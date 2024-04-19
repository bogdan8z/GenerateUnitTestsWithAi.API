namespace GenerateUnitTestsWithAi.API.Services.Configuration
{
    public class AiOptions
    {
        public const string SectionKey = "Ai";
        public required RapidApiOptions RapidApi { get; set; }
    }
}
