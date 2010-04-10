using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	public class BoundingCylinder
	{
		public Vector4 Position;
		public float Radius;
		public float Height;

		public float Volume
		{
			get
			{
				return (float)(Math.PI * Radius * Radius * Height);
			}
		}
		public float SurfaceArea
		{
			get
			{
				return (float)(2 * Math.PI * Radius * Radius + 2 * Math.PI * Radius);
			}
		}
		public bool Intersects(BoundingCylinder other)
		{
			float h2 = Height / 2;
			float oh2 = other.Height / 2;
			return (Position.x + Radius < other.Position.x + other.Radius && Position.x + Radius > other.Position.x - other.Radius
				|| Position.x - Radius > other.Position.x - other.Radius && Position.x + Radius < other.Position.x + other.Radius) &&
					(Position.z + Radius < other.Position.z + other.Radius && Position.z + Radius > other.Position.z - other.Radius
				|| Position.z - Radius > other.Position.z - other.Radius && Position.z + Radius < other.Position.z + other.Radius) &&
					(Position.y + h2 < other.Position.y + oh2 && Position.y + h2 > other.Position.y - oh2
				|| Position.y - h2 > other.Position.y - oh2 && Position.y + h2 < other.Position.y + oh2);
		}

	}
}
