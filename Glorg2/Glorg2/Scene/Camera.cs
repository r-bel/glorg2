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

using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics.OpenGL.Shaders;

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
			get { return invalidated; }
			set { invalidated = value; }
		}

		public Matrix GetProjectionMatrix()
		{
			if (invalidated)
			{
				projection = BuildCamera();
				invalidated = false;
			}
			return projection;
		}

		public virtual Intersection IsVisible(BoundingBox box)
		{
			return Intersection.Contains;
		}
		public virtual Intersection IsVisible(Vector3 point)
		{
			return Intersection.Contains;
		}
		public virtual Intersection IsVisible(BoundingSphere sphere)
		{
			return Intersection.Contains;
		}

	}
	[Serializable()]
	public class PerspectiveCamera : Camera//, IRenderable
	{
		protected float fov;
		protected float aspect;
		float near;
		float far;

		[Browsable(false)]
		public float FieldOfView { get { return fov; } set { fov = value; Invalidated = true; } }
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

		Vector3[] points;
		Vector4[] frustum = new Vector4[6];
		private void ComputeFrustumInfo()
		{
			/*ComputeProjectionInfo();
			Vector3 l;
			Vector3 p = position.ToVector3();
			if (Target != null)
				l = Target.Position.ToVector3();
			else
				l = p + Orientation.ToNormal();

			Vector3 nc, fc, X, Y, Z;

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

			points = new Vector3[] { ntl, ntr, nbr, nbl, ftl, ftr, fbr, fbl };

			// compute the six planes
			// the function set3Points assumes that the points
			// are given in counter clockwise order
			planes[TOP] = Plane.FromPoints(ntr, ntl, ftl);
			planes[BOTTOM] = Plane.FromPoints(nbl, nbr, fbr);
			planes[LEFT] = Plane.FromPoints(ntl, nbl, fbl);
			planes[RIGHT] = Plane.FromPoints(nbr, ntr, fbr);
			planes[BACK] = Plane.FromPoints(ntl, ntr, nbr);
			planes[FRONT] = Plane.FromPoints(ftr, ftl, fbl);*/
			Matrix modl = GetTransform();// = absolute_transform;
			modl.Invert(out modl);
			//absolute_transform.Invert(out modl);
			Matrix clip = GetProjectionMatrix() * modl;
			
			/* Extract the numbers for the RIGHT plane */
			
			frustum[0].x = clip.m14 - clip.m11;
			frustum[0][1] = clip.m24 - clip.m21;
			frustum[0][2] = clip.m34 - clip.m31;
			frustum[0][3] = clip.m44 - clip.m41;

			/* Normalize the result */
			frustum[0] = frustum[0].Normalize();

			/* Extract the numbers for the LEFT plane */
			frustum[1].x = clip.m14 + clip.m11;
			frustum[1].y = clip.m24 + clip.m21;
			frustum[1].z = clip.m34 + clip.m31;
			frustum[1].w = clip.m44 + clip.m41;

			/* Normalize the result */
			frustum[1] = frustum[1].Normalize();

			/* Extract the BOTTOM plane */
			frustum[2].x = clip.m14 + clip.m12;
			frustum[2].y = clip.m24 + clip.m22;
			frustum[2].z = clip.m34 + clip.m32;
			frustum[2].w = clip.m44 + clip.m42;

			/* Normalize the result */
			frustum[2] = frustum[2].Normalize();

			/* Extract the TOP plane */
			frustum[3].x = clip.m14 - clip.m12;
			frustum[3].y = clip.m24 - clip.m22;
			frustum[3].z = clip.m34 - clip.m32;
			frustum[3].w = clip.m44 - clip.m42;

			/* Normalize the result */
			frustum[3] = frustum[3].Normalize();

			/* Extract the FAR plane */
			frustum[4].x = clip.m14 - clip.m13;
			frustum[4].y = clip.m24- clip.m23;
			frustum[4].z = clip.m34 - clip.m33;
			frustum[4].w = clip.m44 - clip.m43;

			/* Normalize the result */
			frustum[4] = frustum[4].Normalize();

			/* Extract the NEAR plane */
			frustum[5].x = clip.m14 + clip.m13;
			frustum[5].y = clip.m24 + clip.m23;
			frustum[5].z = clip.m34 + clip.m33;
			frustum[5].w = clip.m44 + clip.m43;

			/* Normalize the result */
			frustum[5] = frustum[5].Normalize();
		}

		public override Intersection IsVisible(Vector3 p)
		{
			for (int i = 0; i < 6; i++)
				if (frustum[i].x * p.x + frustum[i].y * p.y + frustum[i].z * p.z + frustum[i].w <= 0)
					return Intersection.None;
			return Intersection.Contains;
		}

		public override Intersection IsVisible(BoundingSphere sphere)
		{
			for (int i = 0; i < 6; i++)
				if (frustum[i].x * sphere.Position.x + frustum[i].y * sphere.Position.y + frustum[i].z * sphere.Position.z + frustum[i].w <= -sphere.Radius)
					return  Intersection.None;
			return Intersection.Contains;
		}

		public override Intersection IsVisible(BoundingBox box)
		{
			var size = box.Size / 2;
			// for each plane do ...
			for (int i = 0; i < 6; i++)
			{
				if (frustum[i].x * (box.Position.x - size.x) + frustum[i].y * (box.Position.y - size.y) + frustum[i].z * (box.Position.z - size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x + size.x) + frustum[i].y * (box.Position.y - size.y) + frustum[i].z * (box.Position.z - size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x - size.x) + frustum[i].y * (box.Position.y + size.y) + frustum[i].z * (box.Position.z - size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x + size.x) + frustum[i].y * (box.Position.y + size.y) + frustum[i].z * (box.Position.z - size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x - size.x) + frustum[i].y * (box.Position.y - size.y) + frustum[i].z * (box.Position.z + size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x + size.x) + frustum[i].y * (box.Position.y - size.y) + frustum[i].z * (box.Position.z + size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x - size.x) + frustum[i].y * (box.Position.y + size.y) + frustum[i].z * (box.Position.z + size.z) + frustum[i].w > 0)
					continue;
				if (frustum[i].x * (box.Position.x + size.x) + frustum[i].y * (box.Position.y + size.y) + frustum[i].z * (box.Position.z + size.z) + frustum[i].w > 0)
					continue;
				return Intersection.None;
			}
			return Intersection.Contains;
		}
		public override void DoDispose()
		{
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


		#region IRenderable Members

		VertexBuffer<VertexPositionColor> vb;
		IndexBuffer<uint> ib;
		StdMaterial mat;

		public void Render(float time, Graphics.GraphicsDevice dev)
		{
			dev.SetActiveMaterial(mat);
			dev.SetVertexBuffer(vb);
			dev.SetIndexBuffer(ib);
			dev.Draw(DrawMode.Lines);
			dev.SetActiveMaterial(null);
			dev.SetVertexBuffer(null);
			dev.SetIndexBuffer(null);
		}

		public void InitializeGraphics()
		{
			if(mat == null)
				owner.Resources.Load("shaders\\Grid", out mat);
			if (vb == null)
			{
				vb = new VertexBuffer<VertexPositionColor>(VertexPositionColor.Descriptor);
				vb.Allocate(8);
			}
			if (ib == null)
			{
				ib = new IndexBuffer<uint>();
				ib.Allocate(24);
				ib[0] = 0;
				ib[1] = 1;
				ib[2] = 1;
				ib[3] = 2;
				ib[4] = 2;
				ib[5] = 3;
				ib[6] = 3;
				ib[7] = 0;

				ib[8] = 4;
				ib[9] = 5;
				ib[10] = 5;
				ib[11] = 6;
				ib[12] = 6;
				ib[13] = 7;
				ib[14] = 4;

				ib[15] = 0;
				ib[16] = 4;

				ib[17] = 1;
				ib[18] = 5;

				ib[19] = 2;
				ib[20] = 6;

				ib[21] = 3;
				ib[22] = 7;
				ib.BufferData(VboUsage.GL_STATIC_DRAW);
			}

			if (points != null)
			{
				for (int i = 0; i < 8; i++)
				{
					vb[i] = new VertexPositionColor() { Position = points[i], Color = Colors.Yellow };
				}
			}

			vb.BufferData(VboUsage.GL_DYNAMIC_DRAW);
		}

		public bool GraphicsInitialized
		{
			get { return vb != null && mat != null; }
		}

		public bool GraphicsInvalidated
		{
			get;
			set;
		}

		public int Priority
		{
			get
			{
				return 0;
			}
			set
			{

			}
		}

		#endregion
	}
}
