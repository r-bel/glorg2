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
	public class GeometryShaderImporter : ResourceImporter
	{
		public override string FileDescriptor
		{
			get { return "gs"; }
		}

		public override int Priority
		{
			get { return 100; }
		}

		internal readonly Type[] supported_types = new Type[] { typeof(GeometryShader), typeof(Shader) };

		public override IEnumerable<Type> SupportedTypes
		{
			get { return supported_types; }
		}

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			System.IO.StreamReader rd = new System.IO.StreamReader(source);
			GeometryShader ret = new GeometryShader(rd.ReadToEnd());
			return ret as T;
		}
	}
	public class FragmentShaderImporter : ResourceImporter
	{
		public override string FileDescriptor
		{
			get { return "fs"; }
		}

		public override int Priority
		{
			get { return 100; }
		}

		internal readonly Type[] supported_types = new Type[] { typeof(FragmentShader), typeof(Shader) };

		public override IEnumerable<Type> SupportedTypes
		{
			get { return supported_types; }
		}

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			System.IO.StreamReader rd = new System.IO.StreamReader(source);
			FragmentShader ret = new FragmentShader(rd.ReadToEnd());
			return ret as T;
		}
	}
}
