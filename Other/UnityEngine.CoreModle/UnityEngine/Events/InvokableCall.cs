using System;
using System.Reflection;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	internal class InvokableCall : BaseInvokableCall
	{
		private event UnityAction Delegate;

		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			Delegate += (UnityAction)NetFxCoreExtensions.CreateDelegate(theFunction, typeof(UnityAction), target);
		}

		public InvokableCall(UnityAction action)
		{
			Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate();
			}
		}

		public void Invoke()
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate();
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && NetFxCoreExtensions.GetMethodInfo(this.Delegate).Equals(method);
		}
	}
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		protected event UnityAction<T1> Delegate;

		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			Delegate += (UnityAction<T1>)NetFxCoreExtensions.CreateDelegate(theFunction, typeof(UnityAction<T1>), target);
		}

		public InvokableCall(UnityAction<T1> action)
		{
			Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			if (args.Length != 1)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)args[0]);
			}
		}

		public virtual void Invoke(T1 args0)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate(args0);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && NetFxCoreExtensions.GetMethodInfo(this.Delegate).Equals(method);
		}
	}
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		protected event UnityAction<T1, T2> Delegate;

		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)NetFxCoreExtensions.CreateDelegate(theFunction, typeof(UnityAction<T1, T2>), target);
		}

		public InvokableCall(UnityAction<T1, T2> action)
		{
			Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			if (args.Length != 2)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)args[0], (T2)args[1]);
			}
		}

		public void Invoke(T1 args0, T2 args1)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate(args0, args1);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && NetFxCoreExtensions.GetMethodInfo(this.Delegate).Equals(method);
		}
	}
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		protected event UnityAction<T1, T2, T3> Delegate;

		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)NetFxCoreExtensions.CreateDelegate(theFunction, typeof(UnityAction<T1, T2, T3>), target);
		}

		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			if (args.Length != 3)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)args[0], (T2)args[1], (T3)args[2]);
			}
		}

		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate(args0, args1, args2);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && NetFxCoreExtensions.GetMethodInfo(this.Delegate).Equals(method);
		}
	}
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)NetFxCoreExtensions.CreateDelegate(theFunction, typeof(UnityAction<T1, T2, T3, T4>), target);
		}

		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			Delegate += action;
		}

		public override void Invoke(object[] args)
		{
			if (args.Length != 4)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);
			}
		}

		public void Invoke(T1 args0, T2 args1, T3 args2, T4 args3)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate(args0, args1, args2, args3);
			}
		}

		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && NetFxCoreExtensions.GetMethodInfo(this.Delegate).Equals(method);
		}
	}
}
