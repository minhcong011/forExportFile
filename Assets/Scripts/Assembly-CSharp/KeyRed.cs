using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000049 RID: 73
public class KeyRed : MonoBehaviour
{
	// Token: 0x0600013F RID: 319 RVA: 0x000093A4 File Offset: 0x000075A4
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000093B6 File Offset: 0x000075B6
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000093EC File Offset: 0x000075EC
	private void OnMouseUp()
	{
		this.ClickUI = false;
		if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "IcoDropDown")
		{
			this.ClickUI = true;
		}
		if (Time.time - this.clickTime <= 0.3f && !this.ClickUI && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00009488 File Offset: 0x00007688
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "KeyRed";
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400017A RID: 378
	private GameObject Player;

	// Token: 0x0400017B RID: 379
	private float clickTime;

	// Token: 0x0400017C RID: 380
	private bool ClickUI;
}
