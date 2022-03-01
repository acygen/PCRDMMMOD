using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class ComputeShader : Object
	{
		private ComputeShader()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[RequiredByNativeCode]
		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public extern int FindKernel(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::HasKernel", HasExplicitThis = true)]
		public extern bool HasKernel(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<float>", HasExplicitThis = true)]
		public extern void SetFloat(int nameID, float val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		public extern void SetInt(int nameID, int val);

		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			SetVector_Injected(nameID, ref val);
		}

		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			SetMatrix_Injected(nameID, ref val);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<float>", HasExplicitThis = true)]
		private extern void SetFloatArray(int nameID, float[] values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		private extern void SetIntArray(int nameID, int[] values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		public extern void SetVectorArray(int nameID, Vector4[] values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		public extern void SetMatrixArray(int nameID, Matrix4x4[] values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ComputeShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public extern void SetTexture(int kernelIndex, int nameID, [NotNull] Texture texture, int mipLevel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ComputeShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public extern void SetTextureFromGlobal(int kernelIndex, int nameID, int globalTextureNameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		public extern void SetBuffer(int kernelIndex, int nameID, [NotNull] ComputeBuffer buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ComputeShaderScripting::GetKernelThreadGroupSizes", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public extern void GetKernelThreadGroupSizes(int kernelIndex, out uint x, out uint y, out uint z);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("DispatchComputeShader")]
		public extern void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		private extern void Internal_DispatchIndirect(int kernelIndex, [NotNull] ComputeBuffer argsBuffer, uint argsOffset);

		public void SetFloat(string name, float val)
		{
			SetFloat(Shader.PropertyToID(name), val);
		}

		public void SetInt(string name, int val)
		{
			SetInt(Shader.PropertyToID(name), val);
		}

		public void SetVector(string name, Vector4 val)
		{
			SetVector(Shader.PropertyToID(name), val);
		}

		public void SetMatrix(string name, Matrix4x4 val)
		{
			SetMatrix(Shader.PropertyToID(name), val);
		}

		public void SetVectorArray(string name, Vector4[] values)
		{
			SetVectorArray(Shader.PropertyToID(name), values);
		}

		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			SetMatrixArray(Shader.PropertyToID(name), values);
		}

		public void SetFloats(string name, params float[] values)
		{
			SetFloatArray(Shader.PropertyToID(name), values);
		}

		public void SetFloats(int nameID, params float[] values)
		{
			SetFloatArray(nameID, values);
		}

		public void SetInts(string name, params int[] values)
		{
			SetIntArray(Shader.PropertyToID(name), values);
		}

		public void SetInts(int nameID, params int[] values)
		{
			SetIntArray(nameID, values);
		}

		public void SetBool(string name, bool val)
		{
			SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		public void SetBool(int nameID, bool val)
		{
			SetInt(nameID, val ? 1 : 0);
		}

		public void SetTexture(int kernelIndex, int nameID, Texture texture)
		{
			SetTexture(kernelIndex, nameID, texture, 0);
		}

		public void SetTexture(int kernelIndex, string name, Texture texture)
		{
			SetTexture(kernelIndex, Shader.PropertyToID(name), texture, 0);
		}

		public void SetTexture(int kernelIndex, string name, Texture texture, int mipLevel)
		{
			SetTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel);
		}

		public void SetTextureFromGlobal(int kernelIndex, string name, string globalTextureName)
		{
			SetTextureFromGlobal(kernelIndex, Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		public void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer)
		{
			SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			if (argsBuffer == null)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			if (argsBuffer.m_Ptr == IntPtr.Zero)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			Internal_DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
		}

		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer)
		{
			DispatchIndirect(kernelIndex, argsBuffer, 0u);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector_Injected(int nameID, ref Vector4 val);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix_Injected(int nameID, ref Matrix4x4 val);
	}
}
