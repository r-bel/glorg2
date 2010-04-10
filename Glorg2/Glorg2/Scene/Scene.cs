using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
	public class Scene : IDisposable
	{
		Node children;
		
		[NonSerialized()]
		private Game owner;

		public Game Owner { get { return owner; } }

		public Scene(Game owner)
		{
			this.owner = owner;
			children = new Node(this);
			children.Name = "__WorldSpawn";
		}
		public Node ParentNode { get { return children; } }

		public IEnumerable<Node> Find(Predicate<Node> pred)
		{
			return null;
		}

		public void Dispose()
		{
			children.Dispose();
		}
	}
}
