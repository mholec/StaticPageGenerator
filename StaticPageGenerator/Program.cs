using System.Threading;

namespace StaticPageGenerator
{
	public class Program
	{
		// args[0] - input
		// args[1] - repeat
		static void Main(string[] args)
		{
			new Generator().Gen(args[0]);

			while (args.Length > 1 && args[1] == "r")
			{
				new Generator().Gen(args[0]);

				Thread.Sleep(2000);
			}
		}
	}
}
