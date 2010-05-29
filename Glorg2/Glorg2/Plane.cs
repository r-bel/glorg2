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
	public struct Plane
	{
		public Vector3 Normal { get; set; }
		public Vector3 Position { get; set; }
		public float Distance { get { return Position.Length; } set { Position = Normal * value; } }

		public float GetDistance(Vector3 p)
		{
			return Vector3.Dot(p - Position, Normal);
		}

		public static Plane FromPoints(Vector3 p1, Vector3 p2, Vector3 p3)
		{
			var normal = Vector3.Cross(p2 - p1, p3 - p1);
			var pos = (p1 + p2 + p3) / 3;
			return new Plane()
			{
				Normal = normal,
				Position = pos,
			};
		}
	}
}
