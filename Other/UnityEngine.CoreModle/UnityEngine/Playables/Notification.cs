namespace UnityEngine.Playables
{
	public class Notification : INotification
	{
		public PropertyName id { get; }

		public Notification(string name)
		{
			id = new PropertyName(name);
		}
	}
}
