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
			  m11, m12, m13, m14,
			  m21, m22, m23, m24,
			  m31, m32, m33, m34,
			  m41, m42, m43, m44;

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

		public static Matrix Scale(float x, float y, float z)
		{
			return new Matrix()
			{
				m11 = x, m22 = y, m33 = z, m44 = 1
			};
		}
		public static Matrix Scale(Vector4 vec)
		{
			return new Matrix()
			{
				m11 = vec.x,
				m22 = vec.y,
				m33 = vec.z
			};
		}
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
				m33 = 1, m34 = z
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
	}
}
