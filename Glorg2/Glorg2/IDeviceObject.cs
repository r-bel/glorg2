using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	public interface IDeviceObject : IDisposable
	{
		void MakeCurrent();
		void MakeNonCurrent();
		//uint Handle { get; }
	}
}
