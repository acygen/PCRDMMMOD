using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	[NativeType("Runtime/Graphics/ScriptableRenderLoop/ScriptableRenderContext.h")]
	public class RenderPassAttachment : Object
	{
		public extern RenderBufferLoadAction loadAction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			private set;
		}

		public extern RenderBufferStoreAction storeAction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			private set;
		}

		public extern RenderTextureFormat format
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			private set;
		}

		private RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				get_loadStoreTarget_Injected(out var ret);
				return ret;
			}
			set
			{
				set_loadStoreTarget_Injected(ref value);
			}
		}

		private RenderTargetIdentifier resolveTarget
		{
			get
			{
				get_resolveTarget_Injected(out var ret);
				return ret;
			}
			set
			{
				set_resolveTarget_Injected(ref value);
			}
		}

		public Color clearColor
		{
			get
			{
				get_clearColor_Injected(out var ret);
				return ret;
			}
			private set
			{
				set_clearColor_Injected(ref value);
			}
		}

		public extern float clearDepth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			private set;
		}

		public extern uint clearStencil
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			private set;
		}

		public RenderPassAttachment(RenderTextureFormat fmt)
		{
			Internal_CreateAttachment(this);
			loadAction = RenderBufferLoadAction.DontCare;
			storeAction = RenderBufferStoreAction.DontCare;
			format = fmt;
			loadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			resolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			clearColor = new Color(0f, 0f, 0f, 0f);
			clearDepth = 1f;
		}

		public void BindSurface(RenderTargetIdentifier tgt, bool loadExistingContents, bool storeResults)
		{
			loadStoreTarget = tgt;
			if (loadExistingContents && loadAction != RenderBufferLoadAction.Clear)
			{
				loadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				if (storeAction == RenderBufferStoreAction.StoreAndResolve || storeAction == RenderBufferStoreAction.Resolve)
				{
					storeAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					storeAction = RenderBufferStoreAction.Store;
				}
			}
		}

		public void BindResolveSurface(RenderTargetIdentifier tgt)
		{
			resolveTarget = tgt;
			if (storeAction == RenderBufferStoreAction.StoreAndResolve || storeAction == RenderBufferStoreAction.Store)
			{
				storeAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				storeAction = RenderBufferStoreAction.Resolve;
			}
		}

		public void Clear(Color clearCol, float clearDep = 1f, uint clearStenc = 0u)
		{
			clearColor = clearCol;
			clearDepth = clearDep;
			clearStencil = clearStenc;
			loadAction = RenderBufferLoadAction.Clear;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "RenderPassAttachment::Internal_CreateAttachment", IsFreeFunction = true)]
		public static extern void Internal_CreateAttachment([Writable] RenderPassAttachment self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_loadStoreTarget_Injected(out RenderTargetIdentifier ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_loadStoreTarget_Injected(ref RenderTargetIdentifier value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_resolveTarget_Injected(out RenderTargetIdentifier ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_resolveTarget_Injected(ref RenderTargetIdentifier value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_clearColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_clearColor_Injected(ref Color value);
	}
}
