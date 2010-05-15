using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL.Shaders
{
	public interface IStdShader
	{
		MatrixUniform Projection { get; }
		MatrixUniform ModelView { get; }
		MatrixUniform Texture { get; }

		int PositionAttribute { get; }
		int NormalAttribute { get; }
		int Color0Attribute { get; }
		int Color1Attribute { get; }
		int Color2Attribute { get; }
		int Color3Attribute { get; }
		int TexCoord0Attribute { get; }
		int TexCoord1Attribute { get; }
		int TexCoord2Attribute { get; }
		int TexCoord3Attribute { get; }
	}
	
	public interface ISpotLightShader
	{
		Vector3FloatUniform LightPosition { get; }
		Vector3FloatUniform LightDirection { get; }
		Vector4FloatUniform LightColor { get; }
		Vector4FloatUniform LightAmbientColor { get; }
		ScalarFloatUniform LightInnerRadius { get; }
		ScalarFloatUniform LightOuterRadius { get; }
	}
	public interface IPointLigtShader
	{
		Vector3FloatUniform LightPosition { get; }
		Vector4FloatUniform LightColor { get; }
		Vector4FloatUniform LightAmbientColor { get; }
		ScalarFloatUniform LightInnerRadius { get; }
		ScalarFloatUniform LightOuterRadius { get; }
	}

}
