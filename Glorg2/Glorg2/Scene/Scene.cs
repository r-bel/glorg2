using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
	public class Scene : IDisposable, IEnumerable<Node>
	{
		WorldSpawn children;

		[NonSerialized()]
		internal List<Node> items;
		[NonSerialized()]
		private Game owner;
        
		internal NodeReference<Camera> camera;
		[NonSerialized()]
		private Resource.ResourceManager res;
		[NonSerialized()]
		internal Matrix local_transform;
		[NonSerialized()]
		internal float sim_time;

		private Vector4 background;
		public Vector4 Background { get { return background; } set { background = value; } }

		public float SimulationTime { get { return sim_time; } }

		public Vector3 gravity;
		public Vector3 Gravity { get { return gravity; } set { gravity = value; } }


		public Resource.ResourceManager Resources { get { return res; } }

		public Camera Camera { get { return camera.Value; } set { camera.Value = value; } }
		public Game Owner { get { return owner; } }

		public Scene(Game owner)
			: this()
		{
			this.owner = owner;
		}
		public Scene()
		{
			res = new Glorg2.Resource.ResourceManager();
			items = new List<Node>();
			camera = new NodeReference<Camera>();
			children = new WorldSpawn();
            children.owner = this;
			children.Name = "__WorldSpawn";
		}
		public Node ParentNode { get { return children; } }

		public IEnumerable<Node> Find(string name)
		{
			int crc = Crc32.Hash(name);
			return items.FindAll(i => i.hash_code == crc);
		}

		public void ToStream(System.IO.Stream dst)
		{
			var fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			fmt.Serialize(dst, this);
		}
		protected void InitScene()
		{
			AddItem(children);
			InitNode(children);
		}

		private void AddItem(Node n)
		{
			items.Add(n);
			foreach (var child in n.Children)
				AddItem(child);
		}
		public IEnumerable<Node> HitTest(Ray ray)
		{
			foreach (var n in items)
			{
				Vector3 ret = new Vector3();
				if (n.HitTest(ray, out ret))
				{
					yield return n;
				}
			}
		}

		/// <summary>
		/// Unproject a viewport coordinate to world coordinates
		/// </summary>
		/// <param name="pos">Viewport coordinate to unproject</param>
		/// <param name="z">Z depth for resulting coordinate</param>
		/// <returns>Vector representing the point in 3D world space</returns>
		public Vector3 Unproject(Vector2 pos, float z)
		
		{
			if (this.owner.Device != null)
			{

				var worldview = camera.Value.GetCameraTransform();
				var proj = camera.Value.GetProjectionMatrix();
				var vp = this.owner.Device.Viewport;
				var p = (worldview * proj).Invert() * new Vector3(
					(2 * (pos.x - vp.X) - 1) / vp.Width,
					(2 * (pos.y - vp.Y) - 1) / vp.Height,
					2 * z - 1);
				return p;
			}
			else
				return default(Vector3);
		}

		public Vector2 Project(Vector3 pos)
		{
			return default(Vector2);
		}

		public Node HitTest(Ray ray, out Vector3 pos)
		{
			Node current = null;
			
			Vector3 closest = ray.Origin + ray.Normal;
			float cl = closest.Length;
			foreach (var n in items)
			{
				Vector3 ret = new Vector3();
				if (n.HitTest(ray, out ret))
				{
					if (ret.Length < closest.Length)
					{
						cl = ret.Length;
						closest = ret;
					}
				}
			}
			pos = closest;
			return current;
		}
		protected void InitNode(Node n)
		{
			owner.GraphicInvoke(() => { n.InitializeGraphics(); n.graphics_initialized = true; });
			n.InternalPostSerialize();
			foreach (var node in n.Children)
			{
				InitNode(node);
			}
		}
		public static Scene FromStream(System.IO.Stream src, Game owner)
		{
			var fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			Scene res = fmt.Deserialize(src) as Scene;
			res.owner = owner;
			res.InitScene();
			return res;
		}
		public void GraphicsDispose()
		{
			res.Dispose();
			ParentNode.Dispose();
		}
		public void Dispose()
		{
			children.Dispose();
		}

		#region IEnumerable<Node> Members

		public IEnumerator<Node> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator() as System.Collections.IEnumerator;
		}

		#endregion
	}
}
