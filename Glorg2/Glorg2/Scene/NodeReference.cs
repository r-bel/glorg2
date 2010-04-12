using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{

	internal interface INodeReference
	{
		Scene Owner { get; set; }
		void Update();
	}

	public sealed class NodeReference<T>
		where T : Node
	{
		public static readonly NodeReference<T> Empty = new NodeReference<T>();

		[NonSerialized()]
		T node;
		[NonSerialized()]
		internal Scene owner;

		Guid guid;

		public T Value { get { return node; } set { node = value; guid = value.Guid; } }

		public Scene Owner { get { return owner; } set { owner = value; } }

		public NodeReference()
		{
			node = null;
			guid = new Guid();
			owner = null;
		}

		public NodeReference(Scene owner)
		{
			guid = new Guid();
			node = null;
			this.owner = owner;
		}

		public NodeReference(T node)
		{
			guid = node.Guid;
			this.node = node;
			owner = node.owner;
		}

		private void Update()
		{
			Guid g = guid;
			node = (T)owner.items.Find(i => i.Guid == g);
			if (node == null)
				guid = new Guid();
			else
				guid = node.Guid;
		}

	}
}
