using Programming_of_corporate_industrial_systems_Practice_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_of_corporate_industrial_systems_Practice_2.Services
{
    /// <summary>
    /// Интерфейс для анализа текстовых файлов.
    /// Определяет контракт для классов, которые выполняют подсчёт количества слов и символов в файле.
    /// </summary>
    public interface IFileAnalyzer
    {
        /// <summary>
        /// Асинхронно анализирует содержимое текстового файла.
        /// Подсчитывает количество слов и символов в файле.
        /// </summary>
        /// <param name="filePath">Путь к файлу, который необходимо проанализировать.</param>
        /// <returns>
        /// Задача, возвращающая структуру <see cref="FileAnalysis"/>, содержащую имя файла,
        /// количество слов и количество символов.
        /// </returns>
        Task<FileAnalysis> AnalyzeFileAsync(string filePath);
    }
}
