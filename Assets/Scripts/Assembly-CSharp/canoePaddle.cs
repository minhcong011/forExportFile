using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class canoePaddle : MonoBehaviour
{
	// Token: 0x0600011C RID: 284 RVA: 0x00008D8D File Offset: 0x00006F8D
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00008D9F File Offset: 0x00006F9F
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00008DD4 File Offset: 0x00006FD4
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00008E21 File Offset: 0x00007021
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "canoePaddle";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000169 RID: 361
	private GameObject Player;

	// Token: 0x0400016A RID: 362
	private float clickTime;

	// Token: 0x0400016B RID: 363
	private int StateMessage;
}
