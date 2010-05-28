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

namespace Glorg2.Graphics.OpenGL
{
	public enum Test : uint
	{
		Never = OpenGL.Const.GL_NEVER,
		Less = OpenGL.Const.GL_LESS,
		Equal = OpenGL.Const.GL_EQUAL,
		LessOrEqual = OpenGL.Const.GL_LEQUAL,
		Greater = OpenGL.Const.GL_GREATER,
		NotEqual = OpenGL.Const.GL_NOTEQUAL,
		GreaterOrEqual = OpenGL.Const.GL_GEQUAL,
		Always = OpenGL.Const.GL_ALWAYS
	}

	public enum OpenGLError
	{
			NoError = 0,
			InvalidEnum = 0x0500,
			InvalidValue = 0x0501,
			InvalidOperation = 0x0502,
			OutOfMemory = 0x0505
	}

	public static partial class OpenGL
	{
		public static class Const
		{
			#region GL_AMD_debug_output
			public const uint GL_MAX_DEBUG_LOGGED_MESSAGES_AMD = 0x9144;
			public const uint GL_DEBUG_LOGGED_MESSAGES_AMD = 0x9145;
			public const uint GL_DEBUG_SEVERITY_HIGH_AMD = 0x9146;
			public const uint GL_DEBUG_SEVERITY_MEDIUM_AMD = 0x9147;
			public const uint GL_DEBUG_SEVERITY_LOW_AMD = 0x9148;
			public const uint GL_DEBUG_CATEGORY_API_ERROR_AMD = 0x9149;
			public const uint GL_DEBUG_CATEGORY_WINDOW_SYSTEM_AMD = 0x914A;
			public const uint GL_DEBUG_CATEGORY_DEPRECATION_AMD = 0x914B;
			public const uint GL_DEBUG_CATEGORY_UNDEFINED_BEHAVIOR_AMD = 0x914C;
			public const uint GL_DEBUG_CATEGORY_PERFORMANCE_AMD = 0x914D;
			public const uint GL_DEBUG_CATEGORY_SHADER_COMPILER_AMD = 0x914E;
			public const uint GL_DEBUG_CATEGORY_APPLICATION_AMD = 0x914F;
			public const uint GL_DEBUG_CATEGORY_OTHER_AMD = 0x9150;
			#endregion
			#region OpenGL 1.1

