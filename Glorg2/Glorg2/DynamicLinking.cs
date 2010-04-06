using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace Glorg2
{
	/// <summary>
	/// This class is the base class for library linking. It is used to import functions
	/// from dynamic link libraries such as .so or .dll files.
	/// This class is abstract, use one of it's descendants.
	/// </summary>
	public abstract class DynamicLinking : IDisposable
	{
		protected IntPtr handle;
		protected string filename;
		public abstract T GetFunction<T>(string name);
		public abstract void Dispose();

		public IntPtr Handle { get { return handle; } }

	}
	/// <summary>
	/// Implements LoadLibrary functions for Windows
	/// </summary>
	public class DllLinking : DynamicLinking
	{
		[DllImport("kernel32")]
		private static extern IntPtr LoadLibrary(string filename);
		[DllImport("kernel32")]
		public static extern bool FreeLibrary(IntPtr handle);
		[DllImport("kernel32")]
		public static extern Delegate GetProcAddress(IntPtr module, string proc);



		public DllLinking(string library)
		{
			filename = library;
			handle = LoadLibrary(library);
			if (handle == IntPtr.Zero)
				throw new System.IO.FileLoadException("Unable to load dynamic library \"" + library + "\"");
		}

		public override T GetFunction<T>(string name)
		{
			Delegate v = GetProcAddress(handle, name);
			return (T)Convert.ChangeType(v, typeof(T));

		}
		public override void Dispose()
		{
			FreeLibrary(handle);
		}
	}
	/// <summary>
	/// Implements loader functionality for Unix operating systems
	/// </summary>
	public class SoLinking : DynamicLinking
	{
		private const string DllName = "libdl.so";
		[DllImport(DllName)]
		public static extern IntPtr dlopen(string name, int param);
		[DllImport(DllName)]
		public static extern Delegate dlsym(IntPtr handle, string symbol);
		[DllImport(DllName)]
		public static extern int dlclose(IntPtr handle);

		public SoLinking(string library)
		{
			filename = library;
			handle = dlopen(filename, 2);
			if(handle == IntPtr.Zero)
				throw new System.IO.FileLoadException("Unable to load dynamic library \"" + library + "\"");
		}

		public override T GetFunction<T>(string name)
		{
			Delegate v = dlsym(handle, name);
			return (T)Convert.ChangeType(v, typeof(T));
		}

		public override void Dispose()
		{
			if (dlclose(handle) != 0)
				throw new InvalidOperationException("Something went wrong...");
		}
	}
}
