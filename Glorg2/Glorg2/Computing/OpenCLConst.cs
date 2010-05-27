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



namespace Glorg2.Computing
{
	public static partial class OpenCL
	{
		public static class Const
		{
			public const uint CL_SUCCESS = 0;
			public const uint CL_DEVICE_NOT_FOUND = unchecked ((uint)-1);
			public const uint CL_DEVICE_NOT_AVAILABLE = unchecked((uint)-2);
			public const uint CL_COMPILER_NOT_AVAILABLE = unchecked((uint)-3);
			public const uint CL_MEM_OBJECT_ALLOCATION_FAILURE = unchecked((uint)-4);
			public const uint CL_OUT_OF_RESOURCES = unchecked((uint)-5);
			public const uint CL_OUT_OF_HOST_MEMORY = unchecked((uint)-6);
			public const uint CL_PROFILING_INFO_NOT_AVAILABLE = unchecked((uint)-7);
			public const uint CL_MEM_COPY_OVERLAP = unchecked((uint)-8);
			public const uint CL_IMAGE_FORMAT_MISMATCH = unchecked((uint)-9);
			public const uint CL_IMAGE_FORMAT_NOT_SUPPORTED = unchecked((uint)-10);
			public const uint CL_BUILD_PROGRAM_FAILURE = unchecked((uint)-11);
			public const uint CL_MAP_FAILURE = unchecked((uint)-12);

			public const uint CL_INVALID_VALUE = unchecked((uint)-30);
			public const uint CL_INVALID_DEVICE_TYPE = unchecked((uint)-31);
			public const uint CL_INVALID_PLATFORM = unchecked((uint)-32);
			public const uint CL_INVALID_DEVICE = unchecked((uint)-33);
			public const uint CL_INVALID_CONTEXT = unchecked((uint)-34);
			public const uint CL_INVALID_QUEUE_PROPERTIES = unchecked((uint)-35);
			public const uint CL_INVALID_COMMAND_QUEUE = unchecked((uint)-36);
			public const uint CL_INVALID_HOST_PTR = unchecked((uint)-37);
			public const uint CL_INVALID_MEM_OBJECT = unchecked((uint)-38);
			public const uint CL_INVALID_IMAGE_FORMAT_DESCRIPTOR = unchecked((uint)-39);
			public const uint CL_INVALID_IMAGE_SIZE = unchecked((uint)-40);
			public const uint CL_INVALID_SAMPLER = unchecked((uint)-41);
			public const uint CL_INVALID_BINARY = unchecked((uint)-42);
			public const uint CL_INVALID_BUILD_OPTIONS = unchecked((uint)-43);
			public const uint CL_INVALID_PROGRAM = unchecked((uint)-44);
			public const uint CL_INVALID_PROGRAM_EXECUTABLE = unchecked((uint)-45);
			public const uint CL_INVALID_KERNEL_NAME = unchecked((uint)-46);
			public const uint CL_INVALID_KERNEL_DEFINITION = unchecked((uint)-47);
			public const uint CL_INVALID_KERNEL = unchecked((uint)-48);
			public const uint CL_INVALID_ARG_INDEX = unchecked((uint)-49);
			public const uint CL_INVALID_ARG_VALUE = unchecked((uint)-50);
			public const uint CL_INVALID_ARG_SIZE = unchecked((uint)-51);
			public const uint CL_INVALID_KERNEL_ARGS = unchecked((uint)-52);
			public const uint CL_INVALID_WORK_DIMENSION = unchecked((uint)-53);
			public const uint CL_INVALID_WORK_GROUP_SIZE = unchecked((uint)-54);
			public const uint CL_INVALID_WORK_ITEM_SIZE = unchecked((uint)-55);
			public const uint CL_INVALID_GLOBAL_OFFSET = unchecked((uint)-56);
			public const uint CL_INVALID_EVENT_WAIT_LIST = unchecked((uint)-57);
			public const uint CL_INVALID_EVENT = unchecked((uint)-58);
			public const uint CL_INVALID_OPERATION = unchecked((uint)-59);
			public const uint CL_INVALID_GL_OBJECT = unchecked((uint)-60);
			public const uint CL_INVALID_BUFFER_SIZE = unchecked((uint)-61);
			public const uint CL_INVALID_MIP_LEVEL = unchecked((uint)-62);
			public const uint CL_INVALID_GLOBAL_WORK_SIZE = unchecked((uint)-63);
		
