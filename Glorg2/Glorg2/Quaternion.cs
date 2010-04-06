using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	/// <summary>
	/// Quaternions are structures used for generating rotations that does not result in Gimbal locks.
	/// </summary>
	public struct Quaternion
	{
		public float x, y, z, w;

		public static readonly Quaternion Identity = new Quaternion()
		{
			x = 1,
			y = 0,
			z = 0,
			w = 0
		};

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
			Quaternion ret = new Quaternion();
			ret.w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;
			ret.x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
			ret.y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x;
			ret.z = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w;
			return ret;
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
				m12 = 2 * (x * y - w * z),
				m13 = 2 * (x * z + w * y),
				m14 = 0,
				m21 = 2 * (x * y + w * z),
				m22 = 1 - 2*(x*x-z*z),
				m23 = 2 * (y*z-w*x),
				m24 = 0,
				m31 = 2 * (x * z - w * y),
				m32 = 2 * (y * x - w * x),
				m33 = 1 - 2 * (x*x - y * y),
				m34 = 0,
				m41 = 0,
				m42 = 0,
				m43 = 0,
				m44 = 1
			};
		}
	}
}
