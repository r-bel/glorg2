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
using System.Runtime.InteropServices;

/*
 public delegate [a-zA-Z0-9]+ {[a-zA-Z0-9]+}\(.*\);		public static \1 wgl\1;
 extern:b+{[A-Z]+}:b+WINAPI:b+{[a-zA-Z0-9]*}:b*{.*};	public delegate \1 \2\3;
 */
namespace Glorg2.Graphics.OpenGL
{
	public sealed class WglContext : OpenGLContext
	{
		#region wgl Extensions

		public const int WGL_CONTEXT_DEBUG_BIT_ARB = 0x00000001;
		public const int WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 0x00000002;
		public const int WGL_CONTEXT_MAJOR_VERSION_ARB = 0x2091;
		public const int WGL_CONTEXT_MINOR_VERSION_ARB = 0x2092;
		public const int WGL_CONTEXT_LAYER_PLANE_ARB = 0x2093;
		public const int WGL_CONTEXT_FLAGS_ARB = 0x2094;
		public const int WGL_CONTEXT_PROFILE_MASK_ARB = 0x9126;
		public const int WGL_CONTEXT_CORE_PROFILE_BIT_ARB = 0x00000001;
		public const int WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB = 0x00000002;
		public const int ERROR_INVALID_VERSION_ARB = 0x2095;

		public const int WGL_NUMBER_PIXEL_FORMATS_ARB = 0x2000;
		public const int WGL_DRAW_TO_WINDOW_ARB = 0x2001;
		public const int WGL_DRAW_TO_BITMAP_ARB = 0x2002;
		public const int WGL_ACCELERATION_ARB = 0x2003;
		public const int WGL_NEED_PALETTE_ARB = 0x2004;
		public const int WGL_NEED_SYSTEM_PALETTE_ARB = 0x2005;
		public const int WGL_SWAP_LAYER_BUFFERS_ARB = 0x2006;
		public const int WGL_SWAP_METHOD_ARB = 0x2007;
		public const int WGL_NUMBER_OVERLAYS_ARB = 0x2008;
		public const int WGL_NUMBER_UNDERLAYS_ARB = 0x2009;
		public const int WGL_TRANSPARENT_ARB = 0x200A;
		public const int WGL_TRANSPARENT_RED_VALUE_ARB = 0x2037;
		public const int WGL_TRANSPARENT_GREEN_VALUE_ARB = 0x2038;
		public const int WGL_TRANSPARENT_BLUE_VALUE_ARB = 0x2039;
		public const int WGL_TRANSPARENT_ALPHA_VALUE_ARB = 0x203A;
		public const int WGL_TRANSPARENT_INDEX_VALUE_ARB = 0x203B;
		public const int WGL_SHARE_DEPTH_ARB = 0x200C;
		public const int WGL_SHARE_STENCIL_ARB = 0x200D;
		public const int WGL_SHARE_ACCUM_ARB = 0x200E;
		public const int WGL_SUPPORT_GDI_ARB = 0x200F;
		public const int WGL_SUPPORT_OPENGL_ARB = 0x2010;
		public const int WGL_DOUBLE_BUFFER_ARB = 0x2011;
		public const int WGL_STEREO_ARB = 0x2012;
		public const int WGL_PIXEL_TYPE_ARB = 0x2013;
		public const int WGL_COLOR_BITS_ARB = 0x2014;
		public const int WGL_RED_BITS_ARB = 0x2015;
		public const int WGL_RED_SHIFT_ARB = 0x2016;
		public const int WGL_GREEN_BITS_ARB = 0x2017;
		public const int WGL_GREEN_SHIFT_ARB = 0x2018;
		public const int WGL_BLUE_BITS_ARB = 0x2019;
		public const int WGL_BLUE_SHIFT_ARB = 0x201A;
		public const int WGL_ALPHA_BITS_ARB = 0x201B;
		public const int WGL_ALPHA_SHIFT_ARB = 0x201C;
		public const int WGL_ACCUM_BITS_ARB = 0x201D;
		public const int WGL_ACCUM_RED_BITS_ARB = 0x201E;
		public const int WGL_ACCUM_GREEN_BITS_ARB = 0x201F;
		public const int WGL_ACCUM_BLUE_BITS_ARB = 0x2020;
		public const int WGL_ACCUM_ALPHA_BITS_ARB = 0x2021;
		public const int WGL_DEPTH_BITS_ARB = 0x2022;
		public const int WGL_STENCIL_BITS_ARB = 0x2023;
		public const int WGL_AUX_BUFFERS_ARB = 0x2024;
		public const int WGL_NO_ACCELERATION_ARB = 0x2025;
		public const int WGL_GENERIC_ACCELERATION_ARB = 0x2026;
		public const int WGL_FULL_ACCELERATION_ARB = 0x2027;
		public const int WGL_SWAP_EXCHANGE_ARB = 0x2028;
		public const int WGL_SWAP_COPY_ARB = 0x2029;
		public const int WGL_SWAP_UNDEFINED_ARB = 0x202A;
		public const int WGL_TYPE_RGBA_ARB = 0x202B;
		public const int WGL_TYPE_COLORINDEX_ARB = 0x202C;

