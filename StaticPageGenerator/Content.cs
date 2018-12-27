namespace StaticPageGenerator
{
	public class Content
	{
		public string Html { get; set; }
		public string Markdown { get; set; }

		private Content()
		{
		}

		public static Content Build(bool isMarkdown, string content)
		{
			Content provider = new Content();

			if (isMarkdown)
			{
				provider.Markdown = content;
				provider.Html = new Markdown().Transform(content);
			}
			else
			{
				provider.Markdown = null;
				provider.Html = content;
			}

			return provider;
		}
	}
}
