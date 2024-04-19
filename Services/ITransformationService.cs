using GenerateUnitTestsWithAi.API.Services.Models;

namespace GenerateUnitTestsWithAi.API.Services
{
    public interface ITransformationService
    {
        string? WriteTransformation(string key);

        string? WriteTransformationPair(string key, string? value);

        List<TransformationGetModel> GetAllTransformation();


    }
}