			/* OpenCL Version */
			public const uint CL_VERSION_1_0 = 1;

			/* cl_bool */
			public const uint CL_FALSE = 0;
			public const uint CL_TRUE = 1;

			/* cl_platform_info */
			public const uint CL_PLATFORM_PROFILE = 0x0900;
			public const uint CL_PLATFORM_VERSION = 0x0901;
			public const uint CL_PLATFORM_NAME = 0x0902;
			public const uint CL_PLATFORM_VENDOR = 0x0903;
			public const uint CL_PLATFORM_EXTENSIONS = 0x0904;

			/* cl_device_type - bitfield */
			public const uint CL_DEVICE_TYPE_DEFAULT = (1 << 0);
			public const uint CL_DEVICE_TYPE_CPU = (1 << 1);
			public const uint CL_DEVICE_TYPE_GPU = (1 << 2);
			public const uint CL_DEVICE_TYPE_ACCELERATOR = (1 << 3);
			public const uint CL_DEVICE_TYPE_ALL = 0xFFFFFFFF;

			/* cl_device_info */
			public const uint CL_DEVICE_TYPE = 0x1000;
			public const uint CL_DEVICE_VENDOR_ID = 0x1001;
			public const uint CL_DEVICE_MAX_COMPUTE_UNITS = 0x1002;
			public const uint CL_DEVICE_MAX_WORK_ITEM_DIMENSIONS = 0x1003;
			public const uint CL_DEVICE_MAX_WORK_GROUP_SIZE = 0x1004;
			public const uint CL_DEVICE_MAX_WORK_ITEM_SIZES = 0x1005;
			public const uint CL_DEVICE_PREFERRED_VECTOR_WIDTH_CHAR = 0x1006;
			public const uint CL_DEVICE_PREFERRED_VECTOR_WIDTH_SHORT = 0x1007;
			public const uint CL_DEVICE_PREFERRED_VECTOR_WIDTH_INT = 0x1008;
			public const uint CL_DEVICE_PREFERRED_VECTOR_WIDTH_LONG = 0x1009;
			public const uint CL_DEVICE_PREFERRED_VECTOR_WIDTH_FLOAT = 0x100A;
			public const uint CL_DEVICE_PREFERRED_VECTOR_WIDTH_DOUBLE = 0x100B;
			public const uint CL_DEVICE_MAX_CLOCK_FREQUENCY = 0x100C;
			public const uint CL_DEVICE_ADDRESS_BITS = 0x100D;
			public const uint CL_DEVICE_MAX_READ_IMAGE_ARGS = 0x100E;
			public const uint CL_DEVICE_MAX_WRITE_IMAGE_ARGS = 0x100F;
			public const uint CL_DEVICE_MAX_MEM_ALLOC_SIZE = 0x1010;
			public const uint CL_DEVICE_IMAGE2D_MAX_WIDTH = 0x1011;
			public const uint CL_DEVICE_IMAGE2D_MAX_HEIGHT = 0x1012;
			public const uint CL_DEVICE_IMAGE3D_MAX_WIDTH = 0x1013;
			public const uint CL_DEVICE_IMAGE3D_MAX_HEIGHT = 0x1014;
			public const uint CL_DEVICE_IMAGE3D_MAX_DEPTH = 0x1015;
			public const uint CL_DEVICE_IMAGE_SUPPORT = 0x1016;
			public const uint CL_DEVICE_MAX_PARAMETER_SIZE = 0x1017;
			public const uint CL_DEVICE_MAX_SAMPLERS = 0x1018;
			public const uint CL_DEVICE_MEM_BASE_ADDR_ALIGN = 0x1019;
			public const uint CL_DEVICE_MIN_DATA_TYPE_ALIGN_SIZE = 0x101A;
			public const uint CL_DEVICE_SINGLE_FP_CONFIG = 0x101B;
			public const uint CL_DEVICE_GLOBAL_MEM_CACHE_TYPE = 0x101C;
			public const uint CL_DEVICE_GLOBAL_MEM_CACHELINE_SIZE = 0x101D;
			public const uint CL_DEVICE_GLOBAL_MEM_CACHE_SIZE = 0x101E;
			public const uint CL_DEVICE_GLOBAL_MEM_SIZE = 0x101F;
			public const uint CL_DEVICE_MAX_CONSTANT_BUFFER_SIZE = 0x1020;
			public const uint CL_DEVICE_MAX_CONSTANT_ARGS = 0x1021;
			public const uint CL_DEVICE_LOCAL_MEM_TYPE = 0x1022;
			public const uint CL_DEVICE_LOCAL_MEM_SIZE = 0x1023;
			public const uint CL_DEVICE_ERROR_CORRECTION_SUPPORT = 0x1024;
			public const uint CL_DEVICE_PROFILING_TIMER_RESOLUTION = 0x1025;
			public const uint CL_DEVICE_ENDIAN_LITTLE = 0x1026;
			public const uint CL_DEVICE_AVAILABLE = 0x1027;
			public const uint CL_DEVICE_COMPILER_AVAILABLE = 0x1028;
			public const uint CL_DEVICE_EXECUTION_CAPABILITIES = 0x1029;
			public const uint CL_DEVICE_QUEUE_PROPERTIES = 0x102A;
			public const uint CL_DEVICE_NAME = 0x102B;
			public const uint CL_DEVICE_VENDOR = 0x102C;
			public const uint CL_DRIVER_VERSION = 0x102D;
			public const uint CL_DEVICE_PROFILE = 0x102E;
			public const uint CL_DEVICE_VERSION = 0x102F;
			public const uint CL_DEVICE_EXTENSIONS = 0x1030;
			public const uint CL_DEVICE_PLATFORM = 0x1031;
			/* 0x1032 reserved for CL_DEVICE_DOUBLE_FP_CONFIG */
			/* 0x1033 reserved for CL_DEVICE_HALF_FP_CONFIG */

