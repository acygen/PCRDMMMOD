namespace UnityEngine
{
	public class AndroidJavaProxy
	{
		public readonly AndroidJavaClass javaInterface;

		public AndroidJavaProxy(string javaInterface)
		{
		}

		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
		}

		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			return null;
		}

		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			return null;
		}

		public virtual bool equals(AndroidJavaObject obj)
		{
			return false;
		}

		public virtual int hashCode()
		{
			return 0;
		}

		public virtual string toString()
		{
			return "<c# proxy java object>";
		}
	}
}
