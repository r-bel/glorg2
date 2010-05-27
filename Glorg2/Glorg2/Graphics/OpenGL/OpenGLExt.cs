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
using System.Runtime.InteropServices;

using GLintptr = System.IntPtr;
using GLenum = System.UInt32;
using GLchar = System.Byte;
using GLbyte = System.SByte;
using GLubyte = System.Byte;
using GLshort = System.Int16;
using GLushort = System.UInt16;
using GLint = System.Int32;
using GLuint = System.UInt32;
using GLlong = System.Int64;
using GLulong = System.UInt64;
using GLboolean = Glorg2.Graphics.OpenGL.OpenGL.boolean;
using GLsizei = System.Int32;
using GLclampf = System.Single;
using GLclampd = System.Double;
using GLfloat = System.Single;
using GLdouble = System.Double;
using GLhalf = Glorg2.Half;

/*
public static {[a-zA-Z0-9]+} {[a-zA-Z0-9]+};			\2 = ctx.GetProc<\1>("\2");
public delegate [a-zA-Z0-9]+ {[a-zA-Z0-9]+}\(.*\);		public static \1 gl\1;
GLAPI {[a-zA-Z0-9]+} APIENTRY gl{[a-zA-Z0-9]+} \({.*}\);	public delegate \1 \2(\3);

\#define:b{[a-zA-Z_0-9]+}:b{.*}$	public const uint \1 = \2;
*/

namespace Glorg2.Graphics.OpenGL
{
	public enum VboTarget : uint
	{
		GL_ARRAY_BUFFER = 0x8892,
		GL_ELEMENT_ARRAY_BUFFER = 0x8893
	}
	public enum VboAccess
	{
		GL_READ_ONLY = 0x88B8,
		GL_WRITE_ONLY = 0x88B9,
		GL_READ_WRITE = 0x88BA,
	}
	public enum VboUsage : uint
	{
		GL_STREAM_DRAW = 0x88E0,
		GL_STREAM_READ = 0x88E1,
		GL_STREAM_COPY = 0x88E2,
		GL_STATIC_DRAW = 0x88E4,
		GL_STATIC_READ = 0x88E5,
		GL_STATIC_COPY = 0x88E6,
		GL_DYNAMIC_DRAW = 0x88E8,
		GL_DYNAMIC_READ = 0x88E9,
		GL_DYNAMIC_COPY = 0x88EA,
	}

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
					IntPtr ptr = glGetStringi(Const.GL_EXTENSIONS, (GLuint)i);
					string s = Marshal.PtrToStringAnsi(ptr);
					extensions.Add(s);
				}

				Extensions = extensions.AsReadOnly();
			}
			return Extensions;

		}

		
		
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

		#region OpenGL 1.2

		public delegate void BlendColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);
public delegate void BlendEquation(GLenum mode);
public delegate void DrawRangeElements(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, IntPtr indices);
public delegate void TexImage3D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, IntPtr pixels);
public delegate void TexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, IntPtr pixels);
public delegate void CopyTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

		public static BlendColor glBlendColor;
public static BlendEquation glBlendEquation;
public static DrawRangeElements glDrawRangeElements;
public static TexImage3D glTexImage3D;
public static TexSubImage3D glTexSubImage3D;
public static CopyTexSubImage3D glCopyTexSubImage3D;


		public static void InitGL_1_2(OpenGLContext ctx)
		{
					glBlendColor = ctx.GetProc<BlendColor>("glBlendColor");
glBlendEquation = ctx.GetProc<BlendEquation>("glBlendEquation");
glDrawRangeElements = ctx.GetProc<DrawRangeElements>("glDrawRangeElements");
glTexImage3D = ctx.GetProc<TexImage3D>("glTexImage3D");
glTexSubImage3D = ctx.GetProc<TexSubImage3D>("glTexSubImage3D");
glCopyTexSubImage3D = ctx.GetProc<CopyTexSubImage3D>("glCopyTexSubImage3D");

		}
		#endregion

		#region OpenGL 1.3
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

		public static void InitGL_1_3(OpenGLContext ctx)
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

		#region OpenGL 1.4
		public delegate void BlendFuncSeparate(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);
