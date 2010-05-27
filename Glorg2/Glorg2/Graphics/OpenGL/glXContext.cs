using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Tao.Platform.X11;

namespace Glorg2.Graphics.OpenGL
{
	public sealed class glXContext : OpenGLContext
	{
		public const string DllName = "libGL.so";
		public const string XDllName = "libX11.so";


		private IntPtr wnd;
		private IntPtr visual;
		public struct XVisualInfo
		{
			public IntPtr visual;
			public IntPtr visualid;
			public int screen;
			public int depth;
			public int @class;
			public IntPtr red_mask;
			public IntPtr green_mask;
			public IntPtr blue_mask;
			public int colormap_size;
			public int bits_per_rgb;
		}
		public struct XSetWindowAttributes
		{
			public IntPtr background_pixmap;	/* background, None, or ParentRelative */
			public ulong background_pixel;	/* background pixel */
			public IntPtr border_pixmap;		/* border of the window or CopyFromParent */
			public ulong border_pixel;	/* border pixel value */
			public int bit_gravity;		/* one of bit gravity values */
			public int win_gravity;		/* one of the window gravity values */
			public int backing_store;		/* NotUseful, WhenMapped, Always */
			public ulong backing_planes;	/* planes to be preserved if possible */
			public ulong backing_pixel;	/* value to use in restoring planes */
			public bool save_under;		/* should bits under be saved? (popups) */
			public long event_mask;		/* set of events that should be saved */
			public long do_not_propagate_mask;	/* set of events that should not propagate */
			public bool override_redirect;		/* boolean value for override_redirect */
			public IntPtr colormap;		/* color map to be associated with window */
			public IntPtr cursor;			/* cursor to be displayed (or None) */
		} ;

		public struct XWindowAttributes
		{
			int x, y;			/* location of window */
			int width, height;		/* width and height of window */
			int border_width;		/* border width of window */
			int depth;			/* depth of window */
			IntPtr visual;			/* the associated visual structure */
			IntPtr root;			/* root of screen containing window */
			int @class;			/* InputOutput, InputOnly*/
			int bit_gravity;		/* one of the bit gravity values */
			int win_gravity;		/* one of the window gravity values */
			int backing_store;		/* NotUseful, WhenMapped, Always */
			ulong backing_planes;	/* planes to be preserved if possible */
			ulong backing_pixel;	/* value to be used when restoring planes */
			bool save_under;		/* boolean, should bits under be saved? */
			IntPtr colormap;		/* color map to be associated with window */
			bool map_installed;		/* boolean, is color map currently installed*/
			int map_state;			/* IsUnmapped, IsUnviewable, IsViewable */
			long all_event_masks;		/* set of events all people have interest in*/
			long your_event_mask;		/* my event mask */
			long do_not_propagate_mask;	/* set of events that should not propagate */
			bool override_redirect;		/* boolean value for override-redirect */
			IntPtr screen;			/* back pointer to correct screen */
		} ;

		private const int GLX_RGBA = 4;
		public const int GLX_DEPTH_SIZE = 12;
		public const int GLX_DOUBLEBUFFER = 5;
		public const int GLX_CONTEXT_MAJOR_VERSION_ARB = 0x2091;
		public const int GLX_CONTEXT_MINOR_VERSION_ARB = 0x2092;
		public delegate IntPtr XCreateContextAttribsARBProc(IntPtr display, IntPtr GLXFBConfig, IntPtr share_context, bool something, int[] parameters);

		public static XCreateContextAttribsARBProc glXCreateContextAttribsARB;

		public override T GetProc<T>(string procname)
		{
			IntPtr ptr = Tao.Platform.X11.Glx.glxGetProcAddress(procname);
			if (ptr == IntPtr.Zero || ptr == new IntPtr(1) || ptr == new IntPtr(2))
				return default(T);
			var obj = Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
			return (T)Convert.ChangeType(obj, typeof(T));
		}

