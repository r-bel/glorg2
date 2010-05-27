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
using System.Text.RegularExpressions;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;


namespace Glorg2
{
	/// <summary>
	/// Represents a vector in 2D space
	/// </summary>
	[Serializable()]
	public struct Vector2
	{
		public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(new ElementType[]
		{
		  ElementType.Position | ElementType.TwoDimension | ElementType.Float | ElementType.Bits32
		}, typeof(Vector2));
		public float x, y;
		public Vector2(float x, float y)
		{
			this.x = x; this.y = y;
		}
		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

        public Vector2 Abs()
        {
            return new Vector2(Math.Abs(x), Math.Abs(y));
        }
		internal static readonly string float_reg = @"-?([0-9]*\.[0-9]+)|([0-9]+(\.[0-9]+))";
		private static readonly Regex vec_reg = new Regex("{" + string.Format(@"\s*(?<X>{0})\s*,\s*(?<Y>{0})\s*", float_reg) + "}");

		public static Vector2 Parse(string input, System.IFormatProvider prov)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			Vector2 ret = new Vector2();
			ret.x = float.Parse(x, prov);
			ret.y = float.Parse(y, prov);
			return ret;
		}
		public static Vector2 Parse(string input)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			Vector2 ret = new Vector2();
			ret.x = float.Parse(x);
			ret.y = float.Parse(y);
			return ret;

		}
		public static bool TryParse(string input, System.Globalization.NumberStyles styles, IFormatProvider prov, out Vector2 ret)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			ret = new Vector2();
			if (float.TryParse(x, styles, prov, out ret.x))
				return float.TryParse(y, styles, prov, out ret.y);
			else return false;
		}
		public static bool TryParse(string input, out Vector2 ret)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			ret = new Vector2();
			if (float.TryParse(x, out ret.x))
				return float.TryParse(y, out ret.y);
			else return false;
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
		public override string ToString()
		{
			return "[" + x.ToString() + ", " + y.ToString() + "]";
		}
		public float this[int index]
		{
			get
			{
				if (index == 0)
					return x;
				else if (index == 1)
					return y;
				else
					throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0)
					x = value;
				else if (index == 1)
					y = value;
				else
					throw new IndexOutOfRangeException();
			}
		}

	}
	/// <summary>
	/// Represents a vector in 3D space
	/// </summary>
	[Serializable()]
	public struct Vector3
	{
		public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(new ElementType[]
		{
		  ElementType.Position | ElementType.ThreeDimension | ElementType.Float | ElementType.Bits32
		}, typeof(Vector3));

		public float x, y, z;



		public static readonly Vector3 Up = new Vector3(0, 1, 0);
		public static readonly Vector3 Down = new Vector3(0, -1, 0);
		public static readonly Vector3 West = new Vector3(-1, 0, 0);
		public static readonly Vector3 East = new Vector3(1, 0, 0);
		public static readonly Vector3 North = new Vector3(0, 0, 1);
		public static readonly Vector3 South = new Vector3(0, 0, -1);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public Vector3 Abs()
        {
            return new Vector3(Math.Abs(x), Math.Abs(y), Math.Abs(z));
        }
		private static readonly Regex vec_reg = new Regex("{" + string.Format(@"\s*(?<X>{0})\s*,\s*(?<Y>{0})\s*,\s*(?<Z>{0})\s*", Vector2.float_reg) + "}");

		public static Vector3 Parse(string input, System.IFormatProvider prov)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			Vector3 ret = new Vector3();
			ret.x = float.Parse(x, prov);
			ret.y = float.Parse(y, prov);
			ret.z = float.Parse(z, prov);
			return ret;
		}
		public static Vector3 Parse(string input)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			Vector3 ret = new Vector3();
			ret.x = float.Parse(x);
			ret.y = float.Parse(y);
			ret.z = float.Parse(z);
			return ret;

		}
		public static bool TryParse(string input, System.Globalization.NumberStyles styles, IFormatProvider prov, out Vector3 ret)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			ret = new Vector3();
			if (float.TryParse(x, styles, prov, out ret.x))
				if (float.TryParse(y, styles, prov, out ret.y))
					return float.TryParse(z, styles, prov, out ret.z);
				else return false;
			else return false;
		}
		public static bool TryParse(string input, out Vector3 ret)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			ret = new Vector3();
			if (float.TryParse(x, out ret.x))
				if (float.TryParse(y, out ret.y))
					return float.TryParse(z, out ret.z);
				else return false;
			else return false;
		}


		public override int GetHashCode()
		{
			return ((int)x) ^ ((int)y) ^ ((int)z) ;
		}
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

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(a.y * b.z - a.z * b.y,
                            a.z * b.x - a.x * b.z,
                            a.x * b.y - a.y * b.x);

        }

		public static bool operator == (Vector3 a, Vector3 b)
		{
			float f = (b - a).Length;
			return f >= -float.Epsilon || f <= float.Epsilon;
		}
		public static bool operator !=(Vector3 a, Vector3 b)
		{
			float f = (b - a).Length;
			return !(f >= -float.Epsilon || f <= float.Epsilon);
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
		public override string ToString()
		{
			return "[" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + "]";
		}
		public float this[int index]
		{
			get
			{
				if (index == 0)
					return x;
				else if (index == 1)
					return y;
				else if (index == 2)
					return z;
				else
					throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0)
					x = value;
				else if (index == 1)
					y = value;
				else if(index == 2)
					z = value;
				else
					throw new IndexOutOfRangeException();
			}
		}
	}
	/// <summary>
	/// Represents a vector in 4D space
	/// </summary>
	[Serializable()]
	public struct Vector4
	{
		public static readonly VertexBufferDescriptor Descriptor = new VertexBufferDescriptor(new ElementType[]
		{
		  ElementType.Position | ElementType.FourDimension | ElementType.Float | ElementType.Bits32
		}, typeof(Vector4));

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
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

        public Vector4 Abs()
        {
            return new Vector4(Math.Abs(x), Math.Abs(y), Math.Abs(z), Math.Abs(w));
        }
		private static readonly Regex vec_reg = new Regex("{" + string.Format(@"\s*(?<X>{0})\s*,\s*(?<Y>{0})\s*,\s*(?<Z>{0})\s*,\s*(?<W>{0})\s*", Vector2.float_reg) + "}");
		public static Vector4 Parse(string input, System.IFormatProvider prov)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			var w = m.Groups["W"].Value;
			Vector4 ret = new Vector4();
			ret.x = float.Parse(x, prov);
			ret.y = float.Parse(y, prov);
			ret.z = float.Parse(z, prov);
			ret.w = float.Parse(w, prov);
			
			return ret;
		}
		public static Vector4 Parse(string input)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			var w = m.Groups["W"].Value;
			Vector4 ret = new Vector4();
			ret.x = float.Parse(x);
			ret.y = float.Parse(y);
			ret.z = float.Parse(z);
			ret.w = float.Parse(w);
			return ret;
		}
		public static bool TryParse(string input, System.Globalization.NumberStyles styles, IFormatProvider prov, out Vector4 ret)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			var w = m.Groups["W"].Value;
			ret = new Vector4();
			if (float.TryParse(x, styles, prov, out ret.x))
				if (float.TryParse(y, styles, prov, out ret.y))
					if (float.TryParse(z, styles, prov, out ret.z))
						return float.TryParse(w, styles, prov, out ret.w);
					else return false;
				else return false;
			else return false;
		}
		public static bool TryParse(string input, out Vector4 ret)
		{
			var m = vec_reg.Match(input);
			var x = m.Groups["X"].Value;
			var y = m.Groups["Y"].Value;
			var z = m.Groups["Z"].Value;
			var w = m.Groups["W"].Value;
			ret = new Vector4();
			if (float.TryParse(x, out ret.x))
				if (float.TryParse(y, out ret.y))
					if (float.TryParse(z, out ret.z))
						return float.TryParse(w, out ret.w);
					else return false;
				else return false;
			else return false;
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
		public override string ToString()
		{
			return "[" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + w.ToString() + "]";
		}
		public float this[int index]
		{
			get
			{
				if (index == 0)
					return x;
				else if (index == 1)
					return y;
				else if (index == 2)
					return z;
				else if (index == 3)
					return w;
				else
					throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0)
					x = value;
				else if (index == 1)
					y = value;
				else if (index == 2)
					z = value;
				else if (index == 3)
					w = value;
				else
					throw new IndexOutOfRangeException();
			}
		}
		public static explicit operator Quaternion(Vector4 vec)
		{
			return new Quaternion(vec.x, vec.y, vec.z, vec.w);
		}

	}
	/// <summary>
	/// Represents a vector of 4 16-bit floating points
	/// </summary>
	/// <remarks>This datatype does not support any type of calculations, and is to be considered a strict storage type.</remarks>
	[Serializable()]
	public struct Vector4Half
	{
		public Half x, y, z, w;
		public Vector4Half(float x, float y, float z, float w)
		{
			this.x = (Half)x;
			this.y = (Half)y;
			this.z = (Half)z;
			this.w = (Half)w;
		}
		public static implicit operator Vector4(Vector4Half val)
		{
			return new Vector4((float)val.x, (float)val.y, (float)val.z, (float)val.w);
		}
		public static explicit operator Vector4Half(Vector4 val)
		{
			return new Vector4Half(val.x, val.y, val.z, val.w);
		}
		public override string ToString()
		{
			return "[" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ", " + w.ToString() + "]";
		}

	}
	/// <summary>
	/// Represents a vector of 3 16-bit floating points
	/// </summary>
	/// <remarks>This datatype does not support any type of calculations, and is to be considered a strict storage type.</remarks>
	[Serializable()]
	public struct Vector3Half
	{
		public Half x, y, z;
		public Vector3Half(float x, float y, float z)
		{
			this.x = (Half)x;
			this.y = (Half)y;
			this.z = (Half)z;
		}
		public static implicit operator Vector3(Vector3Half val)
		{
			return new Vector3((float)val.x, (float)val.y, (float)val.z);
		}
		public static explicit operator Vector3Half(Vector3 val)
		{
			return new Vector3Half(val.x, val.y, val.z);
		}
		public override string ToString()
		{
			return "[" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + "]";
		}


	}
	/// <summary>
	/// Represents a vector of 2 16-bit floating points
	/// </summary>
	/// <remarks>This datatype does not support any type of calculations, and is to be considered a strict storage type.</remarks>
	[Serializable()]
	public struct Vector2Half
	{
		public Half x, y;
		public Vector2Half(float x, float y)
		{
			this.x = (Half)x;
			this.y = (Half)y;
		}
		public static implicit operator Vector2(Vector2Half val)
		{
			return new Vector2((float)val.x, (float)val.y);
		}
		public static explicit operator Vector2Half(Vector2 val)
		{
			return new Vector2Half(val.x, val.y);
		}
		public override string ToString()
		{
			return "[" + x.ToString() + ", " + y.ToString() + "]";
		}
	}
}
