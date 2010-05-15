using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	/// <summary>
	/// Uniforms are used to write data to a shader program.
	/// </summary>
	/// <remarks>The developer is required to know the datatype of the uniform. Setting an invalid value may cause the program to behave irregularily, or not at all.</remarks>
	public sealed class Uniform
	{
		private int location;
		public int Location { get { return location; } }

		internal Uniform(int location)
		{
			this.location = location;
		}

		/// <summary>
		/// Sets the value of this unifrom as an integer
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(int value)
		{
			OpenGL.glUniform1iARB(location, value);
		}
		/// <summary>
		/// Sets the value of this uniform as a 32-bit floating point
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(float value)
		{
			OpenGL.glUniform1fARB(location, value);
		}
		/// <summary>
		/// Sets the value of this uniform as an 2-dimensional vector of 32-bit floating points
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Vector2 value)
		{
			OpenGL.glUniform2fvARB(location, 2, ref value);
		}
		/// <summary>
		/// Sets the value of this uniform as an 3-dimensional vector of 32-bit floating points
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Vector3 value)
		{
			OpenGL.glUniform3fvARB(location, 3, ref value);
		}
		/// <summary>
		/// Sets the value of this uniform as an 4-dimensional vector of 32-bit floating points
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Vector4 value)
		{
			OpenGL.glUniform4fvARB(location, 4, ref value);
		}

		public void SetValue(Vector2Int value)
		{
			OpenGL.glUniform2iARB(location, value.x, value.y);
		}

		public void SetValue(Vector3Int value)
		{
			OpenGL.glUniform3iARB(location, value.x, value.y, value.z);
		}

		public void SetValue(Vector4Int value)
		{
			OpenGL.glUniform4iARB(location, value.x, value.y, value.z, value.w);
		}


		/// <summary>
		/// Sets the value of this uniform as an 4x4 matrix of floating points
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Matrix value)
		{
			OpenGL.glUniformMatrix4fvARB(location, 16, OpenGL.boolean.FALSE, ref value);
		}
		/// <summary>
		/// Assign s a texture object to this uniform
		/// </summary>
		/// <param name="value"></param>
		public void SetValue(Texture value)
		{
			OpenGL.glUniform1iARB(location, (int)value.Handle);
		}
	}
}
