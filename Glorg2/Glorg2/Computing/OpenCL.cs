using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using cl_char = System.SByte;
using cl_uchar = System.Byte;
using cl_short = System.Int16;
using cl_ushort = System.UInt16;
using cl_int = System.Int32;
using cl_uint = System.UInt32;
using cl_long = System.Int64;
using cl_ulong = System.UInt64;

using cl_half = Glorg2.Half;
using cl_float = System.Single;
using cl_double = System.Double;

using cl_platform_id = System.IntPtr;
using cl_device_id = System.IntPtr;
using cl_context = System.IntPtr;
using cl_command_queue = System.IntPtr;
using cl_mem = System.IntPtr;
using cl_program = System.IntPtr;
using cl_kernel = System.IntPtr;
using cl_event = System.IntPtr;
using cl_sampler = System.IntPtr;

using intptr_t = System.IntPtr;
using size_t = System.Int32;

using cl_bool = System.UInt32;
using cl_bitfield = System.UInt64;
using cl_device_type = System.UInt64;
using cl_platform_info = System.UInt32;
using cl_device_info = System.UInt32;
using cl_device_address_info = System.UInt64;
using cl_device_fp_config = System.UInt64;
using cl_device_mem_cache_type = System.UInt32;
using cl_device_local_mem_type = System.UInt32;
using cl_device_exec_capabilities = System.UInt64;
using cl_command_queue_properties = System.UInt64;

using cl_context_properties = System.IntPtr;
using cl_context_info = System.UInt32;
using cl_command_queue_info = System.UInt32;
using cl_channel_order = System.UInt32;
using cl_channel_type = System.UInt32;
using cl_mem_flags = System.UInt64;
using cl_mem_object_type = System.UInt32;
using cl_mem_info = System.UInt32;
using cl_image_info = System.UInt32;
using cl_addressing_mode = System.UInt32;
using cl_filter_mode = System.UInt32;
using cl_sampler_info = System.UInt32;
using cl_map_flags = System.UInt64;
using cl_program_info = System.UInt32;
using cl_program_build_info = System.UInt32;
using cl_build_status = System.Int32;
using cl_kernel_info = System.UInt32;
using cl_kernel_work_group_info = System.UInt32;
using cl_event_info = System.UInt32;
using cl_command_type = System.UInt32;
using cl_profiling_info = System.UInt32;



namespace Glorg2.Computing
{

	public struct cl_image_format
	{
		public cl_channel_order image_channel_order;
		public cl_channel_type image_channel_data_type;
	}

	public static partial class OpenCL
	{
		public const string DllName = "OpenCL32.dll";

		/* Platform API */
		[DllImport(DllName)]
		public static extern cl_int clGetPlatformIDs(cl_uint num_entries,
						 cl_platform_id[] platforms,
						 ref cl_uint num_platforms);

		[DllImport(DllName)]
		public static extern cl_int clGetPlatformInfo(cl_platform_id platform,
						  cl_platform_info param_name,
						  size_t param_value_size,
						  IntPtr param_value,
						  ref size_t param_value_size_ret);

		/* Device APIs */
		[DllImport(DllName)]
		public static extern cl_int clGetDeviceIDs(cl_platform_id platform,
					   cl_device_type device_type,
					   cl_uint num_entries,
					   cl_device_id[] devices,
					   ref cl_uint num_devices);

		[DllImport(DllName)]
		public static extern cl_int clGetDeviceInfo(cl_device_id device,
						cl_device_info param_name,
						size_t param_value_size,
						IntPtr param_value,
						ref size_t param_value_size_ret);

