using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Resource
{
	public abstract class ResourceImporter
	{
		/// <summary>
		/// Semicolon seperated list of file extensions.
		/// Example png|bmp|jpg|gif
		/// </summary>
		public abstract string FileDescriptor { get; }
		public abstract T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
			where T : Resource;
	}
}
