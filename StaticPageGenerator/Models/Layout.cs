using System.Collections.Generic;

namespace StaticPageGenerator.Models
{
	public class Layout
	{
		public Layout()
		{
			Variables = new List<string>();
		}

		public string Id { get; set; }
		public string Html { get; set; }
		public List<string> Variables { get; set; }
	}
}