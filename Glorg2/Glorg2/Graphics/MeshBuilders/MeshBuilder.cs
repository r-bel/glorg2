using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.MeshBuilders
{
	[Serializable()]
	public abstract class MeshBuilder
	{
		public abstract Model Build();
	}
}
