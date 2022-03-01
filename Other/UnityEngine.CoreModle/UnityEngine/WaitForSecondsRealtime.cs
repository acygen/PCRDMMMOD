namespace UnityEngine
{
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		private float m_WaitUntilTime = -1f;

		public float waitTime { get; set; }

		public override bool keepWaiting
		{
			get
			{
				if (m_WaitUntilTime < 0f)
				{
					m_WaitUntilTime = Time.realtimeSinceStartup + waitTime;
				}
				bool flag = Time.realtimeSinceStartup < m_WaitUntilTime;
				if (!flag)
				{
					m_WaitUntilTime = -1f;
				}
				return flag;
			}
		}

		public WaitForSecondsRealtime(float time)
		{
			waitTime = time;
		}
	}
}
