using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.MeshBuilders
{
	[Serializable()]
	public class CylinderBuilder : MeshBuilder
	{
		private float radius;
		private float height;
		private float taper;
		private int sides;

		public float Radius { get { return radius; } set { radius = value; } }
		public float Height { get { return height; } set { height = value; } }
		public float Taper { get { return taper; } set { taper = value; } }
		public int Sides { get { return sides; } set { sides = value; } }

		public override Model Build()
		{
			Model ret = new Model();
			Glorg2.Graphics.OpenGL.VertexBuffer<VertexPositionTexCoordNormal> vb;
			Glorg2.Graphics.OpenGL.IndexBuffer<uint> ib;

			vb = new OpenGL.VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
			ib = new OpenGL.IndexBuffer<uint>();

			int vertices = sides * 4 + 2;
			int indices = sides * 12;

			vb.Allocate(vertices);
			ib.Allocate(indices);

			float ang_inc = (float)((Math.PI * 2) / sides);
			float ang = (float)Math.PI / 2;
			float h2 = height / 2;
			Vector3 up = new Vector3(0, 1, 0);
			Vector3 dn = new Vector3(0, -1, 0);
			vb[vertices - 1] = new VertexPositionTexCoordNormal()
			{
				Normal = Vector3.Up,
				Position = new Vector3(0, h2, 0),
				TexCoord = new Vector2(.5f, .5f)
			};
			vb[vertices - 2] = new VertexPositionTexCoordNormal()
			{
				Normal = Vector3.Down,
				Position = new Vector3(0, -h2, 0),
				TexCoord = new Vector2(.5f, .5f)
			};
			int index = 0;
			for (int i = 0; i < sides; i++, ang += ang_inc)
			{
				
				float cs = (float)Math.Cos(ang);
				float ss = (float)Math.Sin(ang);
				//float x = cs * radius;
				//float z = ss * radius;
				Vector3 n = new Vector3(cs, 0, ss);
				Vector3 top = new Vector3(cs * (radius + taper), h2, ss * (radius + taper));
				Vector3 bot = new Vector3(cs * (radius - taper), -h2, ss * (radius - taper));



				int i4 = i * 4;
				vb[i4] = new VertexPositionTexCoordNormal()
				{
					Normal = Vector3.Up,
					Position = top,
					TexCoord = new Vector2(i / (float)sides, 0)					
				};
				vb[i4 + 1] = new VertexPositionTexCoordNormal()
				{
					Normal = n,
					Position = top,
					TexCoord = new Vector2(i / (float)sides, 0)
				};
				vb[i4 + 2] = new VertexPositionTexCoordNormal()
				{
					Normal = n,
					Position = bot,
					TexCoord = new Vector2(i / (float)sides, 0)
				};
				vb[i4 + 3] = new VertexPositionTexCoordNormal()
				{
					Normal = Vector3.Down,
					Position = bot,
					TexCoord = new Vector2(i / (float)sides, 0)
				};
				// Top 
				ib[index++] = (uint)(vertices - 1);
				ib[index++] = (uint)((i4 + 4) % (vertices - 2));
				ib[index++] = (uint)(i4);
				// Side
				ib[index++] = (uint)(i4 + 1);
				ib[index++] = (uint)((i4 + 4 + 1) % (vertices - 2));
				ib[index++] = (uint)((i4 + 4 + 2) % (vertices - 2));

				// Side
				ib[index++] = (uint)(i4 + 1);
				ib[index++] = (uint)((i4 + 4 + 2) % (vertices - 2));
				ib[index++] = (uint)(i4 + 2);

				// Bottom
				ib[index++] = (uint)(vertices - 2);
				ib[index++] = (uint)(i4 + 3);
				ib[index++] = (uint)((i4 + 4 + 3) % (vertices - 2));
			}

			vb.BufferData(OpenGL.OpenGL.VboUsage.GL_STATIC_DRAW_ARB);
			ib.BufferData(OpenGL.OpenGL.VboUsage.GL_STATIC_DRAW_ARB);

			ret.VertexBuffer = vb;
			ret.Parts.Add(new ModelPart() { IndexBuffer = ib });

			return ret;
		}
	}
}
