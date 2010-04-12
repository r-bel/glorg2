using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	public abstract class Camera
	{
		Matrix projection;
		protected bool invalidated;

		protected abstract Matrix BuildCamera();

		public bool Invalidated
		{
			get { return invalidated; }set {invalidated = value; }
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
