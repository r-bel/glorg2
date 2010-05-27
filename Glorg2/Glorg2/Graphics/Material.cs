/*
Copyright (C) 2010 Henning Moe

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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
			foreach (var un in uniforms)
			{
				var tex = un as TextureUniform;
				if (tex != null && tex.val != null)
				{
					tex.val.Dispose();
					tex.val = null;
				}
			}
			uniforms.Clear();
		}

		public virtual void SetupMaterial()
		{
		}


		#region IDeviceObject Members

		public void MakeCurrent()
		{
			shader.MakeCurrent();
			uint index = 0;
			foreach (var u in uniforms)
			{
				var tex = u as TextureUniform;
				if (tex != null)
				{
					tex.val.MakeCurrent(index++);
					u.Uniform.SetValue(index);
				}
				else
					u.SetValue();
			}
		}

		public void MakeNonCurrent()
		{
			shader.MakeNonCurrent();
		}

		#endregion
	}
}
