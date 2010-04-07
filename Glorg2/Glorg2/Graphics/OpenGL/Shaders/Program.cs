using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	public class Program : IDisposable
	{
		private IntPtr handle;
		internal List<Shader> shaders;
		public IntPtr Handle { get { return handle; } }
		public System.Collections.ObjectModel.ReadOnlyCollection<Shader> Shaders { get { return shaders.AsReadOnly(); } }

		public Uniform GetUniform(string name)
		{
			return new Uniform(OpenGL.glGetUniformLocationARB(handle, name));
		}

		public void Compile()
		{
			foreach (var shader in shaders)
			{
				shader.Compile();
			}
			OpenGL.glLinkProgramARB(handle);
		}


		public Program()
		{
			shaders = new List<Shader>();
			handle = OpenGL.glCreateProgramObjectARB();
		}

		protected void Cleanup()
		{
			OpenGL.glDeleteObjectARB(handle);
		}

		public void Dispose()
		{
			Cleanup();
			GC.SuppressFinalize(this);
		}
		~Program()
		{
			Cleanup();
		}
	}
}
