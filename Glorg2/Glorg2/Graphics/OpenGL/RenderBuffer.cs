using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL
{
	public abstract class RenderBuffer : IDeviceObject
	{
		protected uint handle;
		FrameBuffer framebuffer;
		internal RenderBuffer(FrameBuffer fb)
		{
			if (fb == null)
				throw new NullReferenceException();
			uint[] buffers = new uint[1];
			OpenGL.glGenRenderbuffers(1, buffers);
			handle = buffers[0];
			framebuffer = fb;
			fb.render_buffers.Add(this);

		}
		public void Dispose()
		{
			OpenGL.glDeleteRenderbuffers(1, new uint[handle]);
		}
		public void MakeCurrent()
		{
			OpenGL.glBindRenderbuffer(OpenGL.Const.GL_RENDERBUFFER, handle);
		}
		public void MakeNonCurrent()
		{
			OpenGL.glBindRenderbuffer(OpenGL.Const.GL_RENDERBUFFER, 0);
		}
	}

	public enum DepthType : uint
	{
		Depth16 = OpenGL.Const.GL_DEPTH_COMPONENT16,
		Depth24 = OpenGL.Const.GL_DEPTH_COMPONENT24,
		Depth32 = OpenGL.Const.GL_DEPTH_COMPONENT32
	}

	public sealed class DepthBuffer : RenderBuffer
	{
		public DepthBuffer(FrameBuffer buffer, DepthType type)
			: base(buffer)
		{
			MakeCurrent();
			OpenGL.glRenderbufferStorage(OpenGL.Const.GL_RENDERBUFFER, (uint)type, buffer.Width, buffer.Height);
			OpenGL.glFramebufferRenderbuffer(OpenGL.Const.GL_FRAMEBUFFER, OpenGL.Const.GL_DEPTH_ATTACHMENT, OpenGL.Const.GL_RENDERBUFFER, handle);
		}
		public DepthBuffer(FrameBuffer buffer, DepthType type, int samples)
			: base(buffer)
		{
			MakeCurrent();
			OpenGL.glRenderbufferStorageMultisample(OpenGL.Const.GL_RENDERBUFFER, samples, (uint)type, buffer.Width, buffer.Height);
			OpenGL.glFramebufferRenderbuffer(OpenGL.Const.GL_FRAMEBUFFER, OpenGL.Const.GL_DEPTH_ATTACHMENT, OpenGL.Const.GL_RENDERBUFFER, handle);
		}
	}
	public sealed class StencilBuffer : RenderBuffer
	{
		public StencilBuffer(FrameBuffer buffer)
			: base(buffer)
		{
			MakeCurrent();
			OpenGL.glRenderbufferStorage(OpenGL.Const.GL_RENDERBUFFER, OpenGL.Const.GL_STENCIL_INDEX8, buffer.Width, buffer.Height);
			OpenGL.glFramebufferRenderbuffer(OpenGL.Const.GL_RENDERBUFFER, OpenGL.Const.GL_STENCIL_ATTACHMENT, OpenGL.Const.GL_RENDERBUFFER, handle);
		}

		public StencilBuffer(FrameBuffer buffer, int samples)
			: base(buffer)
		{
			MakeCurrent();
			OpenGL.glRenderbufferStorageMultisample(OpenGL.Const.GL_RENDERBUFFER, samples, OpenGL.Const.GL_STENCIL_INDEX8, buffer.Width, buffer.Height);
			OpenGL.glFramebufferRenderbuffer(OpenGL.Const.GL_RENDERBUFFER, OpenGL.Const.GL_STENCIL_ATTACHMENT, OpenGL.Const.GL_RENDERBUFFER, handle);
		}

	}
	public sealed class ColorBuffer : RenderBuffer
	{
		public ColorBuffer(FrameBuffer fb, InternalFormat fmt)
			: base(fb)
		{
			//fb.MakeCurrent();
			MakeCurrent();
			OpenGL.glRenderbufferStorage(OpenGL.Const.GL_RENDERBUFFER, (uint)fmt, fb.Width, fb.Height);
			OpenGL.glFramebufferRenderbuffer(OpenGL.Const.GL_FRAMEBUFFER, OpenGL.Const.GL_COLOR_ATTACHMENT0, OpenGL.Const.GL_RENDERBUFFER, handle);
		}
		public ColorBuffer(FrameBuffer fb, InternalFormat fmt, int samples)
		:	base(fb)
		{
			MakeCurrent();
			OpenGL.glRenderbufferStorageMultisample(OpenGL.Const.GL_RENDERBUFFER, samples, (uint)fmt, fb.Width, fb.Height);
			OpenGL.glFramebufferRenderbuffer(OpenGL.Const.GL_FRAMEBUFFER, OpenGL.Const.GL_COLOR_ATTACHMENT0, OpenGL.Const.GL_RENDERBUFFER, handle);
		}
	}
	
}
