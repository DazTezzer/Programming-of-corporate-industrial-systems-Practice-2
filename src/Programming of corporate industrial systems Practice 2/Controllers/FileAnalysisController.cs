using Programming_of_corporate_industrial_systems_Practice_2.Models;
using Programming_of_corporate_industrial_systems_Practice_2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_of_corporate_industrial_systems_Practice_2.Controllers
{
    public class FileAnalysisController
    {
        private readonly IFileAnalyzer _fileAnalyzer;
        private readonly List<FileAnalysis> _analysisResults;
        private readonly Mutex _mutex;

        public FileAnalysisController(IFileAnalyzer fileAnalyzer)
        {
            _fileAnalyzer = fileAnalyzer;
            _analysisResults = new List<FileAnalysis>();
            _mutex = new Mutex();
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Введите путь к файлу (или Enter для завершения):");

            List<Task> tasks = new List<Task>();

            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    break;

                var task = Task.Run(async () =>
                {
                    try
                    {
                        var result = await _fileAnalyzer.AnalyzeFileAsync(input);
                        _mutex.WaitOne();
                        _analysisResults.Add(result);
                        _mutex.ReleaseMutex();
                        PrintResults();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при обработке файла {input}: {ex.Message}");
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("\nФинальные результаты:");
            PrintResults();
        }

        private void PrintResults()
        {
            _mutex.WaitOne();

            int totalWords = _analysisResults.Sum(r => r.WordCount);
            int totalChars = _analysisResults.Sum(r => r.CharCount);

            Console.WriteLine("\nТекущие результаты анализа:");
            for (int i = 0; i < _analysisResults.Count; i++)
            {
                var result = _analysisResults[i];
                Console.WriteLine($"{i + 1}. {result.FileName}: {result.WordCount} слов, {result.CharCount} символов");
            }
            Console.WriteLine($"\nИтог: {totalWords} слов, {totalChars} символов.\n");

            _mutex.ReleaseMutex();
        }
    }
}
