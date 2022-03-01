using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	[RequiredByNativeCode]
	public interface INotificationReceiver
	{
		void OnNotify(Playable origin, INotification notification, object context);
	}
}
