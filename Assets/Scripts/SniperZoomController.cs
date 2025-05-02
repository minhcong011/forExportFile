// dnSpy decompiler from Assembly-CSharp.dll class: SniperZoomController
using System;
using System.Collections;
using UnityEngine;

public class SniperZoomController : MonoBehaviour
{
	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		this.ShowObjects(true);
	}

	private void ShowObjects(bool val)
	{
		for (int i = 0; i < this.objects.Length; i++)
		{
			this.objects[i].SetActive(val);
		}
	}

	public void ShowAim(bool val)
	{
		base.StartCoroutine(this.showWithDelay(val));
	}

	private IEnumerator showWithDelay(bool val)
	{
		if (val)
		{
			yield return new WaitForSeconds(0.15f);
		}
		this.ShowObjects(!val);
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	public void SetZoomVal(float val)
	{
		base.gameObject.BroadcastMessage("SetZoomSlider", val, SendMessageOptions.DontRequireReceiver);
	}

	public GameObject[] objects;
}
