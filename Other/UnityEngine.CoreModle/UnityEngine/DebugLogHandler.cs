using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Debug.bindings.h")]
	internal sealed class DebugLogHandler : ILogHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadAndSerializationSafe]
		internal static extern void Internal_Log(LogType level, string msg, Object obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadAndSerializationSafe]
		internal static extern void Internal_LogException(Exception exception, Object obj);

		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			Internal_Log(logType, string.Format(format, args), context);
		}

		public void LogException(Exception exception, Object context)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Internal_LogException(exception, context);
		}
	}
}
