using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using StaticPageGenerator.Extensions;
using StaticPageGenerator.Models;

namespace StaticPageGenerator
{
	/// <summary>
	/// Načítá surové soubory do podoby objektů
	/// </summary>
	public class ContentLoader
	{
		private readonly PathFinder pathFinder;

		public ContentLoader(PathFinder pathFinder)
		{
			this.pathFinder = pathFinder;
		}

		public ContentHolder GetContentHolder()
		{
			ContentHolder contentHolder = new ContentHolder
			{
				Includes = LoadIncludes(), 
				Layouts = LoadLayouts(),
				Pages = LoadPages()
			};

			return contentHolder;
		}

		private List<Include> LoadIncludes()
		{
			List<Include> includes = new List<Include>();

			foreach (string path in pathFinder.GetIncludes)
			{
				FileInfo fileInfo = new FileInfo(path);
				string body = File.ReadAllText(path, Encoding.Default);
				Content content = Content.Build(fileInfo.IsMarkdown(), body);

				var include = new Include()
				{
					Html = content.Html,
					Id = fileInfo.FileName()
				};

				includes.Add(include);
			}

			return includes;
		}

		private List<Layout> LoadLayouts()
		{
			List<Layout> layouts = new List<Layout>();

			foreach (string path in pathFinder.GetLayouts)
			{
				FileInfo fileInfo = new FileInfo(path);
				string body = File.ReadAllText(path, Encoding.Default);

				var layout = new Layout()
				{
					Html = body,
					Id = fileInfo.FileName(),
					Variables = new List<string>() { "var.title" }
				};

				layouts.Add(layout);
			}

			return layouts;
		}

		private List<Page> LoadPages()
		{
			List<Page> pages = new List<Page>();

			foreach (string path in pathFinder.GetPages)
			{
				FileInfo fileInfo = new FileInfo(path);
				string body = File.ReadAllText(path, Encoding.Default);

				if (!body.Contains("---"))
				{
					continue;
				}

				List<string> headerLines = File.ReadAllLines(path, Encoding.UTF8).Take(10).ToList();
				var headers = headerLines
					.Select(x => x.Split(":"))
					.Where(x => x.Length == 2)
					.Select(x => new { Key = x[0], Value = x[1] }).ToList();

				// základní informace o stránce
				Page page = new Page()
				{
					Id = fileInfo.FileName(),
					Layout = headers.FirstOrDefault(x => x.Key == "layout")?.Value ?? "layout"
				};

				// zapíše nalezené proměnné (zatím hardcoded)
				page.Variables.Add("var.title", headers.FirstOrDefault(x => x.Key == "var.title")?.Value);

				// odřízne hlavičku stránky s metadaty
				body = body.Substring(body.IndexOf("---", StringComparison.InvariantCultureIgnoreCase) + 3);
				Content content = Content.Build(fileInfo.IsMarkdown(), body);

				// nastaví obsah
				page.Html = content.Html;

				pages.Add(page);
			}

			return pages;
		}
	}
}
