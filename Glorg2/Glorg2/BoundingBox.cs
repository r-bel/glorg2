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

	public class Box
	{
		internal Vector3[] Points;

		public Box(Vector3 size, Vector3 pos)
		{
			Points = new Vector3[4];
		}
	}
	[Serializable()]
	public struct BoundingBox
	{
		public Vector3 Position;
		public Vector3 Size;


		public BoundingBox Scale(float scale)
		{
			return new BoundingBox()
			{
				Position = this.Position,
				Size = this.Size * scale
			};
		}
		public BoundingBox Scale(Vector3 scale)
		{
			return new BoundingBox()
			{
				Position = this.Position,
				Size = this.Size * scale
			};
		}

		

		public List<Vector3> Points
		{
			get
			{
				List<Vector3> pts = new List<Vector3>();
				Vector3 s = Size / 2;
				pts.Add(Position + new Vector3(-s.x, -s.y, s.z));
				pts.Add(Position + new Vector3(s.x, -s.y, s.z));
				pts.Add(Position + new Vector3(-s.x, s.y, s.z));
				pts.Add(Position + new Vector3(s.x, s.y, s.z));
				pts.Add(Position + new Vector3(-s.x, -s.y, -s.z));
				pts.Add(Position + new Vector3(s.x, -s.y, -s.z));
				pts.Add(Position + new Vector3(-s.x, s.y, -s.z));
				pts.Add(Position + new Vector3(s.x, s.y, -s.z));
				return pts;
			}
		}

		public float Volume
		{
			get
			{
				return Size.x * Size.y * Size.z;
			}
		}
		public float SurfaceArea
		{
			get
			{
				return 2 * (Size.x * Size.x + Size.y * Size.y + Size.z * Size.z);
			}
		}

		

		public bool Intersects(BoundingBox other)
		{
			Vector3 sz = this.Size / 2;
			Vector3 os = other.Size / 2;
			return (Position.x + sz.x > other.Position.x - os.x && Position.x + sz.x < other.Position.x + os.x
				|| Position.x - sz.x > other.Position.x - os.x && Position.x - sz.x < other.Position.x + os.x) &&
					(Position.y + sz.y > other.Position.y - os.y && Position.y + sz.y < other.Position.y + os.y
				|| Position.y - sz.y > other.Position.y - os.y && Position.y - sz.y < other.Position.y + os.y) &&
					(Position.z + sz.z > other.Position.z - os.z && Position.z + sz.z < other.Position.z + os.z
				|| Position.z - sz.z > other.Position.z - os.z && Position.z - sz.z < other.Position.z + os.z);

		}

	}
}
