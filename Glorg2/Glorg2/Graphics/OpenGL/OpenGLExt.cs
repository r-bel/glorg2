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
				var count = new int[1];
				glGetIntegerv(Const.GL_NUM_EXTENSIONS, count);
				List<string> extensions = new List<string>();
				for (int i = 0; i < count[0]; i++)
				{
					IntPtr ptr = glGetStringi(Const.GL_EXTENSIONS, i);
					string s = Marshal.PtrToStringAnsi(ptr);
					extensions.Add(s);
				}

				Extensions = extensions.AsReadOnly();
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
		public const uint GL_BUFFER_SIZE_ARB =                0x8764;
		public const uint GL_BUFFER_USAGE_ARB =               0x8765;


		public const uint GL_ARRAY_BUFFER_BINDING_ARB =       0x8894;
		public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING_ARB = 0x8895;
		public const uint GL_VERTEX_ARRAY_BUFFER_BINDING_ARB = 0x8896;
		public const uint GL_NORMAL_ARRAY_BUFFER_BINDING_ARB = 0x8897;
		public const uint GL_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x8898;
		public const uint GL_INDEX_ARRAY_BUFFER_BINDING_ARB = 0x8899;
		public const uint GL_TEXTURE_COORD_ARRAY_BUFFER_BINDING_ARB = 0x889A;
		public const uint GL_EDGE_FLAG_ARRAY_BUFFER_BINDING_ARB = 0x889B;
		public const uint GL_SECONDARY_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x889C;
		public const uint GL_FOG_COORDINATE_ARRAY_BUFFER_BINDING_ARB = 0x889D;
		public const uint GL_WEIGHT_ARRAY_BUFFER_BINDING_ARB = 0x889E;
		public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING_ARB = 0x889F*/;
		public enum VboAccess
		{
			GL_READ_ONLY_ARB = 0x88B8,
			GL_WRITE_ONLY_ARB = 0x88B9,
			GL_READ_WRITE_ARB = 0x88BA,
		}
		/*public const uint GL_BUFFER_ACCESS_ARB =              0x88BB;
		public const uint GL_BUFFER_MAPPED_ARB =              0x88BC;
		public const uint GL_BUFFER_MAP_POINTER_ARB =         0x88BD*/;
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
		public unsafe delegate void BufferSubDataARB(VboTarget target, int offset, int size, IntPtr data);
		public delegate void DeleteBuffersARB(int n, uint[] ids);
		public unsafe delegate void* MapBufferARB(VboTarget target, VboAccess access);
		public delegate boolean UnmapBufferARB(VboTarget target);

		public delegate IntPtr GetStringi(uint pname, int index);
		public static GetStringi glGetStringi;


		public static GenBuffersARB glGenBuffersARB;
		public static BindBufferARB glBindBufferARB;
		public static BufferDataARB glBufferDataARB;
		public static BufferSubDataARB glBufferSubDataARB;
		public static DeleteBuffersARB glDeleteBuffersARB;
		public static MapBufferARB glMapBufferARB;
		public static UnmapBufferARB glUnmapBufferARB;

		public static void InitGeneral(OpenGLContext ctx)
		{
			glGetStringi = ctx.GetProc<GetStringi>("glGetStringi");
		}

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
				Console.WriteLine(glGenBuffersARB.ToString() + glGenBuffersARB != null ? " OK" : " FAILED");
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
		public unsafe delegate void VertexAttribPointerARB(uint index, int size, uint type, boolean normalized, int stride, IntPtr pointer);
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

		public delegate void DeleteObjectARB(uint obj);
		public delegate uint GetHandleARB(uint pname);
		public delegate void DetachObjectARB(uint containerObj, uint attachedObj);
		public delegate uint CreateShaderObjectARB(uint shaderType);
		public delegate void ShaderSourceARB(uint shaderObj, int count, string[] code, int[] length);
		public delegate void CompileShaderARB(uint shaderObj);
		public delegate uint CreateProgramObjectARB();
		public delegate void AttachObjectARB(uint containerObj, uint obj);
		public delegate void LinkProgramARB(uint programObj);
		public delegate void UseProgramObjectARB(uint programObj);
		public delegate void ValidateProgramARB(uint programObj);
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
		public delegate void GetObjectParameterfvARB(uint obj, uint pname, float[] parameters);
		public delegate void GetObjectParameterivARB(uint obj, uint pname, int[] parameters);
		public delegate void GetInfoLogARB(uint obj, int maxLength, ref int length, byte[] infoLog);
		public delegate void GetAttachedObjectsARB(uint containerObj, int maxCount, ref int count, uint[] obj);
		public delegate int GetUniformLocationARB(uint programObj, byte[] name);
		public delegate void GetActiveUniformARB(uint programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, byte[] name);
		public delegate void GetUniformfvARB(uint programObj, int location, float[] parameters);
		public delegate void GetUniformivARB(uint programObj, int location, int[] parameters);
		public delegate void GetShaderSourceARB(uint obj, int maxLength, ref int length, byte[] source);

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
			if (IsExtensionSupported("GL_ARB_shader_objects"))
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

		#region Multi-texturing



		public delegate void ActiveTextureARB(uint texture);
		public delegate void ClientActiveTextureARB(uint texture);
		public delegate void MultiTexCoord1dARB(uint target, double s);
		public delegate void MultiTexCoord1dvARB(uint target, double[] v);
		public delegate void MultiTexCoord1fARB(uint target, float s);
		public delegate void MultiTexCoord1fvARB(uint target, float[] v);
		public delegate void MultiTexCoord1iARB(uint target, int s);
		public delegate void MultiTexCoord1ivARB(uint target, int[] v);
		public delegate void MultiTexCoord1sARB(uint target, short s);
		public delegate void MultiTexCoord1svARB(uint target, short[] v);
		public delegate void MultiTexCoord2dARB(uint target, double s, double t);
		public delegate void MultiTexCoord2dvARB(uint target, double[] v);
		public delegate void MultiTexCoord2fARB(uint target, float s, float t);
		public delegate void MultiTexCoord2fvARB(uint target, float[] v);
		public delegate void MultiTexCoord2iARB(uint target, int s, int t);
		public delegate void MultiTexCoord2ivARB(uint target, int[] v);
		public delegate void MultiTexCoord2sARB(uint target, short s, short t);
		public delegate void MultiTexCoord2svARB(uint target, short[] v);
		public delegate void MultiTexCoord3dARB(uint target, double s, double t, double r);
		public delegate void MultiTexCoord3dvARB(uint target, double[] v);
		public delegate void MultiTexCoord3fARB(uint target, float s, float t, float r);
		public delegate void MultiTexCoord3fvARB(uint target, float[] v);
		public delegate void MultiTexCoord3iARB(uint target, int s, int t, int r);
		public delegate void MultiTexCoord3ivARB(uint target, int[] v);
		public delegate void MultiTexCoord3sARB(uint target, short s, short t, short r);
		public delegate void MultiTexCoord3svARB(uint target, short[] v);
		public delegate void MultiTexCoord4dARB(uint target, double s, double t, double r, double q);
		public delegate void MultiTexCoord4dvARB(uint target, double[] v);
		public delegate void MultiTexCoord4fARB(uint target, float s, float t, float r, float q);
		public delegate void MultiTexCoord4fvARB(uint target, float[] v);
		public delegate void MultiTexCoord4iARB(uint target, int s, int t, int r, int q);
		public delegate void MultiTexCoord4ivARB(uint target, int[] v);
		public delegate void MultiTexCoord4sARB(uint target, short s, short t, short r, short q);
		public delegate void MultiTexCoord4svARB(uint target, short[] v);

		public static ActiveTextureARB glActiveTextureARB;
		public static ClientActiveTextureARB glClientActiveTextureARB;
		public static MultiTexCoord1dARB glMultiTexCoord1dARB;
		public static MultiTexCoord1dvARB glMultiTexCoord1dvARB;
		public static MultiTexCoord1fARB glMultiTexCoord1fARB;
		public static MultiTexCoord1fvARB glMultiTexCoord1fvARB;
		public static MultiTexCoord1iARB glMultiTexCoord1iARB;
		public static MultiTexCoord1ivARB glMultiTexCoord1ivARB;
		public static MultiTexCoord1sARB glMultiTexCoord1sARB;
		public static MultiTexCoord1svARB glMultiTexCoord1svARB;
		public static MultiTexCoord2dARB glMultiTexCoord2dARB;
		public static MultiTexCoord2dvARB glMultiTexCoord2dvARB;
		public static MultiTexCoord2fARB glMultiTexCoord2fARB;
		public static MultiTexCoord2fvARB glMultiTexCoord2fvARB;
		public static MultiTexCoord2iARB glMultiTexCoord2iARB;
		public static MultiTexCoord2ivARB glMultiTexCoord2ivARB;
		public static MultiTexCoord2sARB glMultiTexCoord2sARB;
		public static MultiTexCoord2svARB glMultiTexCoord2svARB;
		public static MultiTexCoord3dARB glMultiTexCoord3dARB;
		public static MultiTexCoord3dvARB glMultiTexCoord3dvARB;
		public static MultiTexCoord3fARB glMultiTexCoord3fARB;
		public static MultiTexCoord3fvARB glMultiTexCoord3fvARB;
		public static MultiTexCoord3iARB glMultiTexCoord3iARB;
		public static MultiTexCoord3ivARB glMultiTexCoord3ivARB;
		public static MultiTexCoord3sARB glMultiTexCoord3sARB;
		public static MultiTexCoord3svARB glMultiTexCoord3svARB;
		public static MultiTexCoord4dARB glMultiTexCoord4dARB;
		public static MultiTexCoord4dvARB glMultiTexCoord4dvARB;
		public static MultiTexCoord4fARB glMultiTexCoord4fARB;
		public static MultiTexCoord4fvARB glMultiTexCoord4fvARB;
		public static MultiTexCoord4iARB glMultiTexCoord4iARB;
		public static MultiTexCoord4ivARB glMultiTexCoord4ivARB;
		public static MultiTexCoord4sARB glMultiTexCoord4sARB;
		public static MultiTexCoord4svARB glMultiTexCoord4svARB;

		public static void InitMultiTexture(OpenGLContext ctx)
		{

			glActiveTextureARB = ctx.GetProc<ActiveTextureARB>("glActiveTextureARB");
			glClientActiveTextureARB = ctx.GetProc<ClientActiveTextureARB>("glClientActiveTextureARB");
			glMultiTexCoord1dARB = ctx.GetProc<MultiTexCoord1dARB>("glMultiTexCoord1dARB");
			glMultiTexCoord1dvARB = ctx.GetProc<MultiTexCoord1dvARB>("glMultiTexCoord1dvARB");
			glMultiTexCoord1fARB = ctx.GetProc<MultiTexCoord1fARB>("glMultiTexCoord1fARB");
			glMultiTexCoord1fvARB = ctx.GetProc<MultiTexCoord1fvARB>("glMultiTexCoord1fvARB");
			glMultiTexCoord1iARB = ctx.GetProc<MultiTexCoord1iARB>("glMultiTexCoord1iARB");
			glMultiTexCoord1ivARB = ctx.GetProc<MultiTexCoord1ivARB>("glMultiTexCoord1ivARB");
			glMultiTexCoord1sARB = ctx.GetProc<MultiTexCoord1sARB>("glMultiTexCoord1sARB");
			glMultiTexCoord1svARB = ctx.GetProc<MultiTexCoord1svARB>("glMultiTexCoord1svARB");
			glMultiTexCoord2dARB = ctx.GetProc<MultiTexCoord2dARB>("glMultiTexCoord2dARB");
			glMultiTexCoord2dvARB = ctx.GetProc<MultiTexCoord2dvARB>("glMultiTexCoord2dvARB");
			glMultiTexCoord2fARB = ctx.GetProc<MultiTexCoord2fARB>("glMultiTexCoord2fARB");
			glMultiTexCoord2fvARB = ctx.GetProc<MultiTexCoord2fvARB>("glMultiTexCoord2fvARB");
			glMultiTexCoord2iARB = ctx.GetProc<MultiTexCoord2iARB>("glMultiTexCoord2iARB");
			glMultiTexCoord2ivARB = ctx.GetProc<MultiTexCoord2ivARB>("glMultiTexCoord2ivARB");
			glMultiTexCoord2sARB = ctx.GetProc<MultiTexCoord2sARB>("glMultiTexCoord2sARB");
			glMultiTexCoord2svARB = ctx.GetProc<MultiTexCoord2svARB>("glMultiTexCoord2svARB");
			glMultiTexCoord3dARB = ctx.GetProc<MultiTexCoord3dARB>("glMultiTexCoord3dARB");
			glMultiTexCoord3dvARB = ctx.GetProc<MultiTexCoord3dvARB>("glMultiTexCoord3dvARB");
			glMultiTexCoord3fARB = ctx.GetProc<MultiTexCoord3fARB>("glMultiTexCoord3fARB");
			glMultiTexCoord3fvARB = ctx.GetProc<MultiTexCoord3fvARB>("glMultiTexCoord3fvARB");
			glMultiTexCoord3iARB = ctx.GetProc<MultiTexCoord3iARB>("glMultiTexCoord3iARB");
			glMultiTexCoord3ivARB = ctx.GetProc<MultiTexCoord3ivARB>("glMultiTexCoord3ivARB");
			glMultiTexCoord3sARB = ctx.GetProc<MultiTexCoord3sARB>("glMultiTexCoord3sARB");
			glMultiTexCoord3svARB = ctx.GetProc<MultiTexCoord3svARB>("glMultiTexCoord3svARB");
			glMultiTexCoord4dARB = ctx.GetProc<MultiTexCoord4dARB>("glMultiTexCoord4dARB");
			glMultiTexCoord4dvARB = ctx.GetProc<MultiTexCoord4dvARB>("glMultiTexCoord4dvARB");
			glMultiTexCoord4fARB = ctx.GetProc<MultiTexCoord4fARB>("glMultiTexCoord4fARB");
			glMultiTexCoord4fvARB = ctx.GetProc<MultiTexCoord4fvARB>("glMultiTexCoord4fvARB");
			glMultiTexCoord4iARB = ctx.GetProc<MultiTexCoord4iARB>("glMultiTexCoord4iARB");
			glMultiTexCoord4ivARB = ctx.GetProc<MultiTexCoord4ivARB>("glMultiTexCoord4ivARB");
			glMultiTexCoord4sARB = ctx.GetProc<MultiTexCoord4sARB>("glMultiTexCoord4sARB");
			glMultiTexCoord4svARB = ctx.GetProc<MultiTexCoord4svARB>("glMultiTexCoord4svARB");
		}
		#endregion

		#region Framebuffer objects



		public delegate boolean IsRenderbuffer(uint renderbuffer);
		public delegate void BindRenderbuffer(uint target, uint renderbuffer);
		public delegate void DeleteRenderbuffers(int n, uint[] renderbuffers);
		public delegate void GenRenderbuffers(int n, uint[] renderbuffers);
		public delegate void RenderbufferStorage(uint target, uint internalformat, int width, int height);
		public delegate void GetRenderbufferParameteriv(uint target, uint pname, int[] parameters);
		public delegate boolean IsFramebuffer(uint framebuffer);
		public delegate void BindFramebuffer(uint target, uint framebuffer);
		public delegate void DeleteFramebuffers(int n, uint[] framebuffers);
		public delegate void GenFramebuffers(int n, uint[] framebuffers);
		public delegate uint CheckFramebufferStatus(uint target);
		public delegate void FramebufferTexture1D(uint target, uint attachment, uint textarget, uint texture, int level);
		public delegate void FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level);
		public delegate void FramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);
		public delegate void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);
		public delegate void GetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int[] parameters);
		public delegate void GenerateMipmap(uint target);
		public delegate void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);
		public delegate void RenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height);
		public delegate void FramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer);

		public static IsRenderbuffer glIsRenderbuffer;
		public static BindRenderbuffer glBindRenderbuffer;
		public static DeleteRenderbuffers glDeleteRenderbuffers;
		public static GenRenderbuffers glGenRenderbuffers;
		public static RenderbufferStorage glRenderbufferStorage;
		public static GetRenderbufferParameteriv glGetRenderbufferParameteriv;
		public static IsFramebuffer glIsFramebuffer;
		public static BindFramebuffer glBindFramebuffer;
		public static DeleteFramebuffers glDeleteFramebuffers;
		public static GenFramebuffers glGenFramebuffers;
		public static CheckFramebufferStatus glCheckFramebufferStatus;
		public static FramebufferTexture1D glFramebufferTexture1D;
		public static FramebufferTexture2D glFramebufferTexture2D;
		public static FramebufferTexture3D glFramebufferTexture3D;
		public static FramebufferRenderbuffer glFramebufferRenderbuffer;
		public static GetFramebufferAttachmentParameteriv glGetFramebufferAttachmentParameteriv;
		public static GenerateMipmap glGenerateMipmap;
		public static BlitFramebuffer glBlitFramebuffer;
		public static RenderbufferStorageMultisample glRenderbufferStorageMultisample;
		public static FramebufferTextureLayer glFramebufferTextureLayer;

		public static void InitFramebuffers(OpenGLContext ctx)
		{
			glIsRenderbuffer = ctx.GetProc<IsRenderbuffer>("glIsRenderbuffer");
			glBindRenderbuffer = ctx.GetProc<BindRenderbuffer>("glBindRenderbuffer");
			glDeleteRenderbuffers = ctx.GetProc<DeleteRenderbuffers>("glDeleteRenderbuffers");
			glGenRenderbuffers = ctx.GetProc<GenRenderbuffers>("glGenRenderbuffers");
			glRenderbufferStorage = ctx.GetProc<RenderbufferStorage>("glRenderbufferStorage");
			glGetRenderbufferParameteriv = ctx.GetProc<GetRenderbufferParameteriv>("glGetRenderbufferParameteriv");
			glIsFramebuffer = ctx.GetProc<IsFramebuffer>("glIsFramebuffer");
			glBindFramebuffer = ctx.GetProc<BindFramebuffer>("glBindFramebuffer");
			glDeleteFramebuffers = ctx.GetProc<DeleteFramebuffers>("glDeleteFramebuffers");
			glGenFramebuffers = ctx.GetProc<GenFramebuffers>("glGenFramebuffers");
			glCheckFramebufferStatus = ctx.GetProc<CheckFramebufferStatus>("glCheckFramebufferStatus");
			glFramebufferTexture1D = ctx.GetProc<FramebufferTexture1D>("glFramebufferTexture1D");
			glFramebufferTexture2D = ctx.GetProc<FramebufferTexture2D>("glFramebufferTexture2D");
			glFramebufferTexture3D = ctx.GetProc<FramebufferTexture3D>("glFramebufferTexture3D");
			glFramebufferRenderbuffer = ctx.GetProc<FramebufferRenderbuffer>("glFramebufferRenderbuffer");
			glGetFramebufferAttachmentParameteriv = ctx.GetProc<GetFramebufferAttachmentParameteriv>("glGetFramebufferAttachmentParameteriv");
			glGenerateMipmap = ctx.GetProc<GenerateMipmap>("glGenerateMipmap");
			glBlitFramebuffer = ctx.GetProc<BlitFramebuffer>("glBlitFramebuffer");
			glRenderbufferStorageMultisample = ctx.GetProc<RenderbufferStorageMultisample>("glRenderbufferStorageMultisample");
			glFramebufferTextureLayer = ctx.GetProc<FramebufferTextureLayer>("glFramebufferTextureLayer");
		}
		#endregion

		#region GL_EXT_gup_shader4
		public delegate void GetUniformuivEXT(uint program, int location, uint[] parameters);
		public delegate void BindFragDataLocationEXT(uint program, uint color, string name);
		public delegate int GetFragDataLocationEXT(uint program, out string name);
		public delegate void Uniform1uiEXT(int location, int v0);
		public delegate void Uniform2uiEXT(int location, int v0, uint v1);
		public delegate void Uniform3uiEXT(int location, int v0, uint v1, uint v2);
		public delegate void Uniform4uiEXT(int location, int v0, uint v1, uint v2, uint v3);
		public delegate void Uniform1uivEXT(int location, int count, uint[] value);
		public delegate void Uniform2uivEXT(int location, int count, uint[] value);
		public delegate void Uniform3uivEXT(int location, int count, uint[] value);
		public delegate void Uniform4uivEXT(int location, int count, uint[] value);

		public static GetUniformuivEXT glGetUniformuivEXT;
		public static BindFragDataLocationEXT glBindFragDataLocationEXT;
		public static GetFragDataLocationEXT glGetFragDataLocationEXT;
		public static Uniform1uiEXT glUniform1uiEXT;
		public static Uniform2uiEXT glUniform2uiEXT;
		public static Uniform3uiEXT glUniform3uiEXT;
		public static Uniform4uiEXT glUniform4uiEXT;
		public static Uniform1uivEXT glUniform1uivEXT;
		public static Uniform2uivEXT glUniform2uivEXT;
		public static Uniform3uivEXT glUniform3uivEXT;
		public static Uniform4uivEXT glUniform4uivEXT;

		public static void InitGpuShader4(OpenGLContext ctx)
		{
			glGetUniformuivEXT = ctx.GetProc<GetUniformuivEXT>("glGetUniformuivEXT");
			glBindFragDataLocationEXT = ctx.GetProc<BindFragDataLocationEXT>("glBindFragDataLocationEXT");
			glGetFragDataLocationEXT = ctx.GetProc<GetFragDataLocationEXT>("glGetFragDataLocationEXT");
			glUniform1uiEXT = ctx.GetProc<Uniform1uiEXT>("glUniform1uiEXT");
			glUniform2uiEXT = ctx.GetProc<Uniform2uiEXT>("glUniform2uiEXT");
			glUniform3uiEXT = ctx.GetProc<Uniform3uiEXT>("glUniform3uiEXT");
			glUniform4uiEXT = ctx.GetProc<Uniform4uiEXT>("glUniform4uiEXT");
			glUniform1uivEXT = ctx.GetProc<Uniform1uivEXT>("glUniform1uivEXT");
			glUniform2uivEXT = ctx.GetProc<Uniform2uivEXT>("glUniform2uivEXT");
			glUniform3uivEXT = ctx.GetProc<Uniform3uivEXT>("glUniform3uivEXT");
			glUniform4uivEXT = ctx.GetProc<Uniform4uivEXT>("glUniform4uivEXT");
		}

		#endregion


		#region OpenGL_1_3
		public delegate void ActiveTexture(uint texture);
		public delegate void SampleCoverage(float value, boolean invert);
		public delegate void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data);
		public delegate void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data);
		public delegate void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data);
		public delegate void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
		public delegate void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
		public delegate void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
		public delegate void GetCompressedTexImage(uint target, int level, IntPtr img);

		public static ActiveTexture glActiveTexture;
		public static SampleCoverage glSampleCoverage;
		public static CompressedTexImage3D glCompressedTexImage3D;
		public static CompressedTexImage2D glCompressedTexImage2D;
		public static CompressedTexImage1D glCompressedTexImage1D;
		public static CompressedTexSubImage3D glCompressedTexSubImage3D;
		public static CompressedTexSubImage2D glCompressedTexSubImage2D;
		public static CompressedTexSubImage1D glCompressedTexSubImage1D;
		public static GetCompressedTexImage glGetCompressedTexImage;

		public static void InitGl13(OpenGLContext ctx)
		{
			glActiveTexture = ctx.GetProc<ActiveTexture>("glActiveTexture");
			glSampleCoverage = ctx.GetProc<SampleCoverage>("glSampleCoverage");
			glCompressedTexImage3D = ctx.GetProc<CompressedTexImage3D>("glCompressedTexImage3D");
			glCompressedTexImage2D = ctx.GetProc<CompressedTexImage2D>("glCompressedTexImage2D");
			glCompressedTexImage1D = ctx.GetProc<CompressedTexImage1D>("glCompressedTexImage1D");
			glCompressedTexSubImage3D = ctx.GetProc<CompressedTexSubImage3D>("glCompressedTexSubImage3D");
			glCompressedTexSubImage2D = ctx.GetProc<CompressedTexSubImage2D>("glCompressedTexSubImage2D");
			glCompressedTexSubImage1D = ctx.GetProc<CompressedTexSubImage1D>("glCompressedTexSubImage1D");
			glGetCompressedTexImage = ctx.GetProc<GetCompressedTexImage>("glGetCompressedTexImage");
		}
		#endregion

		#region OpenGL_2_0
		public delegate void BlendEquationSeparate(uint modeRGB, uint modeAlpha);
		public delegate void DrawBuffers(int n, uint[] bufs);
		public delegate void StencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass);
		public delegate void StencilFuncSeparate(uint frontfunc, uint backfunc, int reference, uint mask);
		public delegate void StencilMaskSeparate(uint face, uint mask);
		public delegate void AttachShader(uint program, uint shader);
		public delegate void BindAttribLocation(uint program, uint index, string name);
		public delegate void CompileShader(uint shader);
		public delegate uint CreateProgram();
		public delegate uint CreateShader(uint type);
		public delegate void DeleteProgram(uint program);
		public delegate void DeleteShader(uint shader);
		public delegate void DetachShader(uint program, uint shader);
		public delegate void DisableVertexAttribArray(uint index);
		public delegate void EnableVertexAttribArray(uint index);
		public delegate void GetActiveAttrib(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);
		public delegate void GetActiveUniform(uint program, uint index, int bufSize, ref int length, ref int size, ref uint type, byte[] name);
		public delegate void GetAttachedShaders(uint program, int maxCount, int[] count, uint[] obj);
		public delegate int GetAttribLocation(uint program, string name);
		public delegate void GetProgramiv(uint program, uint pname, int[] parameters);
		public delegate void GetProgramInfoLog(uint program, int bufSize, ref int length, byte[] infoLog);
		public delegate void GetShaderiv(uint shader, uint pname, int[] parameters);
		public delegate void GetShaderInfoLog(uint shader, int bufSize, ref int length, byte[] infoLog);
		public delegate void GetShaderSource(uint shader, int bufSize, int[] length, string source);
		public delegate int GetUniformLocation(uint program, byte[] name);
		public delegate void GetUniformfv(uint program, int location, float[] parameters);
		public delegate void GetUniformiv(uint program, int location, int[] parameters);
		public delegate void GetVertexAttribdv(uint index, uint pname, double[] parameters);
		public delegate void GetVertexAttribfv(uint index, uint pname, float[] parameters);
		public delegate void GetVertexAttribiv(uint index, uint pname, int[] parameters);
		public delegate void GetVertexAttribPointerv(uint index, uint pname, IntPtr[] pointer);
		public delegate boolean IsProgram(uint program);
		public delegate boolean IsShader(uint shader);
		public delegate void LinkProgram(uint program);
		public delegate void ShaderSource(uint shader, int count, string[] strings, int[] length);
		public delegate void UseProgram(uint program);
		public delegate void Uniform1f(int location, float v0);
		public delegate void Uniform2f(int location, float v0, float v1);
		public delegate void Uniform3f(int location, float v0, float v1, float v2);
		public delegate void Uniform4f(int location, float v0, float v1, float v2, float v3);
		public delegate void Uniform1i(int location, int v0);
		public delegate void Uniform2i(int location, int v0, int v1);
		public delegate void Uniform3i(int location, int v0, int v1, int v2);
		public delegate void Uniform4i(int location, int v0, int v1, int v2, int v3);
		public delegate void Uniform1fv(int location, int count, float[] value);
		public delegate void Uniform2fv(int location, int count, ref Vector2 value);
		public delegate void Uniform3fv(int location, int count, ref Vector3 value);
		public delegate void Uniform4fv(int location, int count, ref Vector4 value);
		public delegate void Uniform1iv(int location, int count, ref int value);
		public delegate void Uniform2iv(int location, int count, ref Vector2Int value);
		public delegate void Uniform3iv(int location, int count, ref Vector3Int value);
		public delegate void Uniform4iv(int location, int count, ref Vector4Int value);
		public delegate void UniformMatrix2fv(int location, int count, boolean transpose, float[] value);
		public delegate void UniformMatrix3fv(int location, int count, boolean transpose, float[] value);
		public delegate void UniformMatrix4fv(int location, int count, boolean transpose, ref Matrix value);
		public delegate void ValidateProgram(uint program);
		public delegate void VertexAttrib1d(uint index, double x);
		public delegate void VertexAttrib1dv(uint index, double[] v);
		public delegate void VertexAttrib1f(uint index, float x);
		public delegate void VertexAttrib1fv(uint index, float[] v);
		public delegate void VertexAttrib1s(uint index, short x);
		public delegate void VertexAttrib1sv(uint index, short[] v);
		public delegate void VertexAttrib2d(uint index, double x, double y);
		public delegate void VertexAttrib2dv(uint index, double[] v);
		public delegate void VertexAttrib2f(uint index, float x, float y);
		public delegate void VertexAttrib2fv(uint index, float[] v);
		public delegate void VertexAttrib2s(uint index, short x, short y);
		public delegate void VertexAttrib2sv(uint index, short[] v);
		public delegate void VertexAttrib3d(uint index, double x, double y, double z);
		public delegate void VertexAttrib3dv(uint index, double[] v);
		public delegate void VertexAttrib3f(uint index, float x, float y, float z);
		public delegate void VertexAttrib3fv(uint index, float[] v);
		public delegate void VertexAttrib3s(uint index, short x, short y, short z);
		public delegate void VertexAttrib3sv(uint index, short[] v);
		public delegate void VertexAttrib4Nbv(uint index, byte[] v);
		public delegate void VertexAttrib4Niv(uint index, int[] v);
		public delegate void VertexAttrib4Nsv(uint index, short[] v);
		public delegate void VertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w);
		public delegate void VertexAttrib4Nubv(uint index, byte[] v);
		public delegate void VertexAttrib4Nuiv(uint index, uint[] v);
		public delegate void VertexAttrib4Nusv(uint index, ushort[] v);
		public delegate void VertexAttrib4bv(uint index, byte[] v);
		public delegate void VertexAttrib4d(uint index, double x, double y, double z, double w);
		public delegate void VertexAttrib4dv(uint index, double[] v);
		public delegate void VertexAttrib4f(uint index, float x, float y, float z, float w);
		public delegate void VertexAttrib4fv(uint index, float[] v);
		public delegate void VertexAttrib4iv(uint index, int[] v);
		public delegate void VertexAttrib4s(uint index, short x, short y, short z, short w);
		public delegate void VertexAttrib4sv(uint index, short[] v);
		public delegate void VertexAttrib4ubv(uint index, byte[] v);
		public delegate void VertexAttrib4uiv(uint index, uint[] v);
		public delegate void VertexAttrib4usv(uint index, ushort[] v);
		public delegate void VertexAttribPointer(uint index, int size, uint type, boolean normalized, int stride, IntPtr pointer);


		public static BlendEquationSeparate glBlendEquationSeparate;
		public static DrawBuffers glDrawBuffers;
		public static StencilOpSeparate glStencilOpSeparate;
		public static StencilFuncSeparate glStencilFuncSeparate;
		public static StencilMaskSeparate glStencilMaskSeparate;
		public static AttachShader glAttachShader;
		public static BindAttribLocation glBindAttribLocation;
		public static CompileShader glCompileShader;
		public static CreateProgram glCreateProgram;
		public static CreateShader glCreateShader;
		public static DeleteProgram glDeleteProgram;
		public static DeleteShader glDeleteShader;
		public static DetachShader glDetachShader;
		public static DisableVertexAttribArray glDisableVertexAttribArray;
		public static EnableVertexAttribArray glEnableVertexAttribArray;
		public static GetActiveAttrib glGetActiveAttrib;
		public static GetActiveUniform glGetActiveUniform;
		public static GetAttachedShaders glGetAttachedShaders;
		public static GetAttribLocation glGetAttribLocation;
		public static GetProgramiv glGetProgramiv;
		public static GetProgramInfoLog glGetProgramInfoLog;
		public static GetShaderiv glGetShaderiv;
		public static GetShaderInfoLog glGetShaderInfoLog;
		public static GetShaderSource glGetShaderSource;
		public static GetUniformLocation glGetUniformLocation;
		public static GetUniformfv glGetUniformfv;
		public static GetUniformiv glGetUniformiv;
		public static GetVertexAttribdv glGetVertexAttribdv;
		public static GetVertexAttribfv glGetVertexAttribfv;
		public static GetVertexAttribiv glGetVertexAttribiv;
		public static GetVertexAttribPointerv glGetVertexAttribPointerv;
		public static IsProgram glIsProgram;
		public static IsShader glIsShader;
		public static LinkProgram glLinkProgram;
		public static ShaderSource glShaderSource;
		public static UseProgram glUseProgram;
		public static Uniform1f glUniform1f;
		public static Uniform2f glUniform2f;
		public static Uniform3f glUniform3f;
		public static Uniform4f glUniform4f;
		public static Uniform1i glUniform1i;
		public static Uniform2i glUniform2i;
		public static Uniform3i glUniform3i;
		public static Uniform4i glUniform4i;
		public static Uniform1fv glUniform1fv;
		public static Uniform2fv glUniform2fv;
		public static Uniform3fv glUniform3fv;
		public static Uniform4fv glUniform4fv;
		public static Uniform1iv glUniform1iv;
		public static Uniform2iv glUniform2iv;
		public static Uniform3iv glUniform3iv;
		public static Uniform4iv glUniform4iv;
		public static UniformMatrix2fv glUniformMatrix2fv;
		public static UniformMatrix3fv glUniformMatrix3fv;
		public static UniformMatrix4fv glUniformMatrix4fv;
		public static ValidateProgram glValidateProgram;
		public static VertexAttrib1d glVertexAttrib1d;
		public static VertexAttrib1dv glVertexAttrib1dv;
		public static VertexAttrib1f glVertexAttrib1f;
		public static VertexAttrib1fv glVertexAttrib1fv;
		public static VertexAttrib1s glVertexAttrib1s;
		public static VertexAttrib1sv glVertexAttrib1sv;
		public static VertexAttrib2d glVertexAttrib2d;
		public static VertexAttrib2dv glVertexAttrib2dv;
		public static VertexAttrib2f glVertexAttrib2f;
		public static VertexAttrib2fv glVertexAttrib2fv;
		public static VertexAttrib2s glVertexAttrib2s;
		public static VertexAttrib2sv glVertexAttrib2sv;
		public static VertexAttrib3d glVertexAttrib3d;
		public static VertexAttrib3dv glVertexAttrib3dv;
		public static VertexAttrib3f glVertexAttrib3f;
		public static VertexAttrib3fv glVertexAttrib3fv;
		public static VertexAttrib3s glVertexAttrib3s;
		public static VertexAttrib3sv glVertexAttrib3sv;
		public static VertexAttrib4Nbv glVertexAttrib4Nbv;
		public static VertexAttrib4Niv glVertexAttrib4Niv;
		public static VertexAttrib4Nsv glVertexAttrib4Nsv;
		public static VertexAttrib4Nub glVertexAttrib4Nub;
		public static VertexAttrib4Nubv glVertexAttrib4Nubv;
		public static VertexAttrib4Nuiv glVertexAttrib4Nuiv;
		public static VertexAttrib4Nusv glVertexAttrib4Nusv;
		public static VertexAttrib4bv glVertexAttrib4bv;
		public static VertexAttrib4d glVertexAttrib4d;
		public static VertexAttrib4dv glVertexAttrib4dv;
		public static VertexAttrib4f glVertexAttrib4f;
		public static VertexAttrib4fv glVertexAttrib4fv;
		public static VertexAttrib4iv glVertexAttrib4iv;
		public static VertexAttrib4s glVertexAttrib4s;
		public static VertexAttrib4sv glVertexAttrib4sv;
		public static VertexAttrib4ubv glVertexAttrib4ubv;
		public static VertexAttrib4uiv glVertexAttrib4uiv;
		public static VertexAttrib4usv glVertexAttrib4usv;
		public static VertexAttribPointer glVertexAttribPointer;
		public static void InitGl2(OpenGLContext ctx)
		{
			glBlendEquationSeparate = ctx.GetProc<BlendEquationSeparate>("glBlendEquationSeparate");
			glDrawBuffers = ctx.GetProc<DrawBuffers>("glDrawBuffers");
			glStencilOpSeparate = ctx.GetProc<StencilOpSeparate>("glStencilOpSeparate");
			glStencilFuncSeparate = ctx.GetProc<StencilFuncSeparate>("glStencilFuncSeparate");
			glStencilMaskSeparate = ctx.GetProc<StencilMaskSeparate>("glStencilMaskSeparate");
			glAttachShader = ctx.GetProc<AttachShader>("glAttachShader");
			glBindAttribLocation = ctx.GetProc<BindAttribLocation>("glBindAttribLocation");
			glCompileShader = ctx.GetProc<CompileShader>("glCompileShader");
			glCreateProgram = ctx.GetProc<CreateProgram>("glCreateProgram");
			glCreateShader = ctx.GetProc<CreateShader>("glCreateShader");
			glDeleteProgram = ctx.GetProc<DeleteProgram>("glDeleteProgram");
			glDeleteShader = ctx.GetProc<DeleteShader>("glDeleteShader");
			glDetachShader = ctx.GetProc<DetachShader>("glDetachShader");
			glDisableVertexAttribArray = ctx.GetProc<DisableVertexAttribArray>("glDisableVertexAttribArray");
			glEnableVertexAttribArray = ctx.GetProc<EnableVertexAttribArray>("glEnableVertexAttribArray");
			glGetActiveAttrib = ctx.GetProc<GetActiveAttrib>("glGetActiveAttrib");
			glGetActiveUniform = ctx.GetProc<GetActiveUniform>("glGetActiveUniform");
			glGetAttachedShaders = ctx.GetProc<GetAttachedShaders>("glGetAttachedShaders");
			glGetAttribLocation = ctx.GetProc<GetAttribLocation>("glGetAttribLocation");
			glGetProgramiv = ctx.GetProc<GetProgramiv>("glGetProgramiv");
			glGetProgramInfoLog = ctx.GetProc<GetProgramInfoLog>("glGetProgramInfoLog");
			glGetShaderiv = ctx.GetProc<GetShaderiv>("glGetShaderiv");
			glGetShaderInfoLog = ctx.GetProc<GetShaderInfoLog>("glGetShaderInfoLog");
			glGetShaderSource = ctx.GetProc<GetShaderSource>("glGetShaderSource");
			glGetUniformLocation = ctx.GetProc<GetUniformLocation>("glGetUniformLocation");
			glGetUniformfv = ctx.GetProc<GetUniformfv>("glGetUniformfv");
			glGetUniformiv = ctx.GetProc<GetUniformiv>("glGetUniformiv");
			glGetVertexAttribdv = ctx.GetProc<GetVertexAttribdv>("glGetVertexAttribdv");
			glGetVertexAttribfv = ctx.GetProc<GetVertexAttribfv>("glGetVertexAttribfv");
			glGetVertexAttribiv = ctx.GetProc<GetVertexAttribiv>("glGetVertexAttribiv");
			glGetVertexAttribPointerv = ctx.GetProc<GetVertexAttribPointerv>("glGetVertexAttribPointerv");
			glIsProgram = ctx.GetProc<IsProgram>("glIsProgram");
			glIsShader = ctx.GetProc<IsShader>("glIsShader");
			glLinkProgram = ctx.GetProc<LinkProgram>("glLinkProgram");
			glShaderSource = ctx.GetProc<ShaderSource>("glShaderSource");
			glUseProgram = ctx.GetProc<UseProgram>("glUseProgram");
			glUniform1f = ctx.GetProc<Uniform1f>("glUniform1f");
			glUniform2f = ctx.GetProc<Uniform2f>("glUniform2f");
			glUniform3f = ctx.GetProc<Uniform3f>("glUniform3f");
			glUniform4f = ctx.GetProc<Uniform4f>("glUniform4f");
			glUniform1i = ctx.GetProc<Uniform1i>("glUniform1i");
			glUniform2i = ctx.GetProc<Uniform2i>("glUniform2i");
			glUniform3i = ctx.GetProc<Uniform3i>("glUniform3i");
			glUniform4i = ctx.GetProc<Uniform4i>("glUniform4i");
			glUniform1fv = ctx.GetProc<Uniform1fv>("glUniform1fv");
			glUniform2fv = ctx.GetProc<Uniform2fv>("glUniform2fv");
			glUniform3fv = ctx.GetProc<Uniform3fv>("glUniform3fv");
			glUniform4fv = ctx.GetProc<Uniform4fv>("glUniform4fv");
			glUniform1iv = ctx.GetProc<Uniform1iv>("glUniform1iv");
			glUniform2iv = ctx.GetProc<Uniform2iv>("glUniform2iv");
			glUniform3iv = ctx.GetProc<Uniform3iv>("glUniform3iv");
			glUniform4iv = ctx.GetProc<Uniform4iv>("glUniform4iv");
			glUniformMatrix2fv = ctx.GetProc<UniformMatrix2fv>("glUniformMatrix2fv");
			glUniformMatrix3fv = ctx.GetProc<UniformMatrix3fv>("glUniformMatrix3fv");
			glUniformMatrix4fv = ctx.GetProc<UniformMatrix4fv>("glUniformMatrix4fv");
			glValidateProgram = ctx.GetProc<ValidateProgram>("glValidateProgram");
			glVertexAttrib1d = ctx.GetProc<VertexAttrib1d>("glVertexAttrib1d");
			glVertexAttrib1dv = ctx.GetProc<VertexAttrib1dv>("glVertexAttrib1dv");
			glVertexAttrib1f = ctx.GetProc<VertexAttrib1f>("glVertexAttrib1f");
			glVertexAttrib1fv = ctx.GetProc<VertexAttrib1fv>("glVertexAttrib1fv");
			glVertexAttrib1s = ctx.GetProc<VertexAttrib1s>("glVertexAttrib1s");
			glVertexAttrib1sv = ctx.GetProc<VertexAttrib1sv>("glVertexAttrib1sv");
			glVertexAttrib2d = ctx.GetProc<VertexAttrib2d>("glVertexAttrib2d");
			glVertexAttrib2dv = ctx.GetProc<VertexAttrib2dv>("glVertexAttrib2dv");
			glVertexAttrib2f = ctx.GetProc<VertexAttrib2f>("glVertexAttrib2f");
			glVertexAttrib2fv = ctx.GetProc<VertexAttrib2fv>("glVertexAttrib2fv");
			glVertexAttrib2s = ctx.GetProc<VertexAttrib2s>("glVertexAttrib2s");
			glVertexAttrib2sv = ctx.GetProc<VertexAttrib2sv>("glVertexAttrib2sv");
			glVertexAttrib3d = ctx.GetProc<VertexAttrib3d>("glVertexAttrib3d");
			glVertexAttrib3dv = ctx.GetProc<VertexAttrib3dv>("glVertexAttrib3dv");
			glVertexAttrib3f = ctx.GetProc<VertexAttrib3f>("glVertexAttrib3f");
			glVertexAttrib3fv = ctx.GetProc<VertexAttrib3fv>("glVertexAttrib3fv");
			glVertexAttrib3s = ctx.GetProc<VertexAttrib3s>("glVertexAttrib3s");
			glVertexAttrib3sv = ctx.GetProc<VertexAttrib3sv>("glVertexAttrib3sv");
			glVertexAttrib4Nbv = ctx.GetProc<VertexAttrib4Nbv>("glVertexAttrib4Nbv");
			glVertexAttrib4Niv = ctx.GetProc<VertexAttrib4Niv>("glVertexAttrib4Niv");
			glVertexAttrib4Nsv = ctx.GetProc<VertexAttrib4Nsv>("glVertexAttrib4Nsv");
			glVertexAttrib4Nub = ctx.GetProc<VertexAttrib4Nub>("glVertexAttrib4Nub");
			glVertexAttrib4Nubv = ctx.GetProc<VertexAttrib4Nubv>("glVertexAttrib4Nubv");
			glVertexAttrib4Nuiv = ctx.GetProc<VertexAttrib4Nuiv>("glVertexAttrib4Nuiv");
			glVertexAttrib4Nusv = ctx.GetProc<VertexAttrib4Nusv>("glVertexAttrib4Nusv");
			glVertexAttrib4bv = ctx.GetProc<VertexAttrib4bv>("glVertexAttrib4bv");
			glVertexAttrib4d = ctx.GetProc<VertexAttrib4d>("glVertexAttrib4d");
			glVertexAttrib4dv = ctx.GetProc<VertexAttrib4dv>("glVertexAttrib4dv");
			glVertexAttrib4f = ctx.GetProc<VertexAttrib4f>("glVertexAttrib4f");
			glVertexAttrib4fv = ctx.GetProc<VertexAttrib4fv>("glVertexAttrib4fv");
			glVertexAttrib4iv = ctx.GetProc<VertexAttrib4iv>("glVertexAttrib4iv");
			glVertexAttrib4s = ctx.GetProc<VertexAttrib4s>("glVertexAttrib4s");
			glVertexAttrib4sv = ctx.GetProc<VertexAttrib4sv>("glVertexAttrib4sv");
			glVertexAttrib4ubv = ctx.GetProc<VertexAttrib4ubv>("glVertexAttrib4ubv");
			glVertexAttrib4uiv = ctx.GetProc<VertexAttrib4uiv>("glVertexAttrib4uiv");
			glVertexAttrib4usv = ctx.GetProc<VertexAttrib4usv>("glVertexAttrib4usv");
			glVertexAttribPointer = ctx.GetProc<VertexAttribPointer>("glVertexAttribPointer");
		}
		#endregion
		#region OpenGL 3.2
		public delegate void GetInteger64i_v(uint target, uint index, long[] data);
		public delegate void GetBufferParameteri64v(uint target, uint pname, long[] parameters);
		public delegate void ProgramParameteri(uint program, uint pname, int value);
		public delegate void FramebufferTexture(uint target, uint attachment, uint texture, int level);

		public static GetInteger64i_v glGetInteger64i_v;
		public static GetBufferParameteri64v glGetBufferParameteri64v;
		public static ProgramParameteri glProgramParameteri;
		public static FramebufferTexture glFramebufferTexture;
		public static void InitGL32(OpenGLContext ctx)
		{
			glGetInteger64i_v = ctx.GetProc<GetInteger64i_v>("glGetInteger64i_v");
			glGetBufferParameteri64v = ctx.GetProc<GetBufferParameteri64v>("glGetBufferParameteri64v");
			glProgramParameteri = ctx.GetProc<ProgramParameteri>("glProgramParameteri");
			glFramebufferTexture = ctx.GetProc<FramebufferTexture>("glFramebufferTexture");
		}
		#endregion
	}
}
