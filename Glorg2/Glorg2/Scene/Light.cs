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

		//private static HashSet<uint> Lights = new List<uint>();

		

		private static uint lights;

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

		public Light(Scene owner)
			: base(owner)
		{
			intensity = 1f;
			diffuse = new Vector4(1, 1, 1, 1);
			specular = new Vector4(1, 1, 1, 1);
			enabled = true;
		}

		internal static void DisableAllLights()
		{
			for (int i = 0; i < lights; i++)
				OpenGL.glDisable((uint)(OpenGL.Const.GL_LIGHT0 + i));
			lights = 0;
		}
		private static uint CreateLight()
		{
			return OpenGL.Const.GL_LIGHT0 + lights++;
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

		internal void SetLight()
		{
			if (enabled)
			{
				uint index = CreateLight();
				OpenGL.glEnable(index);
				OpenGL.glLightfv(index, OpenGL.Const.GL_POSITION, new float[] {0, 0, 0, 1});
				OpenGL.glLightfv(index, OpenGL.Const.GL_DIFFUSE, ref diffuse);
				OpenGL.glLightfv(index, OpenGL.Const.GL_SPECULAR, ref diffuse);
				OpenGL.glLightf(index, OpenGL.Const.GL_INTENSITY, intensity);
			}
		}




	}
}
