using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Glorg2
{
	/// <summary>
	/// Represents a stream which is confined to an area of another stream.
	/// </summary>
	public class SubStream : Stream
	{
		Stream src;
		long start, len;
		long pos;

		public override bool CanWrite
		{
			get { return false; }
		}

		public override bool CanSeek
		{
			get { return true; }
		}
		public override bool CanRead
		{
			get { return true; }
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		public override void WriteByte(byte value)
		{
			throw new NotImplementedException();
		}
		public override void SetLength(long value)
		{
			len = value;
		}
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch(origin)
			{
				case SeekOrigin.Begin:
					pos = start + offset;
					break;
				case SeekOrigin.Current:
					pos += offset;
					break;
				case SeekOrigin.End:
					pos = len - offset;
					break;
			}
			return src.Seek(offset, origin);
		}

		public override long Length
		{
			get { return len; }
		}

		public override long Position
		{
			get
			{
				return pos;
			}
			set
			{
				pos = value;
			}
		}
		public override void Flush()
		{
			src.Flush();
		}

		public SubStream(Stream source, long start, long len)
		{
			src = source;
			this.start = start;
			this.len = len;
		}
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (src.Position != pos)
				src.Seek(pos, SeekOrigin.Begin);
			if (pos + count > len)
				count += (int)(len - (pos + count));
			int res = src.Read(buffer, offset, count);
			pos += res;
			return res;
		}
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotSupportedException("Asynchronous read is not supported by this stream.");
		}
		public override void Close()
		{
			src.Close();
		}
		protected override void Dispose(bool disposing)
		{
			src.Dispose();
		}
		
		public override int ReadByte()
		{
			if (pos + 1 > len)
				return -1;
			else
				return src.ReadByte();
		}
		
		
	}
}
