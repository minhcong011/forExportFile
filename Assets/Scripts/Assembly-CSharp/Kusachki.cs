using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200004B RID: 75
public class Kusachki : MonoBehaviour
{
	// Token: 0x06000149 RID: 329 RVA: 0x0000959C File Offset: 0x0000779C
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000095AE File Offset: 0x000077AE
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000095E4 File Offset: 0x000077E4
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

	// Token: 0x0600014C RID: 332 RVA: 0x00009680 File Offset: 0x00007880
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Kusachki";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000180 RID: 384
	private GameObject Player;

	// Token: 0x04000181 RID: 385
	private float clickTime;

	// Token: 0x04000182 RID: 386
	private bool ClickUI;
}
