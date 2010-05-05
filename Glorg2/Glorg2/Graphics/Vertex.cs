using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL;
namespace Glorg2.Graphics
{
	/// <summary>
	/// Defines a common vertex format with 3-dimensional vector for both position and normals
	/// </summary>
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
				ElementType.Position3Float,
				ElementType.Normal3Float
			},typeof(VertexPositionNormal));
	}
	/// <summary>
	/// Defines a common vertex format with three dimensional vector for position and color, and two dimensional vector for texture coordinates.
	/// </summary>
	[Serializable()]
	public struct VertexPositionTexCoordNormal
	{
		public Vector3 Position;
		public Vector3 Normal;
		public Vector2 TexCoord;

		public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(
			new ElementType[] 
			{
				ElementType.Position3Float,
				ElementType.Normal3Float,
				ElementType.TexCoord2Float
			}, typeof(VertexPositionTexCoordNormal));

	}
}
