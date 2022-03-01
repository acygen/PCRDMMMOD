using System.Collections;

namespace UnityEngine
{
	public abstract class CustomYieldInstruction : IEnumerator
	{
		public abstract bool keepWaiting { get; }

		public object Current => null;

		public bool MoveNext()
		{
			return keepWaiting;
		}

		public void Reset()
		{
		}
	}
}
