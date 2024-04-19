using GenerateUnitTestsWithAi.API.Services.Models;

namespace GenerateUnitTestsWithAi.API.Services
{
    public interface ITransformationService
    {
        void WriteTransformation(string key);

        void WriteTransformationPair(string key, string? value);

        List<TransformationGetModel> GetAllTransformation();

        string TransformEncode(string code);

        string TransformDecode(string encodedCode);


    }
}
