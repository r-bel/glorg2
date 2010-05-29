using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
using VB = Glorg2.Graphics.OpenGL.VertexBuffer<Glorg2.Graphics.VertexPositionTexCoordNormal>;
namespace Glorg2.Scene
{
	public class Terrain : Node, IRenderable
	{

		public class TerrainBlock : IDisposable
		{
			List<TerrainBlock> children;
			private BoundingBox bounds;
			internal IndexBuffer<int> ib;

			public BoundingBox Bounds { get { return bounds; } }
			public IEnumerable<TerrainBlock> Children { get { return children; } }

			public void Dispose()
			{
				if (children != null)
					foreach (var child in children)
						child.Dispose();
				if (ib != null)
					ib.Dispose();
			}

			public TerrainBlock(Terrain owner, List<int> indices, BoundingBox box, int subdivision)
			{
				bounds = box;

				if (subdivision < owner.Subdivisions)
				{
					Vector3 new_size = box.Size / 2;
					new_size.y = box.Size.y;
					float offset = box.Size.x / 4;

					children = new List<TerrainBlock>();

					children.Add(new TerrainBlock(owner, indices, new BoundingBox() { Size = new_size, Position = box.Position + new Vector3(-offset, 0, offset) }, subdivision + 1));
					children.Add(new TerrainBlock(owner, indices, new BoundingBox() { Size = new_size, Position = box.Position + new Vector3(offset, 0, offset) }, subdivision + 1));
					children.Add(new TerrainBlock(owner, indices, new BoundingBox() { Size = new_size, Position = box.Position + new Vector3(offset, 0, -offset) }, subdivision + 1));
					children.Add(new TerrainBlock(owner, indices, new BoundingBox() { Size = new_size, Position = box.Position + new Vector3(-offset, 0, -offset) }, subdivision + 1));
					
				}
				else
				{
					List<int> mil = new List<int>();
					for (int i = 0; i < indices.Count; i += 3)
					{
						var i1 = indices[i];
						var i2 = indices[i + 1];
						var i3 = indices[i + 2];

						var v1 = owner.vb[i1];
						var v2 = owner.vb[i2];
						var v3 = owner.vb[i3];

						var p1 = new Vector2(box.Position.x - box.Size.x / 2, box.Position.z - box.Size.z / 2);
						var p2 = new Vector2(box.Position.x + box.Size.x / 2, box.Position.z + box.Size.z / 2);

						if ((v1.Position.x >= p1.x && v1.Position.x <= p2.x && v1.Position.z >= p1.y && v1.Position.z <= p2.y) ||
							(v2.Position.x >= p1.x && v2.Position.x <= p2.x && v2.Position.z >= p1.y && v2.Position.z <= p2.y) ||
							(v3.Position.x >= p1.x && v3.Position.x <= p2.x && v3.Position.z >= p1.y && v3.Position.z <= p2.y))
						{
							mil.Add(i1);
							mil.Add(i2);
							mil.Add(i3);
						}
					}
					ib = new IndexBuffer<int>();
					ib.Allocate(mil);
					ib.BufferData(VboUsage.GL_STATIC_DRAW);
					//mil.Clear();
				}

			}

		}
		private float size;
		string hmap;
		public float CellSize { get; set; }
		public string Heightmap { get { return hmap; } set { hmap = value; } }
		public int BlockSize { get; set; }
		public float Size { get { return size; } }
		public int Subdivisions { get; set; }
		public bool GraphicsInvalidated { get; set; }
		/// <summary>
		/// Number of vertices needed for further subdivision
		/// </summary>
		public int MaxVertices { get; set; }

		public float Height { get; set; }
		
		[NonSerialized()]
		Heightmap heightmap;

		[NonSerialized()]
		TerrainBlock[] blocks;
		[NonSerialized()]
		StdMaterial mat;

		private string mat_name;

		public string MaterialName { get { return mat_name; } set { mat_name = value; } }

		/// <summary>
		/// Retrieves the height of a position at a speceif point on the terrain
		/// </summary>
		/// <param name="x">X position relative to the center of the terrain</param>
		/// <param name="y">Y position relative to the center of the terrain</param>
		/// <returns>Height value at that point relative to the center of the terrain</returns>
		public float Sample(float x, float y)
		{
			x += size / 2;
			y += size / 2;
			x /= CellSize;
			y /= CellSize;
			return heightmap.Sample(x, y) * size;
		}


		public void SetHeightmap(Heightmap map)
		{
			BlockSize = 17;
			heightmap = map;
		}

		public Terrain()
		{
			Subdivisions = 2;
			CellSize = .5f;
			Height = 10f;
		}

