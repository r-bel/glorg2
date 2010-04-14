using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Primitives
{
	public abstract class Primitive
	{
		protected internal VertexBuffer<Vector3> vb;
		protected internal IndexBuffer<ushort> ib;

		protected abstract void Build();
		public abstract DrawMode DrawMode { get; }

		
	}
}
