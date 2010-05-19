using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Glorg2.Graphics;

namespace Glorg2.Resource
{
    /// <summary>
    /// Loads .3ds files
    /// Reference: 3D Studio File Format 0.97 - January 1997
    /// Rewritten by Martin van Velsen (email: vvelsen@ronix.ptf.hro.nl )
    ///and Robin Fercoq ( 3ds-bin + mli )(email: robin@msrwww.fc-net.fr)
    /// Based on documentation by Jim Pitts ( email: jim@micronetics.com )
    /// Source update provided by: 
    /// Albert Szilvasy (email: szilvasy@almos.vein.hu )
    /// 
    /// Also
    /// 3D Studio File Format Information
    /// by Jochen Wilhelmy
    /// a.k.a.
    /// digisnap
    /// digisnap@cs.tu-berlin.de
    /// </summary>
	public class Model3dsImporter : ResourceImporter
	{


        public enum ChunkId : ushort
        {
//------ Primary chunk

 MAIN3DS = 0x4D4D,

 //------ Main Chunks

 EDIT3DS = 0x3D3D,  // this is the start of the editor config
 KEYF3DS = 0xB000,  // this is the start of the keyframer config

 MESH_VERSION = 0x3D3E,
 //------ sub defines of EDIT3DS

 EDIT_MATERIAL = 0xAFFF,
 EDIT_CONFIG1 = 0x0100,
 EDIT_CONFIG2 = 0x3E3D,
 EDIT_VIEW_P1 = 0x7012,
 EDIT_VIEW_P2 = 0x7011,
 EDIT_VIEW_P3 = 0x7020,
 EDIT_VIEW1 = 0x7001,
 EDIT_BACKGR = 0x1200,
 EDIT_AMBIENT = 0x2100,
 EDIT_OBJECT = 0x4000,

 EDIT_UNKNW01 = 0x1100,
 EDIT_UNKNW02 = 0x1201,
 EDIT_UNKNW03 = 0x1300,
 EDIT_UNKNW04 = 0x1400,
 EDIT_UNKNW05 = 0x1420,
 EDIT_UNKNW06 = 0x1450,
 EDIT_UNKNW07 = 0x1500,
 EDIT_UNKNW08 = 0x2200,
 EDIT_UNKNW09 = 0x2201,
 EDIT_UNKNW10 = 0x2210,
 EDIT_UNKNW11 = 0x2300,
 EDIT_UNKNW12 = 0x2302, 
 EDIT_UNKNW13 = 0x3000,
 EDIT_UNKNW14 = 0xAFFF,

 //------ sub defines of EDIT_OBJECT
 OBJ_TRIMESH = 0x4100,
 OBJ_LIGHT = 0x4600,
 OBJ_CAMERA = 0x4700,

 OBJ_UNKNWN01 = 0x4010,
 OBJ_UNKNWN02 = 0x4012, //---- Could be shadow

 //------ sub defines of OBJ_CAMERA
 CAM_UNKNWN01 = 0x4710, 
 CAM_UNKNWN02 = 0x4720, 

 //------ sub defines of OBJ_LIGHT
 LIT_OFF = 0x4620,
 LIT_SPOT = 0x4610,
 LIT_UNKNWN01 = 0x465A,

 //------ sub defines of OBJ_TRIMESH
 TRI_VERTEXL = 0x4110,
 TRI_FACEL2 = 0x4111, 
 TRI_FACEL1 = 0x4120,
 TRI_SMOOTH = 0x4150,
 TRI_LOCAL = 0x4160,
 TRI_VISIBLE = 0x4165,

 //------ sub defs of KEYF3DS

 KEYF_UNKNWN01 = 0xB009,
 KEYF_UNKNWN02 = 0xB00A,
 KEYF_FRAMES = 0xB008,
 KEYF_OBJDES = 0xB002,

 /*//------  these define the different color chunk types
 COL_RGB = 0x0010,
 COL_TRU = 0x0011,
 COL_UNK = 0x0013,

 //------ defines for viewport chunks

 TOP = 0x0001,
 BOTTOM = 0x0002,
 LEFT = 0x0003,
 RIGHT = 0x0004,
 FRONT = 0x0005,
 BACK = 0x0006,
 USER = 0x0007,
 CAMERA = 0x0008, // 0xFFFF is the actual code read from file
 LIGHT = 0x0009,
 DISABLED = 0x0010,
 BOGUS = 0x0011*/
        }

        public struct Chunk
        {
            public ChunkId id;
            public uint length;

            public static Chunk FromStream(Stream src)
            {
                Chunk ret = new Chunk();
                BinaryReader rd = new BinaryReader(src);
                ret.id = (ChunkId)ReadShort(src);
                ret.length = ReadInt(src);
                return ret;
            }
        }

        private static string ReadCStr(System.IO.Stream src)
        {
            StringBuilder builder = new StringBuilder();
            int i = 0;
            while ((i = src.ReadByte()) > 0)
                builder.Append((char)i);
            return builder.ToString();
        }

