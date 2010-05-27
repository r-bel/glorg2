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
	/// Base class for shaders
	/// </summary>
	/// <seealso cref="VertexShader"/>
	/// <seealso cref="FragmentShader"/>
	/// <seealso cref="GeometryShader"/>
	public abstract class Shader : Resource.Resource
	{
		private uint handle;
		private string source;
		private bool is_compiled;

		public uint Handle { get { return handle; } }
		public string Source { get { return source; } }

		protected Shader(string source, uint type, Program parent)
			: this(source, type)
		{
			parent.shaders.Add(this);
		}
		internal Shader(string source, uint type)
		{
			this.source = source;
			handle = OpenGL.glCreateShader(type);
			OpenGL.glShaderSource(handle, 1, new string[] { source }, new int[] { source.Length });
		}

		internal bool Compile()
		{
			if (!is_compiled)
			{
				OpenGL.glCompileShader(handle);
				var status = new int[1];
				OpenGL.glGetShaderiv(handle, OpenGL.Const.GL_COMPILE_STATUS, status);
				is_compiled = true;
				return status[0] != 0;
			}
			else
			{
				var status = new int[1];
				OpenGL.glGetShaderiv(handle, OpenGL.Const.GL_COMPILE_STATUS, status);
				return status[0] != 0;
			}
		}
		public string GetCompileLog()
		{
			int[] len = new int[1];
			OpenGL.glGetShaderiv(handle, OpenGL.Const.GL_INFO_LOG_LENGTH, len);
			if (len[0] > 1)
			{
				byte[] fill = new byte[len[0]];
				int tl = 0;
				OpenGL.glGetShaderInfoLog(handle, len[0], ref tl, fill);
				return Encoding.ASCII.GetString(fill);
			}
			else
				return "";
		}
		protected void Cleanup()
		{
			OpenGL.glDeleteShader(handle);
		}

		public override void  DoDispose()
		{
			Cleanup();
			GC.SuppressFinalize(this);
		}
		~Shader()
		{
			Cleanup();
		}
	}
	/// <summary>
	/// Represents a vertex shader
	/// </summary>
	public sealed class VertexShader : Shader
	{
		public VertexShader(string source, Program parent)
			: base(source, OpenGL.Const.GL_VERTEX_SHADER, parent)
		{
		}

		internal VertexShader(string source)
			: base(source, OpenGL.Const.GL_VERTEX_SHADER)
		{
		}

	}
	/// <summary>
	/// Represents a fragment shader
	/// </summary>
	public sealed class FragmentShader : Shader
	{
		public FragmentShader(string source, Program parent)
			: base(source, OpenGL.Const.GL_FRAGMENT_SHADER, parent)
		{
		}
		internal FragmentShader(string source)
			: base(source, OpenGL.Const.GL_FRAGMENT_SHADER)
		{
		}
	}
	/// <summary>
	/// Represents a geometry shader
	/// </summary>
	public sealed class GeometryShader : Shader
	{
		public GeometryShader(string source, Program parent)
			: base(source, OpenGL.Const.GL_GEOMETRY_SHADER, parent)
		{
		}
		internal GeometryShader(string source)
			: base(source, OpenGL.Const.GL_GEOMETRY_SHADER)
		{
		}
	}
}
