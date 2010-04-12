using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	public sealed class Uniform
	{
		private int location;
		public int Location { get { return location; } }

		internal Uniform(int location)
		{
			this.location = location;
		}

		public void SetValue(int value)
		{
			OpenGL.glUniform1iARB(location, value);
		}
		public void SetValue(float value)
		{
			OpenGL.glUniform1fARB(location, value);
		}
		public void SetValue(Vector2 value)
		{
			OpenGL.glUniform2fvARB(location, 2, ref value);
		}
		public void SetValue(Vector3 value)
		{
			OpenGL.glUniform3fvARB(location, 3, ref value);
		}
		public void SetValue(Vector4 value)
		{
			OpenGL.glUniform4fvARB(location, 4, ref value);
		}
		public void SetValue(Matrix value)
		{
			OpenGL.glUniformMatrix4fvARB(location, 16, OpenGL.boolean.FALSE, ref value);
		}
	}
}
