using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[StaticAccessor("AsyncGPUReadbackManager::GetInstance()", StaticAccessorType.Dot)]
	public static class AsyncGPUReadback
	{
		private static void SetUpScriptingRequest(AsyncGPUReadbackRequest request, Action<AsyncGPUReadbackRequest> callback)
		{
			request.SetScriptingCallback(callback);
		}

		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = Request_Internal_ComputeBuffer_1(src);
			SetUpScriptingRequest(asyncGPUReadbackRequest, callback);
			return asyncGPUReadbackRequest;
		}

		public static AsyncGPUReadbackRequest Request(ComputeBuffer src, int size, int offset, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = Request_Internal_ComputeBuffer_2(src, size, offset);
			SetUpScriptingRequest(asyncGPUReadbackRequest, callback);
			return asyncGPUReadbackRequest;
		}

		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex = 0, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = Request_Internal_Texture_1(src, mipIndex);
			SetUpScriptingRequest(asyncGPUReadbackRequest, callback);
			return asyncGPUReadbackRequest;
		}

		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = Request_Internal_Texture_2(src, mipIndex, dstFormat);
			SetUpScriptingRequest(asyncGPUReadbackRequest, callback);
			return asyncGPUReadbackRequest;
		}

		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = Request_Internal_Texture_3(src, mipIndex, x, width, y, height, z, depth);
			SetUpScriptingRequest(asyncGPUReadbackRequest, callback);
			return asyncGPUReadbackRequest;
		}

		public static AsyncGPUReadbackRequest Request(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, Action<AsyncGPUReadbackRequest> callback = null)
		{
			AsyncGPUReadbackRequest asyncGPUReadbackRequest = Request_Internal_Texture_4(src, mipIndex, x, width, y, height, z, depth, dstFormat);
			SetUpScriptingRequest(asyncGPUReadbackRequest, callback);
			return asyncGPUReadbackRequest;
		}

		[NativeMethod("Request")]
		private static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_1([NotNull] ComputeBuffer buffer)
		{
			Request_Internal_ComputeBuffer_1_Injected(buffer, out var ret);
			return ret;
		}

		[NativeMethod("Request")]
		private static AsyncGPUReadbackRequest Request_Internal_ComputeBuffer_2([NotNull] ComputeBuffer src, int size, int offset)
		{
			Request_Internal_ComputeBuffer_2_Injected(src, size, offset, out var ret);
			return ret;
		}

		[NativeMethod("Request")]
		private static AsyncGPUReadbackRequest Request_Internal_Texture_1([NotNull] Texture src, int mipIndex)
		{
			Request_Internal_Texture_1_Injected(src, mipIndex, out var ret);
			return ret;
		}

		[NativeMethod("Request")]
		private static AsyncGPUReadbackRequest Request_Internal_Texture_2([NotNull] Texture src, int mipIndex, TextureFormat dstFormat)
		{
			Request_Internal_Texture_2_Injected(src, mipIndex, dstFormat, out var ret);
			return ret;
		}

		[NativeMethod("Request")]
		private static AsyncGPUReadbackRequest Request_Internal_Texture_3([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth)
		{
			Request_Internal_Texture_3_Injected(src, mipIndex, x, width, y, height, z, depth, out var ret);
			return ret;
		}

		[NativeMethod("Request")]
		private static AsyncGPUReadbackRequest Request_Internal_Texture_4([NotNull] Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat)
		{
			Request_Internal_Texture_4_Injected(src, mipIndex, x, width, y, height, z, depth, dstFormat, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Request_Internal_ComputeBuffer_1_Injected(ComputeBuffer buffer, out AsyncGPUReadbackRequest ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Request_Internal_ComputeBuffer_2_Injected(ComputeBuffer src, int size, int offset, out AsyncGPUReadbackRequest ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Request_Internal_Texture_1_Injected(Texture src, int mipIndex, out AsyncGPUReadbackRequest ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Request_Internal_Texture_2_Injected(Texture src, int mipIndex, TextureFormat dstFormat, out AsyncGPUReadbackRequest ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Request_Internal_Texture_3_Injected(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, out AsyncGPUReadbackRequest ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Request_Internal_Texture_4_Injected(Texture src, int mipIndex, int x, int width, int y, int height, int z, int depth, TextureFormat dstFormat, out AsyncGPUReadbackRequest ret);
	}
}