		public override string FileDescriptor
		{
			get { return "model.3ds"; }
		}
        internal static readonly Type[] supported_types = new Type[] { typeof(Model) };
        public override IEnumerable<Type> SupportedTypes
        {
            get { return supported_types ; }
        }

        public override int Priority
        {
            get { return 50; }
        }

        internal static ushort ReadShort(System.IO.Stream src)
        {
            var b1 = (byte)src.ReadByte();
            var b2 = (byte)src.ReadByte();
            return (ushort)(b1 | b2 << 8);
        }

        internal static uint ReadInt(System.IO.Stream src)
        {
            var s1 = ReadShort(src);
            var s2 = ReadShort(src);
            return (uint)(s1 | s2 << 16);
        }

		public override T Import<T>(System.IO.Stream source, string source_name, ResourceManager man)
		{
            var ch = Chunk.FromStream(source);
            if (ch.id != ChunkId.MAIN3DS)
                throw new FormatException("Invalid .3ds format");
            ret = new Model();
            var version = Chunk.FromStream(source);
            if (version.id != (ChunkId)0x0002)
                throw new FormatException("Invalid .3ds format");
            uint ver = ReadInt(source);
            //uint version2 = ReadInt(source);
            ReadChunk(ch, source, 0);
            ret.GenerateNormals();
            ret.VertexBuffer.BufferData(Glorg2.Graphics.OpenGL.VboUsage.GL_STATIC_DRAW);
            return ret as T;
		}

        string current_name;

        List<VertexPositionTexCoordNormal> vertex_list;
        List<uint> index_list;

        uint index_offset;

        Model ret;

        private void ReadVertexList(System.IO.Stream src)
        {
            BinaryReader rd = new BinaryReader(src);
            ushort count = ReadShort(src);
            vertex_list = new List<VertexPositionTexCoordNormal>(count);
            for (int i = 0; i < count; i++)
            {
                float x = rd.ReadSingle(), y = rd.ReadSingle(), z = rd.ReadSingle();
                
                vertex_list.Add(new VertexPositionTexCoordNormal()
                {
                    Position = new Vector3(x, z, y)
                });
            }
        }
        private void ReadIndexList(System.IO.Stream src)
        {
            ushort count = ReadShort(src);
            index_list = new List<uint>(count);
            for (int i = 0; i < count; i++)
            {
                uint a = ReadShort(src) + index_offset;
                uint b = ReadShort(src) + index_offset;
                uint c = ReadShort(src) + index_offset;
                index_list.Add(a);
                index_list.Add(c);
                index_list.Add(b);
                ReadShort(src);
            }
        }

        private void SkipChunk(Stream src)
        {
            Chunk ch = Chunk.FromStream(src);
            src.Seek(ch.length - 6, SeekOrigin.Current);
        }

        private void ReadChunk(Chunk chunk, System.IO.Stream src, long origin)
        {
            long or;
            while(src.Position < chunk.length + origin)
            {
                var new_chunk = Chunk.FromStream(src);
                switch (new_chunk.id)
                {
                    case ChunkId.EDIT3DS:
                        ReadChunk(new_chunk, src, src.Position);
                        break;
                    case ChunkId.MESH_VERSION:
                        uint mesh_version = ReadInt(src);
                        break;
                    case ChunkId.EDIT_OBJECT:
                        or = src.Position;
                        current_name = ReadCStr(src);
                        ReadChunk(new_chunk, src, or);

                        break;
                    case ChunkId.OBJ_TRIMESH:
                        ReadChunk(new_chunk, src, src.Position);

                        break;
                    case ChunkId.TRI_VERTEXL:
                        ReadVertexList(src);
                        break;
                    case ChunkId.TRI_FACEL1:
                        or = src.Position;
                        ReadIndexList(src);
                        SkipChunk(src);
                        SkipChunk(src);
                        if (vertex_list != null)
                        {
                            if (ret.VertexBuffer == null)
                                ret.VertexBuffer = new Glorg2.Graphics.OpenGL.VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
                            ret.VertexBuffer.Add(vertex_list);
                            ModelPart part = new ModelPart();
                            part.Name = current_name;
                            part.StartVertex = (int)index_offset;
                            part.VertexCount = vertex_list.Count;
                            part.IndexBuffer = new Glorg2.Graphics.OpenGL.IndexBuffer<uint>();
                            part.IndexBuffer.Add(index_list);
                            part.IndexBuffer.BufferData(Glorg2.Graphics.OpenGL.VboUsage.GL_STATIC_DRAW);
                            ret.Parts.Add(part);
                            index_offset += (uint)vertex_list.Count;
                            vertex_list = null;
                            index_list = null;
                        }
                        //ReadChunk(Chunk.FromStream(src), src);
                        //ReadChunk(new_chunk, src, or);
                        break;
                    //case ChunkId.TRI_MA
                    default:
                        // Chunk ignored, skip past it.
                        src.Seek(new_chunk.length - 6, SeekOrigin.Current);
                        break;
                }
            }
        }
	}
}
