﻿/*
Copyright (C) 2010 Henning Moe

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Resource
{
	public class ResourceManager
	{
		List<ResourceImporter> importers;
		internal List<Resource> resources;
		public IEnumerable<Resource> Resources { get { return resources; } }
		public string Path { get; set; }
		public void AddAssembly(System.Reflection.Assembly asm)
		{
			var types = new List<Type>(asm.GetTypes());
			types.AddRange(System.Reflection.Assembly.GetEntryAssembly().GetTypes());
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

		public ResourceManager()
		{
			Path = Environment.CurrentDirectory;
			var asm = System.Reflection.Assembly.GetCallingAssembly();
			AddAssembly(asm);			
		}
		/// <summary>
		/// Creates a list of objects ready for removal (i.e. has no current references)
		/// </summary>
		/// <returns></returns>
		public List<Resource> Janitorial()
		{
			List<Resource> remove = new List<Resource>();
			lock (resources)
			{
				foreach (var r in resources)
				{
					if (r.Links <= 0)
						remove.Add(r);
				}
			}
			return remove;
		}

		private System.IO.Stream GetStream(string res_name, string handler)
		{
            string fname =System.IO.Path.Combine(Path, res_name + "." + handler);
			if (System.IO.File.Exists(fname))
				return new System.IO.FileStream(fname, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			else
			{
				try
				{
					object res = Properties.Resources.ResourceManager.GetObject(res_name);
					if (res is string)
						return new System.IO.MemoryStream(Encoding.UTF8.GetBytes(res as string), false);
					else
						return null;
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		public void Manage(Resource resource, string name)
		{
			if (resource.handled)
				throw new InvalidOperationException("Object is already managed.");
			resource.SourceName = name;
			lock (resources)
			{
				resources.Add(resource);
			}
		}

        public bool Load<T>(string name, out T ret)
            where T : Resource
        {
            return Load<T>(name, null, out ret);
        }

		public bool Load<T>(string name, string handler, out T ret)
			where T : Resource
		{
			var hash = Hashing.Hash(name);

			foreach (var res in resources)
			{
				if (res.GetHashCode() == hash)
				{
					++res.Links;
					ret = res as T;
                    return true;
				}
			}

			int index = name.IndexOf('.');
			if (index == 0)
				throw new System.IO.IOException("Unknown filetype");

			string desc = handler;

            IEnumerable<ResourceImporter> imps;
            if (string.IsNullOrEmpty(handler))
                imps = from item in importers where item.SupportedTypes.Contains(typeof(T)) orderby item.Priority select item;
            else
                imps = from item in importers where item.FileDescriptor == handler orderby item.Priority select item;

            foreach (var imp in imps)
            {
                using (var stream = GetStream(name, imp.FileDescriptor))
                {
                    if (stream != null)
                    {
                        var res = imp.Import<T>(stream, name, this);
						res.SourceName = name;
                        res.handled = true;
                        ++res.Links;
                        lock (resources)
                        {
                            resources.Add(res);
                        }
                        ret = res;
                        return true;
                    }
                }
            }

            ret = default(T);
			return false;
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
