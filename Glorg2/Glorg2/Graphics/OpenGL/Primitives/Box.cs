using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Primitives
{
	public sealed class Box : Primitive
	{
		protected override void Build()
		{
			vb = new VertexBuffer<Vector3>(Vector3.Descriptor);

			vb.Allocate(8);
			ib = new IndexBuffer<ushort>();
			ib.Allocate(4 * 6);

			vb[0] = new Vector3(-0.5f, -0.5f, -0.5f);
			vb[1] = new Vector3(0.5f, -0.5f, -0.5f);
			vb[2] = new Vector3(0.5f, 0.5f, -0.5f);
			vb[3] = new Vector3(-0.5f, 0.5f, -0.5f);

			vb[4] = new Vector3(-0.5f, -0.5f, 0.5f);
			vb[5] = new Vector3(0.5f, -0.5f, 0.5f);
			vb[6] = new Vector3(0.5f, 0.5f, 0.5f);
			vb[7] = new Vector3(-0.5f, 0.5f, 0.5f);

			ib[0] = 0;
			ib[1] = 1;
			ib[2] = 2;
			ib[3] = 3;

			ib[4] = 4;
			ib[5] = 0;
			ib[6] = 3;
			ib[7] = 7;

			ib[8] = 5;
			ib[9] = 4;
			ib[10] = 7;
			ib[11] = 6;

			ib[12] = 1;
			ib[13] = 5;
			ib[14] = 6;
			ib[15] = 2;

			ib[16] = 6;
			ib[17] = 7;
			ib[18] = 3;
			ib[19] = 2;

			ib[20] = 0;
			ib[21] = 4;
			ib[22] = 5;
			ib[23] = 1;

			vb.BufferData(OpenGL.VboUsage.GL_STATIC_DRAW_ARB);
			ib.BufferData(OpenGL.VboUsage.GL_STATIC_DRAW_ARB);
			// Client data is no longer needed. Free it!
			vb.FreeClientData();
			ib.FreeClientData();
		}
		public override DrawMode DrawMode
		{
			get { return Graphics.DrawMode.Quads; }
		}
	}
}
