// dnSpy decompiler from Assembly-CSharp.dll class: Singleton`1
using System;
using System.Collections;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class
{
	public Singleton()
	{
		Singleton<T>._instance = (this as T);
	}

	public static T Instance
	{
		get
		{
			if (Singleton<T>._instance == null)
			{
				Singleton<T>._instance = (SingletonManager.gameObject.AddComponent(typeof(T)) as T);
			}
			return Singleton<T>._instance;
		}
	}

	public static void Instantiate()
	{
		Singleton<T>._instance = Singleton<T>.Instance;
	}

	public void ExecuteAfterCoroutine(IEnumerator coroutine, Action action)
	{
		base.StartCoroutine(this.ExecuteAfterCoroutineActual(coroutine, action));
	}

	public IEnumerator ExecuteAfterCoroutineActual(IEnumerator coroutine, Action action)
	{
		yield return base.StartCoroutine(coroutine);
		action();
		yield break;
	}

	protected static T _instance;
}
