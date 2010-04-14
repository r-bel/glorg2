using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
	public class Scene : IDisposable, IEnumerable<Node>
	{
		Node children;

		[NonSerialized()]
		internal List<Node> items;
		[NonSerialized()]
		private Game owner;
        [NonSerialized()]
		internal NodeReference<Camera> camera;
		[NonSerialized()]
		private Resource.ResourceManager res;
		[NonSerialized()]
		internal Matrix local_transform;
		[NonSerialized()]
		internal float sim_time;

		public float SimulationTime { get { return sim_time; } }

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
			children = new Node(this);
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
