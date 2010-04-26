using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL.Shaders;

namespace Glorg2.Graphics.OpenGL
{
	[Serializable()]
	public sealed class Material : Resource.Resource, IDeviceObject
	{
		[NonSerialized()]
		Program shader;

		string vertex_shader;
		string geometry_shader;
		string fragment_shader;

		internal Material()
		{
		}

		public Material(Program shader)
		{
			this.shader = shader;
		}

		public Material CreateReference()
		{
			Material ret = new Material();
			ret.fragment_shader = fragment_shader;
			ret.geometry_shader = geometry_shader;
			ret.vertex_shader = vertex_shader;
			ret.shader = shader;
			ret.entries = new List<UniformBase>();
			//Links++;
			foreach (var e in entries)
				ret.entries.Add(e.Clone());
			return ret;
		}

		List<UniformBase> entries;

		public Program Shader { get { return shader; } }
		public List<UniformBase> Entries { get { return entries; } }

		public T GetUniform<T>(string name)
			where T : UniformBase
		{
			return entries.Find(item => item.name == name) as T;
		}

		#region IDeviceObject Members

		public void MakeCurrent()
		{
			int index = 0;
			foreach(var item in entries)
			{
				if (item is TextureUniform)
				{
					OpenGL.glActiveTextureARB((uint)++index);
					(item as TextureUniform).val.MakeCurrent();
				}
				item.SetValue();				
			}
			shader.MakeCurrent();
		}

		public void MakeNonCurrent()
		{
			shader.MakeNonCurrent();
			int index = 0;
			foreach (var item in entries)
			{
				if (item is TextureUniform)
				{
					OpenGL.glActiveTextureARB((uint)++index);
					(item as TextureUniform).val.MakeNonCurrent();
				}
			}
		}

		#endregion
	}
}
