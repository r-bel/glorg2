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
	[Serializable()]
	public struct Quaternion
	{
		public float x, y, z, w;

		public static readonly Quaternion Identity = new Quaternion(
			0,
			0,
			0,
			1
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

		public static Quaternion operator -(Quaternion q)
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
		/// Normalizes a quaternion if it is not a unit quaternion
		/// </summary>
		/// <returns>Returns a unit quaternion</returns>
		public Quaternion Normalize()
		{
			//if (!IsUnit)
			{
				float mag = Magnitude;
				if(!float.IsNaN(mag) && mag != 0f)
					return new Quaternion(x / mag, y / mag, z / mag, w / mag);
				else 
					return this;
			}
			//else
				//return this;
		}
		public static Quaternion operator +(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w).Normalize();
		}
		public static Quaternion operator *(Quaternion a, float b)
		{
			return new Quaternion(a.x, a.y, a.z, a.w * b);
		}
		public static Quaternion operator *(float a, Quaternion b)
		{
			return new Quaternion(b.x, b.y, b.z, b.w * a);
		}

		public static Quaternion operator *(Quaternion a, Quaternion b)
		{
            return new Quaternion()
            {
                x = a.y * b.z - a.z * b.y + a.w * b.x + a.x * b.w,
                y = a.z * b.x - a.x * b.z + a.w * b.y + a.y * b.w,
                z = a.x * b.y - a.y * b.x + a.w * b.z + a.z * b.w,
                w = a.w * b.w - Vector3.Dot((Vector3)a, (Vector3)b)
            };
                


            
            /*return new Quaternion()
            {
                w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z,
                x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
                z = a.w * b.z + a.x * b.y - a.y * b.x + a.z + b.w
            };*/
            /*
             (Q1 * Q2).w = (w1w2 - x1x2 - y1y2 - z1z2)
(Q1 * Q2).x = (w1x2 + x1w2 + y1z2 - z1y2)
(Q1 * Q2).y = (w1y2 - x1z2 + y1w2 + z1x2)
(Q1 * Q2).z = (w1z2 + x1y2 - y1x2 + z1w2 
             */
        }
        public static Quaternion FromEulerAngle(float x, float y, float z)
        {
            float cx = (float)Math.Cos(.5f * x);
            float cy = (float)Math.Cos(.5f * y);
            float cz = (float)Math.Cos(.5f * z);

            float sx = (float)Math.Sin(0.5 * x);
            float sy = (float)Math.Sin(0.5 * y);
            float sz = (float)Math.Sin(0.5 * z);
            return new Quaternion()
            {
                x = cz * cy * sx - sz * sy * cx,
                y = cz * sy * cx + sz * cy * sx,
                z = sz * cy * cx - cz * sy * sx,
                w = cz * cy * cx + sz * sy * sx
            };
        }
        public static Quaternion FromAxisAngle(float angle, Vector3 axis)
        {
			float s = (float)Math.Sin(angle / 2);
            return new Quaternion()
            {
                w = (float)Math.Cos(angle / 2),
                x = axis.x * s,
                y = axis.y * s,
                z = axis.z * s
            };

        }

		/// <summary>
		/// Creates a new matrix representing the quaternion
		/// </summary>
		/// <returns></returns>
		public Matrix ToMatrix()
		{
			return new Matrix()
			{
				m11 = 1f - 2 * (y * y + z * z),
				m21 = 2 * (x * y + w * z),
				m31 = 2 * (x * z - w * y),
				m41 = 0,
				m12 = 2 * (x * y - w * z),
				m22 = 1f - 2 * (x * x + z * z),
				m32 = 2 * (y * z + w * x),
				m42 = 0,
				m13 = 2 * (x * z + w * y),
				m23 = 2 * (y * z - w * x),
				m33 = 1f - 2 * (x * x + y * y),
				m43 = 0,
				m14 = 0,
				m24 = 0,
				m34 = 0,
				m44 = 1
			};
		}
        public static explicit operator Vector3(Quaternion quat)
        {
            return new Vector3(quat.x, quat.y, quat.z);
        }
		public static explicit operator Vector4(Quaternion quat)
		{
			return new Vector4(quat.x, quat.y, quat.z, quat.w);
		}
	}
}
