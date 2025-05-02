// dnSpy decompiler from Assembly-CSharp.dll class: ObjectEnabler
using System;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (this.isCrossed)
		{
			return;
		}
		if (collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<PlayerEventListener>() != null)
		{
			for (int i = 0; i < this.objToShow.Length; i++)
			{
				this.objToShow[i].SetActive(true);
			}
			this.isCrossed = true;
		}
	}

	public bool isCrossed;

	public GameObject[] objToShow;
}
