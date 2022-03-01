using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	[NativeHeader("Runtime/Graphics/Director/TextureMixerPlayable.h")]
	[NativeHeader("Runtime/Export/Director/TextureMixerPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[StaticAccessor("TextureMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct TextureMixerPlayable : IPlayable, IEquatable<TextureMixerPlayable>
	{
		private PlayableHandle m_Handle;

		internal TextureMixerPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<TextureMixerPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an TextureMixerPlayable.");
			}
			m_Handle = handle;
		}

		public static TextureMixerPlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = CreateHandle(graph);
			return new TextureMixerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateTextureMixerPlayableInternal(ref graph, ref handle))
			{
				return PlayableHandle.Null;
			}
			return handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(TextureMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator TextureMixerPlayable(Playable playable)
		{
			return new TextureMixerPlayable(playable.GetHandle());
		}

		public bool Equals(TextureMixerPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool CreateTextureMixerPlayableInternal(ref PlayableGraph graph, ref PlayableHandle handle);
	}
}
