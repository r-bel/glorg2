using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL
{
	public interface IBufferObject
	{
		void MakeCurrent();
		void Reset();
		int Count { get; }
	}
	public interface IVertexBuffer : IBufferObject
	{
	}
	public interface IIndexBuffer : IBufferObject
	{
	}
	public abstract class BufferObject<T> : IDisposable, IBufferObject
		where T : struct
	{
		private uint handle;
		private int size_of_t;
		private int count;
		private T[] internal_array;
		internal List<Action> initialize;
		internal List<Action> reset;
		protected uint target;

		public uint Handle { get { return handle; } }
		public int ElementSize { get { return size_of_t; } }
		public int Count { get { return count; } }
		protected BufferObject()
		{
			count = 0;
			initialize = new List<Action>();
			reset = new List<Action>();
			size_of_t = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
			uint[] buffers = new uint[1] { 0 };
			OpenGL.glGenBuffersARB(1, buffers);
			handle = buffers[0];
		}
		public void Allocate(int num_elements)
		{
			internal_array = new T[num_elements];
			count = internal_array.Length;
		}
		public void Allocate(IEnumerable<T> source)
		{
			internal_array = source.ToArray();
			count = internal_array.Length;
		}
		public void FreeClientData()
		{
			internal_array = new T[] {};
		}
		public T this[int index]
		{
			get
			{
				return internal_array[index];
			}
			set
			{
				internal_array[index] = value;
			}
		}

		public void BufferData(OpenGL.VboUsage usage)
		{
			OpenGL.glBindBufferARB((OpenGL.VboTarget)target, handle);
			var h = System.Runtime.InteropServices.GCHandle.Alloc(internal_array, System.Runtime.InteropServices.GCHandleType.Pinned);
			OpenGL.glBufferDataARB((OpenGL.VboTarget)target, size_of_t * internal_array.Length, h.AddrOfPinnedObject(), usage);
			h.Free();
		}
		public void MakeCurrent()
		{
			OpenGL.glBindBufferARB((OpenGL.VboTarget)target, handle);
			foreach (var act in initialize)
				act();
		}
		public void Reset()
		{
			foreach (var act in reset)
				act();
		}
		protected void Cleanup()
		{
			OpenGL.glDeleteBuffersARB(1, new uint[] { handle });
		}

		public void Dispose()
		{
			Cleanup();
			GC.SuppressFinalize(this);
		}

		~BufferObject()
		{
			Cleanup();
		}
	}

	public enum ElementType
	{
		Ignore =			0x00000000,
		Position =			0x00000001,
		TexCoord =			0x00000002,
		Normals =			0x00000003,
		Color =				0x00000004,
		Weight =			0x00000005,
		Attribute =			0x00000006,
		OneDimension =		0x00000010,
		TwoDimension =		0x00000020,
		ThreeDimension =	0x00000030,
		FourDimension =		0x00000040,
		SixteenDimension =	0x00000100,
		Integer =			0x00001000,
		Float =				0x00002000,
		Bits8 =				0x00010000,
		Bits16 =			0x00020000,
		Bits32 =			0x00040000,
		Bits64 =			0x00080000,
		Channel0 =			0x00000000,
		Channel1 =			0x01000000,
		Channel2 =			0x02000000,
		Channel3 =			0x03000000,
		Channel4 =			0x04000000,
		Matrix = Channel0 | Attribute | SixteenDimension | Float | Bits32
	}

	public sealed class VertexBufferDescriptor
	{
		Type description_type;
		int size;
		ElementType[] types;
		public static int GetElementSize(ElementType t)
		{
			int tt = (int)t;
			int dim = (tt & 0x00000ff0) >> 4;
			int bits = ((tt & 0x00ff0000) >> 16) * 8;
			return dim * bits;
		}
		public VertexBufferDescriptor(IEnumerable<ElementType> types, Type T)
		{
			description_type = T;
			size = System.Runtime.InteropServices.Marshal.SizeOf(T);
			this.types = types.ToArray();
			int total = 0;
			foreach (var item in this.types)
			{
				total += GetElementSize(item);
			}
			if (total != size)
				throw new InvalidOperationException("Elements does not properly describe type T");
		}
		public IEnumerable<ElementType> Types { get { return types; } }
		public Type Type { get { return description_type; } }
		public int TotalSize { get { return size; } }
	}
	/// <summary>
	/// Represent a vertex buffer
	/// </summary>
	/// <typeparam name="T">Vertex element type</typeparam>
	public sealed class VertexBuffer<T> : BufferObject<T>, IVertexBuffer
		where T : struct
	{
		private VertexBufferDescriptor desc;
		/// <summary>
		/// Standard constructor for vertex buffers
		/// </summary>
		/// <param name="desc">VertexBufferDescriptor which describes each element in the structure T</param>
		/// <exception cref="InvalidCastException">Throws InvalidCastException if VertexBufferDescriptor describes anything other that T
		/// </exception>
		public VertexBuffer(VertexBufferDescriptor desc)
			: base()
		{
			target = (uint)OpenGL.VboTarget.GL_ARRAY_BUFFER_ARB;
			if (typeof(T) != desc.Type)
				throw new InvalidCastException("Vertex buffer descriptor does not describe contained type");
			this.desc = desc;
			int offset = 0;
			int tot_size = desc.Types.Count() == 1 ? 0 : desc.TotalSize;
			foreach (var el in desc.Types)
			{
				int tt = (int)el;
				ElementType type;
				int size;
				ElementType data_type;
				data_type = (ElementType)(tt & 0x00003000);
				type = (ElementType)(tt & 0xf);
				int dim = (tt & 0x00000ff0) >> 4;
				int bits = ((tt & 0x00ff0000) >> 16) * 8;
				size = dim * bits;
				
				uint gl_type = 0;

				if(bits == 16 && type == ElementType.Integer)
					gl_type = (uint)OpenGL.DataType.GL_SHORT;
				else if(bits == 32 && type == ElementType.Integer)
					gl_type = (uint)OpenGL.DataType.GL_INT;
				else if(bits == 32 && type == ElementType.Float)
					gl_type = (uint)OpenGL.DataType.GL_FLOAT;
				else if(bits == 64 && type == ElementType.Float)
					gl_type = (uint)OpenGL.DataType.GL_DOUBLE;
				switch (type)
				{
					case ElementType.Position:
						initialize.Add(() =>
						{
							OpenGL.glEnableClientState((uint)OpenGL.VertexArray.GL_VERTEX_ARRAY);
							OpenGL.glVertexPointer(dim, gl_type, tot_size, new IntPtr(offset));
						});
						reset.Add(() =>
						{
							OpenGL.glDisableClientState((uint)OpenGL.VertexArray.GL_VERTEX_ARRAY);
						});
						break;
					case ElementType.Normals:
						initialize.Add(() =>
						{
							OpenGL.glEnableClientState((uint)OpenGL.VertexArray.GL_NORMAL_ARRAY);
							OpenGL.glNormalPointer(gl_type, tot_size, new IntPtr(offset));
						});
						reset.Add(() =>
						{
							OpenGL.glDisableClientState((uint)OpenGL.VertexArray.GL_NORMAL_ARRAY);
						});
						break;
					case ElementType.TexCoord:
						initialize.Add(() =>
						{
							OpenGL.glEnableClientState((uint)OpenGL.VertexArray.GL_TEXTURE_COORD_ARRAY);
							OpenGL.glTexCoordPointer(dim, gl_type, tot_size, new IntPtr(offset));
						});
						reset.Add(() =>
						{
							OpenGL.glDisableClientState((uint)OpenGL.VertexArray.GL_TEXTURE_COORD_ARRAY);
						});
						break;
					case ElementType.Color:
						initialize.Add(() =>
						{
							OpenGL.glEnableClientState((uint)OpenGL.VertexArray.GL_COLOR_ARRAY);
							OpenGL.glColorPointer(dim, gl_type, tot_size, new IntPtr(offset));
						});
						reset.Add(() =>
						{
							OpenGL.glDisableClientState((uint)OpenGL.VertexArray.GL_COLOR_ARRAY);
						});
						break;

				}
				offset += size;
			}
		}
	}
	public sealed class IndexBuffer<T> : BufferObject<T>, IIndexBuffer
		where T : struct
	{
		public IndexBuffer()
			: base()
		{
			target = (uint)OpenGL.VboTarget.GL_ELEMENT_ARRAY_BUFFER_ARB;
		}
	}
}
