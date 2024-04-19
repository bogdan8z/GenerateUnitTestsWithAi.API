namespace GenerateUnitTestsWithAi.API.Services
{
    public interface ICsvWriterService
    {
        bool Write(string filePath, IList<IList<string>> data);
        //void Write2(string filePath, string[][] matrix);
        string? SearchValue(string filePath, string searchValue);
        IList<IList<string>> ReadData(string filePath);
    }
}
