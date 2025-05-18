using Programming_of_corporate_industrial_systems_Practice_2.Services;
using Xunit;

namespace Programming_of_corporat_2.Tests
{
    public class FileAnalyzerTests
    {
        [Fact]
        public async Task AnalyzeFileAsync_ValidFile_ReturnsCorrectCounts()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempFile, "Hello world! This is a test.");

            var analyzer = new FileAnalyzer();

            // Act
            var result = await analyzer.AnalyzeFileAsync(tempFile);

            // Assert
            Assert.Equal(tempFile, result.FileName);
            Assert.Equal(6, result.WordCount);
            Assert.Equal(28, result.CharCount);

            // Cleanup
            File.Delete(tempFile);
        }

        [Fact]
        public async Task AnalyzeFileAsync_FileNotFound_ThrowsException()
        {
            // Arrange
            var analyzer = new FileAnalyzer();
            string nonExistentFile = "nonexistentfile.txt";

            // Act & Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => analyzer.AnalyzeFileAsync(nonExistentFile));
        }
    }
}
