using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Newtonsoft0.Json.Utilities
{
	internal class ListWrapper<T> : IList<T>, ICollection<T>, IEnumerable<T>, IWrappedList, IList, ICollection, IEnumerable
	{
		private readonly IList _list;

		private readonly IList<T> _genericList;

		private object _syncRoot;

		public T this[int index]
		{
			get
			{
				if (_genericList != null)
				{
					return _genericList[index];
				}
				return (T)_list[index];
			}
			set
			{
				if (_genericList != null)
				{
					_genericList[index] = value;
				}
				else
				{
					_list[index] = value;
				}
			}
		}

		public int Count
		{
			get
			{
				if (_genericList != null)
				{
					return _genericList.Count;
				}
				return _list.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (_genericList != null)
				{
					return _genericList.IsReadOnly;
				}
				return _list.IsReadOnly;
			}
		}

		bool IList.IsFixedSize => false;

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				VerifyValueType(value);
				this[index] = (T)value;
			}
		}

		bool ICollection.IsSynchronized => false;

		object ICollection.SyncRoot
		{
			get
			{
				if (_syncRoot == null)
				{
					Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}
				return _syncRoot;
			}
		}

		public object UnderlyingList
		{
			get
			{
				if (_genericList != null)
				{
					return _genericList;
				}
				return _list;
			}
		}

		public ListWrapper(IList list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			if (list is IList<T>)
			{
				_genericList = (IList<T>)list;
			}
			else
			{
				_list = list;
			}
		}

		public ListWrapper(IList<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			_genericList = list;
		}

		public int IndexOf(T item)
		{
			if (_genericList != null)
			{
				return _genericList.IndexOf(item);
			}
			return _list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			if (_genericList != null)
			{
				_genericList.Insert(index, item);
			}
			else
			{
				_list.Insert(index, item);
			}
		}

		public void RemoveAt(int index)
		{
			if (_genericList != null)
			{
				_genericList.RemoveAt(index);
			}
			else
			{
				_list.RemoveAt(index);
			}
		}

		public void Add(T item)
		{
			if (_genericList != null)
			{
				_genericList.Add(item);
			}
			else
			{
				_list.Add(item);
			}
		}

		public void Clear()
		{
			if (_genericList != null)
			{
				_genericList.Clear();
			}
			else
			{
				_list.Clear();
			}
		}

		public bool Contains(T item)
		{
			if (_genericList != null)
			{
				return _genericList.Contains(item);
			}
			return _list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (_genericList != null)
			{
				_genericList.CopyTo(array, arrayIndex);
			}
			else
			{
				_list.CopyTo(array, arrayIndex);
			}
		}

		public bool Remove(T item)
		{
			if (_genericList != null)
			{
				return _genericList.Remove(item);
			}
			bool flag = _list.Contains(item);
			if (flag)
			{
				_list.Remove(item);
			}
			return flag;
		}

		public IEnumerator<T> GetEnumerator()
		{
			if (_genericList != null)
			{
				return _genericList.GetEnumerator();
			}
			return _list.Cast<T>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (_genericList != null)
			{
				return _genericList.GetEnumerator();
			}
			return _list.GetEnumerator();
		}

		int IList.Add(object value)
		{
			VerifyValueType(value);
			Add((T)value);
			return Count - 1;
		}

		bool IList.Contains(object value)
		{
			if (IsCompatibleObject(value))
			{
				return Contains((T)value);
			}
			return false;
		}

		int IList.IndexOf(object value)
		{
			if (IsCompatibleObject(value))
			{
				return IndexOf((T)value);
			}
			return -1;
		}

		void IList.Insert(int index, object value)
		{
			VerifyValueType(value);
			Insert(index, (T)value);
		}

		void IList.Remove(object value)
		{
			if (IsCompatibleObject(value))
			{
				Remove((T)value);
			}
		}

		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			CopyTo((T[])array, arrayIndex);
		}

		private static void VerifyValueType(object value)
		{
			if (!IsCompatibleObject(value))
			{
				throw new ArgumentException("The value '{0}' is not of type '{1}' and cannot be used in this generic collection.".FormatWith(CultureInfo.InvariantCulture, value, typeof(T)), "value");
			}
		}

		private static bool IsCompatibleObject(object value)
		{
			if (!(value is T) && (value != null || (typeof(T).IsValueType && !ReflectionUtils.IsNullableType(typeof(T)))))
			{
				return false;
			}
			return true;
		}
	}
}
