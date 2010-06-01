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

namespace Glorg2.Scene
{
	[Serializable()]
	public class DynamicNode : Node, Physics.IPhysicsObject
	{
		internal const float dt = .001f;
		[NonSerialized()]
		private float accumulator;
		[NonSerialized()]
		private float interp;
		[NonSerialized()]
		private float sim_time;

		Physics.ObjectState linear_state;
		Physics.ObjectState angular_state;
		Vector4 acceleration;
		Vector4 angular_acceleration;
		Vector4 center_of_mass;


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
		protected virtual Vector4 AngularAcceleration(Physics.ObjectState state, float t)
		{
			//return angular_momentum * t;
			return angular_acceleration * t;
		}

		public virtual Vector4 ConstantLinearAcceleration { get; set; }
		public virtual Vector4 ConstantAngularAcceleration { get; set; }

		public virtual Vector4 LinearVelocity { get { return linear_state.Velocity; } set { linear_state.Velocity = value; } }
		/// <summary>
		/// Gets or sets the acceleration of this node
		/// </summary>
		public virtual Vector4 ConstLinearAcceleration { get { return acceleration; } set { acceleration = value; } }
		/// <summary>
		/// Gets or sets the mass of this node
		/// </summary>
		public virtual float Mass { get; set; }
		/// <summary>
		/// Gets or sets the center of mass for this node
		/// </summary>
		public virtual Vector4 CenterOfMass { get { return center_of_mass; } set { center_of_mass = value; } }
		/// <summary>
		/// Gets or sets the angular momentum for this node
		/// </summary>
		public virtual Vector4 AngularVelocity { get { return angular_state.Velocity; } set { angular_state.Velocity = value; } }

		public virtual Vector4 ConstAngularAcceleration { get { return angular_acceleration; } set { angular_acceleration = value; } }



		public bool IsStatic
		{
			get;
			set;
		}

		public void SimulationStep(float time)
		{
			accumulator += time;
			// If we are lagging to much, we need to speed up.
			//if (accumulator > .2f)
				Physics.Integration.RK4Integrate(ref linear_state, sim_time, time, new Func<Glorg2.Physics.ObjectState, float, Vector4>(LinearAcceleratiom));
				Physics.Integration.RK4Integrate(ref angular_state, sim_time, time, new Func<Glorg2.Physics.ObjectState, float, Vector4>(AngularAcceleration));

			Position += linear_state.Velocity;
			Quaternion spin = .5f * new Quaternion(angular_state.Velocity.x, angular_state.Velocity.y, angular_state.Velocity.z, 0) * Orientation;
			Orientation += spin;
		}

		public void ApplyLocalForce(Vector3 origin, Vector3 direction, float force)
		{
			throw new NotImplementedException();
		}

		public void ApplyLocalForce(Vector3 direction, float force)
		{
			throw new NotImplementedException();
		}

		public void ApplyForce(Vector3 direction, float force)
		{
			throw new NotImplementedException();
		}

		public void ApplyForce(Vector3 origin, Vector3 direction, float force)
		{
			throw new NotImplementedException();
		}

	}
}
