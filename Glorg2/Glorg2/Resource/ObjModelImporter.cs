using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Glorg2.Graphics;
namespace Glorg2.Resource
{
	public class ModelObjImporter : ResourceImporter
	{
		private static readonly string float_reg = @"-?\d+\.\d+";
		private static readonly Regex vertex_reg = new Regex(string.Format(@"^v\s*(?<X>{0})\s+(?<Y>{0})\s+(?<Z>{0})", float_reg), RegexOptions.Compiled);
		private static readonly Regex tex_reg = new Regex(string.Format(@"^vt\s*(?<X>{0})\s+(?<Y>{0})", float_reg), RegexOptions.Compiled);
		private static readonly Regex norm_reg = new Regex(string.Format(@"^vn\s*(?<X>{0})\s+(?<Y>{0})\s+(?<Z>{0})", float_reg), RegexOptions.Compiled);
		private static readonly Regex mtl_lib = new Regex(@"^mtllib\s+(?<Name>.*)", RegexOptions.Compiled);
		private static readonly Regex mtl_entry = new Regex(@"^usemtl\s+(?<Name>.*)", RegexOptions.Compiled);
		private static readonly Regex obj = new Regex(@"^o\s+(?<Name>.*)", RegexOptions.Compiled);
		private static readonly Regex group = new Regex(@"^g\s+(?<Name>.*)", RegexOptions.Compiled);
		private static readonly Regex face = new Regex(@"^f\s*(?<A1>\d+)/(?<A2>\d+)/(?<A3>\d+)\s+(?<B1>\d+)/(?<B2>\d+)/(?<B3>\d+)\s+(?<C1>\d+)/(?<C2>\d+)/(?<C3>\d+)", RegexOptions.Compiled);
		public override string FileDescriptor
		{
			get { return "model.obj"; }
		}
		private int Emit(List<Vector3> pos, List<Vector2> tex, List<Vector3> norms, List<int> indices, int offset, Model mod)
		{
			int count = 0;
			if (!(pos == null || tex == null || norms == null))
			{
				count = pos.Count;
				if (tex.Count > count)
					count = tex.Count;
				if (norms.Count > count)
					count = norms.Count;
				

				//p.StartVertex = mod.VertexBuffer.Count;
				//p.VertexCount = count;
				for (int i = 0; i < count; i++)
				{
					var v = new VertexPositionTexCoordNormal();
					if (i < pos.Count)
						v.Position = pos[i];
					if (i < tex.Count)
						v.TexCoord = tex[i];
					if (i < norms.Count)
						v.Normal = norms[i];
					mod.VertexBuffer.Add(v);
				}
			}
			if (indices.Count > 0)
			{
				ModelPart p = new ModelPart();
				p.IndexBuffer = new Graphics.OpenGL.IndexBuffer<uint>();
				p.IndexBuffer.Allocate(indices.Count);
				for (int i = 0; i < indices.Count; i++)
				{
					p.IndexBuffer[i] = (uint)(indices[i] + offset);
				}
				p.IndexBuffer.BufferData(Graphics.OpenGL.OpenGL.VboUsage.GL_STATIC_DRAW_ARB);
				//p.IndexBuffer.FreeClientData();
				mod.Parts.Add(p);
			}
			return count;
		}
		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			if (typeof(T) == typeof(Glorg2.Graphics.Model))
			{
				var ret = new Model();
				ret.VertexBuffer = new Graphics.OpenGL.VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
				ret.SourceName = source_name;
				string obj_name = "";
				System.IO.StreamReader rd = new System.IO.StreamReader(source);
				List<Vector3> pos = new List<Vector3>();
				List<Vector2> tex = new List<Vector2>();
				List<Vector3> nrm = new List<Vector3>();
				List<int> indices = new List<int>();
				string ln;
				string mtl = "materials";
				string current_mat = "";
				int vi = 0;
				while ((ln = rd.ReadLine()) != null)
				{
					Match m;
					if((m = vertex_reg.Match(ln)).Success)
					{
						float x = float.Parse(m.Groups["X"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						float y = float.Parse(m.Groups["Y"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						float z = float.Parse(m.Groups["Z"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						pos.Add(new Vector3(x, y, z));
					}
					else if((m = tex_reg.Match(ln)).Success)
					{
						float x = float.Parse(m.Groups["X"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						float y = float.Parse(m.Groups["Y"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						tex.Add(new Vector2(x, y));
					}
					else if ((m = norm_reg.Match(ln)).Success)
					{
						float x = float.Parse(m.Groups["X"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						float y = float.Parse(m.Groups["Y"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						float z = float.Parse(m.Groups["Z"].Value, System.Globalization.NumberFormatInfo.InvariantInfo);
						nrm.Add(new Vector3(x, y, z).Normalize());
					}
					else if ((m = mtl_lib.Match(ln)).Success)
					{
						mtl = m.Groups["Name"].Value;
					}
					else if ((m = mtl_entry.Match(ln)).Success)
					{
						current_mat = mtl + "/" + m.Groups["Name"].Value + ".material";
						
					}
					else if ((m = obj.Match(ln)).Success)
					{
						vi += Emit(pos, tex, nrm, indices, vi, ret);

						pos.Clear();
						tex.Clear();
						nrm.Clear();
						indices.Clear();
					}
					else if ((m = group.Match(ln)).Success)
					{
						Emit(null, null, null, indices, vi, ret);
						indices.Clear();
					}
					else if ((m = face.Match(ln)).Success)
					{
						indices.Add(int.Parse(m.Groups["A1"].Value) - 1);
						indices.Add(int.Parse(m.Groups["B1"].Value) - 1);
						indices.Add(int.Parse(m.Groups["C1"].Value) - 1);

						/*indices.Add(int.Parse(m.Groups["B1"].Value));
						indices.Add(int.Parse(m.Groups["B2"].Value));
						indices.Add(int.Parse(m.Groups["B3"].Value));

						indices.Add(int.Parse(m.Groups["C1"].Value));
						indices.Add(int.Parse(m.Groups["C2"].Value));
						indices.Add(int.Parse(m.Groups["C3"].Value));*/
					}
				}

				if (indices.Count > 0 && pos.Count > 0)
					Emit(pos, tex, nrm, indices, vi, ret);
				else if (indices.Count > 0)
					Emit(null, null, null, indices, vi, ret);

				ret.VertexBuffer.BufferData(Graphics.OpenGL.OpenGL.VboUsage.GL_STATIC_DRAW_ARB);
				//ret.VertexBuffer.FreeClientData();
				return ret as T;
			}
			else
				throw new InvalidCastException();
		}
	}
}
