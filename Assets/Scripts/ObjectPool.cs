// dnSpy decompiler from Assembly-CSharp.dll class: ObjectPool
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("ObjectPooling/ObjectPool")]
public class ObjectPool : MonoBehaviour
{
	public static ObjectPool instance { get; private set; }

	private void OnEnable()
	{
		ObjectPool.instance = this;
	}

	private void Start()
	{
		this.ContainerObject = new GameObject("ObjectPool");
		this.Pool = new List<GameObject>[this.Entries.Length];
		for (int i = 0; i < this.Entries.Length; i++)
		{
			ObjectPool.ObjectPoolEntry objectPoolEntry = this.Entries[i];
			this.Pool[i] = new List<GameObject>();
			for (int j = 0; j < objectPoolEntry.Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(objectPoolEntry.Prefab);
				gameObject.name = objectPoolEntry.Prefab.name;
				this.PoolObject(gameObject);
			}
		}
		MonoBehaviour.print("Pool count " + this.Pool[0].Count);
		base.Invoke("init", 2f);
	}

	private void init()
	{
		this.GetObjectForType("Camera", true);
	}

	public GameObject GetObjectForType(string objectType, bool onlyPooled)
	{
		for (int i = 0; i < this.Entries.Length; i++)
		{
			GameObject prefab = this.Entries[i].Prefab;
			if (!(prefab.name != objectType))
			{
				if (this.Pool[i].Count > 0)
				{
					GameObject gameObject = this.Pool[i][0];
					if (gameObject != null)
					{
						gameObject.transform.parent = null;
					}
					this.Pool[i].RemoveAt(0);
					return gameObject;
				}
				if (!onlyPooled)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.Entries[i].Prefab);
					gameObject2.name = objectType;
					return gameObject2;
				}
			}
		}
		return null;
	}

	public void PoolObject(GameObject obj)
	{
		for (int i = 0; i < this.Entries.Length; i++)
		{
			if (!(this.Entries[i].Prefab.name != obj.name))
			{
				obj.SetActive(false);
				obj.transform.parent = this.ContainerObject.transform;
				this.Pool[i].Add(obj);
				return;
			}
		}
	}

	public ObjectPool.ObjectPoolEntry[] Entries;

	[HideInInspector]
	public List<GameObject>[] Pool;

	protected GameObject ContainerObject;

	[Serializable]
	public class ObjectPoolEntry
	{
		[SerializeField]
		public GameObject Prefab;

		[SerializeField]
		public int Count;
	}
}
