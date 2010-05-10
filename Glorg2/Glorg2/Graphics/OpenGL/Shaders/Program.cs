using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	/// <summary>
	/// Represents a shader program.
	/// </summary>
	public sealed class Program : Resource.Resource, IDeviceObject
	{
		private uint handle;
		internal List<Shader> shaders;
		public uint Handle { get { return handle; } }
		/// <summary>
		/// Shaders linked to this program
		/// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Shader> Shaders { get { return shaders.AsReadOnly(); } }

		/// <summary>
		/// Makes this program current
		/// </summary>
		public void MakeCurrent()
		{
			//OpenGL.glBindProgramARB(OpenGL.Const.GL_PROGRAM_OBJECT_ARB, handle);
			OpenGL.glUseProgramObjectARB(handle);
		}
		/// <summary>
		/// Makes no program current
		/// </summary>
		public void MakeNonCurrent()
		{
			//OpenGL.glBindProgramARB(OpenGL.Const.GL_PROGRAM_OBJECT_ARB, 0);
			OpenGL.glUseProgramObjectARB(0);
		}
		/// <summary>
		/// Retrieves a standard uniform
		/// </summary>
		/// <param name="name">Uniform name</param>
		/// <returns></returns>
		public Uniform GetUniform(string name)
		{
			return new Uniform(OpenGL.glGetUniformLocationARB(handle, name));
		}

		/// <summary>
		/// Retrieves the named uniform of type T with subtype S
		/// </summary>
		/// <typeparam name="T">Uniform type</typeparam>
		/// <typeparam name="S">Data type (float, Vector3 etc.)</typeparam>
		/// <param name="name">Uniform name</param>
		/// <returns></returns>
		public T GetUniformType<T, S>(string name)
			where T : UniformBaseType<S>, new()
		{
			var ret = new T();
			ret.name = name;
			ret.uniform = GetUniform(name);
			return ret;
		}

		/// <summary>
		/// Compiles all shaders and links the program
		/// </summary>
		/// <returns>True if program compiled and linked</returns>
		/// <remarks>Use GetCompileLog to retrieve compile errors if any</remarks>
		public bool Compile()
		{
			uint err = OpenGL.glGetError();
			foreach (var shader in shaders)
			{
				shader.Compile();
				OpenGL.glAttachObjectARB(handle, shader.Handle);
			}
			OpenGL.glLinkProgramARB(handle);
			err = OpenGL.glGetError();
			return err == 0;			
		}

		/// <summary>
		/// Retrievs the compile log for this shader
		/// </summary>
		/// <returns></returns>
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
		/// <summary>
		/// Cleans up unmanaged resources.
		/// </summary>
		protected void Cleanup()
		{
			foreach (var sh in shaders)
			{
				sh.Dispose();
			}
			shaders.Clear();
			OpenGL.glDeleteObjectARB(handle);
		}

		/// <summary>
		/// Disposes all unmanaged resources.
		/// </summary>
		public override void  DoDispose()
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
