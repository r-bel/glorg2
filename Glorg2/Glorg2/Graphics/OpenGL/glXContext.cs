using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Glorg2.Graphics.OpenGL
{
	public sealed class glXContext : OpenGLContext
	{
		public const string DllName = "libGL.so";
		public const string XDllName = "libX11.so";
		#region Xgl Implementation details

		[DllImport(XDllName)]
		public static extern IntPtr XOpenDisplay(string display_name);

		//[DllImport(XDllName)]
		//int XGetWindowAttributes(IntPtr display, IntPtr window, ref XWindowAttributes attribs);

		//[DllImport(XDllName)]
		//public static extern IntPtr XCreateColormap(IntPtr display, IntPtr w, IntPtr visual, int alloc);

		[DllImport(XDllName)]
		public static extern int XMapWindow(IntPtr display, IntPtr window);

		[DllImport(DllName)]
		public static extern IntPtr glXChooseVisual(IntPtr display, int screen,
							 int[] attribList);
		[DllImport(DllName)]
		public static extern IntPtr glXCreateContext(IntPtr dpy, IntPtr vis,
							IntPtr shareList, bool direct);
		[DllImport(DllName)]
		public static extern void glXDestroyContext(IntPtr display, IntPtr ctx);
		[DllImport(DllName)]
		public static extern bool glXMakeCurrent(IntPtr dpy, IntPtr drawable,
						IntPtr ctx);
		[DllImport(DllName)]
		public static extern void glXCopyContext(IntPtr dpy, IntPtr src, IntPtr dst,
						ulong mask);
		[DllImport(DllName)]
		public static extern void glXSwapBuffers(IntPtr dpy, IntPtr drawable);

		/*public static extern IntPtr glXCreateGLXPixmap( IntPtr dpy, XVisualInfo *visual,
							 IntPtr pixmap );*/

		/*extern void glXDestroyGLXPixmap( Display *dpy, GLXPixmap pixmap );*/
		[DllImport(DllName)]
		public static extern bool glXQueryExtension(IntPtr dpy, ref int errorb, ref int evnt);
		[DllImport(DllName)]
		public static extern bool glXQueryVersion(IntPtr dpy, ref int maj, ref int min);
		[DllImport(DllName)]
		public static extern bool glXIsDirect(IntPtr dpy, IntPtr ctx);
		[DllImport(DllName)]
		public static extern int glXGetConfig(IntPtr dpy, IntPtr visual,
					 int attrib, ref int value);
		[DllImport(DllName)]
		public static extern IntPtr glXGetCurrentContext();
		[DllImport(DllName)]
		public static extern IntPtr glXGetCurrentDrawable();
		[DllImport(DllName)]
		public static extern void glXWaitGL();
		[DllImport(DllName)]
		public static extern void glXWaitX();
		[DllImport(DllName)]
		public static extern void glXUseXFont(IntPtr font, int first, int count, int list);



		/* GLX 1.1 and later */
		/*extern const char *glXQueryExtensionsString( Display *dpy, int screen );

		extern const char *glXQueryServerString( Display *dpy, int screen, int name );

		extern const char *glXGetClientString( Display *dpy, int name );*/


		/* GLX 1.2 and later */
		[DllImport(DllName)]
		public static extern IntPtr glXGetCurrentDisplay();


		/* GLX 1.3 and later */
		[DllImport(DllName)]
		public static extern IntPtr glXChooseFBConfig(IntPtr dpy, int screen,
												int[] attribList, ref int nitems);
		[DllImport(DllName)]
		public static extern int glXGetFBConfigAttrib(IntPtr dpy, IntPtr config,
										 int attribute, ref int value);
		[DllImport(DllName)]
		public static extern IntPtr glXGetFBConfigs(IntPtr dpy, int screen,
											ref int nelements);
		[DllImport(DllName)]
		public static extern IntPtr glXGetVisualFromFBConfig(IntPtr dpy,
													  IntPtr config);
		[DllImport(DllName)]
		public static extern IntPtr glXCreateWindow(IntPtr dpy, IntPtr config,
										  IntPtr win, int[] attribList);
		[DllImport(DllName)]
		public static extern void glXDestroyWindow(IntPtr dpy, IntPtr window);

		/*public static extern GLXPixmap glXCreatePixmap( Display *dpy, GLXFBConfig config,
										  Pixmap pixmap, const int *attribList );*/

		/*extern void glXDestroyPixmap( Display *dpy, GLXPixmap pixmap );*/

		/*extern GLXPbuffer glXCreatePbuffer( Display *dpy, GLXFBConfig config,
											const int *attribList );

		extern void glXDestroyPbuffer( Display *dpy, GLXPbuffer pbuf );*/
		[DllImport(DllName)]
		public static extern void glXQueryDrawable(IntPtr dpy, IntPtr draw, int attribute,
									  ref uint value);
		[DllImport(DllName)]
		public static extern IntPtr glXCreateNewContext(IntPtr dpy, IntPtr config,
											   int renderType, IntPtr shareList,
											   bool direct);
		[DllImport(DllName)]
		public static extern bool glXMakeContextCurrent(IntPtr dpy, IntPtr draw,
										   IntPtr read, IntPtr ctx);
		[DllImport(DllName)]
		public static extern IntPtr glXGetCurrentReadDrawable();
		[DllImport(DllName)]
		public static extern int glXQueryContext(IntPtr dpy, IntPtr ctx, int attribute,
									ref int value);
		[DllImport(DllName)]
		public static extern void glXSelectEvent(IntPtr dpy, IntPtr drawable,
									ulong mask);
		[DllImport(DllName)]
		public static extern void glXGetSelectedEvent(IntPtr dpy, IntPtr drawable,
										 ref ulong mask);
		[DllImport(DllName)]
		public static extern IntPtr glXGetProcAddressARB (string procname);
		#endregion

		private IntPtr display;
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

		public override T GetProc<T>(string procname)
		{
			IntPtr ptr = glXGetProcAddressARB(procname);
			if (ptr == IntPtr.Zero)
				return default(T);
			var obj = Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
			return (T)Convert.ChangeType(obj, typeof(T));
		}

		public override void CreateContext(IntPtr wnd_handle)
		{
			// Force loading of OpenGL library
			// This is later used by the OpenGL class to implement extensions.
			linker = GetLinker();

			display = XOpenDisplay(null);
			if (display == IntPtr.Zero)
				throw new InvalidOperationException("Cannot connect to X server");

			wnd = wnd_handle;
			int[] attr = { GLX_RGBA, GLX_DEPTH_SIZE, 32, GLX_DOUBLEBUFFER, 0 };

			visual = glXChooseVisual(display, 0, attr);

			XMapWindow(display, wnd);

			handle = glXCreateContext(display, visual, IntPtr.Zero, true);

			if (handle == IntPtr.Zero)
				throw new InvalidOperationException("Unable to create OpenGL context");

			glXMakeCurrent(display, visual, handle);
			
			// TODO: Add Xgl context creation

			/*
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#define GL_GLEXT_PROTOTYPES 1
#define GLX_GLXEXT_PROTOTYPES 1
#include <X11/Xlib.h>
#include <X11/Xutil.h>
#include <GL/gl.h>
#include <GL/glx.h>

#define GLX_CONTEXT_MAJOR_VERSION_ARB       0x2091
#define GLX_CONTEXT_MINOR_VERSION_ARB       0x2092
typedef GLXContext (*glXCreateContextAttribsARBProc)(Display*, GLXFBConfig, GLXContext, Bool, const int*);

int main (int argc, char ** argv)
{
  Display *display = XOpenDisplay(0);

  if ( !display )
  {
    printf( "Failed to open X display\n" );
    exit(1);
  }

  // Get a matching FB config
  static int visual_attribs[] =
    {
      GLX_X_RENDERABLE    , True,
      GLX_DRAWABLE_TYPE   , GLX_WINDOW_BIT,
      GLX_RENDER_TYPE     , GLX_RGBA_BIT,
      GLX_X_VISUAL_TYPE   , GLX_TRUE_COLOR,
      GLX_RED_SIZE        , 8,
      GLX_GREEN_SIZE      , 8,
      GLX_BLUE_SIZE       , 8,
      GLX_ALPHA_SIZE      , 8,
      GLX_DEPTH_SIZE      , 24,
      GLX_STENCIL_SIZE    , 8,
      GLX_DOUBLEBUFFER    , True,
      //GLX_SAMPLE_BUFFERS  , 1,
      //GLX_SAMPLES         , 4,
      None
    };

  printf( "Getting matching framebuffer configs\n" );
  int fbcount;
  GLXFBConfig *fbc = glXChooseFBConfig( display, DefaultScreen( display ), 
                                        visual_attribs, &fbcount );
  if ( !fbc )
  {
    printf( "Failed to retrieve a framebuffer config\n" );
    exit(1);
  }
  printf( "Found %d matching FB configs.\n", fbcount );

  // Pick the FB config/visual with the most samples per pixel
  printf( "Getting XVisualInfos\n" );
  int best_fbc = -1, worst_fbc = -1, best_num_samp = -1, worst_num_samp = 999;

  int i;
  for ( i = 0; i < fbcount; i++ )
  {
    XVisualInfo *vi = glXGetVisualFromFBConfig( display, fbc[i] );
    if ( vi )
    {
      int samp_buf, samples;
      glXGetFBConfigAttrib( display, fbc[i], GLX_SAMPLE_BUFFERS, &samp_buf );
      glXGetFBConfigAttrib( display, fbc[i], GLX_SAMPLES       , &samples  );
      
      printf( "  Matching fbconfig %d, visual ID 0x%2x: SAMPLE_BUFFERS = %d,"
              " SAMPLES = %d\n", 
              i, vi -> visualid, samp_buf, samples );

      if ( best_fbc < 0 || samp_buf && samples > best_num_samp )
        best_fbc = i, best_num_samp = samples;
      if ( worst_fbc < 0 || !samp_buf || samples < worst_num_samp )
        worst_fbc = i, worst_num_samp = samples;
    }
    XFree( vi );
  }

  // Get a visual
  int fbc_id = best_fbc;
  //int fbc_id = worst_fbc;

  XVisualInfo *vi = glXGetVisualFromFBConfig( display, fbc[ fbc_id ]  );
  printf( "Chosen visual ID = 0x%x\n", vi->visualid );

  printf( "Creating colormap\n" );
  XSetWindowAttributes swa;
  swa.colormap = XCreateColormap( display, RootWindow( display, vi->screen ), 
                                  vi->visual, AllocNone );
  swa.background_pixmap = None ;
  swa.border_pixel      = 0;
  swa.event_mask        = StructureNotifyMask;

  printf( "Creating window\n" );
  Window win = XCreateWindow( display, RootWindow( display, vi->screen ), 
                              0, 0, 100, 100, 0, vi->depth, InputOutput, 
                              vi->visual, 
                              CWBorderPixel|CWColormap|CWEventMask, &swa );
  if ( !win )
  {
    printf( "Failed to create window.\n" );
    exit(1);
  }

  XStoreName( display, win, "GL 3.0 Window");

  printf( "Mapping window\n" );
  XMapWindow( display, win );

  // See if GL driver supports glXCreateContextAttribsARB()
  //   Create an old-style GLX context first, to get the correct function ptr.
  glXCreateContextAttribsARBProc glXCreateContextAttribsARB = 0;

  GLXContext ctx_old = glXCreateContext( display, vi, 0, True );
  glXCreateContextAttribsARB = (glXCreateContextAttribsARBProc)
           glXGetProcAddress( (const GLubyte *) "glXCreateContextAttribsARB" );

  GLXContext ctx = 0;

  // If it doesn't, just use the old-style 2.x GLX context
  if ( !glXCreateContextAttribsARB )
  {
    printf( "glXCreateContextAttribsARB() not found"
            " ... using old-style GLX context\n" );
    ctx = ctx_old;
  }

  // If it "does", try to get a GL 3.0 context!
  else
  {
    glXMakeCurrent( display, 0, 0 );
    glXDestroyContext( display, ctx_old );

    static int context_attribs[] =
      {
        GLX_CONTEXT_MAJOR_VERSION_ARB, 3,
        GLX_CONTEXT_MINOR_VERSION_ARB, 0,
        //GLX_CONTEXT_FLAGS_ARB        , GLX_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB,
        None
      };

    printf( "Creating context\n" );
    ctx = glXCreateContextAttribsARB( display, fbc[ fbc_id ], 0, 
                                      True, context_attribs );
    if ( ctx )
      printf( "Created GL 3.0 context\n" );
    else
    {
      // Couldn't create GL 3.0 context.  Fall back to old-style 2.x context.
      printf( "Failed to create GL 3.0 context"
              " ... using old-style GLX context\n" );
      ctx = glXCreateContext( display, vi, 0, True );
    }
  }

  XFree( fbc );

  // Verifying that context is a direct context
  printf( "Verifying that context is direct\n" );
  if ( ! glXIsDirect ( display, ctx ) )
  {
    printf( "Indirect GLX rendering context obtained" );
    exit(1);
  }

  printf( "Making context current\n" );
  glXMakeCurrent( display, win, ctx );

  glClearColor ( 0, 0.5, 1, 1 );
  glClear ( GL_COLOR_BUFFER_BIT );
  glXSwapBuffers ( display, win );

  sleep( 1 );

  glClearColor ( 1, 0.5, 0, 1 );
  glClear ( GL_COLOR_BUFFER_BIT );
  glXSwapBuffers ( display, win );

  sleep( 1 );

  ctx = glXGetCurrentContext(  );
  glXMakeCurrent( display, 0, 0 );
  glXDestroyContext( display, ctx );
}
			 */
			
		}

		public override void MakeCurrent()
		{

		}

		public override void Swap()
		{

		}

		public override void Dispose()
		{
			glXMakeCurrent(display, visual, IntPtr.Zero);
			glXDestroyContext(display, handle);
		}

		protected override DynamicLinking GetLinker()
		{
			return new SoLinking("libGL.so");
		}
	}
}