			/* cl_device_fp_config - bitfield */
			public const uint CL_FP_DENORM = (1 << 0);
			public const uint CL_FP_INF_NAN = (1 << 1);
			public const uint CL_FP_ROUND_TO_NEAREST = (1 << 2);
			public const uint CL_FP_ROUND_TO_ZERO = (1 << 3);
			public const uint CL_FP_ROUND_TO_INF = (1 << 4);
			public const uint CL_FP_FMA = (1 << 5);

			/* cl_device_mem_cache_type */
			public const uint CL_NONE = 0x0;
			public const uint CL_READ_ONLY_CACHE = 0x1;
			public const uint CL_READ_WRITE_CACHE = 0x2;

			/* cl_device_local_mem_type */
			public const uint CL_LOCAL = 0x1;
			public const uint CL_GLOBAL = 0x2;

			/* cl_device_exec_capabilities - bitfield */
			public const uint CL_EXEC_KERNEL = (1 << 0);
			public const uint CL_EXEC_NATIVE_KERNEL = (1 << 1);

			/* cl_command_queue_properties - bitfield */
			public const uint CL_QUEUE_OUT_OF_ORDER_EXEC_MODE_ENABLE = (1 << 0);
			public const uint CL_QUEUE_PROFILING_ENABLE = (1 << 1);

			/* cl_context_info  */
			public const uint CL_CONTEXT_REFERENCE_COUNT = 0x1080;
			public const uint CL_CONTEXT_DEVICES = 0x1081;
			public const uint CL_CONTEXT_PROPERTIES = 0x1082;

			/* cl_context_info + cl_context_properties */
			public const uint CL_CONTEXT_PLATFORM = 0x1084;

			/* cl_command_queue_info */
			public const uint CL_QUEUE_CONTEXT = 0x1090;
			public const uint CL_QUEUE_DEVICE = 0x1091;
			public const uint CL_QUEUE_REFERENCE_COUNT = 0x1092;
			public const uint CL_QUEUE_PROPERTIES = 0x1093;

			/* cl_mem_flags - bitfield */
			public const uint CL_MEM_READ_WRITE = (1 << 0);
			public const uint CL_MEM_WRITE_ONLY = (1 << 1);
			public const uint CL_MEM_READ_ONLY = (1 << 2);
			public const uint CL_MEM_USE_HOST_PTR = (1 << 3);
			public const uint CL_MEM_ALLOC_HOST_PTR = (1 << 4);
			public const uint CL_MEM_COPY_HOST_PTR = (1 << 5);