		/* Context APIs  */
		[DllImport(DllName)]
		public static extern cl_context
		clCreateContext(cl_context_properties[] properties,
						cl_uint num_devices,
						cl_device_id[] devices,
						Delegate pfn_notify,
						IntPtr user_data,
						ref cl_int errcode_ret);
		[DllImport(DllName)]
		public static extern cl_context
		clCreateContextFromType(cl_context_properties[] properties,
								cl_device_type device_type,
								Delegate pfn_notify,
								IntPtr user_data,
								ref int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainContext(cl_context context);

		[DllImport(DllName)]
		public static extern cl_int clReleaseContext(cl_context context);

		[DllImport(DllName)]
		public static extern cl_int clGetContextInfo(cl_context context,
						 cl_context_info param_name,
						 size_t param_value_size,
						 IntPtr param_value,
						 ref size_t param_value_size_ret);

		/* Command Queue APIs */
		[DllImport(DllName)]
		public static extern cl_command_queue clCreateCommandQueue(cl_context context,
							 cl_device_id device,
							 cl_command_queue_properties properties,
							 ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainCommandQueue(cl_command_queue command_queue);

		[DllImport(DllName)]
		public static extern cl_int clReleaseCommandQueue(cl_command_queue command_queue);

		[DllImport(DllName)]
		public static extern cl_int clGetCommandQueueInfo(cl_command_queue command_queue,
							  cl_command_queue_info param_name,
							  size_t param_value_size,
							  IntPtr param_value,
							  ref size_t param_value_size_ret);

		[DllImport(DllName)]
		public static extern cl_int clSetCommandQueueProperty(cl_command_queue command_queue,
								  cl_command_queue_properties properties,
								  cl_bool enable,
								  ref cl_command_queue_properties old_properties);

		/* Memory Object APIs  */
		[DllImport(DllName)]
		public static extern cl_mem clCreateBuffer(cl_context context,
					   cl_mem_flags flags,
					   size_t size,
					   IntPtr host_ptr,
					   ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_mem clCreateImage2D(cl_context context,
						cl_mem_flags flags,
						cl_image_format[] image_format,
						size_t image_width,
						size_t image_height,
						size_t image_row_pitch,
						IntPtr host_ptr,
						ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_mem clCreateImage3D(cl_context context,
						cl_mem_flags flags,
						cl_image_format[] image_format,
						size_t image_width,
						size_t image_height,
						size_t image_depth,
						size_t image_row_pitch,
						size_t image_slice_pitch,
						IntPtr host_ptr,
						ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainMemObject(cl_mem memobj);

		[DllImport(DllName)]
		public static extern cl_int clReleaseMemObject(cl_mem memobj);

		[DllImport(DllName)]
		public static extern cl_int clGetSupportedImageFormats(cl_context context,
								   cl_mem_flags flags,
								   cl_mem_object_type image_type,
								   cl_uint num_entries,
								   cl_image_format[] image_formats,
								   ref cl_uint num_image_formats);

		[DllImport(DllName)]
		public static extern cl_int clGetMemObjectInfo(cl_mem memobj,
						   cl_mem_info param_name,
						   size_t param_value_size,
						   IntPtr param_value,
						   ref size_t param_value_size_ret);

		[DllImport(DllName)]
		public static extern cl_int clGetImageInfo(cl_mem image,
					   cl_image_info param_name,
					   size_t param_value_size,
					   IntPtr param_value,
					   ref size_t param_value_size_ret);

		/* Sampler APIs  */
		[DllImport(DllName)]
		public static extern cl_sampler clCreateSampler(cl_context context,
						cl_bool normalized_coords,
						cl_addressing_mode addressing_mode,
						cl_filter_mode filter_mode,
						ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainSampler(cl_sampler sampler);

		[DllImport(DllName)]
		public static extern cl_int clReleaseSampler(cl_sampler sampler);

		[DllImport(DllName)]
		public static extern cl_int clGetSamplerInfo(cl_sampler sampler,
						 cl_sampler_info param_name,
						 size_t param_value_size,
						 IntPtr param_value,
						 ref size_t param_value_size_ret);

		/* Program Object APIs  */
		[DllImport(DllName)]
		public static extern cl_program clCreateProgramWithSource(cl_context context,
								  cl_uint count,
								  string[] strings,
								  size_t[] lengths,
								  ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_program clCreateProgramWithBinary(cl_context context,
								  cl_uint num_devices,
								  cl_device_id[] device_list,
								  size_t[] lengths,
								  byte[][] binaries,
								  ref cl_int binary_status,
								  ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainProgram(cl_program program);

		[DllImport(DllName)]
		public static extern cl_int clReleaseProgram(cl_program program);

		[DllImport(DllName)]
		public static extern cl_int
		clBuildProgram(cl_program program,
					   cl_uint num_devices,
					   cl_device_id[] device_list,
					   byte[] options,
					   Delegate notify,
					   IntPtr user_data);

		[DllImport(DllName)]
		public static extern cl_int clUnloadCompiler();

		[DllImport(DllName)]
		public static extern cl_int clGetProgramInfo(cl_program program,
						 cl_program_info param_name,
						 size_t param_value_size,
						 IntPtr param_value,
						 ref size_t param_value_size_ret);

		[DllImport(DllName)]
		public static extern cl_int clGetProgramBuildInfo(cl_program program,
							  cl_device_id device,
							  cl_program_build_info param_name,
							  size_t param_value_size,
							  IntPtr param_value,
							  ref size_t param_value_size_ret);

		/* Kernel Object APIs */
		[DllImport(DllName)]
		public static extern cl_kernel clCreateKernel(cl_program program,
					   string kernel_name,
					   ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clCreateKernelsInProgram(cl_program program,
								 cl_uint num_kernels,
								 cl_kernel[] kernels,
								 ref cl_uint num_kernels_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainKernel(cl_kernel kernel);

		[DllImport(DllName)]
		public static extern cl_int clReleaseKernel(cl_kernel kernel);

		[DllImport(DllName)]
		public static extern cl_int clSetKernelArg(cl_kernel kernel,
					   cl_uint arg_index,
					   size_t arg_size,
					   IntPtr arg_value);

		[DllImport(DllName)]
		public static extern cl_int clGetKernelInfo(cl_kernel kernel,
						cl_kernel_info param_name,
						size_t param_value_size,
						IntPtr param_value,
						ref size_t param_value_size_ret);

		[DllImport(DllName)]
		public static extern cl_int clGetKernelWorkGroupInfo(cl_kernel kernel,
								 cl_device_id device,
								 cl_kernel_work_group_info param_name,
								 size_t param_value_size,
								 IntPtr param_value,
								 ref size_t param_value_size_ret);

		/* @event Object APIs  */
		[DllImport(DllName)]
		public static extern cl_int clWaitForEvents(cl_uint num_events,
						cl_event[] event_list);

		[DllImport(DllName)]
		public static extern cl_int clGetEventInfo(cl_event @event,
					   cl_event_info param_name,
					   size_t param_value_size,
					   IntPtr param_value,
					   ref size_t param_value_size_ret);

		[DllImport(DllName)]
		public static extern cl_int clRetainEvent(cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clReleaseEvent(cl_event @event);

		/* Profiling APIs  */
		[DllImport(DllName)]
		public static extern cl_int clGetEventProfilingInfo(cl_event @event,
								cl_profiling_info param_name,
								size_t param_value_size,
								IntPtr param_value,
								ref size_t param_value_size_ret);

		/* Flush and Finish APIs */
		[DllImport(DllName)]
		public static extern cl_int clFlush(cl_command_queue command_queue);

		[DllImport(DllName)]
		public static extern cl_int clFinish(cl_command_queue command_queue);

		/* Enqueued Commands APIs */
		[DllImport(DllName)]
		public static extern cl_int clEnqueueReadBuffer(cl_command_queue command_queue,
							cl_mem buffer,
							cl_bool blocking_read,
							size_t offset,
							size_t cb,
							IntPtr ptr,
							cl_uint num_events_in_wait_list,
							cl_event[] event_wait_list,
							ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueWriteBuffer(cl_command_queue command_queue,
							 cl_mem buffer,
							 cl_bool blocking_write,
							 size_t offset,
							 size_t cb,
							 IntPtr ptr,
							 cl_uint num_events_in_wait_list,
							 cl_event[] event_wait_list,
							 ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueCopyBuffer(cl_command_queue command_queue,
							cl_mem src_buffer,
							cl_mem dst_buffer,
							size_t src_offset,
							size_t dst_offset,
							size_t cb,
							cl_uint num_events_in_wait_list,
							cl_event[] event_wait_list,
							ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueReadImage(cl_command_queue command_queue,
						   cl_mem image,
						   cl_bool blocking_read,
						   ref Vector3Int origin,
						   ref Vector3Int region,
						   size_t row_pitch,
						   size_t slice_pitch,
						   IntPtr ptr,
						   cl_uint num_events_in_wait_list,
						   cl_event[] event_wait_list,
						   ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueWriteImage(cl_command_queue command_queue,
							cl_mem image,
							cl_bool blocking_write,
							ref Vector3Int origin,
							ref Vector3Int region,
							size_t input_row_pitch,
							size_t input_slice_pitch,
							IntPtr ptr,
							cl_uint num_events_in_wait_list,
							cl_event[] event_wait_list,
							ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueCopyImage(cl_command_queue command_queue,
						   cl_mem src_image,
						   cl_mem dst_image,
						   ref Vector3Int src_origin,
						   ref Vector3Int dst_origin,
						   ref Vector3Int region,
						   cl_uint num_events_in_wait_list,
						   cl_event[] event_wait_list,
						   ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueCopyImageToBuffer(cl_command_queue command_queue,
								   cl_mem src_image,
								   cl_mem dst_buffer,
								   ref Vector3Int origin,
								   ref Vector3Int region,
								   size_t dst_offset,
								   cl_uint num_events_in_wait_list,
								   cl_event[] event_wait_list,
								   ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueCopyBufferToImage(cl_command_queue command_queue,
								   cl_mem src_buffer,
								   cl_mem dst_image,
								   size_t src_offset,
								   ref Vector3Int origin,
								   ref Vector3Int region,
								   cl_uint num_events_in_wait_list,
								   cl_event[] event_wait_list,
								   ref cl_event @event);
		[DllImport(DllName)]
		public static extern IntPtr
		clEnqueueMapBuffer(cl_command_queue command_queue,
						   cl_mem buffer,
						   cl_bool blocking_map,
						   cl_map_flags map_flags,
						   size_t offset,
						   size_t cb,
						   cl_uint num_events_in_wait_list,
						   cl_event[] event_wait_list,
						   ref cl_event @event,
						   ref cl_int errcode_ret);
		[DllImport(DllName)]
		public static extern IntPtr
		clEnqueueMapImage(cl_command_queue command_queue,
						  cl_mem image,
						  cl_bool blocking_map,
						  cl_map_flags map_flags,
						  ref Vector3Int origin,
						  ref Vector3Int region,
						  ref size_t image_row_pitch,
						  ref size_t image_slice_pitch,
						  cl_uint num_events_in_wait_list,
						  cl_event[] event_wait_list,
						  ref cl_event @event,
						  ref cl_int errcode_ret);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueUnmapMemObject(cl_command_queue command_queue,
								cl_mem memobj,
								IntPtr mapped_ptr,
								cl_uint num_events_in_wait_list,
								cl_event event_wait_list,
								ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueNDRangeKernel(cl_command_queue command_queue,
							   cl_kernel kernel,
							   cl_uint work_dim,
							   size_t[] global_work_offset,
							   size_t[] global_work_size,
							   size_t[] local_work_size,
							   cl_uint num_events_in_wait_list,
							   cl_event[] event_wait_list,
							   ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueTask(cl_command_queue command_queue,
					  cl_kernel kernel,
					  cl_uint num_events_in_wait_list,
					  cl_event[] event_wait_list,
					  ref cl_event @event);
		[DllImport(DllName)]
		public static extern cl_int
		clEnqueueNativeKernel(cl_command_queue command_queue,
							  Delegate user_func,
							  IntPtr args,
							  size_t cb_args,
							  cl_uint num_mem_objects,
							  cl_mem[] mem_list,
							  IntPtr[] args_mem_loc,
							  cl_uint num_events_in_wait_list,
							  cl_event[] event_wait_list,
							  ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueMarker(cl_command_queue command_queue,
						ref cl_event @event);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueWaitForEvents(cl_command_queue command_queue,
							   cl_uint num_events,
							   cl_event[] event_list);

		[DllImport(DllName)]
		public static extern cl_int clEnqueueBarrier(cl_command_queue command_queue);

		/* Extension function access
		 *
		 * Returns the extension function address for the given function name,
		 * or NULL if a valid function can not be found.  The client must
		 * check to make sure the address is not NULL, before using or 
		 * calling the returned function address.
		 */
		[DllImport(DllName)]
		public static extern Delegate clGetExtensionFunctionAddress(string func_name);


	}
}
