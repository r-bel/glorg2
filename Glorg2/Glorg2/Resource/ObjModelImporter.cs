﻿/*
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
		private static readonly Regex face = new Regex(@"^f\s*(?<A1>\d+)/(?<A2>\d+)/(?<A3>\d+)\s+(?<B1>\d*)/(?<B2>\d+)/(?<B3>\d+)\s+(?<C1>\d+)/(?<C2>\d+)/(?<C3>\d+)", RegexOptions.Compiled);
		public override string FileDescriptor
		{
			get { return "model.obj"; }
		}
        internal static readonly Type[] supported_types = new Type[] { typeof(Model) };

        public override IEnumerable<Type> SupportedTypes
        {
            get { return supported_types; }
        }

        public override int Priority
        {
            get { return 75; }
        }

		private List<VertexPositionTexCoordNormal> verts;
		
		private struct Tuple
		{
			public int v, n, t;
			public int result;
			public bool CompareTo(Tuple o)
			{
				return v == o.v && n == o.n && t == o.t;
			}
		}
		List<Tuple> unique;
		private int Emit(List<Vector3> pos, List<Vector2> tex, List<Vector3> norms, List<Tuple> ind, int offset, Model mod, string name)
		{
			int count = 0;

			
			List<uint> indices = new List<uint>();
			for (int i = 0; i < ind.Count; i++)
			{
				int j = unique.FindIndex(item => item.CompareTo(ind[i]));
				if (j >= 0)
					indices.Add((uint)unique[j].result);
				else
				{
					++count;
					Tuple t = ind[i];
					t.result = verts.Count;
					unique.Add(t);
					indices.Add((uint)t.result);
					verts.Add(new VertexPositionTexCoordNormal()
					{
						Position = pos[t.v],
						Normal = norms[t.n],
						TexCoord = tex[t.t]
					});
				}
			}
			ModelPart part = new ModelPart()
			{
				IndexBuffer = new Graphics.OpenGL.IndexBuffer<uint>(),
				Name = name
			};
			part.IndexBuffer.Add(indices);
			part.IndexBuffer.BufferData(Graphics.OpenGL.VboUsage.GL_STATIC_DRAW);
			mod.Parts.Add(part);
			//mod.VertexBuffer.Add(verts);
			return count;
		}
		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			if (typeof(T) == typeof(Glorg2.Graphics.Model))
			{
				verts = new List<VertexPositionTexCoordNormal>();
				Vector3 min = new Vector3();
				Vector3 max = new Vector3();
				Vector3 mid = new Vector3();
				unique = new List<Tuple>();
				var ret = new Model();
				ret.VertexBuffer = new Graphics.OpenGL.VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
				ret.SourceName = source_name;
				string name = "default";
				System.IO.StreamReader rd = new System.IO.StreamReader(source);
				List<Vector3> pos = new List<Vector3>();
				List<Vector2> tex = new List<Vector2>();
				List<Vector3> nrm = new List<Vector3>();
				List<Tuple> indices = new List<Tuple>();
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
						//var v = vb[i].Position;
						if (x < min.x)
							min.x = x;
						if (y < min.y)
							min.y = y;
						if (z < min.z)
							min.z = z;

						if (x > max.x)
							max.x = x;
						if (y > max.y)
							max.y = y;
						if (z > max.z)
							max.z = z;
						mid += new Vector3(x, y, z);

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
						if (indices.Count > 0)
						{
							vi += Emit(pos, tex, nrm, indices, vi, ret, name);
							indices.Clear();
						}
						name = m.Groups["Name"].Value;
					}
					else if ((m = group.Match(ln)).Success)
					{
						if (indices.Count > 0)
						{
							vi += Emit(pos, tex, nrm, indices, vi, ret, name);
							indices.Clear();
						}
						name = m.Groups["Name"].Value;
					}
					else if ((m = face.Match(ln)).Success)
					{
						indices.Add(new Tuple() 
						{ 
							v = int.Parse(m.Groups["A1"].Value) - 1,
							t = int.Parse(m.Groups["A2"].Value) - 1,
							n = int.Parse(m.Groups["A3"].Value) - 1
						});

						indices.Add(new Tuple() 
						{ 
							v = int.Parse(m.Groups["B1"].Value) - 1,
							t = int.Parse(m.Groups["B2"].Value) - 1,
							n = int.Parse(m.Groups["B3"].Value) - 1
						});
						indices.Add(new Tuple() 
						{ 
							v = int.Parse(m.Groups["C1"].Value) - 1,
							t = int.Parse(m.Groups["C2"].Value) - 1,
							n = int.Parse(m.Groups["C3"].Value) - 1
						});
					}
				}

				if (indices.Count > 0)
					Emit(pos, tex, nrm, indices, vi, ret, name);

				ret.VertexBuffer.Add(verts);
				ret.VertexBuffer.BufferData(Graphics.OpenGL.VboUsage.GL_STATIC_DRAW);
				verts = null;
				indices = null;
				unique = null;
				ret.bounds = new BoundingBox()
				{
					Position = mid / ret.VertexBuffer.Count,
					Size = (max - min) / 2
				};

				ret.VertexBuffer.FreeClientData();
				return ret as T;
			}
			else
				throw new InvalidCastException();
		}
	}
}
