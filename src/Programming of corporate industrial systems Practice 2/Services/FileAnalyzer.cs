using Programming_of_corporate_industrial_systems_Practice_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_of_corporate_industrial_systems_Practice_2.Services
{
    public class FileAnalyzer : IFileAnalyzer
    {
        public async Task<FileAnalysis> AnalyzeFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден", filePath);

            string content = await File.ReadAllTextAsync(filePath);

            int wordCount = content.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int charCount = content.Length;

            return new FileAnalysis
            {
                FileName = filePath,
                WordCount = wordCount,
                CharCount = charCount
            };
        }
    }
}
