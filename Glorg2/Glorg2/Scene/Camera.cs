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
	public abstract class Camera : Node
	{
		Matrix projection;
        
		protected bool invalidated;

		protected abstract Matrix BuildCamera();

        public void SetActive()
        {
			owner.camera.Value = this;
        }

		public bool Invalidated
		{
			get { return invalidated; }set { invalidated = value; }
		}

		public  Matrix GetProjectionMatrix()
		{
			if (invalidated)
			{
				projection = BuildCamera();
				invalidated = false;
			}
			return projection;
		}
	}
	[Serializable()]
	public class PerspectiveCamera : Camera
	{
		protected float fov;
		protected float aspect;
		float near;
		float far;

		public float FieldOfView { get { return fov; } set { fov = value; Invalidated = true;} }
		public float Aspect { get { return aspect; } set { aspect = value; Invalidated = true; } }
		public float Near { get { return near; } set { near = value; Invalidated = true; } }
		public float Far { get { return far; } set { far = value; Invalidated = true; } }

		protected override Matrix BuildCamera()
		{
			return Matrix.Perspective(fov, aspect, near, far);
		}

	}
}
