using System;
using System.Reflection;

namespace Newtonsoft0.Json.Utilities
{
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		public static readonly LateBoundReflectionDelegateFactory Instance = new LateBoundReflectionDelegateFactory();

		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (T o, object[] a) => c.Invoke(a);
			}
			return (T o, object[] a) => method.Invoke(o, a);
		}

		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			if (type.IsValueType)
			{
				return () => (T)ReflectionUtils.CreateInstance(type);
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, nonPublic: true);
			return () => (T)constructorInfo.Invoke(null);
		}

		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			return (T o) => propertyInfo.GetValue(o, null);
		}

		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			return (T o) => fieldInfo.GetValue(o);
		}

		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			return delegate(T o, object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			return delegate(T o, object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}
	}
}
