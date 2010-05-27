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
		static readonly Type[] supported_types = new Type[] { typeof(Glorg2.Graphics.Material), typeof(Glorg2.Graphics.StdMaterial) };
		public override IEnumerable<Type> SupportedTypes
		{
			get { return supported_types; }
		}

		public override int Priority
		{
			get { return 50; }
		}
		private static T LoadShader<T>(XmlNode n, ResourceManager man, Program prog)
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
				return Activator.CreateInstance(typeof(T), n.InnerText, prog) as T;
			}
		}
		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			
			XmlDocument doc = new XmlDocument();
			doc.Load(source);
			if (doc.ChildNodes.Count == 2)
			{
				var vsn = doc.SelectSingleNode(".//VertexShader");
				var fsn = doc.SelectSingleNode(".//FragmentShader");
				var gsn = doc.SelectSingleNode(".//GeometryShader");

				T ret = Activator.CreateInstance<T>();

				if (vsn == null)
					return default(T);
				Program prog = new Program();
				var vs = LoadShader<VertexShader>(vsn, man, prog);

				if (gsn != null)
					LoadShader<GeometryShader>(gsn, man, prog);

				if (fsn != null)
					LoadShader<FragmentShader>(fsn, man, prog);
				(ret as Material).Shader = prog;
				if (!prog.Compile())
				{
					StringBuilder err = new StringBuilder();
					err.AppendLine("Compile and link failed for " + source_name);
					foreach (var sh in prog.shaders)
					{
						err.AppendLine(sh.GetType().Name + " " + sh.SourceName + ":");
						err.AppendLine(sh.GetCompileLog());
					}
					err.AppendLine("Linker:\n");
					err.AppendLine(prog.GetLinkLog());
					System.Diagnostics.Debug.WriteLine(err.ToString());
				}
				string log;
				if (!prog.Validate(out log))
				{
					System.Diagnostics.Debug.WriteLine(log);
				}
				
				var uniforms = doc.SelectSingleNode(".//Uniforms");
				if (uniforms != null && uniforms.HasChildNodes)
				{
					int tex_index = 0;
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
									if ((uni = prog.GetUniformType<ScalarFloatUniform, float>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as ScalarFloatUniform).val = float.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "float2":
									if ((uni = prog.GetUniformType<Vector2FloatUniform, Vector2>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as Vector2FloatUniform).val = Vector2.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "float3":
									if ((uni = prog.GetUniformType<Vector3FloatUniform, Vector3>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as Vector3FloatUniform).val = Vector3.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "float4":
									if ((uni = prog.GetUniformType<Vector4FloatUniform, Vector4>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as Vector4FloatUniform).val = Vector4.Parse(val, System.Globalization.NumberFormatInfo.InvariantInfo);
									break;
								case "int":
									if ((uni = prog.GetUniformType<ScalarIntUniform, int>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as ScalarIntUniform).val = int.Parse(val);
									break;
								case "int2":
									if ((uni = prog.GetUniformType<Vector2IntUniform, Vector2Int>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as Vector2FloatUniform).val = Vector2Int.Parse(val);
									break;
								case "int3":
									if ((uni = prog.GetUniformType<Vector3IntUniform, Vector3Int>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as Vector3FloatUniform).val = Vector3Int.Parse(val);
									break;
								case "int4":
									if ((uni = prog.GetUniformType<Vector4IntUniform, Vector4Int>(name)) != null && !string.IsNullOrEmpty(val))
										(uni as Vector4FloatUniform).val = Vector4Int.Parse(val);
									break;
								case "texture2d":
									if ((uni = prog.GetUniformType<TextureUniform, Texture>(name)) != null && !string.IsNullOrEmpty(val) && man != null)
									{
										man.Load(val, out (uni as TextureUniform).val);
										if (uni != null)
										{
											(uni as TextureUniform).TextureIndex = tex_index++;
										}
									}
									break;
							}
							
							if (uni != null)
							{
								(ret as Material).uniforms.Add(uni);
								uni.SetValue();
							}
						}
					}
				}
				(ret as Material).SetupMaterial();
				return ret as T;
			}
			else
				return default(T);
		}
	}
}
