using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	/// <summary>
	/// Represents a vector in 2D space
	/// </summary>
	public struct Vector2
	{
		public float x, y;
		public Vector2(float x, float y)
		{
			this.x = x; this.y = y;
		}
		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}
		public float Length
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y);
			}
		}
		public Vector2 Normalize()
		{
			return this / Length;
		}
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}
		public static Vector2 operator *(Vector2 a, float b)
		{
			return new Vector2(a.x * b, a.y * b);
		}
		public static Vector2 operator /(Vector2 a, float b)
		{
			return new Vector2(a.x / b, a.y / b);
		}
		public static Vector2 operator *(float a, Vector2 b)
		{
			return new Vector2(a * b.x, a * b.y);
		}
		public static Vector2 operator /(float a, Vector2 b)
		{
			return new Vector2(a / b.x, a / b.y);
		}

	}
	/// <summary>
	/// Represents a vector in 3D space
	/// </summary>
	public struct Vector3
	{
		public float x, y, z;
		public Vector3(float x, float y, float z)
		{
			this.x = x; this.y = y; this.z = z;
		}
		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}
		public float Length
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y + z * z);
			}
		}
		public Vector3 Normalize()
		{
			return this / Length;
		}

		public static Vector3 operator *(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}
		public static Vector3 operator /(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}
		public static Vector3 operator *(Vector3 a, float b)
		{
			return new Vector3(a.x * b, a.y * b, a.z * b);
		}
		public static Vector3 operator /(Vector3 a, float b)
		{
			return new Vector3(a.x / b, a.y / b, a.z / b);
		}
		public static Vector3 operator *(float a, Vector3 b)
		{
			return new Vector3(a * b.x, a * b.y, a * b.z);
		}
		public static Vector3 operator /(float a, Vector3 b)
		{
			return new Vector3(a / b.x, a / b.y, a / b.z);
		}
		public Vector4 ToVector4()
		{
			return new Vector4(x, y, z);
		}
	}
	/// <summary>
	/// Represents a vector in 4D space
	/// </summary>
	public struct Vector4
	{
		public float x, y, z, w;
		public Vector4(float x, float y, float z)
		{
			this.x = x; this.y = y; this.z = z; this.w = 1;
		}
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x; this.y = y; this.z = z; this.w = w;
		}
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y + b.y + a.z * b.z;
		}
		public float Length
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y + z * z);
			}
		}
		public Vector4 Normalize()
		{
			return this / Length;
		}

		public static Vector4 operator *(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z);
		}
		public static Vector4 operator /(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z);
		}
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, a.w);
		}
		public static Vector4 operator *(Vector4 a, float b)
		{
			return new Vector4(a.x * b, a.y * b, a.z * b, a.w);
		}
		public static Vector4 operator /(Vector4 a, float b)
		{
			return new Vector4(a.x / b, a.y / b, a.z / b, a.w);
		}
		public static Vector4 operator *(float a, Vector4 b)
		{
			return new Vector4(a * b.x, a * b.y, a * b.z, b.w);
		}
		public static Vector4 operator /(float a, Vector4 b)
		{
			return new Vector4(a / b.x, a / b.y, a / b.z, b.w);
		}
		public Vector3 ToVector3()
		{
			return new Vector3(x, y, z);
		}

	}
}
