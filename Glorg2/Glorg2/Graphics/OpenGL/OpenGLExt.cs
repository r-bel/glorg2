using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Glorg2.Graphics.OpenGL
{
	public static partial class OpenGL
	{


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
		public unsafe delegate void BufferDataARB(VboTarget target, int size, void* data, VboUsage usage);
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
			glGenBuffersARB = ctx.GetProc<GenBuffersARB>("glGenBuffersARB");
			glBindBufferARB = ctx.GetProc<BindBufferARB>("glBindBufferARB");
			glBufferDataARB = ctx.GetProc<BufferDataARB>("glBufferDataARB");
			glBufferSubDataARB = ctx.GetProc<BufferSubDataARB>("glBufferSubDataARB");
			glDeleteBuffersARB = ctx.GetProc<DeleteBuffersARB>("glDeleteBuffersARB");
			glMapBufferARB = ctx.GetProc<MapBufferARB>("glMapBufferARB");
			glUnmapBufferARB = ctx.GetProc<UnmapBufferARB>("glUnmapBufferARB");
		}

		#endregion

		#region Shader Program

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

		public static void InitShaderProgram(OpenGLContext ctx)
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
		}

		#endregion
	}
}
