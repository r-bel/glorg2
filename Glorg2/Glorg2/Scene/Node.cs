using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
	public class Node : IDisposable, IEnumerable<Node>
	{
		LinkedList<Node> children;
		
		[NonSerialized()]
		Node parent;

		[NonSerialized()]
		Scene owner;

		internal bool graphics_initialized;

		[NonSerialized()]
		private List<Node> remove_children;
		[NonSerialized()]
		private List<Node> add_children;

		string name;
		int hash_code;
		Vector4 position;
		Vector4 center_of_mass;
		Vector4 velocity;
		Vector4 acceleration;
		float radius;
		Quaternion angular_momentum;
		Quaternion orientation;
		float mass;

		public void Dispose()
		{
			foreach (var child in children)
				child.Dispose();
			DoDispose();
			Parent = null;
		}

		protected virtual void InitializeGraphics()
		{

		}

		public virtual void DoDispose()
		{
			
		}

		public Node(Scene owner)
		{
			angular_momentum = Quaternion.Identity;
			orientation = Quaternion.Identity;
			name = "";
			children = new LinkedList<Node>();
			remove_children = new List<Node>();
			add_children = new List<Node>();
			this.owner = owner;
			if (owner.Owner != null)
			{
				owner.Owner.GraphicInvoke(() => { InitializeGraphics(); graphics_initialized = true; });
			}
		}

		protected virtual void Process(float time)
		{
			position += velocity;
			velocity += acceleration;
		}

		public virtual void InternalProcess(float time)
		{
			Process(time);
			foreach (var child in children)
				child.InternalProcess(time);
			if (remove_children.Count > 0)
			{
				lock (children)
				{
					foreach (var child in remove_children)
					{
						if (child.parent == this)
							child.parent = null;
						children.Remove(child);
					}
				}
				remove_children.Clear();
			}
			if (add_children.Count > 0)
			{
				lock (children)
				{
					foreach (var child in add_children)
					{
						if (child.parent != this)
							child.parent = this;
						children.AddLast(child);
					}
				}
				add_children.Clear();
			}
			
		}

		protected virtual void Render(float time, Graphics.GraphicsDevice dev)
		{
		}


		protected internal virtual void InternalRender(float time, Graphics.GraphicsDevice dev)
		{
			var old_mat = dev.ModelviewMatrix;
			var trans = GetTransform();
			dev.ModelviewMatrix = old_mat * trans;
			if(graphics_initialized)
				Render(time, dev);
			foreach (var child in children)
					child.InternalRender(time, dev);
			dev.ModelviewMatrix = old_mat;
		}

		public IEnumerable<Node> Find(Predicate<Node> pred)
		{
			foreach (var node in children)
			{
				if (pred(node))
					yield return node;
				foreach (var n in node.Find(pred))
					yield return n;
			}
		}

		protected IEnumerable<Node> Find(int hash)
		{
			foreach (var node in children)
			{
				if (node.GetHashCode() == hash)
					yield return node;
				foreach (var n in node.Find(hash))
					yield return n;
			}
		}

		public IEnumerable<Node> Find(string name)
		{
			int crc = Crc32.Hash(Encoding.Unicode.GetBytes(name));
			return Find(crc);
		}


		public IEnumerable<Node> Children { get { return children.AsEnumerable(); } }

		/// <summary>
		/// Adds a child node to this node
		/// </summary>
		/// <param name="child">Child to remove</param>
		/// <remarks>This is not a synchronous function. Children will be added on the next oppurtunity.</remarks>
		public void Add(IEnumerable<Node> nodes)
		{
			add_children.AddRange(nodes);
		}

		/// <summary>
		/// Adds a child node to this node
		/// </summary>
		/// <param name="child">Child to remove</param>
		/// <remarks>This is not a synchronous function. Children will be added on the next oppurtunity.</remarks>
		public void Add(Node child)
		{
			add_children.Add(child);
		}
		/// <summary>
		/// Removes child nodes from this node
		/// </summary>
		/// <param name="nodes">Nodes to remove</param>
		/// <remarks>This is not a synchronous function. Children will be removed on the next oppurtunity.</remarks>
		public void Remove(IEnumerable<Node> nodes)
		{
			remove_children.AddRange(nodes);
		}
		/// <summary>
		/// Removes a child node from this node
		/// </summary>
		/// <param name="nodes">Node to remove</param>
		/// <remarks>This is not a synchronous function. Children will be removed on the next oppurtunity.</remarks>

		public void Remove(Node child)
		{
			remove_children.Add(child);
		}

		public override int GetHashCode()
		{
			return hash_code;
		}
		/// <summary>
		/// Gets or sets the parent of this node.
		/// </summary>
		/// <remarks>This is not a synchronous function. Parent will be set on next oppurtunity.</remarks>
		public Node Parent
		{
			get
			{
				return parent;
			}
			set
			{

				if (parent != null)
				{
					parent.Remove(this);
				}
				if (value != null)
				{
					value.Add(this);
				}
			}
		}

		/// <summary>
		/// Gets or sets the position of this node
		/// </summary>
		public virtual Vector4 Position { get { return position; } set { position = value; } }
		/// <summary>
		/// Gets or sets the orientation of this node
		/// </summary>
		public virtual Quaternion Orientation { get { return orientation; } set { orientation = value; } }
		/// <summary>
		/// Gets or sets the velocity of this node
		/// </summary>
		public virtual Vector4 Velocity { get { return velocity; } set { velocity = value; } }
		/// <summary>
		/// Gets or sets the acceleration of this node
		/// </summary>
		public virtual Vector4 Acceleration { get { return acceleration; } set { acceleration = value; } }
		/// <summary>
		/// Gets or sets the mass of this node
		/// </summary>
		public virtual float Mass { get { return mass; } set { mass = value; } }
		/// <summary>
		/// Gets or sets the center of mass for this node
		/// </summary>
		public virtual Vector4 CenterOfMass { get { return center_of_mass; } set { center_of_mass = value; } }
		/// <summary>
		/// Gets or sets the angular momentum for this node
		/// </summary>
		public virtual Quaternion AngularMomentum { get { return angular_momentum; } set { angular_momentum = value; } }

		/// <summary>
		/// Gets the local matrix transform for this object
		/// </summary>
		/// <returns>A translation+rotation matrix representing position and orientation</returns>
		public virtual Matrix GetTransform()
		{
			var mat = orientation.ToMatrix();
			mat.m14 = position.x;
			mat.m24 = position.y;
			mat.m34 = position.z;
			return mat;
		}

		/// <summary>
		/// Gets or sets the name of this object.
		/// </summary>
		/// <remarks>This property is used to identity an object. 
		/// Names must be strings of at least two characters. 
		/// Note that names does not necessarily have to be unique.
		/// This function also generates a hash code which can be obtained from the GetHashCode function</remarks>
		public string Name 
		{ 
			get 
			{ 
				return name; 
			} 
			set 
			{ 
				name = value;
				if (!string.IsNullOrEmpty(value) && value.Length > 2)
				{
					var bytes = Encoding.Unicode.GetBytes(name);
					hash_code = Crc32.Hash(bytes);
				}
				else
					hash_code = 0;
			} 
		}

		#region IEnumerable<Node> Members

		public IEnumerator<Node> GetEnumerator()
		{
			return new NodeEnumerator(this);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new NodeEnumerator(this);
		}

		#endregion

		public class NodeEnumerator : IEnumerator<Node>, System.Collections.IEnumerator
		{
			Node parent;
			Node current;

			public NodeEnumerator(Node n)
			{
				parent = n;
			}

			#region IEnumerator<Node> Members

			public Node Current
			{
				get { return current; }
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return current; }
			}

			public bool MoveNext()
			{
				throw new NotImplementedException();
				
			}

			public void Reset()
			{
				current = null;
			}

			#endregion

			#region IEnumerator Members


			bool System.Collections.IEnumerator.MoveNext()
			{
				return MoveNext();
			}

			void System.Collections.IEnumerator.Reset()
			{
				Reset();
			}

			#endregion
		}


	}

}
