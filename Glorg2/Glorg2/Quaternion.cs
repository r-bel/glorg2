﻿/*
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
	/// <summary>
	/// Quaternions are structures used for generating rotations that does not result in Gimbal locks.
	/// </summary>
	/// <remarks>Inspired by the article Rotations in Three Dimension Part Five: Quaternions by Confuted. 
	/// http://www.cprogramming.com/tutorial/3d/quaternions.html</remarks>
	[Serializable()]
	[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
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
		/// Gets or set sthe angle of the quaternion in degrees
		/// Note: This is intened to be used by component designers.
		/// </summary>
		public float Angle
		{
			get
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				return ang / (float)(Math.PI / 180);
			}
			set
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				var q = FromAxisAngle(value, axis);
				x = q.x; y = q.y; z = q.z; w = q.z;
			}
		}
		/// <summary>
		/// Gets or sets the x component of a axis angle quaternion
		/// Note: This is intened to be used by component designers.
		/// </summary>
		public float X
		{
			get
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				return axis.x;
			}
			set
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				axis.x = value;
				var q = FromAxisAngle(ang, axis);
				x = q.x; y = q.y; z = q.z; w = q.z;
			}
		}
		/// <summary>
		/// Gets or sets the y component of a axis angle quaternion
		/// Note: This is intened to be used by component designers.
		/// </summary>
		public float Y
		{
			get
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				return axis.y;
			}
			set
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				axis.y = value;
				var q = FromAxisAngle(ang, axis);
				x = q.x; y = q.y; z = q.z; w = q.z;
			}
		}
		/// <summary>
		/// Gets or sets the z component of a axis angle quaternion
		/// Note: This is intened to be used by component designers.
		/// </summary>
		public float Z
		{
			get
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				return axis.z;
			}
			set
			{
				float ang = 0;
				Vector3 axis;
				ToAxisAngle(out ang, out axis);
				axis.z = value;
				var q = FromAxisAngle(ang, axis);
				x = q.x; y = q.y; z = q.z; w = q.z;
			}
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
		public static Quaternion operator -(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w).Normalize();
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

		/// <summary>
		/// Gets a directional vector for this quaternion
		/// </summary>
		/// <returns>A rotated by the quaternion</returns>
		public Vector3 ToNormal()
		{
			Matrix mat = ToMatrix().Transpose();
			return mat * new Vector3(0, 0, -1);
		}

		public void ToAxisAngle(out float angle, out Vector3 axis)
		{
			angle = (float)(2 * Math.Acos(w));
			if (angle < float.Epsilon && angle > -float.Epsilon)
				axis = new Vector3(1, 0, 0);
			else
			{
				axis.x = (float)(x / Math.Sqrt(1 - w * w));
				axis.y = (float)(y / Math.Sqrt(1 - w * w));
				axis.z = (float)(z / Math.Sqrt(1 - w * w));
			}
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
		public static Quaternion FromGreatCircle(Vector3 a, Vector3 b)
		{
			Vector3 axis = Vector3.Cross(a, b);
			float angle = (float)Math.Acos(Vector3.Dot(a, b));

			axis = axis.Normalize();

			return FromAxisAngle(angle, axis);

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
		public override string ToString()
		{
			float angle = 0;
			Vector3 axis;
			ToAxisAngle(out angle, out axis);
			return angle.ToString() + ", {" + axis.x.ToString() + ", " + axis.y.ToString() + ", " + axis.z.ToString() + "}";
		}
	}
}
