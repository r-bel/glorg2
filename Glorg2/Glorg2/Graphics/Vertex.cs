/*
Copyright (C) 2010 Henning Moe

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
	public struct VertexPositionColor
	{
		public Vector3 Position;
		public Vector4 Color;

		public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(
			new ElementType[]
			{
				ElementType.Position3Float,
				ElementType.Color4Floats
			}, typeof(VertexPositionColor));
	}
}
