using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics.OpenGL.Shaders;

namespace Glorg2.Scene
{
	[Serializable()]
	public class SkyDome : Node, IRenderable
	{

		[Serializable()]
		public struct SkydomeVertex
		{
			public Vector3 Position;
			//public Vector2 TexCoord;
			public Vector3 Normal;

			public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(new ElementType[] { ElementType.Position3Float, ElementType.Normal3Float }, typeof(SkydomeVertex));
		}
		[NonSerialized()]
		StdMaterial mat;
		[NonSerialized()]
		VertexBuffer<SkydomeVertex> vb;
		[NonSerialized()]
		IndexBuffer<uint> ib;

		private bool init_finished;

		public bool GraphicsInitialized { get { return init_finished; } }

		public override void DoDispose()
		{
			mat.Dispose();
			vb.Dispose();
			ib.Dispose();
		}

		#region IRenderable Members

		public void Render(float time, GraphicsDevice dev)
		{
			dev.State.DepthTest = false;
			dev.SetActiveMaterial(mat);
			dev.SetVertexBuffer(vb);
			dev.SetIndexBuffer(ib);
			var matrix = dev.ModelviewMatrix;
			matrix.Translation = new Vector4(0, 0, 0, 1);
			dev.ModelviewMatrix = matrix;
			dev.Draw(DrawMode.Triangles);
			dev.SetIndexBuffer(null);
			dev.SetVertexBuffer(null);
			dev.State.DepthTest = true;
		}

		private const int Segments = 32;

		private void GenerateDome()
		{
			int vertices = Segments * Segments * 2 + 1;
			int indices = Segments * 6 + (Segments - 1) * 2 * Segments * 6 ;


			vb = new VertexBuffer<SkydomeVertex>(SkydomeVertex.Descriptor);
			ib = new IndexBuffer<uint>();
			vb.Allocate(vertices);
			ib.Allocate(indices);
			int index = 0;

			float vang_inc = (float)(Math.PI / 2) / Segments;
			float hang_inc = (float)(2 * Math.PI) / Segments;

			float hang = hang_inc;
			float vang = vang_inc;

			for (int i = 0; i < Segments; i++, vang += vang_inc)
			{
				float v_c = (float)Math.Cos(vang);
				float v_s = (float)Math.Sin(vang);
				for (int j = 0; j < Segments * 2; j++, hang += hang_inc)
				{
					float h_c = (float)Math.Cos(hang);
					float h_s = (float)Math.Sin(hang);
					Vector3 pos = new Vector3(h_c * v_s, v_c, h_s * v_s);
					vb[index++] = new SkydomeVertex()
					{
						Position = pos,
						Normal = -pos.Normalize()
					};
				}
				hang = 0;
			}
			vb[index] = new SkydomeVertex()
			{
				Normal = Vector3.Down,
				Position = new Vector3(0, 1f, 0)
			};
			vb.BufferData(VboUsage.GL_STATIC_DRAW);
			vb.FreeClientData();
			index = 0;
			for (int i = 0; i < Segments * 2; i++)
			{
				ib[index++] = (uint)vertices - 1;
				ib[index++] = (uint)i;
				ib[index++] = (uint)(i + 1) % (Segments * 2); // Wraparound
				
			}
			for (int i = 0; i < Segments - 1; i++)
			{
				for (int j = 0; j < Segments * 2; j++)
				{
					int offset_1 = i * Segments * 2;
					int offset_2 = i * Segments * 2 + Segments * 2;
					int j2 = (j + 1) % (Segments * 2);
					
					ib[index++] = (uint)(offset_1 + j);
					ib[index++] = (uint)(offset_2 + j);
					ib[index++] = (uint)(offset_1 + j2);
					
					ib[index++] = (uint)(offset_2 + j);
					ib[index++] = (uint)(offset_2 + j2);
					ib[index++] = (uint)(offset_1 + j2);
				}
			}
			ib.BufferData(VboUsage.GL_STATIC_DRAW);
			ib.FreeClientData();
		}


		public void InitializeGraphics()
		{
			Owner.Resources.Load("SkydomeMaterial", out mat);
			GenerateDome();
			init_finished = true;
		}

		#endregion
	}
}
