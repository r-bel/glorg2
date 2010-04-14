using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Resource
{
	public class ResourceManager
	{
		List<ResourceImporter> importers;
		List<Resource> resources;

		private System.IO.Stream GetStream(string res_name)
		{
			if (System.IO.File.Exists(res_name))
				return new System.IO.FileStream(res_name, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			else
			{
				return null;
			}
		}

		public T LoadResource<T>(string name)
			where T : Resource
		{
			var hash = Crc32.Hash(name);

			foreach (var res in resources)
			{
				if (res.GetHashCode() == hash)
					return res as T;
			}

			int index = name.IndexOf('.');
			if (index == 0)
				throw new System.IO.IOException("Unknown filetype");

			string desc = name.Substring(index + 1).ToLower();
			foreach(var imp in importers)
				if (imp.FileDescriptor == desc)
				{
					using (var stream = GetStream(name))
					{
						imp.Import<T>(stream, name);
					}
				}

			return default(T);
		}
	}
}
