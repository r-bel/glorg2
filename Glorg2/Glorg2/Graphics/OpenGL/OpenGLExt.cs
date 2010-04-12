using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

/*
public static {[a-zA-Z0-9]+} {[a-zA-Z0-9]+};			\2 = ctx.GetProc<\1>("\2");
public delegate [a-zA-Z0-9]+ {[a-zA-Z0-9]+}\(.*\);		public static \1 gl\1;
GLAPI {[a-zA-Z0-9]+} APIENTRY gl{[a-zA-Z0-9]+} \({.*}\);	public delegate \1 \2(\3);

\#define:b{[a-zA-Z_0-9]+}:b{.*}$	public const uint \1 = \2;
*/

namespace Glorg2.Graphics.OpenGL
{
	public static partial class OpenGL
	{
		private static System.Collections.ObjectModel.ReadOnlyCollection<string> Extensions;

		public static bool IsExtensionSupported(string ext)
		{
			return GetSupportedExtensions().Contains(ext);
		}

		public static System.Collections.ObjectModel.ReadOnlyCollection<string> GetSupportedExtensions()
		{
			if (Extensions == null)
			{
				string ext = Marshal.PtrToStringAnsi(glGetString(Const.GL_EXTENSIONS));
				string[] items = ext.Split(' ');
				Extensions = items.ToList().AsReadOnly();
			}
			return Extensions;
				
		}




		#region Vertex Buffer Objects
		public enum VboTarget : uint
		{
			GL_ARRAY_BUFFER_ARB = 0x8892,
			GL_ELEMENT_ARRAY_BUFFER_ARB = 0x8893
		}
		/*
		#define GL_BUFFER_SIZE_ARB                0x8764
		#define GL_BUFFER_USAGE_ARB               0x8765


		#define GL_ARRAY_BUFFER_BINDING_ARB       0x8894
		#define GL_ELEMENT_ARRAY_BUFFER_BINDING_ARB 0x8895
		#define GL_VERTEX_ARRAY_BUFFER_BINDING_ARB 0x8896
		#define GL_NORMAL_ARRAY_BUFFER_BINDING_ARB 0x8897
		#define GL_COLOR_ARRAY_BUFFER_BINDING_ARB 0x8898
		#define GL_INDEX_ARRAY_BUFFER_BINDING_ARB 0x8899
		#define GL_TEXTURE_COORD_ARRAY_BUFFER_BINDING_ARB 0x889A
		#define GL_EDGE_FLAG_ARRAY_BUFFER_BINDING_ARB 0x889B
		#define GL_SECONDARY_COLOR_ARRAY_BUFFER_BINDING_ARB 0x889C
		#define GL_FOG_COORDINATE_ARRAY_BUFFER_BINDING_ARB 0x889D
		#define GL_WEIGHT_ARRAY_BUFFER_BINDING_ARB 0x889E
		#define GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING_ARB 0x889F*/
		public enum VboAccess
		{
			GL_READ_ONLY_ARB = 0x88B8,
			GL_WRITE_ONLY_ARB = 0x88B9,
			GL_READ_WRITE_ARB = 0x88BA,
		}
		/*#define GL_BUFFER_ACCESS_ARB              0x88BB
		#define GL_BUFFER_MAPPED_ARB              0x88BC
		#define GL_BUFFER_MAP_POINTER_ARB         0x88BD*/
		public enum VboUsage : uint
		{
			GL_STREAM_DRAW_ARB = 0x88E0,
			GL_STREAM_READ_ARB = 0x88E1,
			GL_STREAM_COPY_ARB = 0x88E2,
			GL_STATIC_DRAW_ARB = 0x88E4,
			GL_STATIC_READ_ARB = 0x88E5,
			GL_STATIC_COPY_ARB = 0x88E6,
			GL_DYNAMIC_DRAW_ARB = 0x88E8,
			GL_DYNAMIC_READ_ARB = 0x88E9,
			GL_DYNAMIC_COPY_ARB = 0x88EA,
		}

		public delegate void GenBuffersARB(int n, uint[] ids);
		public delegate void BindBufferARB(VboTarget target, uint id);
		public unsafe delegate void BufferDataARB(VboTarget target, int size, IntPtr data, VboUsage usage);
		public unsafe delegate void BufferSubDataARB(VboTarget target, int offset, int size, void* data);
		public delegate void DeleteBuffersARB(int n, uint[] ids);
		public unsafe delegate void* MapBufferARB(VboTarget target, VboAccess access);
		public delegate boolean UnmapBufferARB(VboTarget target);

		public static GenBuffersARB glGenBuffersARB;
		public static BindBufferARB glBindBufferARB;
		public static BufferDataARB glBufferDataARB;
		public static BufferSubDataARB glBufferSubDataARB;
		public static DeleteBuffersARB glDeleteBuffersARB;
		public static MapBufferARB glMapBufferARB;
		public static UnmapBufferARB glUnmapBufferARB;

