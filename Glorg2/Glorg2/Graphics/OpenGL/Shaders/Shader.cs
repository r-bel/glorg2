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
	public abstract class Shader : IDisposable
	{
		private IntPtr handle;
		private string source;
		private Program parent;

		public IntPtr Handle { get { return handle; } }
		public string Source { get { return source; } }
		public Program Program { get { return parent; } }

		protected Shader(string source, uint type, Program parent)
		{
			this.source = source;
			handle = OpenGL.glCreateShaderObjectARB(type);
			OpenGL.glShaderSourceARB(handle, 1, new string[] { source }, new int[] { source.Length });
			this.parent = parent;
			parent.shaders.Add(this);
		}

		internal void Compile()
		{
			OpenGL.glCompileShaderARB(handle);
			OpenGL.glAttachObjectARB(parent.Handle, handle);
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
		~Shader()
		{
			Cleanup();
		}
	}
	/// <summary>
	/// Represents a vertex shader
	/// </summary>
	public class VertexShader : Shader
	{
		public VertexShader(string source, Program parent)
			: base(source, OpenGL.Const.GL_VERTEX_SHADER_ARB, parent)
		{
		}

	}
	/// <summary>
	/// Represents a fragment shader
	/// </summary>
	public class FragmentShader : Shader
	{
		public FragmentShader(string source, Program parent)
			: base(source, OpenGL.Const.GL_FRAGMENT_SHADER_ARB, parent)
		{
		}
	}
	/// <summary>
	/// Represents a geometry shader
	/// </summary>
	public class GeometryShader : Shader
	{
		public GeometryShader(string source, Program parent)
			: base(source, OpenGL.Const.GL_GEOMETRY_SHADER_ARB, parent)
		{
		}
	}
}
