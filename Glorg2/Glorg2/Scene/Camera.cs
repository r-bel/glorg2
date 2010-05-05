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

		public virtual Matrix GetCameraTransform()
		{
			var or = Orientation;
			//or.x = or.x * -1;
			//or.y = or.y * -1;
			//or.z = or.z * -1;
            or.w = -or.w;
			var mat = or.ToMatrix();
            var trans = Matrix.Translate(Position);
			//mat.Translation = new Vector4(-Position.x, -Position.y, -Position.z);
            return mat * trans;
			//return mat;
		}

        public void SetActive()
        {
			owner.camera.Value = this;
        }

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
