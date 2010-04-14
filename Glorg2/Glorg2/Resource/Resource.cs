using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Resource
{
	[Serializable()]
	public abstract class Resource
	{
		private string source_name;
		private int hash;

		public override int GetHashCode()
		{
			return hash;
		}

		public override bool Equals(object obj)
		{
			var o = obj as Resource;
			if (o == null)
				return false;
			else
				return o.hash == hash;
		}

		public virtual void BuildResource(ResourceManager manager)
		{
		}

		/// <summary>
		/// Reads resource data from stream. This is used to build resource data which is not serialized
		/// such as texture data or similar.
		/// </summary>
		/// <param name="input">Stream to read from</param>
		protected virtual void StreamRead(System.IO.Stream input)
		{
		}
		/// <summary>
		/// Writes resource data to stream.
		/// </summary>
		/// <param name="output"></param>
		protected virtual void StreamWrite(System.IO.Stream output)
		{
		}

		/// <summary>
		/// Resource input file used to create the resource from source
		/// </summary>
		public string SourceName
		{
			get
			{
				return source_name;
			}
			set
			{
				source_name = value;
				
				var bytes = Encoding.Unicode.GetBytes(value);
				hash = Crc32.Hash(bytes);
			}
		}
	}
}
