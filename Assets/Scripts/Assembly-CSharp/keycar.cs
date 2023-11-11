using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class keycar : MonoBehaviour
{
	// Token: 0x06000130 RID: 304 RVA: 0x00009100 File Offset: 0x00007300
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00009112 File Offset: 0x00007312
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00009148 File Offset: 0x00007348
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00009195 File Offset: 0x00007395
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "keycar";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000172 RID: 370
	private GameObject Player;

	// Token: 0x04000173 RID: 371
	private float clickTime;
}
