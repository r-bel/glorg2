using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics;
namespace Glorg2.Graphics.MeshBuilders
{
	[Serializable()]
	public class SphereBuilder : MeshBuilder
	{
		int sides;
		float radius;
		public int Sides { get { return sides; } set { sides = value; } }
		public float Radius { get { return radius; } set { radius = value; } }

		public override Model Build()
		{
			Model ret = new Model();

			VertexBuffer<VertexPositionTexCoordNormal> vb = new VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
			//IndexBuffer<uint> ib = new IndexBuffer<uint>();

			float ang_inc = (float)(2 * Math.PI) / sides;
			int vsides = sides / 2 - 2;
            float ang = 0;
			vb.Allocate(sides * (sides / 2 - 2) + 2);
			int index = 0;
			for (int i = 0; i < sides; i++, ang += ang_inc)
			{
				float cs = (float)Math.Cos(ang);
				float ss = (float)Math.Sin(ang);
				
				float sang_inc = (float)Math.PI / vsides;
                float sang = -((float)Math.PI / 2) + sang_inc;
				for (int j = 0; j < vsides; j++, sang += sang_inc, ++index)
				{
					float vcs = (float)Math.Cos(sang);
					float vss = (float)Math.Sin(sang);
					Vector3 pos = new Vector3(
						cs * radius * vcs,
						vss * radius,
						ss * radius * vss);
					vb[index] = new VertexPositionTexCoordNormal()
					{
						Position = pos,
						TexCoord = new Vector2(),
						Normal = new Vector3(cs * vcs, vss, ss * vss)
					};
				}
				vb[index] = new VertexPositionTexCoordNormal()
				{
					Position = new Vector3(0, -radius, 0),
					Normal = new Vector3(0, -1, 0)
				};
				vb[index + 1] = new VertexPositionTexCoordNormal()
				{
					Position = new Vector3(0, radius, 0),
					Normal = new Vector3(0, radius, 0)
				};
			}
            ret.VertexBuffer = vb;
            vb.BufferData(Glorg2.Graphics.OpenGL.VboUsage.GL_STATIC_DRAW);
			return ret;
		}
	}
}
