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
		OpenGL.Material material;
		int vertex_start;
		int vertex_count;
		string name;

		public int StartVertex { get { return vertex_start; } set { vertex_start = value; } }
		public int VertexCount { get { return vertex_count; } set { vertex_count = value; } }
		public string Name { get { return name; } set { name = value; } }

		public OpenGL.Material Material { get { return material; } set { material = value; } }
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

		public BoundingBox BoundingBox { get { return bounds; } }

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
	}
}
