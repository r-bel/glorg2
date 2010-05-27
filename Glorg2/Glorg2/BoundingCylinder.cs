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

namespace Glorg2
{
	[Serializable()]
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
