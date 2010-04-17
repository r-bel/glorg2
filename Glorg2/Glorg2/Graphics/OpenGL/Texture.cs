using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2;

namespace Glorg2.Graphics.OpenGL
{

	public enum PixelType : uint
	{
		Red = OpenGL.Const.GL_RED,
		Green = OpenGL.Const.GL_GREEN,
		Blue = OpenGL.Const.GL_BLUE,
		Alpha = OpenGL.Const.GL_ALPHA,
		Rgb = OpenGL.Const.GL_RGB,
		Bgr = OpenGL.Const.GL_BGR,
		Rgba = OpenGL.Const.GL_RGBA,
		Bgra = OpenGL.Const.GL_BGRA,
		Luminance = OpenGL.Const.GL_LUMINANCE,
		LuminanceAlpha = OpenGL.Const.GL_LUMINANCE_ALPHA
	}

	public enum InternalFormat : uint
	{
		Alpha = OpenGL.Const.GL_ALPHA, 
		Alpha4 = OpenGL.Const.GL_ALPHA4, 
		Alpha8 = OpenGL.Const.GL_ALPHA8, 
		Alpha12 = OpenGL.Const.GL_ALPHA12, 
		Alpha16 = OpenGL.Const.GL_ALPHA16, 
		//CompressedAlpha = OpenGL.Const.GL_COMPRESSED_ALPHA, 
		//CompressedLuminance = OpenGL.Const.GL_COMPRESSED_LUMINANCE, 
		//CompressedLuminanceAlpha = OpenGL.Const.GL_COMPRESSED_LUMINANCE_ALPHA, 
		//CompressedIntensity = OpenGL.Const.GL_COMPRESSED_INTENSITY, 
		CompressedRgb = OpenGL.Const.GL_COMPRESSED_RGB, 
		CompressedRgba = OpenGL.Const.GL_COMPRESSED_RGBA, 
		DepthComponent = OpenGL.Const.GL_DEPTH_COMPONENT, 
		DepthComponent16 = OpenGL.Const.GL_DEPTH_COMPONENT16, 
		DepthComponent24 = OpenGL.Const.GL_DEPTH_COMPONENT24, 
		DepthComponent32 = OpenGL.Const.GL_DEPTH_COMPONENT32, 
		Luminance = OpenGL.Const.GL_LUMINANCE, 
		Luminance4 = OpenGL.Const.GL_LUMINANCE4, 
		Luminance8 = OpenGL.Const.GL_LUMINANCE8, 
		Luminance12 = OpenGL.Const.GL_LUMINANCE12, 
		Luminance16 = OpenGL.Const.GL_LUMINANCE16, 
		LuminanceAlpha = OpenGL.Const.GL_LUMINANCE_ALPHA, 
		Luminance4Alpha4 = OpenGL.Const.GL_LUMINANCE4_ALPHA4, 
		Luminance6Alpha2 = OpenGL.Const.GL_LUMINANCE6_ALPHA2, 
		Luminance8Alpha8 = OpenGL.Const.GL_LUMINANCE8_ALPHA8, 
		Luminance12Alpga4 = OpenGL.Const.GL_LUMINANCE12_ALPHA4,
		Luminance12Alpha12 = OpenGL.Const.GL_LUMINANCE12_ALPHA12, 
		Luminance16Alpha16 = OpenGL.Const.GL_LUMINANCE16_ALPHA16, 
		Intensity = OpenGL.Const.GL_INTENSITY, 
		Intensity4 = OpenGL.Const.GL_INTENSITY4, 
		Intensity8 = OpenGL.Const.GL_INTENSITY8, 
		Intensity12 = OpenGL.Const.GL_INTENSITY12, 
		Intensity16 = OpenGL.Const.GL_INTENSITY16, 
		R3G3B2 = OpenGL.Const.GL_R3_G3_B2, 
		Rgb = OpenGL.Const.GL_RGB, 
		Rbb4 = OpenGL.Const.GL_RGB4,
		Rgb5 = OpenGL.Const.GL_RGB5,
		Rgb8 = OpenGL.Const.GL_RGB8, 
		Rgb10 = OpenGL.Const.GL_RGB10, 
		Rgb12 = OpenGL.Const.GL_RGB12, 
		Rgb16 = OpenGL.Const.GL_RGB16, 
		Rgba = OpenGL.Const.GL_RGBA, 
		Rgba2 = OpenGL.Const.GL_RGBA2, 
		Rgba4 = OpenGL.Const.GL_RGBA4, 
		Rgb5A1 = OpenGL.Const.GL_RGB5_A1, 
		Rgba8 = OpenGL.Const.GL_RGBA8, 
		Rgb10A2 = OpenGL.Const.GL_RGB10_A2, 
		Rgba12 = OpenGL.Const.GL_RGBA12, 
		Rgba16 = OpenGL.Const.GL_RGBA16, 
		//SLuminance = OpenGL.Const.GL_SLUMINANCE, 
		//SLuminance8 = OpenGL.Const.GL_SLUMINANCE8,
		//SLuminanceAlpha = OpenGL.Const.GL_SLUMINANCE_ALPHA, 
		//SLuminance8Alpha8 = OpenGL.Const.GL_SLUMINANCE8_ALPHA8, 
		SRgb = OpenGL.Const.GL_SRGB, 
		SRgb8 = OpenGL.Const.GL_SRGB8, 
		SRgbAlpha = OpenGL.Const.GL_SRGB_ALPHA, 
		SRgb8Alpha8 = OpenGL.Const.GL_SRGB8_ALPHA8
	}

