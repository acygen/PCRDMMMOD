using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	public sealed class ShaderVariantCollection : Object
	{
		public struct ShaderVariant
		{
			public Shader shader;

			public PassType passType;

			public string[] keywords;

			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeConditional("UNITY_EDITOR")]
			[FreeFunction]
			private static extern string CheckShaderVariant(Shader shader, PassType passType, string[] keywords);
		}

		public extern int shaderCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int variantCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isWarmedUp
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsWarmedUp")]
			get;
		}

		public ShaderVariantCollection()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool AddVariant(Shader shader, PassType passType, string[] keywords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool RemoveVariant(Shader shader, PassType passType, string[] keywords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ContainsVariant(Shader shader, PassType passType, string[] keywords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("ClearVariants")]
		public extern void Clear();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("WarmupShaders")]
		public extern void WarmUp();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("CreateFromScript")]
		private static extern void Internal_Create([Writable] ShaderVariantCollection svc);

		public bool Add(ShaderVariant variant)
		{
			return AddVariant(variant.shader, variant.passType, variant.keywords);
		}

		public bool Remove(ShaderVariant variant)
		{
			return RemoveVariant(variant.shader, variant.passType, variant.keywords);
		}

		public bool Contains(ShaderVariant variant)
		{
			return ContainsVariant(variant.shader, variant.passType, variant.keywords);
		}
	}
}
