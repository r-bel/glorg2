using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL;
namespace Glorg2.Graphics
{
	[Serializable()]
	public struct VertexPositionNormal
	{
		public Vector3 Position;
		public Vector3 Normal;

		public VertexPositionNormal(Vector3 pos, Vector3 norm)
		{
			Position = pos;
			Normal = norm;
		}

		public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(
			new ElementType[] { 
				OpenGL.ElementType.Float | ElementType.Position | ElementType.ThreeDimension | ElementType.Bits32,
				OpenGL.ElementType.Float | ElementType.Normals | ElementType.ThreeDimension | ElementType.Bits32
			},typeof(VertexPositionNormal));
	}
}
