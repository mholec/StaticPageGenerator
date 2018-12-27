using System.IO;
using System.Text;
using dotless.Core.configuration;

namespace StaticPageGenerator
{
	public static class LessCompiler
	{
		public static void CompileLessFiles(string folder)
		{
			string[] lessFiles = new string[0];

			try
			{
				lessFiles = Directory.GetFiles(folder);
			}
			catch (System.Exception)
			{
			}

			foreach (string lessPath in lessFiles)
			{
				string less = File.ReadAllText(lessPath, Encoding.Default);
				string css = dotless.Core.Less.Parse(less, DotlessConfiguration.GetDefault());

				string cssPath = lessPath.Replace("less", "css");
				File.WriteAllText(cssPath, css);
			}
		}
	}
}
