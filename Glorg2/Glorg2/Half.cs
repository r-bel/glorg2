using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	// Reference:
	// http://www.fox-toolkit.org/ftp/fasthalffloatconversion.pdf
	// Fast Half Float Conversions
	// Jeroen van der Zijp
	/// <summary>
	/// Represents a 16-bit floating point
	/// </summary>
	/// <remarks>Taken from the document Fast Half Float Conversions by Jeroen van der Zijp</remarks>
	public struct Half
	{
		private static uint[] mantissa_table;
		private static uint[] exponent_table;
		private static uint[] offset_table;

		private static int[] base_table;
		private static int[] shift_table;

		static Half()
		{
			mantissa_table = new uint[2048];
			exponent_table = new uint[64];
			offset_table = new uint[64];

			// Set up mantissa table
			mantissa_table[0] = 0;
			for (uint i = 0; i < 1024; i++)
				mantissa_table[i] = convertmantissa(i);

			for (uint i = 1024; i < 2048; i++)
			{
				mantissa_table[i] = 0x38000000 + ((i - 1024) << 13);
			}

			// Set up exponent table
			exponent_table[0] = 0;
			exponent_table[32]= 0x80000000;

			for(uint i = 1; i < 32; i++)
				exponent_table[i] = i << 23 ;

			for(uint i = 33; i < 63; i++)
				exponent_table[i] = 0x80000000 + (i-32) << 23 ;

			exponent_table[31] = 0x47800000;
			exponent_table[63] = 0xC7800000;

			// Set up offset table
			for (int i = 1; i < offset_table.Length; i++)
				offset_table[i] = 1024;
			
			offset_table[0] = 0;
			offset_table[32]= 0;

			// Set up base table
			base_table = new int[1024];
			// Set up shift table
			shift_table = new int[512];
			int e;
			for (int i = 0; i < 256; ++i)
			{
				e = i - 127;
				if (e < -24)
				{ // Very small numbers map to zero
					base_table[i | 0x000] = 0x0000;
					base_table[i | 0x100] = 0x8000;
					shift_table[i | 0x000] = 24;
					shift_table[i | 0x100] = 24;
				}
				else if (e < -14)
				{ // Small numbers map to denorms
					base_table[i | 0x000] = (0x0400 >> (18 - e));
					base_table[i | 0x100] = (0x0400 >> (18 - e)) | 0x8000;
					shift_table[i | 0x000] = -e - 1;
					shift_table[i | 0x100] = -e - 1;
				}
				else if (e <= 15)
				{ // Normal numbers just lose precision
					base_table[i | 0x000] = ((e + 15) << 10);
					base_table[i | 0x100] = ((e + 15) << 10) | 0x8000;
					shift_table[i | 0x000] = 13;
					shift_table[i | 0x100] = 13;
				}
				else if (e < 128)
				{ // Large numbers map to Infinity
					base_table[i | 0x000] = 0x7C00;
					base_table[i | 0x100] = 0xFC00;
					shift_table[i | 0x000] = 24;
					shift_table[i | 0x100] = 24;
				}
				else
				{ // Infinity and NaN's stay Infinity and NaN's
					base_table[i | 0x000] = 0x7C00;
					base_table[i | 0x100] = 0xFC00;
					shift_table[i | 0x000] = 13;
					shift_table[i | 0x100] = 13;
				}
			}

		}

		static uint convertmantissa(uint i)
		{
			uint m=i<<13; // Zero pad mantissa bits
			uint e=0; // Zero exponent
			while((m&0x00800000) != 0)
			{ // While not normalized
				e -= 0x00800000; // Decrement exponent (1<<23)
				m <<= 1; // Shift mantissa
			}
			m &= 0xFF7FFFFF; // Clear leading 1 bit
			e += 0x38800000; // Adjust bias ((127-14)<<23)
			return m | e; // Return combined number
		}

		private ushort value;

		private Half(ushort val)
		{
			value = val;
		}

		private bool Negative
		{
			get
			{
				return (value & 0x8000) == 0x8000;
			}
			set
			{
				if (value)
					this.value = (ushort)(this.value & 0x7FFFu);
				else
					this.value = (ushort)(this.value | 0x8000u);
			}
		}
		private byte Mantissa
		{
			get
			{
				return (byte)(value & 0x3f);
			}
			set
			{
				this.value = (ushort)((this.value & 0xffc0u) | value);
			}
		}
		private byte Exponent
		{
			get
			{
				return (byte)((this.value & 0x7C00) >> 10);
			}
			set
			{
				this.value = (ushort)((ushort)(this.value & 0x83FFu) | ((ushort)value) << 10);
			}
		}
		private bool IsNormalized
		{
			get
			{
				return Exponent < 31;
			}
		}
		private bool IsSubnominal
		{
			get
			{
				return Exponent == 0 && Mantissa != 0;
			}
		}

		public static readonly Half NegativeInfinity = new Half(0xFC00);
		public static readonly Half PositiveInfinity = new Half(0x7C00);

		public static bool IsNaN(Half value)
		{
			return value.Mantissa != 0 && value.Exponent == 31;
		}

		public static implicit operator float(Half value)
		{
			uint result = mantissa_table[offset_table[value.value >> 10] + (value.value & 0x3ff)] + exponent_table[value.value >> 10];
			var bytes = BitConverter.GetBytes(result);
			return BitConverter.ToSingle(bytes, 0);
		}
		// Explicit operator converting float to half, since half is of lower precision than float (obviously)
		public static explicit operator Half(float value)
		{
			var bytes = BitConverter.GetBytes(value);
			uint f = BitConverter.ToUInt32(bytes, 0);
			return new Half((ushort)(base_table[(f >> 23) & 0x1ff] + ((f & 0x007fffff) >> shift_table[(f >> 23) & 0x1ff])));
		}
		public override string ToString()
		{
			return ((float)this).ToString();
		}

	}
}
