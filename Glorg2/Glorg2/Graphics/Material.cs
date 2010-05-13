using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL.Shaders;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics;
using Glorg2;
namespace Glorg2.Graphics
{
	public class Material : Resource.Resource, Glorg2.IDeviceObject
	{
		internal Material reference;
		internal List<UniformBase> uniforms;
		Program shader;
		public List<UniformBase> Entries { get { return uniforms; } }
		public Program Shader { get { return shader; } set { shader = value; } }
		public Material()
		{
			uniforms = new List<UniformBase>();
		}
		public override void DoDispose()
		{
			shader.Dispose();
			uniforms.Clear();
		}


		#region IDeviceObject Members

		public void MakeCurrent()
		{
			shader.MakeCurrent();
			foreach (var u in uniforms)
			{
				u.SetValue();
				var tex = u as TextureUniform;
				//if(tex != null)
					//OpenGL.OpenGL.glActiveTextureARB(u.
			}
		}

		public void MakeNonCurrent()
		{
			shader.MakeNonCurrent();
		}

		#endregion
	}
}
