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

		private OpenGLState state;

		public OpenGLState State { get { return state; } }

		public GraphicsDevice(IntPtr target)
		{
			if ((Environment.OSVersion.Platform & PlatformID.Win32NT) == PlatformID.Win32NT)
				context = new OpenGL.WglContext();
			else if ((Environment.OSVersion.Platform & PlatformID.Unix) == PlatformID.Unix)
				context = new OpenGL.glXContext();
			else if ((Environment.OSVersion.Platform & PlatformID.MacOSX) == PlatformID.MacOSX)
				throw new NotSupportedException("Mac OS X not yet supported.");
			
			// Create context using platform specific methods
			context.CreateContext(target);
			var err = OpenGL.OpenGL.glGetError();
			// Initialize Vertex Buffer Objects
			OpenGL.OpenGL.InitVbo(context);
			err = OpenGL.OpenGL.glGetError();
			// Initialize shaders
			
			OpenGL.OpenGL.InitShaderProgram(context);
			err = OpenGL.OpenGL.glGetError();

			OpenGL.OpenGL.InitOcclusionQueries(context);
			err = OpenGL.OpenGL.glGetError();

			state = new OpenGLState(this);
			state.Culling = true;
			state.DepthTest = true;
			
			
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

		public System.Drawing.Rectangle Viewport
		{
			get
			{
				int[] values = new int[4];
				OpenGL.OpenGL.glGetIntegerv(OpenGL.OpenGL.Const.GL_VIEWPORT, values);
				return new System.Drawing.Rectangle(values[0], values[1], values[2], values[3]);
				
			}
			set
			{
				OpenGL.OpenGL.glViewport(value.X, value.Y, value.Width, value.Height);
			}
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
				OpenGL.OpenGL.glGetFloatv(OpenGL.OpenGL.Const.GL_PROJECTION_MATRIX, ref ret);
				return ret;
			}
			set
			{
				OpenGL.OpenGL.glMatrixMode((uint)OpenGL.OpenGL.Const.GL_PROJECTION);
				OpenGL.OpenGL.glLoadMatrixf(ref value);
			}
		}
		public unsafe Matrix ModelviewMatrix
		{
			get
			{
				Matrix ret = new Matrix();
				OpenGL.OpenGL.glGetFloatv(OpenGL.OpenGL.Const.GL_MODELVIEW_MATRIX, ref ret);
				return ret;
			}
			set
			{
				OpenGL.OpenGL.glMatrixMode((uint)OpenGL.OpenGL.Const.GL_MODELVIEW);
				OpenGL.OpenGL.glLoadMatrixf(ref value);
			}
		}
		public unsafe Matrix TextureMatrix
		{
			get
			{
				Matrix ret = new Matrix();
				OpenGL.OpenGL.glGetFloatv(OpenGL.OpenGL.Const.GL_TEXTURE_MATRIX, ref ret);
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
	public sealed class OpenGLState
	{
		GraphicsDevice owner;
		internal OpenGLState(GraphicsDevice owner)
		{
			this.owner = owner;
		}

		public bool DepthTest
		{
			get
			{
				return OpenGL.OpenGL.glIsEnabled(OpenGL.OpenGL.Const.GL_DEPTH_TEST) == Glorg2.Graphics.OpenGL.OpenGL.boolean.TRUE;
			}
			set
			{
				if (value)
					OpenGL.OpenGL.glEnable(OpenGL.OpenGL.Const.GL_DEPTH_TEST);
				else
					OpenGL.OpenGL.glDisable(OpenGL.OpenGL.Const.GL_DEPTH_TEST);
			}
		}

		public bool Lighting
		{
			get
			{
				return OpenGL.OpenGL.glIsEnabled(OpenGL.OpenGL.Const.GL_LIGHTING) == Glorg2.Graphics.OpenGL.OpenGL.boolean.TRUE;
			}
			set
			{
				if (value)
					OpenGL.OpenGL.glEnable(OpenGL.OpenGL.Const.GL_LIGHTING);
				else
					OpenGL.OpenGL.glDisable(OpenGL.OpenGL.Const.GL_LIGHTING);
			}
		}
		public bool Culling
		{
			get
			{
				return OpenGL.OpenGL.glIsEnabled(OpenGL.OpenGL.Const.GL_CULL_FACE) == Glorg2.Graphics.OpenGL.OpenGL.boolean.TRUE;
			}
			set
			{
				if (value)
					OpenGL.OpenGL.glEnable(OpenGL.OpenGL.Const.GL_CULL_FACE);
				else
					OpenGL.OpenGL.glDisable(OpenGL.OpenGL.Const.GL_CULL_FACE);
			}
		}
		public CullFace CullFace
		{
			get
			{
				int[] vals = new int[1];
				OpenGL.OpenGL.glGetIntegerv(OpenGL.OpenGL.Const.GL_CULL_FACE_MODE, vals);
				return (CullFace)vals[0];
			}
			set
			{
				OpenGL.OpenGL.glCullFace((uint)value);
			}
		}
		public PolygonMode GetPolygonMode(CullFace face)
		{
				int[] vals = new int[1];
				OpenGL.OpenGL.glGetIntegerv(OpenGL.OpenGL.Const.GL_POLYGON_MODE, vals);
				return (PolygonMode)vals[0];
		}
		public void SetPolygonMode(CullFace face, PolygonMode mode)
		{
			OpenGL.OpenGL.glPolygonMode((uint)face, (uint)mode);
		}

		
	}
	public enum CullFace : uint
	{
		Front = OpenGL.OpenGL.Const.GL_FRONT,
		Back = OpenGL.OpenGL.Const.GL_BACK,
		FrontAndBack = OpenGL.OpenGL.Const.GL_FRONT_AND_BACK
	}
	public enum PolygonMode : uint
	{
		Line = OpenGL.OpenGL.Const.GL_LINE,
		Point = OpenGL.OpenGL.Const.GL_POINT,
		Fill = OpenGL.OpenGL.Const.GL_FILL
	}
}
