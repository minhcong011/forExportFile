using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200005E RID: 94
public class wrench01 : MonoBehaviour
{
	// Token: 0x060001A8 RID: 424 RVA: 0x0000AA7D File Offset: 0x00008C7D
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000AA8F File Offset: 0x00008C8F
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000AAC4 File Offset: 0x00008CC4
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

	// Token: 0x060001AB RID: 427 RVA: 0x0000AB60 File Offset: 0x00008D60
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "wrench01";
		base.gameObject.SetActive(false);
	}

	// Token: 0x040001AF RID: 431
	private GameObject Player;

	// Token: 0x040001B0 RID: 432
	private float clickTime;

	// Token: 0x040001B1 RID: 433
	private bool ClickUI;
}
