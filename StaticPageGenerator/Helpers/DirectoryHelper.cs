using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaticPageGenerator.Helpers
{
	public class DirectoryHelper
	{
		public HashSet<string> StopFolders = new HashSet<string>();

		public DirectoryHelper()
		{
			StopFolders.Add("less");
		}

		public void CopyDirectory(string sourceDirectory, string destinationDirectory, bool includeSubdirectories, string[] excludes = null)
		{
			DirectoryInfo dir = new DirectoryInfo(sourceDirectory);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirectory);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			
			if (!Directory.Exists(destinationDirectory))
			{
				Directory.CreateDirectory(destinationDirectory);
			}

			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
			    if (excludes != null && excludes.Contains(file.Name.ToLowerInvariant()))
			    {
                    continue;
			    }

				string temppath = Path.Combine(destinationDirectory, file.Name);
				file.CopyTo(temppath, true);
			}

			if (includeSubdirectories)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					if (StopFolders.Contains(subdir.Name))
					{
						continue;
					}

					string temppath = Path.Combine(destinationDirectory, subdir.Name);
					CopyDirectory(subdir.FullName, temppath, true);
				}
			}
		}
	}
}