	public enum PixelDataType : uint
	{
		UnsignedByte = OpenGL.Const.GL_UNSIGNED_BYTE,
		SignedByte = OpenGL.Const.GL_BYTE, 
		Bitmap = OpenGL.Const.GL_BITMAP, 
		UnsignedShort = OpenGL.Const.GL_UNSIGNED_SHORT, 
		SignedShort = OpenGL.Const.GL_SHORT, 
		UnsignedInt = OpenGL.Const.GL_UNSIGNED_INT, 
		SignedInt = OpenGL.Const.GL_INT, 
		Float = OpenGL.Const.GL_FLOAT, 
		UnsignedByte_3_3_2 = OpenGL.Const.GL_UNSIGNED_BYTE_3_3_2, 
		UnsignedByte_2_3_3_Rev = OpenGL.Const.GL_UNSIGNED_BYTE_2_3_3_REV, 
		UnsignedShort_5_6_5 = OpenGL.Const.GL_UNSIGNED_SHORT_5_6_5, 
		UnsignedShort_5_6_5_Rev = OpenGL.Const.GL_UNSIGNED_SHORT_5_6_5_REV, 
		UnsignedShort_4 = OpenGL.Const.GL_UNSIGNED_SHORT_4_4_4_4, 
		UnsignedShort_4_Rev = OpenGL.Const.GL_UNSIGNED_SHORT_4_4_4_4_REV,
		UnsignedShort_5_5_5_1 = OpenGL.Const.GL_UNSIGNED_SHORT_5_5_5_1, 
		UnsignedShort_1_5_5_5_Rev = OpenGL.Const.GL_UNSIGNED_SHORT_1_5_5_5_REV, 
		UnsignedInt8 = OpenGL.Const.GL_UNSIGNED_INT_8_8_8_8, 
		UnsignedInt8Rev = OpenGL.Const.GL_UNSIGNED_INT_8_8_8_8_REV, 
		UnsignedInt_10_10_10_2 = OpenGL.Const.GL_UNSIGNED_INT_10_10_10_2, 
		UnsignedInt_2_10_10_10_Rev = OpenGL.Const.GL_UNSIGNED_INT_2_10_10_10_REV
	}

	public abstract class Texture : Glorg2.Resource.Resource
	{
		protected uint handle;
		internal uint target;
		public uint Handle { get { return handle; } }

		protected override void StreamRead(System.IO.Stream input)
		{
			base.StreamRead(input);
		}

		protected abstract void Cleanup();

		public abstract void MakeCurrent();

