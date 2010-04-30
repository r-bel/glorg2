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

		public ResourceManager()
		{
			var asm = System.Reflection.Assembly.GetCallingAssembly();
			var types = asm.GetTypes();
			importers = new List<ResourceImporter>();
			resources = new List<Resource>();
			foreach (var t in types)
			{
				if (t.IsSubclassOf(typeof(ResourceImporter)))
				{
					var item = Activator.CreateInstance(t) as ResourceImporter;
					importers.Add(item);
				}
			}
			
		}

		public List<Resource> Janitorial()
		{
			List<Resource> remove = new List<Resource>();
			foreach (var r in resources)
			{
				if (r.Links <= 0)
					remove.Add(r);
			}
			return remove;
		}

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
				{
					++res.Links;
					return res as T;
				}
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
						var res = imp.Import<T>(stream, name, this);
                        res.handled = true;
                        ++res.Links;
						lock (resources)
						{
							resources.Add(res);
						}
						return res;
					}
				}

			return default(T);
		}

		internal void Dispose()
		{
			foreach (var res in resources)
				res.Dispose();
		}

		internal void Remove(IEnumerable<Resource> res)
		{
			foreach (var item in res)
			{
				item.DoDispose();
				resources.Remove(item);
			}
		}
	}
}
