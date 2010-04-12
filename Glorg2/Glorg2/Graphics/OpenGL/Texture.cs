using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL
{
	public abstract class Texture : Resource.Resource, IDisposable
	{
		protected uint handle;
		internal uint target;
		public uint Handle { get { return handle; } }

		protected override void StreamRead(System.IO.Stream input)
		{
			base.StreamRead(input);
		}

		protected abstract void Cleanup();

		public void Dispose()
		{
			Cleanup();
			GC.SuppressFinalize(this);
		}
		~Texture()
		{
			Cleanup();
		}
	}
	public sealed class Texture1D : Texture
	{
		private int width;

		public int Width { get { return width; } }

		internal Texture1D()
			: base()
		{
			width = 0;
			uint[] names = new uint[1];
			OpenGL.glGenTextures(1, names);
			handle = names[0];
			target = (uint)OpenGL.Const.GL_TEXTURE_1D;
		}

		protected override void Cleanup()
		{
			OpenGL.glDeleteTextures(1, new uint[] { handle });
		}
	}
	public sealed class Texture2D : Texture
	{
		private int width, height;
		
		public int Width { get { return width; } }
		public int Height { get { return height; } }

		internal Texture2D()
			: base()
		{
			uint[] names = new uint[1];
			OpenGL.glGenTextures(1, names);
			handle = names[0];
			target = (uint)OpenGL.Const.GL_TEXTURE_2D;
		}

		public Texture2D(System.IO.Stream src, string sourcename)
			: this()
		{
			SourceName = sourcename;
			var bmp = System.Drawing.Bitmap.FromStream(src) as System.Drawing.Bitmap;
			width = bmp.Width;
			height = bmp.Height;
			var lc = bmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
			OpenGL.glBindTexture(target, handle);
			OpenGL.glTexImage2D(target, 0, (int)OpenGL.Const.GL_RGBA8, bmp.Width, bmp.Height, 0, (uint)OpenGL.Const.GL_RGBA, (uint)OpenGL.Const.GL_UNSIGNED_BYTE, lc.Scan0);
			
			bmp.UnlockBits(lc);
			bmp.Dispose();
		}

		protected override void Cleanup()
		{
			OpenGL.glDeleteTextures(1, new uint[] { handle });
		}
	}
	public sealed class Texture3D : Texture
	{
		private int width, height, depth;

		public int Width { get { return width; } }
		public int Height { get { return height; } }
		public int Depth { get { return depth; } }

		internal Texture3D()
			: base()
		{
			width = 0;
			height = 0;
			depth = 0;
			uint[] names = new uint[1];
			OpenGL.glGenTextures(1, names);
			handle = names[0];
			target = (uint)OpenGL.Const.GL_TEXTURE_3D;
		}
		
		protected override void Cleanup()
		{
			OpenGL.glDeleteTextures(1, new uint[] { handle });
		}

	}

	public sealed class CubeTexture : Texture
	{
		private System.Collections.ObjectModel.ReadOnlyCollection<Texture2D> sides;

		public System.Collections.ObjectModel.ReadOnlyCollection<Texture2D> Sides { get { return sides; } }

		internal CubeTexture()
			: base()
		{
			sides = new List<Texture2D>().AsReadOnly();
		}

		protected override void Cleanup()
		{
			
		}
	}
}
