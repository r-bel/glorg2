using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics
{
	public sealed class GraphicsDevice : IDisposable
	{
		private OpenGL.OpenGLContext context;
		
		internal OpenGL.IVertexBuffer vertex_buffer;
		internal OpenGL.IIndexBuffer index_buffer;

		public OpenGL.OpenGLContext Context { get { return context; } }

		public GraphicsDevice(IntPtr target)
		{
			if ((Environment.OSVersion.Platform & PlatformID.Win32NT) == PlatformID.Win32NT)
				context = new OpenGL.WglContext();
			else if ((Environment.OSVersion.Platform & PlatformID.Unix) == PlatformID.Unix)
				context = new OpenGL.glXContext();
			else if ((Environment.OSVersion.Platform & PlatformID.MacOSX) == PlatformID.MacOSX)
				throw new NotImplementedException("Mac OS X not yet supported.");
			
			// Create context using platform specific methods
			context.CreateContext(target);
			
			// Initialize Vertex Buffer Objects
			OpenGL.OpenGL.InitVbo(context);
			
			// Initialize shaders
			OpenGL.OpenGL.InitShaderProgram(context);
		}

		public void MakeCurrent(OpenGL.Shaders.Program prog)
		{
			OpenGL.OpenGL.glUseProgramObjectARB(prog.Handle);
		}
		public void MakeCurrent(OpenGL.Texture texture)
		{
			OpenGL.OpenGL.glBindTexture((uint)texture.target, texture.Handle);
		}
		public void SetVertexBuffer(OpenGL.IVertexBuffer vert)
		{
			if (vertex_buffer != null)
			{
				vertex_buffer.Reset();
			}
			if (vert != null)
				vert.MakeCurrent();
			vertex_buffer = vert;
		}
		public void SetIndexBuffer(OpenGL.IIndexBuffer indices)
		{
			if (index_buffer != null)
				index_buffer.Reset();
			if (indices != null)
				index_buffer.MakeCurrent();
			index_buffer = indices;
		}

		public void Clear(ClearFlags buffers, Vector4 color, double depth, int stencil)
		{
			OpenGL.OpenGL.glClearColor(color.x, color.y, color.z, color.w);
			OpenGL.OpenGL.glClearDepth(depth);
			OpenGL.OpenGL.glClearStencil(stencil);
			OpenGL.OpenGL.glClear((uint)buffers);
		}

		public void Clear(ClearFlags buffers, Vector4 color, double depth)
		{
			OpenGL.OpenGL.glClearColor(color.x, color.y, color.z, color.w);
			OpenGL.OpenGL.glClearDepth(depth);
			OpenGL.OpenGL.glClear((uint)buffers);
		}
		public void Clear(ClearFlags buffers, Vector4 color)
		{
			OpenGL.OpenGL.glClearColor(color.x, color.y, color.z, color.w);
			OpenGL.OpenGL.glClear((uint)buffers);
		}
		public void Clear(ClearFlags buffers)
		{
			OpenGL.OpenGL.glClear((uint)buffers);
		}

		public void Draw(DrawMode mode)
		{
			if (index_buffer != null)
				OpenGL.OpenGL.glDrawElements((uint)mode, index_buffer.Count, index_buffer.Type, IntPtr.Zero);
			else
				OpenGL.OpenGL.glDrawArrays((uint)mode, 0, vertex_buffer.Count);
		}

		public unsafe Matrix ProjectionMatrix
		{
			get
			{
				Matrix ret = new Matrix();
				OpenGL.OpenGL.glGetFloatv((uint)OpenGL.OpenGL.Const.GL_PROJECTION_MATRIX, ref ret);
				return ret;
			}
			set
			{
				OpenGL.OpenGL.glMatrixMode((uint)OpenGL.OpenGL.Const.GL_PROJECTION);
				OpenGL.OpenGL.glLoadMatrixf(ref value);
			}
		}
		public Matrix ModelviewMatrix
		{
			get
			{
				Matrix ret = new Matrix();
				OpenGL.OpenGL.glGetFloatv((uint)OpenGL.OpenGL.Const.GL_MODELVIEW_MATRIX, ref ret);
				return ret;
			}
			set
			{
				OpenGL.OpenGL.glMatrixMode((uint)OpenGL.OpenGL.Const.GL_MODELVIEW);
				OpenGL.OpenGL.glLoadMatrixf(ref value);
			}
		}
		public Matrix TextureMatrix
		{
			get
			{
				Matrix ret = new Matrix();
				OpenGL.OpenGL.glGetFloatv((uint)OpenGL.OpenGL.Const.GL_TEXTURE_MATRIX, ref ret);
				return ret;
			}
			set
			{
				OpenGL.OpenGL.glMatrixMode((uint)OpenGL.OpenGL.Const.GL_TEXTURE);
				OpenGL.OpenGL.glLoadMatrixf(ref value);
			}
		}
		public void Present()
		{
			context.Swap();
		}

		private void Cleanup()
		{
			context.Dispose();
		}

		public void Dispose()
		{
			Cleanup();
			GC.SuppressFinalize(this);
		}
		~GraphicsDevice()
		{
			Cleanup();
		}
	}
	public enum DrawMode : uint
	{
		Triangles = OpenGL.OpenGL.Const.GL_TRIANGLES,
		TriangleFan = OpenGL.OpenGL.Const.GL_TRIANGLE_FAN,
		TriangleStrip = OpenGL.OpenGL.Const.GL_TRIANGLE_STRIP,
		Quads = OpenGL.OpenGL.Const.GL_QUADS,
		QuadStrip = OpenGL.OpenGL.Const.GL_QUAD_STRIP,
		Polygon = OpenGL.OpenGL.Const.GL_POLYGON,
		Points = OpenGL.OpenGL.Const.GL_POINTS,
		Lines = OpenGL.OpenGL.Const.GL_LINES,
		LineLoop = OpenGL.OpenGL.Const.GL_LINE_LOOP,
		LineStrip = OpenGL.OpenGL.Const.GL_LINE_STRIP
	}
	public enum ClearFlags : uint
	{
		Color = OpenGL.OpenGL.Const.GL_COLOR_BUFFER_BIT,
		Depth = OpenGL.OpenGL.Const.GL_DEPTH_BUFFER_BIT,
		Stencil = OpenGL.OpenGL.Const.GL_STENCIL_BUFFER_BIT
	}
}