			/* AttribMask */
			public const uint GL_DEPTH_BUFFER_BIT = 0x00000100;
			public const uint GL_STENCIL_BUFFER_BIT = 0x00000400;
			public const uint GL_COLOR_BUFFER_BIT = 0x00004000;
			/* Boolean */
			public const uint GL_FALSE = 0;
			public const uint GL_TRUE = 1;
			/* BeginMode */
			public const uint GL_POINTS = 0x0000;
			public const uint GL_LINES = 0x0001;
			public const uint GL_LINE_LOOP = 0x0002;
			public const uint GL_LINE_STRIP = 0x0003;
			public const uint GL_TRIANGLES = 0x0004;
			public const uint GL_TRIANGLE_STRIP = 0x0005;
			public const uint GL_TRIANGLE_FAN = 0x0006;
			/* AlphaFunction */
			public const uint GL_NEVER = 0x0200;
			public const uint GL_LESS = 0x0201;
			public const uint GL_EQUAL = 0x0202;
			public const uint GL_LEQUAL = 0x0203;
			public const uint GL_GREATER = 0x0204;
			public const uint GL_NOTEQUAL = 0x0205;
			public const uint GL_GEQUAL = 0x0206;
			public const uint GL_ALWAYS = 0x0207;
			/* BlendingFactorDest */
			public const uint GL_ZERO = 0;
			public const uint GL_ONE = 1;
			public const uint GL_SRC_COLOR = 0x0300;
			public const uint GL_ONE_MINUS_SRC_COLOR = 0x0301;
			public const uint GL_SRC_ALPHA = 0x0302;
			public const uint GL_ONE_MINUS_SRC_ALPHA = 0x0303;
			public const uint GL_DST_ALPHA = 0x0304;
			public const uint GL_ONE_MINUS_DST_ALPHA = 0x0305;
			/* BlendingFactorSrc */
			public const uint GL_DST_COLOR = 0x0306;
			public const uint GL_ONE_MINUS_DST_COLOR = 0x0307;
			public const uint GL_SRC_ALPHA_SATURATE = 0x0308;
			/* DrawBufferMode */
			public const uint GL_NONE = 0;
			public const uint GL_FRONT_LEFT = 0x0400;
			public const uint GL_FRONT_RIGHT = 0x0401;
			public const uint GL_BACK_LEFT = 0x0402;
			public const uint GL_BACK_RIGHT = 0x0403;
			public const uint GL_FRONT = 0x0404;
			public const uint GL_BACK = 0x0405;
			public const uint GL_LEFT = 0x0406;
			public const uint GL_RIGHT = 0x0407;
			public const uint GL_FRONT_AND_BACK = 0x0408;
			/* ErrorCode */
			public const uint GL_NO_ERROR = 0;
			public const uint GL_INVALID_ENUM = 0x0500;
			public const uint GL_INVALID_VALUE = 0x0501;
			public const uint GL_INVALID_OPERATION = 0x0502;
			public const uint GL_OUT_OF_MEMORY = 0x0505;
			/* FrontFaceDirection */
			public const uint GL_CW = 0x0900;
			public const uint GL_CCW = 0x0901;
			/* GetPName */
			public const uint GL_POINT_SIZE = 0x0B11;
			public const uint GL_POINT_SIZE_RANGE = 0x0B12;
			public const uint GL_POINT_SIZE_GRANULARITY = 0x0B13;
			public const uint GL_LINE_SMOOTH = 0x0B20;
			public const uint GL_LINE_WIDTH = 0x0B21;
			public const uint GL_LINE_WIDTH_RANGE = 0x0B22;
			public const uint GL_LINE_WIDTH_GRANULARITY = 0x0B23;
			public const uint GL_POLYGON_SMOOTH = 0x0B41;
			public const uint GL_CULL_FACE = 0x0B44;
			public const uint GL_CULL_FACE_MODE = 0x0B45;
			public const uint GL_FRONT_FACE = 0x0B46;
			public const uint GL_DEPTH_RANGE = 0x0B70;
			public const uint GL_DEPTH_TEST = 0x0B71;
			public const uint GL_DEPTH_WRITEMASK = 0x0B72;
			public const uint GL_DEPTH_CLEAR_VALUE = 0x0B73;
			public const uint GL_DEPTH_FUNC = 0x0B74;
			public const uint GL_STENCIL_TEST = 0x0B90;
			public const uint GL_STENCIL_CLEAR_VALUE = 0x0B91;
			public const uint GL_STENCIL_FUNC = 0x0B92;
			public const uint GL_STENCIL_VALUE_MASK = 0x0B93;
			public const uint GL_STENCIL_FAIL = 0x0B94;
			public const uint GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95;
			public const uint GL_STENCIL_PASS_DEPTH_PASS = 0x0B96;
			public const uint GL_STENCIL_REF = 0x0B97;
			public const uint GL_STENCIL_WRITEMASK = 0x0B98;
			public const uint GL_VIEWPORT = 0x0BA2;
			public const uint GL_DITHER = 0x0BD0;
			public const uint GL_BLEND_DST = 0x0BE0;
			public const uint GL_BLEND_SRC = 0x0BE1;
			public const uint GL_BLEND = 0x0BE2;
			public const uint GL_LOGIC_OP_MODE = 0x0BF0;
			public const uint GL_COLOR_LOGIC_OP = 0x0BF2;
			public const uint GL_DRAW_BUFFER = 0x0C01;
			public const uint GL_READ_BUFFER = 0x0C02;
			public const uint GL_SCISSOR_BOX = 0x0C10;
			public const uint GL_SCISSOR_TEST = 0x0C11;
			public const uint GL_COLOR_CLEAR_VALUE = 0x0C22;
			public const uint GL_COLOR_WRITEMASK = 0x0C23;
			public const uint GL_DOUBLEBUFFER = 0x0C32;
			public const uint GL_STEREO = 0x0C33;
			public const uint GL_LINE_SMOOTH_HINT = 0x0C52;
			public const uint GL_POLYGON_SMOOTH_HINT = 0x0C53;
			public const uint GL_UNPACK_SWAP_BYTES = 0x0CF0;
			public const uint GL_UNPACK_LSB_FIRST = 0x0CF1;
			public const uint GL_UNPACK_ROW_LENGTH = 0x0CF2;
			public const uint GL_UNPACK_SKIP_ROWS = 0x0CF3;
			public const uint GL_UNPACK_SKIP_PIXELS = 0x0CF4;
			public const uint GL_UNPACK_ALIGNMENT = 0x0CF5;
			public const uint GL_PACK_SWAP_BYTES = 0x0D00;
			public const uint GL_PACK_LSB_FIRST = 0x0D01;
			public const uint GL_PACK_ROW_LENGTH = 0x0D02;
			public const uint GL_PACK_SKIP_ROWS = 0x0D03;
			public const uint GL_PACK_SKIP_PIXELS = 0x0D04;
			public const uint GL_PACK_ALIGNMENT = 0x0D05;
			public const uint GL_MAX_TEXTURE_SIZE = 0x0D33;
			public const uint GL_MAX_VIEWPORT_DIMS = 0x0D3A;
			public const uint GL_SUBPIXEL_BITS = 0x0D50;
			public const uint GL_TEXTURE_1D = 0x0DE0;
			public const uint GL_TEXTURE_2D = 0x0DE1;
			public const uint GL_POLYGON_OFFSET_UNITS = 0x2A00;
			public const uint GL_POLYGON_OFFSET_POINT = 0x2A01;
			public const uint GL_POLYGON_OFFSET_LINE = 0x2A02;
			public const uint GL_POLYGON_OFFSET_FILL = 0x8037;
			public const uint GL_POLYGON_OFFSET_FACTOR = 0x8038;
			public const uint GL_TEXTURE_BINDING_1D = 0x8068;
			public const uint GL_TEXTURE_BINDING_2D = 0x8069;
			/* GetTextureParameter */
			public const uint GL_TEXTURE_WIDTH = 0x1000;
			public const uint GL_TEXTURE_HEIGHT = 0x1001;
			public const uint GL_TEXTURE_INTERNAL_FORMAT = 0x1003;
			public const uint GL_TEXTURE_BORDER_COLOR = 0x1004;
			public const uint GL_TEXTURE_RED_SIZE = 0x805C;
			public const uint GL_TEXTURE_GREEN_SIZE = 0x805D;
			public const uint GL_TEXTURE_BLUE_SIZE = 0x805E;
			public const uint GL_TEXTURE_ALPHA_SIZE = 0x805F;
			/* HintMode */
			public const uint GL_DONT_CARE = 0x1100;
			public const uint GL_FASTEST = 0x1101;
			public const uint GL_NICEST = 0x1102;
			/* DataType */
			public const uint GL_BYTE = 0x1400;
			public const uint GL_UNSIGNED_BYTE = 0x1401;
			public const uint GL_SHORT = 0x1402;
			public const uint GL_UNSIGNED_SHORT = 0x1403;
			public const uint GL_INT = 0x1404;
			public const uint GL_UNSIGNED_INT = 0x1405;
			public const uint GL_FLOAT = 0x1406;
			public const uint GL_DOUBLE = 0x140A;
			/* LogicOp */
			public const uint GL_CLEAR = 0x1500;
			public const uint GL_AND = 0x1501;
			public const uint GL_AND_REVERSE = 0x1502;
			public const uint GL_COPY = 0x1503;
			public const uint GL_AND_INVERTED = 0x1504;
			public const uint GL_NOOP = 0x1505;
			public const uint GL_XOR = 0x1506;
			public const uint GL_OR = 0x1507;
			public const uint GL_NOR = 0x1508;
			public const uint GL_EQUIV = 0x1509;
			public const uint GL_INVERT = 0x150A;
			public const uint GL_OR_REVERSE = 0x150B;
			public const uint GL_COPY_INVERTED = 0x150C;
			public const uint GL_OR_INVERTED = 0x150D;
			public const uint GL_NAND = 0x150E;
			public const uint GL_SET = 0x150F;
			/* MatrixMode (for gl3.h, FBO attachment type) */
			public const uint GL_TEXTURE = 0x1702;
			/* PixelCopyType */
			public const uint GL_COLOR = 0x1800;
			public const uint GL_DEPTH = 0x1801;
			public const uint GL_STENCIL = 0x1802;
			/* PixelFormat */
			public const uint GL_STENCIL_INDEX = 0x1901;
			public const uint GL_DEPTH_COMPONENT = 0x1902;
			public const uint GL_RED = 0x1903;
			public const uint GL_GREEN = 0x1904;
			public const uint GL_BLUE = 0x1905;
			public const uint GL_ALPHA = 0x1906;
			public const uint GL_RGB = 0x1907;
			public const uint GL_RGBA = 0x1908;
			/* PolygonMode */
			public const uint GL_POINT = 0x1B00;
			public const uint GL_LINE = 0x1B01;
			public const uint GL_FILL = 0x1B02;
			/* StencilOp */
			public const uint GL_KEEP = 0x1E00;
			public const uint GL_REPLACE = 0x1E01;
			public const uint GL_INCR = 0x1E02;
			public const uint GL_DECR = 0x1E03;
			/* StringName */
			public const uint GL_VENDOR = 0x1F00;
			public const uint GL_RENDERER = 0x1F01;
			public const uint GL_VERSION = 0x1F02;
			public const uint GL_EXTENSIONS = 0x1F03;
			/* TextureMagFilter */
			public const uint GL_NEAREST = 0x2600;
			public const uint GL_LINEAR = 0x2601;
			/* TextureMinFilter */
			public const uint GL_NEAREST_MIPMAP_NEAREST = 0x2700;
			public const uint GL_LINEAR_MIPMAP_NEAREST = 0x2701;
			public const uint GL_NEAREST_MIPMAP_LINEAR = 0x2702;
			public const uint GL_LINEAR_MIPMAP_LINEAR = 0x2703;
			/* TextureParameterName */
			public const uint GL_TEXTURE_MAG_FILTER = 0x2800;
			public const uint GL_TEXTURE_MIN_FILTER = 0x2801;
			public const uint GL_TEXTURE_WRAP_S = 0x2802;
			public const uint GL_TEXTURE_WRAP_T = 0x2803;
			/* TextureTarget */
			public const uint GL_PROXY_TEXTURE_1D = 0x8063;
			public const uint GL_PROXY_TEXTURE_2D = 0x8064;
			/* TextureWrapMode */
			public const uint GL_REPEAT = 0x2901;
			/* PixelInternalFormat */
			public const uint GL_R3_G3_B2 = 0x2A10;
			public const uint GL_RGB4 = 0x804F;
			public const uint GL_RGB5 = 0x8050;
			public const uint GL_RGB8 = 0x8051;
			public const uint GL_RGB10 = 0x8052;
			public const uint GL_RGB12 = 0x8053;
			public const uint GL_RGB16 = 0x8054;
			public const uint GL_RGBA2 = 0x8055;
			public const uint GL_RGBA4 = 0x8056;
			public const uint GL_RGB5_A1 = 0x8057;
			public const uint GL_RGBA8 = 0x8058;
			public const uint GL_RGB10_A2 = 0x8059;
			public const uint GL_RGBA12 = 0x805A;
			public const uint GL_RGBA16 = 0x805B;

