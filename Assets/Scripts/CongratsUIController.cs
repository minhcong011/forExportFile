// dnSpy decompiler from Assembly-CSharp.dll class: CongratsUIController
using System;
using UnityEngine;
using UnityEngine.UI;

public class CongratsUIController : MonoBehaviour
{
	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void OK()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void SetData(string tit, string des)
	{
		this.title.text = tit;
		this.desc.text = des;
	}

	public Text title;

	public Text desc;

	public Text reward;

	public RectTransform root;

	public RectTransform bottomBar;

	public RectTransform congratsBar;

	private Vector3 rootDefaultPos;

	private Vector3 congratsBarDefaultPos;

	private Vector3 bottomBarDefaultPos;
}
