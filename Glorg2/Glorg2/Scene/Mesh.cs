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

		public Graphics.Model Model { get { return model; } }

		public virtual void InitializeGraphics()
		{
			
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
			if (model != null)
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
