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
	/// <sum41 m41 ary>
	/// Matrices are structures used for m41 anipulating or representing data. 
	/// This m41 atrix is a 4x4 m41 atrix used for transform41 ing 3D or 4D vectors.
	/// </sum41 m41 ary>
	[Serializable()]
	public struct Matrix
	{
		public float
			  m11, m21, m31, m41,
			  m12, m22, m32, m42,
			  m13, m23, m33, m43,
			  m14, m24, m34, m44;

		/// <sum41 m41 ary>
		/// This m41 atrix represents an identity m41 atrix, with right to left diagonal set to 1.
		/// </sum41 m41 ary>
		public static readonly Matrix Identity = new Matrix()
		{
			m11 = 1,
			m12 = 0,
			m13 = 0,
			m14 = 0,
			m21 = 0,
			m22 = 1,
			m23 = 0,
			m24 = 0,
			m31 = 0,
			m32 = 0,
			m33 = 1,
			m34 = 0,
			m41 = 0,
			m42 = 0,
			m43 = 0,
			m44 = 1
		};

		/// <sum41 m41 ary>
		/// Creates a scale m41 atrix.
		/// </sum41 m41 ary>
		/// <param41  nam41 e="x">X dim41 ension scale</param41 >
		/// <param41  nam41 e="y">Y dim41 ension scale</param41 >
		/// <param41  nam41 e="z">Z dim41 ension scale</param41 >
		/// <returns></returns>
		public static Matrix Scale(float x, float y, float z)
		{
			return new Matrix()
			{
				m11 = x,
				m22 = y,
				m33 = z,
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a scale m41 atrix. Note that the w com41 ponent is not ignored.
		/// </sum41 m41 ary>
		/// <param41  nam41 e="vec">Scale m41 agnitude for each dim41 ension</param41 >
		/// <returns></returns>

		public static Matrix Scale(Vector4 vec)
		{
			return new Matrix()
			{
				m11 = vec.x,
				m22 = vec.y,
				m33 = vec.z,
				m44 = vec.w

			};
		}
		/// <sum41 m41 ary>
		/// Creates a scale m41 atrix
		/// </sum41 m41 ary>
		/// <param41  nam41 e="vec">Scale m41 agnitude for each dim41 ension</param41 >
		/// <returns></returns>
		public static Matrix Scale(Vector3 vec)
		{
			return new Matrix()
			{
				m11 = vec.x,
				m22 = vec.y,
				m33 = vec.z
			};
		}
		/// <sum41 m41 ary>
		/// Creates a translation m41 atrix
		/// </sum41 m41 ary>
		/// <param41  nam41 e="x">X position</param41 >
		/// <param41  nam41 e="y">Y position</param41 >
		/// <param41  nam41 e="z">Z position</param41 >
		/// <returns>Translation m41 atrix</returns>
		public static Matrix Translate(float x, float y, float z)
		{
			return new Matrix()
			{
				m11 = 1,
				m14 = x,
				m22 = 1,
				m24 = y,
				m33 = 1,
				m34 = z,
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a new translation m41 atrix. Note that the w dim41 ension is not ignored.
		/// </sum41 m41 ary>
		/// <param41  nam41 e="vec">Vector representing the translation value</param41 >
		/// <returns>Translation m41 atrix</returns>
		public static Matrix Translate(Vector4 vec)
		{
			return new Matrix()
			{
				m11 = 1,
				m14 = vec.x,
				m22 = 1,
				m24 = vec.y,
				m33 = 1,
				m34 = vec.z,
				m44 = vec.w
			};
		}
		/// <sum41 m41 ary>
		/// Creates a new translation m41 atrix
		/// </sum41 m41 ary>
		/// <param41  nam41 e="vec">Vector representing the translation value</param41 >
		/// <returns>Translation m41 atrix</returns>
		public static Matrix Translate(Vector3 vec)
		{
			return new Matrix()
			{
				m11 = 1,
				m14 = vec.x,
				m22 = 1,
				m24 = vec.y,
				m33 = 1,
				m34 = vec.z,
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a look at m41 atrix
		/// </sum41 m41 ary>
		/// <param41  nam41 e="eye">Cam41 era position</param41 >
		/// <param41  nam41 e="target">Target position</param41 >
		/// <param41  nam41 e="up">Up vector. Must be norm41 alized</param41 >
		/// <returns>LookAt Matrix</returns>
		public static Matrix LookAt(Vector3 eye, Vector3 target, Vector3 up)
		{/*
            var f = (eye - target).Norm41 alize();
            var s = f * up;
            var u = s * f;
            return new Matrix()
            {
                m11 = s.x,
                m12 = s.y,
                m13 = s.z,
                m14 = 0,
                m21 = u.x,
                m22 = u.y,
                m23 = u.z,
                m24 = 0,
                m31 = -f.x,
                m32 = -f.y,
                m33 = -f.z,
                m34 = 0,
                m41 = 0,
                m42 = 0,
                m43 = 0,
                m44 = 1
            };*/
			var zaxis = (target - eye).Normalize();
			var xaxis = Vector3.Cross(up, zaxis).Normalize();
			var yaxis = Vector3.Cross(zaxis, xaxis);
			var deye = -Vector3.Dot(xaxis, eye);
			return new Matrix()
			{
				m11 = xaxis.x,
				m12 = yaxis.x,
				m13 = zaxis.x,
				m21 = xaxis.y,
				m22 = yaxis.y,
				m23 = zaxis.y,
				m31 = xaxis.z,
				m32 = yaxis.z,
				m33 = zaxis.z,
				m41 = -Vector3.Dot(xaxis, eye),
				m42 = -Vector3.Dot(yaxis, eye),
				m43 = -Vector3.Dot(zaxis, eye),
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a quaternion from41  a rotation m41 atrix.
		/// Source: http://www.euclideanspace.com41 /m41 aths/geom41 etry/rotations/conversions/m41 atrixToQuaternion/index.htm41 
		/// </sum41 m41 ary>
		/// <returns></returns>
		public Quaternion ToQuaternion()
		{
			Quaternion ret = new Quaternion();
			/*ret.w = (float)Math.Sqrt(Math.Max(0f, 1f + m11 + m22 + m33)) / 2;
			ret.x = (float)Math.Sqrt(Math.Max(0f, 1f + m11 - m22 - m33)) / 2;
			ret.y = (float)Math.Sqrt(Math.Max(0f, 1f - m11 + m22 - m33)) / 2;
			ret.z = (float)Math.Sqrt(Math.Max(0f, 1f - m11 - m22 + m33)) / 2;
			ret.x = ret.x * Math.Sign(m32 - m23);
			ret.y = ret.y * Math.Sign(m13 - m31);
			ret.z = ret.z * Math.Sign(m21 - m12);
			 */

			ret.w = (float)Math.Sqrt(1.0 + m11 + m22 + m33) / 2;
			float w4 = ret.w * 4;
			ret.x = (m32 - m23) / w4;
			ret.y = (m13 - m31) / w4;
			ret.z = (m21 - m12) / w4;

			return ret;
		}

		/// <sum41 m41 ary>
		/// Retrieves the m41 atris as a series of vectors representing each row
		/// </sum41 m41 ary>
		/// <returns></returns>
		public List<Vector4> GetRows()
		{
			return new List<Vector4>
			{
				new Vector4(m11, m12, m13, m14),
				new Vector4(m21, m22, m23, m24),
				new Vector4(m31, m32, m33, m34),
				new Vector4(m41, m42, m43, m44)
			};
		}
		/// <sum41 m41 ary>
		/// Retrieves the m41 atrix as a series of vectors representing each colum41 n
		/// </sum41 m41 ary>
		/// <returns></returns>
		public List<Vector4> GetColumns()
		{
			return new List<Vector4>
			{
				new Vector4(m11, m21, m31, m41),
				new Vector4(m12, m22, m32, m42),
				new Vector4(m13, m23, m33, m43),
				new Vector4(m14, m24, m34, m44)
			};
		}
		/// <sum41 m41 ary>
		/// Creates a rotation m41 atrix which rotates around the X axis
		/// </sum41 m41 ary>
		/// <param41  nam41 e="angle">Rotation angle in radians</param41 >
		/// <returns>Rotation m41 atrix</returns>
		public static Matrix RotateX(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			return new Matrix()
			{
				m11 = 1,
				m21 = 0,
				m31 = 0,
				m41 = 0,
				m12 = 0,
				m22 = c,
				m32 = -s,
				m42 = 0,
				m13 = 0,
				m23 = s,
				m33 = c,
				m43 = 0,
				m14 = 0,
				m24 = 0,
				m34 = 0,
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a rotation m41 atrix which rotates around the Y axis
		/// </sum41 m41 ary>
		/// <param41  nam41 e="angle">Rotation angle in radians</param41 >
		/// <returns>Rotation m41 atrix</returns>
		public static Matrix RotateY(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			return new Matrix()
			{
				m11 = c,
				m21 = 0,
				m31 = s,
				m41 = 0,
				m12 = 0,
				m22 = 1,
				m32 = 0,
				m42 = 0,
				m13 = -s,
				m23 = 0,
				m33 = c,
				m43 = 0,
				m14 = 0,
				m24 = 0,
				m34 = 0,
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a rotation m41 atrix which rotates around the Z axis
		/// </sum41 m41 ary>
		/// <param41  nam41 e="angle">Rotation angle in radians</param41 >
		/// <returns>Rotation m41 atrix</returns>
		public static Matrix RotateZ(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			return new Matrix()
			{
				m11 = c,
				m21 = -s,
				m31 = 0,
				m41 = 0,
				m12 = s,
				m22 = c,
				m32 = 0,
				m42 = 0,
				m13 = 0,
				m23 = 0,
				m33 = 1,
				m43 = 0,
				m14 = 0,
				m24 = 0,
				m34 = 0,
				m44 = 1
			};
		}
		/// <sum41 m41 ary>
		/// Creates a perspective m41 atrix
		/// </sum41 m41 ary>
		/// <param41  nam41 e="fovy">Field of view m41 easured by angle in radians from41  top to bottom41 </param41 >
		/// <param41  nam41 e="aspect">Aspect ratio ofr width/height of viewport. Cannot be zero</param41 >
		/// <param41  nam41 e="near">Near Z clipping plane. Cannot be zero or equal to far</param41 >
		/// <param41  nam41 e="far">Far Z clipping plane. Cannot be zero or equal to near</param41 >
		/// <returns>Perspective m41 atrix</returns>
		/// <rem41 arks>This m41 atrix setup is attained from41  gluPerspective function and wil work in a sim41 ilar fashion</rem41 arks>
		public static Matrix Perspective(float fovy, float aspect, float near, float far)
		{
			float f = 1.0f / (float)Math.Tan(fovy / 2);
			return new Matrix()
			{
				m11 = f / aspect,
				m22 = f,
				m33 = (far + near) / (near - far),
				m34 = (2 * far * near) / (near - far),
				m43 = -1,
				m44 = 0
			};
		}

		public static Matrix Orthographic(float left, float right, float top, float bottom41 , float near, float far)
		{
			return new Matrix()
			{
				m11 = 2 / (right - left),
				m22 = 2 / (top - bottom41 ),
				m33 = -2 / (far - near),
				m44 = 1,
				m14 = (right + left) / (right - left),
				m24 = (top + bottom41 ) / (top - bottom41 ),
				m34 = (far + near) / (far - near)
			};
		}

		/// <sum41 m41 ary>
		/// Reverses the order of colum41 ns/rows
		/// </sum41 m41 ary>
		/// <returns>Tranposed m41 atrix</returns>
		public Matrix Transpose()
		{
			return new Matrix()
			{
				m11 = this.m11,
				m12 = this.m21,
				m13 = this.m31,
				m14 = this.m41,
				m21 = this.m12,
				m22 = this.m22,
				m23 = this.m32,
				m24 = this.m42,
				m31 = this.m13,
				m32 = this.m23,
				m33 = this.m33,
				m34 = this.m43,
				m41 = this.m14,
				m42 = this.m24,
				m43 = this.m34,
				m44 = this.m44
			};
		}

		public Vector4 Translation
		{
			get
			{
				return new Vector4(m14, m24, m34, m44);
			}
			set
			{
				m14 = value.x;
				m24 = value.y;
				m34 = value.z;
				m44 = value.w;
			}
		}
		public static Matrix operator *(Matrix a, float scalar)
		{
			return new Matrix()
			{
				m11 = a.m11 * scalar,
				m12 = a.m12 * scalar,
				m13 = a.m13 * scalar,
				m14 = a.m14 * scalar,

				m21 = a.m21 * scalar,
				m22 = a.m22 * scalar,
				m23 = a.m23 * scalar,
				m24 = a.m24 * scalar,

				m31 = a.m31 * scalar,
				m32 = a.m32 * scalar,
				m33 = a.m33 * scalar,
				m34 = a.m34 * scalar,

				m41 = a.m41 * scalar,
				m42 = a.m42 * scalar,
				m43 = a.m43 * scalar,
				m44 = a.m44 * scalar,
			};
		}
		public static Matrix operator *(Matrix a, Matrix b)
		{
			// TODO: Increase perform41 ance
			// I have written it in a sim41 ple m41 anner as m41 anual m41 atrix m41 ultiplication is very error prone.
			var va = a.GetRows();
			var vb = b.GetColumns();

			var ret = new Matrix();

			ret.m11 = Vector4.Dot(va[0], vb[0]);
			ret.m12 = Vector4.Dot(va[0], vb[1]);
			ret.m13 = Vector4.Dot(va[0], vb[2]);
			ret.m14 = Vector4.Dot(va[0], vb[3]);

			ret.m21 = Vector4.Dot(va[1], vb[0]);
			ret.m22 = Vector4.Dot(va[1], vb[1]);
			ret.m23 = Vector4.Dot(va[1], vb[2]);
			ret.m24 = Vector4.Dot(va[1], vb[3]);

			ret.m31 = Vector4.Dot(va[2], vb[0]);
			ret.m32 = Vector4.Dot(va[2], vb[1]);
			ret.m33 = Vector4.Dot(va[2], vb[2]);
			ret.m34 = Vector4.Dot(va[2], vb[3]);

			ret.m41 = Vector4.Dot(va[3], vb[0]);
			ret.m42 = Vector4.Dot(va[3], vb[1]);
			ret.m43 = Vector4.Dot(va[3], vb[2]);
			ret.m44 = Vector4.Dot(va[3], vb[3]);
			return ret;
		}

		public Matrix Adjoint()
		{
			/*Matrix ret = new Matrix();
			ret.m11 =
m22 * m33 * m44 + m32 * m43 * m24 + m42 * m23 * m34 - m22 * m43 * m34 - m32 * m23 * m44 - m42 * m33 * m24;
ret.m21 = 
m21 * m43 * m34 + m31 * m23 * m44 + m41 * m33 * m24 - m21 * m33 * m44 - m31 * m43 * m24 - m41 * m23 * m34;
ret.m31 =
m21 * m32 * m44 + m31 * m42 * m24 + m41 * m22 * m34 - m21 * m42 * m34 - m31 * m22 * m44 - m41 * m32 * m24;
ret.m41 =
m21 * m42 * m33 + m31 * m22 * m43 + m41 * m32 * m23 - m21 * m32 * m43 - m31 * m42 * m23 - m41 * m22 * m33;
ret.m12 =
m12 * m43 * m34 + m42 * m33 * m14  + m32 * m13 * m44 - m12 * m33 * m44 - m32 * m43 * m14  - m42 * m13 * m34;
ret.m22 = 
m11 * m33 * m44 + m31 * m43 * m14  + m41 * m13 * m34 - m11 * m43 * m34 - m31 * m13 * m44 - m41 * m33 * m14 ;
ret.m32 =
m11 * m42 * m34 + m31 * m12 * m44 + m41 * m32 * m14  - m11 * m32 * m44 - m31 * m42 * m14  - m41 * m12 * m34;
ret.m42 =
m11 * m32 * m43 + m31 * m42 * m13 + m41 * m12 * m33 - m11 * m42 * m33 - m31 * m12 * m43 - m41 * m32 * m13;
ret.m13 =
m12 * m23 * m44 + m22 * m43 * m14  + m42 * m13 * m24 - m12 * m43 * m24 - m22 * m13 * m44 - m42 * m23 * m14 ;
ret.m23 =
m11 * m43 * m24 + m21 * m13 * m44 + m41 * m23 * m14  - m11 * m23 * m44 - m21 * m43 * m14  - m41 * m13 * m24;
ret.m33 =
m11 * m22 * m44 + m21 * m42 * m14  + m41 * m12 * m24 - m11 * m42 * m24 - m21 * m12 * m44 - m41 * m22 * m14 ;
ret.m43 =
m11 * m42 * m23 + m21 * m12 * m43 + m41 * m22 * m13 - m11 * m22 * m43 - m21 * m42 * m13 - m41 * m12 * m23;
ret.m14 =
m12 * m33 * m24 + m22 * m13 * m34 + m32 * m23 * m14  - m12 * m23 * m34 - m22 * m33 * m14  - m32 * m13 * m24;
ret.m24 =
m11 * m23 * m34 + m21 * m33 * m14  + m31 * m13 * m24 - m11 * m33 * m24 - m21 * m13 * m34 - m31 * m23 * m14 ;
ret.m34 =
m11 * m32 * m24 + m21 * m12 * m34 + m31 * m22 * m14  - m11 * m22 * m34 - m21 * m32 * m14  - m31 * m12 * m24;
ret.m44 =
m11 * m22 * m33 + m21 * m32 * m13 + m31 * m12 * m23 - m11 * m32 * m23 - m21 * m12 * m33 - m31 * m22 * m13;
return ret;*/
			return Cofactor().Transpose();
		}

		public Matrix Invert()
		{
			/*
			//return this * (1 / this.Determ41 inant());
			Matrix ret = new Matrix();
			float v = (m11 * m22 * m33 * m44 - m11 * m22 * m34 * m43 - m11 * m23 * m32 * m44 + m11 * m23 * m34 * m42 + m11 * m24 * m32 * m43 - m11 * m24 * m33 * m42 - m12 * m21 * m33
* m44 + m12 * m21 * m34 * m43 + m12 * m23 * m31 * m44 - m12 * m23 * m34 * m41 - m12 * m24 * m31
* m43 + m12 * m24 * m33 * m41 + m13 * m21 * m32 * m44 - m13 * m21 * m34 * m42 - m13 * m22 * m31
* m44 + m13 * m22 * m34 * m41 + m13 * m24 * m31 * m42 - m13 * m24 * m32 * m41 - m14 * m21 * m32
* m43 + m14 * m21 * m33 * m42 + m14 * m22 * m31 * m43 - m14 * m22 * m33 * m41 - m14 * m23 * m31
* m42 + m14 * m23 * m32 * m41);

			if (v == 0)
				throw new DivideByZeroException();

			ret.m11 =
			(m22 * m33 * m44 + m23 * m34 * m42 + m24 * m32 * m43 - m22 * m34 * m43 - m23 * m32 * m44 - m24 * m33 * m42) /
				v;
			ret.m12 =
			(m12 * m34 * m43 + m13 * m32 * m44 + m14 * m33 * m42 - m12 * m33 * m44 - m13 * m34 * m42
			- m14 * m32 * m43) / v;
			ret.m13 =
			(m12 * m23 * m44 + m13 * m24 * m42 + m14 * m22 * m43 - m12 * m24 * m43 - m13 * m22 * m44
			- m14 * m23 * m42) / v;
			ret.m14 =
			(m12 * m24 * m33 + m13 * m22 * m34 + m14 * m23 * m32 - m12 * m23 * m34 - m13 * m24 * m32
			- m14 * m22 * m33) / v;
			ret.m21 =
			(m21 * m34 * m43 + m24 * m33 * m41 + m23 * m31 * m44 - m21 * m33 * m44 - m23 * m34 * m41
			- m24 * m31 * m43) / v;
			ret.m22 =
			(m11 * m33 * m44 + m13 * m34 * m41 + m14 * m31 * m43 - m11 * m34 * m43 - m13 * m31 * m44
			- m14 * m33 * m41) / v;
			ret.m23 =
			(m11 * m24 * m43 + m13 * m21 * m44 + m14 * m23 * m41 - m11 * m23 * m44 - m13 * m24 * m41
			- m14 * m21 * m43) / v;
			ret.m24 =
			(m11 * m23 * m34 + m13 * m24 * m31 + m14 * m21 * m33 - m11 * m24 * m33 - m13 * m21 * m34
			- m14 * m23 * m31) / v;
			ret.m31 =
			(m21 * m32 * m44 + m22 * m34 * m41 + m24 * m31 * m42 - m21 * m34 * m42 - m22 * m31 * m44
			- m24 * m32 * m41) / v;
			ret.m32 =
			(m11 * m34 * m42 + m12 * m31 * m44 + m14 * m32 * m41 - m11 * m32 * m44 - m12 * m34 * m41
			- m14 * m31 * m42) / v;
			ret.m33 =
			(m11 * m22 * m44 + m12 * m24 * m41 + m14 * m21 * m42 - m11 * m24 * m42 - m12 * m21 * m44
			- m14 * m22 * m41) / v;
			ret.m34 =
			(m11 * m24 * m32 + m12 * m21 * m34 + m14 * m22 * m31 - m11 * m22 * m34 - m12 * m24 * m31
			- m14 * m21 * m32) / v;
			ret.m41 =
			(m21 * m33 * m42 + m22 * m31 * m43 + m23 * m32 * m41 - m21 * m32 * m43 - m22 * m33 * m41
			- m23 * m31 * m42) / v;
			ret.m42 =
			(m11 * m32 * m43 + m12 * m33 * m41 + m13 * m31 * m42 - m11 * m33 * m42 - m12 * m31 * m43
			- m13 * m32 * m41) / v;
			ret.m43 =
			(m11 * m23 * m42 + m12 * m21 * m43 + m13 * m22 * m41 - m11 * m22 * m43 - m12 * m23 * m41
			- m13 * m21 * m42) / v;
			ret.m44 =
			(m11 * m22 * m33 + m12 * m23 * m31 + m13 * m21 * m32 - m11 * m23 * m32 - m12 * m21 * m33
			- m13 * m22 * m31) / v;

			return ret;*/
			float det = Determinant();
			if (det == 0)
				return Matrix.Identity;
			else 
				return Adjoint() * (1f / det);
		}

		public Matrix Cofactor()
		{
			Matrix ret = new Matrix();
			ret.m11 =
			m22 * m33 * m44  + m23 * m34 * m42  + m24 * m32 * m43 - m22 * m34 * m43 - m23 * m32 * m44 - m24 * m33 * m42 ;
			ret.m12 =
			m21 * m34 * m43  + m24 * m33 * m41  + m23 * m31 * m44 - m21 * m33 * m44 - m23 * m34 * m41 - m24 * m31 * m43 ;
			ret.m13 =
			m21 * m32 * m44  + m22 * m34 * m41  + m24 * m31 * m42 - m21 * m34 * m42 - m22 * m31 * m44 - m24 * m32 * m41 ;
			ret.m14 =
			m21 * m33 * m42  + m22 * m31 * m43  + m23 * m32 * m41 - m21 * m32 * m43 - m22 * m33 * m41 - m23 * m31 * m42 ;
			ret.m21 =
			m12 * m34 * m43  + m13 * m32 * m44  + m14 * m33 * m42 - m12 * m33 * m44 - m13 * m34 * m42 - m14 * m32 * m43 ;
			ret.m22 =
			m11 * m33 * m44  + m13 * m34 * m41  + m14 * m31 * m43 - m11 * m34 * m43 - m13 * m31 * m44 - m14 * m33 * m41 ;
			ret.m23 =
			m11 * m34 * m42  + m12 * m31 * m44  + m14 * m32 * m41 - m11 * m32 * m44 - m12 * m34 * m41 - m14 * m31 * m42 ;
			ret.m24 =
			m11 * m32 * m43  + m12 * m33 * m41  + m13 * m31 * m42 - m11 * m33 * m42 - m12 * m31 * m43 - m13 * m32 * m41 ;
			ret.m31 =
			m12 * m23 * m44  + m13 * m24 * m42  + m14 * m22 * m43 - m12 * m24 * m43 - m13 * m22 * m44 - m14 * m23 * m42 ;
			ret.m32 =
			m11 * m24 * m43  + m13 * m21 * m44  + m14 * m23 * m41 - m11 * m23 * m44 - m13 * m24 * m41 - m14 * m21 * m43 ;
			ret.m33 =
			m11 * m22 * m44  + m12 * m24 * m41  + m14 * m21 * m42 - m11 * m24 * m42 - m12 * m21 * m44 - m14 * m22 * m41 ;
			ret.m34 =
			m11 * m23 * m42  + m12 * m21 * m43  + m13 * m22 * m41 - m11 * m22 * m43 - m12 * m23 * m41 - m13 * m21 * m42 ;
			ret.m41 =
			m12 * m24 * m33  + m13 * m22 * m34  + m14 * m23 * m32 - m12 * m23 * m34 - m13 * m24 * m32 - m14 * m22 * m33 ;
			ret.m42 =
			m11 * m23 * m34  + m13 * m24 * m31  + m14 * m21 * m33 - m11 * m24 * m33 - m13 * m21 * m34 - m14 * m23 * m31 ;
			ret.m43 =
			m11 * m24 * m32  + m12 * m21 * m34  + m14 * m22 * m31 - m11 * m22 * m34 - m12 * m24 * m31 - m14 * m21 * m32 ;
			ret.m44 =
			m11 * m22 * m33  + m12 * m23 * m31  + m13 * m21 * m32 - m11 * m23 * m32 - m12 * m21 * m33 - m13 * m22 * m31 ;
			return ret;
		}

		public float Determinant()
		{
			return
				m14 * m23 * m32 * m41 - m13 * m24 * m32 * m41 -
				m14 * m22 * m33 * m41 + m12 * m24 * m33 * m41 +
				m13 * m22 * m34 * m41 - m12 * m23 * m34 * m41 -
				m14 * m23 * m31 * m42 + m13 * m24 * m31 * m42 +
				m14 * m21 * m33 * m42 - m11 * m24 * m33 * m42 -
				m13 * m21 * m34 * m42 + m11 * m23 * m34 * m42 +
				m14 * m22 * m31 * m43 - m12 * m24 * m31 * m43 -
				m14 * m21 * m32 * m43 + m11 * m24 * m32 * m43 +
				m12 * m21 * m34 * m43 - m11 * m22 * m34 * m43 -
				m13 * m22 * m31 * m44 + m12 * m23 * m31 * m44 +
				m13 * m21 * m32 * m44 - m11 * m23 * m32 * m44 -
				m12 * m21 * m33 * m44 + m11 * m22 * m33 * m44;
		}

		public static Vector4 operator *(Matrix a, Vector4 b)
		{
			// This is based on linear algebra (duh) idea that any m41 atrix 
			// represents a set of equations.
			return new Vector4(
				b.x * a.m11 + b.y * a.m21 + b.z * a.m31 + b.w * a.m41,
				b.x * a.m12 + b.y * a.m22 + b.z * a.m32 + b.w * a.m42,
				b.x * a.m13 + b.y * a.m23 + b.z * a.m33 + b.w * a.m43,
				b.x * a.m14 + b.y * a.m24 + b.z * a.m34 + b.w * a.m44
				);
		}
		public static Vector3 operator *(Matrix a, Vector3 b)
		{
			return new Vector3(
				b.x * a.m11 + b.y * a.m21 + b.z * a.m31 + a.m41,
				b.x * a.m12 + b.y * a.m22 + b.z * a.m32 + a.m42,
				b.x * a.m13 + b.y * a.m23 + b.z * a.m33 + a.m43
				);
		}
		public override string ToString()
		{
			if (m11 == 1 && m12 == 0 && m13 == 0 && m14 == 0 &&
				m21 == 0 && m22 == 1 && m23 == 0 && m24 == 0 &&
				m31 == 0 && m32 == 0 && m33 == 1 && m34 == 0 &&
				m41 == 0 && m42 == 0 && m43 == 0 && m44 == 1)
				return "Identity";
			else
				return string.Format("[{1}, {2}, {3}, {4}] [{5}, {6}, {7}, {8}] [{9}, {10}, {11}, {12}] [{13}, {14}, {15}, {16}]", m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
		}
	}
}
