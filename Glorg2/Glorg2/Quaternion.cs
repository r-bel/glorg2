using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	/// <summary>
	/// Quaternions are structures used for generating rotations that does not result in Gimbal locks.
	/// </summary>
	/// <remarks>Inspired by the article Rotations in Three Dimension Part Five: Quaternions by Confuted. 
	/// http://www.cprogramming.com/tutorial/3d/quaternions.html</remarks>
	public struct Quaternion
	{
		public float x, y, z, w;

		public static readonly Quaternion Identity = new Quaternion(
			1,
			0,
			0,
			0
		);

		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x; this.y = y; this.z = z; this.w = w;
		}

		public Quaternion(Vector3 axis, float angle)
		{
			float angle2 = angle / 2;
			float sinf = (float)Math.Sin(angle2);
			w = (float)Math.Cos(angle2);
			x = axis.x * sinf;
			y = axis.y * sinf;
			z = axis.z * sinf;
		}
		/// <summary>
		/// Create quaternion from euler angles
		/// </summary>
		/// <param name="x_angle">X angle in radians</param>
		/// <param name="y_angle">Y angle in radians</param>
		/// <param name="z_angle">Z angle in radians</param>
		public Quaternion(float x_angle, float y_angle, float z_angle)
		{
			float cx = (float)Math.Cos(.5f * x_angle);
			float cy = (float)Math.Cos(.5f * y_angle);
			float cz = (float)Math.Cos(.5f * z_angle);

			float sx = (float)Math.Sin(0.5 * x_angle);
			float sy = (float)Math.Sin(0.5 * y_angle);
			float sz = (float)Math.Sin(0.5 * z_angle);
			
			x = cz * cy * sx - sz * sy * cx;
			y = cz * sy * cx + sz * cy * sx;
			z = sz * cy * cx - cz * sy * sx;
			w = cz * cy * cx + sz * sy * sx;
		}

		public static Quaternion  operator -(Quaternion q)
		{
			return new Quaternion(-q.x, -q.y, -q.z, q.w);
		}

		public Quaternion Invert()
		{
			float l = x * x + y * y + z * z + w * w;
			return new Quaternion(-x / l, -y / l, -z / l, w / l);
		}

		/// <summary>
		/// Returns the magnitude of this quaternion
		/// </summary>
		public float Magnitude
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
			}
		}
		/// <summary>
		/// Retrieves if the quaternion is a unit quaternion
		/// This should return true for all valid quaternions, if it does not, the quaternion should be normalized.
		/// </summary>
		public bool IsUnit
		{
			get
			{
				float val = x * x + y * y + z * z + w * w;
				return val > 0.9998f && val < 1.0001f;
			}
		}
		/// <summary>
		/// Normalizes a quaternion to ensure it is a unit quaternion
		/// </summary>
		/// <returns>Returns a unit quaternion</returns>
		public Quaternion Normalize()
		{
			float mag = Magnitude;
			return new Quaternion(x / mag, y / mag, z / mag, w / mag);
		}
		public static Quaternion operator *(Quaternion a, Quaternion b)
		{
			return new Quaternion(
				  a.y*b.z - a.z*b.y + a.w*b.x + a.x*b.w,
				  a.z*b.x - a.x*b.z + a.w*b.y + a.y*b.w,
				  a.x*b.y - a.y*b.x + a.w*b.z + a.z*b.w,
				  a.w*b.w - Vector3.Dot(new Vector3(a.x, a.y, a.z), new Vector3(b.x, b.y, b.z)));
		}
		/// <summary>
		/// Creates a new matrix representing the quaternion
		/// </summary>
		/// <returns></returns>
		public Matrix ToMatrix()
		{
			return new Matrix()
			{
				m11 = 1 - 2*(y * y - z*z),
				m12 = 2 * (x * y -  w * z),
				m13 = 2 * (x * z + w * y),
				m14 = 0,
				m21 = 2 * (x * y + w * z),
				m22 = 1 - 2*(x*x - z*z),
				m23 = 2 * (y*z-w*x),
				m24 = 0,
				m31 = 2 * (x * z - w * y),
				m32 = 2 * (y * x - w * x),
				m33 = 1 - 2 * x*x - 2 * y * y,
				m34 = 0,
				m41 = 0,
				m42 = 0,
				m43 = 0,
				m44 = 1
			};
		}
	}
}
