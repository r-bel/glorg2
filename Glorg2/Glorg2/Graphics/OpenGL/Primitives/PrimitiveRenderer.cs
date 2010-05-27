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

namespace Glorg2.Graphics.OpenGL.Primitives
{
	public class PrimitiveRenderer
	{
		List<Primitive> primitives;

		public List<Primitive> Primitives { get { return primitives; } }

		public PrimitiveRenderer()
		{
			primitives = new List<Primitive>();
		}

		public void Draw<T>(GraphicsDevice dev, Vector3 scale)
		{
			var prim = primitives.Find(p => p is T);
			if (prim != null)
			{
				dev.SetVertexBuffer(prim.vb);
				dev.SetIndexBuffer(prim.ib);
				dev.Draw(prim.DrawMode);
			}
		}
		public void DrawLines<T>(GraphicsDevice dev, Vector3 scale)
		{
			var prim = primitives.Find(p => p is T);
			if (prim != null)
			{

				dev.SetVertexBuffer(prim.vb);
				dev.SetIndexBuffer(prim.ib);
				dev.State.SetPolygonMode(CullFace.FrontAndBack, PolygonMode.Line);
				dev.Draw(prim.DrawMode);
				dev.State.SetPolygonMode(CullFace.FrontAndBack, PolygonMode.Fill);
			}
		}
	}
}
