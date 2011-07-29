using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Collections
{
	/// <summary>
	/// 泛型列表
	/// </summary>
	public class IListT<T> : System.Collections.Generic.IList<T>
	{
		#region 装饰
		/// <summary>
		/// System.Collections.Generic.IList
		/// </summary>
		protected System.Collections.Generic.IList<T> list;
		/// <summary>
		/// 是否启用所有事件
		/// </summary>
		public bool MayAll = true;
		/// <summary>
		/// 装饰
		/// </summary>
		/// <param name="List"></param>
		public IListT(System.Collections.Generic.IList<T> List)
		{
			list = List;
		}
		#endregion

		#region ListChaned
		/// <summary>
		/// ListChanged 事件
		/// </summary>
		public event System.ComponentModel.ListChangedEventHandler ListChanged;
		/// <summary>
		/// 是否启用 ListChanged
		/// </summary>
		public bool MayListChanged = true;
		/// <summary>
		/// 引发 ListChanged
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
		{
			if (MayAll && MayListChanged)
			{
				if (ListChanged != null)
				{
					ListChanged(this, e);
				}
			}
		}
		#endregion

		#region 附加方法
		/// <summary>
		/// 返回列表中是否包含指定项(支持null)
		/// </summary>
		public static bool Contains(System.Collections.Generic.IList<T> list, T obj)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == null && obj == null || obj != null && obj.Equals(list[i]))
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 将某项添加到 System.Collections.Generic.IList 中，该项唯一。
		/// </summary>
		/// <returns>新项的插入位置。</returns>
		public void AddUnique(T value)
		{
			if (!this.Contains(value))
			{
				this.Add(value);
			}
		}
		#endregion

		#region IList<T> 成员

		public int IndexOf(T item)
		{
			return list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			list.Insert(index, item);
			System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemAdded, index);
			OnListChanged(e);
		}

		public void RemoveAt(int index)
		{
			T item = list[index];
			this.Remove(item);
		}

		public T this[int index]
		{
			get { return list[index]; }
			set
			{
				object old = list[index];
				list[index] = value;
				if (old == null)
				{
					old = value;
					value = default(T);
				}
				if (old != null && !old.Equals(value))
				{
					System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemChanged, index);
					OnListChanged(e);
				}
			}
		}

		#endregion

		#region ICollection<T> 成员

		public void Add(T item)
		{
			list.Add(item);
			System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemAdded, -1);
			OnListChanged(e);
		}

		public void Clear()
		{
			int nCount = list.Count;
			list.Clear();
			if (nCount > 0)
			{
				System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1);
				OnListChanged(e);
			}
		}

		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return list.Count; }
		}

		public bool IsReadOnly
		{
			get { return list.IsReadOnly; }
		}

		public bool Remove(T item)
		{
			int idx = list.IndexOf(item);
			bool rtn = list.Remove(item);
			if (rtn)
			{
				System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemDeleted, -1, idx);
				OnListChanged(e);
			}
			return rtn;
		}

		#endregion

		#region IEnumerable<T> 成员

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IEnumerable 成员

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion
	}
}