		/// <summary>
		/// Initializes Vertex Buffer Objects
		/// This is an internal function, and should not be called directly, since
		/// this is done by the rendering device.
		/// </summary>
		/// <param name="ctx">Context used to initialize functions</param>
		public static void InitVbo(OpenGLContext ctx)
		{
			if (IsExtensionSupported("GL_ARB_vertex_buffer_object"))
			{
				glGenBuffersARB = ctx.GetProc<GenBuffersARB>("glGenBuffersARB");
				glBindBufferARB = ctx.GetProc<BindBufferARB>("glBindBufferARB");
				glBufferDataARB = ctx.GetProc<BufferDataARB>("glBufferDataARB");
				glBufferSubDataARB = ctx.GetProc<BufferSubDataARB>("glBufferSubDataARB");
				glDeleteBuffersARB = ctx.GetProc<DeleteBuffersARB>("glDeleteBuffersARB");
				glMapBufferARB = ctx.GetProc<MapBufferARB>("glMapBufferARB");
				glUnmapBufferARB = ctx.GetProc<UnmapBufferARB>("glUnmapBufferARB");
			}
		}

		#endregion

		#region Shader Program

		// Shader objects
		public const int GL_PROGRAM_OBJECT_ARB = 0x8B40;
		public const int GL_SHADER_OBJECT_ARB = 0x8B48;
		public const int GL_OBJECT_TYPE_ARB = 0x8B4E;
		public const int GL_OBJECT_SUBTYPE_ARB = 0x8B4F;
		public const int GL_FLOAT_VEC2_ARB = 0x8B50;
		public const int GL_FLOAT_VEC3_ARB = 0x8B51;
		public const int GL_FLOAT_VEC4_ARB = 0x8B52;
		public const int GL_INT_VEC2_ARB = 0x8B53;
		public const int GL_INT_VEC3_ARB = 0x8B54;
		public const int GL_INT_VEC4_ARB = 0x8B55;
		public const int GL_BOOL_ARB = 0x8B56;
		public const int GL_BOOL_VEC2_ARB = 0x8B57;
		public const int GL_BOOL_VEC3_ARB = 0x8B58;
		public const int GL_BOOL_VEC4_ARB = 0x8B59;
		public const int GL_FLOAT_MAT2_ARB = 0x8B5A;
		public const int GL_FLOAT_MAT3_ARB = 0x8B5B;
		public const int GL_FLOAT_MAT4_ARB = 0x8B5C;
		public const int GL_SAMPLER_1D_ARB = 0x8B5D;
		public const int GL_SAMPLER_2D_ARB = 0x8B5E;
		public const int GL_SAMPLER_3D_ARB = 0x8B5F;
		public const int GL_SAMPLER_CUBE_ARB = 0x8B60;
		public const int GL_SAMPLER_1D_SHADOW_ARB = 0x8B61;
		public const int GL_SAMPLER_2D_SHADOW_ARB = 0x8B62;
		public const int GL_SAMPLER_2D_RECT_ARB = 0x8B63;
		public const int GL_SAMPLER_2D_RECT_SHADOW_ARB = 0x8B64;
		public const int GL_OBJECT_DELETE_STATUS_ARB = 0x8B80;
		public const int GL_OBJECT_COMPILE_STATUS_ARB = 0x8B81;
		public const int GL_OBJECT_LINK_STATUS_ARB = 0x8B82;
		public const int GL_OBJECT_VALIDATE_STATUS_ARB = 0x8B83;
		public const int GL_OBJECT_INFO_LOG_LENGTH_ARB = 0x8B84;
		public const int GL_OBJECT_ATTACHED_OBJECTS_ARB = 0x8B85;
		public const int GL_OBJECT_ACTIVE_UNIFORMS_ARB = 0x8B86;
		public const int GL_OBJECT_ACTIVE_UNIFORM_MAX_LENGTH_ARB = 0x8B87;
		public const int GL_OBJECT_SHADER_SOURCE_LENGTH_ARB = 0x8B88;

		// Vertex shader
		public const int GL_VERTEX_SHADER_ARB = 0x8B31;
		public const int GL_MAX_VERTEX_UNIFORM_COMPONENTS_ARB = 0x8B4A;
		public const int GL_MAX_VARYING_FLOATS_ARB = 0x8B4B;
		public const int GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS_ARB = 0x8B4C;
		public const int GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS_ARB = 0x8B4D;
		public const int GL_OBJECT_ACTIVE_ATTRIBUTES_ARB = 0x8B89;
		public const int GL_OBJECT_ACTIVE_ATTRIBUTE_MAX_LENGTH_ARB = 0x8B8A;

		//Fragment shader
		public const int GL_FRAGMENT_SHADER_ARB = 0x8B30;
		public const int GL_MAX_FRAGMENT_UNIFORM_COMPONENTS_ARB = 0x8B49;
		public const int GL_FRAGMENT_SHADER_DERIVATIVE_HINT_ARB = 0x8B8B;

		// Geometry shader
		public const int GL_LINES_ADJACENCY_ARB = 0x000A;
		public const int GL_LINE_STRIP_ADJACENCY_ARB = 0x000B;
		public const int GL_TRIANGLES_ADJACENCY_ARB = 0x000C;
		public const int GL_TRIANGLE_STRIP_ADJACENCY_ARB = 0x000D;
		public const int GL_PROGRAM_POINT_SIZE_ARB = 0x8642;
		public const int GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS_ARB = 0x8C29;
		public const int GL_FRAMEBUFFER_ATTACHMENT_LAYERED_ARB = 0x8DA7;
		public const int GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS_ARB = 0x8DA8;
		public const int GL_FRAMEBUFFER_INCOMPLETE_LAYER_COUNT_ARB = 0x8DA9;
		public const int GL_GEOMETRY_SHADER_ARB = 0x8DD9;
		public const int GL_GEOMETRY_VERTICES_OUT_ARB = 0x8DDA;
		public const int GL_GEOMETRY_INPUT_TYPE_ARB = 0x8DDB;
		public const int GL_GEOMETRY_OUTPUT_TYPE_ARB = 0x8DDC;
		public const int GL_MAX_GEOMETRY_VARYING_COMPONENTS_ARB = 0x8DDD;
		public const int GL_MAX_VERTEX_VARYING_COMPONENTS_ARB = 0x8DDE;
		public const int GL_MAX_GEOMETRY_UNIFORM_COMPONENTS_ARB = 0x8DDF;
		public const int GL_MAX_GEOMETRY_OUTPUT_VERTICES_ARB = 0x8DE0;
		public const int GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS_ARB = 0x8DE1;


