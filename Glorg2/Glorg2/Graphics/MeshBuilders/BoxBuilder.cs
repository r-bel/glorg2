using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
namespace Glorg2.Graphics.MeshBuilders
{
    public class BoxBuilder : MeshBuilder
    {
        private float width;
        private float height;
        private float depth;
        public float Width { get { return width; } set { width = value; } }
        public float Height { get { return height; } set { height = value; } }
        public float Depth { get { return depth; } set { depth = value; } }

        private int AddFaces(int vertex_offset, int index_offset, IndexBuffer<uint> ib)
        {
            ib[index_offset++] = (uint)vertex_offset;
            ib[index_offset++] = (uint)vertex_offset + 1;
            ib[index_offset++] = (uint)vertex_offset + 3;

            ib[index_offset++] = (uint)vertex_offset + 1;
            ib[index_offset++] = (uint)vertex_offset + 2;
            ib[index_offset++] = (uint)vertex_offset + 3;
            return index_offset;

        }

        public override Model Build()
        {
            Model ret = new Model();

            var w2 = width / 2;
            var h2 = height / 2;
            var d2 = depth / 2;

            ret.VertexBuffer = new VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
            ret.VertexBuffer.Allocate(24);
            var vb = ret.VertexBuffer;
            var ib = new IndexBuffer<uint>();
            //ib.Allocate(36);

            Vector3[] pos = new Vector3[8];
            pos[0] = new Vector3(-w2, h2, d2);
            pos[1] = new Vector3(w2, h2, d2);
            pos[2] = new Vector3(w2, h2, -d2);
            pos[3] = new Vector3(-w2, h2, -d2);

            pos[4] = new Vector3(-w2, -h2, d2);
            pos[5] = new Vector3(w2, -h2, d2);
            pos[6] = new Vector3(w2, -h2, -d2);
            pos[7] = new Vector3(-w2, -h2, -d2);


            ret.Parts.Add(
                new ModelPart()
                {
                    IndexBuffer = ib
                }
                );

            vb[0] = new VertexPositionTexCoordNormal()
            {
                Position = pos[0],
                Normal = Vector3.Up,
                TexCoord = new Vector2(0, 0)
            };

            vb[1] = new VertexPositionTexCoordNormal()
            {
                Position = pos[1],
                Normal = Vector3.Up,
                TexCoord = new Vector2(1, 0)
            };

            vb[2] = new VertexPositionTexCoordNormal()
            {
                Position = pos[2],
                Normal = Vector3.Up,
                TexCoord = new Vector2(1, 1)
            };
            vb[3] = new VertexPositionTexCoordNormal()
            {
                Position = pos[3],
                Normal = Vector3.Up,
                TexCoord = new Vector2(0, 1)
            };

            vb[4] = new VertexPositionTexCoordNormal()
            {
                Position = pos[7],
                Normal = Vector3.Down,
                TexCoord = new Vector2(0, 0)
            };

            vb[5] = new VertexPositionTexCoordNormal()
            {
                Position = pos[6],
                Normal = Vector3.Down,
                TexCoord = new Vector2(1, 0)
            };

            vb[6] = new VertexPositionTexCoordNormal()
            {
                Position = pos[5],
                Normal = Vector3.Down,
                TexCoord = new Vector2(1, 1)
            };
            vb[7] = new VertexPositionTexCoordNormal()
            {
                Position = pos[4],
                Normal = Vector3.Down,
                TexCoord = new Vector2(0, 1)
            };

            vb[8] = new VertexPositionTexCoordNormal()
            {
                Position = pos[4],
                Normal = Vector3.North,
                TexCoord = new Vector2(0, 0)
            };

            vb[9] = new VertexPositionTexCoordNormal()
            {
                Position = pos[5],
                Normal = Vector3.North,
                TexCoord = new Vector2(0, 0)
            };

            vb[10] = new VertexPositionTexCoordNormal()
            {
                Position = pos[1],
                Normal = Vector3.North,
                TexCoord = new Vector2(0, 0)
            };

            vb[11] = new VertexPositionTexCoordNormal()
            {
                Position = pos[0],
                Normal = Vector3.North,
                TexCoord = new Vector2(0, 0)
            };

            vb[12] = new VertexPositionTexCoordNormal()
            {
                Position = pos[0],
                Normal = Vector3.West,
                TexCoord = new Vector2()
            };

            vb[13] = new VertexPositionTexCoordNormal()
            {
                Position = pos[3],
                Normal = Vector3.West,
                TexCoord = new Vector2()
            };


            vb[14] = new VertexPositionTexCoordNormal()
            {
                Position = pos[7],
                Normal = Vector3.West,
                TexCoord = new Vector2()
            };

            vb[15] = new VertexPositionTexCoordNormal()
            {
                Position = pos[4],
                Normal = Vector3.West,
                TexCoord = new Vector2()
            };

            vb[16] = new VertexPositionTexCoordNormal()
            {
                Position = pos[3],
                Normal = Vector3.South,
                TexCoord = new Vector2()
            };

            vb[17] = new VertexPositionTexCoordNormal()
            {
                Position = pos[2],
                Normal = Vector3.South,
                TexCoord = new Vector2()
            };

            vb[18] = new VertexPositionTexCoordNormal()
            {
                Position = pos[6],
                Normal = Vector3.South,
                TexCoord = new Vector2()
            };
            vb[19] = new VertexPositionTexCoordNormal()
            {
                Position = pos[7],
                Normal = Vector3.South,
                TexCoord = new Vector2()
            };

            vb[23] = new VertexPositionTexCoordNormal()
            {
                Position = pos[1],
                Normal = Vector3.East,
                TexCoord = new Vector2()
            };

            vb[22] = new VertexPositionTexCoordNormal()
            {
                Position = pos[2],
                Normal = Vector3.East,
                TexCoord = new Vector2()
            };

            vb[21] = new VertexPositionTexCoordNormal()
            {
                Position = pos[6],
                Normal = Vector3.East,
                TexCoord = new Vector2()
            };
            vb[20] = new VertexPositionTexCoordNormal()
            {
                Position = pos[5],
                Normal = Vector3.East,
                TexCoord = new Vector2()
            };

            int offset = 0;
            ib.Allocate(36);
            for (int i = 0; i < 6; i++)
            {
                offset = AddFaces(i * 4, offset, ib);
            }

            vb.BufferData(Glorg2.Graphics.OpenGL.OpenGL.VboUsage.GL_STATIC_DRAW_ARB);
            ib.BufferData(Glorg2.Graphics.OpenGL.OpenGL.VboUsage.GL_STATIC_DRAW_ARB);

            return ret;
            
        }
    }
}
