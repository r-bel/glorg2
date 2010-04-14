using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Resource
{
	public class BitmapImporter : ResourceImporter
	{

		public override string FileDescriptor
		{
			get { return "texture2d"; }
		}

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			if (typeof(T) != typeof(Glorg2.Graphics.OpenGL.Texture2D))
				throw new System.IO.InvalidDataException("Invalid file format");

			Graphics.OpenGL.Texture2D tex = new Glorg2.Graphics.OpenGL.Texture2D(source, source_name);
			return tex as T;
		}
	}
}
