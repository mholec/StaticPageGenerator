using System.IO;
using System.Text;
using StaticPageGenerator.Models;

namespace StaticPageGenerator
{
    public class ConfigLoader
    {
        public GeneratorConfig GetConfig(string inputFolder = null)
        {
            if (inputFolder == null)
            {
                inputFolder = Directory.GetCurrentDirectory();
            }

            GeneratorConfig config = new GeneratorConfig(inputFolder);

            string iniFilePath = config.InputPath + "/spg.ini";

            if (!File.Exists(iniFilePath))
            {
                return config;
            }

            string[] iniFile = File.ReadAllLines(iniFilePath, Encoding.UTF8);

            foreach (var line in iniFile)
            {
                string key = line.Split("=")[0].Trim();
                string value = line.Split("=")[1].Trim();

                if (key == "recurrent")
                {
                    config.IsRecurrent = bool.Parse(value);
                }

                if (key == "output")
                {
                    config.Output = value;
                }
            }

            return config;
        }
    }
}
