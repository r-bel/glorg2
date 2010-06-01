using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	public struct Line2D
	{
		public Vector2 Start { get; set; }
		public Vector2 End { get; set; }

		public Vector2 GetNormal()
		{
			return (End - Start).Normalize();
		}
		public float Length { get { return (End - Start).Length; } }

	}
}
