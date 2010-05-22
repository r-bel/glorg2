using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL;
using GL = Glorg2.Graphics.OpenGL.OpenGL;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL.Shaders;
namespace Glorg2.Graphics
{
	public sealed class GraphicsDevice : IDisposable
	{


		private OpenGLContext context;
		
		internal IVertexBuffer vertex_buffer;
		internal IIndexBuffer index_buffer;

		private Matrix modelview;
		private Matrix projection;
		private Matrix texture;


		private bool projection_changed;
		private bool modelview_changed;
		private bool texture_changed;

		private MatrixUniform projection_uniform;
		private MatrixUniform modelview_uniform;
		private MatrixUniform texture_uniform;

		Dictionary<ElementType, string> attributes;

		private Material active_shader;

		/// <summary>
		/// Gets the context for this rendering device
		/// </summary>
		public OpenGLContext Context { get { return context; } }

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
			modelview = Matrix.Identity;
			projection = Matrix.Identity;
			texture = Matrix.Identity;
			context.Samples = 4;
			// Create context using platform specific methods
			context.CreateContext(target, null);
			//GL.InitGeneral(context);
			//foreach (var str in GL.GetSupportedExtensions())
				//Console.WriteLine(str);

			var err = GL.glGetError();
			GL.InitGL_1_2(context);
			GL.InitGL_1_3(context);
			GL.InitGL_1_4(context);
			GL.InitGL_1_5(context);
			GL.InitGL_2_0(context);
			GL.InitGL_2_1(context);
			GL.InitGL_3_0(context);
			GL.InitGL_3_1(context);
			GL.InitGL_3_2(context);

			err = GL.glGetError();

			GL.glEnable(GL.Const.GL_TEXTURE_2D);

			state = new OpenGLState(this);
			state.Culling = true;
			state.DepthTest = true;
			
			
		}

		public void SetActiveMaterial(Graphics.Material material)
		{
			active_shader = material;
			active_shader.MakeCurrent();
			var std = material as IStdShader;

			if (std != null)
			{
				projection_uniform = std.Projection;
				modelview_uniform = std.ModelView;
				texture_uniform = std.Texture;
			}
			active_shader.Shader.SetFragmentOutput("out_frag", 0);
		}
        

