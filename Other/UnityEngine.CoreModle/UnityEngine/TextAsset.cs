using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Scripting/TextAsset.h")]
	public class TextAsset : Object
	{
		internal enum CreateOptions
		{
			None,
			CreateNativeObject
		}

		public extern string text
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern byte[] bytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public TextAsset()
			: this(CreateOptions.CreateNativeObject, null)
		{
		}

		public TextAsset(string text)
			: this(CreateOptions.CreateNativeObject, text)
		{
		}

		internal TextAsset(CreateOptions options, string text)
		{
			if (options == CreateOptions.CreateNativeObject)
			{
				Internal_CreateInstance(this, text);
			}
		}

		public override string ToString()
		{
			return text;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance([Writable] TextAsset self, string text);
	}
}