			#endregion

			#region OpenGL 1.2
			public const uint GL_UNSIGNED_BYTE_3_3_2 = 0x8032;
			public const uint GL_UNSIGNED_SHORT_4_4_4_4 = 0x8033;
			public const uint GL_UNSIGNED_SHORT_5_5_5_1 = 0x8034;
			public const uint GL_UNSIGNED_INT_8_8_8_8 = 0x8035;
			public const uint GL_UNSIGNED_INT_10_10_10_2 = 0x8036;
			public const uint GL_TEXTURE_BINDING_3D = 0x806A;
			public const uint GL_PACK_SKIP_IMAGES = 0x806B;
			public const uint GL_PACK_IMAGE_HEIGHT = 0x806C;
			public const uint GL_UNPACK_SKIP_IMAGES = 0x806D;
			public const uint GL_UNPACK_IMAGE_HEIGHT = 0x806E;
			public const uint GL_TEXTURE_3D = 0x806F;
			public const uint GL_PROXY_TEXTURE_3D = 0x8070;
			public const uint GL_TEXTURE_DEPTH = 0x8071;
			public const uint GL_TEXTURE_WRAP_R = 0x8072;
			public const uint GL_MAX_3D_TEXTURE_SIZE = 0x8073;
			public const uint GL_UNSIGNED_BYTE_2_3_3_REV = 0x8362;
			public const uint GL_UNSIGNED_SHORT_5_6_5 = 0x8363;
			public const uint GL_UNSIGNED_SHORT_5_6_5_REV = 0x8364;
			public const uint GL_UNSIGNED_SHORT_4_4_4_4_REV = 0x8365;
			public const uint GL_UNSIGNED_SHORT_1_5_5_5_REV = 0x8366;
			public const uint GL_UNSIGNED_INT_8_8_8_8_REV = 0x8367;
			public const uint GL_UNSIGNED_INT_2_10_10_10_REV = 0x8368;
			public const uint GL_BGR = 0x80E0;
			public const uint GL_BGRA = 0x80E1;
			public const uint GL_MAX_ELEMENTS_VERTICES = 0x80E8;
			public const uint GL_MAX_ELEMENTS_INDICES = 0x80E9;
			public const uint GL_CLAMP_TO_EDGE = 0x812F;
			public const uint GL_TEXTURE_MIN_LOD = 0x813A;
			public const uint GL_TEXTURE_MAX_LOD = 0x813B;
			public const uint GL_TEXTURE_BASE_LEVEL = 0x813C;
			public const uint GL_TEXTURE_MAX_LEVEL = 0x813D;
			public const uint GL_SMOOTH_POINT_SIZE_RANGE = 0x0B12;
			public const uint GL_SMOOTH_POINT_SIZE_GRANULARITY = 0x0B13;
			public const uint GL_SMOOTH_LINE_WIDTH_RANGE = 0x0B22;
			public const uint GL_SMOOTH_LINE_WIDTH_GRANULARITY = 0x0B23;
			public const uint GL_ALIASED_LINE_WIDTH_RANGE = 0x846E;
			#endregion

			#region ARB_imaging
			public const uint GL_CONSTANT_COLOR = 0x8001;
			public const uint GL_ONE_MINUS_CONSTANT_COLOR = 0x8002;
			public const uint GL_CONSTANT_ALPHA = 0x8003;
			public const uint GL_ONE_MINUS_CONSTANT_ALPHA = 0x8004;
			public const uint GL_BLEND_COLOR = 0x8005;
			public const uint GL_FUNC_ADD = 0x8006;
			public const uint GL_MIN = 0x8007;
			public const uint GL_MAX = 0x8008;
			public const uint GL_BLEND_EQUATION = 0x8009;
			public const uint GL_FUNC_SUBTRACT = 0x800A;
			public const uint GL_FUNC_REVERSE_SUBTRACT = 0x800B;
			#endregion

