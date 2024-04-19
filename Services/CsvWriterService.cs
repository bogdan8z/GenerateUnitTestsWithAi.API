
namespace GenerateUnitTestsWithAi.API.Services
{
    public class CsvWriterService : ICsvWriterService
    {
        public bool Write(string filePath, IList<IList<string>> data)
        {
            var isOk = CheckIfFileExists(filePath);

            if(!isOk)
            {
                return false;
            }

            using var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            using var streamwriter = new StreamWriter(fileStream);
            foreach (var row in data)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        string value = row[i].ToString();
                        if (value.Contains(','))
                        {
                            value = string.Format("\"{0}\"", value);
                            streamwriter.Write(value);
                        }
                        else
                        {
                            streamwriter.Write(row[i].ToString());
                        }
                    }
                    if (i < row.Count - 1)
                    {
                        streamwriter.Write(",");
                    }
                }
                streamwriter.Write(streamwriter.NewLine);
            }

            return true;
        }

        public IList<IList<string>> ReadData(string filePath)
        {
            var result = new List<IList<string>>();
            using var reader = new StreamReader(filePath);
            while (!reader.EndOfStream)
            {
                // Read the current line
                var line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                var lineResult = new List<string>();
                result.Add(lineResult);
            
                // Split the line into values
                var values = line.Split(',');
                if (values == null)
                {
                    break;
                }

                // Loop through each value in the line
                foreach (var value in values)
                {
                    lineResult.Add(value);
                }
            }
            return result;
        }

        public string? SearchValue(string filePath, string searchValue)
        {
            using var reader = new StreamReader(filePath);
            //// Read the header line to skip it
            //reader.ReadLine();

            // Loop through each line in the CSV file
            while (!reader.EndOfStream)
            {
                // Read the current line
                var line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }

                // Split the line into values
                var values = line.Split(',');
                if (values == null)
                {
                    break;
                }

                // Loop through each value in the line
                foreach (var value in values)
                {
                    // Check if the current value matches the search value
                    //if (value.Equals(searchValue,og StringComparison.OrdinalIgnoreCase))
                    if(value == searchValue)
                    {
                        return line;
                    }
                }
            }
            return null;
        }

        private static bool CheckIfFileExists(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (dir == null)
            {
                return false;
            }
            Directory.CreateDirectory(dir);
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }

            return true;
        }

        //public void Write2(string filePath, string[][] matrix)
        //{
        //    StreamWriter streamwriter = new StreamWriter(filePath, false);
        //    foreach (var row in matrix)
        //    {
        //        for (int i = 0; i < row.Length; i++)
        //        {
        //            if (!Convert.IsDBNull(row[i]))
        //            {
        //                string value = row[i].ToString();
        //                if (value.Contains(','))
        //                {
        //                    value = String.Format("\"{0}\"", value);
        //                    streamwriter.Write(value);
        //                }
        //                else
        //                {
        //                    streamwriter.Write(row[i].ToString());
        //                }
        //            }
        //            if (i < row.Length - 1)
        //            {
        //                streamwriter.Write(",");
        //            }
        //        }
        //        streamwriter.Write(streamwriter.NewLine);
        //    }
        //    streamwriter.Close();
        //}

    }
}
