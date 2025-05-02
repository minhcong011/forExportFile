// dnSpy decompiler from Assembly-CSharp.dll class: SingletonManager
using System;
using UnityEngine;

public class SingletonManager
{
	public static GameObject gameObject
	{
		get
		{
			if (SingletonManager._gameObject == null)
			{
				SingletonManager._gameObject = new GameObject("-SingletonManager");
				UnityEngine.Object.DontDestroyOnLoad(SingletonManager._gameObject);
			}
			return SingletonManager._gameObject;
		}
	}

	private static GameObject _gameObject;
}