			#region OpenGL 1.3
			public const uint GL_TEXTURE1 = 0x84C1;
			public const uint GL_TEXTURE2 = 0x84C2;
			public const uint GL_TEXTURE3 = 0x84C3;
			public const uint GL_TEXTURE4 = 0x84C4;
			public const uint GL_TEXTURE5 = 0x84C5;
			public const uint GL_TEXTURE6 = 0x84C6;
			public const uint GL_TEXTURE7 = 0x84C7;
			public const uint GL_TEXTURE8 = 0x84C8;
			public const uint GL_TEXTURE9 = 0x84C9;
			public const uint GL_TEXTURE10 = 0x84CA;
			public const uint GL_TEXTURE11 = 0x84CB;
			public const uint GL_TEXTURE12 = 0x84CC;
			public const uint GL_TEXTURE13 = 0x84CD;
			public const uint GL_TEXTURE14 = 0x84CE;
			public const uint GL_TEXTURE15 = 0x84CF;
			public const uint GL_TEXTURE16 = 0x84D0;
			public const uint GL_TEXTURE17 = 0x84D1;
			public const uint GL_TEXTURE18 = 0x84D2;
			public const uint GL_TEXTURE19 = 0x84D3;
			public const uint GL_TEXTURE20 = 0x84D4;
			public const uint GL_TEXTURE21 = 0x84D5;
			public const uint GL_TEXTURE22 = 0x84D6;
			public const uint GL_TEXTURE23 = 0x84D7;
			public const uint GL_TEXTURE24 = 0x84D8;
			public const uint GL_TEXTURE25 = 0x84D9;
			public const uint GL_TEXTURE26 = 0x84DA;
			public const uint GL_TEXTURE27 = 0x84DB;
			public const uint GL_TEXTURE28 = 0x84DC;
			public const uint GL_TEXTURE29 = 0x84DD;
			public const uint GL_TEXTURE30 = 0x84DE;
			public const uint GL_TEXTURE31 = 0x84DF;
			public const uint GL_ACTIVE_TEXTURE = 0x84E0;
			public const uint GL_MULTISAMPLE = 0x809D;
			public const uint GL_SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
			public const uint GL_SAMPLE_ALPHA_TO_ONE = 0x809F;
			public const uint GL_SAMPLE_COVERAGE = 0x80A0;
			public const uint GL_SAMPLE_BUFFERS = 0x80A8;
			public const uint GL_SAMPLES = 0x80A9;
			public const uint GL_SAMPLE_COVERAGE_VALUE = 0x80AA;
			public const uint GL_SAMPLE_COVERAGE_INVERT = 0x80AB;
			public const uint GL_TEXTURE_CUBE_MAP = 0x8513;
			public const uint GL_TEXTURE_BINDING_CUBE_MAP = 0x8514;
			public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
			public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
			public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
			public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
			public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
			public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
			public const uint GL_PROXY_TEXTURE_CUBE_MAP = 0x851B;
			public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;
			public const uint GL_COMPRESSED_RGB = 0x84ED;
			public const uint GL_COMPRESSED_RGBA = 0x84EE;
			public const uint GL_TEXTURE_COMPRESSION_HINT = 0x84EF;
			public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE = 0x86A0;
			public const uint GL_TEXTURE_COMPRESSED = 0x86A1;
			public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
			public const uint GL_COMPRESSED_TEXTURE_FORMATS = 0x86A3;
			public const uint GL_CLAMP_TO_BORDER = 0x812D;
			#endregion

			#region OpenGL 1.4
			public const uint GL_BLEND_DST_RGB = 0x80C8;
			public const uint GL_BLEND_SRC_RGB = 0x80C9;
			public const uint GL_BLEND_DST_ALPHA = 0x80CA;
			public const uint GL_BLEND_SRC_ALPHA = 0x80CB;
			public const uint GL_POINT_FADE_THRESHOLD_SIZE = 0x8128;
			public const uint GL_DEPTH_COMPONENT16 = 0x81A5;
			public const uint GL_DEPTH_COMPONENT24 = 0x81A6;
			public const uint GL_DEPTH_COMPONENT32 = 0x81A7;
			public const uint GL_MIRRORED_REPEAT = 0x8370;
			public const uint GL_MAX_TEXTURE_LOD_BIAS = 0x84FD;
			public const uint GL_TEXTURE_LOD_BIAS = 0x8501;
			public const uint GL_INCR_WRAP = 0x8507;
			public const uint GL_DECR_WRAP = 0x8508;
			public const uint GL_TEXTURE_DEPTH_SIZE = 0x884A;
			public const uint GL_TEXTURE_COMPARE_MODE = 0x884C;
			public const uint GL_TEXTURE_COMPARE_FUNC = 0x884D;
			#endregion

			#region OpenGL 1.5

			public const uint GL_BUFFER_SIZE = 0x8764;
			public const uint GL_BUFFER_USAGE = 0x8765;
			public const uint GL_QUERY_COUNTER_BITS = 0x8864;
			public const uint GL_CURRENT_QUERY = 0x8865;
			public const uint GL_QUERY_RESULT = 0x8866;
			public const uint GL_QUERY_RESULT_AVAILABLE = 0x8867;
			public const uint GL_ARRAY_BUFFER = 0x8892;
			public const uint GL_ELEMENT_ARRAY_BUFFER = 0x8893;
			public const uint GL_ARRAY_BUFFER_BINDING = 0x8894;
			public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;
			public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;
			public const uint GL_READ_ONLY = 0x88B8;
			public const uint GL_WRITE_ONLY = 0x88B9;
			public const uint GL_READ_WRITE = 0x88BA;
			public const uint GL_BUFFER_ACCESS = 0x88BB;
			public const uint GL_BUFFER_MAPPED = 0x88BC;
			public const uint GL_BUFFER_MAP_POINTER = 0x88BD;
			public const uint GL_STREAM_DRAW = 0x88E0;
			public const uint GL_STREAM_READ = 0x88E1;
			public const uint GL_STREAM_COPY = 0x88E2;
			public const uint GL_STATIC_DRAW = 0x88E4;
			public const uint GL_STATIC_READ = 0x88E5;
			public const uint GL_STATIC_COPY = 0x88E6;
			public const uint GL_DYNAMIC_DRAW = 0x88E8;
			public const uint GL_DYNAMIC_READ = 0x88E9;
			public const uint GL_DYNAMIC_COPY = 0x88EA;
			public const uint GL_SAMPLES_PASSED = 0x8914;

			#endregion

