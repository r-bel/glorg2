using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	public class Program : Resource.Resource, IDisposable
	{
		private IntPtr handle;
		internal List<Shader> shaders;
		public IntPtr Handle { get { return handle; } }
		public System.Collections.ObjectModel.ReadOnlyCollection<Shader> Shaders { get { return shaders.AsReadOnly(); } }

		public Uniform GetUniform(string name)
		{
			return new Uniform(OpenGL.glGetUniformLocationARB(handle, name));
		}

		public bool Compile()
		{
			uint err = OpenGL.glGetError();
			foreach (var shader in shaders)
			{
				shader.Compile();
			}
			OpenGL.glLinkProgramARB(handle);
			err = OpenGL.glGetError();
			return err == 0;			
		}

		public string GetCompileLog()
		{
			byte[] val = new byte[8192];
			int len = 0;
			OpenGL.glGetInfoLogARB(handle, val.Length, ref len, val);
			return Encoding.ASCII.GetString(val);
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
