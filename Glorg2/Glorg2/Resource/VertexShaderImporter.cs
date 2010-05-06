using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL.Shaders;
namespace Glorg2.Resource
{
	public class VertexShaderImporter : ResourceImporter
	{
		public override string FileDescriptor
		{
			get { return "vs"; }
		}

		public override int Priority
		{
			get { return 100; }
		}

		internal readonly Type[] supported_types = new Type[] { typeof(VertexShader), typeof(Shader) };

		public override IEnumerable<Type> SupportedTypes
		{
			get { return supported_types; }
		}

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			System.IO.StreamReader rd = new System.IO.StreamReader(source);
			VertexShader ret = new VertexShader(rd.ReadToEnd());
			return ret as T;
				
			
		}
	}
}
