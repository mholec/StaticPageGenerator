using System.Collections.Generic;
using System.IO;
using StaticPageGenerator.Helpers;
using StaticPageGenerator.Models;

namespace StaticPageGenerator
{
	public class Generator
	{
		private string outputFolder = "_site";

		public void Gen(string root)
		{
			// hledátko cest k souborům
			PathFinder pathFinder = new PathFinder(root);

			// načítání obsahu souborů
			ContentLoader contentLoader = new ContentLoader(pathFinder);

			// objekt se seznamem všech souborů
			ContentHolder contentHolder = contentLoader.GetContentHolder();

			// sestavení sady HTML stránek
			List<HtmlPage> pages = contentHolder.Build();

			// uložení výsledných stránek
			Directory.CreateDirectory(root + "/" + outputFolder);
			foreach (var page in pages)
			{
				File.WriteAllText(root + "/" + outputFolder + "/" + page.FileName + ".html", page.Content);
			}

			// kompilace assetů
			LessCompiler.CompileLessFiles(root + "/assets/less");

			// kopírování assetů (kompletní adresář)
			new DirectoryHelper().CopyDirectory(root + "/assets", root + "/" + outputFolder + "/assets", true);
			new DirectoryHelper().CopyDirectory(root, root + "/" + outputFolder, false);
		}


	}
}
