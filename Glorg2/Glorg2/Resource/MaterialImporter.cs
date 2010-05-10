using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics.OpenGL.Shaders;
namespace Glorg2.Resource
{
	public class MaterialImporter : ResourceImporter
	{
		public override string FileDescriptor
		{
			get { return "mxl"; }
		}
		static readonly Type[] supported_types = new Type[] { typeof(Glorg2.Graphics.Material) };
		public override IEnumerable<Type> SupportedTypes
		{
			get { return supported_types; }
		}

		public override int Priority
		{
			get { return 50; }
		}
		private static T LoadShader<T>(XmlNode n, ResourceManager man)
			where T : Resource
		{
			var att = n.Attributes["src"];
			if (att != null)
			{
				T ret;
				man.Load<T>(att.Value, out ret);
				return ret;
			}
			else
			{
				return Activator.CreateInstance(typeof(T), n.InnerText) as T;
			}
		}
		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			
			XmlDocument doc = new XmlDocument();
			doc.Load(source);
			if (doc.ChildNodes.Count == 2)
			{
				var vsn = doc.SelectSingleNode("Material/VertexShader");
				var fsn = doc.SelectSingleNode("Material/FragmentShader");
				var gsn = doc.SelectSingleNode("Material/GeometryShader");

				Material ret = new Material();

				if (vsn == null)
					return default(T);
				var vs = LoadShader<VertexShader>(vsn, man);
				Program prog = new Program();
				prog.shaders.Add(vs);

				if (gsn != null)
				{
					var gs = LoadShader<GeometryShader>(gsn, man);
					prog.shaders.Add(gs);
				}

				if (fsn != null)
				{
					var fs = LoadShader<FragmentShader>(fsn, man);
					prog.shaders.Add(fs);
				}
				ret.Shader = prog;
				prog.Compile();

				var uniforms = doc.SelectSingleNode("Material/Uniforms");
				if (uniforms != null && uniforms.HasChildNodes)
				{
					foreach (var n in uniforms.ChildNodes)
					{
						var node = n as XmlNode;
						if (node.Name.ToLower() == "uniform")
						{
							string t = node.Attributes["type"].Value;
							string val = node.Attributes["value"].Value;
							string name = node.Attributes["name"].Value;
							UniformBase uni = null;
							switch (t)
							{
								case "float":
									if((uni = prog.GetUniformType<ScalarFloatUniform, float>(name)) != null);
										(uni as ScalarFloatUniform).val = float.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "int":
									break;
								case "float2":
									if((uni = prog.GetUniformType<Vector2FloatUniform, Vector2>(name)) != null)
										(uni as Vector2FloatUniform).val = Vector2.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "float3":
									if ((uni = prog.GetUniformType<Vector3FloatUniform, Vector3>(name)) != null)
										(uni as Vector3FloatUniform).val = Vector3.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "float4":
									if ((uni = prog.GetUniformType<Vector4FloatUniform, Vector4>(name)) != null)
										(uni as Vector4FloatUniform).val = Vector4.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "texture2d":
									if ((uni = prog.GetUniformType<TextureUniform, Texture>(name)) != null)
										man.Load(name, out (uni as TextureUniform).val);
									break;
							}
							if (uni != null)
							{
								ret.uniforms.Add(uni);
								uni.SetValue();
							}
						}
					}
				}

				return ret as T;
			}
			else
				return default(T);
		}
	}
}