			#region OpenGL 2.0
			public const uint GL_BLEND_EQUATION_RGB = 0x8009;
			public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;
			public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;
			public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;
			public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;
			public const uint GL_CURRENT_VERTEX_ATTRIB = 0x8626;
			public const uint GL_VERTEX_PROGRAM_POINT_SIZE = 0x8642;
			public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER = 0x8645;
			public const uint GL_STENCIL_BACK_FUNC = 0x8800;
			public const uint GL_STENCIL_BACK_FAIL = 0x8801;
			public const uint GL_STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;
			public const uint GL_STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;
			public const uint GL_MAX_DRAW_BUFFERS = 0x8824;
			public const uint GL_DRAW_BUFFER0 = 0x8825;
			public const uint GL_DRAW_BUFFER1 = 0x8826;
			public const uint GL_DRAW_BUFFER2 = 0x8827;
			public const uint GL_DRAW_BUFFER3 = 0x8828;
			public const uint GL_DRAW_BUFFER4 = 0x8829;
			public const uint GL_DRAW_BUFFER5 = 0x882A;
			public const uint GL_DRAW_BUFFER6 = 0x882B;
			public const uint GL_DRAW_BUFFER7 = 0x882C;
			public const uint GL_DRAW_BUFFER8 = 0x882D;
			public const uint GL_DRAW_BUFFER9 = 0x882E;
			public const uint GL_DRAW_BUFFER10 = 0x882F;
			public const uint GL_DRAW_BUFFER11 = 0x8830;
			public const uint GL_DRAW_BUFFER12 = 0x8831;
			public const uint GL_DRAW_BUFFER13 = 0x8832;
			public const uint GL_DRAW_BUFFER14 = 0x8833;
			public const uint GL_DRAW_BUFFER15 = 0x8834;
			public const uint GL_BLEND_EQUATION_ALPHA = 0x883D;
			public const uint GL_MAX_VERTEX_ATTRIBS = 0x8869;
			public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED = 0x886A;
			public const uint GL_MAX_TEXTURE_IMAGE_UNITS = 0x8872;
			public const uint GL_FRAGMENT_SHADER = 0x8B30;
			public const uint GL_VERTEX_SHADER = 0x8B31;
			public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS = 0x8B49;
			public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS = 0x8B4A;
			public const uint GL_MAX_VARYING_FLOATS = 0x8B4B;
			public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;
			public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
			public const uint GL_SHADER_TYPE = 0x8B4F;
			public const uint GL_FLOAT_VEC2 = 0x8B50;
			public const uint GL_FLOAT_VEC3 = 0x8B51;
			public const uint GL_FLOAT_VEC4 = 0x8B52;
			public const uint GL_INT_VEC2 = 0x8B53;
			public const uint GL_INT_VEC3 = 0x8B54;
			public const uint GL_INT_VEC4 = 0x8B55;
			public const uint GL_BOOL = 0x8B56;
			public const uint GL_BOOL_VEC2 = 0x8B57;
			public const uint GL_BOOL_VEC3 = 0x8B58;
			public const uint GL_BOOL_VEC4 = 0x8B59;
			public const uint GL_FLOAT_MAT2 = 0x8B5A;
			public const uint GL_FLOAT_MAT3 = 0x8B5B;
			public const uint GL_FLOAT_MAT4 = 0x8B5C;
			public const uint GL_SAMPLER_1D = 0x8B5D;
			public const uint GL_SAMPLER_2D = 0x8B5E;
			public const uint GL_SAMPLER_3D = 0x8B5F;
			public const uint GL_SAMPLER_CUBE = 0x8B60;
			public const uint GL_SAMPLER_1D_SHADOW = 0x8B61;
			public const uint GL_SAMPLER_2D_SHADOW = 0x8B62;
			public const uint GL_DELETE_STATUS = 0x8B80;
			public const uint GL_COMPILE_STATUS = 0x8B81;
			public const uint GL_LINK_STATUS = 0x8B82;
			public const uint GL_VALIDATE_STATUS = 0x8B83;
			public const uint GL_INFO_LOG_LENGTH = 0x8B84;
			public const uint GL_ATTACHED_SHADERS = 0x8B85;
			public const uint GL_ACTIVE_UNIFORMS = 0x8B86;
			public const uint GL_ACTIVE_UNIFORM_MAX_LENGTH = 0x8B87;
			public const uint GL_SHADER_SOURCE_LENGTH = 0x8B88;
			public const uint GL_ACTIVE_ATTRIBUTES = 0x8B89;
			public const uint GL_ACTIVE_ATTRIBUTE_MAX_LENGTH = 0x8B8A;
			public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B;
			public const uint GL_SHADING_LANGUAGE_VERSION = 0x8B8C;
			public const uint GL_CURRENT_PROGRAM = 0x8B8D;
			public const uint GL_POINT_SPRITE_COORD_ORIGIN = 0x8CA0;
			public const uint GL_LOWER_LEFT = 0x8CA1;
			public const uint GL_UPPER_LEFT = 0x8CA2;
			public const uint GL_STENCIL_BACK_REF = 0x8CA3;
			public const uint GL_STENCIL_BACK_VALUE_MASK = 0x8CA4;
			public const uint GL_STENCIL_BACK_WRITEMASK = 0x8CA5;
			#endregion

			#region OpenGL 2.1
			public const uint GL_PIXEL_PACK_BUFFER = 0x88EB;
			public const uint GL_PIXEL_UNPACK_BUFFER = 0x88EC;
			public const uint GL_PIXEL_PACK_BUFFER_BINDING = 0x88ED;
			public const uint GL_PIXEL_UNPACK_BUFFER_BINDING = 0x88EF;
			public const uint GL_FLOAT_MAT2x3 = 0x8B65;
			public const uint GL_FLOAT_MAT2x4 = 0x8B66;
			public const uint GL_FLOAT_MAT3x2 = 0x8B67;
			public const uint GL_FLOAT_MAT3x4 = 0x8B68;
			public const uint GL_FLOAT_MAT4x2 = 0x8B69;
			public const uint GL_FLOAT_MAT4x3 = 0x8B6A;
			public const uint GL_SRGB = 0x8C40;
			public const uint GL_SRGB8 = 0x8C41;
			public const uint GL_SRGB_ALPHA = 0x8C42;
			public const uint GL_SRGB8_ALPHA8 = 0x8C43;
			public const uint GL_COMPRESSED_SRGB = 0x8C48;
			public const uint GL_COMPRESSED_SRGB_ALPHA = 0x8C49;
			#endregion