		public delegate void VertexAttrib1dARB(uint index, double x);
		public delegate void VertexAttrib1dvARB(uint index, double[] v);
		public delegate void VertexAttrib1fARB(uint index, float x);
		public delegate void VertexAttrib1fvARB(uint index, float[] v);
		public delegate void VertexAttrib1sARB(uint index, short x);
		public delegate void VertexAttrib1svARB(uint index, short[] v);
		public delegate void VertexAttrib2dARB(uint index, double x, double y);
		public delegate void VertexAttrib2dvARB(uint index, double[] v);
		public delegate void VertexAttrib2fARB(uint index, float x, float y);
		public delegate void VertexAttrib2fvARB(uint index, float[] v);
		public delegate void VertexAttrib2sARB(uint index, short x, short y);
		public delegate void VertexAttrib2svARB(uint index, short[] v);
		public delegate void VertexAttrib3dARB(uint index, double x, double y, double z);
		public delegate void VertexAttrib3dvARB(uint index, double[] v);
		public delegate void VertexAttrib3fARB(uint index, float x, float y, float z);
		public delegate void VertexAttrib3fvARB(uint index, float[] v);
		public delegate void VertexAttrib3sARB(uint index, short x, short y, short z);
		public delegate void VertexAttrib3svARB(uint index, short[] v);
		public delegate void VertexAttrib4NbvARB(uint index, byte[] v);
		public delegate void VertexAttrib4NivARB(uint index, int[] v);
		public delegate void VertexAttrib4NsvARB(uint index, short[] v);
		public delegate void VertexAttrib4NubARB(uint index, byte x, byte y, byte z, byte w);
		public delegate void VertexAttrib4NubvARB(uint index, byte[] v);
		public delegate void VertexAttrib4NuivARB(uint index, uint[] v);
		public delegate void VertexAttrib4NusvARB(uint index, ushort[] v);
		public delegate void VertexAttrib4bvARB(uint index, byte[] v);
		public delegate void VertexAttrib4dARB(uint index, double x, double y, double z, double w);
		public delegate void VertexAttrib4dvARB(uint index, double[] v);
		public delegate void VertexAttrib4fARB(uint index, float x, float y, float z, float w);
		public delegate void VertexAttrib4fvARB(uint index, float[] v);
		public delegate void VertexAttrib4ivARB(uint index, int[] v);
		public delegate void VertexAttrib4sARB(uint index, short x, short y, short z, short w);
		public delegate void VertexAttrib4svARB(uint index, short[] v);
		public delegate void VertexAttrib4ubvARB(uint index, byte[] v);
		public delegate void VertexAttrib4uivARB(uint index, uint[] v);
		public delegate void VertexAttrib4usvARB(uint index, ushort[] v);
		public unsafe delegate void VertexAttribPointerARB(uint index, int size, uint type, boolean normalized, int stride, void* pointer);
		public delegate void EnableVertexAttribArrayARB(uint index);
		public delegate void DisableVertexAttribArrayARB(uint index);
		public delegate void ProgramStringARB(uint target, uint format, int len, [MarshalAs(UnmanagedType.LPWStr)] string code);
		public delegate void BindProgramARB(uint target, uint program);
		public delegate void DeleteProgramsARB(int n, uint[] programs);
		public delegate void GenProgramsARB(int n, uint[] programs);
		public delegate void ProgramEnvParameter4dARB(uint target, uint index, double x, double y, double z, double w);
		public delegate void ProgramEnvParameter4dvARB(uint target, uint index, double[] parameters);
		public delegate void ProgramEnvParameter4fARB(uint target, uint index, float x, float y, float z, float w);
		public delegate void ProgramEnvParameter4fvARB(uint target, uint index, float[] parameters);
		public delegate void ProgramLocalParameter4dARB(uint target, uint index, double x, double y, double z, double w);
		public delegate void ProgramLocalParameter4dvARB(uint target, uint index, double[] parameters);
		public delegate void ProgramLocalParameter4fARB(uint target, uint index, float x, float y, float z, float w);
		public delegate void ProgramLocalParameter4fvARB(uint target, uint index, float[] parameters);
		public delegate void GetProgramEnvParameterdvARB(uint target, uint index, ref double parameters);
		public delegate void GetProgramEnvParameterfvARB(uint target, uint index, float[] parameters);
		public delegate void GetProgramLocalParameterdvARB(uint target, uint index, ref double parameters);
		public delegate void GetProgramLocalParameterfvARB(uint target, uint index, ref float parameters);
		public delegate void GetProgramivARB(uint target, uint pname, ref int parameters);
		public delegate void GetProgramStringARB(uint target, uint pname, out string code);
		public delegate void GetVertexAttribdvARB(uint index, uint pname, ref double parameters);
		public delegate void GetVertexAttribfvARB(uint index, uint pname, ref float parameters);
		public delegate void GetVertexAttribivARB(uint index, uint pname, ref int parameters);
		public unsafe delegate void GetVertexAttribPointervARB(uint index, uint pname, void** pointer);
		public delegate boolean IsProgramARB(uint program);

