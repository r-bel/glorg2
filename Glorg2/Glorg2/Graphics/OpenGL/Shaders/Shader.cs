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
