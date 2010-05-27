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
		/// <param name="acceleration">Acceleration function</param>
		public static void RK4Integrate(ref ObjectState state, float t, float dt, Func<ObjectState, float, Vector4> acceleration)
		{
         StateDerivative a = EulerEvaluate(state, t, 0.0f, new StateDerivative(), acceleration);
         StateDerivative b = EulerEvaluate(state, t + dt * .5f, dt * .5f, a, acceleration);
         StateDerivative c = EulerEvaluate(state, t + dt * .5f, dt * .5f, b, acceleration);
         StateDerivative d = EulerEvaluate(state, t + dt, dt, c, acceleration);

         Vector4 dxdt = 1f / 6 * (a.Velocity + 2 *(b.Velocity + c.Velocity) + d.Velocity);
		 Vector4 dvdt = 1f / 6 * (a.Acceletaion + 2 * (b.Acceletaion + c.Acceletaion) + d.Acceletaion);

         state.Value = state.Value + dxdt * dt;
         state.Velocity = state.Velocity + dvdt * dt;

		}

		public static float RK4(float x, float y, float dt, Func<float, float, float> f)
		{
			float K1 = (dt * f(x, y));
			float K2 = (dt * f((x + .5f * dt), (y + .5f * K1)));
			float K3 = (dt * f((x + .5f * dt), (y + .5f * K2)));
			float K4 = (dt * f((x + dt), (y + K3)));
			return (y + ((K1 + 2 * K2 + 2 * K3 + K4) / 6));
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

			state.Value = state.Value + dxdt * dt;
			state.Velocity = state.Velocity + dvdt * dt;

		}

	}
}
