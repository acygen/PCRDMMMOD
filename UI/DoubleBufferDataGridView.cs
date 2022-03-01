using System.Reflection;
using System.Windows.Forms;

namespace SetBox
{
	public static class DoubleBufferDataGridView
	{
		public static void DoubleBufferedDataGirdView(this DataGridView dgv, bool flag)
		{
			dgv.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgv, flag, null);
		}
	}
}
