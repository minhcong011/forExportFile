using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class Saw : MonoBehaviour
{
	// Token: 0x06000171 RID: 369 RVA: 0x00009DCE File Offset: 0x00007FCE
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00009DE0 File Offset: 0x00007FE0
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00009E14 File Offset: 0x00008014
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00009E61 File Offset: 0x00008061
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Saw";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000196 RID: 406
	private GameObject Player;

	// Token: 0x04000197 RID: 407
	private float clickTime;
}
