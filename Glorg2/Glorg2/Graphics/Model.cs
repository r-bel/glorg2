using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics
{
	public class ModelPart
	{
		OpenGL.IndexBuffer<int> ib;
		OpenGL.Material material;

		public OpenGL.Material Material { get { return material; } set { material = value; } }
		public OpenGL.IndexBuffer<int> IndexBuffer { get { return ib; } set { ib = value; } }
	}

	public sealed class Model : Resource.Resource
	{
		[NonSerialized()]
		OpenGL.VertexBuffer<VertexPositionTexCoordNormal> vb;
		
		[NonSerialized()]
		List<ModelPart> parts;

		public OpenGL.VertexBuffer<VertexPositionTexCoordNormal> VertexBuffer { get { return vb; } set { vb = value; } }
		public List<ModelPart> Parts { get { return parts; } }

		public Model()
		{
			parts = new List<ModelPart>();
		}
	}
}
