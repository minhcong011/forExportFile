using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000048 RID: 72
public class KeyGreen : MonoBehaviour
{
	// Token: 0x0600013A RID: 314 RVA: 0x000092A8 File Offset: 0x000074A8
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000092BA File Offset: 0x000074BA
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000092F0 File Offset: 0x000074F0
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

	// Token: 0x0600013D RID: 317 RVA: 0x0000938C File Offset: 0x0000758C
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "KeyGreen";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000177 RID: 375
	private GameObject Player;

	// Token: 0x04000178 RID: 376
	private float clickTime;

	// Token: 0x04000179 RID: 377
	private bool ClickUI;
}
