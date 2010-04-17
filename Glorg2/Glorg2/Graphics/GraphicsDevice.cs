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

		/// <summary>
		/// Gets the context for this rendering device
		/// </summary>
		public OpenGL.OpenGLContext Context { get { return context; } }

		private OpenGLState state;

		/// <summary>
		/// Gets the state of this rendering device
		/// </summary>
		public OpenGLState State { get { return state; } }

		public GraphicsDevice(IntPtr target)
		{
			if ((Environment.OSVersion.Platform & PlatformID.Win32NT) == PlatformID.Win32NT)
				context = new OpenGL.WglContext();
			else if ((Environment.OSVersion.Platform & PlatformID.Unix) == PlatformID.Unix)
				context = new OpenGL.glXContext();
			else if ((Environment.OSVersion.Platform & PlatformID.MacOSX) == PlatformID.MacOSX)
				throw new NotSupportedException("Mac OS X not yet supported.");

			context.Samples = 4;
			// Create context using platform specific methods
			context.CreateContext(target);

			foreach (var str in OpenGL.OpenGL.GetSupportedExtensions())
				Console.WriteLine(str);

			var err = OpenGL.OpenGL.glGetError();
			// Initialize Vertex Buffer Objects
			OpenGL.OpenGL.InitVbo(context);
			err = OpenGL.OpenGL.glGetError();
			// Initialize shaders
			
			OpenGL.OpenGL.InitShaderProgram(context);
			err = OpenGL.OpenGL.glGetError();

			// Initialize occlusion queries
			OpenGL.OpenGL.InitOcclusionQueries(context);
			err = OpenGL.OpenGL.glGetError();

			OpenGL.OpenGL.InitMultiTexture(context);

			OpenGL.OpenGL.glEnable(OpenGL.OpenGL.Const.GL_TEXTURE_2D);

			state = new OpenGLState(this);
			state.Culling = true;
			state.DepthTest = true;
			state.Normalize = true;
			
			
		}

		/// <summary>
		/// Applies a shader program.
		/// </summary>
		/// <param name="prog">Program to apply. Set this to null to disable program</param>
		public void MakeCurrent(OpenGL.Shaders.Program prog)
		{
			OpenGL.OpenGL.glUseProgramObjectARB(prog.Handle);
		}
		/// <summary>
		/// Applies a texture
		/// </summary>
		/// <param name="texture"></param>
		public void MakeCurrent(OpenGL.Texture texture, uint index)
		{
			OpenGL.OpenGL.glActiveTextureARB(index);
			OpenGL.OpenGL.glBindTexture((uint)texture.target, texture.Handle);
		}
		/// <summary>
		/// Sets a vertex buffer as the current buffer. 
		/// </summary>
		/// <param name="vert">Vertex buffer to set. Pass this as null to disable vertex buffer</param>
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
		/// <summary>
		/// Sets an index buffer as the current buffer
		/// </summary>
		/// <param name="indices">Index buffer to set. Pass this as null to disable index buffer</param>
		public void SetIndexBuffer(OpenGL.IIndexBuffer indices)
		{
			if (index_buffer != null)
				index_buffer.Reset();
			if (indices != null)
				indices.MakeCurrent();
			index_buffer = indices;
		}

		/// <summary>
		/// Clear buffers
		/// </summary>
		/// <param name="buffers">Buffers to clear</param>
		/// <param name="color">Color value to set</param>
		/// <param name="depth">Depth value to set</param>
		/// <param name="stencil">Stencil value to set</param>
		/// <remarks>Color, depth and stencil will set those default values whether they are set in the buffers parameter or not.</remarks>
		public void Clear(ClearFlags buffers, Vector4 color, double depth, int stencil)
		{
			OpenGL.OpenGL.glClearColor(color.x, color.y, color.z, color.w);
			OpenGL.OpenGL.glClearDepth(depth);
			OpenGL.OpenGL.glClearStencil(stencil);
			OpenGL.OpenGL.glClear((uint)buffers);
		}
		/// <summary>
		/// Clear buffers
		/// </summary>
		/// <param name="buffers">Buffers to clear</param>
		/// <param name="color">Color value to set</param>
		/// <param name="depth">Depth value to set</param>
		/// <remarks>Color and depth will set those default values whether they are set in the buffers parameter or not.</remarks>

		public void Clear(ClearFlags buffers, Vector4 color, double depth)
		{
			OpenGL.OpenGL.glClearColor(color.x, color.y, color.z, color.w);
			OpenGL.OpenGL.glClearDepth(depth);
			OpenGL.OpenGL.glClear((uint)buffers);
		}
		/// <summary>
		/// Clear buffers
		/// </summary>
		/// <param name="buffers">Buffers to clear</param>
		/// <param name="color">Color value to set</param>
		/// <remarks>Color will set those default values whether they are set in the buffers parameter or not.</remarks>
		public void Clear(ClearFlags buffers, Vector4 color)
		{
			OpenGL.OpenGL.glClearColor(color.x, color.y, color.z, color.w);
			OpenGL.OpenGL.glClear((uint)buffers);
		}
		/// <summary>
		/// Clear buffers
		/// </summary>
		/// <param name="buffers">Buffers to clear</param>
		/// <remarks>Values for color, depth and stencil will be the previously set values if they are mentioned in the buffers paramter</remarks>
		public void Clear(ClearFlags buffers)
		{
			OpenGL.OpenGL.glClear((uint)buffers);
		}


		private System.Drawing.Rectangle vp;

		public System.Drawing.Rectangle Viewport
		{
			get
			{
				//int[] values = new int[4];
				//OpenGL.OpenGL.glGetIntegerv(OpenGL.OpenGL.Const.GL_VIEWPORT, values);
				//return new System.Drawing.Rectangle(values[0], values[1], values[2], values[3]);
				return vp;
				
			}
			set
			{
				vp = value;
				OpenGL.OpenGL.glViewport(value.X, value.Y, value.Width, value.Height);
			}
		}
		/// <summary>
		/// Draw buffers
		/// </summary>
		/// <param name="mode">Element type to draw</param>
		/// <remarks>This function uses the currently set vertex and index buffer (if any) If no index buffer is set, the function will assume that elements follows each other in the vertex buffer.</remarks>
		public void Draw(DrawMode mode)
		{
			if (vertex_buffer == null)
				throw new InvalidOperationException("No vertex buffer has been set.");
			if (index_buffer != null)
				OpenGL.OpenGL.glDrawElements((uint)mode, index_buffer.Count, index_buffer.Type, IntPtr.Zero);
			else
				OpenGL.OpenGL.glDrawArrays((uint)mode, 0, vertex_buffer.Count);
		}
		/// <summary>
		/// Gets or sets the projection matrix used by OpenGL
		/// </summary>
		public Matrix ProjectionMatrix
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
		/// <summary>
		/// Gets or sets the modelview matrix used by OpenGL
		/// </summary>
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
		/// <summary>
		/// Gets or sets the texture matrix used by OpenGL
		/// </summary>
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
		/// <summary>
		/// Presents the rendered image
		/// </summary>
		public void Present()
		{
			context.Swap();
		}

		private void Cleanup()
		{
			context.Dispose();
		}
		/// <summary>
		/// Disposes the rendering device and frees all unmanaged memory
		/// </summary>
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
		/// <summary>
		/// Gets or sets if OpenGL depth test is enabled
		/// </summary>
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
		/// <summary>
		/// Enables or disables multisampling if the rendering context supports it
		/// </summary>
		public bool MultiSample
		{
			get
			{
				return OpenGL.OpenGL.glIsEnabled(OpenGL.OpenGL.Const.GL_MULTISAMPLE) == OpenGL.OpenGL.boolean.TRUE;
			}
			set
			{
				if (value)
					OpenGL.OpenGL.glEnable(OpenGL.OpenGL.Const.GL_MULTISAMPLE);
				else
					OpenGL.OpenGL.glDisable(OpenGL.OpenGL.Const.GL_MULTISAMPLE);
			}
		}
		/// <summary>
		/// Gets or sets if lighting is enabled
		/// </summary>
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
		/// <summary>
		/// Gets or sets if face culling is enabled
		/// </summary>
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
		/// <summary>
		/// Gets or sets which side(s) to cull
		/// </summary>
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
		/// <summary>
		/// Gets or sets automatic normalization
		/// </summary>
		public bool Normalize
		{
			get
			{
				return OpenGL.OpenGL.glIsEnabled(OpenGL.OpenGL.Const.GL_NORMALIZE) == Glorg2.Graphics.OpenGL.OpenGL.boolean.TRUE;
			}
			set
			{
				if (value)
					OpenGL.OpenGL.glEnable(OpenGL.OpenGL.Const.GL_NORMALIZE);
				else
					OpenGL.OpenGL.glDisable(OpenGL.OpenGL.Const.GL_NORMALIZE);
			}
		}
		/// <summary>
		/// Gets polygon mode for a face
		/// </summary>
		/// <param name="face">Face to get</param>
		/// <returns>Polygonode for the face</returns>
		public PolygonMode GetPolygonMode(CullFace face)
		{
				int[] vals = new int[1];
				OpenGL.OpenGL.glGetIntegerv(OpenGL.OpenGL.Const.GL_POLYGON_MODE, vals);
				return (PolygonMode)vals[0];
		}
		/// <summary>
		/// Sets polugon mode for a face
		/// </summary>
		/// <param name="face"></param>
		/// <param name="mode"></param>
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
