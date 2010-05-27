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

namespace Glorg2.Graphics.OpenGL.Shaders
{
	public interface IStdShader
	{
		MatrixUniform Projection { get; }
		MatrixUniform ModelView { get; }
		MatrixUniform Texture { get; }
		MatrixUniform Normal { get; }

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