		[NonSerialized()]
		VertexBuffer<VertexPositionTexCoordNormal> vb;

		[NonSerialized()]
		bool init_finished;

		#region IRenderable Members

		public void Render(float time, Graphics.GraphicsDevice dev)
		{
			if (mat != null)
				dev.SetActiveMaterial(mat);
			dev.SetVertexBuffer(vb);
			foreach (var block in blocks)
				RenderBlock(block, dev);
			dev.SetVertexBuffer(null);
			dev.SetIndexBuffer(null);
			
		}
		private void RenderBlock(TerrainBlock block, GraphicsDevice dev)
		{
			if (Owner.Camera.IsBoxVisible(block.Bounds) != Intersection.None)
			{
				if (block.ib != null)
				{
					dev.SetIndexBuffer(block.ib);
					dev.Draw(DrawMode.Triangles);
				}
				else
				{
					foreach (var child in block.Children)
					{
						RenderBlock(child, dev);
					}
				}
			}
		}

		public override void DoDispose()
		{
			if (mat != null)
				mat.Dispose();
			if (vb != null)
				vb.Dispose();
			foreach (var child in blocks)
				child.Dispose();
		}

		public virtual void InitializeGraphics()
		{
			if (heightmap == null && string.IsNullOrEmpty(Heightmap))
				return;


			if (heightmap == null)
				owner.Resources.Load(Heightmap, out heightmap);

			if (heightmap.Width != heightmap.Height)
				throw new NotSupportedException("Must be rectangular heightmap");
			int num_vertices = (heightmap.Width + 1) * (heightmap.Height + 1);


			vb = new VertexBuffer<VertexPositionTexCoordNormal>(VertexPositionTexCoordNormal.Descriptor);
			vb.Allocate(num_vertices);

			if (mat_name != null)
				owner.Resources.Load(mat_name, out mat);

			size = CellSize * heightmap.Width;
			float s2 = size / 2;

			int vi = 0;
			float xx = 0;
			float yy = -s2;
			Vector3[,] norms = new Vector3[heightmap.Height, heightmap.Width];
			for (int i = 0; i < heightmap.Height; i++)
			{
				for (int j = 0; j < heightmap.Width; j++)
				{
					Vector3 n = new Vector3();
					n.x = heightmap[i - 1, j] - heightmap[i + 1, j];
					n.y = heightmap[i, j - 1] - heightmap[i, j + 1];
					n.z = 2.0f / (heightmap.Width + 1) + 2.0f / (heightmap.Height + 1);
					norms[i, j] = n.Normalize();
				}
			}

			List<int> indices = new List<int>(6 * heightmap.Width * heightmap.Height);
			for (int i = 0; i < heightmap.Height; i++)
			{
				xx = -s2;
				for (int j = 0; j < heightmap.Width; j++, vi++)
				{
					// Only add indices within the terrain
					if(i < heightmap.Height - 1 && j < heightmap.Width - 1)
					{
						indices.Add(vi);
						indices.Add(vi + heightmap.Width + 1);
						indices.Add(vi + 1);

						indices.Add(vi);
						indices.Add(vi + heightmap.Width);
						indices.Add(vi + heightmap.Width + 1);
					}
					vb[vi] = new VertexPositionTexCoordNormal()
					{
						Position = new Vector3(xx, heightmap[i, j] * Height, yy),
						Normal = norms[i, j]
					};
					xx += CellSize;
				}
				yy += CellSize;
			}

			float offset = s2 / 2;
			
			var new_size = new Vector3(s2, size / 8, s2);
			blocks = new TerrainBlock[4];
			blocks[0] = new TerrainBlock(this, indices, new BoundingBox()
			{
				Size = new_size,
				Position = new Vector3(-offset, 0, offset)
			}, 0);
			blocks[1] = new TerrainBlock(this, indices, new BoundingBox()
			{
				Size = new_size,
				Position = new Vector3(offset, 0, offset)
			}, 0);

			blocks[2] = new TerrainBlock(this, indices, new BoundingBox()
			{
				Size = new_size,
				Position = new Vector3(offset, 0, -offset)
			}, 0);
			blocks[3] = new TerrainBlock(this, indices, new BoundingBox()
			{
				Size = new_size,
				Position = new Vector3(-offset, 0, -offset)
			}, 0);
			indices.Clear();


			vb.BufferData(VboUsage.GL_STATIC_DRAW);
			vb.FreeClientData();
			init_finished = true;
		}

		public bool GraphicsInitialized
		{
			get { return init_finished; }
		}

		#endregion
	}
}
