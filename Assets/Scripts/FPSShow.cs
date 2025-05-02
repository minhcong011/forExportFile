// dnSpy decompiler from Assembly-CSharp.dll class: FPSShow
using System;
using UnityEngine;
using UnityEngine.UI;

public class FPSShow : MonoBehaviour
{
	private void Update()
	{
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		float num = this.deltaTime * 1000f;
		float num2 = 1f / this.deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", num, num2);
		this.textLbl.text = text;
	}

	public Text textLbl;

	private float deltaTime;
}
