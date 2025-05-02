// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.ObjectPoolBase`1
using System;
using System.Collections.Generic;

namespace ControlFreak2
{
	public abstract class ObjectPoolBase<T>
	{
		public ObjectPoolBase()
		{
			this.usedList = new List<T>();
			this.unusedList = new List<T>();
		}

		protected abstract T CreateInternalObject();

		protected virtual void OnDestroyObject(T obj)
		{
		}

		protected virtual void OnUseObject(T obj)
		{
		}

		protected virtual void OnUnuseObeject(T obj)
		{
		}

		public int GetAllocatedCount()
		{
			return this.usedList.Count + this.unusedList.Count;
		}

		public int GetUsedCount()
		{
			return this.usedList.Count;
		}

		public int GetUnusedCount()
		{
			return this.unusedList.Count;
		}

		public List<T> GetList()
		{
			return this.usedList;
		}

		public T GetNewObject(int insertPos = -1)
		{
			if (this.unusedList.Count == 0)
			{
				return default(T);
			}
			T t = this.unusedList[this.unusedList.Count - 1];
			this.unusedList.RemoveAt(this.unusedList.Count - 1);
			this.OnUseObject(t);
			if (insertPos < 0)
			{
				this.usedList.Add(t);
			}
			else
			{
				this.usedList.Insert(insertPos, t);
			}
			return t;
		}

		public void UnuseObject(T obj)
		{
			int num = this.usedList.IndexOf(obj);
			if (num < 0)
			{
				return;
			}
			this.usedList.RemoveAt(num);
			this.OnUnuseObeject(obj);
			this.unusedList.Add(obj);
		}

		public void Clear()
		{
			for (int i = 0; i < this.usedList.Count; i++)
			{
				this.OnUnuseObeject(this.usedList[i]);
			}
			this.unusedList.AddRange(this.usedList);
			this.usedList.Clear();
		}

		public void Trim(int maxCount, bool trimAtEnd = true)
		{
			if (maxCount < 0)
			{
				maxCount = 0;
			}
			int num = this.GetUsedCount() - maxCount;
			if (num <= 0)
			{
				return;
			}
			int num2 = (!trimAtEnd) ? 0 : (this.usedList.Count - num);
			int num3 = num2 + num;
			for (int i = num2; i < num3; i++)
			{
				T t = this.usedList[i];
				this.OnUnuseObeject(t);
				this.unusedList.Add(t);
			}
			this.usedList.RemoveRange(num2, num);
		}

		public void EnsureCapacity(int count)
		{
			if (this.GetAllocatedCount() < count)
			{
				this.Allocate(count);
			}
		}

		public void Allocate(int count)
		{
			if (count < 0)
			{
				count = 0;
			}
			if (count == this.GetAllocatedCount())
			{
				return;
			}
			if (count > this.GetAllocatedCount())
			{
				if (this.usedList.Capacity < count)
				{
					this.usedList.Capacity = count;
				}
				if (this.unusedList.Capacity < count)
				{
					this.unusedList.Capacity = count;
				}
				int num = count - this.GetAllocatedCount();
				for (int i = 0; i < num; i++)
				{
					T t = this.CreateInternalObject();
					if (t == null)
					{
						throw new Exception("Could not create a new object pool element [" + typeof(T).ToString() + "]!");
					}
					this.unusedList.Add(t);
				}
			}
			else
			{
				int num2 = -(count - this.unusedList.Count);
				if (num2 > 0)
				{
					int num3 = this.usedList.Count - num2;
					for (int j = num3; j < this.usedList.Count; j++)
					{
						T t2 = this.usedList[j];
						this.OnUnuseObeject(t2);
						this.unusedList.Add(t2);
					}
					this.usedList.RemoveRange(num3, num2);
				}
				int num4 = this.unusedList.Count - count;
				int num5 = this.unusedList.Count - num4;
				for (int k = num5; k < this.unusedList.Count; k++)
				{
					this.OnDestroyObject(this.unusedList[k]);
				}
				this.unusedList.RemoveRange(num5, num4);
			}
		}

		public void DestroyAll()
		{
			this.Allocate(0);
		}

		protected List<T> usedList;

		protected List<T> unusedList;
	}
}
