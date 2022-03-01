using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace Newtonsoft0.Json.Utilities
{
	internal class DynamicReflectionDelegateFactory : ReflectionDelegateFactory
	{
		public static DynamicReflectionDelegateFactory Instance = new DynamicReflectionDelegateFactory();

		private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
		{
			return (!owner.IsInterface) ? new DynamicMethod(name, returnType, parameterTypes, owner, skipVisibility: true) : new DynamicMethod(name, returnType, parameterTypes, (Module)null, skipVisibility: true);
		}

		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			DynamicMethod dynamicMethod = CreateDynamicMethod(method.ToString(), typeof(object), new Type[2]
			{
				typeof(object),
				typeof(object[])
			}, method.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			ParameterInfo[] parameters = method.GetParameters();
			Label label = iLGenerator.DefineLabel();
			iLGenerator.Emit(OpCodes.Ldarg_1);
			iLGenerator.Emit(OpCodes.Ldlen);
			iLGenerator.Emit(OpCodes.Ldc_I4, parameters.Length);
			iLGenerator.Emit(OpCodes.Beq, label);
			iLGenerator.Emit(OpCodes.Newobj, typeof(TargetParameterCountException).GetConstructor(Type.EmptyTypes));
			iLGenerator.Emit(OpCodes.Throw);
			iLGenerator.MarkLabel(label);
			if (!method.IsConstructor && !method.IsStatic)
			{
				iLGenerator.PushInstance(method.DeclaringType);
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				iLGenerator.Emit(OpCodes.Ldarg_1);
				iLGenerator.Emit(OpCodes.Ldc_I4, i);
				iLGenerator.Emit(OpCodes.Ldelem_Ref);
				iLGenerator.UnboxIfNeeded(parameters[i].ParameterType);
			}
			if (method.IsConstructor)
			{
				iLGenerator.Emit(OpCodes.Newobj, (ConstructorInfo)method);
			}
			else if (method.IsFinal || !method.IsVirtual)
			{
				iLGenerator.CallMethod((MethodInfo)method);
			}
			Type type = (method.IsConstructor ? method.DeclaringType : ((MethodInfo)method).ReturnType);
			if (type != typeof(void))
			{
				iLGenerator.BoxIfNeeded(type);
			}
			else
			{
				iLGenerator.Emit(OpCodes.Ldnull);
			}
			iLGenerator.Return();
			return (MethodCall<T, object>)dynamicMethod.CreateDelegate(typeof(MethodCall<T, object>));
		}

		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			DynamicMethod dynamicMethod = CreateDynamicMethod("Create" + type.FullName, typeof(object), Type.EmptyTypes, type);
			dynamicMethod.InitLocals = true;
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			if (type.IsValueType)
			{
				iLGenerator.DeclareLocal(type);
				iLGenerator.Emit(OpCodes.Ldloc_0);
				iLGenerator.Emit(OpCodes.Box, type);
			}
			else
			{
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
				if (constructor == null)
				{
					throw new Exception("Could not get constructor for {0}.".FormatWith(CultureInfo.InvariantCulture, type));
				}
				iLGenerator.Emit(OpCodes.Newobj, constructor);
			}
			iLGenerator.Return();
			return (Func<T>)dynamicMethod.CreateDelegate(typeof(Func<T>));
		}

		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			MethodInfo getMethod = propertyInfo.GetGetMethod(nonPublic: true);
			if (getMethod == null)
			{
				throw new Exception("Property '{0}' does not have a getter.".FormatWith(CultureInfo.InvariantCulture, propertyInfo.Name));
			}
			DynamicMethod dynamicMethod = CreateDynamicMethod("Get" + propertyInfo.Name, typeof(T), new Type[1]
			{
				typeof(object)
			}, propertyInfo.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			if (!getMethod.IsStatic)
			{
				iLGenerator.PushInstance(propertyInfo.DeclaringType);
			}
			iLGenerator.CallMethod(getMethod);
			iLGenerator.BoxIfNeeded(propertyInfo.PropertyType);
			iLGenerator.Return();
			return (Func<T, object>)dynamicMethod.CreateDelegate(typeof(Func<T, object>));
		}

		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			DynamicMethod dynamicMethod = CreateDynamicMethod("Get" + fieldInfo.Name, typeof(T), new Type[1]
			{
				typeof(object)
			}, fieldInfo.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			if (!fieldInfo.IsStatic)
			{
				iLGenerator.PushInstance(fieldInfo.DeclaringType);
			}
			iLGenerator.Emit(OpCodes.Ldfld, fieldInfo);
			iLGenerator.BoxIfNeeded(fieldInfo.FieldType);
			iLGenerator.Return();
			return (Func<T, object>)dynamicMethod.CreateDelegate(typeof(Func<T, object>));
		}

		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			DynamicMethod dynamicMethod = CreateDynamicMethod("Set" + fieldInfo.Name, null, new Type[2]
			{
				typeof(object),
				typeof(object)
			}, fieldInfo.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			if (!fieldInfo.IsStatic)
			{
				iLGenerator.PushInstance(fieldInfo.DeclaringType);
			}
			iLGenerator.Emit(OpCodes.Ldarg_1);
			iLGenerator.UnboxIfNeeded(fieldInfo.FieldType);
			iLGenerator.Emit(OpCodes.Stfld, fieldInfo);
			iLGenerator.Return();
			return (Action<T, object>)dynamicMethod.CreateDelegate(typeof(Action<T, object>));
		}

		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod(nonPublic: true);
			DynamicMethod dynamicMethod = CreateDynamicMethod("Set" + propertyInfo.Name, null, new Type[2]
			{
				typeof(object),
				typeof(object)
			}, propertyInfo.DeclaringType);
			ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
			if (!setMethod.IsStatic)
			{
				iLGenerator.PushInstance(propertyInfo.DeclaringType);
			}
			iLGenerator.Emit(OpCodes.Ldarg_1);
			iLGenerator.UnboxIfNeeded(propertyInfo.PropertyType);
			iLGenerator.CallMethod(setMethod);
			iLGenerator.Return();
			return (Action<T, object>)dynamicMethod.CreateDelegate(typeof(Action<T, object>));
		}
	}
}
