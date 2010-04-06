using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics
{
	public class GraphicsDevice
	{
		protected OpenGL.OpenGLContext context;
		public OpenGL.OpenGLContext Context { get { return context; } }

		public GraphicsDevice(IntPtr target)
		{
			if ((Environment.OSVersion.Platform & PlatformID.Win32NT) == PlatformID.Win32NT)
				context = new OpenGL.WglContext();
			else if ((Environment.OSVersion.Platform & PlatformID.Unix) == PlatformID.Unix)
				context = new OpenGL.glXContext();
			else if ((Environment.OSVersion.Platform & PlatformID.MacOSX) == PlatformID.MacOSX)
				throw new NotImplementedException("Mac OS X not yet supported.");
			
			context.CreateContext(target);
			//OpenGL.OpenGL.InitVbo(context);
		}
	}
}
