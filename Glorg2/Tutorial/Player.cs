using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial
{
	public class Player : DynamicMesh
	{
		
		public Player()
		{
			
		}
		public override void InitializeGraphics()
		{
			Owner.Resources.Load("data\\spship", out model);

		}
	}
}
