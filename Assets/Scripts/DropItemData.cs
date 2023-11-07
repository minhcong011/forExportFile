using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DropItemData : MonoBehaviour
{
	

	public List<Data> dataList;

	public static DropItemData Instance;

	private void Awake()
	{
		Instance = this;
	}

	public Data GetDataForId(DropItemId _itemId)
	{
		
		for (int i = 0; i < dataList.Count; i++)
		{
			if(dataList[i].itemId == _itemId)
			return dataList[i];
		}

		return dataList[0];
		
	}


}


[System.Serializable]
	public class Data
	{
		public DropItemId itemId;

		public DropItem itemObject;

		public Sprite itemSprite;

		public int scoreForMerge;
	}