		public const int WGL_NUMBER_PIXEL_FORMATS_EXT = 0x2000;
		public const int WGL_DRAW_TO_WINDOW_EXT = 0x2001;
		public const int WGL_DRAW_TO_BITMAP_EXT = 0x2002;
		public const int WGL_ACCELERATION_EXT = 0x2003;
		public const int WGL_NEED_PALETTE_EXT = 0x2004;
		public const int WGL_NEED_SYSTEM_PALETTE_EXT = 0x2005;
		public const int WGL_SWAP_LAYER_BUFFERS_EXT = 0x2006;
		public const int WGL_SWAP_METHOD_EXT = 0x2007;
		public const int WGL_NUMBER_OVERLAYS_EXT = 0x2008;
		public const int WGL_NUMBER_UNDERLAYS_EXT = 0x2009;
		public const int WGL_TRANSPARENT_EXT = 0x200A;
		public const int WGL_TRANSPARENT_VALUE_EXT = 0x200B;
		public const int WGL_SHARE_DEPTH_EXT = 0x200C;
		public const int WGL_SHARE_STENCIL_EXT = 0x200D;
		public const int WGL_SHARE_ACCUM_EXT = 0x200E;
		public const int WGL_SUPPORT_GDI_EXT = 0x200F;
		public const int WGL_SUPPORT_OPENGL_EXT = 0x2010;
		public const int WGL_DOUBLE_BUFFER_EXT = 0x2011;
		public const int WGL_STEREO_EXT = 0x2012;
		public const int WGL_PIXEL_TYPE_EXT = 0x2013;
		public const int WGL_COLOR_BITS_EXT = 0x2014;
		public const int WGL_RED_BITS_EXT = 0x2015;
		public const int WGL_RED_SHIFT_EXT = 0x2016;
		public const int WGL_GREEN_BITS_EXT = 0x2017;
		public const int WGL_GREEN_SHIFT_EXT = 0x2018;
		public const int WGL_BLUE_BITS_EXT = 0x2019;
		public const int WGL_BLUE_SHIFT_EXT = 0x201A;
		public const int WGL_ALPHA_BITS_EXT = 0x201B;
		public const int WGL_ALPHA_SHIFT_EXT = 0x201C;
		public const int WGL_ACCUM_BITS_EXT = 0x201D;
		public const int WGL_ACCUM_RED_BITS_EXT = 0x201E;
		public const int WGL_ACCUM_GREEN_BITS_EXT = 0x201F;
		public const int WGL_ACCUM_BLUE_BITS_EXT = 0x2020;
		public const int WGL_ACCUM_ALPHA_BITS_EXT = 0x2021;
		public const int WGL_DEPTH_BITS_EXT = 0x2022;
		public const int WGL_STENCIL_BITS_EXT = 0x2023;
		public const int WGL_AUX_BUFFERS_EXT = 0x2024;
		public const int WGL_NO_ACCELERATION_EXT = 0x2025;
		public const int WGL_GENERIC_ACCELERATION_EXT = 0x2026;
		public const int WGL_FULL_ACCELERATION_EXT = 0x2027;
		public const int WGL_SWAP_EXCHANGE_EXT = 0x2028;
		public const int WGL_SWAP_COPY_EXT = 0x2029;
		public const int WGL_SWAP_UNDEFINED_EXT = 0x202A;
		public const int WGL_TYPE_RGBA_EXT = 0x202B;
		public const int WGL_TYPE_COLORINDEX_EXT = 0x202C;
		public const int WGL_SAMPLE_BUFFERS_ARB = 0x2041;
		public const int WGL_SAMPLES_ARB = 0x2042;

		public delegate IntPtr CreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);

