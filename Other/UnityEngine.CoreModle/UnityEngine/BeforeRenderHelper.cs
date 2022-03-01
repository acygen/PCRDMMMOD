using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	internal static class BeforeRenderHelper
	{
		private struct OrderBlock
		{
			internal int order;

			internal UnityAction callback;
		}

		private static List<OrderBlock> s_OrderBlocks = new List<OrderBlock>();

		private static int GetUpdateOrder(UnityAction callback)
		{
			object[] customAttributes = callback.Method.GetCustomAttributes(typeof(BeforeRenderOrderAttribute), inherit: true);
			return ((customAttributes == null || customAttributes.Length <= 0) ? null : (customAttributes[0] as BeforeRenderOrderAttribute))?.order ?? 0;
		}

		public static void RegisterCallback(UnityAction callback)
		{
			int updateOrder = GetUpdateOrder(callback);
			lock (s_OrderBlocks)
			{
				int i;
				for (i = 0; i < s_OrderBlocks.Count && s_OrderBlocks[i].order <= updateOrder; i++)
				{
					if (s_OrderBlocks[i].order == updateOrder)
					{
						OrderBlock value = s_OrderBlocks[i];
						value.callback = (UnityAction)Delegate.Combine(value.callback, callback);
						s_OrderBlocks[i] = value;
						return;
					}
				}
				OrderBlock item = default(OrderBlock);
				item.order = updateOrder;
				item.callback = (UnityAction)Delegate.Combine(item.callback, callback);
				s_OrderBlocks.Insert(i, item);
			}
		}

		public static void UnregisterCallback(UnityAction callback)
		{
			int updateOrder = GetUpdateOrder(callback);
			lock (s_OrderBlocks)
			{
				for (int i = 0; i < s_OrderBlocks.Count && s_OrderBlocks[i].order <= updateOrder; i++)
				{
					if (s_OrderBlocks[i].order == updateOrder)
					{
						OrderBlock value = s_OrderBlocks[i];
						value.callback = (UnityAction)Delegate.Remove(value.callback, callback);
						s_OrderBlocks[i] = value;
						if (value.callback == null)
						{
							s_OrderBlocks.RemoveAt(i);
						}
						break;
					}
				}
			}
		}

		public static void Invoke()
		{
			lock (s_OrderBlocks)
			{
				for (int i = 0; i < s_OrderBlocks.Count; i++)
				{
					s_OrderBlocks[i].callback?.Invoke();
				}
			}
		}
	}
}
