using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL;

namespace Glorg2.Resource
{
	public class BitmapImporter : ResourceImporter
	{

		public override string FileDescriptor
		{
			get { return "texture"; }
		}

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(Texture), typeof(Texture2D)}; }
        }

        public override int Priority
        {
            get { return 100; }
        }

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			if (typeof(T) != typeof(Glorg2.Graphics.OpenGL.Texture2D) && typeof(T) != typeof(Glorg2.Graphics.OpenGL.Texture))
				throw new System.IO.InvalidDataException("Invalid file format");

			Graphics.OpenGL.Texture2D tex = new Glorg2.Graphics.OpenGL.Texture2D(source, source_name);
			return tex as T;
		}
	}
}