		public override void CreateContext(IntPtr wnd_handle, IntPtr drawable, OpenGLContext share)
		{
			// Force loading of OpenGL library
			// This is later used by the OpenGL class to implement extensions.
			//linker = GetLinker();
			
			display = API.OpenDisplay(IntPtr.Zero);
			if (display == IntPtr.Zero)
				throw new InvalidOperationException("Cannot connect to X server");

			wnd = wnd_handle;
			int[] attr = {	
							Glx.GLX_X_RENDERABLE    , 1,
							Glx.GLX_DRAWABLE_TYPE   , Glx.GLX_WINDOW_BIT,
							Glx.GLX_RENDER_TYPE     , Glx.GLX_RGBA_BIT,
							Glx.GLX_X_VISUAL_TYPE   , Glx.GLX_TRUE_COLOR,
							Glx.GLX_RED_SIZE        , 8,
							Glx.GLX_GREEN_SIZE      , 8,
							Glx.GLX_BLUE_SIZE       , 8,
							Glx.GLX_ALPHA_SIZE      , 0,
							Glx.GLX_DEPTH_SIZE      , 16,
							//Glx.GLX_STENCIL_SIZE    , 8,
							Glx.GLX_DOUBLEBUFFER    , 1,
							0, 0 };
			
			API.MapWindow(display, wnd);
			int def_screen = API.DefaultScreen(display);
			Console.WriteLine("Display: 0x" + display.ToString("x"));
			Console.WriteLine("Deault screen: 0x" + def_screen.ToString("x"));
			int[] elems = new int[] { 0 };

			//IntPtr fb = Glx.glXChooseFBConfig(display, def_screen, attr, elems);
			
			//if (fb == IntPtr.Zero)
				//throw new InvalidOperationException("Could not retrieve framebuffer configuration");

			Console.WriteLine(elems[0]);

			//var info_ptr = Glx.glXGetVisualFromFBConfig(display, Marshal.ReadIntPtr(fb));
			attr = new int[] { GLX_RGBA, GLX_DEPTH_SIZE, 32, GLX_DOUBLEBUFFER, 0 };
			//IntPtr info_ptr = Glx.glXChooseVisual(display, def_screen, attr);
			var info_ptr = API.DefaultVisual(display, def_screen);

			if (info_ptr == IntPtr.Zero)
				throw new NotSupportedException("OpenGL is not supported.");

			VisualInfo info = new VisualInfo();
			Marshal.PtrToStructure(info_ptr, info);
			visual = info_ptr;

			

			handle = Glx.glXCreateContext(display, visual, IntPtr.Zero, true);

			if (handle == IntPtr.Zero)
				throw new InvalidOperationException("Unable to create OpenGL context");

			Glx.glXMakeCurrent(display, visual, handle);

			int[] parameters = new int[]
			{
				GLX_CONTEXT_MAJOR_VERSION_ARB, 3,
				GLX_CONTEXT_MINOR_VERSION_ARB, 2,
				0
			};

			glXCreateContextAttribsARB = GetProc<XCreateContextAttribsARBProc>("glXCreateContextAttribsARB");

			if (glXCreateContextAttribsARB == null)
				throw new NotSupportedException("OpenGL 3.0 not supported.");

			IntPtr new_handle = glXCreateContextAttribsARB(display, info_ptr, share.Handle, true, parameters);
			if (new_handle == IntPtr.Zero)
				throw new NotSupportedException("OpenGL 3.0 not supported.");

			Glx.glXMakeCurrent(display, visual, IntPtr.Zero);
			Glx.glXDestroyContext(display, handle);
			Glx.glXMakeCurrent(display, visual, new_handle);
			handle = new_handle;

			Console.WriteLine("glX Initialization succeded.");


		}

		public override void MakeCurrent()
		{
			Glx.glXMakeCurrent(display, visual, handle);
		}

		public override void Swap()
		{
			Glx.glXSwapBuffers(display, visual);
		}

		public override void Dispose()
		{
			Glx.glXMakeCurrent(display, visual, IntPtr.Zero);
			Glx.glXDestroyContext(display, handle);
		}

		protected override DynamicLinking GetLinker()
		{
			return new SoLinking(DllName);
		}
	}
}
