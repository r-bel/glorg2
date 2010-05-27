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
	public struct BoundingSphere
	{
		public Vector3 Position;
		public float Radius;

		/// <summary>
		/// Gets or sets the surface area of the sphere
		/// </summary>
		public float Volume
		{
			get
			{
				return (float)((4.0 / 3.0) * Math.PI * Radius * Radius * Radius);
			}
			set
			{
				Radius = (float)Math.Sqrt(Math.Sqrt((value / ((4.0 / 3.0) * Math.PI))));
			}
		}
		/// <summary>
		/// Gets or sets the surface area of the sphere.
		/// </summary>
		public float SurfaceArea
		{
			get
			{
				return (float)(4 * Math.PI * Radius * Radius);
			}
			set
			{
				Radius = (float)Math.Sqrt(value / (4 * Math.PI));
			}
		}

		/// <summary>
		/// Check if this sphere intersects another sphere
		/// </summary>
		/// <param name="other">Other sphere to check</param>
		/// <param name="intersectionplane">Plane of intersection</param>
		/// <returns>True if the spheres intersects, otherwise returns false</returns>
		public bool Intersect(BoundingSphere other, ref Plane intersectionplane)
		{
			if (Math.Abs((other.Position - Position).Length) < (Radius + other.Radius))
			{
				// Spheres intersects
				//Plane ret = new Plane();
				//ret.Position = Position + (other.Position - Position) / 2;
				//ret.Normal = (other.Position - Position).Normalize().ToVector3();
				return true;
			}
			else
				return false;
		}
	}




}