public delegate void MultiDrawArrays(GLenum mode, GLint[] first, GLsizei[] count, GLsizei primcount);
public delegate void MultiDrawElements(GLenum mode, GLsizei[] count, GLenum type, IntPtr[] indices, GLsizei primcount);
public delegate void PointParameterf(GLenum pname, GLfloat param);
public delegate void PointParameterfv(GLenum pname, GLfloat[] @params);
public delegate void PointParameteri(GLenum pname, GLint param);
public delegate void PointParameteriv(GLenum pname, GLint[] @params);
		public static BlendFuncSeparate glBlendFuncSeparate;
public static MultiDrawArrays glMultiDrawArrays;
public static MultiDrawElements glMultiDrawElements;
public static PointParameterf glPointParameterf;
public static PointParameterfv glPointParameterfv;
public static PointParameteri glPointParameteri;
public static PointParameteriv glPointParameteriv;

		public static void InitGL_1_4(OpenGLContext ctx)
		{
					glBlendFuncSeparate = ctx.GetProc<BlendFuncSeparate>("glBlendFuncSeparate");
glMultiDrawArrays = ctx.GetProc<MultiDrawArrays>("glMultiDrawArrays");
glMultiDrawElements = ctx.GetProc<MultiDrawElements>("glMultiDrawElements");
glPointParameterf = ctx.GetProc<PointParameterf>("glPointParameterf");
glPointParameterfv = ctx.GetProc<PointParameterfv>("glPointParameterfv");
glPointParameteri = ctx.GetProc<PointParameteri>("glPointParameteri");
glPointParameteriv = ctx.GetProc<PointParameteriv>("glPointParameteriv");
		}
		#endregion

		#region OpenGL 1.5
		public delegate void DeleteQueries(int n, uint[] ids);
		public delegate boolean IsQuery(uint id);
		public delegate void BeginQuery(uint target, uint id);
		public delegate void EndQuery(uint target);
		public delegate void GetQueryiv(uint target, uint pname, int[] @params);
		public delegate void GetQueryObjectiv(uint id, uint pname, int[] @params);
		public delegate void GetQueryObjectuiv(uint id, uint pname, uint[] @params);
		public delegate void BindBuffer(uint target, uint buffer);
		public delegate void DeleteBuffers(int n, uint[] buffers);
		public delegate void GenBuffers(int n, uint[] buffers);
		public delegate boolean IsBuffer(uint buffer);
		public delegate void BufferData(uint target, IntPtr size, IntPtr data, uint usage);
		public delegate void BufferSubData(uint target, IntPtr offset, IntPtr size, IntPtr data);
		public delegate void GetBufferSubData(uint target, IntPtr offset, IntPtr size, IntPtr data);
		public delegate IntPtr MapBuffer(uint target, uint access);
		public delegate boolean UnmapBuffer(uint target);
		public delegate void GetBufferParameteriv(uint target, uint pname, int[] @params);
		public delegate void GetBufferPointerv(uint target, uint pname, ref IntPtr @params);

		public static DeleteQueries glDeleteQueries;
		public static IsQuery glIsQuery;
		public static BeginQuery glBeginQuery;
		public static EndQuery glEndQuery;
		public static GetQueryiv glGetQueryiv;
		public static GetQueryObjectiv glGetQueryObjectiv;
		public static GetQueryObjectuiv glGetQueryObjectuiv;
		public static BindBuffer glBindBuffer;
		public static DeleteBuffers glDeleteBuffers;
		public static GenBuffers glGenBuffers;
		public static IsBuffer glIsBuffer;
		public static BufferData glBufferData;
		public static BufferSubData glBufferSubData;
		public static GetBufferSubData glGetBufferSubData;
		public static MapBuffer glMapBuffer;
		public static UnmapBuffer glUnmapBuffer;
		public static GetBufferParameteriv glGetBufferParameteriv;
		public static GetBufferPointerv glGetBufferPointerv;
		public static void InitGL_1_5(OpenGLContext ctx)
		{
			glDeleteQueries = ctx.GetProc<DeleteQueries>("glDeleteQueries");
			glIsQuery = ctx.GetProc<IsQuery>("glIsQuery");
			glBeginQuery = ctx.GetProc<BeginQuery>("glBeginQuery");
			glEndQuery = ctx.GetProc<EndQuery>("glEndQuery");
			glGetQueryiv = ctx.GetProc<GetQueryiv>("glGetQueryiv");
			glGetQueryObjectiv = ctx.GetProc<GetQueryObjectiv>("glGetQueryObjectiv");
			glGetQueryObjectuiv = ctx.GetProc<GetQueryObjectuiv>("glGetQueryObjectuiv");
			glBindBuffer = ctx.GetProc<BindBuffer>("glBindBuffer");
			glDeleteBuffers = ctx.GetProc<DeleteBuffers>("glDeleteBuffers");
			glGenBuffers = ctx.GetProc<GenBuffers>("glGenBuffers");
			glIsBuffer = ctx.GetProc<IsBuffer>("glIsBuffer");
			glBufferData = ctx.GetProc<BufferData>("glBufferData");
			glBufferSubData = ctx.GetProc<BufferSubData>("glBufferSubData");
			glGetBufferSubData = ctx.GetProc<GetBufferSubData>("glGetBufferSubData");
			glMapBuffer = ctx.GetProc<MapBuffer>("glMapBuffer");
			glUnmapBuffer = ctx.GetProc<UnmapBuffer>("glUnmapBuffer");
			glGetBufferParameteriv = ctx.GetProc<GetBufferParameteriv>("glGetBufferParameteriv");
			glGetBufferPointerv = ctx.GetProc<GetBufferPointerv>("glGetBufferPointerv");
		}

		#endregion

		#region OpenGL 2.0
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
		public static void InitGL_2_0(OpenGLContext ctx)
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

		#region OpenGL 2.1

		public delegate void UniformMatrix2x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
