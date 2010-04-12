using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	[Serializable()]
	public struct BoundingFrustum
	{
		public Plane[] Sides;
		public Vector3 Position;

		public BoundingFrustum(Matrix projection_matrix)
		{
			Sides = new Plane[6];
			Position = new Vector3();
		}

		public Intersection Intersects(BoundingSphere sphere)
		{
			if (Sides == null)
				throw new InvalidOperationException("Bounding frustum has an invalid format.");
			float distance;

			for (int i = 0; i < Sides.Length; ++i)
			{
				distance = Vector3.Dot(Sides[i].Normal, sphere.Position) + Sides[i].Distance;

				if (distance < -sphere.Radius)
					return  Intersection.None;

				if (Math.Abs(distance) < sphere.Radius)
					return Intersection.Intersect;
			}

			return (Intersection.Contains);
		}

		public Intersection Intersects(BoundingBox box)
		{
			throw new NotImplementedException();

		}
		public Intersection Intersects(Vector3 point)
		{
			int sum;
			sum = Sides.Sum(plane => plane.GetDistance(point) < 0 ? 1 : 0);
			if (sum == 6)
				return Intersection.Contains;
			else
				return Intersection.None;					
		}
	}
	public enum Intersection
	{
		None,
		Intersect,
		Contains
	}
}
