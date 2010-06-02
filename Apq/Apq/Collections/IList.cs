using System;
using System.Collections.Generic;
using System.Text;

namespace Apq.Collections
{
	/// <summary>
	/// 非泛型集合
	/// </summary>
	public class IList : System.Collections.IList
	{
		#region 装饰
		/// <summary>
		/// System.Collections.IList
		/// </summary>
		protected System.Collections.IList list;
		/// <summary>
		/// 是否启用所有事件
		/// </summary>
		public bool MayAll = true;
		/// <summary>
		/// 装饰
		/// </summary>
		/// <param name="List"></param>
		public IList(System.Collections.IList List)
		{
			list = List;
		}
		#endregion

		#region IList
		/// <summary>
		/// 获取一个值，该值指示 System.Collections.IList 是否具有固定大小。
		/// </summary>
		public bool IsFixedSize { get { return list.IsFixedSize; } }
		/// <summary>
		/// 获取一个值，该值指示 System.Collections.IList 是否为只读。
		/// </summary>
		public bool IsReadOnly { get { return list.IsReadOnly; } }

		/// <summary>
		/// 获取或设置指定索引处的元素。
		/// </summary>
		/// <param name="index">要获得或设置的元素从零开始的索引。</param>
		/// <returns>指定索引处的元素。</returns>
		public object this[int index]
		{
			get { return list[index]; }
			set
			{
				object old = list[index];
				list[index] = value;
				if (old == null)
				{
					old = value;
					value = null;
				}
				if (old != null && !old.Equals(value))
				{
					System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemChanged, index);
					OnListChanged(e);
				}
			}
		}

		/// <summary>
		/// 将某项添加到 System.Collections.IList 中。
		/// </summary>
		/// <param name="value">要添加到 System.Collections.IList 的 System.Object。</param>
		/// <returns>新元素的插入位置。</returns>
		public int Add(object value)
		{
			int index = list.Add(value);
			System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemAdded, index);
			OnListChanged(e);
			return index;
		}
		/// <summary>
		/// 从 System.Collections.IList 中移除所有项。
		/// </summary>
		public void Clear()
		{
			list.Clear();
			System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1);
			OnListChanged(e);
		}
		/// <summary>
		/// 确定 System.Collections.IList 是否包含特定值。
		/// </summary>
		/// <param name="value">要在 System.Collections.IList 中查找的 System.Object。</param>
		/// <returns>如果在 System.Collections.IList 中找到 System.Object，则为 true；否则为 false。</returns>
		public bool Contains(object value)
		{
			return list.Contains(value);
		}
		/// <summary>
		/// 确定 System.Collections.IList 中特定项的索引。
		/// </summary>
		/// <param name="value">要在 System.Collections.IList 中查找的 System.Object。</param>
		/// <returns>如果在列表中找到，则为 value 的索引；否则为 -1。</returns>
		public int IndexOf(object value)
		{
			return list.IndexOf(value);
		}
		/// <summary>
		/// 将一个项插入指定索引处的 System.Collections.IList。
		/// </summary>
		/// <param name="index">从零开始的索引，应在该位置插入 value。</param>
		/// <param name="value">要插入 System.Collections.IList 中的 System.Object。</param>
		public void Insert(int index, object value)
		{
			list.Insert(index, value);
			System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemAdded, index);
			OnListChanged(e);
		}
		/// <summary>
		/// 从 System.Collections.IList 中移除特定对象的第一个匹配项。
		/// </summary>
		/// <param name="value">要从 System.Collections.IList 移除的 System.Object。</param>
		public void Remove(object value)
		{
			int index = list.IndexOf(value);
			RemoveAt(index);
		}
		/// <summary>
		/// 移除指定索引处的 System.Collections.IList 项。
		/// </summary>
		/// <param name="index">从零开始的索引（属于要移除的项）。</param>
		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
			System.ComponentModel.ListChangedEventArgs e = new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemDeleted, -1, index);
			OnListChanged(e);
		}
		#endregion

		#region ICollection
		/// <summary>
		/// 获取 System.Collections.ICollection 中包含的元素数。
		/// </summary>
		public int Count { get { return list.Count; } }
		/// <summary>
		/// 获取一个值，该值指示是否同步对 System.Collections.ICollection 的访问（线程安全）。
		/// </summary>
		public bool IsSynchronized { get { return list.IsSynchronized; } }
		/// <summary>
		/// 获取可用于同步 System.Collections.ICollection 访问的对象。
		/// </summary>
		public object SyncRoot { get { return list.SyncRoot; } }

		/// <summary>
		/// 从特定的 System.Array 索引处开始，将 System.Collections.ICollection 的元素复制到一个 System.Array 中。
		/// </summary>
		/// <param name="array">作为从 System.Collections.ICollection 复制的元素的目标位置的一维 System.Array。System.Array 必须具有从零开始的索引。</param>
		/// <param name="index">array 中从零开始的索引，从此处开始复制。</param>
		public void CopyTo(System.Array array, int index)
		{
			list.CopyTo(array, index);
		}
		#endregion

		#region IEnumerable
		/// <summary>
		/// 返回一个循环访问集合的枚举数。
		/// </summary>
		/// <returns>可用于循环访问集合的 System.Collections.IEnumerator 对象。</returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
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
		/// 
		/// </summary>
		/// <param name="list"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool Contains(System.Collections.IList list, object obj)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == null && obj == null || list[i] != null && obj.Equals(list[i]))
				{
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
