using Moq;
using Programming_of_corporate_industrial_systems_Practice_2.Controllers;
using Programming_of_corporate_industrial_systems_Practice_2.Models;
using Programming_of_corporate_industrial_systems_Practice_2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Programming_of_corporate_industrial_systems_Practice_2.Tests
{
    public class FileAnalysisControllerTests
    {
        [Fact]
        public async Task RunAsync_AddsResults_WhenFilesProvided()
        {
            // Arrange
            var mockAnalyzer = new Mock<IFileAnalyzer>();
            var sampleResult = new FileAnalysis
            {
                FileName = "test.txt",
                WordCount = 5,
                CharCount = 25
            };

            mockAnalyzer.Setup(x => x.AnalyzeFileAsync(It.IsAny<string>()))
                        .ReturnsAsync(sampleResult);

            var controller = new FileAnalysisController(mockAnalyzer.Object);

            var inputLines = new System.Collections.Generic.Queue<string>(new[] { "test.txt", "" });
            System.Console.SetIn(new System.IO.StringReader(string.Join("\n", inputLines)));

            // Act
            await controller.RunAsync();

            // Assert
            mockAnalyzer.Verify(x => x.AnalyzeFileAsync("test.txt"), Times.Once);
        }
    }
}
