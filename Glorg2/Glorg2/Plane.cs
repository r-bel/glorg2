using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	[Serializable()]
	public struct Plane
	{
		public Vector3 Normal;
		public float Distance;

		public float GetDistance(Vector3 p)
		{
			return Vector3.Dot(p, Normal) - Distance;
		}
	}
}
