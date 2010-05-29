using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics
{
	public class Heightmap : Glorg2.Resource.Resource
	{

		int w, h;

		public int Width { get { return w; } }
		public int Height { get { return h;} }

		float[] data;

		public Heightmap(int width, int height)
		{
			data = new float[width * height];
			w = width;
			h = height;
		}
		public float this[int column, int row]
		{
			get
			{
				if (column < 0)
					column = w - column;
				if (row < 0)
					row = h - row;
				return data[(row % h) * w + (column % w)];
			}
			set
			{
				data[(row % h) * w + (column % w)] = value;
			}
		}
		/// <summary>
		/// Returns a bilinear interpolation value
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">y position</param>
		/// <returns></returns>
		public float Sample(float x, float y)
		{
			int x1 = (int)(x);
			int y1 = (int)(y);
			int x2 = x1 + 1;
			int y2 = y1 + 1;

			float x1v = Interpolation.Lerp(this[x1, y1], this[x2, y1], x - x1);
			float x2v = Interpolation.Lerp(this[x1, y2], this[x2, y2], x - x1);
			return Interpolation.Lerp(x1v, x2v, y - y1);
		}
		public float this[int index]
		{
			get
			{
				return data[index];
			}
			set
			{
				data[index] = value;
			}
		}
	}
}
