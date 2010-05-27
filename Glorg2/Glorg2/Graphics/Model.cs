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

namespace Glorg2.Graphics
{
	using Vertex = VertexPositionTexCoordNormal;
	public sealed class ModelPart
	{
		OpenGL.IndexBuffer<uint> ib;
		Material material;
		int vertex_start;
		int vertex_count;
		string name;

		public int StartVertex { get { return vertex_start; } set { vertex_start = value; } }
		public int VertexCount { get { return vertex_count; } set { vertex_count = value; } }
		public string Name { get { return name; } set { name = value; } }

		public Material Material { get { return material; } set { material = value; } }
		public OpenGL.IndexBuffer<uint> IndexBuffer { get { return ib; } set { ib = value; } }
	}

	public sealed class Model : Resource.Resource
	{
		[NonSerialized()]
		OpenGL.VertexBuffer<Vertex> vb;
		
		[NonSerialized()]
		List<ModelPart> parts;

		internal BoundingBox bounds;
		public OpenGL.VertexBuffer<Vertex> VertexBuffer { get { return vb; } set { vb = value; } }
		public List<ModelPart> Parts { get { return parts; } }

		public BoundingBox BoundingBox { get { return bounds; } set { bounds = value; } }

		public Model()
		{
			parts = new List<ModelPart>();
		}

		#region IDisposable Members

		public override void DoDispose()
		{
			vb.Dispose();
			foreach (var p in parts)
			{
				p.IndexBuffer.Dispose();
			}
		}

		#endregion

        public void GenerateNormals()
        {
            Vector3[] norms = new Vector3[vb.Count];
            int[] count = new int[vb.Count];

            foreach (var p in parts)
            {
                for (int i = 0; i < p.IndexBuffer.Count; i += 3)
                {
                    int i1= (int)p.IndexBuffer[i];
                    int i2= (int)p.IndexBuffer[i + 1];
                    int i3= (int)p.IndexBuffer[i + 2];
                    var v1 = vb[i1].Position;
                    var v2 = vb[i2].Position;
                    var v3 = vb[i3].Position;

                    var c1 = v2 - v1;
                    var c2 = v3 - v1;
                    var cross = Vector3.Cross(c1, c2).Normalize();
                    norms[i1] += cross;
                    norms[i2] += cross;
                    norms[i3] += cross;
                    ++count[i1];
                    ++count[i2];
                    ++count[i3];
                }
            }
            for (int i = 0; i < norms.Length; i++)
            {
                VertexPositionTexCoordNormal n = vb[i];
                n.Normal = norms[i] / count[i];
                vb[i] = n;
            }
        }
    }
}
