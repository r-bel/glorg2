using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;


namespace Glorg2.Scene
{
	public class DynamicMesh : DynamicNode, IRenderable
	{
		[NonSerialized()]
		protected Graphics.Model model;
		[NonSerialized()]
		protected Graphics.StdMaterial mat;

		[NonSerialized()]
		protected bool init_finished;

		public bool GraphicsInitialized { get { return init_finished; } internal set { init_finished = value; } }

		public Graphics.Model Model { get { return model; } }

		public virtual void InitializeGraphics()
		{
			init_finished = true;
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
