using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Primitives
{
	public class PrimitiveRenderer
	{
		List<Primitive> primitives;

		public List<Primitive> Primitives { get { return primitives; } }

		public PrimitiveRenderer()
		{
			primitives = new List<Primitive>();
		}

		public void Draw<T>(GraphicsDevice dev, Vector3 scale)
		{
			var prim = primitives.Find(p => p is T);
			if (prim != null)
			{
				dev.SetVertexBuffer(prim.vb);
				dev.SetIndexBuffer(prim.ib);
				dev.Draw(prim.DrawMode);
			}
		}
		public void DrawLines<T>(GraphicsDevice dev, Vector3 scale)
		{
			var prim = primitives.Find(p => p is T);
			if (prim != null)
			{

				dev.SetVertexBuffer(prim.vb);
				dev.SetIndexBuffer(prim.ib);
				dev.State.SetPolygonMode(CullFace.FrontAndBack, PolygonMode.Line);
				dev.Draw(prim.DrawMode);
				dev.State.SetPolygonMode(CullFace.FrontAndBack, PolygonMode.Fill);
			}
		}
	}
}
