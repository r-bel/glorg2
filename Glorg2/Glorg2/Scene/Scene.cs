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
		internal HashSet<Node> items;
		[NonSerialized()]
		private Game owner;
		private Camera camera;
		private Resource.ResourceManager res;

		[NonSerialized()]
		internal Matrix local_transform;
		

		public Resource.ResourceManager Resources { get { return res; } }

		public Camera Camera { get { return camera; } set { camera = value; } }
		public Game Owner { get { return owner; } }

		public Scene(Game owner)
			: this()
		{
			this.owner = owner;
		}
		public Scene()
		{
			res = new Glorg2.Resource.ResourceManager();
			items = new HashSet<Node>();
			children = new Node(this);
			children.Name = "__WorldSpawn";
		}
		public Node ParentNode { get { return children; } }

		public IEnumerable<Node> Find(Predicate<Node> pred)
		{
			return null;
		}

		public void ToStream(System.IO.Stream dst)
		{
			var fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			fmt.Serialize(dst, this);
			
		}
		protected void InitScene()
		{
			Node n = children;
			foreach (Node d in n.Children)
			{
				InitNode(n);
			}
		}
		protected void InitNode(Node n)
		{
			owner.GraphicInvoke(() => { n.InitializeGraphics(); n.graphics_initialized = true; });
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