		public delegate void DeleteObjectARB(IntPtr obj);
		public delegate IntPtr GetHandleARB(uint pname);
		public delegate void DetachObjectARB(IntPtr containerObj, IntPtr attachedObj);
		public delegate IntPtr CreateShaderObjectARB(uint shaderType);
		public delegate void ShaderSourceARB(IntPtr shaderObj, int count, string[] code, int[] length);
		public delegate void CompileShaderARB(IntPtr shaderObj);
		public delegate IntPtr CreateProgramObjectARB();
		public delegate void AttachObjectARB(IntPtr containerObj, IntPtr obj);
		public delegate void LinkProgramARB(IntPtr programObj);
		public delegate void UseProgramObjectARB(IntPtr programObj);
		public delegate void ValidateProgramARB(IntPtr programObj);
		public delegate void Uniform1fARB(int location, float v0);
		public delegate void Uniform2fARB(int location, float v0, float v1);
		public delegate void Uniform3fARB(int location, float v0, float v1, float v2);
		public delegate void Uniform4fARB(int location, float v0, float v1, float v2, float v3);
		public delegate void Uniform1iARB(int location, int v0);
		public delegate void Uniform2iARB(int location, int v0, int v1);
		public delegate void Uniform3iARB(int location, int v0, int v1, int v2);
		public delegate void Uniform4iARB(int location, int v0, int v1, int v2, int v3);
		public delegate void Uniform1fvARB(int location, int count, ref float value);
		public delegate void Uniform2fvARB(int location, int count, ref Vector2 value);
		public delegate void Uniform3fvARB(int location, int count, ref Vector3 value);
		public delegate void Uniform4fvARB(int location, int count, ref Vector4 value);
		public delegate void Uniform1ivARB(int location, int count, int[] value);
		public delegate void Uniform2ivARB(int location, int count, int[] value);
		public delegate void Uniform3ivARB(int location, int count, int[] value);
		public delegate void Uniform4ivARB(int location, int count, int[] value);
		public delegate void UniformMatrix2fvARB(int location, int count, boolean transpose, float[] value);
		public delegate void UniformMatrix3fvARB(int location, int count, boolean transpose, float[] value);
		public delegate void UniformMatrix4fvARB(int location, int count, boolean transpose, ref Matrix value);
		public delegate void GetObjectParameterfvARB(IntPtr obj, uint pname, float[] parameters);
		public delegate void GetObjectParameterivARB(IntPtr obj, uint pname, int[] parameters);
		public delegate void GetInfoLogARB(IntPtr obj, int maxLength, ref int length, byte[] infoLog);
		public delegate void GetAttachedObjectsARB(IntPtr containerObj, int maxCount, ref int count, IntPtr[] obj);
		public delegate int GetUniformLocationARB(IntPtr programObj, string name);
		public delegate void GetActiveUniformARB(IntPtr programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, byte[] name);
		public delegate void GetUniformfvARB(IntPtr programObj, int location, float[] parameters);
		public delegate void GetUniformivARB(IntPtr programObj, int location, int[] parameters);
		public delegate void GetShaderSourceARB(IntPtr obj, int maxLength, ref int length, byte[] source);

		public delegate void ProgramParameteriARB(uint program, uint pname, int value);
		public delegate void FramebufferTextureARB(uint target, uint attachment, uint texture, int level);
		public delegate void FramebufferTextureLayerARB(uint target, uint attachment, uint texture, int level, int layer);
		public delegate void FramebufferTextureFaceARB(uint target, uint attachment, uint texture, int level, uint face);


