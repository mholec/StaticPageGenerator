using System.IO;

namespace StaticPageGenerator
{
	/// <summary>
	/// Vyhledává cesty k souborům
	/// </summary>
	public class PathFinder
	{
		private readonly string root;

		public PathFinder(string root)
		{
			this.root = root;
		}

		public string[] GetPages => Directory.GetFiles(root + "/pages");
		public string[] GetIncludes => Directory.GetFiles(root + "/includes");
		public string[] GetLayouts => Directory.GetFiles(root + "/layouts");
		public string[] GetBlogPosts => Directory.GetFiles(root + "/blog");
	}
}
