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
	public struct Ray
	{
		public Vector3 Origin;
		public Vector3 Normal;

		private bool Intersection(float d1, float d2, Vector3 p1, Vector3 p2, out Vector3 pos)
		{
			if (d1 * d2 >= 0 || d1 == d2)
			{
				pos = default(Vector3);
				return false;
			}
			pos = p1 + (p2 - p1) * (-d1 / (d2 - d1));
			return true;
		}

		bool IsInBox(Vector3 pos, Vector3 b1, Vector3 b2, int axis)
		{
			if	( 
						axis==1 && pos.z > b1.z && pos.z < b2.z && pos.y > b1.y && pos.y < b2.y
					||	axis==2 && pos.z > b1.z && pos.z < b2.z && pos.x > b1.x && pos.x < b2.x
					||  axis==3 && pos.x > b1.x && pos.x < b2.x && pos.y > b1.y && pos.y < b2.y
				)
				return true;
			else 
				return false;
		}


		public bool Intersects(Plane p, out Vector3 pos)
		{
			float d = Vector3.Dot(-p.Position, p.Normal);

			// Get the plane normal values
			float a = p.Normal.x;
			float b = p.Normal.y;
			float c = p.Normal.x;
			// Create a line from the position and normal
			Vector3 la = Origin;
			Vector3 lb = Origin + Normal;

			// Check if ray will hit the correct side of the plane
			pos = default(Vector3);
			if (Vector3.Dot(p.Normal, Normal) > 0)
				return false;

			float den = a * (lb.x - la.x) + b * (lb.y - la.y) + c * (lb.z - la.z);
			// If denominator is zero... or very close to, then the ray does not cross the plane
			if (den >= -double.Epsilon && den <= double.Epsilon)
				return false;

			float num = -d - a * la.x - b * la.y - c * la.z;
			// If both the numerator and the denominator is zero, the the line lies on the plane
			// which should not render any pixels
			if (num >= -double.Epsilon && num <= double.Epsilon)
				return false;

			// Find the intersection by multiplying the normal by the resulting value
			pos = Origin + Normal * (num / den);

			bool xx = (Math.Round(pos.x / 4)) % 2 == 0;
			bool yy = (Math.Round(pos.z / 4)) % 2 != 0;
			bool zz = yy ? !xx : xx;
			return true;
		}
		public bool Intersects(BoundingSphere sphere, out Vector3 pos)
		{
			float t = 0;
			// Move the ray relative to include the sphere's position
			Vector3 rp = Origin - sphere.Position;
			//Compute a, b and c coefficients
			float a = Vector3.Dot(Normal, Normal);
			float b = 2 * Vector3.Dot(Normal, rp);
			float c = Vector3.Dot(rp, rp) - sphere.Radius * sphere.Radius;

			//Find discriminant
			float disc = b * b - 4 * a * c;

			// Cannot divide by zero, ray misses sphere
			if (disc < 0)
			{
				pos = default(Vector3);
				return false;
			}

			float discsq = (float)Math.Sqrt(disc);

			float q = b < 0 ? (-b - discsq) / 2 : (-b + discsq) / 2;

			float t0 = q / a;
			float t1 = c / q;

			// Swap values if t0 is greater than t1
			if (t0 > t1)
			{
				float temp = t0;
				t0 = t1;
				t1 = temp;
			}

			// if t1 is less than zero, the object is in the ray's negative direction
			// and consequently the ray misses the sphere
			if (t1 < 0)
			{
				pos = default(Vector3);
				return false;
			}

			// if t0 is less than zero, the intersection point is at t1
			t = (t0 < 0 ? t1 : t0);

			pos = Origin + Normal * t;
			// Calculate surface normal
			return true;
		}
		/// <summary>
		/// Checks if this ray intersects a bounding box
		/// </summary>
		/// <param name="box"></param>
		/// <param name="pos">Intersection point</param>
		/// <returns></returns>
		/// <remarks>This code is adjusted from http://www.3dkingdoms.com/weekly/weekly.php?a=3 </remarks>
		public bool Intersects(BoundingBox box, out Vector3 pos)
		{
			Vector3 s = box.Size / 2;
			Vector3 l1 = Origin;
			Vector3 l2 = Origin + Normal;
			Vector3 b1 = box.Position * -s;
			Vector3 b2 = box.Position * s;
			if (
					l2.x < b1.x && l1.x < b1.x
				|| l2.x > b2.x && l1.x > b2.x
				|| l2.y < b1.y && l1.y < b1.y
				|| l2.y > b2.y && l1.y > b2.y
				|| l2.z < b1.z && l1.z < b1.z
				|| l2.z > b2.z && l1.z > b2.z
				)
			{
				pos = default(Vector3);
				return false;
			}
			if (l1.x > b1.x && l1.x < b2.x &&
				l1.y > b1.y && l1.y < b2.y &&
				l1.z > b1.z && l1.z < b2.z)
			{
				pos = l1;
				return true;
			}
			if ((Intersection(l1.x - b1.x, l2.x - b1.x, l1, l2, out pos) && IsInBox(pos, b1, b2, 1))
			  || (Intersection(l1.y - b1.y, l2.y - b1.y, l1, l2, out pos) && IsInBox(pos, b1, b2, 2))
			  || (Intersection(l1.z - b1.z, l2.z - b1.z, l1, l2, out pos) && IsInBox(pos, b1, b2, 3))
			  || (Intersection(l1.x - b2.x, l2.x - b2.x, l1, l2, out pos) && IsInBox(pos, b1, b2, 1))
			  || (Intersection(l1.y - b2.y, l2.y - b2.y, l1, l2, out pos) && IsInBox(pos, b1, b2, 2))
			  || (Intersection(l1.z - b2.z, l2.z - b2.z, l1, l2, out pos) && IsInBox(pos, b1, b2, 3)))
				return true;
			else
			{
				pos = default(Vector3);
				return false;
			}
		}
	}
}