		public static VertexAttrib1dARB glVertexAttrib1dARB;
		public static VertexAttrib1dvARB glVertexAttrib1dvARB;
		public static VertexAttrib1fARB glVertexAttrib1fARB;
		public static VertexAttrib1fvARB glVertexAttrib1fvARB;
		public static VertexAttrib1sARB glVertexAttrib1sARB;
		public static VertexAttrib1svARB glVertexAttrib1svARB;
		public static VertexAttrib2dARB glVertexAttrib2dARB;
		public static VertexAttrib2dvARB glVertexAttrib2dvARB;
		public static VertexAttrib2fARB glVertexAttrib2fARB;
		public static VertexAttrib2fvARB glVertexAttrib2fvARB;
		public static VertexAttrib2sARB glVertexAttrib2sARB;
		public static VertexAttrib2svARB glVertexAttrib2svARB;
		public static VertexAttrib3dARB glVertexAttrib3dARB;
		public static VertexAttrib3dvARB glVertexAttrib3dvARB;
		public static VertexAttrib3fARB glVertexAttrib3fARB;
		public static VertexAttrib3fvARB glVertexAttrib3fvARB;
		public static VertexAttrib3sARB glVertexAttrib3sARB;
		public static VertexAttrib3svARB glVertexAttrib3svARB;
		public static VertexAttrib4NbvARB glVertexAttrib4NbvARB;
		public static VertexAttrib4NivARB glVertexAttrib4NivARB;
		public static VertexAttrib4NsvARB glVertexAttrib4NsvARB;
		public static VertexAttrib4NubARB glVertexAttrib4NubARB;
		public static VertexAttrib4NubvARB glVertexAttrib4NubvARB;
		public static VertexAttrib4NuivARB glVertexAttrib4NuivARB;
		public static VertexAttrib4NusvARB glVertexAttrib4NusvARB;
		public static VertexAttrib4bvARB glVertexAttrib4bvARB;
		public static VertexAttrib4dARB glVertexAttrib4dARB;
		public static VertexAttrib4dvARB glVertexAttrib4dvARB;
		public static VertexAttrib4fARB glVertexAttrib4fARB;
		public static VertexAttrib4fvARB glVertexAttrib4fvARB;
		public static VertexAttrib4ivARB glVertexAttrib4ivARB;
		public static VertexAttrib4sARB glVertexAttrib4sARB;
		public static VertexAttrib4svARB glVertexAttrib4svARB;
		public static VertexAttrib4ubvARB glVertexAttrib4ubvARB;
		public static VertexAttrib4uivARB glVertexAttrib4uivARB;
		public static VertexAttrib4usvARB glVertexAttrib4usvARB;
		public static VertexAttribPointerARB glVertexAttribPointerARB;
		public static EnableVertexAttribArrayARB glEnableVertexAttribArrayARB;
		public static DisableVertexAttribArrayARB glDisableVertexAttribArrayARB;
		public static ProgramStringARB glProgramStringARB;
		public static BindProgramARB glBindProgramARB;
		public static DeleteProgramsARB glDeleteProgramsARB;
		public static GenProgramsARB glGenProgramsARB;
		public static ProgramEnvParameter4dARB glProgramEnvParameter4dARB;
		public static ProgramEnvParameter4dvARB glProgramEnvParameter4dvARB;
		public static ProgramEnvParameter4fARB glProgramEnvParameter4fARB;
		public static ProgramEnvParameter4fvARB glProgramEnvParameter4fvARB;
		public static ProgramLocalParameter4dARB glProgramLocalParameter4dARB;
		public static ProgramLocalParameter4dvARB glProgramLocalParameter4dvARB;
		public static ProgramLocalParameter4fARB glProgramLocalParameter4fARB;
		public static ProgramLocalParameter4fvARB glProgramLocalParameter4fvARB;
		public static GetProgramEnvParameterdvARB glGetProgramEnvParameterdvARB;
		public static GetProgramEnvParameterfvARB glGetProgramEnvParameterfvARB;
		public static GetProgramLocalParameterdvARB glGetProgramLocalParameterdvARB;
		public static GetProgramLocalParameterfvARB glGetProgramLocalParameterfvARB;
		public static GetProgramivARB glGetProgramivARB;
		public static GetProgramStringARB glGetProgramStringARB;
		public static GetVertexAttribdvARB glGetVertexAttribdvARB;
		public static GetVertexAttribfvARB glGetVertexAttribfvARB;
		public static GetVertexAttribivARB glGetVertexAttribivARB;
		public static GetVertexAttribPointervARB glGetVertexAttribPointervARB;
		public static IsProgramARB glIsProgramARB;

		public static DeleteObjectARB glDeleteObjectARB;
		public static GetHandleARB glGetHandleARB;
		public static DetachObjectARB glDetachObjectARB;
		public static CreateShaderObjectARB glCreateShaderObjectARB;
		public static ShaderSourceARB glShaderSourceARB;
		public static CompileShaderARB glCompileShaderARB;
		public static CreateProgramObjectARB glCreateProgramObjectARB;
		public static AttachObjectARB glAttachObjectARB;
		public static LinkProgramARB glLinkProgramARB;
		public static UseProgramObjectARB glUseProgramObjectARB;
		public static ValidateProgramARB glValidateProgramARB;
		public static Uniform1fARB glUniform1fARB;
		public static Uniform2fARB glUniform2fARB;
		public static Uniform3fARB glUniform3fARB;
		public static Uniform4fARB glUniform4fARB;
		public static Uniform1iARB glUniform1iARB;
		public static Uniform2iARB glUniform2iARB;
		public static Uniform3iARB glUniform3iARB;
		public static Uniform4iARB glUniform4iARB;
		public static Uniform1fvARB glUniform1fvARB;
		public static Uniform2fvARB glUniform2fvARB;
		public static Uniform3fvARB glUniform3fvARB;
		public static Uniform4fvARB glUniform4fvARB;
		public static Uniform1ivARB glUniform1ivARB;
		public static Uniform2ivARB glUniform2ivARB;
		public static Uniform3ivARB glUniform3ivARB;
		public static Uniform4ivARB glUniform4ivARB;
		public static UniformMatrix2fvARB glUniformMatrix2fvARB;
		public static UniformMatrix3fvARB glUniformMatrix3fvARB;
		public static UniformMatrix4fvARB glUniformMatrix4fvARB;
		public static GetObjectParameterfvARB glGetObjectParameterfvARB;
		public static GetObjectParameterivARB glGetObjectParameterivARB;
		public static GetInfoLogARB glGetInfoLogARB;
		public static GetAttachedObjectsARB glGetAttachedObjectsARB;
		public static GetUniformLocationARB glGetUniformLocationARB;
		public static GetActiveUniformARB glGetActiveUniformARB;
		public static GetUniformfvARB glGetUniformfvARB;
		public static GetUniformivARB glGetUniformivARB;
		public static GetShaderSourceARB glGetShaderSourceARB;

