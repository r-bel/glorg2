using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using GLintptr = System.IntPtr;
using GLbitfield = System.UInt32;
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

namespace Glorg2.Graphics.OpenGL
{
	public static partial class OpenGL
	{


		public const string DllName = "OpenGL32.dll";
		public const CallingConvention CallConv = CallingConvention.Winapi;

		public enum boolean : byte
		{
			TRUE = 1,
			FALSE = 0
		}

		/*************************************************************/



		/* EXT_paletted_texture */



		/*************************************************************/
		#region OpenGL 1.0
		[DllImport(DllName)]
		public static extern void glCullFace(GLenum mode);
		[DllImport(DllName)]
		public static extern void glFrontFace(GLenum mode);
		[DllImport(DllName)]
		public static extern void glHint(GLenum target, GLenum mode);
		[DllImport(DllName)]
		public static extern void glLineWidth(GLfloat width);
		[DllImport(DllName)]
		public static extern void glPointSize(GLfloat size);
		[DllImport(DllName)]
		public static extern void glPolygonMode(GLenum face, GLenum mode);
		[DllImport(DllName)]
		public static extern void glScissor(GLint x, GLint y, GLsizei width, GLsizei height);
		[DllImport(DllName)]
		public static extern void glTexParameterf(GLenum target, GLenum pname, GLfloat param);
		[DllImport(DllName)]
		public static extern void glTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);
		[DllImport(DllName)]
		public static extern void glTexParameteri(GLenum target, GLenum pname, GLint param);
		[DllImport(DllName)]
		public static extern void glTexParameteriv(GLenum target, GLenum pname, GLint[] @params);
		[DllImport(DllName)]
		public static extern void glTexImage1D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLint border, GLenum format, GLenum type, IntPtr pixels);
		[DllImport(DllName)]
		public static extern void glTexImage2D(GLenum target, GLint level, GLint internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, IntPtr pixels);
		[DllImport(DllName)]
		public static extern void glDrawBuffer(GLenum mode);
		[DllImport(DllName)]
		public static extern void glClear(GLbitfield mask);
		[DllImport(DllName)]
		public static extern void glClearColor(GLclampf red, GLclampf green, GLclampf blue, GLclampf alpha);
		[DllImport(DllName)]
		public static extern void glClearStencil(GLint s);
		[DllImport(DllName)]
		public static extern void glClearDepth(GLclampd depth);
		[DllImport(DllName)]
		public static extern void glStencilMask(GLuint mask);
		[DllImport(DllName)]
		public static extern void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);
		[DllImport(DllName)]
		public static extern void glDepthMask(GLboolean flag);
		[DllImport(DllName)]
		public static extern void glDisable(GLenum cap);
		[DllImport(DllName)]
		public static extern void glEnable(GLenum cap);
		[DllImport(DllName)]
		public static extern void glFinish();
		[DllImport(DllName)]
		public static extern void glFlush();
		[DllImport(DllName)]
		public static extern void glBlendFunc(GLenum sfactor, GLenum dfactor);
		[DllImport(DllName)]
		public static extern void glLogicOp(GLenum opcode);
		[DllImport(DllName)]
		public static extern void glStencilFunc(GLenum func, GLint @ref, GLuint mask);
		[DllImport(DllName)]
		public static extern void glStencilOp(GLenum fail, GLenum zfail, GLenum zpass);
		[DllImport(DllName)]
		public static extern void glDepthFunc(GLenum func);
		[DllImport(DllName)]
		public static extern void glPixelStoref(GLenum pname, GLfloat param);
		[DllImport(DllName)]
		public static extern void glPixelStorei(GLenum pname, GLint param);
		[DllImport(DllName)]
		public static extern void glReadBuffer(GLenum mode);
		[DllImport(DllName)]
		public static extern void glReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
		[DllImport(DllName)]
		public static extern void glGetBooleanv(GLenum pname, GLboolean[] @params);
		[DllImport(DllName)]
		public static extern void glGetDoublev(GLenum pname, GLdouble[] @params);
		[DllImport(DllName)]
		public static extern GLenum glGetError();
		[DllImport(DllName)]
		public static extern void glGetFloatv(GLenum pname, GLfloat[] @params);
		[DllImport(DllName)]
		public static extern void glGetIntegerv(GLenum pname, GLint[] @params);
		[DllImport(DllName)]
		public static extern IntPtr glGetString(GLenum name);
		[DllImport(DllName)]
		public static extern void glGetTexImage(GLenum target, GLint level, GLenum format, GLenum type, IntPtr pixels);
		[DllImport(DllName)]
		public static extern void glGetTexParameterfv(GLenum target, GLenum pname, GLfloat[] @params);
		[DllImport(DllName)]
		public static extern void glGetTexParameteriv(GLenum target, GLenum pname, GLint[] @params);
		[DllImport(DllName)]
		public static extern void glGetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, GLfloat[] @params);
		[DllImport(DllName)]
		public static extern void glGetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, GLint[] @params);
		[DllImport(DllName)]
		public static extern GLboolean glIsEnabled(GLenum cap);
		[DllImport(DllName)]
		public static extern void glDepthRange(GLclampd near, GLclampd far);
		[DllImport(DllName)]
		public static extern void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

		public static string GetString(int name)
		{
			return Marshal.PtrToStringAnsi(glGetString((uint)name));
		}

		#endregion


		#region OpenGL 1.1
		[DllImport(DllName)]
		public static extern void glDrawArrays(GLenum mode, GLint first, GLsizei count);
		[DllImport(DllName)]
		public static extern void glDrawElements(GLenum mode, GLsizei count, GLenum type, IntPtr indices);
		[DllImport(DllName)]
		public static extern void glGetPointerv(GLenum pname, IntPtr[] @params);
		[DllImport(DllName)]
		public static extern void glPolygonOffset(GLfloat factor, GLfloat units);
		[DllImport(DllName)]
		public static extern void glCopyTexImage1D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLint border);
		[DllImport(DllName)]
		public static extern void glCopyTexImage2D(GLenum target, GLint level, GLenum internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);
		[DllImport(DllName)]
		public static extern void glCopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);
		[DllImport(DllName)]
		public static extern void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);
		[DllImport(DllName)]
		public static extern void glTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, IntPtr pixels);
		[DllImport(DllName)]
		public static extern void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, IntPtr pixels);
		[DllImport(DllName)]
		public static extern void glBindTexture(GLenum target, GLuint texture);
		[DllImport(DllName)]
		public static extern void glDeleteTextures(GLsizei n, GLuint[] textures);
		[DllImport(DllName)]
		public static extern void glGenTextures(GLsizei n, GLuint[] textures);
		[DllImport(DllName)]
		public static extern GLboolean glIsTexture(GLuint texture);

		#endregion
	}
}