public delegate void UniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
public delegate void UniformMatrix2x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
public delegate void UniformMatrix4x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
public delegate void UniformMatrix3x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
public delegate void UniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat[] value);
		public static UniformMatrix2x3fv glUniformMatrix2x3fv;
public static UniformMatrix3x2fv glUniformMatrix3x2fv;
public static UniformMatrix2x4fv glUniformMatrix2x4fv;
public static UniformMatrix4x2fv glUniformMatrix4x2fv;
public static UniformMatrix3x4fv glUniformMatrix3x4fv;
public static UniformMatrix4x3fv glUniformMatrix4x3fv;

		public static void InitGL_2_1(OpenGLContext ctx)
		{
					glUniformMatrix2x3fv = ctx.GetProc<UniformMatrix2x3fv>("glUniformMatrix2x3fv");
glUniformMatrix3x2fv = ctx.GetProc<UniformMatrix3x2fv>("glUniformMatrix3x2fv");
glUniformMatrix2x4fv = ctx.GetProc<UniformMatrix2x4fv>("glUniformMatrix2x4fv");
glUniformMatrix4x2fv = ctx.GetProc<UniformMatrix4x2fv>("glUniformMatrix4x2fv");
glUniformMatrix3x4fv = ctx.GetProc<UniformMatrix3x4fv>("glUniformMatrix3x4fv");
glUniformMatrix4x3fv = ctx.GetProc<UniformMatrix4x3fv>("glUniformMatrix4x3fv");
		}
		#endregion

		#region OpenGL 3.0

		public delegate void ColorMaski(GLuint index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);
		public delegate void GetBooleani_v(GLenum target, GLuint index, GLboolean[] data);
		public delegate void GetIntegeri_v(GLenum target, GLuint index, GLint[] data);
		public delegate void Enablei(GLenum target, GLuint index);
		public delegate void Disablei(GLenum target, GLuint index);
		public delegate GLboolean IsEnabledi(GLenum target, GLuint index);
		public delegate void BeginTransformFeedback(GLenum primitiveMode);
		public delegate void EndTransformFeedback();
		public delegate void BindBufferRange(GLenum target, GLuint index, GLuint buffer, GLintptr offset, IntPtr size);
		public delegate void BindBufferBase(GLenum target, GLuint index, GLuint buffer);
		public delegate void TransformFeedbackVaryings(GLuint program, GLsizei count, string[] varyings, GLenum bufferMode);
		public delegate void GetTransformFeedbackVarying(GLuint program, GLuint index, GLsizei bufSize, ref GLsizei length, ref GLsizei size, ref GLenum type, string name);
		public delegate void ClampColor(GLenum target, GLenum clamp);
		public delegate void BeginConditionalRender(GLuint id, GLenum mode);
		public delegate void EndConditionalRender();
		public delegate void VertexAttribIPointer(GLuint index, GLint size, GLenum type, GLsizei stride, IntPtr pointer);
		public delegate void GetVertexAttribIiv(GLuint index, GLenum pname, GLint[] @params);
		public delegate void GetVertexAttribIuiv(GLuint index, GLenum pname, GLuint[] @params);
		public delegate void VertexAttribI1i(GLuint index, GLint x);
		public delegate void VertexAttribI2i(GLuint index, GLint x, GLint y);
		public delegate void VertexAttribI3i(GLuint index, GLint x, GLint y, GLint z);
		public delegate void VertexAttribI4i(GLuint index, GLint x, GLint y, GLint z, GLint w);
		public delegate void VertexAttribI1ui(GLuint index, GLuint x);
		public delegate void VertexAttribI2ui(GLuint index, GLuint x, GLuint y);
		public delegate void VertexAttribI3ui(GLuint index, GLuint x, GLuint y, GLuint z);
		public delegate void VertexAttribI4ui(GLuint index, GLuint x, GLuint y, GLuint z, GLuint w);
		public delegate void VertexAttribI1iv(GLuint index, GLint[] v);
		public delegate void VertexAttribI2iv(GLuint index, GLint[] v);
		public delegate void VertexAttribI3iv(GLuint index, GLint[] v);
		public delegate void VertexAttribI4iv(GLuint index, GLint[] v);
		public delegate void VertexAttribI1uiv(GLuint index, GLuint[] v);
		public delegate void VertexAttribI2uiv(GLuint index, GLuint[] v);
		public delegate void VertexAttribI3uiv(GLuint index, GLuint[] v);
		public delegate void VertexAttribI4uiv(GLuint index, GLuint[] v);
		public delegate void VertexAttribI4bv(GLuint index, GLbyte[] v);
		public delegate void VertexAttribI4sv(GLuint index, GLshort[] v);
		public delegate void VertexAttribI4ubv(GLuint index, GLubyte[] v);
		public delegate void VertexAttribI4usv(GLuint index, GLushort[] v);
		public delegate void GetUniformuiv(GLuint program, GLint location, GLuint[] @params);
		public delegate void BindFragDataLocation(GLuint program, GLuint color, string name);
		public delegate GLint GetFragDataLocation(GLuint program, out string name);
		public delegate void Uniform1ui(GLint location, GLuint v0);
		public delegate void Uniform2ui(GLint location, GLuint v0, GLuint v1);
		public delegate void Uniform3ui(GLint location, GLuint v0, GLuint v1, GLuint v2);
		public delegate void Uniform4ui(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);
		public delegate void Uniform1uiv(GLint location, GLsizei count, GLuint[] value);
		public delegate void Uniform2uiv(GLint location, GLsizei count, GLuint[] value);
		public delegate void Uniform3uiv(GLint location, GLsizei count, GLuint[] value);
		public delegate void Uniform4uiv(GLint location, GLsizei count, GLuint[] value);
		public delegate void TexParameterIiv(GLenum target, GLenum pname, GLint[] @params);
		public delegate void TexParameterIuiv(GLenum target, GLenum pname, GLuint[] @params);
		public delegate void GetTexParameterIiv(GLenum target, GLenum pname, GLint[] @params);
		public delegate void GetTexParameterIuiv(GLenum target, GLenum pname, GLuint[] @params);
		public delegate void ClearBufferiv(GLenum buffer, GLint drawbuffer, GLint[] value);
		public delegate void ClearBufferuiv(GLenum buffer, GLint drawbuffer, GLuint[] value);
		public delegate void ClearBufferfv(GLenum buffer, GLint drawbuffer, GLfloat[] value);
		public delegate void ClearBufferfi(GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);
		public delegate IntPtr GetStringi (GLenum name, GLuint index);
		public static ColorMaski glColorMaski;
		public static GetBooleani_v glGetBooleani_v;
		public static GetIntegeri_v glGetIntegeri_v;
		public static Enablei glEnablei;
		public static Disablei glDisablei;
		public static IsEnabledi glIsEnabledi;
		public static BeginTransformFeedback glBeginTransformFeedback;
		public static EndTransformFeedback glEndTransformFeedback;
		public static BindBufferRange glBindBufferRange;
		public static BindBufferBase glBindBufferBase;
		public static TransformFeedbackVaryings glTransformFeedbackVaryings;
		public static GetTransformFeedbackVarying glGetTransformFeedbackVarying;
		public static ClampColor glClampColor;
		public static BeginConditionalRender glBeginConditionalRender;
		public static EndConditionalRender glEndConditionalRender;
		public static VertexAttribIPointer glVertexAttribIPointer;
		public static GetVertexAttribIiv glGetVertexAttribIiv;
		public static GetVertexAttribIuiv glGetVertexAttribIuiv;
		public static VertexAttribI1i glVertexAttribI1i;
		public static VertexAttribI2i glVertexAttribI2i;
		public static VertexAttribI3i glVertexAttribI3i;
		public static VertexAttribI4i glVertexAttribI4i;
		public static VertexAttribI1ui glVertexAttribI1ui;
		public static VertexAttribI2ui glVertexAttribI2ui;
		public static VertexAttribI3ui glVertexAttribI3ui;
		public static VertexAttribI4ui glVertexAttribI4ui;
		public static VertexAttribI1iv glVertexAttribI1iv;
		public static VertexAttribI2iv glVertexAttribI2iv;
		public static VertexAttribI3iv glVertexAttribI3iv;
		public static VertexAttribI4iv glVertexAttribI4iv;
		public static VertexAttribI1uiv glVertexAttribI1uiv;
		public static VertexAttribI2uiv glVertexAttribI2uiv;
		public static VertexAttribI3uiv glVertexAttribI3uiv;
		public static VertexAttribI4uiv glVertexAttribI4uiv;
		public static VertexAttribI4bv glVertexAttribI4bv;
		public static VertexAttribI4sv glVertexAttribI4sv;
		public static VertexAttribI4ubv glVertexAttribI4ubv;
		public static VertexAttribI4usv glVertexAttribI4usv;
		public static GetUniformuiv glGetUniformuiv;
		public static BindFragDataLocation glBindFragDataLocation;
		public static GetFragDataLocation glGetFragDataLocation;
		public static Uniform1ui glUniform1ui;
		public static Uniform2ui glUniform2ui;
		public static Uniform3ui glUniform3ui;
		public static Uniform4ui glUniform4ui;
		public static Uniform1uiv glUniform1uiv;
		public static Uniform2uiv glUniform2uiv;
		public static Uniform3uiv glUniform3uiv;
		public static Uniform4uiv glUniform4uiv;
		public static TexParameterIiv glTexParameterIiv;
		public static TexParameterIuiv glTexParameterIuiv;
		public static GetTexParameterIiv glGetTexParameterIiv;
		public static GetTexParameterIuiv glGetTexParameterIuiv;
		public static ClearBufferiv glClearBufferiv;
		public static ClearBufferuiv glClearBufferuiv;
		public static ClearBufferfv glClearBufferfv;
		public static ClearBufferfi glClearBufferfi;
		public static GetStringi glGetStringi;
		public static void InitGL_3_0(OpenGLContext ctx)
		{
			glColorMaski = ctx.GetProc<ColorMaski>("glColorMaski");
			glGetBooleani_v = ctx.GetProc<GetBooleani_v>("glGetBooleani_v");
			glGetIntegeri_v = ctx.GetProc<GetIntegeri_v>("glGetIntegeri_v");
			glEnablei = ctx.GetProc<Enablei>("glEnablei");
			glDisablei = ctx.GetProc<Disablei>("glDisablei");
			glIsEnabledi = ctx.GetProc<IsEnabledi>("glIsEnabledi");
			glBeginTransformFeedback = ctx.GetProc<BeginTransformFeedback>("glBeginTransformFeedback");
			glEndTransformFeedback = ctx.GetProc<EndTransformFeedback>("glEndTransformFeedback");
			glBindBufferRange = ctx.GetProc<BindBufferRange>("glBindBufferRange");
			glBindBufferBase = ctx.GetProc<BindBufferBase>("glBindBufferBase");
			glTransformFeedbackVaryings = ctx.GetProc<TransformFeedbackVaryings>("glTransformFeedbackVaryings");
			glGetTransformFeedbackVarying = ctx.GetProc<GetTransformFeedbackVarying>("glGetTransformFeedbackVarying");
			glClampColor = ctx.GetProc<ClampColor>("glClampColor");
			glBeginConditionalRender = ctx.GetProc<BeginConditionalRender>("glBeginConditionalRender");
			glEndConditionalRender = ctx.GetProc<EndConditionalRender>("glEndConditionalRender");
			glVertexAttribIPointer = ctx.GetProc<VertexAttribIPointer>("glVertexAttribIPointer");
			glGetVertexAttribIiv = ctx.GetProc<GetVertexAttribIiv>("glGetVertexAttribIiv");
			glGetVertexAttribIuiv = ctx.GetProc<GetVertexAttribIuiv>("glGetVertexAttribIuiv");
			glVertexAttribI1i = ctx.GetProc<VertexAttribI1i>("glVertexAttribI1i");
			glVertexAttribI2i = ctx.GetProc<VertexAttribI2i>("glVertexAttribI2i");
			glVertexAttribI3i = ctx.GetProc<VertexAttribI3i>("glVertexAttribI3i");
			glVertexAttribI4i = ctx.GetProc<VertexAttribI4i>("glVertexAttribI4i");
			glVertexAttribI1ui = ctx.GetProc<VertexAttribI1ui>("glVertexAttribI1ui");
			glVertexAttribI2ui = ctx.GetProc<VertexAttribI2ui>("glVertexAttribI2ui");
			glVertexAttribI3ui = ctx.GetProc<VertexAttribI3ui>("glVertexAttribI3ui");
			glVertexAttribI4ui = ctx.GetProc<VertexAttribI4ui>("glVertexAttribI4ui");
			glVertexAttribI1iv = ctx.GetProc<VertexAttribI1iv>("glVertexAttribI1iv");
			glVertexAttribI2iv = ctx.GetProc<VertexAttribI2iv>("glVertexAttribI2iv");
			glVertexAttribI3iv = ctx.GetProc<VertexAttribI3iv>("glVertexAttribI3iv");
			glVertexAttribI4iv = ctx.GetProc<VertexAttribI4iv>("glVertexAttribI4iv");
			glVertexAttribI1uiv = ctx.GetProc<VertexAttribI1uiv>("glVertexAttribI1uiv");
			glVertexAttribI2uiv = ctx.GetProc<VertexAttribI2uiv>("glVertexAttribI2uiv");
			glVertexAttribI3uiv = ctx.GetProc<VertexAttribI3uiv>("glVertexAttribI3uiv");
			glVertexAttribI4uiv = ctx.GetProc<VertexAttribI4uiv>("glVertexAttribI4uiv");
			glVertexAttribI4bv = ctx.GetProc<VertexAttribI4bv>("glVertexAttribI4bv");
			glVertexAttribI4sv = ctx.GetProc<VertexAttribI4sv>("glVertexAttribI4sv");
			glVertexAttribI4ubv = ctx.GetProc<VertexAttribI4ubv>("glVertexAttribI4ubv");
			glVertexAttribI4usv = ctx.GetProc<VertexAttribI4usv>("glVertexAttribI4usv");
			glGetUniformuiv = ctx.GetProc<GetUniformuiv>("glGetUniformuiv");
			glBindFragDataLocation = ctx.GetProc<BindFragDataLocation>("glBindFragDataLocation");
			glGetFragDataLocation = ctx.GetProc<GetFragDataLocation>("glGetFragDataLocation");
			glUniform1ui = ctx.GetProc<Uniform1ui>("glUniform1ui");
			glUniform2ui = ctx.GetProc<Uniform2ui>("glUniform2ui");
			glUniform3ui = ctx.GetProc<Uniform3ui>("glUniform3ui");
			glUniform4ui = ctx.GetProc<Uniform4ui>("glUniform4ui");
			glUniform1uiv = ctx.GetProc<Uniform1uiv>("glUniform1uiv");
			glUniform2uiv = ctx.GetProc<Uniform2uiv>("glUniform2uiv");
			glUniform3uiv = ctx.GetProc<Uniform3uiv>("glUniform3uiv");
			glUniform4uiv = ctx.GetProc<Uniform4uiv>("glUniform4uiv");
			glTexParameterIiv = ctx.GetProc<TexParameterIiv>("glTexParameterIiv");
			glTexParameterIuiv = ctx.GetProc<TexParameterIuiv>("glTexParameterIuiv");
			glGetTexParameterIiv = ctx.GetProc<GetTexParameterIiv>("glGetTexParameterIiv");
			glGetTexParameterIuiv = ctx.GetProc<GetTexParameterIuiv>("glGetTexParameterIuiv");
			glClearBufferiv = ctx.GetProc<ClearBufferiv>("glClearBufferiv");
			glClearBufferuiv = ctx.GetProc<ClearBufferuiv>("glClearBufferuiv");
			glClearBufferfv = ctx.GetProc<ClearBufferfv>("glClearBufferfv");
			glClearBufferfi = ctx.GetProc<ClearBufferfi>("glClearBufferfi");
			glGetStringi = ctx.GetProc<GetStringi>("glGetStringi");
		}
		#endregion

		#region OpenGL 3.1

		public delegate void DrawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei primcount);
