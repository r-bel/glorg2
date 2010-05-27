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
	public sealed class Box : Primitive
	{
		protected override void Build()
		{
			vb = new VertexBuffer<Vector3>(Vector3.Descriptor);

			vb.Allocate(8);
			ib = new IndexBuffer<ushort>();
			ib.Allocate(4 * 6);

			vb[0] = new Vector3(-0.5f, -0.5f, -0.5f);
			vb[1] = new Vector3(0.5f, -0.5f, -0.5f);
			vb[2] = new Vector3(0.5f, 0.5f, -0.5f);
			vb[3] = new Vector3(-0.5f, 0.5f, -0.5f);

			vb[4] = new Vector3(-0.5f, -0.5f, 0.5f);
			vb[5] = new Vector3(0.5f, -0.5f, 0.5f);
			vb[6] = new Vector3(0.5f, 0.5f, 0.5f);
			vb[7] = new Vector3(-0.5f, 0.5f, 0.5f);

			ib[0] = 0;
			ib[1] = 1;
			ib[2] = 2;
			ib[3] = 3;

			ib[4] = 4;
			ib[5] = 0;
			ib[6] = 3;
			ib[7] = 7;

			ib[8] = 5;
			ib[9] = 4;
			ib[10] = 7;
			ib[11] = 6;

			ib[12] = 1;
			ib[13] = 5;
			ib[14] = 6;
			ib[15] = 2;

			ib[16] = 6;
			ib[17] = 7;
			ib[18] = 3;
			ib[19] = 2;

			ib[20] = 0;
			ib[21] = 4;
			ib[22] = 5;
			ib[23] = 1;

			vb.BufferData(VboUsage.GL_STATIC_DRAW);
			ib.BufferData(VboUsage.GL_STATIC_DRAW);
			// Client data is no longer needed. Free it!
			vb.FreeClientData();
			ib.FreeClientData();
		}
		public override DrawMode DrawMode
		{
			get { return Graphics.DrawMode.Triangles; }
		}
	}
}