		/// <summary>
		/// Applies a texture
		/// </summary>
		/// <param name="texture"></param>
		public void MakeCurrent(OpenGL.Texture texture, uint index)
		{
			GL.glActiveTexture(index);
			GL.glBindTexture((uint)texture.target, texture.Handle);
		}
		/// <summary>
		/// Sets a vertex buffer as the current buffer. 
		/// </summary>
		/// <param name="vert">Vertex buffer to set. Pass this as null to disable vertex buffer</param>
		public void SetVertexBuffer(OpenGL.IVertexBuffer vert)
		{
			if (vertex_buffer != null)
			{
				vertex_buffer.MakeNonCurrent();
			}
			if (vert != null)
			{
				if(active_shader != null && active_shader is StdMaterial)
					vert.ApplyStdMaterial(active_shader as StdMaterial);
				vert.MakeCurrent();
			}
			vertex_buffer = vert;
		}
		/// <summary>
		/// Sets an index buffer as the current buffer
		/// </summary>
		/// <param name="indices">Index buffer to set. Pass this as null to disable index buffer</param>
		public void SetIndexBuffer(OpenGL.IIndexBuffer indices)
		{
			if (index_buffer != null)
				index_buffer.MakeNonCurrent();
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
			GL.glClearColor(color.x, color.y, color.z, color.w);
			GL.glClearDepth(depth);
			GL.glClearStencil(stencil);
			GL.glClear((uint)buffers);
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
			GL.glClearColor(color.x, color.y, color.z, color.w);
			GL.glClearDepth(depth);
			GL.glClear((uint)buffers);
		}
		/// <summary>
		/// Clear buffers
		/// </summary>
		/// <param name="buffers">Buffers to clear</param>
		/// <param name="color">Color value to set</param>
		/// <remarks>Color will set those default values whether they are set in the buffers parameter or not.</remarks>
		public void Clear(ClearFlags buffers, Vector4 color)
		{
			GL.glClearColor(color.x, color.y, color.z, color.w);
			GL.glClear((uint)buffers);
		}
		/// <summary>
		/// Clear buffers
		/// </summary>
		/// <param name="buffers">Buffers to clear</param>
		/// <remarks>Values for color, depth and stencil will be the previously set values if they are mentioned in the buffers paramter</remarks>
		public void Clear(ClearFlags buffers)
		{
			//GL.glClear((uint)buffers);
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
				GL.glViewport(value.X, value.Y, value.Width, value.Height);
			}
		}
		/// <summary>
		/// Draw buffers
		/// </summary>
		/// <param name="mode">Element type to draw</param>
		/// <remarks>This function uses the currently set vertex and index buffer (if any) If no index buffer is set, the function will assume that elements follows each other in the vertex buffer.</remarks>
		public void Draw(DrawMode mode)
		{
			if (modelview_changed && modelview_uniform != null)
			{
				modelview_uniform.Value = modelview;
				modelview_uniform.SetValue();
				modelview_changed = false;
			}
			if (projection_changed && projection_uniform != null)
			{
				projection_uniform.Value = projection;
				projection_uniform.SetValue();
				projection_changed = false;
			}
			if (texture_changed && texture_uniform != null)
			{
				texture_uniform.Value = texture;
				texture_uniform.SetValue();
				texture_changed = false;
			}
			if (active_shader != null)
			{
				foreach (var u in active_shader.uniforms)
					u.SetValue();
			}
			if (vertex_buffer == null)
				throw new InvalidOperationException("No vertex buffer has been set.");
			if (index_buffer != null)
			{
				GL.glDrawElements((uint)mode, index_buffer.Count, index_buffer.Type, IntPtr.Zero);
			}
			else
				GL.glDrawArrays((uint)mode, 0, vertex_buffer.Count);
		}
		/// <summary>
		/// Gets or sets the projection matrix used by OpenGL
		/// </summary>
		public Matrix ProjectionMatrix
		{
			get
			{
				//Matrix ret = new Matrix();
				//GL.glGetFloatv(GL.Const.GL_PROJECTION_MATRIX, ref ret);
				return projection; ;
			}
			set
			{
				//GL.glMatrixMode((uint)GL.Const.GL_PROJECTION);
				//GL.glLoadMatrixf(ref value);
				projection = value;
				projection_changed = true;
			}
		}
		/// <summary>
		/// Gets or sets the modelview matrix used by OpenGL
		/// </summary>
		public unsafe Matrix ModelviewMatrix
		{
			get
			{
				//Matrix ret = new Matrix();
				//GL.glGetFloatv(GL.Const.GL_MODELVIEW_MATRIX, ref ret);
				return modelview;
			}
			set
			{
				//GL.glMatrixMode((uint)GL.Const.GL_MODELVIEW);
				//GL.glLoadMatrixf(ref value);
				modelview = value;
				modelview_changed = true;
			}
		}
		/// <summary>
		/// Gets or sets the texture matrix used by OpenGL
		/// </summary>
		public unsafe Matrix TextureMatrix
		{
			get
			{
				//Matrix ret = new Matrix();
				//GL.glGetFloatv(GL.Const.GL_TEXTURE_MATRIX, ref ret);
				return texture;

			}
			set
			{
				//GL.glMatrixMode((uint)GL.Const.GL_TEXTURE);
				//GL.glLoadMatrixf(ref value);
				texture = value;
				texture_changed = true;
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
		Triangles = GL.Const.GL_TRIANGLES,
		TriangleFan = GL.Const.GL_TRIANGLE_FAN,
		TriangleStrip = GL.Const.GL_TRIANGLE_STRIP,

		// Quads and polygons not supported in OpenGL 3

		//Quads = GL.Const.GL_QUADS,
		//QuadStrip = GL.Const.GL_QUAD_STRIP,
		//Polygon = GL.Const.GL_POLYGON,
		Points = GL.Const.GL_POINTS,
		Lines = GL.Const.GL_LINES,
		LineLoop = GL.Const.GL_LINE_LOOP,
		LineStrip = GL.Const.GL_LINE_STRIP
	}
	public enum ClearFlags : uint
	{
		Color = GL.Const.GL_COLOR_BUFFER_BIT,
		Depth = GL.Const.GL_DEPTH_BUFFER_BIT,
		Stencil = GL.Const.GL_STENCIL_BUFFER_BIT
	}

    public struct ColorMask
    {
        public bool Red;
        public bool Green;
        public bool Blue;
        public bool Alpha;
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
				return GL.glIsEnabled(GL.Const.GL_DEPTH_TEST) == GL.boolean.TRUE;
			}
			set
			{
				if (value)
					GL.glEnable(GL.Const.GL_DEPTH_TEST);
				else
					GL.glDisable(GL.Const.GL_DEPTH_TEST);
			}
		}
		/// <summary>
		/// Enables or disables multisampling if the rendering context supports it
		/// </summary>
		public bool MultiSample
		{
			get
			{
				return GL.glIsEnabled(GL.Const.GL_MULTISAMPLE) == GL.boolean.TRUE;
			}
			set
			{
				if (value)
					GL.glEnable(GL.Const.GL_MULTISAMPLE);
				else
					GL.glDisable(GL.Const.GL_MULTISAMPLE);
			}
		}
		/// <summary>
		/// Gets or sets if face culling is enabled
		/// </summary>
		public bool Culling
		{
			get
			{
				return GL.glIsEnabled(GL.Const.GL_CULL_FACE) == GL.boolean.TRUE;
			}
			set
			{
				if (value)
					GL.glEnable(GL.Const.GL_CULL_FACE);
				else
					GL.glDisable(GL.Const.GL_CULL_FACE);
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
				GL.glGetIntegerv(GL.Const.GL_CULL_FACE_MODE, vals);
				return (CullFace)vals[0];
			}
			set
			{
				GL.glCullFace((uint)value);
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
				//GL.glGetIntegerv(GL.Const.GL_POLYGON_MODE, vals);
				return (PolygonMode)vals[0];
		}
		/// <summary>
		/// Sets polugon mode for a face
		/// </summary>
		/// <param name="face"></param>
		/// <param name="mode"></param>
		public void SetPolygonMode(CullFace face, PolygonMode mode)
		{
			GL.glPolygonMode((uint)face, (uint)mode);
		}

        public bool StencilTest
        {
            get
            {
                return GL.glIsEnabled(GL.Const.GL_STENCIL_TEST) == GL.boolean.TRUE;
            }
            set
            {
                if (value)
                    GL.glEnable(GL.Const.GL_STENCIL_TEST);
                else
                    GL.glDisable(GL.Const.GL_STENCIL_TEST);
            }
        }

        /*public bool AlphaTest
        {
            get
            {
                return GL.glIsEnabled(GL.Const.GL_ALPHA_TEST) == GL.boolean.TRUE;
            }
            set
            {
                if (value)
                    GL.glEnable(GL.Const.GL_ALPHA_TEST);
                else
                    GL.glDisable(GL.Const.GL_ALPHA_TEST);
            }
        }*/

        public bool Blend
        {
            get
            {
                return GL.glIsEnabled(GL.Const.GL_BLEND) == GL.boolean.TRUE;
            }
            set
            {
                if (value)
                    GL.glEnable(GL.Const.GL_BLEND);
                else
                    GL.glDisable(GL.Const.GL_BLEND);
            }
        }

        public bool ScissorTest
        {
            get
            {
                return GL.glIsEnabled(GL.Const.GL_SCISSOR_TEST) == GL.boolean.TRUE;
            }
            set
            {
                if (value)
                    GL.glEnable(GL.Const.GL_SCISSOR_TEST);
                else
                    GL.glDisable(GL.Const.GL_SCISSOR_TEST);
            }
        }

        public Test DepthFunction
        {
            get
            {
				int[] ret = new int[1];
                GL.glGetIntegerv(GL.Const.GL_DEPTH_FUNC, ret);
                return (Test)ret[0];
            }
            set
            {
                GL.glDepthFunc((uint)value);
            }
        }

        /*public Test AlphaTestFunction
        {
            get
            {
                int ret = 0;
                GL.glGetIntegerv(GL.Const.GL_ALPHA_TEST_FUNC, ref ret);
                return (Test)ret;
            }
        }
        public float AlphaTestReference
        {
            get
            {
                float r = 0;
                GL.glGetFloatv(GL.Const.GL_ALPHA_TEST_REF, ref r);
                return r;
            }
        }*/
 
        public ColorMask ColorMask
        {
            get
            {
                GL.boolean[] arr = new GL.boolean[4];
                GL.glGetBooleanv(GL.Const.GL_COLOR_WRITEMASK, arr);
                return new ColorMask()
                {
                    Red = arr[0] == GL.boolean.TRUE,
                    Green = arr[1] == GL.boolean.TRUE,
                    Blue = arr[2] == GL.boolean.TRUE,
                    Alpha = arr[3] == GL.boolean.TRUE
                };
            }
            set
            {
                GL.glColorMask(value.Red ? GL.boolean.TRUE : GL.boolean.FALSE,
                    value.Green ? GL.boolean.TRUE : GL.boolean.FALSE,
                    value.Blue ? GL.boolean.TRUE : GL.boolean.FALSE,
                    value.Alpha ? GL.boolean.TRUE : GL.boolean.FALSE);
            }
        }

        public bool DepthMask
        {
            get
            {
                GL.boolean[] state = new GL.boolean[1];
                GL.glGetBooleanv(GL.Const.GL_DEPTH_WRITEMASK, state);
                return state[0] == GL.boolean.TRUE;
            }
            set
            {
                GL.glDepthMask(value ? GL.boolean.TRUE : GL.boolean.FALSE);
            }
        }

        public uint StencilMask
        {
            get
            {
                int[] arr = new int[1];
                GL.glGetIntegerv(GL.Const.GL_STENCIL_WRITEMASK, arr);
                return (uint)arr[0];
            }
            set
            {
                GL.glStencilMask(value);
            }
        }
		
	}
	public enum CullFace : uint
	{
		Front = GL.Const.GL_FRONT,
		Back = GL.Const.GL_BACK,
		FrontAndBack = GL.Const.GL_FRONT_AND_BACK
	}
	public enum PolygonMode : uint
	{
		Line = GL.Const.GL_LINE,
		Point = GL.Const.GL_POINT,
		Fill = GL.Const.GL_FILL
	}
}
