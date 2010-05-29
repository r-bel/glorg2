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

		string material_name;
		string model_name;

		public bool GraphicsInitialized { get { return init_finished; } internal set { init_finished = value; } }

		public bool GraphicsInvalidated { get; set; }

		public Graphics.Model Model { get { return model; } }

		public string MaterialName { get { return material_name; } set { material_name = value; Owner.Owner.GraphicInvoke(new Action(InitializeGraphics)); } }
		public string ModelName { get { return model_name; } set { model_name = value; Owner.Owner.GraphicInvoke(new Action(InitializeGraphics)); } }


		public virtual void InitializeGraphics()
		{
				if (mat != null)
					mat.Dispose();
				if (model != null)
					model.Dispose();


			if (!string.IsNullOrEmpty(material_name))
			{
				Owner.Resources.Load(material_name, out mat);
			}

			if (!string.IsNullOrEmpty(model_name))
			{
				Owner.Resources.Load(model_name, out model);
			}

			init_finished = true;
		}

		public override void DoDispose()
		{
			base.DoDispose();
			if(mat != null)
				mat.Dispose();
			if(model != null)
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
