// dnSpy decompiler from Assembly-CSharp.dll class: RateUsController
using System;
using UnityEngine;

public class RateUsController : MonoBehaviour
{
	private void Awake()
	{
		if (!RateUsController.isCreated)
		{
			Singleton<GameController>.Instance.rateUsController = this;
			RateUsController.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	public void showWindow(Transform parent)
	{
		this.triggerCount++;
		if (this.triggerCount <= this.showAfter)
		{
			return;
		}
		this.triggerCount = 0;
		this.showAfter++;
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("RateUsScreen")) as GameObject;
		gameObject.transform.SetParent(parent);
		gameObject.transform.SetAsLastSibling();
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
	}

	private static bool isCreated;

	public RateUsController.videoCallBack rewardedVideoCB;

	public bool isReadyToShow;

	public int showAfter = 3;

	public int triggerCount;

	public delegate void videoCallBack(int type);
}