		public static ProgramParameteriARB glProgramParameteriARB;
		public static FramebufferTextureARB glFramebufferTextureARB;
		public static FramebufferTextureLayerARB glFramebufferTextureLayerARB;
		public static FramebufferTextureFaceARB glFramebufferTextureFaceARB;

		public static void InitShaderProgram(OpenGLContext ctx)
		{
			if(IsExtensionSupported("GL_ARB_shader_objects"))
			{
			glVertexAttrib1dARB = ctx.GetProc<VertexAttrib1dARB>("glVertexAttrib1dARB");
			glVertexAttrib1dvARB = ctx.GetProc<VertexAttrib1dvARB>("glVertexAttrib1dvARB");
			glVertexAttrib1fARB = ctx.GetProc<VertexAttrib1fARB>("glVertexAttrib1fARB");
			glVertexAttrib1fvARB = ctx.GetProc<VertexAttrib1fvARB>("glVertexAttrib1fvARB");
			glVertexAttrib1sARB = ctx.GetProc<VertexAttrib1sARB>("glVertexAttrib1sARB");
			glVertexAttrib1svARB = ctx.GetProc<VertexAttrib1svARB>("glVertexAttrib1svARB");
			glVertexAttrib2dARB = ctx.GetProc<VertexAttrib2dARB>("glVertexAttrib2dARB");
			glVertexAttrib2dvARB = ctx.GetProc<VertexAttrib2dvARB>("glVertexAttrib2dvARB");
			glVertexAttrib2fARB = ctx.GetProc<VertexAttrib2fARB>("glVertexAttrib2fARB");
			glVertexAttrib2fvARB = ctx.GetProc<VertexAttrib2fvARB>("glVertexAttrib2fvARB");
			glVertexAttrib2sARB = ctx.GetProc<VertexAttrib2sARB>("glVertexAttrib2sARB");
			glVertexAttrib2svARB = ctx.GetProc<VertexAttrib2svARB>("glVertexAttrib2svARB");
			glVertexAttrib3dARB = ctx.GetProc<VertexAttrib3dARB>("glVertexAttrib3dARB");
			glVertexAttrib3dvARB = ctx.GetProc<VertexAttrib3dvARB>("glVertexAttrib3dvARB");
			glVertexAttrib3fARB = ctx.GetProc<VertexAttrib3fARB>("glVertexAttrib3fARB");
			glVertexAttrib3fvARB = ctx.GetProc<VertexAttrib3fvARB>("glVertexAttrib3fvARB");
			glVertexAttrib3sARB = ctx.GetProc<VertexAttrib3sARB>("glVertexAttrib3sARB");
			glVertexAttrib3svARB = ctx.GetProc<VertexAttrib3svARB>("glVertexAttrib3svARB");
			glVertexAttrib4NbvARB = ctx.GetProc<VertexAttrib4NbvARB>("glVertexAttrib4NbvARB");
			glVertexAttrib4NivARB = ctx.GetProc<VertexAttrib4NivARB>("glVertexAttrib4NivARB");
			glVertexAttrib4NsvARB = ctx.GetProc<VertexAttrib4NsvARB>("glVertexAttrib4NsvARB");
			glVertexAttrib4NubARB = ctx.GetProc<VertexAttrib4NubARB>("glVertexAttrib4NubARB");
			glVertexAttrib4NubvARB = ctx.GetProc<VertexAttrib4NubvARB>("glVertexAttrib4NubvARB");
			glVertexAttrib4NuivARB = ctx.GetProc<VertexAttrib4NuivARB>("glVertexAttrib4NuivARB");
			glVertexAttrib4NusvARB = ctx.GetProc<VertexAttrib4NusvARB>("glVertexAttrib4NusvARB");
			glVertexAttrib4bvARB = ctx.GetProc<VertexAttrib4bvARB>("glVertexAttrib4bvARB");
			glVertexAttrib4dARB = ctx.GetProc<VertexAttrib4dARB>("glVertexAttrib4dARB");
			glVertexAttrib4dvARB = ctx.GetProc<VertexAttrib4dvARB>("glVertexAttrib4dvARB");
			glVertexAttrib4fARB = ctx.GetProc<VertexAttrib4fARB>("glVertexAttrib4fARB");
			glVertexAttrib4fvARB = ctx.GetProc<VertexAttrib4fvARB>("glVertexAttrib4fvARB");
			glVertexAttrib4ivARB = ctx.GetProc<VertexAttrib4ivARB>("glVertexAttrib4ivARB");
			glVertexAttrib4sARB = ctx.GetProc<VertexAttrib4sARB>("glVertexAttrib4sARB");
			glVertexAttrib4svARB = ctx.GetProc<VertexAttrib4svARB>("glVertexAttrib4svARB");
			glVertexAttrib4ubvARB = ctx.GetProc<VertexAttrib4ubvARB>("glVertexAttrib4ubvARB");
			glVertexAttrib4uivARB = ctx.GetProc<VertexAttrib4uivARB>("glVertexAttrib4uivARB");
			glVertexAttrib4usvARB = ctx.GetProc<VertexAttrib4usvARB>("glVertexAttrib4usvARB");
			glVertexAttribPointerARB = ctx.GetProc<VertexAttribPointerARB>("glVertexAttribPointerARB");
			glEnableVertexAttribArrayARB = ctx.GetProc<EnableVertexAttribArrayARB>("glEnableVertexAttribArrayARB");
			glDisableVertexAttribArrayARB = ctx.GetProc<DisableVertexAttribArrayARB>("glDisableVertexAttribArrayARB");
			glProgramStringARB = ctx.GetProc<ProgramStringARB>("glProgramStringARB");
			glBindProgramARB = ctx.GetProc<BindProgramARB>("glBindProgramARB");
			glDeleteProgramsARB = ctx.GetProc<DeleteProgramsARB>("glDeleteProgramsARB");
			glGenProgramsARB = ctx.GetProc<GenProgramsARB>("glGenProgramsARB");
			glProgramEnvParameter4dARB = ctx.GetProc<ProgramEnvParameter4dARB>("glProgramEnvParameter4dARB");
			glProgramEnvParameter4dvARB = ctx.GetProc<ProgramEnvParameter4dvARB>("glProgramEnvParameter4dvARB");
			glProgramEnvParameter4fARB = ctx.GetProc<ProgramEnvParameter4fARB>("glProgramEnvParameter4fARB");
			glProgramEnvParameter4fvARB = ctx.GetProc<ProgramEnvParameter4fvARB>("glProgramEnvParameter4fvARB");
			glProgramLocalParameter4dARB = ctx.GetProc<ProgramLocalParameter4dARB>("glProgramLocalParameter4dARB");
			glProgramLocalParameter4dvARB = ctx.GetProc<ProgramLocalParameter4dvARB>("glProgramLocalParameter4dvARB");
			glProgramLocalParameter4fARB = ctx.GetProc<ProgramLocalParameter4fARB>("glProgramLocalParameter4fARB");
			glProgramLocalParameter4fvARB = ctx.GetProc<ProgramLocalParameter4fvARB>("glProgramLocalParameter4fvARB");
			glGetProgramEnvParameterdvARB = ctx.GetProc<GetProgramEnvParameterdvARB>("glGetProgramEnvParameterdvARB");
			glGetProgramEnvParameterfvARB = ctx.GetProc<GetProgramEnvParameterfvARB>("glGetProgramEnvParameterfvARB");
			glGetProgramLocalParameterdvARB = ctx.GetProc<GetProgramLocalParameterdvARB>("glGetProgramLocalParameterdvARB");
			glGetProgramLocalParameterfvARB = ctx.GetProc<GetProgramLocalParameterfvARB>("glGetProgramLocalParameterfvARB");
			glGetProgramivARB = ctx.GetProc<GetProgramivARB>("glGetProgramivARB");
			glGetProgramStringARB = ctx.GetProc<GetProgramStringARB>("glGetProgramStringARB");
			glGetVertexAttribdvARB = ctx.GetProc<GetVertexAttribdvARB>("glGetVertexAttribdvARB");
			glGetVertexAttribfvARB = ctx.GetProc<GetVertexAttribfvARB>("glGetVertexAttribfvARB");
			glGetVertexAttribivARB = ctx.GetProc<GetVertexAttribivARB>("glGetVertexAttribivARB");
			glGetVertexAttribPointervARB = ctx.GetProc<GetVertexAttribPointervARB>("glGetVertexAttribPointervARB");
			glIsProgramARB = ctx.GetProc<IsProgramARB>("glIsProgramARB");

			glDeleteObjectARB = ctx.GetProc<DeleteObjectARB>("glDeleteObjectARB");
			glGetHandleARB = ctx.GetProc<GetHandleARB>("glGetHandleARB");
			glDetachObjectARB = ctx.GetProc<DetachObjectARB>("glDetachObjectARB");
			glCreateShaderObjectARB = ctx.GetProc<CreateShaderObjectARB>("glCreateShaderObjectARB");
			glShaderSourceARB = ctx.GetProc<ShaderSourceARB>("glShaderSourceARB");
			glCompileShaderARB = ctx.GetProc<CompileShaderARB>("glCompileShaderARB");
			glCreateProgramObjectARB = ctx.GetProc<CreateProgramObjectARB>("glCreateProgramObjectARB");
			glAttachObjectARB = ctx.GetProc<AttachObjectARB>("glAttachObjectARB");
			glLinkProgramARB = ctx.GetProc<LinkProgramARB>("glLinkProgramARB");
			glUseProgramObjectARB = ctx.GetProc<UseProgramObjectARB>("glUseProgramObjectARB");
			glValidateProgramARB = ctx.GetProc<ValidateProgramARB>("glValidateProgramARB");
			glUniform1fARB = ctx.GetProc<Uniform1fARB>("glUniform1fARB");
			glUniform2fARB = ctx.GetProc<Uniform2fARB>("glUniform2fARB");
			glUniform3fARB = ctx.GetProc<Uniform3fARB>("glUniform3fARB");
			glUniform4fARB = ctx.GetProc<Uniform4fARB>("glUniform4fARB");
			glUniform1iARB = ctx.GetProc<Uniform1iARB>("glUniform1iARB");
			glUniform2iARB = ctx.GetProc<Uniform2iARB>("glUniform2iARB");
			glUniform3iARB = ctx.GetProc<Uniform3iARB>("glUniform3iARB");
			glUniform4iARB = ctx.GetProc<Uniform4iARB>("glUniform4iARB");
			glUniform1fvARB = ctx.GetProc<Uniform1fvARB>("glUniform1fvARB");
			glUniform2fvARB = ctx.GetProc<Uniform2fvARB>("glUniform2fvARB");
			glUniform3fvARB = ctx.GetProc<Uniform3fvARB>("glUniform3fvARB");
			glUniform4fvARB = ctx.GetProc<Uniform4fvARB>("glUniform4fvARB");
			glUniform1ivARB = ctx.GetProc<Uniform1ivARB>("glUniform1ivARB");
			glUniform2ivARB = ctx.GetProc<Uniform2ivARB>("glUniform2ivARB");
			glUniform3ivARB = ctx.GetProc<Uniform3ivARB>("glUniform3ivARB");
			glUniform4ivARB = ctx.GetProc<Uniform4ivARB>("glUniform4ivARB");
			glUniformMatrix2fvARB = ctx.GetProc<UniformMatrix2fvARB>("glUniformMatrix2fvARB");
			glUniformMatrix3fvARB = ctx.GetProc<UniformMatrix3fvARB>("glUniformMatrix3fvARB");
			glUniformMatrix4fvARB = ctx.GetProc<UniformMatrix4fvARB>("glUniformMatrix4fvARB");
			glGetObjectParameterfvARB = ctx.GetProc<GetObjectParameterfvARB>("glGetObjectParameterfvARB");
			glGetObjectParameterivARB = ctx.GetProc<GetObjectParameterivARB>("glGetObjectParameterivARB");
			glGetInfoLogARB = ctx.GetProc<GetInfoLogARB>("glGetInfoLogARB");
			glGetAttachedObjectsARB = ctx.GetProc<GetAttachedObjectsARB>("glGetAttachedObjectsARB");
			glGetUniformLocationARB = ctx.GetProc<GetUniformLocationARB>("glGetUniformLocationARB");
			glGetActiveUniformARB = ctx.GetProc<GetActiveUniformARB>("glGetActiveUniformARB");
			glGetUniformfvARB = ctx.GetProc<GetUniformfvARB>("glGetUniformfvARB");
			glGetUniformivARB = ctx.GetProc<GetUniformivARB>("glGetUniformivARB");
			glGetShaderSourceARB = ctx.GetProc<GetShaderSourceARB>("glGetShaderSourceARB");

			glProgramParameteriARB = ctx.GetProc<ProgramParameteriARB>("glProgramParameteriARB");
			glFramebufferTextureARB = ctx.GetProc<FramebufferTextureARB>("glFramebufferTextureARB");
			glFramebufferTextureLayerARB = ctx.GetProc<FramebufferTextureLayerARB>("glFramebufferTextureLayerARB");
			glFramebufferTextureFaceARB = ctx.GetProc<FramebufferTextureFaceARB>("glFramebufferTextureFaceARB");
			}
		}

