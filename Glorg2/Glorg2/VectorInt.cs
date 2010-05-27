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
namespace Glorg2
{
	public struct Vector2Int
	{

		private static readonly Regex vec_reg = new Regex(@"\{\s*(?<X>-?[0-9]+)\s*,\s*(?<Y>-?[0-9]+)\s*\}");

		public int x, y;
		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static int Dot(Vector2Int a, Vector2Int b)
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

		public static Vector2Int Parse(string input)
		{
			var m = vec_reg.Match(input);
			if (m.Success)
			{
				Vector2Int val = new Vector2Int();
				val.x = int.Parse(m.Groups["X"].Value);
				val.x = int.Parse(m.Groups["Y"].Value);
				return val;
			}
			else
				throw new FormatException("Input string was not in correct format");
		}

		public static bool TryParse(string input, out Vector2Int ret)
		{
			var m = vec_reg.Match(input);
			ret = new Vector2Int();
			if (m.Success)
			{
				if(int.TryParse(m.Groups["X"].Value, out ret.x))
					return int.TryParse(m.Groups["Y"].Value, out ret.y);
				else
					return false;
			}
			else
				return false;
		}

		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}
		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}
		public static Vector2Int operator /(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x / b.x, a.y / b.y);
		}
		public static Vector2Int operator %(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x % b.x, a.y % b.y);
		}

		public static Vector2Int operator +(Vector2Int a, int b)
		{
			return new Vector2Int(a.x + b, a.y + b);
		}
		public static Vector2Int operator -(Vector2Int a, int b)
		{
			return new Vector2Int(a.x - b, a.y - b);
		}
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}
		public static Vector2Int operator %(Vector2Int a, int b)
		{
			return new Vector2Int(a.x % b, a.y % b);
		}

		public static implicit operator Vector2(Vector2Int vec)
		{
			return new Vector2(vec.x, vec.y);
		}
		public static explicit operator Vector2Int(Vector2 vec)
		{
			return new Vector2Int((int)vec.x, (int)vec.y);
		}
	}

	public struct Vector3Int
	{
		private static readonly Regex vec_reg = new Regex(@"\{\s*(?<X>-?[0-9]+)\s*,\s*(?<Y>-?[0-9]+)\s*,\s*(?<Z>-?[0-9]+)\s*\}");
		public int x, y, z;
		public Vector3Int(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static int Dot(Vector3Int a, Vector3Int b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public float Length
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y);
			}
		}

		public static Vector3Int Parse(string input)
		{
			var m = vec_reg.Match(input);
			if (m.Success)
			{
				Vector3Int val = new Vector3Int();
				val.x = int.Parse(m.Groups["X"].Value);
				val.y = int.Parse(m.Groups["Y"].Value);
				val.z = int.Parse(m.Groups["Z"].Value);
				return val;
			}
			else
				throw new FormatException("Input string was not in correct format");
		}

		public static bool TryParse(string input, out Vector3Int ret)
		{
			var m = vec_reg.Match(input);
			ret = new Vector3Int();
			if (m.Success)
			{
				if (int.TryParse(m.Groups["X"].Value, out ret.x))
					if (int.TryParse(m.Groups["Y"].Value, out ret.y))
						return int.TryParse(m.Groups["Z"].Value, out ret.z);
					else return false;
				else
					return false;
			}
			else
				return false;
		}

		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}
		public static Vector3Int operator /(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x / b.x, a.y / b.y, a.z / b.z);
		}
		public static Vector3Int operator %(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x % b.x, a.y % b.y, a.z % b.z);
		}

		public static Vector3Int operator +(Vector3Int a, int b)
		{
			return new Vector3Int(a.x + b, a.y + b, a.z + b);
		}
		public static Vector3Int operator -(Vector3Int a, int b)
		{
			return new Vector3Int(a.x - b, a.y - b, a.z - b);
		}
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}
		public static Vector3Int operator %(Vector3Int a, int b)
		{
			return new Vector3Int(a.x % b, a.y % b, a.z % b);
		}

		public static implicit operator Vector3(Vector3Int vec)
		{
			return new Vector3(vec.x, vec.y, vec.z);
		}
		public static explicit operator Vector3Int(Vector3 vec)
		{
			return new Vector3Int((int)vec.x, (int)vec.y, (int)vec.z);
		}
	}
	public struct Vector4Int
	{
		private static readonly Regex vec_reg = new Regex(@"\{\s*(?<X>-?[0-9]+)\s*,\s*(?<Y>-?[0-9]+)\s*,\s*(?<Z>-?[0-9]+)\s*,\s*(?<W>-?[0-9]+)\s*\}");
		public int x, y, z, w;
		public Vector4Int(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static int Dot(Vector4Int a, Vector4Int b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public float Length
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
			}
		}
		public static Vector4Int Parse(string input)
		{
			var m = vec_reg.Match(input);
			if (m.Success)
			{
				var val = new Vector4Int();
				val.x = int.Parse(m.Groups["X"].Value);
				val.y = int.Parse(m.Groups["Y"].Value);
				val.z = int.Parse(m.Groups["Z"].Value);
				val.w = int.Parse(m.Groups["W"].Value);
				return val;
			}
			else
				throw new FormatException("Input string was not in correct format");
		}

		public static bool TryParse(string input, out Vector4Int ret)
		{
			var m = vec_reg.Match(input);
			ret = new Vector4Int();
			if (m.Success)
			{
				if (int.TryParse(m.Groups["X"].Value, out ret.x))
					if (int.TryParse(m.Groups["Y"].Value, out ret.y))
						if (int.TryParse(m.Groups["Z"].Value, out ret.z))
							return int.TryParse(m.Groups["W"].Value, out ret.w);
						else return false;
					else return false;
				else
					return false;
			}
			else
				return false;
		}
		public static Vector4Int operator +(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}
		public static Vector4Int operator -(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}
		public static Vector4Int operator *(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}
		public static Vector4Int operator /(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
		}
		public static Vector4Int operator %(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x % b.x, a.y % b.y, a.z % b.z, a.w % b.w);
		}

		public static Vector4Int operator +(Vector4Int a, int b)
		{
			return new Vector4Int(a.x + b, a.y + b, a.z + b, a.w + b);
		}
		public static Vector4Int operator -(Vector4Int a, int b)
		{
			return new Vector4Int(a.x - b, a.y - b, a.z - b, a.w - b);
		}
		public static Vector4Int operator *(Vector4Int a, int b)
		{
			return new Vector4Int(a.x * b, a.y * b, a.z * b, a.w * b);
		}
		public static Vector4Int operator /(Vector4Int a, int b)
		{
			return new Vector4Int(a.x / b, a.y / b, a.z / b, a.w / b);
		}
		public static Vector4Int operator %(Vector4Int a, int b)
		{
			return new Vector4Int(a.x % b, a.y % b, a.z % b, a.w % b);
		}

		public static implicit operator Vector4(Vector4Int vec)
		{
			return new Vector4(vec.x, vec.y, vec.z, vec.w);
		}
		public static explicit operator Vector4Int(Vector4 vec)
		{
			return new Vector4Int((int)vec.x, (int)vec.y, (int)vec.z, (int)vec.w);
		}
	}
}
