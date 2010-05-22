using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Physics
{
	public interface IPhysicsObject
	{
		/// <summary>
		/// Mass of object in kg
		/// </summary>
		float Mass { get; set; }
		/// <summary>
		/// A static object is an object with infinte mass
		/// </summary>
		bool IsStatic { get; set; }
		/// <summary>
		/// Velocity of object in linear motion (speed)
		/// </summary>
		Vector4 LinearVelocity { get; set; }
		/// <summary>
		/// Velocity of object in angular motion (spin)
		/// </summary>
		Vector4 AngularVelocity { get; set; }

		/// <summary>
		/// Used for objects with constant linear acceleration
		/// </summary>
		Vector4 ConstantLinearAcceleration { get; set; }
		/// <summary>
		/// Used for objects with constant angular acceleration 
		/// </summary>
		Vector4 ConstantAngularAcceleration { get; set; }

		/// <summary>
		/// Applies a force to an object
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="force"></param>
		void ApplyLocalForce(Vector3 origin, Vector3 direction, float force);
		void ApplyLocalForce(Vector3 direction, float force);
		void ApplyForce(Vector3 direction, float force);
		void ApplyForce(Vector3 origin, Vector3 direction, float force);

	}
}
