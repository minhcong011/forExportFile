using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200004A RID: 74
public class Knife : MonoBehaviour
{
	// Token: 0x06000144 RID: 324 RVA: 0x000094A0 File Offset: 0x000076A0
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000145 RID: 325 RVA: 0x000094B2 File Offset: 0x000076B2
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x000094E8 File Offset: 0x000076E8
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

	// Token: 0x06000147 RID: 327 RVA: 0x00009584 File Offset: 0x00007784
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Knife";
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400017D RID: 381
	private GameObject Player;

	// Token: 0x0400017E RID: 382
	private float clickTime;

	// Token: 0x0400017F RID: 383
	private bool ClickUI;
}