		public CreateContextAttribsARB wglCreateContextAttribsARB;

		public delegate bool GetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, int[] piValues);
		public delegate bool GetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, float[] pfValues);
		public delegate bool ChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, uint nMaxFormats, ref int piFormats, ref uint nNumFormats);

		public delegate IntPtr GetExtensionsStringARB(IntPtr hdc);

		public static GetExtensionsStringARB wglGetExtensionStringARB;
		public static GetPixelFormatAttribivARB wglGetPixelFormatAttribivARB;
		public static GetPixelFormatAttribfvARB wglGetPixelFormatAttribfvARB;
		public static ChoosePixelFormatARB wglChoosePixelFormatARB;

		[DllImport(GdiDllName)]
		public static extern bool wglMakeContextCurrentARB (IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);
		//public static MakeContextCurrentARB wglMakeContextCurrentARB;
		#endregion

		#region Windows Platform Specific

		public struct PIXELFORMATDESCRIPTOR
		{ // pfd   
			public short nSize;
			public short nVersion;
			public PixelFormatFlags dwFlags;
			public byte iPixelType;
			public byte cColorBits;
			public byte cRedBits;
			public byte cRedShift;
			public byte cGreenBits;
			public byte cGreenShift;
			public byte cBlueBits;
			public byte cBlueShift;
			public byte cAlphaBits;
			public byte cAlphaShift;
			public byte cAccumBits;
			public byte cAccumRedBits;
			public byte cAccumGreenBits;
			public byte cAccumBlueBits;
			public byte cAccumAlphaBits;
			public byte cDepthBits;
			public byte cStencilBits;
			public byte cAuxBuffers;
			public byte iLayerType;
			public byte bReserved;
			public int dwLayerMask;
			public int dwVisibleMask;
			public int dwDamageMask;
			public int transparency;
		}

		public enum PixelFormatFlags
		{
			PFD_DOUBLEBUFFER = 0x00000001,
			PFD_STEREO = 0x00000002,
			PFD_DRAW_TO_WINDOW = 0x00000004,
			PFD_DRAW_TO_BITMAP = 0x00000008,
			PFD_SUPPORT_GDI = 0x00000010,
			PFD_SUPPORT_OPENGL = 0x00000020,
			PFD_GENERIC_FORMAT = 0x00000040,
			PFD_NEED_PALETTE = 0x00000080,
			PFD_NEED_SYSTEM_PALETTE = 0x00000100,
			PFD_SWAP_EXCHANGE = 0x00000200,
			PFD_SWAP_COPY = 0x00000400,
			PFD_SWAP_LAYER_BUFFERS = 0x00000800,
			PFD_GENERIC_ACCELERATED = 0x00001000,
			PFD_SUPPORT_DIRECTDRAW = 0x00002000,
			PFD_DIRECT3D_ACCELERATED = 0x00004000,
			PFD_SUPPORT_COMPOSITION = 0x00008000
		}

		public const string GdiDllName = "gdi32";
		[DllImport(GdiDllName)]
		public static extern int GetPixelFormat(IntPtr hdc);
		[DllImport(GdiDllName)]
		public static extern int ChoosePixelFormat(IntPtr hdc, ref PIXELFORMATDESCRIPTOR ppfd);
		[DllImport(GdiDllName)]
		public static extern bool SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PIXELFORMATDESCRIPTOR ppfd);
		[DllImport("User32")]
		public static extern IntPtr GetDC(IntPtr hWnd);
		[DllImport("User32")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


		#endregion
		#region WGL Platform specific
		public const string DllName = "OpenGL32.dll";

		[DllImport(DllName)]
		public static extern bool wglCopyContext(IntPtr source, IntPtr dest, uint mask);
		[DllImport(DllName)]
		public static extern IntPtr wglCreateContext(IntPtr hdc);
		[DllImport(DllName)]
		public static extern IntPtr wglCreateLayerContext(IntPtr hdc, int iLayerParam);
		[DllImport(DllName)]
		public static extern bool wglDeleteContext(IntPtr glctx);
		[DllImport(DllName)]
		public static extern IntPtr wglGetCurrentContext();
		[DllImport(DllName)]
		public static extern IntPtr wglGetCurrentDC();
		[DllImport(DllName)]
		public static extern IntPtr wglGetProcAddress(string proc);
		[DllImport(DllName)]
		public static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);
		[DllImport(DllName)]
		public static extern bool wglShareLists(IntPtr gla, IntPtr glb);
		[DllImport(DllName)]
		public static extern bool wglUseFontBitmaps(IntPtr hdc, int first, int count, int listbase);
		[DllImport(GdiDllName)]
		public static extern bool SwapBuffers(IntPtr hdc);

		public struct POINTFLOAT
		{
			public float x;
			public float y;
		}

		public struct GLYPHMETRICSFLOAT
		{
			public float gmfBlackBoxX;
			public float gmfBlackBoxY;
			public POINTFLOAT gmfptGlyphOrigin;
			public float gmfCellIncX;
			public float gmfCellIncY;
		}

		public const int WGL_FONT_LINES = 0;
		public const int WGL_FONT_POLYGONS = 1;
		[DllImport(DllName)]
		public static extern bool wglUseFontOutlines(IntPtr hDC, int first, int count, int listbase, float deviation,
							   float extrusion, int format, ref GLYPHMETRICSFLOAT glyphmetroics);
		/* Layer plane descriptor */
		public struct LAYERPLANEDESCRIPTOR
		{ // lpd
			public short nSize;
			public short nVersion;
			public LayerDescriptorFlags dwFlags;
			public byte iPixelType;
			public byte cColorBits;
			public byte cRedBits;
			public byte cRedShift;
			public byte cGreenBits;
			public byte cGreenShift;
			public byte cBlueBits;
			public byte cBlueShift;
			public byte cAlphaBits;
			public byte cAlphaShift;
			public byte cAccumBits;
			public byte cAccumRedBits;
			public byte cAccumGreenBits;
			public byte cAccumBlueBits;
			public byte cAccumAlphaBits;
			public byte cDepthBits;
			public byte cStencilBits;
			public byte cAuxBuffers;
			public byte iLayerPlane;
			public byte bReserved;
			//public int crTransparent;
		}

		public enum LayerDescriptorFlags
		{
			LPD_DOUBLEBUFFER = 0x00000001,
			LPD_STEREO = 0x00000002,
			LPD_SUPPORT_GDI = 0x00000010,
			LPD_SUPPORT_OPENGL = 0x00000020,
			LPD_SHARE_DEPTH = 0x00000040,
			LPD_SHARE_STENCIL = 0x00000080,
			LPD_SHARE_ACCUM = 0x00000100,
			LPD_SWAP_EXCHANGE = 0x00000200,
			LPD_SWAP_COPY = 0x00000400,
			LPD_TRANSPARENT = 0x00001000
		}

		public enum ColorType
		{
			LPD_TYPE_RGBA = 0,
			LPD_TYPE_COLORINDEX = 1
		}

		public enum SwapLayerBuffersFlags
		{
			WGL_SWAP_MAIN_PLANE = 0x00000001,
			WGL_SWAP_OVERLAY1 = 0x00000002,
			WGL_SWAP_OVERLAY2 = 0x00000004,
			WGL_SWAP_OVERLAY3 = 0x00000008,
			WGL_SWAP_OVERLAY4 = 0x00000010,
			WGL_SWAP_OVERLAY5 = 0x00000020,
			WGL_SWAP_OVERLAY6 = 0x00000040,
			WGL_SWAP_OVERLAY7 = 0x00000080,
			WGL_SWAP_OVERLAY8 = 0x00000100,
			WGL_SWAP_OVERLAY9 = 0x00000200,
			WGL_SWAP_OVERLAY10 = 0x00000400,
			WGL_SWAP_OVERLAY11 = 0x00000800,
			WGL_SWAP_OVERLAY12 = 0x00001000,
			WGL_SWAP_OVERLAY13 = 0x00002000,
			WGL_SWAP_OVERLAY14 = 0x00004000,
			WGL_SWAP_OVERLAY15 = 0x00008000,
			WGL_SWAP_UNDERLAY1 = 0x00010000,
			WGL_SWAP_UNDERLAY2 = 0x00020000,
			WGL_SWAP_UNDERLAY3 = 0x00040000,
			WGL_SWAP_UNDERLAY4 = 0x00080000,
			WGL_SWAP_UNDERLAY5 = 0x00100000,
			WGL_SWAP_UNDERLAY6 = 0x00200000,
			WGL_SWAP_UNDERLAY7 = 0x00400000,
			WGL_SWAP_UNDERLAY8 = 0x00800000,
			WGL_SWAP_UNDERLAY9 = 0x01000000,
			WGL_SWAP_UNDERLAY10 = 0x02000000,
			WGL_SWAP_UNDERLAY11 = 0x04000000,
			WGL_SWAP_UNDERLAY12 = 0x08000000,
			WGL_SWAP_UNDERLAY13 = 0x10000000,
			WGL_SWAP_UNDERLAY14 = 0x20000000,
			WGL_SWAP_UNDERLAY15 = 0x40000000
		}

		[DllImport(DllName)]
		public static extern bool wglDescribeLayerPlane(IntPtr hDC, int iPixelFormat, int iLayerPlane, uint nBytes,
											 ref LAYERPLANEDESCRIPTOR desc);
		[DllImport(DllName)]
		public static extern int wglSetLayerPaletteEntries(IntPtr hDC, int iLayerPlane, int start, int num_entries,
												 int[] entries);
		[DllImport(DllName)]
		public static extern int wglGetLayerPaletteEntries(IntPtr hDC, int iLayerPlane, int start, int num_entries,
												 int[] entries);
		[DllImport(DllName)]
		public static extern bool wglRealizeLayerPalette(IntPtr hDC, int iLayerPlane, bool realize);
		[DllImport(DllName)]
		public static extern bool wglSwapLayerBuffers(IntPtr hDC, uint fuPlanes);

		public struct WGLSWAP
		{
			public IntPtr hdc;
			public uint uiFlags;
		}

		public const int WGL_SWAPMULTIPLE_MAX = 16;

		[DllImport(DllName)]
		public static extern int wglSwapMultipleBuffers(uint count, WGLSWAP[] buffers);
		#endregion

		public override T GetProc<T>(string procname)
		{
			var ptr = wglGetProcAddress(procname);
			if (ptr == IntPtr.Zero)
				return default(T);
			var d = Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
			return (T)Convert.ChangeType(d, typeof(T));
		}

		private IntPtr hwnd;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wnd"></param>
		/// <exception cref="AccessViolationException">OpenGL subsystem encountered an access violation, try updating graphics drivers</exception>
		/// <exception cref="InvalidOperationException">Unable to create OpenGL context</exception>
		/// <exception cref="NotSupportedException">OpenGL 3.2 is not supported by this system</exception>
		/// <example>
		/// public class DisplayForm : Form
		/// {
		///		private WglContext ctx;
		///		protected override virtual void OnHandleCreated(EventArgs e)
		///		{
		///			ctx = new WglContext();
		///			ctx.CreateContext(this.Handle);
		///		}
		///		protected override virtual void OnClosing(ClosingEventArgs e)
		///		{
		///			ctx.Dispose();
		///		}
		/// }
		/// </example>
		public override void CreateContext(IntPtr wnd, IntPtr draw, OpenGLContext share)
		{
			// Force OpenGL library to link
			// or we will not be able to create a context
			linker = GetLinker();

			hwnd = wnd;
			display = hwnd;

			int pixel_format = 0;
			bool valid = false;
			PIXELFORMATDESCRIPTOR desc = new PIXELFORMATDESCRIPTOR();
			if (draw == IntPtr.Zero)
				drawable = GetDC(wnd);
			else
				drawable = draw;
				
			desc.nSize = (short)Marshal.SizeOf(desc);
			desc.nVersion = 1;
			desc.dwFlags = PixelFormatFlags.PFD_DOUBLEBUFFER | PixelFormatFlags.PFD_DRAW_TO_WINDOW | PixelFormatFlags.PFD_SUPPORT_OPENGL;
			desc.cDepthBits = 32;
			desc.cColorBits = 32;
			pixel_format = ChoosePixelFormat(drawable, ref desc);

			SetPixelFormat(drawable, pixel_format, ref desc);
			
			// Create legacy OpenGL context
			handle = wglCreateContext(drawable);

			wglMakeCurrent(drawable, handle);


			wglGetExtensionStringARB = GetProc<GetExtensionsStringARB>("wglGetExtensionStringARB");
			wglChoosePixelFormatARB = GetProc<ChoosePixelFormatARB>("wglChoosePixelFormatARB");
			wglGetPixelFormatAttribfvARB = GetProc<GetPixelFormatAttribfvARB>("wglGetPixelFormatAttribfvARB");
			wglGetPixelFormatAttribivARB = GetProc<GetPixelFormatAttribivARB>("wglGetPixelFormatAttribivARB");
			
			string ext = "";
			if(wglGetExtensionStringARB != null)
				ext = Marshal.PtrToStringAnsi(wglGetExtensionStringARB(drawable));

			var extensions = ext.Split(' ');

			if (drawable != IntPtr.Zero && wglChoosePixelFormatARB != null)
			{
				var fAttributes = new float[] { 0, 0 };
				var iAttributes = new int[] 
				{	WGL_DRAW_TO_WINDOW_ARB, 1,
					WGL_SUPPORT_OPENGL_ARB, 1,
					WGL_ACCELERATION_ARB,WGL_FULL_ACCELERATION_ARB,
					WGL_COLOR_BITS_ARB, 24,
					WGL_ALPHA_BITS_ARB, 8,
					WGL_DEPTH_BITS_ARB, 24,
					WGL_STENCIL_BITS_ARB, 8,
					WGL_DOUBLE_BUFFER_ARB, 1,
					WGL_SAMPLE_BUFFERS_ARB, 1,
					WGL_SAMPLES_ARB, samples ,						// Check For 4x Multisampling
					0,0
				};
				uint num_formats = 0;
				valid = wglChoosePixelFormatARB(drawable, iAttributes, fAttributes, 1, ref pixel_format, ref num_formats) && num_formats > 0;
				if (valid)
				{

					wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
					wglDeleteContext(handle);
					SetPixelFormat(drawable, pixel_format, ref desc);
					handle = wglCreateContext(drawable);
					wglMakeCurrent(drawable, handle);
				}
			}
			wglCreateContextAttribsARB = GetProc<CreateContextAttribsARB>("wglCreateContextAttribsARB");
			var s = OpenGL.glGetString((uint)OpenGL.Const.GL_VERSION);
			var str = Marshal.PtrToStringAnsi(s);
			var sub = str.Split('.');
			int major = 0;
			int minor = 0;
			int revision = 0;
			int.TryParse(sub[0], out major);
			int.TryParse(sub[1], out minor);
			if(sub.Length >= 3)
				int.TryParse(sub[0], out revision);

			Glorg2.Debugging.Debug.WriteLine("OpenGL version " + major + "." + minor + " supported by your system.");

			if (major < 3)
				throw new NotSupportedException("OpenGL 3 is not supported.");
			if (minor < 2 && major == 3)
				System.Diagnostics.Debug.WriteLine("OpenGL 3.2 is not supported. Consider upgrading display drivers.");
				

			if (wglCreateContextAttribsARB != null)
			{

				int[] attribs = new int[]
				{
					WGL_CONTEXT_MAJOR_VERSION_ARB, major,
					WGL_CONTEXT_MINOR_VERSION_ARB, minor, 
					WGL_CONTEXT_FLAGS_ARB, WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB,
					WGL_CONTEXT_PROFILE_MASK_ARB, 	WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB,
					0
				};
				IntPtr share_ctx = IntPtr.Zero;
				if (share != null)
					share_ctx = share.Handle;
				IntPtr newhandle = wglCreateContextAttribsARB(drawable, share_ctx, attribs);
				if (newhandle != IntPtr.Zero)
				{
					wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
					var err = OpenGL.glGetError();
					wglDeleteContext(handle);
					handle = IntPtr.Zero;
					if (newhandle != IntPtr.Zero) // If OpenGL 3.0 context creation failed, fallback to legacy 2.x
						handle = newhandle;
					else
						throw new NotSupportedException("Could not initialize OpenGL " + major + "." + minor + " : " + err.ToString());
				}
				else if (share_ctx != IntPtr.Zero)
				{
					wglShareLists(handle, share_ctx);
				}
			}
			else
				throw new NotSupportedException("OpenGL 3.2 is not supported by your system.");

			if (handle != IntPtr.Zero)
			{
				wglMakeCurrent(drawable, handle);
			}
			else
				throw new NotSupportedException("Could not create OpenGL context");

			

		}
		protected override DynamicLinking GetLinker()
		{
			return new DllLinking("OpenGL32.dll");
		}
		public override void MakeCurrent()
		{
			wglMakeCurrent(drawable, handle);
		}
		public override void Swap()
		{
			SwapBuffers(drawable);
		}

		private void Cleanup()
		{
			wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
			wglDeleteContext(handle);
			ReleaseDC(hwnd, drawable);
		}

		public override void Dispose()
		{
			Cleanup();
			GC.SuppressFinalize(this);
		}
		~WglContext()
		{
			Cleanup();
		}
	}
}
