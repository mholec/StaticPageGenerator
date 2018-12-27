using System;
using System.Collections.Generic;
using System.Text;

namespace StaticPageGenerator.Models
{
	public class Page
	{
		public Page()
		{
			Variables = new Dictionary<string, string>();
		}

		public string Id { get; set; }
		public string Layout { get; set; }
		public Dictionary<string,string> Variables { get; set; }
		public string Html { get; set; }
	}
}