		/// <summary>
		/// Disables this texture unit
		/// </summary>
		public abstract void Disable();
		public override void DoDispose()
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
		public override void MakeCurrent()
		{
			OpenGL.glEnable(OpenGL.Const.GL_TEXTURE_1D);
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_1D, handle);
		}
		public override void Disable()
		{
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_1D, 0);
			OpenGL.glDisable(OpenGL.Const.GL_TEXTURE_1D);
		}

		protected override void Cleanup()
		{
			OpenGL.glDeleteTextures(1, new uint[] { handle });
		}
	}
	public sealed class Texture2D : Texture
	{
		private int width, height;
		internal int int_fmt;
		internal uint fmt;
		internal uint type;
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

		public Texture2D(int width, int height, InternalFormat pixel_format, PixelDataType format, PixelType type, string sourcename)
			: base()
		{
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_2D, handle);
			int_fmt = (int)pixel_format;
			fmt = (uint)format;
			this.type = (uint)type;
		}

		internal void AssignTexture()
		{
			OpenGL.glTexImage2D(OpenGL.Const.GL_TEXTURE_2D, 0, (int)int_fmt, width, height, 0, (uint)fmt, (uint)type, IntPtr.Zero);
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
			int_fmt = (int)OpenGL.Const.GL_RGBA8;
			fmt = OpenGL.Const.GL_RGBA;
			type = OpenGL.Const.GL_UNSIGNED_BYTE;
			
			AssignTexture();
			//OpenGL.glTexImage2D(target, 0, (int)OpenGL.Const.GL_RGBA8, bmp.Width, bmp.Height, 0, (uint)OpenGL.Const.GL_RGBA, (uint)OpenGL.Const.GL_UNSIGNED_BYTE, lc.Scan0);
			
			bmp.UnlockBits(lc);
			bmp.Dispose();
		}

		public override void MakeCurrent()
		{
			OpenGL.glEnable(OpenGL.Const.GL_TEXTURE_2D);
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_2D, handle);
		}
		public override void Disable()
		{
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_2D, 0);
			OpenGL.glDisable(OpenGL.Const.GL_TEXTURE_2D);
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

		public override void MakeCurrent()
		{
			OpenGL.glEnable(OpenGL.Const.GL_TEXTURE_3D);
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_3D, handle);
		}
		public override void Disable()
		{
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_3D, 0);
			OpenGL.glDisable(OpenGL.Const.GL_TEXTURE_3D);
		}
		protected override void Cleanup()
		{
			OpenGL.glDeleteTextures(1, new uint[] { handle });
		}

	}

	public enum CubemapSide
	{
			PositiveX = (int)OpenGL.Const.GL_TEXTURE_CUBE_MAP_POSITIVE_X_ARB,
			NegativeX = (int)OpenGL.Const.GL_TEXTURE_CUBE_MAP_NEGATIVE_X_ARB,
			PositiveY = (int)OpenGL.Const.GL_TEXTURE_CUBE_MAP_POSITIVE_Y_ARB,
			NegativeY = (int)OpenGL.Const.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y_ARB,
			PositiveZ = (int)OpenGL.Const.GL_TEXTURE_CUBE_MAP_POSITIVE_Z_ARB,
			NegativeZ = (int)OpenGL.Const.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z_ARB		
	}

	public sealed class CubeTexture : Texture
	{
		private List<Texture2D> textures;
		internal CubeTexture()
			: base()
		{
//			sides = new List<Texture2D>().AsReadOnly();
		}
		public Texture2D GetSide(CubemapSide side)
		{
			return textures[(int)side];
		}
		public void SetSide(CubemapSide side, Texture2D value)
		{
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_CUBE_MAP, handle);
			textures[(int)side] = value;
			//OpenGL.glTexImage2D(OpenGL.Const.GL_TEXTURE_CUBE_MAP_POSITIVE_X + (uint)side, 0, 
		}
		public override void MakeCurrent()
		{
			OpenGL.glEnable(OpenGL.Const.GL_TEXTURE_CUBE_MAP);
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_CUBE_MAP, handle);
		}
		public override void Disable()
		{
			OpenGL.glBindTexture(OpenGL.Const.GL_TEXTURE_CUBE_MAP, 0);
			OpenGL.glDisable(OpenGL.Const.GL_TEXTURE_CUBE_MAP);
		}

		protected override void Cleanup()
		{
			
		}
	}
}
