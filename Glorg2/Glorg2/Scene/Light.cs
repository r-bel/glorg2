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
