using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics.MeshBuilders;
using Glorg2.Scene;
namespace GlorgIDE
{
	public class CursorIcon : Mesh
	{

		public override void InitializeGraphics()
		{
			if (mat == null)
			{
				Glorg2.Resource.MaterialImporter imp = new Glorg2.Resource.MaterialImporter();
				using (var stream = System.IO.File.OpenRead(".\\shaders\\Default.mxl"))
				{
					mat = imp.Import<StdMaterial>(stream, "Default", null);
				}
			}
				CylinderBuilder builder = new CylinderBuilder()
				{
					Height = 1,
					Radius = 1,
					Sides = 32
				};
				model = builder.Build();

		}
		
	}
}