			#region OpenGL 3.0
			public const uint GL_COMPARE_REF_TO_TEXTURE = 0x884E;
			public const uint GL_CLIP_DISTANCE0 = 0x3000;
			public const uint GL_CLIP_DISTANCE1 = 0x3001;
			public const uint GL_CLIP_DISTANCE2 = 0x3002;
			public const uint GL_CLIP_DISTANCE3 = 0x3003;
			public const uint GL_CLIP_DISTANCE4 = 0x3004;
			public const uint GL_CLIP_DISTANCE5 = 0x3005;
			public const uint GL_CLIP_DISTANCE6 = 0x3006;
			public const uint GL_CLIP_DISTANCE7 = 0x3007;
			public const uint GL_MAX_CLIP_DISTANCES = 0x0D32;
			public const uint GL_MAJOR_VERSION = 0x821B;
			public const uint GL_MINOR_VERSION = 0x821C;
			public const uint GL_NUM_EXTENSIONS = 0x821D;
			public const uint GL_CONTEXT_FLAGS = 0x821E;
			public const uint GL_DEPTH_BUFFER = 0x8223;
			public const uint GL_STENCIL_BUFFER = 0x8224;
			public const uint GL_COMPRESSED_RED = 0x8225;
			public const uint GL_COMPRESSED_RG = 0x8226;
			public const uint GL_CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT = 0x0001;
			public const uint GL_RGBA32F = 0x8814;
			public const uint GL_RGB32F = 0x8815;
			public const uint GL_RGBA16F = 0x881A;
			public const uint GL_RGB16F = 0x881B;
			public const uint GL_VERTEX_ATTRIB_ARRAY_INTEGER = 0x88FD;
			public const uint GL_MAX_ARRAY_TEXTURE_LAYERS = 0x88FF;
			public const uint GL_MIN_PROGRAM_TEXEL_OFFSET = 0x8904;
			public const uint GL_MAX_PROGRAM_TEXEL_OFFSET = 0x8905;
			public const uint GL_CLAMP_READ_COLOR = 0x891C;
			public const uint GL_FIXED_ONLY = 0x891D;
			public const uint GL_MAX_VARYING_COMPONENTS = 0x8B4B;
			public const uint GL_TEXTURE_1D_ARRAY = 0x8C18;
			public const uint GL_PROXY_TEXTURE_1D_ARRAY = 0x8C19;
			public const uint GL_TEXTURE_2D_ARRAY = 0x8C1A;
			public const uint GL_PROXY_TEXTURE_2D_ARRAY = 0x8C1B;
			public const uint GL_TEXTURE_BINDING_1D_ARRAY = 0x8C1C;
			public const uint GL_TEXTURE_BINDING_2D_ARRAY = 0x8C1D;
			public const uint GL_R11F_G11F_B10F = 0x8C3A;
			public const uint GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B;
			public const uint GL_RGB9_E5 = 0x8C3D;
			public const uint GL_UNSIGNED_INT_5_9_9_9_REV = 0x8C3E;
			public const uint GL_TEXTURE_SHARED_SIZE = 0x8C3F;
			public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 0x8C76;
			public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE = 0x8C7F;
			public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS = 0x8C80;
			public const uint GL_TRANSFORM_FEEDBACK_VARYINGS = 0x8C83;
			public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START = 0x8C84;
			public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE = 0x8C85;
			public const uint GL_PRIMITIVES_GENERATED = 0x8C87;
			public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 0x8C88;
			public const uint GL_RASTERIZER_DISCARD = 0x8C89;
			public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A;
			public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS = 0x8C8B;
			public const uint GL_INTERLEAVED_ATTRIBS = 0x8C8C;
			public const uint GL_SEPARATE_ATTRIBS = 0x8C8D;
			public const uint GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E;
			public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING = 0x8C8F;
			public const uint GL_RGBA32UI = 0x8D70;
			public const uint GL_RGB32UI = 0x8D71;
			public const uint GL_RGBA16UI = 0x8D76;
			public const uint GL_RGB16UI = 0x8D77;
			public const uint GL_RGBA8UI = 0x8D7C;
			public const uint GL_RGB8UI = 0x8D7D;
			public const uint GL_RGBA32I = 0x8D82;
			public const uint GL_RGB32I = 0x8D83;
			public const uint GL_RGBA16I = 0x8D88;
			public const uint GL_RGB16I = 0x8D89;
			public const uint GL_RGBA8I = 0x8D8E;
			public const uint GL_RGB8I = 0x8D8F;
			public const uint GL_RED_INTEGER = 0x8D94;
			public const uint GL_GREEN_INTEGER = 0x8D95;
			public const uint GL_BLUE_INTEGER = 0x8D96;
			public const uint GL_RGB_INTEGER = 0x8D98;
			public const uint GL_RGBA_INTEGER = 0x8D99;
			public const uint GL_BGR_INTEGER = 0x8D9A;
			public const uint GL_BGRA_INTEGER = 0x8D9B;
			public const uint GL_SAMPLER_1D_ARRAY = 0x8DC0;
			public const uint GL_SAMPLER_2D_ARRAY = 0x8DC1;
			public const uint GL_SAMPLER_1D_ARRAY_SHADOW = 0x8DC3;
			public const uint GL_SAMPLER_2D_ARRAY_SHADOW = 0x8DC4;
			public const uint GL_SAMPLER_CUBE_SHADOW = 0x8DC5;
			public const uint GL_UNSIGNED_INT_VEC2 = 0x8DC6;
			public const uint GL_UNSIGNED_INT_VEC3 = 0x8DC7;
			public const uint GL_UNSIGNED_INT_VEC4 = 0x8DC8;
			public const uint GL_INT_SAMPLER_1D = 0x8DC9;
			public const uint GL_INT_SAMPLER_2D = 0x8DCA;
			public const uint GL_INT_SAMPLER_3D = 0x8DCB;
			public const uint GL_INT_SAMPLER_CUBE = 0x8DCC;
			public const uint GL_INT_SAMPLER_1D_ARRAY = 0x8DCE;
			public const uint GL_INT_SAMPLER_2D_ARRAY = 0x8DCF;
			public const uint GL_UNSIGNED_INT_SAMPLER_1D = 0x8DD1;
			public const uint GL_UNSIGNED_INT_SAMPLER_2D = 0x8DD2;
			public const uint GL_UNSIGNED_INT_SAMPLER_3D = 0x8DD3;
			public const uint GL_UNSIGNED_INT_SAMPLER_CUBE = 0x8DD4;
			public const uint GL_UNSIGNED_INT_SAMPLER_1D_ARRAY = 0x8DD6;
			public const uint GL_UNSIGNED_INT_SAMPLER_2D_ARRAY = 0x8DD7;
			public const uint GL_QUERY_WAIT = 0x8E13;
			public const uint GL_QUERY_NO_WAIT = 0x8E14;
			public const uint GL_QUERY_BY_REGION_WAIT = 0x8E15;
			public const uint GL_QUERY_BY_REGION_NO_WAIT = 0x8E16;
			public const uint GL_BUFFER_ACCESS_FLAGS = 0x911F;
			public const uint GL_BUFFER_MAP_LENGTH = 0x9120;
			public const uint GL_BUFFER_MAP_OFFSET = 0x9121;
			#endregion

