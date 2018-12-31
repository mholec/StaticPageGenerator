using System.Threading;
using StaticPageGenerator.Models;

namespace StaticPageGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            GeneratorConfig config = new ConfigLoader().GetConfig(args[0]);

            new Generator().Gen(config.InputPath);

            while (config.IsRecurrent)
            {
                new Generator().Gen(args[0]);

                Thread.Sleep(2000);
            }
        }
    }
}
