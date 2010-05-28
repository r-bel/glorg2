using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Debugging
{
	public static class Debug
	{
		public static bool LogEnabled { get; set; }
		static Queue<string> debug_queue = new Queue<string>();

		internal static float fps;

		public static float FramesPerSecond { get { return fps; } }

		public static void WriteLine(string line)
		{
			if (LogEnabled)
			{
				lock (debug_queue)
				{
					debug_queue.Enqueue(line);
				}
			}
		}

		public static string ReadLine()
		{
			lock (debug_queue)
			{
				if (debug_queue.Count > 0)
					return debug_queue.Dequeue();
				else
					return null;
			}
		}

	}
}
