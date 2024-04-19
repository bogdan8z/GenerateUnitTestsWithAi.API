using GenerateUnitTestsWithAi.API.Services.Configuration;
using GenerateUnitTestsWithAi.API.Services.Models;
using Microsoft.Extensions.Options;

namespace GenerateUnitTestsWithAi.API.Services
{
    public class TransformationService : ITransformationService
    {
        private readonly ICsvWriterService _writer;
        private readonly string _filePath;

        public TransformationService(ICsvWriterService writer, IOptions<CsvOptions> csvOptions)
        {
            _writer = writer;
            _filePath = Path.Combine(
                Directory.GetCurrentDirectory(), 
                csvOptions.Value.ExportFileRelativePath ?? "file not found");
        }

        public string? WriteTransformation(string key)
        { 
            return WriteTransformationPair(key, null);
        }

        public string? WriteTransformationPair(string key, string? value)
        {
            if (CheckExists(key))
            {
                return null;
            }

            value ??= GenerateCode(key);

            var rowToBeWritten = new List<IList<string>>
            {
                new List<string>{key, value }
            };

            _writer.Write(_filePath, rowToBeWritten);
            return null;
        }

        public List<TransformationGetModel> GetAllTransformation()
        {
            var result = new List<TransformationGetModel>();
            var data = _writer.ReadData(_filePath).Where(ln => ln.Count == 2);
            foreach (var line in data)
            {
                result.Add(new TransformationGetModel
                {
                    Key = line[0],
                    Value = line[1],
                });
            }
            return result;
        }

        public string TransformCode(string code)
        {
            //todo
            return code;
        }

        private static string? GenerateCode(string key)
        {
            var newGuid = Guid.NewGuid().ToString().Replace("-","");
            if (string.IsNullOrEmpty(key))
            {
                return newGuid;
            }

            var result = key;
            if (key.Length != 1)
            {
                result = key.Substring(0, 2);
            }

            return $"{result}-{newGuid}";
        }

        private bool CheckExists(string key)
        {
            var code = _writer.SearchValue(_filePath, key);
            return code != null;
        }

        private void Replace()
        {
            // method = "public bool IsOneTwoThree(string str){return str==\"123\";}";

            //foreach existing transformations: search <space>key<anythingelse>  or <anythingelse>key<space>
            //or .key<anythingelse> or <anythingelse>key.
            //or <anythingelse>key(
            //or (key<anythingelse>
            //or <key<anythingelse> or <anythingelse>key>
            //                                                                  and replace the key
        }

       
    }
}