			#region OpenGL 3.1

			public const uint GL_SAMPLER_2D_RECT = 0x8B63;
			public const uint GL_SAMPLER_2D_RECT_SHADOW = 0x8B64;
			public const uint GL_SAMPLER_BUFFER = 0x8DC2;
			public const uint GL_INT_SAMPLER_2D_RECT = 0x8DCD;
			public const uint GL_INT_SAMPLER_BUFFER = 0x8DD0;
			public const uint GL_UNSIGNED_INT_SAMPLER_2D_RECT = 0x8DD5;
			public const uint GL_UNSIGNED_INT_SAMPLER_BUFFER = 0x8DD8;
			public const uint GL_TEXTURE_BUFFER = 0x8C2A;
			public const uint GL_MAX_TEXTURE_BUFFER_SIZE = 0x8C2B;
			public const uint GL_TEXTURE_BINDING_BUFFER = 0x8C2C;
			public const uint GL_TEXTURE_BUFFER_DATA_STORE_BINDING = 0x8C2D;
			public const uint GL_TEXTURE_BUFFER_FORMAT = 0x8C2E;
			public const uint GL_TEXTURE_RECTANGLE = 0x84F5;
			public const uint GL_TEXTURE_BINDING_RECTANGLE = 0x84F6;
			public const uint GL_PROXY_TEXTURE_RECTANGLE = 0x84F7;
			public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE = 0x84F8;
			public const uint GL_RED_SNORM = 0x8F90;
			public const uint GL_RG_SNORM = 0x8F91;
			public const uint GL_RGB_SNORM = 0x8F92;
			public const uint GL_RGBA_SNORM = 0x8F93;
			public const uint GL_R8_SNORM = 0x8F94;
			public const uint GL_RG8_SNORM = 0x8F95;
			public const uint GL_RGB8_SNORM = 0x8F96;
			public const uint GL_RGBA8_SNORM = 0x8F97;
			public const uint GL_R16_SNORM = 0x8F98;
			public const uint GL_RG16_SNORM = 0x8F99;
			public const uint GL_RGB16_SNORM = 0x8F9A;
			public const uint GL_RGBA16_SNORM = 0x8F9B;
			public const uint GL_SIGNED_NORMALIZED = 0x8F9C;
			public const uint GL_PRIMITIVE_RESTART = 0x8F9D;
			public const uint GL_PRIMITIVE_RESTART_INDEX = 0x8F9E;

			#endregion

			#region OpenGL 3.2

			public const uint GL_CONTEXT_CORE_PROFILE_BIT = 0x00000001;
			public const uint GL_CONTEXT_COMPATIBILITY_PROFILE_BIT = 0x00000002;
			public const uint GL_LINES_ADJACENCY = 0x000A;
			public const uint GL_LINE_STRIP_ADJACENCY = 0x000B;
			public const uint GL_TRIANGLES_ADJACENCY = 0x000C;
			public const uint GL_TRIANGLE_STRIP_ADJACENCY = 0x000D;
			public const uint GL_PROGRAM_POINT_SIZE = 0x8642;
			public const uint GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS = 0x8C29;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_LAYERED = 0x8DA7;
			public const uint GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS = 0x8DA8;
			public const uint GL_GEOMETRY_SHADER = 0x8DD9;
			public const uint GL_GEOMETRY_VERTICES_OUT = 0x8916;
			public const uint GL_GEOMETRY_INPUT_TYPE = 0x8917;
			public const uint GL_GEOMETRY_OUTPUT_TYPE = 0x8918;
			public const uint GL_MAX_GEOMETRY_UNIFORM_COMPONENTS = 0x8DDF;
			public const uint GL_MAX_GEOMETRY_OUTPUT_VERTICES = 0x8DE0;
			public const uint GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS = 0x8DE1;
			public const uint GL_MAX_VERTEX_OUTPUT_COMPONENTS = 0x9122;
			public const uint GL_MAX_GEOMETRY_INPUT_COMPONENTS = 0x9123;
			public const uint GL_MAX_GEOMETRY_OUTPUT_COMPONENTS = 0x9124;
			public const uint GL_MAX_FRAGMENT_INPUT_COMPONENTS = 0x9125;
			public const uint GL_CONTEXT_PROFILE_MASK = 0x9126;

			#endregion

			#region GL_EXT_texture
			public const uint GL_ALPHA4_EXT = 0x803B;
			public const uint GL_ALPHA8_EXT = 0x803C;
			public const uint GL_ALPHA12_EXT = 0x803D;
			public const uint GL_ALPHA16_EXT = 0x803E;
			public const uint GL_LUMINANCE4_EXT = 0x803F;
			public const uint GL_LUMINANCE8_EXT = 0x8040;
			public const uint GL_LUMINANCE12_EXT = 0x8041;
			public const uint GL_LUMINANCE16_EXT = 0x8042;
			public const uint GL_LUMINANCE4_ALPHA4_EXT = 0x8043;
			public const uint GL_LUMINANCE6_ALPHA2_EXT = 0x8044;
			public const uint GL_LUMINANCE8_ALPHA8_EXT = 0x8045;
			public const uint GL_LUMINANCE12_ALPHA4_EXT = 0x8046;
			public const uint GL_LUMINANCE12_ALPHA12_EXT = 0x8047;
			public const uint GL_LUMINANCE16_ALPHA16_EXT = 0x8048;
			public const uint GL_INTENSITY_EXT = 0x8049;
			public const uint GL_INTENSITY4_EXT = 0x804A;
			public const uint GL_INTENSITY8_EXT = 0x804B;
			public const uint GL_INTENSITY12_EXT = 0x804C;
			public const uint GL_INTENSITY16_EXT = 0x804D;
			public const uint GL_RGB2_EXT = 0x804E;
			public const uint GL_RGB4_EXT = 0x804F;
			public const uint GL_RGB5_EXT = 0x8050;
			public const uint GL_RGB8_EXT = 0x8051;
			public const uint GL_RGB10_EXT = 0x8052;
			public const uint GL_RGB12_EXT = 0x8053;
			public const uint GL_RGB16_EXT = 0x8054;
			public const uint GL_RGBA2_EXT = 0x8055;
			public const uint GL_RGBA4_EXT = 0x8056;
			public const uint GL_RGB5_A1_EXT = 0x8057;
			public const uint GL_RGBA8_EXT = 0x8058;
			public const uint GL_RGB10_A2_EXT = 0x8059;
			public const uint GL_RGBA12_EXT = 0x805A;
			public const uint GL_RGBA16_EXT = 0x805B;
			public const uint GL_TEXTURE_RED_SIZE_EXT = 0x805C;
			public const uint GL_TEXTURE_GREEN_SIZE_EXT = 0x805D;
			public const uint GL_TEXTURE_BLUE_SIZE_EXT = 0x805E;
			public const uint GL_TEXTURE_ALPHA_SIZE_EXT = 0x805F;
			public const uint GL_TEXTURE_LUMINANCE_SIZE_EXT = 0x8060;
			public const uint GL_TEXTURE_INTENSITY_SIZE_EXT = 0x8061;
			public const uint GL_REPLACE_EXT = 0x8062;
			public const uint GL_PROXY_TEXTURE_1D_EXT = 0x8063;
			public const uint GL_PROXY_TEXTURE_2D_EXT = 0x8064;
			public const uint GL_TEXTURE_TOO_LARGE_EXT = 0x8065;
			#endregion

