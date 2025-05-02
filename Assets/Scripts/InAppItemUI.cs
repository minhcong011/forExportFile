// dnSpy decompiler from Assembly-CSharp.dll class: InAppItemUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class InAppItemUI : MonoBehaviour
{
	private void Start()
	{
	}

	public void setInitials(int p, int c)
	{
		this.price.text = (float)p + 0.99f + " $";
		this.count.text = c.ToString();
	}

	public Text price;

	public Text count;
}