			/* cl_channel_order */
			public const uint CL_R = 0x10B0;
			public const uint CL_A = 0x10B1;
			public const uint CL_RG = 0x10B2;
			public const uint CL_RA = 0x10B3;
			public const uint CL_RGB = 0x10B4;
			public const uint CL_RGBA = 0x10B5;
			public const uint CL_BGRA = 0x10B6;
			public const uint CL_ARGB = 0x10B7;
			public const uint CL_INTENSITY = 0x10B8;
			public const uint CL_LUMINANCE = 0x10B9;

			/* cl_channel_type */
			public const uint CL_SNORM_INT8 = 0x10D0;
			public const uint CL_SNORM_INT16 = 0x10D1;
			public const uint CL_UNORM_INT8 = 0x10D2;
			public const uint CL_UNORM_INT16 = 0x10D3;
			public const uint CL_UNORM_SHORT_565 = 0x10D4;
			public const uint CL_UNORM_SHORT_555 = 0x10D5;
			public const uint CL_UNORM_INT_101010 = 0x10D6;
			public const uint CL_SIGNED_INT8 = 0x10D7;
			public const uint CL_SIGNED_INT16 = 0x10D8;
			public const uint CL_SIGNED_INT32 = 0x10D9;
			public const uint CL_UNSIGNED_INT8 = 0x10DA;
			public const uint CL_UNSIGNED_INT16 = 0x10DB;
			public const uint CL_UNSIGNED_INT32 = 0x10DC;
			public const uint CL_HALF_FLOAT = 0x10DD;
			public const uint CL_FLOAT = 0x10DE;

			/* cl_mem_object_type */
			public const uint CL_MEM_OBJECT_BUFFER = 0x10F0;
			public const uint CL_MEM_OBJECT_IMAGE2D = 0x10F1;
			public const uint CL_MEM_OBJECT_IMAGE3D = 0x10F2;

			/* cl_mem_info */
			public const uint CL_MEM_TYPE = 0x1100;
			public const uint CL_MEM_FLAGS = 0x1101;
			public const uint CL_MEM_SIZE = 0x1102;
			public const uint CL_MEM_HOST_PTR = 0x1103;
			public const uint CL_MEM_MAP_COUNT = 0x1104;
			public const uint CL_MEM_REFERENCE_COUNT = 0x1105;
			public const uint CL_MEM_CONTEXT = 0x1106;

			/* cl_image_info */
			public const uint CL_IMAGE_FORMAT = 0x1110;
			public const uint CL_IMAGE_ELEMENT_SIZE = 0x1111;
			public const uint CL_IMAGE_ROW_PITCH = 0x1112;
			public const uint CL_IMAGE_SLICE_PITCH = 0x1113;
			public const uint CL_IMAGE_WIDTH = 0x1114;
			public const uint CL_IMAGE_HEIGHT = 0x1115;
			public const uint CL_IMAGE_DEPTH = 0x1116;

			/* cl_addressing_mode */
			public const uint CL_ADDRESS_NONE = 0x1130;
			public const uint CL_ADDRESS_CLAMP_TO_EDGE = 0x1131;
			public const uint CL_ADDRESS_CLAMP = 0x1132;
			public const uint CL_ADDRESS_REPEAT = 0x1133;

			/* cl_filter_mode */
			public const uint CL_FILTER_NEAREST = 0x1140;
			public const uint CL_FILTER_LINEAR = 0x1141;

			/* cl_sampler_info */
			public const uint CL_SAMPLER_REFERENCE_COUNT = 0x1150;
			public const uint CL_SAMPLER_CONTEXT = 0x1151;
			public const uint CL_SAMPLER_NORMALIZED_COORDS = 0x1152;
			public const uint CL_SAMPLER_ADDRESSING_MODE = 0x1153;
			public const uint CL_SAMPLER_FILTER_MODE = 0x1154;

			/* cl_map_flags - bitfield */
			public const uint CL_MAP_READ = (1 << 0);
			public const uint CL_MAP_WRITE = (1 << 1);

