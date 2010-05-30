using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Glorg2;
using Glorg2.Scene;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;

namespace Tutorial
{
	public class DynamicMesh : DynamicNode, IRenderable
	{
		protected Glorg2.Graphics.Model model;
		protected Glorg2.Graphics.Material mat;

		#region IRenderable Members

		public void Render(float time, Glorg2.Graphics.GraphicsDevice dev)
		{
			dev.SetActiveMaterial(mat);
			dev.SetVertexBuffer(model.VertexBuffer);
			foreach (var part in model.Parts)
			{
				if (part.Material != null)
					dev.SetActiveMaterial(part.Material);
				dev.SetIndexBuffer(part.IndexBuffer);
				dev.Draw(Glorg2.Graphics.DrawMode.Triangles);
			}
			dev.SetVertexBuffer(null);
			dev.SetIndexBuffer(null);
			dev.SetActiveMaterial(null);
		}

		public virtual void InitializeGraphics()
		{

		}

		public bool GraphicsInvalidated { get; set; }
		public int Priority { get; set; }
		public bool GraphicsInitialized
		{
			get { return model != null && mat != null; }
		}

		#endregion
	}
}
