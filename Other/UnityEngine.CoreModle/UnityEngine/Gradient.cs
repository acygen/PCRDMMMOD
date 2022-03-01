using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Runtime/Export/Gradient.bindings.h")]
	[RequiredByNativeCode]
	public class Gradient : IEquatable<Gradient>
	{
		internal IntPtr m_Ptr;

		public extern GradientColorKey[] colorKeys
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Gradient_Bindings::GetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Gradient_Bindings::SetColorKeys", IsThreadSafe = true, HasExplicitThis = true)]
			set;
		}

		public extern GradientAlphaKey[] alphaKeys
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Gradient_Bindings::GetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Gradient_Bindings::SetAlphaKeys", IsThreadSafe = true, HasExplicitThis = true)]
			set;
		}

		public extern GradientMode mode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		public Gradient()
		{
			m_Ptr = Init();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Gradient_Bindings::Init", IsThreadSafe = true)]
		private static extern IntPtr Init();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Gradient_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		private extern void Cleanup();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Gradient_Bindings::Internal_Equals", IsThreadSafe = true, HasExplicitThis = true)]
		private extern bool Internal_Equals(IntPtr other);

		~Gradient()
		{
			Cleanup();
		}

		[FreeFunction(Name = "Gradient_Bindings::Evaluate", IsThreadSafe = true, HasExplicitThis = true)]
		public Color Evaluate(float time)
		{
			Evaluate_Injected(time, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Gradient_Bindings::SetKeys", IsThreadSafe = true, HasExplicitThis = true)]
		public extern void SetKeys(GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys);

		public override bool Equals(object o)
		{
			if (object.ReferenceEquals(null, o))
			{
				return false;
			}
			if (object.ReferenceEquals(this, o))
			{
				return true;
			}
			if ((object)o.GetType() != GetType())
			{
				return false;
			}
			return Equals((Gradient)o);
		}

		public bool Equals(Gradient other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (m_Ptr.Equals(other.m_Ptr))
			{
				return true;
			}
			return Internal_Equals(other.m_Ptr);
		}

		public override int GetHashCode()
		{
			return m_Ptr.GetHashCode();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Evaluate_Injected(float time, out Color ret);
	}
}
