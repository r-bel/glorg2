using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
	public class Node : IDisposable
	{
		internal const float dt = .001f;
		[NonSerialized()]
		private float accumulator;
		[NonSerialized()]
		private float interp;
		[NonSerialized()]
		private float sim_time;

		LinkedList<Node> children;
		// Object is marked for deletion
		internal bool delete;

		[NonSerialized()]
		Node parent;

		[NonSerialized()]
		internal Scene owner;

		[NonSerialized()]
		internal volatile bool graphics_initialized;

		[NonSerialized()]
		private List<Node> remove_children;
		[NonSerialized()]
		private List<Node> add_children;

		string name;

		private Guid identifier;

		public Guid Guid { get { return identifier; } }

		internal int hash_code;
		Physics.ObjectState linear_state;
		Physics.ObjectStateQuat angular_state;
		Vector4 acceleration;
		Quaternion angular_acceleration;
		Vector4 center_of_mass;
        Vector3 up;

		[NonSerialized()]
		internal Matrix absolute_transform;
		

		float radius;
		Quaternion angular_momentum;
		Quaternion orientation;
        
		float mass;

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
        /// <summary>
        /// This function defines a linear acceleration function. Override this to implement non-constant accelerations.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="t"></param>
        /// <returns></returns>
		protected virtual Vector4 LinearAcceleratiom(Physics.ObjectState state, float t)
		{
			return acceleration * t;
		}
        /// <summary>
        /// This function defines an angular acceleration. Override this to implement non-constant accelerations.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="t"></param>
        /// <returns></returns>
		protected virtual Quaternion AngularAcceleration(Physics.ObjectStateQuat state, float t)
		{
			//return angular_momentum * t;
			return angular_acceleration * t;
		}

		public virtual void DoDispose()
		{
			
		}

		public Node()
		{
			absolute_transform = Matrix.Identity;
            up = Vector3.Up;
			identifier = Guid.NewGuid();
			angular_momentum = Quaternion.Identity;
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
			accumulator += time;
			// If we are lagging to much, we need to speed up.
			if (accumulator > .2f)
			{
				Physics.Integration.RK4Integrate(ref linear_state, sim_time, accumulator, new Func<Glorg2.Physics.ObjectState, float, Vector4>(LinearAcceleratiom));
				Physics.Integration.RK4Integrate(ref angular_state, sim_time, accumulator, new Func<Glorg2.Physics.ObjectStateQuat, float, Quaternion>(AngularAcceleration));
			}
			else
			{
				while (accumulator >= dt)
				{
					Physics.Integration.RK4Integrate(ref linear_state, sim_time, dt, new Func<Glorg2.Physics.ObjectState, float, Vector4>(LinearAcceleratiom));
					Physics.Integration.RK4Integrate(ref angular_state, sim_time, dt, new Func<Glorg2.Physics.ObjectStateQuat, float, Quaternion>(AngularAcceleration));
					sim_time += dt;
					accumulator -= dt;
				}
				interp = accumulator / dt;
			}
			
		}
		public virtual void InternalProcess(float time)
		{
            Process(time);
			Matrix old = owner.local_transform;
			owner.local_transform = owner.local_transform * GetTransform();
			absolute_transform = owner.local_transform;
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
							children.Remove(child);
							owner.items.Remove(child);
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
							child.owner = owner;
							if (child.parent != this)
								child.parent = this;
							children.AddLast(child);
							var rend = child as IRenderable;
							if (rend != null)
								lock (owner.renderables)
									owner.renderables.AddLast(rend);
							owner.items.Add(child);
						}
					}
				}
				add_children.Clear();
			}
			
		}

		internal virtual void InternalRender(float time, Graphics.GraphicsDevice dev)
		{
			dev.ModelviewMatrix = absolute_transform;
			var r = this as IRenderable;
			if(graphics_initialized && r != null)
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
            foreach (var node in nodes)
            {
                node.owner = owner;
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
            if (!child.graphics_initialized && rend != null)
            {
                child.graphics_initialized = true;
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
            foreach (var node in nodes)
                node.owner = null;

		}
		/// <summary>
		/// Removes a child node from this node
		/// </summary>
		/// <param name="nodes">Node to remove</param>
		/// <remarks>This is not a synchronous function. Children will be removed on the next oppurtunity.</remarks>

		public void Remove(Node child)
		{
			remove_children.Add(child);
            child.owner = null;
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
		public virtual Vector4 Position { get { return linear_state.Value; } set { linear_state.Value = value; } }
		/// <summary>
		/// Gets or sets the orientation of this node
		/// </summary>
		public virtual Quaternion Orientation { get { return angular_state.Value; } set { angular_state.Value = value; } }
		/// <summary>
		/// Gets or sets the velocity of this node
		/// </summary>
		public virtual Vector4 LinearVelocity { get { return linear_state.Velocity; } set { linear_state.Velocity = value; } }
		/// <summary>
		/// Gets or sets the acceleration of this node
		/// </summary>
		public virtual Vector4 ConstLinearAcceleration { get { return acceleration; } set { acceleration = value; } }
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
		public virtual Quaternion AngularVelocity { get { return angular_state.Velocity; } set { angular_state.Velocity = value; } }

		public virtual Quaternion ConstAngularAcceleration { get { return angular_momentum; } set { angular_momentum = value; } }
		/// <summary>
		/// Gets the local matrix transform for this object
		/// </summary>
		/// <returns>A translation+rotation matrix representing position and orientation</returns>
		public virtual Matrix GetTransform()
		{
			var mat = orientation.ToMatrix();
			mat.m14 = linear_state.Value.x;
			mat.m24 = linear_state.Value.y;
			mat.m34 = linear_state.Value.z;
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
					hash_code = Crc32.Hash(value);
				}
				else
					hash_code = 0;
			} 
		}
	}
}
