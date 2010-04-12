using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL
{
	[Serializable()]
	public sealed class Material : Resource.Resource
	{
		public sealed class MaterialEntry
		{
			Shaders.Uniform uniform;
			string name;
			Texture texture;
			public string Name { get { return name; } }

		}


		Shaders.Program shader;
		List<MaterialEntry> entries;

		public Shaders.Program Shader { get { return shader; } }
		public List<MaterialEntry> Entries { get { return entries; } }
	}
}
