using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Glorg2.Graphics.OpenGL
{
	/// <summary>
	/// An abstract class representing OpenGL context for different platforms.
	/// See glXContext for OpenGL under X Window System, WglContext for OpenGL under Windows.
	/// The RenderingDevice class should implement the correct context for the correct platform.
	/// </summary>
	public abstract class OpenGLContext : IDisposable
	{

		protected int samples;


		protected bool multisampling_enabled;
		protected IntPtr handle;
		protected IntPtr display;
		protected DynamicLinking linker;

		/// <summary>
		/// This is a hint for the context creation to at least try to obtain this number of samples
		/// </summary>
		public int Samples { get { return samples; } set { samples = value; } }

		/// <summary>
		/// Retrieves if this OpenGL context was created with multisampling
		/// </summary>
		public bool MultisamplingEnabled { get { return multisampling_enabled; } }
		
		/// <summary>
		/// Handle to OpenGL context
		/// </summary>
		public IntPtr Handle { get { return handle; } }

		/// <summary>
		/// Handle to subsystem window
		/// </summary>
		public IntPtr DisplayHandle { get { return handle; } }

		/// <summary>
		/// Create a new context and setup all necessary information
		/// </summary>
		/// <param name="handle">Window handle which represents the destination in the window subsystem such as X or Windows.</param>
		public abstract void CreateContext(IntPtr handle);

		/// <summary>
		/// Make context current
		/// </summary>
		public abstract void MakeCurrent();

		/// <summary>
		/// Swap back buffer with front buffer
		/// </summary>
		public abstract void Swap();
		/// <summary>
		/// Disposes the object and frees all unmanaged resources aquired
		/// </summary>
		public abstract void Dispose();
		/// <summary>
		/// Retrieves an extension function for OpenGL or the context subsystem
		/// </summary>
		/// <typeparam name="T">Delegate type to be returned</typeparam>
		/// <param name="name">Name of the function, e.g. glTexImage3D</param>
		/// <returns>A delegate that wraps the native function</returns>
		public abstract T GetProc<T>(string name);
		
		/// <summary>
		/// Retrieves a linker for the OpenGL library 
		/// Only used internally to ensure that the library is loaded
		/// </summary>
		/// <returns></returns>

		protected abstract DynamicLinking GetLinker();
		/// <summary>
		/// Retrieves a linker for the OpenGL library 
		/// </summary>
		/// <returns></returns>
		public DynamicLinking Linker { get { return linker; } }
	}
}
