﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Physics
{
	[Serializable()]
	public struct ObjectState
	{
		public Vector4 Value;
		public Vector4 Velocity;
	}
	[Serializable()]
	public struct StateDerivative
	{
		public Vector4 Velocity;
		public Vector4 Acceletaion;

	}

	[Serializable()]
	public struct ObjectStateQuat
	{
		public Quaternion Value;
		public Quaternion Velocity;
	}
	[Serializable()]
	public struct StateDerivativeQuat
	{
		public Quaternion Velocity;
		public Quaternion Acceletaion;

	}

	public static class Integration
	{
		public static StateDerivative EulerEvaluate(ObjectState initial, float t, float dt, StateDerivative der, Func<ObjectState, float, Vector4> acceleration )
		{
			ObjectState state;
			state.Value = initial.Value + der.Velocity * dt;
			state.Velocity = initial.Velocity + der.Acceletaion * dt;

			StateDerivative output;
			output.Velocity = state.Velocity;
			output.Acceletaion = acceleration(state, t + dt);
			return output;
		}
		/// <summary>
		/// Uses Runge-Kutta fourth order integration
		/// 
		/// </summary>
		/// <remarks>Reference: Integration Basics by Glenn Fiedler
		/// http://gafferongames.com/game-physics/integration-basics/</remarks>
		/// <param name="state">Physics state</param>
		/// <param name="t">Time</param>
		/// <param name="dt">Time derived (step)</param>
		/// <param name="acceleration"></param>
		public static void RK4Integrate(ref ObjectState state, float t, float dt, Func<ObjectState, float, Vector4> acceleration)
		{
         StateDerivative a = EulerEvaluate(state, t, 0.0f, new StateDerivative(), acceleration);
         StateDerivative b = EulerEvaluate(state, t + dt * .5f, dt * .5f, a, acceleration);
         StateDerivative c = EulerEvaluate(state, t + dt * .5f, dt * .5f, b, acceleration);
         StateDerivative d = EulerEvaluate(state, t+dt, dt, c, acceleration);

         Vector4 dxdt = 1f / 6 * (a.Velocity + 2 *(b.Velocity + c.Velocity) + d.Velocity);
		 Vector4 dvdt = 1f / 6 * (a.Acceletaion + 2 * (b.Acceletaion + c.Acceletaion) + d.Acceletaion);

         state.Value = state.Value + dxdt * dt;
         state.Velocity = state.Velocity + dvdt * dt;

		}

		public static StateDerivativeQuat EulerEvaluate(ObjectStateQuat initial, float t, float dt, StateDerivativeQuat der, Func<ObjectStateQuat, float, Quaternion> acceleration)
		{
			ObjectStateQuat state;
			state.Value = initial.Value + der.Velocity * dt;
			state.Velocity = initial.Velocity + der.Acceletaion * dt;

			StateDerivativeQuat output;
			output.Velocity = state.Velocity;
			output.Acceletaion = acceleration(state, t + dt);
			return output;
		}
		/// <summary>
		/// Uses Runge-Kutta fourth order integration
		/// 
		/// </summary>
		/// <remarks>Reference: Integration Basics by Glenn Fiedler
		/// http://gafferongames.com/game-physics/integration-basics/</remarks>
		/// <param name="state">Physics state</param>
		/// <param name="t">Time</param>
		/// <param name="dt">Time derived (step)</param>
		/// <param name="acceleration"></param>
		public static void RK4Integrate(ref ObjectStateQuat state, float t, float dt, Func<ObjectStateQuat, float, Quaternion> acceleration)
		{
			StateDerivativeQuat a = EulerEvaluate(state, t, 0.0f, new StateDerivativeQuat(), acceleration);
			StateDerivativeQuat b = EulerEvaluate(state, t + dt * .5f, dt * .5f, a, acceleration);
			StateDerivativeQuat c = EulerEvaluate(state, t + dt * .5f, dt * .5f, b, acceleration);
			StateDerivativeQuat d = EulerEvaluate(state, t + dt, dt, c, acceleration);

			Quaternion dxdt = 1f / 6 * (a.Velocity + 2f * (b.Velocity + c.Velocity) + d.Velocity);
			Quaternion dvdt = 1f / 6 * (a.Acceletaion + 2f * (b.Acceletaion + c.Acceletaion) + d.Acceletaion);

			state.Value = (state.Value + dxdt * dt).Normalize();
			state.Velocity = (state.Velocity + dvdt * dt).Normalize();

		}

	}
}
