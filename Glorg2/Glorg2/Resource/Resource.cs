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

namespace Glorg2.Resource
{
	[Serializable()]
	public abstract class Resource : IDisposable
	{
        internal bool handled;
		private string source_name;
		
		
		[NonSerialized()]
		private int hash;
		
		[NonSerialized()]
		private int links;

		public int Links { get { return links; } set { links = value; } }

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
				hash = Hashing.Hash(bytes);
			}
		}
		public void Dispose()
		{
            if (handled)
                --links;
            else
                DoDispose();
		}
		public virtual void DoDispose()
		{
		}
	}
}
