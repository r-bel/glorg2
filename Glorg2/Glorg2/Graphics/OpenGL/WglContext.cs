using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Glorg2.Graphics.OpenGL
{
	public sealed class WglContext : OpenGLContext
	{
		#region wgl Extensions

		public enum AttribBits
		{
			WGL_CONTEXT_DEBUG_BIT_ARB = 0x00000001,
			WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 0x00000002,
			WGL_CONTEXT_MAJOR_VERSION_ARB = 0x2091,
			WGL_CONTEXT_MINOR_VERSION_ARB = 0x2092,
			WGL_CONTEXT_LAYER_PLANE_ARB = 0x2093,
			WGL_CONTEXT_FLAGS_ARB = 0x2094,
			ERROR_INVALID_VERSION_ARB = 0x2095
		}

		public delegate IntPtr CreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);

		public CreateContextAttribsARB wglCreateContextAttribsARB;

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
		public static extern IntPtr ReleaseDC(IntPtr hDC);


		#endregion
		#region WGL Platform specific
		public const string DllName = "OpenGL32";

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
		public static extern Delegate wglGetProcAddress(string proc);
		[DllImport(DllName)]
		public static extern bool wglMakeCurrent(IntPtr hDC, IntPtr hGL);
		[DllImport(DllName)]
		public static extern bool wglShareLists(IntPtr gla, IntPtr glb);
		[DllImport(DllName)
				]
		public static extern bool wglUseFontBitmaps(IntPtr hdc, int first, int count, int listbase);
		[DllImport(DllName)]
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
			return (T)Convert.ChangeType(wglGetProcAddress(procname), typeof(T));
		}


		private IntPtr hdc;

		public IntPtr Hdc { get { return hdc; } }

		public override void CreateContext(IntPtr handle)
		{
			// Force OpenGL library to link
			// or we will not be able to create a context
			linker = GetLinker();

			int pixelformat;
			PIXELFORMATDESCRIPTOR desc = new PIXELFORMATDESCRIPTOR();

			hdc = GetDC(handle);
			desc.nSize = (short)Marshal.SizeOf(desc);
			desc.nVersion = 1;
			desc.dwFlags = PixelFormatFlags.PFD_DOUBLEBUFFER | PixelFormatFlags.PFD_DRAW_TO_WINDOW | PixelFormatFlags.PFD_SUPPORT_OPENGL;
			desc.cDepthBits = 32;
			desc.cColorBits = 32;

			pixelformat = ChoosePixelFormat(hdc, ref desc);

			SetPixelFormat(hdc, pixelformat, ref desc);

			handle = wglCreateContext(hdc);
			wglMakeCurrent(hdc, handle);
			var s = OpenGL.glGetString((uint)OpenGL.StringName.GL_VERSION);
			var str = Marshal.PtrToStringAnsi(s);
			wglCreateContextAttribsARB = GetProc<CreateContextAttribsARB>("wglCreateContextAttribsARB");

			int[] attribs = new int[]
			{
				(int)AttribBits.WGL_CONTEXT_MAJOR_VERSION_ARB,
				3,
				(int)AttribBits.WGL_CONTEXT_MINOR_VERSION_ARB,
				2,
				0
			};

			IntPtr newhandle = wglCreateContextAttribsARB(hdc, handle, attribs);
			if (newhandle == IntPtr.Zero)
			{
				wglDeleteContext(handle);
				throw new InvalidOperationException("System does not support OpenGL 3.2");
			}
			handle = newhandle;
			wglMakeCurrent(hdc, handle);

		}
		protected override DynamicLinking GetLinker()
		{
			return new DllLinking("OpenGL32.dll");
		}
		public override void MakeCurrent()
		{
			wglMakeCurrent(hdc, handle);
		}
		public override void Swap()
		{
			SwapBuffers(hdc);
		}

		public override void Dispose()
		{
			wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
			wglDeleteContext(handle);
			ReleaseDC(hdc);
		}
	}
}
