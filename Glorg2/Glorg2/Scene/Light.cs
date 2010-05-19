using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2;
using Glorg2.Graphics;
using Glorg2.Graphics.OpenGL;
namespace Glorg2.Scene
{
	[Serializable()]
	public class Light : Node
	{
		private bool enabled;
		private float inner_radius;
		private float outer_radius;
		private float intensity;
		private Vector4 diffuse;
		private Vector4 specular;

		public float InnerRadius { get { return inner_radius; } set { inner_radius = value; } }
		public float OuterRadius { get { return outer_radius; } set { outer_radius = value; } }
		public float Intensity { get { return intensity; } set { intensity = value; } }
		public Vector4 Diffuse { get { return diffuse; } set { diffuse = value; } }
		public Vector4 Specular { get { return specular; } set { specular = value; } }

		public Light()
			: base()
		{
			intensity = 1f;
			diffuse = new Vector4(1, 1, 1, 1);
			specular = new Vector4(1, 1, 1, 1);
			enabled = true;
		}

		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

	}
}
