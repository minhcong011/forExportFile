using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class Axe : MonoBehaviour
{
	// Token: 0x0600010D RID: 269 RVA: 0x00008A0B File Offset: 0x00006C0B
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600010E RID: 270 RVA: 0x00008A1D File Offset: 0x00006C1D
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00008A51 File Offset: 0x00006C51
	private void OnMouseUp()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00008A80 File Offset: 0x00006C80
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Axe";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000161 RID: 353
	private GameObject Player;

	// Token: 0x04000162 RID: 354
	private float clickTime;
}
