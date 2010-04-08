using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Graphics.OpenGL
{
	public static partial class OpenGL
	{
		public static class Const
		{

			public const uint GL_ACCUM = 0x0100;
			public const uint GL_LOAD = 0x0101;
			public const uint GL_RETURN = 0x0102;
			public const uint GL_MULT = 0x0103;
			public const uint GL_ADD = 0x0104;



			public const uint GL_NEVER = 0x0200;
			public const uint GL_LESS = 0x0201;
			public const uint GL_EQUAL = 0x0202;
			public const uint GL_LEQUAL = 0x0203;
			public const uint GL_GREATER = 0x0204;
			public const uint GL_NOTEQUAL = 0x0205;
			public const uint GL_GEQUAL = 0x0206;
			public const uint GL_ALWAYS = 0x0207;



			public const uint GL_CURRENT_BIT = 0x00000001;
			public const uint GL_POINT_BIT = 0x00000002;
			public const uint GL_LINE_BIT = 0x00000004;
			public const uint GL_POLYGON_BIT = 0x00000008;
			public const uint GL_POLYGON_STIPPLE_BIT = 0x00000010;
			public const uint GL_PIXEL_MODE_BIT = 0x00000020;
			public const uint GL_LIGHTING_BIT = 0x00000040;
			public const uint GL_FOG_BIT = 0x00000080;
			public const uint GL_DEPTH_BUFFER_BIT = 0x00000100;
			public const uint GL_ACCUM_BUFFER_BIT = 0x00000200;
			public const uint GL_STENCIL_BUFFER_BIT = 0x00000400;
			public const uint GL_VIEWPORT_BIT = 0x00000800;
			public const uint GL_TRANSFORM_BIT = 0x00001000;
			public const uint GL_ENABLE_BIT = 0x00002000;
			public const uint GL_COLOR_BUFFER_BIT = 0x00004000;
			public const uint GL_HINT_BIT = 0x00008000;
			public const uint GL_EVAL_BIT = 0x00010000;
			public const uint GL_LIST_BIT = 0x00020000;
			public const uint GL_TEXTURE_BIT = 0x00040000;
			public const uint GL_SCISSOR_BIT = 0x00080000;
			public const uint GL_ALL_ATTRIB_BITS = 0x000fffff;


			public const uint GL_POINTS = 0x0000;
			public const uint GL_LINES = 0x0001;
			public const uint GL_LINE_LOOP = 0x0002;
			public const uint GL_LINE_STRIP = 0x0003;
			public const uint GL_TRIANGLES = 0x0004;
			public const uint GL_TRIANGLE_STRIP = 0x0005;
			public const uint GL_TRIANGLE_FAN = 0x0006;
			public const uint GL_QUADS = 0x0007;
			public const uint GL_QUAD_STRIP = 0x0008;
			public const uint GL_POLYGON = 0x0009;



			public const uint GL_ZERO = 0;
			public const uint GL_ONE = 1;
			public const uint GL_SRC_COLOR = 0x0300;
			public const uint GL_ONE_MINUS_SRC_COLOR = 0x0301;
			public const uint GL_SRC_ALPHA = 0x0302;
			public const uint GL_ONE_MINUS_SRC_ALPHA = 0x0303;
			public const uint GL_DST_ALPHA = 0x0304;
			public const uint GL_ONE_MINUS_DST_ALPHA = 0x0305;



			public const uint GL_DST_COLOR = 0x0306;
			public const uint GL_ONE_MINUS_DST_COLOR = 0x0307;
			public const uint GL_SRC_ALPHA_SATURATE = 0x0308;


			/* ClearBufferMask */
			/*      GL_COLOR_BUFFER_BIT */
			/*      GL_ACCUM_BUFFER_BIT */
			/*      GL_STENCIL_BUFFER_BIT */
			/*      GL_DEPTH_BUFFER_BIT */

			/* ClientArrayType */
			/*      GL_VERTEX_ARRAY */
			/*      GL_NORMAL_ARRAY */
			/*      GL_COLOR_ARRAY */
			/*      GL_INDEX_ARRAY */
			/*      GL_TEXTURE_COORD_ARRAY */
			/*      GL_EDGE_FLAG_ARRAY */


			public const uint GL_CLIP_PLANE0 = 0x3000;
			public const uint GL_CLIP_PLANE1 = 0x3001;
			public const uint GL_CLIP_PLANE2 = 0x3002;
			public const uint GL_CLIP_PLANE3 = 0x3003;
			public const uint GL_CLIP_PLANE4 = 0x3004;
			public const uint GL_CLIP_PLANE5 = 0x3005;


			/* ColorMaterialFace */
			/*      GL_FRONT */
			/*      GL_BACK */
			/*      GL_FRONT_AND_BACK */

			/* ColorMaterialParameter */
			/*      GL_AMBIENT */
			/*      GL_DIFFUSE */
			/*      GL_SPECULAR */
			/*      GL_EMISSION */
			/*      GL_AMBIENT_AND_DIFFUSE */

			/* ColorPointerType */
			/*      GL_BYTE */
			/*      GL_UNSIGNED_BYTE */
			/*      GL_SHORT */
			/*      GL_UNSIGNED_SHORT */
			/*      GL_INT */
			/*      GL_UNSIGNED_INT */
			/*      GL_FLOAT */
			/*      GL_DOUBLE */

			/* CullFaceMode */
			/*      GL_FRONT */
			/*      GL_BACK */
			/*      GL_FRONT_AND_BACK */


			public const uint GL_BYTE = 0x1400;
			public const uint GL_UNSIGNED_BYTE = 0x1401;
			public const uint GL_SHORT = 0x1402;
			public const uint GL_UNSIGNED_SHORT = 0x1403;
			public const uint GL_INT = 0x1404;
			public const uint GL_UNSIGNED_INT = 0x1405;
			public const uint GL_FLOAT = 0x1406;
			public const uint GL_2_BYTES = 0x1407;
			public const uint GL_3_BYTES = 0x1408;
			public const uint GL_4_BYTES = 0x1409;
			public const uint GL_DOUBLE = 0x140A;

			/* DepthFunction */
			/*      GL_NEVER */
			/*      GL_LESS */
			/*      GL_EQUAL */
			/*      GL_LEQUAL */
			/*      GL_GREATER */
			/*      GL_NOTEQUAL */
			/*      GL_GEQUAL */
			/*      GL_ALWAYS */


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
			public const uint GL_AUX0 = 0x0409;
			public const uint GL_AUX1 = 0x040A;
			public const uint GL_AUX2 = 0x040B;
			public const uint GL_AUX3 = 0x040C;

			/* Enable */
			/*      GL_FOG */
			/*      GL_LIGHTING */
			/*      GL_TEXTURE_1D */
			/*      GL_TEXTURE_2D */
			/*      GL_LINE_STIPPLE */
			/*      GL_POLYGON_STIPPLE */
			/*      GL_CULL_FACE */
			/*      GL_ALPHA_TEST */
			/*      GL_BLEND */
			/*      GL_INDEX_LOGIC_OP */
			/*      GL_COLOR_LOGIC_OP */
			/*      GL_DITHER */
			/*      GL_STENCIL_TEST */
			/*      GL_DEPTH_TEST */
			/*      GL_CLIP_PLANE0 */
			/*      GL_CLIP_PLANE1 */
			/*      GL_CLIP_PLANE2 */
			/*      GL_CLIP_PLANE3 */
			/*      GL_CLIP_PLANE4 */
			/*      GL_CLIP_PLANE5 */
			/*      GL_LIGHT0 */
			/*      GL_LIGHT1 */
			/*      GL_LIGHT2 */
			/*      GL_LIGHT3 */
			/*      GL_LIGHT4 */
			/*      GL_LIGHT5 */
			/*      GL_LIGHT6 */
			/*      GL_LIGHT7 */
			/*      GL_TEXTURE_GEN_S */
			/*      GL_TEXTURE_GEN_T */
			/*      GL_TEXTURE_GEN_R */
			/*      GL_TEXTURE_GEN_Q */
			/*      GL_MAP1_VERTEX_3 */
			/*      GL_MAP1_VERTEX_4 */
			/*      GL_MAP1_COLOR_4 */
			/*      GL_MAP1_INDEX */
			/*      GL_MAP1_NORMAL */
			/*      GL_MAP1_TEXTURE_COORD_1 */
			/*      GL_MAP1_TEXTURE_COORD_2 */
			/*      GL_MAP1_TEXTURE_COORD_3 */
			/*      GL_MAP1_TEXTURE_COORD_4 */
			/*      GL_MAP2_VERTEX_3 */
			/*      GL_MAP2_VERTEX_4 */
			/*      GL_MAP2_COLOR_4 */
			/*      GL_MAP2_INDEX */
			/*      GL_MAP2_NORMAL */
			/*      GL_MAP2_TEXTURE_COORD_1 */
			/*      GL_MAP2_TEXTURE_COORD_2 */
			/*      GL_MAP2_TEXTURE_COORD_3 */
			/*      GL_MAP2_TEXTURE_COORD_4 */
			/*      GL_POINT_SMOOTH */
			/*      GL_LINE_SMOOTH */
			/*      GL_POLYGON_SMOOTH */
			/*      GL_SCISSOR_TEST */
			/*      GL_COLOR_MATERIAL */
			/*      GL_NORMALIZE */
			/*      GL_AUTO_NORMAL */
			/*      GL_VERTEX_ARRAY */
			/*      GL_NORMAL_ARRAY */
			/*      GL_COLOR_ARRAY */
			/*      GL_INDEX_ARRAY */
			/*      GL_TEXTURE_COORD_ARRAY */
			/*      GL_EDGE_FLAG_ARRAY */
			/*      GL_POLYGON_OFFSET_POINT */
			/*      GL_POLYGON_OFFSET_LINE */
			/*      GL_POLYGON_OFFSET_FILL */


			public const uint GL_NO_ERROR = 0;
			public const uint GL_INVALID_ENUM = 0x0500;
			public const uint GL_INVALID_VALUE = 0x0501;
			public const uint GL_INVALID_OPERATION = 0x0502;
			public const uint GL_STACK_OVERFLOW = 0x0503;
			public const uint GL_STACK_UNDERFLOW = 0x0504;
			public const uint GL_OUT_OF_MEMORY = 0x0505;



			public const uint GL_2D = 0x0600;
			public const uint GL_3D = 0x0601;
			public const uint GL_3D_COLOR = 0x0602;
			public const uint GL_3D_COLOR_TEXTURE = 0x0603;
			public const uint GL_4D_COLOR_TEXTURE = 0x0604;


			public const uint GL_PASS_THROUGH_TOKEN = 0x0700;
			public const uint GL_POINT_TOKEN = 0x0701;
			public const uint GL_LINE_TOKEN = 0x0702;
			public const uint GL_POLYGON_TOKEN = 0x0703;
			public const uint GL_BITMAP_TOKEN = 0x0704;
			public const uint GL_DRAW_PIXEL_TOKEN = 0x0705;
			public const uint GL_COPY_PIXEL_TOKEN = 0x0706;
			public const uint GL_LINE_RESET_TOKEN = 0x0707;



			/*      GL_LINEAR */
			public const uint GL_EXP = 0x0800;
			public const uint GL_EXP2 = 0x0801;



			/* FogParameter */
			/*      GL_FOG_COLOR */
			/*      GL_FOG_DENSITY */
			/*      GL_FOG_END */
			/*      GL_FOG_INDEX */
			/*      GL_FOG_MODE */
			/*      GL_FOG_START */


			public const uint GL_CW = 0x0900;
			public const uint GL_CCW = 0x0901;



			public const uint GL_COEFF = 0x0A00;
			public const uint GL_ORDER = 0x0A01;
			public const uint GL_DOMAIN = 0x0A02;


			/* GetPixelMap */
			/*      GL_PIXEL_MAP_I_TO_I */
			/*      GL_PIXEL_MAP_S_TO_S */
			/*      GL_PIXEL_MAP_I_TO_R */
			/*      GL_PIXEL_MAP_I_TO_G */
			/*      GL_PIXEL_MAP_I_TO_B */
			/*      GL_PIXEL_MAP_I_TO_A */
			/*      GL_PIXEL_MAP_R_TO_R */
			/*      GL_PIXEL_MAP_G_TO_G */
			/*      GL_PIXEL_MAP_B_TO_B */
			/*      GL_PIXEL_MAP_A_TO_A */

			/* GetPointerTarget */
			/*      GL_VERTEX_ARRAY_POINTER */
			/*      GL_NORMAL_ARRAY_POINTER */
			/*      GL_COLOR_ARRAY_POINTER */
			/*      GL_INDEX_ARRAY_POINTER */
			/*      GL_TEXTURE_COORD_ARRAY_POINTER */
			/*      GL_EDGE_FLAG_ARRAY_POINTER */


			public const uint GL_CURRENT_COLOR = 0x0B00;
			public const uint GL_CURRENT_INDEX = 0x0B01;
			public const uint GL_CURRENT_NORMAL = 0x0B02;
			public const uint GL_CURRENT_TEXTURE_COORDS = 0x0B03;
			public const uint GL_CURRENT_RASTER_COLOR = 0x0B04;
			public const uint GL_CURRENT_RASTER_INDEX = 0x0B05;
			public const uint GL_CURRENT_RASTER_TEXTURE_COORDS = 0x0B06;
			public const uint GL_CURRENT_RASTER_POSITION = 0x0B07;
			public const uint GL_CURRENT_RASTER_POSITION_VALID = 0x0B08;
			public const uint GL_CURRENT_RASTER_DISTANCE = 0x0B09;
			public const uint GL_POINT_SMOOTH = 0x0B10;
			public const uint GL_POINT_SIZE = 0x0B11;
			public const uint GL_POINT_SIZE_RANGE = 0x0B12;
			public const uint GL_POINT_SIZE_GRANULARITY = 0x0B13;
			public const uint GL_LINE_SMOOTH = 0x0B20;
			public const uint GL_LINE_WIDTH = 0x0B21;
			public const uint GL_LINE_WIDTH_RANGE = 0x0B22;
			public const uint GL_LINE_WIDTH_GRANULARITY = 0x0B23;
			public const uint GL_LINE_STIPPLE = 0x0B24;
			public const uint GL_LINE_STIPPLE_PATTERN = 0x0B25;
			public const uint GL_LINE_STIPPLE_REPEAT = 0x0B26;
			public const uint GL_LIST_MODE = 0x0B30;
			public const uint GL_MAX_LIST_NESTING = 0x0B31;
			public const uint GL_LIST_BASE = 0x0B32;
			public const uint GL_LIST_INDEX = 0x0B33;
			public const uint GL_POLYGON_MODE = 0x0B40;
			public const uint GL_POLYGON_SMOOTH = 0x0B41;
			public const uint GL_POLYGON_STIPPLE = 0x0B42;
			public const uint GL_EDGE_FLAG = 0x0B43;
			public const uint GL_CULL_FACE = 0x0B44;
			public const uint GL_CULL_FACE_MODE = 0x0B45;
			public const uint GL_FRONT_FACE = 0x0B46;
			public const uint GL_LIGHTING = 0x0B50;
			public const uint GL_LIGHT_MODEL_LOCAL_VIEWER = 0x0B51;
			public const uint GL_LIGHT_MODEL_TWO_SIDE = 0x0B52;
			public const uint GL_LIGHT_MODEL_AMBIENT = 0x0B53;
			public const uint GL_SHADE_MODEL = 0x0B54;
			public const uint GL_COLOR_MATERIAL_FACE = 0x0B55;
			public const uint GL_COLOR_MATERIAL_PARAMETER = 0x0B56;
			public const uint GL_COLOR_MATERIAL = 0x0B57;
			public const uint GL_FOG = 0x0B60;
			public const uint GL_FOG_INDEX = 0x0B61;
			public const uint GL_FOG_DENSITY = 0x0B62;
			public const uint GL_FOG_START = 0x0B63;
			public const uint GL_FOG_END = 0x0B64;
			public const uint GL_FOG_MODE = 0x0B65;
			public const uint GL_FOG_COLOR = 0x0B66;
			public const uint GL_DEPTH_RANGE = 0x0B70;
			public const uint GL_DEPTH_TEST = 0x0B71;
			public const uint GL_DEPTH_WRITEMASK = 0x0B72;
			public const uint GL_DEPTH_CLEAR_VALUE = 0x0B73;
			public const uint GL_DEPTH_FUNC = 0x0B74;
			public const uint GL_ACCUM_CLEAR_VALUE = 0x0B80;
			public const uint GL_STENCIL_TEST = 0x0B90;
			public const uint GL_STENCIL_CLEAR_VALUE = 0x0B91;
			public const uint GL_STENCIL_FUNC = 0x0B92;
			public const uint GL_STENCIL_VALUE_MASK = 0x0B93;
			public const uint GL_STENCIL_FAIL = 0x0B94;
			public const uint GL_STENCIL_PASS_DEPTH_FAIL = 0x0B95;
			public const uint GL_STENCIL_PASS_DEPTH_PASS = 0x0B96;
			public const uint GL_STENCIL_REF = 0x0B97;
			public const uint GL_STENCIL_WRITEMASK = 0x0B98;
			public const uint GL_MATRIX_MODE = 0x0BA0;
			public const uint GL_NORMALIZE = 0x0BA1;
			public const uint GL_VIEWPORT = 0x0BA2;
			public const uint GL_MODELVIEW_STACK_DEPTH = 0x0BA3;
			public const uint GL_PROJECTION_STACK_DEPTH = 0x0BA4;
			public const uint GL_TEXTURE_STACK_DEPTH = 0x0BA5;
			public const uint GL_MODELVIEW_MATRIX = 0x0BA6;
			public const uint GL_PROJECTION_MATRIX = 0x0BA7;
			public const uint GL_TEXTURE_MATRIX = 0x0BA8;
			public const uint GL_ATTRIB_STACK_DEPTH = 0x0BB0;
			public const uint GL_CLIENT_ATTRIB_STACK_DEPTH = 0x0BB1;
			public const uint GL_ALPHA_TEST = 0x0BC0;
			public const uint GL_ALPHA_TEST_FUNC = 0x0BC1;
			public const uint GL_ALPHA_TEST_REF = 0x0BC2;
			public const uint GL_DITHER = 0x0BD0;
			public const uint GL_BLEND_DST = 0x0BE0;
			public const uint GL_BLEND_SRC = 0x0BE1;
			public const uint GL_BLEND = 0x0BE2;
			public const uint GL_LOGIC_OP_MODE = 0x0BF0;
			public const uint GL_INDEX_LOGIC_OP = 0x0BF1;
			public const uint GL_COLOR_LOGIC_OP = 0x0BF2;
			public const uint GL_AUX_BUFFERS = 0x0C00;
			public const uint GL_DRAW_BUFFER = 0x0C01;
			public const uint GL_READ_BUFFER = 0x0C02;
			public const uint GL_SCISSOR_BOX = 0x0C10;
			public const uint GL_SCISSOR_TEST = 0x0C11;
			public const uint GL_INDEX_CLEAR_VALUE = 0x0C20;
			public const uint GL_INDEX_WRITEMASK = 0x0C21;
			public const uint GL_COLOR_CLEAR_VALUE = 0x0C22;
			public const uint GL_COLOR_WRITEMASK = 0x0C23;
			public const uint GL_INDEX_MODE = 0x0C30;
			public const uint GL_RGBA_MODE = 0x0C31;
			public const uint GL_DOUBLEBUFFER = 0x0C32;
			public const uint GL_STEREO = 0x0C33;
			public const uint GL_RENDER_MODE = 0x0C40;
			public const uint GL_PERSPECTIVE_CORRECTION_HINT = 0x0C50;
			public const uint GL_POINT_SMOOTH_HINT = 0x0C51;
			public const uint GL_LINE_SMOOTH_HINT = 0x0C52;
			public const uint GL_POLYGON_SMOOTH_HINT = 0x0C53;
			public const uint GL_FOG_HINT = 0x0C54;
			public const uint GL_TEXTURE_GEN_S = 0x0C60;
			public const uint GL_TEXTURE_GEN_T = 0x0C61;
			public const uint GL_TEXTURE_GEN_R = 0x0C62;
			public const uint GL_TEXTURE_GEN_Q = 0x0C63;
			public const uint GL_PIXEL_MAP_I_TO_I = 0x0C70;
			public const uint GL_PIXEL_MAP_S_TO_S = 0x0C71;
			public const uint GL_PIXEL_MAP_I_TO_R = 0x0C72;
			public const uint GL_PIXEL_MAP_I_TO_G = 0x0C73;
			public const uint GL_PIXEL_MAP_I_TO_B = 0x0C74;
			public const uint GL_PIXEL_MAP_I_TO_A = 0x0C75;
			public const uint GL_PIXEL_MAP_R_TO_R = 0x0C76;
			public const uint GL_PIXEL_MAP_G_TO_G = 0x0C77;
			public const uint GL_PIXEL_MAP_B_TO_B = 0x0C78;
			public const uint GL_PIXEL_MAP_A_TO_A = 0x0C79;
			public const uint GL_PIXEL_MAP_I_TO_I_SIZE = 0x0CB0;
			public const uint GL_PIXEL_MAP_S_TO_S_SIZE = 0x0CB1;
			public const uint GL_PIXEL_MAP_I_TO_R_SIZE = 0x0CB2;
			public const uint GL_PIXEL_MAP_I_TO_G_SIZE = 0x0CB3;
			public const uint GL_PIXEL_MAP_I_TO_B_SIZE = 0x0CB4;
			public const uint GL_PIXEL_MAP_I_TO_A_SIZE = 0x0CB5;
			public const uint GL_PIXEL_MAP_R_TO_R_SIZE = 0x0CB6;
			public const uint GL_PIXEL_MAP_G_TO_G_SIZE = 0x0CB7;
			public const uint GL_PIXEL_MAP_B_TO_B_SIZE = 0x0CB8;
			public const uint GL_PIXEL_MAP_A_TO_A_SIZE = 0x0CB9;
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
			public const uint GL_MAP_COLOR = 0x0D10;
			public const uint GL_MAP_STENCIL = 0x0D11;
			public const uint GL_INDEX_SHIFT = 0x0D12;
			public const uint GL_INDEX_OFFSET = 0x0D13;
			public const uint GL_RED_SCALE = 0x0D14;
			public const uint GL_RED_BIAS = 0x0D15;
			public const uint GL_ZOOM_X = 0x0D16;
			public const uint GL_ZOOM_Y = 0x0D17;
			public const uint GL_GREEN_SCALE = 0x0D18;
			public const uint GL_GREEN_BIAS = 0x0D19;
			public const uint GL_BLUE_SCALE = 0x0D1A;
			public const uint GL_BLUE_BIAS = 0x0D1B;
			public const uint GL_ALPHA_SCALE = 0x0D1C;
			public const uint GL_ALPHA_BIAS = 0x0D1D;
			public const uint GL_DEPTH_SCALE = 0x0D1E;
			public const uint GL_DEPTH_BIAS = 0x0D1F;
			public const uint GL_MAX_EVAL_ORDER = 0x0D30;
			public const uint GL_MAX_LIGHTS = 0x0D31;
			public const uint GL_MAX_CLIP_PLANES = 0x0D32;
			public const uint GL_MAX_TEXTURE_SIZE = 0x0D33;
			public const uint GL_MAX_PIXEL_MAP_TABLE = 0x0D34;
			public const uint GL_MAX_ATTRIB_STACK_DEPTH = 0x0D35;
			public const uint GL_MAX_MODELVIEW_STACK_DEPTH = 0x0D36;
			public const uint GL_MAX_NAME_STACK_DEPTH = 0x0D37;
			public const uint GL_MAX_PROJECTION_STACK_DEPTH = 0x0D38;
			public const uint GL_MAX_TEXTURE_STACK_DEPTH = 0x0D39;
			public const uint GL_MAX_VIEWPORT_DIMS = 0x0D3A;
			public const uint GL_MAX_CLIENT_ATTRIB_STACK_DEPTH = 0x0D3B;
			public const uint GL_SUBPIXEL_BITS = 0x0D50;
			public const uint GL_INDEX_BITS = 0x0D51;
			public const uint GL_RED_BITS = 0x0D52;
			public const uint GL_GREEN_BITS = 0x0D53;
			public const uint GL_BLUE_BITS = 0x0D54;
			public const uint GL_ALPHA_BITS = 0x0D55;
			public const uint GL_DEPTH_BITS = 0x0D56;
			public const uint GL_STENCIL_BITS = 0x0D57;
			public const uint GL_ACCUM_RED_BITS = 0x0D58;
			public const uint GL_ACCUM_GREEN_BITS = 0x0D59;
			public const uint GL_ACCUM_BLUE_BITS = 0x0D5A;
			public const uint GL_ACCUM_ALPHA_BITS = 0x0D5B;
			public const uint GL_NAME_STACK_DEPTH = 0x0D70;
			public const uint GL_AUTO_NORMAL = 0x0D80;
			public const uint GL_MAP1_COLOR_4 = 0x0D90;
			public const uint GL_MAP1_INDEX = 0x0D91;
			public const uint GL_MAP1_NORMAL = 0x0D92;
			public const uint GL_MAP1_TEXTURE_COORD_1 = 0x0D93;
			public const uint GL_MAP1_TEXTURE_COORD_2 = 0x0D94;
			public const uint GL_MAP1_TEXTURE_COORD_3 = 0x0D95;
			public const uint GL_MAP1_TEXTURE_COORD_4 = 0x0D96;
			public const uint GL_MAP1_VERTEX_3 = 0x0D97;
			public const uint GL_MAP1_VERTEX_4 = 0x0D98;
			public const uint GL_MAP2_COLOR_4 = 0x0DB0;
			public const uint GL_MAP2_INDEX = 0x0DB1;
			public const uint GL_MAP2_NORMAL = 0x0DB2;
			public const uint GL_MAP2_TEXTURE_COORD_1 = 0x0DB3;
			public const uint GL_MAP2_TEXTURE_COORD_2 = 0x0DB4;
			public const uint GL_MAP2_TEXTURE_COORD_3 = 0x0DB5;
			public const uint GL_MAP2_TEXTURE_COORD_4 = 0x0DB6;
			public const uint GL_MAP2_VERTEX_3 = 0x0DB7;
			public const uint GL_MAP2_VERTEX_4 = 0x0DB8;
			public const uint GL_MAP1_GRID_DOMAIN = 0x0DD0;
			public const uint GL_MAP1_GRID_SEGMENTS = 0x0DD1;
			public const uint GL_MAP2_GRID_DOMAIN = 0x0DD2;
			public const uint GL_MAP2_GRID_SEGMENTS = 0x0DD3;
			public const uint GL_TEXTURE_1D = 0x0DE0;
			public const uint GL_TEXTURE_2D = 0x0DE1;
			public const uint GL_TEXTURE_3D = 0x806F;
			public const uint GL_FEEDBACK_BUFFER_POINTER = 0x0DF0;
			public const uint GL_FEEDBACK_BUFFER_SIZE = 0x0DF1;
			public const uint GL_FEEDBACK_BUFFER_TYPE = 0x0DF2;
			public const uint GL_SELECTION_BUFFER_POINTER = 0x0DF3;
			public const uint GL_SELECTION_BUFFER_SIZE = 0x0DF4;
			/*      GL_TEXTURE_BINDING_1D */
			/*      GL_TEXTURE_BINDING_2D */
			/*      GL_VERTEX_ARRAY */
			/*      GL_NORMAL_ARRAY */
			/*      GL_COLOR_ARRAY */
			/*      GL_INDEX_ARRAY */
			/*      GL_TEXTURE_COORD_ARRAY */
			/*      GL_EDGE_FLAG_ARRAY */
			/*      GL_VERTEX_ARRAY_SIZE */
			/*      GL_VERTEX_ARRAY_TYPE */
			/*      GL_VERTEX_ARRAY_STRIDE */
			/*      GL_NORMAL_ARRAY_TYPE */
			/*      GL_NORMAL_ARRAY_STRIDE */
			/*      GL_COLOR_ARRAY_SIZE */
			/*      GL_COLOR_ARRAY_TYPE */
			/*      GL_COLOR_ARRAY_STRIDE */
			/*      GL_INDEX_ARRAY_TYPE */
			/*      GL_INDEX_ARRAY_STRIDE */
			/*      GL_TEXTURE_COORD_ARRAY_SIZE */
			/*      GL_TEXTURE_COORD_ARRAY_TYPE */
			/*      GL_TEXTURE_COORD_ARRAY_STRIDE */
			/*      GL_EDGE_FLAG_ARRAY_STRIDE */
			/*      GL_POLYGON_OFFSET_FACTOR */
			/*      GL_POLYGON_OFFSET_UNITS */


			/*      GL_TEXTURE_MAG_FILTER */
			/*      GL_TEXTURE_MIN_FILTER */
			/*      GL_TEXTURE_WRAP_S */
			/*      GL_TEXTURE_WRAP_T */
			public const uint GL_TEXTURE_WIDTH = 0x1000;
			public const uint GL_TEXTURE_HEIGHT = 0x1001;
			public const uint GL_TEXTURE_INTERNAL_FORMAT = 0x1003;
			public const uint GL_TEXTURE_BORDER_COLOR = 0x1004;
			public const uint GL_TEXTURE_BORDER = 0x1005;
			/*      GL_TEXTURE_RED_SIZE */
			/*      GL_TEXTURE_GREEN_SIZE */
			/*      GL_TEXTURE_BLUE_SIZE */
			/*      GL_TEXTURE_ALPHA_SIZE */
			/*      GL_TEXTURE_LUMINANCE_SIZE */
			/*      GL_TEXTURE_INTENSITY_SIZE */
			/*      GL_TEXTURE_PRIORITY */
			/*      GL_TEXTURE_RESIDENT */


			public const uint GL_DONT_CARE = 0x1100;
			public const uint GL_FASTEST = 0x1101;
			public const uint GL_NICEST = 0x1102;


			/* HintTarget */
			/*      GL_PERSPECTIVE_CORRECTION_HINT */
			/*      GL_POINT_SMOOTH_HINT */
			/*      GL_LINE_SMOOTH_HINT */
			/*      GL_POLYGON_SMOOTH_HINT */
			/*      GL_FOG_HINT */
			/*      GL_PHONG_HINT */

			/* IndexPointerType */
			/*      GL_SHORT */
			/*      GL_INT */
			/*      GL_FLOAT */
			/*      GL_DOUBLE */

			/* LightModelParameter */
			/*      GL_LIGHT_MODEL_AMBIENT */
			/*      GL_LIGHT_MODEL_LOCAL_VIEWER */
			/*      GL_LIGHT_MODEL_TWO_SIDE */



			public const uint GL_LIGHT0 = 0x4000;
			public const uint GL_LIGHT1 = 0x4001;
			public const uint GL_LIGHT2 = 0x4002;
			public const uint GL_LIGHT3 = 0x4003;
			public const uint GL_LIGHT4 = 0x4004;
			public const uint GL_LIGHT5 = 0x4005;
			public const uint GL_LIGHT6 = 0x4006;
			public const uint GL_LIGHT7 = 0x4007;


			public const uint GL_AMBIENT = 0x1200;
			public const uint GL_DIFFUSE = 0x1201;
			public const uint GL_SPECULAR = 0x1202;
			public const uint GL_POSITION = 0x1203;
			public const uint GL_SPOT_DIRECTION = 0x1204;
			public const uint GL_SPOT_EXPONENT = 0x1205;
			public const uint GL_SPOT_CUTOFF = 0x1206;
			public const uint GL_CONSTANT_ATTENUATION = 0x1207;
			public const uint GL_LINEAR_ATTENUATION = 0x1208;
			public const uint GL_QUADRATIC_ATTENUATION = 0x1209;


			/* InterleavedArrays */
			/*      GL_V2F */
			/*      GL_V3F */
			/*      GL_C4UB_V2F */
			/*      GL_C4UB_V3F */
			/*      GL_C3F_V3F */
			/*      GL_N3F_V3F */
			/*      GL_C4F_N3F_V3F */
			/*      GL_T2F_V3F */
			/*      GL_T4F_V4F */
			/*      GL_T2F_C4UB_V3F */
			/*      GL_T2F_C3F_V3F */
			/*      GL_T2F_N3F_V3F */
			/*      GL_T2F_C4F_N3F_V3F */
			/*      GL_T4F_C4F_N3F_V4F */


			public const uint GL_COMPILE = 0x1300;
			public const uint GL_COMPILE_AND_EXECUTE = 0x1301;


			/* ListNameType */
			/*      GL_BYTE */
			/*      GL_UNSIGNED_BYTE */
			/*      GL_SHORT */
			/*      GL_UNSIGNED_SHORT */
			/*      GL_INT */
			/*      GL_UNSIGNED_INT */
			/*      GL_FLOAT */
			/*      GL_2_BYTES */
			/*      GL_3_BYTES */
			/*      GL_4_BYTES */


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


			/* MapTarget */
			/*      GL_MAP1_COLOR_4 */
			/*      GL_MAP1_INDEX */
			/*      GL_MAP1_NORMAL */
			/*      GL_MAP1_TEXTURE_COORD_1 */
			/*      GL_MAP1_TEXTURE_COORD_2 */
			/*      GL_MAP1_TEXTURE_COORD_3 */
			/*      GL_MAP1_TEXTURE_COORD_4 */
			/*      GL_MAP1_VERTEX_3 */
			/*      GL_MAP1_VERTEX_4 */
			/*      GL_MAP2_COLOR_4 */
			/*      GL_MAP2_INDEX */
			/*      GL_MAP2_NORMAL */
			/*      GL_MAP2_TEXTURE_COORD_1 */
			/*      GL_MAP2_TEXTURE_COORD_2 */
			/*      GL_MAP2_TEXTURE_COORD_3 */
			/*      GL_MAP2_TEXTURE_COORD_4 */
			/*      GL_MAP2_VERTEX_3 */
			/*      GL_MAP2_VERTEX_4 */

			/* MaterialFace */
			/*      GL_FRONT */
			/*      GL_BACK */
			/*      GL_FRONT_AND_BACK */


			public const uint GL_EMISSION = 0x1600;
			public const uint GL_SHININESS = 0x1601;
			public const uint GL_AMBIENT_AND_DIFFUSE = 0x1602;
			public const uint GL_COLOR_INDEXES = 0x1603;

			/*      GL_AMBIENT */
			/*      GL_DIFFUSE */
			/*      GL_SPECULAR */


			public const uint GL_MODELVIEW = 0x1700;
			public const uint GL_PROJECTION = 0x1701;
			public const uint GL_TEXTURE = 0x1702;


			/* MeshMode1 */
			/*      GL_POINT */
			/*      GL_LINE */

			/* MeshMode2 */
			/*      GL_POINT */
			/*      GL_LINE */
			/*      GL_FILL */

			/* NormalPointerType */
			/*      GL_BYTE */
			/*      GL_SHORT */
			/*      GL_INT */
			/*      GL_FLOAT */
			/*      GL_DOUBLE */



			public const uint GL_COLOR = 0x1800;
			public const uint GL_DEPTH = 0x1801;
			public const uint GL_STENCIL = 0x1802;



			public const uint GL_COLOR_INDEX = 0x1900;
			public const uint GL_STENCIL_INDEX = 0x1901;
			public const uint GL_DEPTH_COMPONENT = 0x1902;
			public const uint GL_RED = 0x1903;
			public const uint GL_GREEN = 0x1904;
			public const uint GL_BLUE = 0x1905;
			public const uint GL_ALPHA = 0x1906;
			public const uint GL_RGB = 0x1907;
			public const uint GL_RGBA = 0x1908;
			public const uint GL_LUMINANCE = 0x1909;
			public const uint GL_LUMINANCE_ALPHA = 0x190A;


			/* PixelMap */
			/*      GL_PIXEL_MAP_I_TO_I */
			/*      GL_PIXEL_MAP_S_TO_S */
			/*      GL_PIXEL_MAP_I_TO_R */
			/*      GL_PIXEL_MAP_I_TO_G */
			/*      GL_PIXEL_MAP_I_TO_B */
			/*      GL_PIXEL_MAP_I_TO_A */
			/*      GL_PIXEL_MAP_R_TO_R */
			/*      GL_PIXEL_MAP_G_TO_G */
			/*      GL_PIXEL_MAP_B_TO_B */
			/*      GL_PIXEL_MAP_A_TO_A */

			/* PixelStore */
			/*      GL_UNPACK_SWAP_BYTES */
			/*      GL_UNPACK_LSB_FIRST */
			/*      GL_UNPACK_ROW_LENGTH */
			/*      GL_UNPACK_SKIP_ROWS */
			/*      GL_UNPACK_SKIP_PIXELS */
			/*      GL_UNPACK_ALIGNMENT */
			/*      GL_PACK_SWAP_BYTES */
			/*      GL_PACK_LSB_FIRST */
			/*      GL_PACK_ROW_LENGTH */
			/*      GL_PACK_SKIP_ROWS */
			/*      GL_PACK_SKIP_PIXELS */
			/*      GL_PACK_ALIGNMENT */

			/* PixelTransfer */
			/*      GL_MAP_COLOR */
			/*      GL_MAP_STENCIL */
			/*      GL_INDEX_SHIFT */
			/*      GL_INDEX_OFFSET */
			/*      GL_RED_SCALE */
			/*      GL_RED_BIAS */
			/*      GL_GREEN_SCALE */
			/*      GL_GREEN_BIAS */
			/*      GL_BLUE_SCALE */
			/*      GL_BLUE_BIAS */
			/*      GL_ALPHA_SCALE */
			/*      GL_ALPHA_BIAS */
			/*      GL_DEPTH_SCALE */
			/*      GL_DEPTH_BIAS */


			public const uint GL_BITMAP = 0x1A00;
			/*      GL_BYTE */
			/*      GL_UNSIGNED_BYTE */
			/*      GL_SHORT */
			/*      GL_UNSIGNED_SHORT */
			/*      GL_INT */
			/*      GL_UNSIGNED_INT */
			/*      GL_FLOAT */



			public const uint GL_POINT = 0x1B00;
			public const uint GL_LINE = 0x1B01;
			public const uint GL_FILL = 0x1B02;


			/* ReadBufferMode */
			/*      GL_FRONT_LEFT */
			/*      GL_FRONT_RIGHT */
			/*      GL_BACK_LEFT */
			/*      GL_BACK_RIGHT */
			/*      GL_FRONT */
			/*      GL_BACK */
			/*      GL_LEFT */
			/*      GL_RIGHT */
			/*      GL_AUX0 */
			/*      GL_AUX1 */
			/*      GL_AUX2 */
			/*      GL_AUX3 */


			public const uint GL_RENDER = 0x1C00;
			public const uint GL_FEEDBACK = 0x1C01;
			public const uint GL_SELECT = 0x1C02;



			public const uint GL_FLAT = 0x1D00;
			public const uint GL_SMOOTH = 0x1D01;



			/* StencilFunction */
			/*      GL_NEVER */
			/*      GL_LESS */
			/*      GL_EQUAL */
			/*      GL_LEQUAL */
			/*      GL_GREATER */
			/*      GL_NOTEQUAL */
			/*      GL_GEQUAL */
			/*      GL_ALWAYS */


			public const uint GL_KEEP = 0x1E00;
			public const uint GL_REPLACE = 0x1E01;
			public const uint GL_INCR = 0x1E02;
			public const uint GL_DECR = 0x1E03;
			/*      GL_INVERT */



			public const uint GL_VENDOR = 0x1F00;
			public const uint GL_RENDERER = 0x1F01;
			public const uint GL_VERSION = 0x1F02;
			public const uint GL_EXTENSIONS = 0x1F03;



			public const uint GL_S = 0x2000;
			public const uint GL_T = 0x2001;
			public const uint GL_R = 0x2002;
			public const uint GL_Q = 0x2003;


			/* TexCoordPointerType */
			/*      GL_SHORT */
			/*      GL_INT */
			/*      GL_FLOAT */
			/*      GL_DOUBLE */


			public const uint GL_MODULATE = 0x2100;
			public const uint GL_DECAL = 0x2101;
			/*      GL_BLEND */
			/*      GL_REPLACE */



			public const uint GL_TEXTURE_ENV_MODE = 0x2200;
			public const uint GL_TEXTURE_ENV_COLOR = 0x2201;



			public const uint GL_TEXTURE_ENV = 0x2300;



			public const uint GL_EYE_LINEAR = 0x2400;
			public const uint GL_OBJECT_LINEAR = 0x2401;
			public const uint GL_SPHERE_MAP = 0x2402;



			public const uint GL_TEXTURE_GEN_MODE = 0x2500;
			public const uint GL_OBJECT_PLANE = 0x2501;
			public const uint GL_EYE_PLANE = 0x2502;



			public const uint GL_NEAREST = 0x2600;
			public const uint GL_LINEAR = 0x2601;



			/*      GL_NEAREST */
			/*      GL_LINEAR */
			public const uint GL_NEAREST_MIPMAP_NEAREST = 0x2700;
			public const uint GL_LINEAR_MIPMAP_NEAREST = 0x2701;
			public const uint GL_NEAREST_MIPMAP_LINEAR = 0x2702;
			public const uint GL_LINEAR_MIPMAP_LINEAR = 0x2703;



			public const uint GL_TEXTURE_MAG_FILTER = 0x2800;
			public const uint GL_TEXTURE_MIN_FILTER = 0x2801;
			public const uint GL_TEXTURE_WRAP_S = 0x2802;
			public const uint GL_TEXTURE_WRAP_T = 0x2803;
			/*      GL_TEXTURE_BORDER_COLOR */
			/*      GL_TEXTURE_PRIORITY */


			/* TextureTarget */
			/*      GL_TEXTURE_1D */
			/*      GL_TEXTURE_2D */
			/*      GL_PROXY_TEXTURE_1D */
			/*      GL_PROXY_TEXTURE_2D */


			public const uint GL_CLAMP = 0x2900;
			public const uint GL_REPEAT = 0x2901;


			/* VertexPointerType */
			/*      GL_SHORT */
			/*      GL_INT */
			/*      GL_FLOAT */
			/*      GL_DOUBLE */

			public const uint GL_CLIENT_PIXEL_STORE_BIT = 0x00000001;
			public const uint GL_CLIENT_VERTEX_ARRAY_BIT = 0x00000002;
			public const uint GL_CLIENT_ALL_ATTRIB_BITS = 0xffffffff;


			public const uint GL_POLYGON_OFFSET_FACTOR = 0x8038;
			public const uint GL_POLYGON_OFFSET_UNITS = 0x2A00;
			public const uint GL_POLYGON_OFFSET_POINT = 0x2A01;
			public const uint GL_POLYGON_OFFSET_LINE = 0x2A02;
			public const uint GL_POLYGON_OFFSET_FILL = 0x8037;


			public const uint GL_ALPHA4 = 0x803B;
			public const uint GL_ALPHA8 = 0x803C;
			public const uint GL_ALPHA12 = 0x803D;
			public const uint GL_ALPHA16 = 0x803E;
			public const uint GL_LUMINANCE4 = 0x803F;
			public const uint GL_LUMINANCE8 = 0x8040;
			public const uint GL_LUMINANCE12 = 0x8041;
			public const uint GL_LUMINANCE16 = 0x8042;
			public const uint GL_LUMINANCE4_ALPHA4 = 0x8043;
			public const uint GL_LUMINANCE6_ALPHA2 = 0x8044;
			public const uint GL_LUMINANCE8_ALPHA8 = 0x8045;
			public const uint GL_LUMINANCE12_ALPHA4 = 0x8046;
			public const uint GL_LUMINANCE12_ALPHA12 = 0x8047;
			public const uint GL_LUMINANCE16_ALPHA16 = 0x8048;
			public const uint GL_INTENSITY = 0x8049;
			public const uint GL_INTENSITY4 = 0x804A;
			public const uint GL_INTENSITY8 = 0x804B;
			public const uint GL_INTENSITY12 = 0x804C;
			public const uint GL_INTENSITY16 = 0x804D;
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
			public const uint GL_TEXTURE_RED_SIZE = 0x805C;
			public const uint GL_TEXTURE_GREEN_SIZE = 0x805D;
			public const uint GL_TEXTURE_BLUE_SIZE = 0x805E;
			public const uint GL_TEXTURE_ALPHA_SIZE = 0x805F;
			public const uint GL_TEXTURE_LUMINANCE_SIZE = 0x8060;
			public const uint GL_TEXTURE_INTENSITY_SIZE = 0x8061;
			public const uint GL_PROXY_TEXTURE_1D = 0x8063;
			public const uint GL_PROXY_TEXTURE_2D = 0x8064;


			public const uint GL_TEXTURE_PRIORITY = 0x8066;
			public const uint GL_TEXTURE_RESIDENT = 0x8067;
			public const uint GL_TEXTURE_BINDING_1D = 0x8068;
			public const uint GL_TEXTURE_BINDING_2D = 0x8069;


			public const uint GL_VERTEX_ARRAY = 0x8074;
			public const uint GL_NORMAL_ARRAY = 0x8075;
			public const uint GL_COLOR_ARRAY = 0x8076;
			public const uint GL_INDEX_ARRAY = 0x8077;
			public const uint GL_TEXTURE_COORD_ARRAY = 0x8078;
			public const uint GL_EDGE_FLAG_ARRAY = 0x8079;
			public const uint GL_VERTEX_ARRAY_SIZE = 0x807A;
			public const uint GL_VERTEX_ARRAY_TYPE = 0x807B;
			public const uint GL_VERTEX_ARRAY_STRIDE = 0x807C;
			public const uint GL_NORMAL_ARRAY_TYPE = 0x807E;
			public const uint GL_NORMAL_ARRAY_STRIDE = 0x807F;
			public const uint GL_COLOR_ARRAY_SIZE = 0x8081;
			public const uint GL_COLOR_ARRAY_TYPE = 0x8082;
			public const uint GL_COLOR_ARRAY_STRIDE = 0x8083;
			public const uint GL_INDEX_ARRAY_TYPE = 0x8085;
			public const uint GL_INDEX_ARRAY_STRIDE = 0x8086;
			public const uint GL_TEXTURE_COORD_ARRAY_SIZE = 0x8088;
			public const uint GL_TEXTURE_COORD_ARRAY_TYPE = 0x8089;
			public const uint GL_TEXTURE_COORD_ARRAY_STRIDE = 0x808A;
			public const uint GL_EDGE_FLAG_ARRAY_STRIDE = 0x808C;
			public const uint GL_VERTEX_ARRAY_POINTER = 0x808E;
			public const uint GL_NORMAL_ARRAY_POINTER = 0x808F;
			public const uint GL_COLOR_ARRAY_POINTER = 0x8090;
			public const uint GL_INDEX_ARRAY_POINTER = 0x8091;
			public const uint GL_TEXTURE_COORD_ARRAY_POINTER = 0x8092;
			public const uint GL_EDGE_FLAG_ARRAY_POINTER = 0x8093;
			public const uint GL_V2F = 0x2A20;
			public const uint GL_V3F = 0x2A21;
			public const uint GL_C4UB_V2F = 0x2A22;
			public const uint GL_C4UB_V3F = 0x2A23;
			public const uint GL_C3F_V3F = 0x2A24;
			public const uint GL_N3F_V3F = 0x2A25;
			public const uint GL_C4F_N3F_V3F = 0x2A26;
			public const uint GL_T2F_V3F = 0x2A27;
			public const uint GL_T4F_V4F = 0x2A28;
			public const uint GL_T2F_C4UB_V3F = 0x2A29;
			public const uint GL_T2F_C3F_V3F = 0x2A2A;
			public const uint GL_T2F_N3F_V3F = 0x2A2B;
			public const uint GL_T2F_C4F_N3F_V3F = 0x2A2C;
			public const uint GL_T4F_C4F_N3F_V4F = 0x2A2D;

			/*
			public const uint GL_EXT_vertex_array = 1;
			public const uint GL_EXT_bgra = 1;
			public const uint GL_EXT_paletted_texture = 1;
			public const uint GL_WIN_swap_hint = 1;
			public const uint GL_WIN_draw_range_elements = 1;
			// public const uint GL_WIN_phong_shading = 1;
			// public const uint GL_WIN_specular_fog = 1;
			*/

			public const uint GL_VERTEX_ARRAY_EXT = 0x8074;
			public const uint GL_NORMAL_ARRAY_EXT = 0x8075;
			public const uint GL_COLOR_ARRAY_EXT = 0x8076;
			public const uint GL_INDEX_ARRAY_EXT = 0x8077;
			public const uint GL_TEXTURE_COORD_ARRAY_EXT = 0x8078;
			public const uint GL_EDGE_FLAG_ARRAY_EXT = 0x8079;
			public const uint GL_VERTEX_ARRAY_SIZE_EXT = 0x807A;
			public const uint GL_VERTEX_ARRAY_TYPE_EXT = 0x807B;
			public const uint GL_VERTEX_ARRAY_STRIDE_EXT = 0x807C;
			public const uint GL_VERTEX_ARRAY_COUNT_EXT = 0x807D;
			public const uint GL_NORMAL_ARRAY_TYPE_EXT = 0x807E;
			public const uint GL_NORMAL_ARRAY_STRIDE_EXT = 0x807F;
			public const uint GL_NORMAL_ARRAY_COUNT_EXT = 0x8080;
			public const uint GL_COLOR_ARRAY_SIZE_EXT = 0x8081;
			public const uint GL_COLOR_ARRAY_TYPE_EXT = 0x8082;
			public const uint GL_COLOR_ARRAY_STRIDE_EXT = 0x8083;
			public const uint GL_COLOR_ARRAY_COUNT_EXT = 0x8084;
			public const uint GL_INDEX_ARRAY_TYPE_EXT = 0x8085;
			public const uint GL_INDEX_ARRAY_STRIDE_EXT = 0x8086;
			public const uint GL_INDEX_ARRAY_COUNT_EXT = 0x8087;
			public const uint GL_TEXTURE_COORD_ARRAY_SIZE_EXT = 0x8088;
			public const uint GL_TEXTURE_COORD_ARRAY_TYPE_EXT = 0x8089;
			public const uint GL_TEXTURE_COORD_ARRAY_STRIDE_EXT = 0x808A;
			public const uint GL_TEXTURE_COORD_ARRAY_COUNT_EXT = 0x808B;
			public const uint GL_EDGE_FLAG_ARRAY_STRIDE_EXT = 0x808C;
			public const uint GL_EDGE_FLAG_ARRAY_COUNT_EXT = 0x808D;
			public const uint GL_VERTEX_ARRAY_POINTER_EXT = 0x808E;
			public const uint GL_NORMAL_ARRAY_POINTER_EXT = 0x808F;
			public const uint GL_COLOR_ARRAY_POINTER_EXT = 0x8090;
			public const uint GL_INDEX_ARRAY_POINTER_EXT = 0x8091;
			public const uint GL_TEXTURE_COORD_ARRAY_POINTER_EXT = 0x8092;
			public const uint GL_EDGE_FLAG_ARRAY_POINTER_EXT = 0x8093;
			public const uint GL_DOUBLE_EXT = 0x140A;


			public const uint GL_BGR_EXT = 0x80E0;
			public const uint GL_BGRA_EXT = 0x80E1;

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

			public const uint GL_TEXTURE0 = 0x84C0;
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

			// End of ARB 3.2 constants
		}
	}
}