public delegate void DrawElementsInstanced(GLenum mode, GLsizei count, GLenum type, IntPtr[] indices, GLsizei primcount);
public delegate void TexBuffer(GLenum target, GLenum internalformat, GLuint buffer);
public delegate void PrimitiveRestartIndex(GLuint index);

		public static DrawArraysInstanced glDrawArraysInstanced;
public static DrawElementsInstanced glDrawElementsInstanced;
public static TexBuffer glTexBuffer;
public static PrimitiveRestartIndex glPrimitiveRestartIndex;

		public static void InitGL_3_1(OpenGLContext ctx)
		{
					glDrawArraysInstanced = ctx.GetProc<DrawArraysInstanced>("glDrawArraysInstanced");
glDrawElementsInstanced = ctx.GetProc<DrawElementsInstanced>("glDrawElementsInstanced");
glTexBuffer = ctx.GetProc<TexBuffer>("glTexBuffer");
glPrimitiveRestartIndex = ctx.GetProc<PrimitiveRestartIndex>("glPrimitiveRestartIndex");
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
		public static void InitGL_3_2(OpenGLContext ctx)
		{
			glGetInteger64i_v = ctx.GetProc<GetInteger64i_v>("glGetInteger64i_v");
			glGetBufferParameteri64v = ctx.GetProc<GetBufferParameteri64v>("glGetBufferParameteri64v");
			glProgramParameteri = ctx.GetProc<ProgramParameteri>("glProgramParameteri");
			glFramebufferTexture = ctx.GetProc<FramebufferTexture>("glFramebufferTexture");
		}
		#endregion
	}
}