			/* cl_program_info */
			public const uint CL_PROGRAM_REFERENCE_COUNT = 0x1160;
			public const uint CL_PROGRAM_CONTEXT = 0x1161;
			public const uint CL_PROGRAM_NUM_DEVICES = 0x1162;
			public const uint CL_PROGRAM_DEVICES = 0x1163;
			public const uint CL_PROGRAM_SOURCE = 0x1164;
			public const uint CL_PROGRAM_BINARY_SIZES = 0x1165;
			public const uint CL_PROGRAM_BINARIES = 0x1166;

			/* cl_program_build_info */
			public const uint CL_PROGRAM_BUILD_STATUS = 0x1181;
			public const uint CL_PROGRAM_BUILD_OPTIONS = 0x1182;
			public const uint CL_PROGRAM_BUILD_LOG = 0x1183;

			/* cl_build_status */
			public const uint CL_BUILD_SUCCESS = 0;
			public const uint CL_BUILD_NONE = unchecked((uint)-1);
			public const uint CL_BUILD_ERROR = unchecked((uint)-2);
			public const uint CL_BUILD_IN_PROGRESS = unchecked((uint)-3);

			/* cl_kernel_info */
			public const uint CL_KERNEL_FUNCTION_NAME = 0x1190;
			public const uint CL_KERNEL_NUM_ARGS = 0x1191;
			public const uint CL_KERNEL_REFERENCE_COUNT = 0x1192;
			public const uint CL_KERNEL_CONTEXT = 0x1193;
			public const uint CL_KERNEL_PROGRAM = 0x1194;

			/* cl_kernel_work_group_info */
			public const uint CL_KERNEL_WORK_GROUP_SIZE = 0x11B0;
			public const uint CL_KERNEL_COMPILE_WORK_GROUP_SIZE = 0x11B1;
			public const uint CL_KERNEL_LOCAL_MEM_SIZE = 0x11B2;

			/* cl_event_info  */
			public const uint CL_EVENT_COMMAND_QUEUE = 0x11D0;
			public const uint CL_EVENT_COMMAND_TYPE = 0x11D1;
			public const uint CL_EVENT_REFERENCE_COUNT = 0x11D2;
			public const uint CL_EVENT_COMMAND_EXECUTION_STATUS = 0x11D3;

			/* cl_command_type */
			public const uint CL_COMMAND_NDRANGE_KERNEL = 0x11F0;
			public const uint CL_COMMAND_TASK = 0x11F1;
			public const uint CL_COMMAND_NATIVE_KERNEL = 0x11F2;
			public const uint CL_COMMAND_READ_BUFFER = 0x11F3;
			public const uint CL_COMMAND_WRITE_BUFFER = 0x11F4;
			public const uint CL_COMMAND_COPY_BUFFER = 0x11F5;
			public const uint CL_COMMAND_READ_IMAGE = 0x11F6;
			public const uint CL_COMMAND_WRITE_IMAGE = 0x11F7;
			public const uint CL_COMMAND_COPY_IMAGE = 0x11F8;
			public const uint CL_COMMAND_COPY_IMAGE_TO_BUFFER = 0x11F9;
			public const uint CL_COMMAND_COPY_BUFFER_TO_IMAGE = 0x11FA;
			public const uint CL_COMMAND_MAP_BUFFER = 0x11FB;
			public const uint CL_COMMAND_MAP_IMAGE = 0x11FC;
			public const uint CL_COMMAND_UNMAP_MEM_OBJECT = 0x11FD;
			public const uint CL_COMMAND_MARKER = 0x11FE;
			public const uint CL_COMMAND_ACQUIRE_GL_OBJECTS = 0x11FF;
			public const uint CL_COMMAND_RELEASE_GL_OBJECTS = 0x1200;

			/* command execution status */
			public const uint CL_COMPLETE = 0x0;
			public const uint CL_RUNNING = 0x1;
			public const uint CL_SUBMITTED = 0x2;
			public const uint CL_QUEUED = 0x3;

			/* cl_profiling_info  */
			public const uint CL_PROFILING_COMMAND_QUEUED = 0x1280;
			public const uint CL_PROFILING_COMMAND_SUBMIT = 0x1281;
			public const uint CL_PROFILING_COMMAND_START = 0x1282;
			public const uint CL_PROFILING_COMMAND_END = 0x1283;


		}
	}
}
