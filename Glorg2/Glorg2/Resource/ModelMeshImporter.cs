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
using System.IO;

namespace Glorg2.Resource
{
	public class ModelMeshImporter : ResourceImporter
	{

		private struct Chunk
		{
			public short id;
			public short version;
			public int size;
		}

        private static readonly Type[] supported_types = new Type[] { typeof(Glorg2.Graphics.Model) };

        public override IEnumerable<Type> SupportedTypes
        {
            get { return supported_types; }
        }

		public override string FileDescriptor
		{
			get { return "model.mesh"; }
		}
		private Chunk ReadChunk(System.IO.BinaryReader rd)
		{
			return new Chunk()
			{
				id = rd.ReadInt16(),
				version = rd.ReadInt16(),
				size = rd.ReadInt32()
			};
		}

        public override int Priority
        {
            get { return 100; }
        }

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			Glorg2.Graphics.Model ret = new Graphics.Model();
			ret.VertexBuffer = new Graphics.OpenGL.VertexBuffer<Graphics.VertexPositionTexCoordNormal>(Graphics.VertexPositionTexCoordNormal.Descriptor);
			System.IO.BinaryReader rd = new System.IO.BinaryReader(source);

			Chunk c = ReadChunk(rd);

			return ret as T;
		}
	}
}
