using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Glorg2;
using Glorg2.Scene;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;

namespace GlorgIDE
{
	public class IDEApp : Game
	{
		Wireframe wires;
		PerspectiveCamera ide_camera;
		public CursorIcon cursor;
		protected override void Init()
		{
			cursor = new CursorIcon();
			Scene.Background = Colors.DodgerBlue;
			ide_camera = new PerspectiveCamera()
			{
				Position = new Vector4(0, 2f, 0),
				FieldOfView = (float)Math.PI / 3,
				Far = 128f,
				Near = 0.001f,
				Aspect = 1.0f
			};
			Scene.Camera = ide_camera;
			Scene.ParentNode.Add(ide_camera);
		}

		protected override void GraphicsClosing()
		{
			base.GraphicsClosing();
			wires.Dispose();
			cursor.Dispose();
		}

		protected override void InitializeGraphics()
		{
			base.InitializeGraphics();
			wires = new Wireframe()
			{
				Columns = 128,
				Rows = 128,
				Major = 8,
				MajorColor = Colors.White,
				MinorColor = Colors.SlateGray,
				Size = 1f
			};
			wires.InitializeGraphics();
			cursor.InitializeGraphics();
		}

		protected override void Render(GraphicsDevice dev, float frame_time, float total_time)
		{
			dev.Clear(ClearFlags.Color | ClearFlags.Depth, Scene.Background);
			dev.ProjectionMatrix = ide_camera.GetProjectionMatrix();
			dev.ModelviewMatrix = ide_camera.GetTransform().Invert();
			wires.Render(frame_time, dev);
			dev.ModelviewMatrix *= cursor.GetTransform();
			cursor.Render(frame_time, dev);
			dev.ModelviewMatrix = ide_camera.GetTransform().Invert();
			var old_color = Scene.Background;
			Scene.Background = new Vector4(0, 0, 0, 0);
			base.Render(dev, frame_time, total_time);
			Scene.Background = old_color;
		}

	}
}
