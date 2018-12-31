using System.Security.Cryptography.X509Certificates;

namespace StaticPageGenerator.Models
{
    public class GeneratorConfig
    {
        public string InputPath { get; private set; }
        public string OutputPath => InputPath + "/" + Output;
        public string Output { get; set; } = "_site";
        public bool IsRecurrent { get; set; } = false;

        public GeneratorConfig(string inputPath)
        {
            InputPath = inputPath;
        }
    }
}
