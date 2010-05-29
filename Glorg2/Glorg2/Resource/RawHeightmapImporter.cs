using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics;
namespace Glorg2.Resource
{
	public class RawHeightmapImporter : ResourceImporter
	{

		public override string FileDescriptor
		{
			get { return "raw"; }
		}
		private static readonly Type[] supported_types = new Type[] { typeof(Heightmap) };
		public override IEnumerable<Type> SupportedTypes
		{
			get { return supported_types; }
		}

		public override int Priority
		{
			get { return 100; }
		}

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
			int count = (int)source.Length >> 1;
			int size = (int)Math.Sqrt(count);
			if (size * size != count)
				throw new NotSupportedException("File format not supported or malformed.");
			Heightmap result = new Heightmap(size, size);
			System.IO.BinaryReader rd = new System.IO.BinaryReader(source);
			for (int i = 0; i < count; i++)
			{
				var value = rd.ReadUInt16();
				int v = value - (UInt16.MaxValue >> 1);
				result[i] = ((float)v) / (UInt16.MaxValue >> 1);
			}
			return result as T;
		}
	}
}
