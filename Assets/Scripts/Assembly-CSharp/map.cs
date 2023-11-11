using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class map : MonoBehaviour
{
	// Token: 0x0600014E RID: 334 RVA: 0x00009698 File Offset: 0x00007898
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600014F RID: 335 RVA: 0x000096AA File Offset: 0x000078AA
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x000096E0 File Offset: 0x000078E0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000972D File Offset: 0x0000792D
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "map";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000183 RID: 387
	private GameObject Player;

	// Token: 0x04000184 RID: 388
	private float clickTime;
}
