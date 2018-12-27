using System;
using System.IO;

namespace StaticPageGenerator.Extensions
{
	public static class FileInfoExtensions
	{
		/// <summary>
		/// Vrací název souboru bez přípony
		/// </summary>
		public static string FileName(this FileInfo fileInfo)
		{
			return fileInfo.Name.Replace(fileInfo.Extension, "");
		}

		public static bool IsMarkdown(this FileInfo fileInfo)
		{
			return fileInfo.Extension.Equals(".md", StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool IsHtml(this FileInfo fileInfo)
		{
			return fileInfo.Extension.Equals(".html", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
