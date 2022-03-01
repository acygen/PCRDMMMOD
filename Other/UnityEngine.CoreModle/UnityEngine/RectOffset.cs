using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/IMGUI/GUIStyle.h")]
	[NativeHeader("Runtime/Camera/RenderLayers/GUILayer.h")]
	[UsedByNativeCode]
	public class RectOffset
	{
		[NonSerialized]
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal IntPtr m_Ptr;

		private readonly object m_SourceStyle;

		[NativeProperty("left", false, TargetType.Field)]
		public extern int left
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("right", false, TargetType.Field)]
		public extern int right
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("top", false, TargetType.Field)]
		public extern int top
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("bottom", false, TargetType.Field)]
		public extern int bottom
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int horizontal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int vertical
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public RectOffset()
		{
			m_Ptr = InternalCreate();
		}

		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal RectOffset(object sourceStyle, IntPtr source)
		{
			m_SourceStyle = sourceStyle;
			m_Ptr = source;
		}

		public RectOffset(int left, int right, int top, int bottom)
		{
			m_Ptr = InternalCreate();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadAndSerializationSafe]
		private static extern IntPtr InternalCreate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadAndSerializationSafe]
		private static extern void InternalDestroy(IntPtr ptr);

		public Rect Add(Rect rect)
		{
			Add_Injected(ref rect, out var ret);
			return ret;
		}

		public Rect Remove(Rect rect)
		{
			Remove_Injected(ref rect, out var ret);
			return ret;
		}

		~RectOffset()
		{
			if (m_SourceStyle == null)
			{
				Destroy();
			}
		}

		public override string ToString()
		{
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", left, right, top, bottom);
		}

		private void Destroy()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				InternalDestroy(m_Ptr);
				m_Ptr = IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Add_Injected(ref Rect rect, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Remove_Injected(ref Rect rect, out Rect ret);
	}
}
