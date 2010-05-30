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

namespace Glorg2.Scene
{
	[Serializable()]
	public class Mesh : Node, IRenderable
	{
		[NonSerialized()]
		protected Graphics.Model model;
		[NonSerialized()]
		protected Graphics.StdMaterial mat;

		[NonSerialized()]
		protected bool init_finished;

		public bool GraphicsInitialized { get { return init_finished; } internal set { init_finished = value; } }

		public Graphics.Model Model { get { return model; } }

		public bool GraphicsInvalidated { get; set; }
		public int Priority { get; set; }
		public virtual void InitializeGraphics()
		{
			init_finished = true;
		}

		public Mesh()
		{
		}

		public override void DoDispose()
		{
			base.DoDispose();
			mat.Dispose();
			model.Dispose();
		}
		

		public virtual void Render(float time, Graphics.GraphicsDevice dev)
		{
			if (model != null && mat != null)
			{
				if (mat != null)
					dev.SetActiveMaterial(mat);
				dev.SetVertexBuffer(model.VertexBuffer);
				foreach (var part in model.Parts)
				{
					dev.SetIndexBuffer(part.IndexBuffer);
					dev.Draw(Graphics.DrawMode.Triangles);
				}
				dev.SetIndexBuffer(null);
				dev.SetVertexBuffer(null);
				dev.SetActiveMaterial(null);
			}
			
		}


	}
}
