using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	/// <summary>
	/// Matrices are structures used for manipulating or representing data. 
	/// This matrix is a 4x4 matrix used for transforming 3D or 4D vectors.
	/// </summary>
	public struct Matrix
	{
		public float
			  m11, m21, m31, m41,
			  m12, m22, m32, m42,
			  m13, m23, m33, m43,
			  m14, m24, m34, m44;

		/// <summary>
		/// This matrix represents an identity matrix, with right to left diagonal set to 1.
		/// </summary>
		public static readonly Matrix Identity = new Matrix()
		{
			m11 = 1, m12 = 0, m13 = 0, m14 = 0,
			m21 = 0, m22 = 1, m23 = 0, m24 = 0,
			m31 = 0, m32 = 0, m33 = 1, m34 = 0,
			m41 = 0, m42 = 0, m43 = 0, m44 = 1
		};

		/// <summary>
		/// Creates a scale matrix.
		/// </summary>
		/// <param name="x">X dimension scale</param>
		/// <param name="y">Y dimension scale</param>
		/// <param name="z">Z dimension scale</param>
		/// <returns></returns>
		public static Matrix Scale(float x, float y, float z)
		{
			return new Matrix()
			{
				m11 = x, m22 = y, m33 = z, m44 = 1
			};
		}
		/// <summary>
		/// Creates a scale matrix. Note that the w component is not ignored.
		/// </summary>
		/// <param name="vec">Scale magnitude for each dimension</param>
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
		/// <summary>
		/// Creates a scale matrix
		/// </summary>
		/// <param name="vec">Scale magnitude for each dimension</param>
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
		/// <summary>
		/// Creates a translation matrix
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="z">Z position</param>
		/// <returns>Translation matrix</returns>
		public static Matrix Translate(float x, float y, float z)
		{
			return new Matrix()
			{
				m11 = 1, m14 = x,
				m22 = 1, m24 = y,
				m33 = 1, m34 = z,
				m44 = 1
			};
		}
		/// <summary>
		/// Creates a new translation matrix. Note that the w dimension is not ignored.
		/// </summary>
		/// <param name="vec">Vector representing the translation value</param>
		/// <returns>Translation matrix</returns>
		public static Matrix Translate(Vector4 vec)
		{
			return new Matrix()
			{
				m11 = 1, m14 = vec.x,
				m22 = 1, m24 = vec.y,
				m33 = 1, m34 = vec.z,
				m44 = vec.w			
			};
		}
		/// <summary>
		/// Creates a new translation matrix
		/// </summary>
		/// <param name="vec">Vector representing the translation value</param>
		/// <returns>Translation matrix</returns>
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
		/// <summary>
		/// Retrieves the matris as a series of vectors representing each row
		/// </summary>
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
		/// <summary>
		/// Retrieves the matrix as a series of vectors representing each column
		/// </summary>
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
		/// <summary>
		/// Creates a rotation matrix which rotates around the X axis
		/// </summary>
		/// <param name="angle">Rotation angle in radians</param>
		/// <returns>Rotation matrix</returns>
		public static Matrix RotateX(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			return new Matrix()
			{
				m11 = 1, m21 = 0, m31 = 0, m41 = 0,
				m12 = 0, m22 = c, m32 =-s, m42 = 0,
				m13 = 0, m23 = s, m33 = c, m43 = 0,
				m14 = 0, m24 = 0, m34 = 0, m44 = 1
			};
		}
		/// <summary>
		/// Creates a rotation matrix which rotates around the Y axis
		/// </summary>
		/// <param name="angle">Rotation angle in radians</param>
		/// <returns>Rotation matrix</returns>
		public static Matrix RotateY(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			return new Matrix()
			{
				m11 = c, m21 = 0, m31 = s, m41 = 0,
				m12 = 0, m22 = 1, m32 = 0, m42 = 0,
				m13 =-s, m23 = 0, m33 = c, m43 = 0,
				m14 = 0, m24 = 0, m34 = 0, m44 = 1
			};
		}
		/// <summary>
		/// Creates a rotation matrix which rotates around the Z axis
		/// </summary>
		/// <param name="angle">Rotation angle in radians</param>
		/// <returns>Rotation matrix</returns>
		public static Matrix RotateZ(float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			return new Matrix()
			{
				m11 = c, m21 =-s, m31 = 0, m41 = 0,
				m12 = s, m22 = c, m32 = 0, m42 = 0,
				m13 = 0, m23 = 0, m33 = 1, m43 = 0,
				m14 = 0, m24 = 0, m34 = 0, m44 = 1
			};
		}
		/// <summary>
		/// Creates a perspective matrix
		/// </summary>
		/// <param name="fovy">Field of view measured by angle in radians from top to bottom</param>
		/// <param name="aspect">Aspect ratio ofr width/height of viewport. Cannot be zero</param>
		/// <param name="near">Near Z clipping plane. Cannot be zero or equal to far</param>
		/// <param name="far">Far Z clipping plane. Cannot be zero or equal to near</param>
		/// <returns>Perspective matrix</returns>
		/// <remarks>This matrix setup is attained from gluPerspective function and wil work in a similar fashion</remarks>
		public static Matrix Perspective(float fovy, float aspect, float near, float far)
		{
			float f = 1.0f / (float)Math.Tan(fovy / 2);
			return new Matrix()
			{
				m11 = f / aspect, 
				m22 = f,
				m33 = (far+ near) / (near - far), m34 = (2 * far * near) / (near - far),
				m43 = -1, m44 = 0
			};
		}

		public static Matrix Orthographic(float left, float right, float top, float bottom, float near, float far)
		{
			return new Matrix()
			{
				m11 = 2 / (right - left),
				m22 = 2 / (top - bottom),
				m33 = -2 / (far - near),
				m44 = 1,
				m14 = (right + left) / (right - left),
				m24 = (top + bottom) / (top - bottom),
				m34 = (far + near) / (far  - near)
			};
		}

		/// <summary>
		/// Reverses the order of columns/rows
		/// </summary>
		/// <returns>Tranposed matrix</returns>
		public Matrix Transpose()
		{
			return new Matrix()
			{
				m11 = this.m11, m12 = this.m21, m13 = this.m31, m14 = this.m41,
				m21 = this.m12, m22 = this.m22, m23 = this.m32, m24 = this.m42,
				m31 = this.m13, m32 = this.m23, m33 = this.m33, m34 = this.m43,
				m41 = this.m14, m42 = this.m24, m43 = this.m34, m44 = this.m44
			};
		}

		public static Matrix operator *(Matrix a, Matrix b)
		{
			// TODO: Increase performance
			// I have written it in a simple manner as manual matrix multiplication is very error prone.
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
		public static Vector4 operator * (Matrix a, Vector4 b)
		{
			// This is based on linear algebra (duh) idea that any matrix 
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
	}
}
