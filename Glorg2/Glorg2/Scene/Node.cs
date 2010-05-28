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
using System.ComponentModel;
namespace Glorg2.Scene
{
	[Serializable()]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Name")]
	public class Node : IDisposable
	{


		LinkedList<Node> children;
		// Object is marked for deletion
		internal bool delete;

		[NonSerialized()]
		Node parent;

		[NonSerialized()]
		internal Scene owner;

		[NonSerialized()]
		internal volatile bool graphics_pending;

		[NonSerialized()]
		private List<Node> remove_children;
		[NonSerialized()]
		private List<Node> add_children;

		string name;

		private Guid identifier;

		[Browsable(false)]
		public Guid Guid { get { return identifier; } }

		internal int hash_code;
		Vector3 up;

		[NonSerialized()]
		internal Matrix absolute_transform;

		protected Vector4 position;

		Quaternion orientation;

		[NonSerialized()]
		internal bool in_use;

		NodeReference<Node> target;

		/// <summary>
		/// Defines an object which this node will look at
		/// </summary>
		public Node Target { get { return target.Value; } set { target.Value = value; } }


		/// <summary>
		/// The scene which owns this node
		/// </summary>
		public Scene Owner { get { return owner; } }

		/// <summary>
		/// The up vector for this node. This is used by the PerformLookAt() function.
		/// </summary>
		[DefaultValue("0, 1, 0")]
		public Vector3 Up { get { return up; } set { up = value; } }

		public virtual bool HitTest(Ray ray, out Vector3 pos)
		{
			pos = default(Vector3);
			return false;
		}

		/// <summary>
		/// Makes this nodes orientation look at 'Target'
		/// </summary>
		protected void PerformLookAt()
		{
			if (target.Value != null)
			{
				var mat = Matrix.LookAt(this.Position.ToVector3(), target.Value.Position.ToVector3(), up);
				orientation = mat.ToQuaternion().Normalize();
			}
		}

		internal void InternalPostSerialize()
		{
			var t = GetType();

			var fields = t.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.FlattenHierarchy | System.Reflection.BindingFlags.Instance);
			foreach (var f in fields)
			{

				//if (f.FieldType.IsSubclassOf(typeof(Resource.Resource)))
				//{
				//f.FieldType.InvokeMember("BuildResource", System.Reflection.BindingFlags.Instance, null, f.GetValue(this), new object[] {owner.Resources});
				//}
				//else
				{
					var s = f.FieldType.Name;
					int index = s.IndexOf('`');
					if (index > 0)
					{
						s = s.Substring(0, index);
						if (s == "NodeReference")
						{
							var nd = f.GetValue(this) as INodeReference;
							if (nd != null)
							{
								nd.Owner = owner;
								nd.Update();
							}

						}
					}
				}
			}

			PostSerialize();
		}
		/// <summary>
		/// This functions is called afters serialization, and is used to seet up data which cannot be serialized.
		/// </summary>
		public virtual void PostSerialize()
		{

		}

		public void Dispose()
		{
			foreach (var child in children)
				child.Dispose();
			DoDispose();
			Parent = null;
		}


		public virtual void DoDispose()
		{

		}

		public Node()
		{
			position = new Vector4(0, 0, 0, 1);
			absolute_transform = Matrix.Identity;
			up = Vector3.Up;
			identifier = Guid.NewGuid();
			orientation = Quaternion.Identity;
			name = "";
			children = new LinkedList<Node>();
			remove_children = new List<Node>();
			add_children = new List<Node>();
		}

		protected virtual void Process(float time)
		{
			//position += velocity * time;
			//velocity += acceleration * time;
			PerformLookAt();

		}
		public virtual void InternalProcess(float time)
		{
			in_use = true;
			Process(time);
			Matrix old = owner.local_transform;
			owner.local_transform = owner.local_transform * GetTransform();
			absolute_transform = owner.local_transform;
			if (float.IsNaN(absolute_transform.m11))
				System.Diagnostics.Debugger.Break();
			foreach (var child in children)
				child.InternalProcess(time);
			owner.local_transform = old;

			if (remove_children.Count > 0)
			{
				lock (children)
				{
					lock (owner.items)
					{
						foreach (var child in remove_children)
						{
							if (child.parent == this)
								child.parent = null;
							var rend = child as IRenderable;
							if (rend != null)
								lock (owner.renderables)
									owner.renderables.Remove(rend);
							var ph = child as Physics.IPhysicsObject;
							if (ph != null)
								lock (owner.physics)
									owner.physics.Remove(ph);
							
							children.Remove(child);
							owner.items.Remove(child);
							owner.Owner.ResourceInvoke(new Action(child.Dispose));
						}
					}
				}
				remove_children.Clear();
			}
			if (add_children.Count > 0)
			{
				lock (children)
				{
					lock (owner.items)
					{
						foreach (var child in add_children)
						{
							lock (child.owner)
							{
								child.owner = owner;
								if (child.parent != this)
									child.parent = this;
								children.AddLast(child);
								var rend = child as IRenderable;
								if (rend != null)
									lock (owner.renderables)
										owner.renderables.AddLast(rend);
								var ph = child as Physics.IPhysicsObject;
								if (ph != null)
									lock (owner.physics)
										owner.physics.AddLast(ph);
								owner.items.Add(child);
							}
						}
					}
				}
				add_children.Clear();
			}
			in_use = false;
		}

		internal virtual void InternalRender(float time, Graphics.GraphicsDevice dev)
		{
			dev.ModelviewMatrix = owner.camera_mat * absolute_transform;
			var r = this as IRenderable;
			if (r != null && r.GraphicsInitialized)
				r.Render(time, dev);
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
			int crc = Hashing.Hash(Encoding.Unicode.GetBytes(name));
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

			foreach (var node in nodes)
			{
				Add(node);
			}
		}
		/// <summary>
		/// Adds a child node to this node
		/// </summary>
		/// <param name="child">Child to remove</param>
		/// <remarks>This is not a synchronous function. Children will be added on the next oppurtunity.</remarks>
		public void Add(Node child)
		{
			var rend = child as IRenderable;
			if (!child.graphics_pending && rend != null)
			{
				child.graphics_pending = true;
				owner.Owner.GraphicInvoke(new Action(rend.InitializeGraphics));
			}
			add_children.Add(child);
			child.owner = owner;
		}
		/// <summary>
		/// Removes child nodes from this node
		/// </summary>
		/// <param name="nodes">Nodes to remove</param>
		/// <remarks>This is not a synchronous function. Children will be removed on the next oppurtunity.</remarks>
		public void Remove(IEnumerable<Node> nodes)
		{
			remove_children.AddRange(nodes);
			//foreach (var node in nodes)
				//node.owner = null;

		}
		/// <summary>
		/// Removes a child node from this node
		/// </summary>
		/// <param name="nodes">Node to remove</param>
		/// <remarks>This is not a synchronous function. Children will be removed on the next oppurtunity.</remarks>

		public void Remove(Node child)
		{
			remove_children.Add(child);
			//child.owner = null;
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

		/// <summary>
		/// Gets the local matrix transform for this object
		/// </summary>
		/// <returns>A translation+rotation matrix representing position and orientation</returns>
		public virtual Matrix GetTransform()
		{
			var mat = Orientation.ToMatrix();
			mat.m44 = 1;
			mat = Matrix.Translate(position) * mat;
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
					hash_code = Hashing.Hash(value);
				}
				else
					hash_code = 0;
			}
		}

		public override string ToString()
		{
			return Name + " (" + GetType().Name + ")";
		}
	}
}
