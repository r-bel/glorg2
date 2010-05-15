using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glorg2.Graphics.OpenGL;
using Glorg2.Graphics.OpenGL.Shaders;

namespace Glorg2.Graphics
{
	public class StdMaterial : Material, IStdShader
	{
		MatrixUniform projection;
		MatrixUniform modelview;
		MatrixUniform texture;

		int pos_attrib;
		int norm_attrib;
		int color0_attrib;
		int color1_attrib;
		int color2_attrib;
		int color3_attrib;

		int texcoord0_attrib;
		int texcoord1_attrib;
		int texcoord2_attrib;
		int texcoord3_attrib;


		public override void SetupMaterial()
		{
			MakeCurrent();
			projection = Shader.GetUniformType<MatrixUniform, Matrix>("projection_mat");
			modelview = Shader.GetUniformType<MatrixUniform, Matrix>("modelview_mat");
			texture = Shader.GetUniformType<MatrixUniform, Matrix>("texture_mat");

			pos_attrib = Shader.GetAttributeLocation("in_position");
			norm_attrib = Shader.GetAttributeLocation("in_normal");
			color0_attrib = Shader.GetAttributeLocation("in_color0");
			color1_attrib = Shader.GetAttributeLocation("in_color1");
			color2_attrib = Shader.GetAttributeLocation("in_color2");
			color3_attrib = Shader.GetAttributeLocation("in_color3");

			texcoord0_attrib = Shader.GetAttributeLocation("in_texcoord0");
			texcoord1_attrib = Shader.GetAttributeLocation("in_texcoord1");
			texcoord2_attrib = Shader.GetAttributeLocation("in_texcoord2");
			texcoord3_attrib = Shader.GetAttributeLocation("in_texcoord3");
		}
		
		#region IStdShader Members

		public MatrixUniform Projection
		{
			get { return projection; }
		}

		public MatrixUniform ModelView
		{
			get { return modelview; }
		}

		public MatrixUniform Texture
		{
			get { return texture; }
		}

		public int PositionAttribute
		{
			get { return pos_attrib; }
		}

		public int NormalAttribute
		{
			get { return norm_attrib; }
		}

		public int Color0Attribute
		{
			get { return color0_attrib; }
		}

		public int Color1Attribute
		{
			get { return color1_attrib; }
		}

		public int Color2Attribute
		{
			get { return color2_attrib; }
		}

		public int Color3Attribute
		{
			get { return color3_attrib; }
		}

		public int TexCoord0Attribute
		{
			get { return texcoord0_attrib; }
		}

		public int TexCoord1Attribute
		{
			get { return texcoord1_attrib; }
		}

		public int TexCoord2Attribute
		{
			get { return texcoord2_attrib; }
		}

		public int TexCoord3Attribute
		{
			get { return texcoord3_attrib; }
		}

		#endregion
	}
}
