using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	public interface IRenderable
	{
		void Render(float time, Graphics.GraphicsDevice dev);
		void InitializeGraphics();
	}
}
