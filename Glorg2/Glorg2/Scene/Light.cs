using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
namespace Glorg2.Scene
{
	public class Light : Node
	{

		//private static HashSet<uint> Lights = new List<uint>();

		private uint index;
		private bool enabled;

		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				if (value)
					OpenGL.glEnable(index);
				else
					OpenGL.glDisable(index);
			}
		}

		public uint Handle { get { return index; } }

		protected internal override void InitializeGraphics()
		{
			
		}

		protected override void Render(float time, Glorg2.Graphics.GraphicsDevice dev)
		{
			OpenGL.glLightfv(index, OpenGL.Const.GL_POSITION, new float[] { 0, 0, 0, 1 });
			base.Render(time, dev);
		}

	}
}
