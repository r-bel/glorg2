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

	public struct BoundingBox
	{
		public Vector3 Position;
		public Vector3 Size;

		

		

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
