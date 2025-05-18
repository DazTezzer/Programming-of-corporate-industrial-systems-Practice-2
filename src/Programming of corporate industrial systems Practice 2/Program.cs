using Programming_of_corporate_industrial_systems_Practice_2.Controllers;
using Programming_of_corporate_industrial_systems_Practice_2.Services;

namespace Programming_of_corporate_industrial_systems_Practice_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var fileAnalyzer = new FileAnalyzer();
            var controller = new FileAnalysisController(fileAnalyzer);

            await controller.RunAsync();
        }
    }
}
