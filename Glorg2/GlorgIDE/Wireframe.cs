using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
using Glorg2.Scene;

namespace GlorgIDE
{
	public struct WireframeVertex
	{
		public static readonly VertexBufferDescriptor Description = new VertexBufferDescriptor(new ElementType[] { ElementType.Position3Float, ElementType.Color4Floats }, typeof(WireframeVertex));
		public Vector3 Position;
		public Vector4 Color;
	}

	public class Wireframe : IRenderable, IDisposable
	{
		VertexBuffer<WireframeVertex> vb;
		StdMaterial mat;

		public int Rows { get; set; }
		public int Columns { get; set; }
		public int Major { get; set; }
		public float Size { get; set; }

		public Vector4 MinorColor { get; set; }
		public Vector4 MajorColor { get; set; }

		#region IRenderable Members

		public void Render(float time, GraphicsDevice dev)
		{
			dev.SetActiveMaterial(mat);
			dev.SetVertexBuffer(vb);
			dev.Draw(DrawMode.Lines);
			dev.SetVertexBuffer(null);
			dev.SetActiveMaterial(null);
		}

		public void InitializeGraphics()
		{
			if (vb != null)
				vb.Dispose();

			if (mat == null)
			{
				Glorg2.Resource.MaterialImporter imp = new Glorg2.Resource.MaterialImporter();
				using(var stream = System.IO.File.OpenRead(".\\shaders\\Grid.mxl"))
				{
					mat = imp.Import<StdMaterial>(stream, "grid", null);
				}
			}
			vb = new VertexBuffer<WireframeVertex>(WireframeVertex.Description);

			vb.Allocate(Columns * 2 + Rows * 2 + 4);
			float tot_w = Size * Columns;
			float tot_h = Size * Rows;
			float fi = -tot_w / 2;
			int i;
			for (i = 0; i <= Columns; i++, fi += Size)
			{
				Vector4 color;
				if(Major == 0 || (i % Major) == 0)
					color = MajorColor;
				else
					color = MinorColor;
				vb[i * 2]     = new WireframeVertex() { Position = new Vector3(fi, 0, -tot_h / 2), Color = color };
				vb[i * 2 + 1] = new WireframeVertex() { Position = new Vector3(fi, 0,  tot_h / 2), Color = color };
			}
			int start = i * 2;
			fi = -tot_h / 2;
			for (int j = 0; j <= Rows; j++, fi += Size)
			{
				Vector4 color;
				if (Major == 0 || (j % Major) == 0)
					color = MajorColor;
				else
					color = MinorColor;
				vb[start + j * 2]     = new WireframeVertex() { Position = new Vector3(-tot_h / 2, 0, fi), Color = color };
				vb[start + j * 2 + 1] = new WireframeVertex() { Position = new Vector3( tot_h / 2, 0, fi), Color = color };
			}
			vb.BufferData(VboUsage.GL_STATIC_DRAW);
			vb.FreeClientData();
		}

		public bool GraphicsInitialized
		{
			get { return vb != null; }
		}

		#endregion

		public void Dispose()
		{
			if (vb != null)
				vb.Dispose();
			vb = null;
			if(mat != null)
				mat.Dispose();
		}
	}
}