			#region GL_half_float
			public const uint GL_HALF_FLOAT = 0x140B;
			#endregion

			#region GL_EXT_texture_cube_map
			public const uint GL_TEXTURE_CUBE_MAP_EXT = 0x8513;
			public const uint GL_TEXTURE_BINDING_CUBE_MAP_EXT = 0x8514;
			public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X_EXT = 0x8515;
			public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X_EXT = 0x8516;
			public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y_EXT = 0x8517;
			public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y_EXT = 0x8518;
			public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z_EXT = 0x8519;
			public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z_EXT = 0x851A;
			public const uint GL_PROXY_TEXTURE_CUBE_MAP_EXT = 0x851B;
			public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE_EXT = 0x851C;
			#endregion

			#region GL_ARB_framebuffer
			public const uint GL_FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING = 0x8210;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE = 0x8211;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_RED_SIZE = 0x8212;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_GREEN_SIZE = 0x8213;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_BLUE_SIZE = 0x8214;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE = 0x8215;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE = 0x8216;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE = 0x8217;
			public const uint GL_FRAMEBUFFER_DEFAULT = 0x8218;
			public const uint GL_FRAMEBUFFER_UNDEFINED = 0x8219;
			public const uint GL_DEPTH_STENCIL_ATTACHMENT = 0x821A;
			public const uint GL_MAX_RENDERBUFFER_SIZE = 0x84E8;
			public const uint GL_DEPTH_STENCIL = 0x84F9;
			public const uint GL_UNSIGNED_INT_24_8 = 0x84FA;
			public const uint GL_DEPTH24_STENCIL8 = 0x88F0;
			public const uint GL_TEXTURE_STENCIL_SIZE = 0x88F1;
			public const uint GL_TEXTURE_RED_TYPE = 0x8C10;
			public const uint GL_TEXTURE_GREEN_TYPE = 0x8C11;
			public const uint GL_TEXTURE_BLUE_TYPE = 0x8C12;
			public const uint GL_TEXTURE_ALPHA_TYPE = 0x8C13;
			public const uint GL_TEXTURE_DEPTH_TYPE = 0x8C16;
			public const uint GL_UNSIGNED_NORMALIZED = 0x8C17;
			public const uint GL_FRAMEBUFFER_BINDING = 0x8CA6;
			public const uint GL_DRAW_FRAMEBUFFER_BINDING = GL_FRAMEBUFFER_BINDING;
			public const uint GL_RENDERBUFFER_BINDING = 0x8CA7;
			public const uint GL_READ_FRAMEBUFFER = 0x8CA8;
			public const uint GL_DRAW_FRAMEBUFFER = 0x8CA9;
			public const uint GL_READ_FRAMEBUFFER_BINDING = 0x8CAA;
			public const uint GL_RENDERBUFFER_SAMPLES = 0x8CAB;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;
			public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER = 0x8CD4;
			public const uint GL_FRAMEBUFFER_COMPLETE = 0x8CD5;
			public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
			public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
			public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER = 0x8CDB;
			public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER = 0x8CDC;
			public const uint GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
			public const uint GL_MAX_COLOR_ATTACHMENTS = 0x8CDF;
			public const uint GL_COLOR_ATTACHMENT0 = 0x8CE0;
			public const uint GL_COLOR_ATTACHMENT1 = 0x8CE1;
			public const uint GL_COLOR_ATTACHMENT2 = 0x8CE2;
			public const uint GL_COLOR_ATTACHMENT3 = 0x8CE3;
			public const uint GL_COLOR_ATTACHMENT4 = 0x8CE4;
			public const uint GL_COLOR_ATTACHMENT5 = 0x8CE5;
			public const uint GL_COLOR_ATTACHMENT6 = 0x8CE6;
			public const uint GL_COLOR_ATTACHMENT7 = 0x8CE7;
			public const uint GL_COLOR_ATTACHMENT8 = 0x8CE8;
			public const uint GL_COLOR_ATTACHMENT9 = 0x8CE9;
			public const uint GL_COLOR_ATTACHMENT10 = 0x8CEA;
			public const uint GL_COLOR_ATTACHMENT11 = 0x8CEB;
			public const uint GL_COLOR_ATTACHMENT12 = 0x8CEC;
			public const uint GL_COLOR_ATTACHMENT13 = 0x8CED;
			public const uint GL_COLOR_ATTACHMENT14 = 0x8CEE;
			public const uint GL_COLOR_ATTACHMENT15 = 0x8CEF;
			public const uint GL_DEPTH_ATTACHMENT = 0x8D00;
			public const uint GL_STENCIL_ATTACHMENT = 0x8D20;
			public const uint GL_FRAMEBUFFER = 0x8D40;
			public const uint GL_RENDERBUFFER = 0x8D41;
			public const uint GL_RENDERBUFFER_WIDTH = 0x8D42;
			public const uint GL_RENDERBUFFER_HEIGHT = 0x8D43;
			public const uint GL_RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
			public const uint GL_STENCIL_INDEX1 = 0x8D46;
			public const uint GL_STENCIL_INDEX4 = 0x8D47;
			public const uint GL_STENCIL_INDEX8 = 0x8D48;
			public const uint GL_STENCIL_INDEX16 = 0x8D49;
			public const uint GL_RENDERBUFFER_RED_SIZE = 0x8D50;
			public const uint GL_RENDERBUFFER_GREEN_SIZE = 0x8D51;
			public const uint GL_RENDERBUFFER_BLUE_SIZE = 0x8D52;
			public const uint GL_RENDERBUFFER_ALPHA_SIZE = 0x8D53;
			public const uint GL_RENDERBUFFER_DEPTH_SIZE = 0x8D54;
			public const uint GL_RENDERBUFFER_STENCIL_SIZE = 0x8D55;
			public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;
			public const uint GL_MAX_SAMPLES = 0x8D57;
			#endregion
		}
	}
}
