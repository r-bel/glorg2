using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
	public class Mesh : Node
	{
		[NonSerialized()]
		protected Graphics.Model model;

		public Graphics.Model Model { get { return model; } set { model = value; } }

		public Mesh()
		{
		}

		public Mesh(Scene owner)
			: base(owner)
		{
		}

		public override void DoDispose()
		{
			base.DoDispose();
			model.Dispose();
		}
		

		protected override void Render(float time, Graphics.GraphicsDevice dev)
		{
			if (model != null)
			{
				dev.SetVertexBuffer(model.VertexBuffer);
				foreach (var part in model.Parts)
				{
					dev.SetIndexBuffer(part.IndexBuffer);
					dev.Draw(Graphics.DrawMode.Triangles);
				}
				dev.SetIndexBuffer(null);
				dev.SetVertexBuffer(null);
			}

			base.Render(time, dev);
		}


	}
}
