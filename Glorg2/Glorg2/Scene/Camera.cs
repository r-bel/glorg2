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

		public virtual Intersection IsBoxVisible(BoundingBox box)
		{
			return Intersection.Contains;
		}
		public virtual Intersection IsPointVisible(Vector3 point)
		{
			return Intersection.Contains;
		}
		public virtual Intersection IsSphereVisible(BoundingSphere sphere)
		{
			return Intersection.Contains;
		}
		
	}
	[Serializable()]
	public class PerspectiveCamera : Camera
	{
		protected float fov;
		protected float aspect;
		float near;
		float far;

		[Browsable(false)]
		public float FieldOfView { get { return fov; } set { fov = value; Invalidated = true;} }
		[DefaultValue(1f)]
		public float Aspect { get { return aspect; } set { aspect = value; Invalidated = true; } }
		[DefaultValue(.0001f)]
		public float Near { get { return near; } set { near = value; Invalidated = true; } }
		[DefaultValue(1f)]
		public float Far { get { return far; } set { far = value; Invalidated = true; } }

		public override Vector4 Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				base.Position = value;
				ComputeFrustumInfo();
			}
		}
		public override Quaternion Orientation
		{
			get
			{
				return base.Orientation;
			}
			set
			{
				base.Orientation = value;
				ComputeFrustumInfo();
			}
		}

		[DisplayName("Field of view")]
		[DefaultValue(90f)]
		[Description("Sets the field of view in degrees")]
		public float FieldOfViewDegrees
		{
			get { return fov / (float)(Math.PI / 180); }
			set { fov = value * (float)(Math.PI / 180); }
		}

		float tang, near_height, near_width, far_height, far_width;
		Plane[] planes;
		private void ComputeProjectionInfo()
		{
			tang = (float)Math.Tan(fov * 0.5);
			near_height = near * tang;
			near_width = near_height * aspect;
			far_height = far * tang;
			far_width = far_height * aspect;
		}
		private const int TOP = 0;
		private const int BOTTOM = 1;
		private const int LEFT = 2;
		private const int RIGHT = 3;
		private const int FRONT = 4;
		private const int BACK = 5;

		private void ComputeFrustumInfo()
		{
			Vector3 l;
			Vector3 p = position.ToVector3();
			if (Target != null)
				l = Target.Position.ToVector3();
			else
				l = Orientation.ToNormal();

			Vector3 dir, nc, fc, X, Y, Z;

			// compute the Z axis of camera
			// this axis points in the opposite direction from 
			// the looking direction
			Z = (p - l).Normalize();

			// X axis of camera with given "up" vector and Z axis
			X = (Up * Z).Normalize();
			

			// the real "up" vector is the cross product of Z and X
			Y = Z * X;

			// compute the centers of the near and far planes
			nc = p - Z * near;
			fc = p - Z * far;

			// compute the 4 corners of the frustum on the near plane
			Vector3 ntl = nc + Y * near_height - X * near_width;
			Vector3 ntr = nc + Y * near_height + X * near_width;
			Vector3 nbl = nc - Y * near_height - X * near_width;
			Vector3 nbr = nc - Y * near_height + X * near_width;

			// compute the 4 corners of the frustum on the far plane
			Vector3 ftl = fc + Y * far_height - X * far_width;
			Vector3 ftr = fc + Y * far_height + X * far_width;
			Vector3 fbl = fc - Y * far_height - X * far_width;
			Vector3 fbr = fc - Y * far_height + X * far_width;

			// compute the six planes
			// the function set3Points assumes that the points
			// are given in counter clockwise order
			planes[TOP] = Plane.FromPoints(ntr, ntl, ftl);
			planes[BOTTOM] = Plane.FromPoints(nbl, nbr, fbr);
			planes[LEFT] = Plane.FromPoints(ntl, nbl, fbl);
			planes[RIGHT] = Plane.FromPoints(nbr, ntr, fbr);
			planes[BACK] = Plane.FromPoints(ntl, ntr, nbr);
			planes[FRONT] = Plane.FromPoints(ftr, ftl, fbl);
		}

		public override Intersection IsPointVisible(Vector3 p)
		{
			for (int i = 0; i < 6; i++)
				if (planes[i].GetDistance(p) < 0)
					return Intersection.None;
			return Intersection.Contains;
		}

		public override Intersection IsSphereVisible(BoundingSphere sphere)
		{
			float distance;
			Intersection result = Intersection.Contains;

			for (int i = 0; i < 6; i++)
			{
				distance = planes[i].GetDistance(sphere.Position);
				if (distance < -sphere.Radius)
					return Intersection.None;
				else if (distance < sphere.Radius)
					result = Intersection.Intersect;
			}
			return result;
		}

		public override Intersection IsBoxVisible(BoundingBox box)
		{
			Intersection result = Intersection.Contains;
			int outside,inside;
			// for each plane do ...
			for(int i=0; i < 6; i++) {

				// reset counters for corners in and out
				outside=0;inside=0;
				// for each corner of the box do ...
				// get out of the cycle as soon as a box as corners
				// both inside and out of the frustum
				for (int k = 0; k < 8 && (inside==0 || outside==0); k++) {
		
					// is the corner outside or inside
					var pts = box.Points;
					if (planes[i].GetDistance(pts[k]) < 0)
						outside++;
					else
						inside++;
				}
				//if all corners are out
				if (inside == 0)
					return Intersection.None;
				// if some corners are out and others are in	
				else if (outside != 0)
					result = Intersection.Intersect;
			}
			return(result);
		}

		public PerspectiveCamera()
		{
			planes = new Plane[6];
			fov = (float)Math.PI / 2;
			near = .0001f;
			far = 1f;
			aspect = 1f;
		}

		protected override Matrix BuildCamera()
		{
			ComputeProjectionInfo();
			return Matrix.Perspective(fov, aspect, near, far);
		}

	}
}
