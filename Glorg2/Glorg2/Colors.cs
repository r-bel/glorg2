using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	public static class Colors
	{
		public static Vector4 FromArgb(uint color)
		{
			byte a = (byte)((color & 0xff000000) >> 24);
			byte r = (byte)((color & 0x00ff0000) >> 16);
			byte g = (byte)((color & 0x0000ff00) >> 8);
			byte b = (byte)(color & 0x000000ff);
			return FromArgb(r, g, b, a);
		}
		public static Vector4 FromArgb(byte red, byte green, byte blue, byte alpha)
		{
			return new Vector4(red / 255f, green / 255f, blue / 255f, alpha / 255f);
		}
		public static Vector4 FromArgb(System.Drawing.Color color)
		{
			return FromArgb(color.R, color.G, color.B, color.A);
		}

		public static Vector3 FromHSL(float hue, float saturation, float lightness)
		{
		}

		public static readonly Vector4 AliceBlue = FromArgb(0xfff0f8ff);
  	  	public static readonly Vector4 AntiqueWhite = FromArgb(0xfffaebd7);
  	  	public static readonly Vector4 Aqua = FromArgb(0xff00ffff);
  	  	public static readonly Vector4 Aquamarine = FromArgb(0xff7fffd4);
  	  	public static readonly Vector4 Azure = FromArgb(0xfff0ffff);
  	  	public static readonly Vector4 Beige = FromArgb(0xfff5f5dc);
  	  	public static readonly Vector4 Bisque = FromArgb(0xffffe4c4);
  	  	public static readonly Vector4 Black = FromArgb(0xff000000);
  	  	public static readonly Vector4 BlanchedAlmond = FromArgb(0xffffebcd);
  	  	public static readonly Vector4 Blue = FromArgb(0xff0000ff);
  	  	public static readonly Vector4 BlueViolet = FromArgb(0xff8a2be2);
  	  	public static readonly Vector4 Brown = FromArgb(0xffa52a2a);
  	  	public static readonly Vector4 Burlywood = FromArgb(0xffdeb887);
  	  	public static readonly Vector4 CadetBlue = FromArgb(0xff5f9ea0);
  	  	public static readonly Vector4 Chartreuse = FromArgb(0xff7fff00);
  	  	public static readonly Vector4 Chocolate = FromArgb(0xffd2691e);
  	  	public static readonly Vector4 Coral = FromArgb(0xffff7f50);
  	  	public static readonly Vector4 CornflowerBlue = FromArgb(0xff6495ed);
  	  	public static readonly Vector4 Cornsilk = FromArgb(0xfffff8dc);
  	  	public static readonly Vector4 Crimson = FromArgb(0xffdc143c);
  	  	public static readonly Vector4 Ryan = FromArgb(0xff00ffff);
  	  	public static readonly Vector4 DarkBlue = FromArgb(0xff00008b);
  	  	public static readonly Vector4 DarkCyan = FromArgb(0xff008b8b);
  	  	public static readonly Vector4 DarkGoldenrod = FromArgb(0xffb8860b);
  	  	public static readonly Vector4 DarkGray = FromArgb(0xffa9a9a9);
  	  	public static readonly Vector4 DarkGreen = FromArgb(0xff006400);
  	  	public static readonly Vector4 DarkGrey = FromArgb(0xffa9a9a9);
  	  	public static readonly Vector4 DarkKhaki = FromArgb(0xffbdb76b);
  	  	public static readonly Vector4 DarkMagenta = FromArgb(0xff8b008b);
  	  	public static readonly Vector4 DarkOliveGreen = FromArgb(0xff556b2f);
  	  	public static readonly Vector4 DarkOrange = FromArgb(0xffff8c00);
  	  	public static readonly Vector4 DarkOrchid = FromArgb(0xff9932cc);
  	  	public static readonly Vector4 DarkRed = FromArgb(0xff8b0000);
  	  	public static readonly Vector4 DarkSalmon = FromArgb(0xffe9967a);
  	  	public static readonly Vector4 DarkSeaGreen = FromArgb(0xff8fbc8f);
  	  	public static readonly Vector4 DarkSlateBlue = FromArgb(0xff483d8b);
  	  	public static readonly Vector4 DarkSlateGray = FromArgb(0xff2f4f4f);
  	  	public static readonly Vector4 DarkSlateGrey = FromArgb(0xff2f4f4f);
  	  	public static readonly Vector4 DarkTurquoise = FromArgb(0xff00ced1);
  	  	public static readonly Vector4 DarkViolet = FromArgb(0xff9400d3);
  	  	public static readonly Vector4 DeepPink = FromArgb(0xffff1493);
  	  	public static readonly Vector4 DeepSkyBlue = FromArgb(0xff00bfff);
  	  	public static readonly Vector4 DimGray = FromArgb(0xff696969);
  	  	public static readonly Vector4 DimGrey = FromArgb(0xff696969);
  	  	public static readonly Vector4 DodgerBlue = FromArgb(0xff1e90ff);
  	  	public static readonly Vector4 FireBrick = FromArgb(0xffb22222);
  	  	public static readonly Vector4 FloralWhite = FromArgb(0xfffffaf0);
  	  	public static readonly Vector4 ForestGreen = FromArgb(0xff228b22);
  	  	public static readonly Vector4 Fuchsia = FromArgb(0xffff00ff);
  	  	public static readonly Vector4 Gainsboro = FromArgb(0xffdcdcdc);
  	  	public static readonly Vector4 GhostWhite = FromArgb(0xfff8f8ff);
  	  	public static readonly Vector4 Gold = FromArgb(0xffffd700);
  	  	public static readonly Vector4 Goldenrod = FromArgb(0xffdaa520);
  	  	public static readonly Vector4 Gray = FromArgb(0xff808080);
  	  	public static readonly Vector4 Green = FromArgb(0xff008000);
  	  	public static readonly Vector4 GreenYellow = FromArgb(0xffadff2f);
  	  	public static readonly Vector4 Grey = FromArgb(0xff808080);
  	  	public static readonly Vector4 Honeydew = FromArgb(0xfff0fff0);
  	  	public static readonly Vector4 HotPink = FromArgb(0xffff69b4);
  	  	public static readonly Vector4 IndianRed = FromArgb(0xffcd5c5c);
  	  	public static readonly Vector4 Indigo = FromArgb(0xff4b0082);
  	  	public static readonly Vector4 Ivory = FromArgb(0xfffffff0);
  	  	public static readonly Vector4 Khaki = FromArgb(0xfff0e68c);
  	  	public static readonly Vector4 Lavender = FromArgb(0xffe6e6fa);
  	  	public static readonly Vector4 LavenderBlush = FromArgb(0xfffff0f5);
  	  	public static readonly Vector4 Lawngreen = FromArgb(0xff7cfc00);
  	  	public static readonly Vector4 LemonChiffon = FromArgb(0xfffffacd);
  	  	public static readonly Vector4 LightBlue = FromArgb(0xffadd8e6);
  	  	public static readonly Vector4 LightCoral = FromArgb(0xfff08080);
  	  	public static readonly Vector4 LightCyan = FromArgb(0xffe0ffff);
  	  	public static readonly Vector4 LightGoldenrodYellow = FromArgb(0xfffafad2);
  	  	public static readonly Vector4 LightGray = FromArgb(0xffd3d3d3);
  	  	public static readonly Vector4 LightGreen = FromArgb(0xff90ee90);
  	  	public static readonly Vector4 LightGrey = FromArgb(0xffd3d3d3);
  	  	public static readonly Vector4 LightPink = FromArgb(0xffffb6c1);
  	  	public static readonly Vector4 LightSalmon = FromArgb(0xffffa07a);
  	  	public static readonly Vector4 LightSeaGreen = FromArgb(0xff20b2aa);
  	  	public static readonly Vector4 LightSkyBlue = FromArgb(0xff87cefa);
  	  	public static readonly Vector4 LightSlateGray = FromArgb(0xff778899);
  	  	public static readonly Vector4 LightSlateGrey = FromArgb(0xff778899);
  	  	public static readonly Vector4 LightSteelBlue = FromArgb(0xffb0c4de);
  	  	public static readonly Vector4 LightYellow = FromArgb(0xffffffe0);
  	  	public static readonly Vector4 Lime = FromArgb(0xff00ff00);
  	  	public static readonly Vector4 LimeGreen = FromArgb(0xff32cd32);
  	  	public static readonly Vector4 Linen = FromArgb(0xfffaf0e6);
  	  	public static readonly Vector4 Magenta = FromArgb(0xffff00ff);
  	  	public static readonly Vector4 Maroon = FromArgb(0xff800000);
  	  	public static readonly Vector4 MediumAquamarine = FromArgb(0xff66cdaa);
  	  	public static readonly Vector4 MediumBlue = FromArgb(0xff0000cd);
  	  	public static readonly Vector4 MediumOrchid = FromArgb(0xffba55d3);
  	  	public static readonly Vector4 MediumPurple = FromArgb(0xff9370db);
  	  	public static readonly Vector4 MediumSeaGreen = FromArgb(0xff3cb371);
  	  	public static readonly Vector4 MediumSlateblue = FromArgb(0xff7b68ee);
  	  	public static readonly Vector4 MediumSpringGreen = FromArgb(0xff00fa9a);
  	  	public static readonly Vector4 MediumTurquoise = FromArgb(0xff48d1cc);
  	  	public static readonly Vector4 MediumVioletRed = FromArgb(0xffc71585);
  	  	public static readonly Vector4 MidnightBlue = FromArgb(0xff191970);
  	  	public static readonly Vector4 MintCream = FromArgb(0xfff5fffa);
  	  	public static readonly Vector4 MistyRose = FromArgb(0xffffe4e1);
  	  	public static readonly Vector4 Moccasin = FromArgb(0xffffe4b5);
  	  	public static readonly Vector4 NavajoWhite = FromArgb(0xffffdead);
  	  	public static readonly Vector4 Navy = FromArgb(0xff000080);
  	  	public static readonly Vector4 OldLace = FromArgb(0xfffdf5e6);
  	  	public static readonly Vector4 Olive = FromArgb(0xff808000);
  	  	public static readonly Vector4 OliveDrab = FromArgb(0xff6b8e23);
  	  	public static readonly Vector4 Orange = FromArgb(0xffffa500);
  	  	public static readonly Vector4 OrangeRed = FromArgb(0xffff4500);
  	  	public static readonly Vector4 Orchid = FromArgb(0xffda70d6);
  	  	public static readonly Vector4 PaleGoldenrod = FromArgb(0xffeee8aa);
  	  	public static readonly Vector4 PaleGreen = FromArgb(0xff98fb98);
  	  	public static readonly Vector4 PaleTurquoise = FromArgb(0xffafeeee);
  	  	public static readonly Vector4 PaleVioletRed = FromArgb(0xffdb7093);
  	  	public static readonly Vector4 PapayaWhip = FromArgb(0xffffefd5);
  	  	public static readonly Vector4 PeachPuff = FromArgb(0xffffdab9);
  	  	public static readonly Vector4 Peru = FromArgb(0xffcd853f);
  	  	public static readonly Vector4 Pink = FromArgb(0xffffc0cb);
  	  	public static readonly Vector4 Plum = FromArgb(0xffdda0dd);
  	  	public static readonly Vector4 PowderBlue = FromArgb(0xffb0e0e6);
  	  	public static readonly Vector4 Purple = FromArgb(0xff800080);
  	  	public static readonly Vector4 Red = FromArgb(0xffff0000);
  	  	public static readonly Vector4 RosyBrown = FromArgb(0xffbc8f8f);
  	  	public static readonly Vector4 RoyalBlue = FromArgb(0xff4169e1);
  	  	public static readonly Vector4 SaddleBrown = FromArgb(0xff8b4513);
  	  	public static readonly Vector4 Salmon = FromArgb(0xfffa8072);
  	  	public static readonly Vector4 SandyBrown = FromArgb(0xfff4a460);
  	  	public static readonly Vector4 SeaGreen = FromArgb(0xff2e8b57);
  	  	public static readonly Vector4 Seashell = FromArgb(0xfffff5ee);
  	  	public static readonly Vector4 Sienna = FromArgb(0xffa0522d);
  	  	public static readonly Vector4 Silver = FromArgb(0xffc0c0c0);
  	  	public static readonly Vector4 SkyBlue = FromArgb(0xff87ceeb);
  	  	public static readonly Vector4 SlateBlue = FromArgb(0xff6a5acd);
  	  	public static readonly Vector4 SlateGray = FromArgb(0xff708090);
  	  	public static readonly Vector4 SlateGrey = FromArgb(0xff708090);
  	  	public static readonly Vector4 Snow = FromArgb(0xfffffafa);
  	  	public static readonly Vector4 SpringGreen = FromArgb(0xff00ff7f);
  	  	public static readonly Vector4 SteelBlue = FromArgb(0xff4682b4);
  	  	public static readonly Vector4 Tan = FromArgb(0xffd2b48c);
  	  	public static readonly Vector4 Teal = FromArgb(0xff008080);
  	  	public static readonly Vector4 Thistle = FromArgb(0xffd8bfd8);
  	  	public static readonly Vector4 Tomato = FromArgb(0xffff6347);
  	  	public static readonly Vector4 Turquoise = FromArgb(0xff40e0d0);
  	  	public static readonly Vector4 Violet = FromArgb(0xffee82ee);
  	  	public static readonly Vector4 Wheat = FromArgb(0xfff5deb3);
  	  	public static readonly Vector4 White = FromArgb(0xffffffff);
  	  	public static readonly Vector4 WhiteSmoke = FromArgb(0xfff5f5f5);
  	  	public static readonly Vector4 Yellow = FromArgb(0xffffff00);
  	  	public static readonly Vector4 Yellowgreen = FromArgb(0xff9acd32);
	}
}
