/*
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
