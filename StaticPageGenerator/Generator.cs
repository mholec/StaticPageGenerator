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
			List<HtmlPage> pages = contentHolder.BuildPages();

            // uložení výsledných stránek
            Directory.CreateDirectory(root + "/" + outputFolder);
			foreach (var page in pages)
			{
				File.WriteAllText(root + "/" + outputFolder + "/" + page.FileName + ".html", page.Content);
			}

		    // sestavení sady HTML postů
		    List<HtmlPage> posts = contentHolder.BuildBlogPosts();

            // uložení výsledných stránek
		    Directory.CreateDirectory(root + "/" + outputFolder + "/blog");
            foreach (var post in posts)
		    {
		        File.WriteAllText(root + "/" + outputFolder + "/blog/" + post.FileName + ".html", post.Content);
		    }

            // kompilace assetů
            LessCompiler.CompileLessFiles(root + "/assets/less");

			// kopírování assetů (kompletní adresář)
			new DirectoryHelper().CopyDirectory(root + "/assets", root + "/" + outputFolder + "/assets", true);

            // kopírování root souborů (web.config, favicon, etc.)
		    string[] excludes = {"run.bat", "spg.ini"};
			new DirectoryHelper().CopyDirectory(root, root + "/" + outputFolder, false, excludes);
		}


	}
}
