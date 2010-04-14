using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Resource
{
	public class Model3dsImporter : ResourceImporter
	{
		public override string FileDescriptor
		{
			get { return "model.3ds"; }
		}

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			return default(T);
		}
	}
}
