// dnSpy decompiler from Assembly-CSharp.dll class: AnimatedText
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
	private void OnEnable()
	{
		base.Invoke("DisableNow", 0.8f);
		Vector3 endValue = base.gameObject.GetComponent<RectTransform>().anchoredPosition;
		endValue.y += 80f;
		base.gameObject.GetComponent<RectTransform>().DOLocalMove(endValue, 0.8f, false).SetEase(Ease.Linear);
	}

	private void Start()
	{
	}

	private void DisableNow()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void setText(string s)
	{
		this.desc.text = s;
	}

	private void Update()
	{
	}

	public Text desc;
}
