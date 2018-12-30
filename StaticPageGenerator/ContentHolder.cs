using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StaticPageGenerator.Models;

namespace StaticPageGenerator
{
	/// <summary>
	/// Drží informace o veškerém obsahu
	/// </summary>
	public class ContentHolder
	{
		public List<Layout> Layouts { get; set; }
		public List<Page> Pages { get; set; }
		public List<Include> Includes { get; set; }

		/// <summary>
		/// Poskládá obsah a vrátí výslednou sadu HtmlPages
		/// </summary>
		/// <returns></returns>
		public List<HtmlPage> Build()
		{
			List<HtmlPage> htmlPages = new List<HtmlPage>();

			// zapíše includy do pages a layoutů
			foreach (var include in Includes)
			{
				foreach (var layout in Layouts)
				{
					string mac = "{{include." + include.Id + "}}";
					layout.Html = layout.Html.Replace(mac, include.Html);
				}

				foreach (var page in Pages)
				{
					string mac = "{{include." + include.Id + "}}";
					page.Html = page.Html.Replace(mac, include.Html);
				}
			}

			foreach (var page in Pages)
			{
				// pokusí se najít layout pro page
				Layout layout = Layouts.FirstOrDefault(x => x.Id == page.Layout);
				if (layout == null)
				{
					continue;
				}

				// vloží content page do layoutu
				string content = layout.Html;
				content = content.Replace("{{body}}", page.Html);

				// nahradí proměnné obsahem a pokud neexistují, vyhodí placeholder
				foreach (string layoutVariable in layout.Variables)
				{
					if (page.Variables.ContainsKey(layoutVariable))
					{
						content = content.Replace("{{" + layoutVariable + "}}", page.Variables[layoutVariable]);
					}
					else
					{
						content = content.Replace("{{" + layoutVariable + "}}", "");
					}
				}

                // nahradí systémové proměnné
			    content = content.Replace("{{system.date}}", DateTime.Now.ToString("dd.MM.yyyy"));
			    content = content.Replace("{{system.datetime}}", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
			    content = content.Replace("{{system.spg.version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                var htmlPage = new HtmlPage()
				{
					FileName = page.Id,
					Content = content
				};

				htmlPages.Add(htmlPage);
			}

			return htmlPages;
		}
	}
}
