﻿using System;
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
		uint Type { get; }
	}
	public abstract class BufferObject<T> : IDisposable, IBufferObject
		where T : struct
	{
		private uint handle;
		protected int size_of_t;
		private int count;
		protected T[] internal_array;
		protected uint target;

		public uint Handle { get { return handle; } }
		public int ElementSize { get { return size_of_t; } }
		public int Count { get { return count; } }
		protected BufferObject()
		{
			count = 0;
			size_of_t = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
			uint[] buffers = new uint[1] { 0 };
			OpenGL.glGenBuffersARB(1, buffers);
			handle = buffers[0];
		}
		/// <summary>
		/// Increases the buffer by one, and sets the last value
		/// </summary>
		/// <param name="value">Value to add</param>
		/// <remarks>Note that this function is slower than using the Allocate function. If you can calculate beforehand the size of the array, you should.</remarks>
		public void Add(T value)
		{
			
			T[] new_arr = new T[count + 1];
			new_arr[count] = value;
			++count;
			if(internal_array != null)
				internal_array.CopyTo(new_arr, 0);
			internal_array = new_arr;
		}
		/// <summary>
		/// Allocates an array of size num_elements
		/// </summary>
		/// <param name="num_elements"></param>
		/// <remarks>Note that this function does not validate the buffer object. Use BufferData or BufferSubData to store data in the buffer object</remarks>
		public void Allocate(int num_elements)
		{
			internal_array = new T[num_elements];
			count = internal_array.Length;
		}
		/// <summary>
		/// Allocates an array the size of the source parameter, and copies source into it.
		/// </summary>
		/// <param name="source">Source data</param>
		/// <remarks>Note that this function does not validate the buffer object. Use BufferData or BufferSubData to store data in the buffer object</remarks>
		public void Allocate(IEnumerable<T> source)
		{
			internal_array = source.ToArray();
			count = internal_array.Length;
		}
		/// <summary>
		/// Frees the arrays stored on the client.
		/// </summary>
		/// <remarks>Note that calling this function will make BufferSubData and BufferData invalid calls, since the local data has been freed.</remarks>
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
		/// <summary>
		/// Buffers the entire local array to designated storage
		/// </summary>
		/// <remarks>Where the data is stored is implementation specific, and the usage variable is considered a hint, not a command.</remarks>
		/// <param name="usage">Data usage.</param>
		/// <seealso cref="Glorg2.Graphics.OpenGL.OpenGL.VboUsage"/>
		public void BufferData(OpenGL.VboUsage usage)
		{
			
			OpenGL.glBindBufferARB((OpenGL.VboTarget)target, handle);
			var h = System.Runtime.InteropServices.GCHandle.Alloc(internal_array, System.Runtime.InteropServices.GCHandleType.Pinned);
			OpenGL.glBufferDataARB((OpenGL.VboTarget)target, size_of_t * internal_array.Length, h.AddrOfPinnedObject(), usage);
			h.Free();
		}
		/// <summary>
		/// Sets the current vertex buffer object to this
		/// </summary>
		public virtual void MakeCurrent()
		{
			OpenGL.glBindBufferARB((OpenGL.VboTarget)target, handle);
		}
		/// <summary>
		/// Sets the current vertex buffer object as null (disabling vertex buffer objects)
		/// </summary>
		public virtual void Reset()
		{
			OpenGL.glBindBufferARB((OpenGL.VboTarget)target, 0);
		}
		protected void Cleanup()
		{
			if (handle != 0)
			{
				OpenGL.glDeleteBuffersARB(1, new uint[] { handle });
				handle = 0;
			}
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
		/// <summary>
		/// Ignore this entry
		/// </summary>
		Ignore =			0x00000000,
		/// <summary>
		/// Vertex position
		/// </summary>
		Position =			0x00000001,
		/// <summary>
		/// Texture coordinate
		/// </summary>
		TexCoord =			0x00000002,
		/// <summary>
		/// Vertex normal
		/// </summary>
		Normals =			0x00000003,
		/// <summary>
		/// Vertex color
		/// </summary>
		Color =				0x00000004,
		/// <summary>
		/// Vertex weight
		/// </summary>
		Weight =			0x00000005,
		/// <summary>
		/// Vertex attribute
		/// </summary>
		Attribute =			0x00000006,
		/// <summary>
		/// Element is a Scalar
		/// </summary>
		OneDimension =		0x00000010,
		/// <summary>
		/// Element is a two-dimensional vector
		/// </summary>
		TwoDimension =		0x00000020,
		/// <summary>
		/// Element is a three-dimensional vector
		/// </summary>
		ThreeDimension =	0x00000030,
		/// <summary>
		/// Element is a four-dimensional vector
		/// </summary>
		FourDimension =		0x00000040,
		/// <summary>
		/// Element is a sixteen dimensional vector (Matrix)
		/// </summary>
		SixteenDimension =	0x00000100,
		/// <summary>
		/// Element is an integer type
		/// </summary>
		Integer =			0x00001000,
		/// <summary>
		/// Element is an floating point type
		/// </summary>
		Float =				0x00002000,
		/// <summary>
		/// Each unit in the element takes iup 1 byte
		/// </summary>
		Bits8 =				0x00010000,
		/// <summary>
		/// Each unit in the element takes up two bytes (short or half float)
		/// </summary>
		Bits16 =			0x00020000,
		/// <summary>
		/// Each unit in the element takes up four bytes (int or float)
		/// </summary>
		Bits32 =			0x00040000,
		/// <summary>
		/// Each unit in the element takes up eight bytes (double)
		/// </summary>
		Bits64 =			0x00080000,
		/// <summary>
		/// The element is the first channel
		/// </summary>
		Channel0 =			0x00000000,
		/// <summary>
		/// The element is the second channel (used for multi-texturing)
		/// </summary>
		Channel1 =			0x01000000,
		/// <summary>
		/// The element is the third channel (used for multi-texturing)
		/// </summary>
		Channel2 =			0x02000000,
		/// <summary>
		/// The element is the fourth channel (used for multi-texturing)
		/// </summary>
		Channel3 =			0x03000000,
		/// <summary>
		/// The element is the fifth channel (used for multi-texturing)
		/// </summary>
		Channel4 =			0x04000000,
		/// <summary>
		/// Element is a 4x4 attribute matrix. This is a complete definition
		/// </summary>
		Matrix = Channel0 | Attribute | SixteenDimension | Float | Bits32,

		/// <summary>
		/// Element is a position with three-dimensional vector of floats. This is a complete definition
		/// </summary>
		Position3Float = Channel0 | Position | ThreeDimension | Float | Bits32,
		/// <summary>
		/// Element is a normal with three-dimensional vector of floats. This is a complete definition
		/// </summary>
		Normal3Float = Channel0 | Normals | ThreeDimension | Float | Bits32,
		/// <summary>
		/// Element is a texture coordinate woth two-dimensional vector of floats. This is a complete definition
		/// </summary>
		TexCoord2Float = Channel0 | TexCoord | TwoDimension | Float | Bits32,
		/// <summary>
		/// Element is a color with four-dimensional vector of bytes. This is a complete definition
		/// </summary>
		Color4Bytes = Channel0 | Color | FourDimension | Integer | Bits8,
		/// <summary>
		/// Element is a color with four-dimensional vector of floats. This is a complete definition
		/// </summary>
		Color4Floats = Channel0 | Color | FourDimension | Float | Bits32,
		/// <summary>
		/// Element is a color with four-dimensional vector of half floats. This is a complete definition
		/// </summary>
		Color4Halfs = Channel0 | Color | FourDimension | Float | Bits16
	}
	/// <summary>
	/// Describes a vertex structure
	/// </summary>
	public sealed class VertexBufferDescriptor
	{
		Type description_type;
		int size;
		internal ElementType[] types;
		public static int GetElementSize(ElementType t)
		{
			int tt = (int)t;
			int dim = (tt & 0x00000ff0) >> 4;
			int bits = ((tt & 0x00ff0000) >> 16);
			return dim * bits;
		}
		/// <summary>
		/// Build a vertex buffer descriptor
		/// </summary>
		/// <param name="types">Array of element type which describes all the types in each element.</param>
		/// <param name="T">Vertex data to be described</param>
		/// <remarks>Note that types MUST describe ALL elements in T. If not, an exception will be thrown.</remarks>
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
		internal class ElementInfo
		{
			public int dimensions;
			public uint gl_type;
			public IntPtr offset_value;
			public uint channel;
		}
		internal List<ElementInfo> elements;
		internal List<Action<ElementInfo>> initialize;
		internal List<uint> types;

		/// <summary>
		/// Buffers a part of the client data
		/// </summary>
		/// <param name="index">Starting element in the vertex definition</param>
		/// <param name="count">Number of elements from the vertex definition to buffer</param>
		/// <remarks>This function cannot be called once the client data has been freed</remarks>
		public void BufferSubData(int index, int count)
		{
			int offset = 0;
			int size = 0;
			if (internal_array == null || internal_array.Length == 0)
				throw new InvalidOperationException("Cannot buffer data from empty client array");
			for (int i = 0; i < index; i++)
				offset += VertexBufferDescriptor.GetElementSize(desc.types[i]);
			for (int i = index; i < index + count; i++)
				size += VertexBufferDescriptor.GetElementSize(desc.types[i]);

			var ptr = System.Runtime.InteropServices.GCHandle.Alloc(internal_array, System.Runtime.InteropServices.GCHandleType.Pinned);
			OpenGL.glBufferSubDataARB((OpenGL.VboTarget)target, offset, size, ptr.AddrOfPinnedObject());
			ptr.Free();
		}

		private void SetVertexPointer(ElementInfo info)
		{
			OpenGL.glVertexPointer(info.dimensions, info.gl_type, size_of_t, info.offset_value);
		}
		private void SetNormalPointer(ElementInfo info)
		{
			OpenGL.glNormalPointer(info.gl_type, size_of_t, info.offset_value);
		}
		private void SetTexCoordPointer(ElementInfo info)
		{
			OpenGL.glClientActiveTextureARB(info.channel + OpenGL.Const.GL_TEXTURE0);
			OpenGL.glTexCoordPointer(info.dimensions, info.gl_type, size_of_t, info.offset_value);
		}
		private void SetColorPointer(ElementInfo info)
		{
			OpenGL.glColorPointer(info.dimensions, info.gl_type, size_of_t, info.offset_value);
		}
		

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

			initialize = new List<Action<VertexBuffer<T>.ElementInfo>>();
			elements = new List<VertexBuffer<T>.ElementInfo>();
			types = new List<uint>();
			uint gl_datatype = 0;

			

			foreach (var el in desc.Types)
			{
				int tt = (int)el;
				ElementType type;
				int size;
				ElementType data_type;
				data_type = (ElementType)(tt & 0x00003000);
				type = (ElementType)(tt & 0xf);
				int dim = (tt & 0x00000ff0) >> 4;
				int bits = ((tt & 0x00ff0000) >> 16);
				size = dim * bits;

				uint ch = ((uint)el & 0xff000000) >> 24;

				if (bits == 2 && data_type == ElementType.Integer)
					gl_datatype = (uint)OpenGL.Const.GL_SHORT;
				else if (bits == 4 && data_type == ElementType.Integer)
					gl_datatype = (uint)OpenGL.Const.GL_INT;
				else if (bits == 2 && data_type == ElementType.Float)
					gl_datatype = OpenGL.Const.GL_HALF_FLOAT_ARB;
				else if (bits == 4 && data_type == ElementType.Float)
					gl_datatype = (uint)OpenGL.Const.GL_FLOAT;
				else if (bits == 8 && data_type == ElementType.Float)
					gl_datatype = (uint)OpenGL.Const.GL_DOUBLE;
				
					

				elements.Add(new VertexBuffer<T>.ElementInfo()
				{
					dimensions = dim,
					gl_type = gl_datatype,
					offset_value = new IntPtr(offset),
					channel = ch
				});

				switch (type)
				{
					case ElementType.Position:
						initialize.Add(new Action<ElementInfo>(SetVertexPointer));
						types.Add(OpenGL.Const.GL_VERTEX_ARRAY);
						break;
					case ElementType.Normals:
						initialize.Add(new Action<ElementInfo>(SetNormalPointer));
						types.Add(OpenGL.Const.GL_NORMAL_ARRAY);
						break;
					case ElementType.TexCoord:
						initialize.Add(new Action<ElementInfo>(SetTexCoordPointer));
						types.Add(OpenGL.Const.GL_TEXTURE_COORD_ARRAY);
						break;
					case ElementType.Color:
						initialize.Add(new Action<ElementInfo>(SetColorPointer));
						types.Add(OpenGL.Const.GL_COLOR_ARRAY);
						break;
					default:
						initialize.Add(null);
						types.Add(0);
						break;

				}
				offset += size;
			}
		}
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			foreach (var t in types)
				OpenGL.glEnableClientState(t);

			for (int i = 0; i < initialize.Count; i++)
				initialize[i](elements[i]);
		}
		public override void Reset()
		{
			base.Reset();
			foreach (var act in types)
				if (act != 0)
					OpenGL.glDisableClientState(act);
		}
	}
	public sealed class IndexBuffer<T> : BufferObject<T>, IIndexBuffer
		where T : struct
	{
		private uint type;

		public uint Type { get { return type; } }

		public IndexBuffer()
			: base()
		{
			target = (uint)OpenGL.VboTarget.GL_ELEMENT_ARRAY_BUFFER_ARB;
			if (typeof(T) == typeof(byte))
				type = (uint)OpenGL.Const.GL_UNSIGNED_BYTE;
			else if (typeof(T) == typeof(sbyte))
				type = (uint)OpenGL.Const.GL_BYTE;
			else if (typeof(T) == typeof(short))
				type = (uint)OpenGL.Const.GL_SHORT;
			else if (typeof(T) == typeof(ushort))
				type = (uint)OpenGL.Const.GL_UNSIGNED_SHORT;
			else if (typeof(T) == typeof(int))
				type = (uint)OpenGL.Const.GL_INT;
			else if (typeof(T) == typeof(uint))
				type = (uint)OpenGL.Const.GL_UNSIGNED_INT;
			else
			{
				int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
				switch (size)
				{
					case 1:
						type = (uint)OpenGL.Const.GL_UNSIGNED_BYTE;
						break;
					case 2:
						type = (uint)OpenGL.Const.GL_UNSIGNED_SHORT;
						break;
					case 4:
						type = (uint)OpenGL.Const.GL_UNSIGNED_INT;
						break;
					default:
						throw new NotSupportedException("Datatype is not supported");
				}
			}
		}
	}
	public enum IndexType : uint
	{
		UnsignedByte = OpenGL.Const.GL_UNSIGNED_BYTE,
		UnsignedShort = OpenGL.Const.GL_UNSIGNED_SHORT,
		UnsignedInt = OpenGL.Const.GL_UNSIGNED_INT,
		SignedByte = OpenGL.Const.GL_BYTE,
		SignedShort = OpenGL.Const.GL_SHORT,
		SignedInt = OpenGL.Const.GL_INT
	}
}