		#endregion

		#region Occlusion Query

		public const uint GL_QUERY_COUNTER_BITS_ARB = 0x8864;
		public const uint GL_CURRENT_QUERY_ARB = 0x8865;
		public const uint GL_QUERY_RESULT_ARB = 0x8866;
		public const uint GL_QUERY_RESULT_AVAILABLE_ARB = 0x8867;
		public const uint GL_SAMPLES_PASSED_ARB = 0x8914;

		public delegate void GenQueriesARB(int n, uint[] ids);
		public delegate void DeleteQueriesARB(int n, uint[] ids);
		public delegate boolean IsQueryARB(int id);
		public delegate void BeginQueryARB(uint target, uint id);
		public delegate void EndQueryARB(uint target);
		public delegate void GetQueryivARB(uint target, uint pname, int[] parameters);
		public delegate void GetQueryObjectivARB(uint id, uint pname, int[] parameters);
		public delegate void GetQueryObjectuivARB(uint id, uint pname, uint[] parameters);

		public static GenQueriesARB glGenQueriesARB;
		public static DeleteQueriesARB glDeleteQueriesARB;
		public static IsQueryARB glIsQueryARB;
		public static BeginQueryARB glBeginQueryARB;
		public static EndQueryARB glEndQueryARB;
		public static GetQueryivARB glGetQueryivARB;
		public static GetQueryObjectivARB glGetQueryObjectivARB;
		public static GetQueryObjectuivARB glGetQueryObjectuivARB;

		public static void InitOcclusionQueries(OpenGLContext ctx)
		{
			if (IsExtensionSupported("GL_ARB_occlusion_query"))
			{
				glGenQueriesARB = ctx.GetProc<GenQueriesARB>("glGenQueriesARB");
				glDeleteQueriesARB = ctx.GetProc<DeleteQueriesARB>("glDeleteQueriesARB");
				glIsQueryARB = ctx.GetProc<IsQueryARB>("glIsQueryARB");
				glBeginQueryARB = ctx.GetProc<BeginQueryARB>("glBeginQueryARB");
				glEndQueryARB = ctx.GetProc<EndQueryARB>("glEndQueryARB");
				glGetQueryivARB = ctx.GetProc<GetQueryivARB>("glGetQueryivARB");
				glGetQueryObjectivARB = ctx.GetProc<GetQueryObjectivARB>("glGetQueryObjectivARB");
				glGetQueryObjectuivARB = ctx.GetProc<GetQueryObjectuivARB>("glGetQueryObjectuivARB");
			}
		}

		#endregion
	}
}
