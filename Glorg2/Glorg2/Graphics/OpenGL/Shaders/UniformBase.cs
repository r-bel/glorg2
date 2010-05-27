using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	[Serializable()]
	public abstract class UniformBase
	{
		internal Uniform uniform;
		internal string name;
		public string Name { get { return name; } }
		internal void GetUniform(Program prog)
		{
			uniform = prog.GetUniform(name);
		}
		public abstract UniformBase Clone();
		public abstract void SetValue();
		public Uniform Uniform { get { return uniform; } }
	}
	[Serializable()]
	public abstract class UniformBaseType<T> : UniformBase
	{
		internal T val;
		
		public T Value { get { return val; } set { val = value; if (uniform != null) SetValue(); } }
		

		


		public override string ToString()
		{
			return val.ToString();
		}


	}

	

	[Serializable()]
	public sealed class ScalarFloatUniform : UniformBaseType<float>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new ScalarFloatUniform()
			{
				name = name,
				uniform = uniform,
				val = val
			};			
		}
	}
	[Serializable()]
	public sealed class ScalarIntUniform : UniformBaseType<int>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new ScalarIntUniform()
			{
				name = name,
				uniform = uniform,
				val = val
			};
		}
	}
	[Serializable()]
	public sealed class Vector2FloatUniform : UniformBaseType<Vector2>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new Vector2FloatUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}

	}
	[Serializable()]
	public sealed class Vector3FloatUniform : UniformBaseType<Vector3>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new Vector3FloatUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}
	}
	[Serializable()]
	public sealed class Vector4FloatUniform : UniformBaseType<Vector4>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new Vector4FloatUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}

	}
	[Serializable()]
	public sealed class Vector4IntUniform : UniformBaseType<Vector4Int>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new Vector4IntUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}

	}
	[Serializable()]
	public sealed class Vector3IntUniform : UniformBaseType<Vector3Int>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new Vector3IntUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}

	}
	[Serializable()]
	public sealed class Vector2IntUniform : UniformBaseType<Vector2Int>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new Vector2IntUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}

	}

	[Serializable()]
	public sealed class MatrixUniform : UniformBaseType<Matrix>
	{
		public override void SetValue()
		{
			uniform.SetValue(val);
		}
		public override UniformBase Clone()
		{
			return new MatrixUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}
	}
	[Serializable()]
	public sealed class QuaterniontUniform : UniformBaseType<Quaternion>
	{
		public override void SetValue()
		{
			uniform.SetValue((Vector4)val);
		}
		public override UniformBase Clone()
		{
			return new QuaterniontUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}

	}

	[Serializable()]
	public sealed class TextureUniform : UniformBaseType<Texture>
	{
		public int TextureIndex { get { return tex_index; } set { tex_index = value; } }
		int tex_index;
		public override void SetValue()
		{
			OpenGL.glActiveTexture(OpenGL.Const.GL_TEXTURE1 + (uint)tex_index);
			val.MakeCurrent();
			uniform.SetValue(tex_index + 1);
		}
		public void SetValue(int index)
		{
			uniform.SetValue(index);
		}
		public override UniformBase Clone()
		{
			return new TextureUniform()
			{
				name = name,
				uniform = uniform,
				val = this.val
			};
		}
	}
	
}
