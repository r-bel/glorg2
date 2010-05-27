/*
Copyright (C) 2010 Henning Moe

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
			OpenGL.glUseProgram(handle);
		}
		/// <summary>
		/// Makes no program current
		/// </summary>
		public void MakeNonCurrent()
		{
			//OpenGL.glBindProgramARB(OpenGL.Const.GL_PROGRAM_OBJECT_ARB, 0);
			OpenGL.glUseProgram(0);
		}

		public void SetFragmentOutput(string name, uint colorNumber)
		{
			OpenGL.glBindFragDataLocation(handle, colorNumber, name);
		}
		public void SetFragmentOutput(string name)
		{
			OpenGL.glBindFragDataLocation(handle, 1, name);
		}

		public int GetAttributeLocation(string attribute)
		{
			return OpenGL.glGetAttribLocation(handle, attribute);
		}

		public Dictionary<string, Uniform> GetUniforms()
		{
			var unis = new Dictionary<string, Uniform>();
			int[] count = new int[1];
			OpenGL.glGetProgramiv(handle, OpenGL.Const.GL_ACTIVE_UNIFORMS, count);
			for(int i = 0; i < count[0]; i++)
			{
				int len = 0;
				int size = 0;
				uint type = 0;
				byte[] name = new byte[100];
				OpenGL.glGetActiveUniform(handle, (uint)i, 100, ref len, ref size, ref type, name);
				string n = Encoding.ASCII.GetString(name).Trim('\0');
				int pos = OpenGL.glGetUniformLocation(handle, name);
				unis.Add(n, new Uniform(pos));
			}
			return unis;
		}
		/// <summary>
		/// Retrieves a standard uniform
		/// </summary>
		/// <param name="name">Uniform name</param>
		/// <returns></returns>
		public Uniform GetUniform(string name)
		{
			
			var str = Encoding.ASCII.GetBytes(name);
			byte[] bytes = new byte[str.Length + 1];
			str.CopyTo(bytes, 0);
			OpenGL.glUseProgram(handle);
			int loc = OpenGL.glGetUniformLocation(handle, bytes);
			if (loc == -1)
			{
				var err = OpenGL.glGetError();
				return null;
			}
			else
				return new Uniform(loc);
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
			var u = GetUniform(name);
			if (u == null)
				return default(T);
			var ret = new T();
			ret.name = name;
			ret.uniform = GetUniform(name);
			return ret;
		}
		public void Debug()
		{
			int[] i = new int[1];
			OpenGL.glGetProgramiv(handle, OpenGL.Const.GL_ACTIVE_ATTRIBUTES, i);

		}
		/// <summary>
		/// Compiles all shaders and links the program
		/// </summary>
		/// <returns>True if program compiled and linked</returns>
		/// <remarks>Use GetCompileLog to retrieve compile errors if any</remarks>
		public bool Compile()
		{
			bool success = true; ;
			uint err = OpenGL.glGetError();
			foreach (var shader in shaders)
			{
				if (!shader.Compile())
					success = false;
				OpenGL.glAttachShader(handle, shader.Handle);
				err = OpenGL.glGetError();
				if (err != OpenGL.Const.GL_NO_ERROR)
					success = false;
			}
			OpenGL.glLinkProgram(handle);
			
			var status = new int[1];
			OpenGL.glGetProgramiv(handle, OpenGL.Const.GL_LINK_STATUS, status);
			if (status[0] == 0)
				return false;
			else
				return success;
		}
		public bool Validate(out string ret)
		{
			var stat = new int[1];
			OpenGL.glValidateProgram(handle);
			OpenGL.glGetProgramiv(handle, OpenGL.Const.GL_VALIDATE_STATUS, stat);
			ret = GetLinkLog();
			return stat[0] != 0;
		}

		/// <summary>
		/// Retrievs the compile log for this shader
		/// </summary>
		/// <returns></returns>
		public string GetLinkLog()
		{
			byte[] fill;
			int lv = 0;
			int[] len = new int[1];
			OpenGL.glGetProgramiv(handle, OpenGL.Const.GL_INFO_LOG_LENGTH, len);
			if (len[0] > 1)
			{
				fill = new byte[len[0]];
				OpenGL.glGetProgramInfoLog(handle, len[0], ref lv, fill);
				return Encoding.ASCII.GetString(fill);
			}
			else 
				return "";
		}


		public Program()
		{
			shaders = new List<Shader>();
			handle = OpenGL.glCreateProgram();
		}
		/// <summary>
		/// Cleans up unmanaged resources.
		/// </summary>
		private void Cleanup()
		{
			foreach (var sh in shaders)
			{
				OpenGL.glDetachShader(handle, sh.Handle);
				sh.Dispose();
			}
			shaders.Clear();
			OpenGL.glDeleteProgram(handle);
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
