using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Director/TexturePlayableOutput.bindings.h")]
	[NativeHeader("Runtime/Graphics/Director/TexturePlayableOutput.h")]
	[StaticAccessor("TexturePlayableOutputBindings", StaticAccessorType.DoubleColon)]
	public struct TexturePlayableOutput : IPlayableOutput
	{
		private PlayableOutputHandle m_Handle;

		public static TexturePlayableOutput Null => new TexturePlayableOutput(PlayableOutputHandle.Null);

		internal TexturePlayableOutput(PlayableOutputHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOutputOfType<TexturePlayableOutput>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an TexturePlayableOutput.");
			}
			m_Handle = handle;
		}

		public static TexturePlayableOutput Create(PlayableGraph graph, string name, RenderTexture target)
		{
			if (!TexturePlayableGraphExtensions.InternalCreateTextureOutput(ref graph, name, out var handle))
			{
				return Null;
			}
			TexturePlayableOutput result = new TexturePlayableOutput(handle);
			result.SetTarget(target);
			return result;
		}

		public PlayableOutputHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator PlayableOutput(TexturePlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		public static explicit operator TexturePlayableOutput(PlayableOutput output)
		{
			return new TexturePlayableOutput(output.GetHandle());
		}

		public RenderTexture GetTarget()
		{
			return InternalGetTarget(ref m_Handle);
		}

		public void SetTarget(RenderTexture value)
		{
			InternalSetTarget(ref m_Handle, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern RenderTexture InternalGetTarget(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, RenderTexture target);
	}
}
