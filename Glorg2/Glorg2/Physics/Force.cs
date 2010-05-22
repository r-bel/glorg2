using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Physics
{
	[Serializable()]
	public class Force
	{
		public string Name { get; set; }
		public float Power { get; set; }
		public Vector3 Direction { get; set; }
	}
}
