using GenerateUnitTestsWithAi.API.Services.Configuration;
using GenerateUnitTestsWithAi.API.Services.Models;
using Microsoft.Extensions.Options;

namespace GenerateUnitTestsWithAi.API.Services
{
    public class TransformationService : ITransformationService
    {
        private readonly ICsvWriterService _writer;
        private readonly string _filePath;
        private static readonly string PrefixEncode = "__";

        private TransformationService()
        {

        }
        public TransformationService(ICsvWriterService writer, IOptions<CsvOptions> csvOptions)
        {
            _writer = writer;
            _filePath = Path.Combine(
                Directory.GetCurrentDirectory(), 
                csvOptions.Value.ExportFileRelativePath ?? "file not found");
        }

        public void WriteTransformation(string key)
        { 
            WriteTransformationPair(key, null);
        }

        public void WriteTransformationPair(string key, string? value)
        {
            var keyToWrite = LowercaseFirst(key);
            SaveKeyValue(keyToWrite, value);
           
            keyToWrite = UppercaseFirst(key);
            SaveKeyValue(keyToWrite, value);
        }

        private void SaveKeyValue(string keyToWrite, string? value)
        {
            if (CheckExists(keyToWrite))
            {
                return;
            }

            value ??= GenerateCode(keyToWrite);

            var rowToBeWritten = new List<IList<string>>
            {
                new List<string>{ keyToWrite, value }
            };

            _writer.Write(_filePath, rowToBeWritten);
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

        public string TransformEncode(string code)
        {
            var transformations = GetAllTransformation();
            transformations.ForEach(tr =>
            {
                code = code.Replace(tr.Key, tr.Value);
            });

            return code;
        }

        public string TransformDecode(string encodedCode)
        {
            var transformations = GetAllTransformation();
            transformations.ForEach(tr =>
            {
                encodedCode = encodedCode.Replace(tr.Value, tr.Key);
            });

            return encodedCode;
        }

        private static string LowercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            else
                return char.ToLower(s[0]) + s[1..];
        }

        private static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            else
                return char.ToUpper(s[0]) + s[1..];
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

            return $"{result}{PrefixEncode}{newGuid}";
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
