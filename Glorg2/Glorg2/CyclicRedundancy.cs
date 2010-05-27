
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

namespace Glorg2
{
	/// <summary>
	/// Class supporting simple Cyclic Redundancy Check
	/// </summary>
	/// <remarks>
	/// http://www.cl.cam.ac.uk/research/srg/bluebook/21/crc/node6.html
	/// by Richard Black
	/// </remarks>
	public static class Hashing
	{
		static uint[] table;
		static Hashing()
		{
			// Set up CRC32 tables
			table = new uint[256];


			for (int i = 0; i < 256; i++)
			{
				uint crc = (uint)i << 24;
				for (int j = 0; j < 8; j++)
				{
					if ((crc & 0x80000000) == 0x80000000)
						crc = (crc << 1) ^ 0x04c11db7;
					else
						crc = crc << 1;
				}
				table[i] = crc;
			}
		}

		public static int Hash(string data)
		{

			return Hash(Encoding.Unicode.GetBytes(data));
		}

		public static int Hash(string data, Encoder enc)
		{
			var arr = data.ToCharArray();
			int count = enc.GetByteCount(arr, 0, arr.Length, false);
			var bytes = new byte[count];
			enc.GetBytes(arr, 0, arr.Length, bytes, 0, false);
			return Hash(bytes);
		}

		public static int Hash(byte[] data)
		{
			var hash = System.Security.Cryptography.MD5.Create();
			var ret = hash.ComputeHash(data);
			return BitConverter.ToInt32(ret, 0);

			/*unchecked
			{
				uint result;
				uint i;

				if (data.Length < 4)
					return -1;

				result = (uint)data[0] << 24;
				result |= (uint)data[1] << 16;
				result |= (uint)data[2] << 8;
				result |= data[3];
				result = ~result;

				for (i = 4; i < data.Length; i++)
				{
					result = ((uint)result << 8 | data[i] ^ table[result >> 24]);
				}

				return (int)~result;
			}*/
		}
	}
}
