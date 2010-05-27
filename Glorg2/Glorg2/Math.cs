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
	public static class Interpolation
	{
		/// <summary>
		/// Linear interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * t;
		}
		/// <summary>
		/// Linear interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			return a + (b - a) * t;
		}
		/// <summary>
		/// Linear interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			return a + (b - a) * t;
		}
		/// <summary>
		/// Linear interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			return a + (b - a) * t;
		}
		/// <summary>
		/// Quadratic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>

		public static float QuadraticBezier(float a, float b, float c, float t)
		{
			return (1 - t) * (1 - t) * a + 2 * (1 - t) * b + t * t * c;
		}
		/// <summary>
		/// Quadratic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>

		public static Vector2 QuadraticBezier(Vector2 a, Vector2 b, Vector2 c, float t)
		{
			return (1 - t) * (1 - t) * a + 2 * (1 - t) * b + t * t * c;
		}
		/// <summary>
		/// Quadratic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>

		public static Vector3 QuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t)
		{
			return (1 - t) * (1 - t) * a + 2 * (1 - t) * b + t * t * c;
		}
		/// <summary>
		/// Quadratic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>

		public static Vector4 QuadraticBezier(Vector4 a, Vector4 b, Vector4 c, float t)
		{
			return (1 - t) * (1 - t) * a + 2 * (1 - t) * b + t * t * c;
		}
		/// <summary>
		/// Cubic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="d">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static float CubicBezier(float a, float b, float c, float d, float t)
		{
			float t1 = (1 - t);
			return t1 * t1 * t1 * a + 3 * t1 * t1 * t * b + 3 * t1 * t * t * c + t * t * t * d;
		}
		/// <summary>
		/// Cubic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="d">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static Vector2 CubicBezier(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
		{
			float t1 = (1 - t);
			return t1 * t1 * t1 * a + 3 * t1 * t1 * t * b + 3 * t1 * t * t * c + t * t * t * d;
		}
		/// <summary>
		/// Cubic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="d">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static Vector3 CubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
		{
			float t1 = (1 - t);
			return t1 * t1 * t1 * a + 3 * t1 * t1 * t * b + 3 * t1 * t * t * c + t * t * t * d;
		}

		/// <summary>
		/// Cubic bezier interpolation
		/// </summary>
		/// <param name="a">start value</param>
		/// <param name="b">intermediate value</param>
		/// <param name="d">intermediate value</param>
		/// <param name="c">end value</param>
		/// <param name="t">interpolation value. Should be between 0.0 and 1.0</param>
		/// <returns></returns>
		public static Vector4 CubicBezier(Vector4 a, Vector4 b, Vector4 c, Vector4 d, float t)
		{
			float t1 = (1 - t);
			return t1 * t1 * t1 * a + 3 * t1 * t1 * t * b + 3 * t1 * t * t * c + t * t * t * d;
		}
	}
}
