// dnSpy decompiler from Assembly-CSharp.dll class: UKMap
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UKMap
{
	public UKMap()
	{
	}

	public UKMap(UKMap baseEntity)
	{
		foreach (string key in baseEntity.Properties)
		{
			this.SetProperty<object>(key, baseEntity.GetProperty<object>(key));
		}
	}

	public UKMap(IDictionary<string, object> obj)
	{
		foreach (string key in obj.Keys)
		{
			this.SetProperty<object>(key, obj[key]);
		}
	}

	public UKMap(Hashtable obj)
	{
		if (obj == null)
		{
			return;
		}
		IEnumerator enumerator = obj.Keys.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj2 = enumerator.Current;
				object obj3 = obj[obj2];
				if (obj3 is Hashtable)
				{
					obj3 = new UKMap(obj3 as Hashtable);
				}
				else if (obj3 is IList)
				{
					IList list = obj3 as IList;
					if (list.Count == 0)
					{
						continue;
					}
					object obj4 = list[0];
					if (obj4 is Hashtable)
					{
						IList<UKMap> list2 = new List<UKMap>();
						for (int i = 0; i < list.Count; i++)
						{
							object obj5 = list[i];
							if (obj5 is Hashtable)
							{
								UKMap item = new UKMap(obj5 as Hashtable);
								list2.Add(item);
							}
							else
							{
								list2.Add(null);
							}
						}
						if (list2.Count > 0)
						{
							obj3 = list2;
						}
					}
					else
					{
						IList list3 = new List<object>();
						for (int j = 0; j < list.Count; j++)
						{
							object obj6 = list[j];
							if (obj6 is Hashtable)
							{
								UKMap value = new UKMap(obj6 as Hashtable);
								list3.Add(value);
							}
							else
							{
								list3.Add(obj6);
							}
						}
						if (list3.Count > 0)
						{
							obj3 = list3;
						}
					}
				}
				this.SetProperty<object>((string)obj2, obj3);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public int count
	{
		get
		{
			return this.m_properties.Count;
		}
	}

	public bool DeleteValueForKey(string key)
	{
		if (this.m_properties.ContainsKey(key))
		{
			this.m_properties.Remove(key);
			return true;
		}
		return false;
	}

	public ICollection<string> Properties
	{
		get
		{
			return this.m_properties.Keys;
		}
	}

	public UKMap this[string key]
	{
		get
		{
			return this.GetProperty(key) as UKMap;
		}
		set
		{
			this.m_properties[key] = value;
		}
	}

	public virtual bool ContainsProperty(string key)
	{
		return this.m_properties.ContainsKey(key);
	}

	public virtual IList<T> GetList<T>(string key)
	{
		return this.GetProperty(key) as IList<T>;
	}

	public virtual IList GetIList(string key)
	{
		return this.GetProperty(key) as IList;
	}

	public virtual T GetProperty<T>(string key)
	{
		T result = default(T);
		if (this.m_properties.ContainsKey(key))
		{
			result = (T)((object)this.m_properties[key]);
		}
		return result;
	}

	public virtual double GetDoubleValueForKey(string key, double err)
	{
		if (this.GetProperty(key) == null)
		{
			return err;
		}
		return Convert.ToDouble(this.GetProperty(key));
	}

	public virtual long GetLongValueForKey(string key, int err)
	{
		if (this.GetProperty(key) == null)
		{
			return (long)err;
		}
		return Convert.ToInt64(this.GetProperty(key));
	}

	public virtual bool GetBoolValueForKey(string key, bool err = false)
	{
		object property = this.GetProperty(key);
		if (property == null)
		{
			return err;
		}
		string text = property.ToString();
		return text.Equals("true") || text.Equals("1") || text.Equals("yes") || Convert.ToBoolean(this.GetProperty(key));
	}

	public virtual T GetValueForKey<T>(string key)
	{
		return (T)((object)this.GetProperty(key));
	}

	public virtual int GetIntValueForKey(string key, int err)
	{
		object property = this.GetProperty<object>(key);
		if (property == null)
		{
			return err;
		}
		int result;
		try
		{
			result = Convert.ToInt32(property);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Exception while converting to int: " + ex.Message);
			result = err;
		}
		return result;
	}

	public virtual float GetFloatValueForKey(string key, int err)
	{
		if (this.GetProperty<object>(key) == null)
		{
			return (float)err;
		}
		return this.GetProperty<float>(key);
	}

	public virtual string GetStringValueForKey(string key, string err)
	{
		string property = this.GetProperty<string>(key);
		if (string.IsNullOrEmpty(property))
		{
			return err;
		}
		return property;
	}

	public virtual object GetProperty(string key)
	{
		object result = null;
		if (this.m_properties.ContainsKey(key))
		{
			result = this.m_properties[key];
		}
		return result;
	}

	public virtual void SetProperty<T>(string key, T val)
	{
		if (this.m_properties.ContainsKey(key))
		{
			this.m_properties[key] = val;
		}
		else
		{
			this.m_properties.Add(key, val);
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("{");
		foreach (string text in this.m_properties.Keys)
		{
			object obj = this.m_properties[text];
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append("\"");
			stringBuilder.Append(text);
			stringBuilder.Append("\":");
			if (obj is string)
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(obj);
				stringBuilder.Append("\"");
			}
			else if (obj is int || obj is double || obj is long || obj is float)
			{
				stringBuilder.Append(obj);
			}
			else if (obj is IEnumerable)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				IEnumerator enumerator2 = (obj as IEnumerable).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						if (stringBuilder2.Length > 0)
						{
							stringBuilder2.Append(",");
						}
						if (obj2 is UKMap)
						{
							stringBuilder2.Append(obj2);
						}
						else
						{
							stringBuilder2.Append("\"");
							stringBuilder2.Append(obj2);
							stringBuilder2.Append("\"");
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator2 as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				stringBuilder.Append("[");
				stringBuilder.Append(stringBuilder2);
				stringBuilder.Append("]");
			}
			else if (obj is UKMap)
			{
				stringBuilder.Append(obj);
			}
			else
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(obj);
				stringBuilder.Append("\"");
			}
		}
		stringBuilder.Append("}");
		return stringBuilder.ToString();
	}

	public List<string> GetAllKeysN()
	{
		return new List<string>(this.m_properties.Keys);
	}

	public IDictionary<string, object> GetDictionary()
	{
		return this.m_properties;
	}

	public List<object> GetAllValuesN()
	{
		return new List<object>(this.m_properties.Values);
	}

	protected IDictionary<string, object> m_properties = new Dictionary<string, object>();
}
