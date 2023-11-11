using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000047 RID: 71
public class KeyDark : MonoBehaviour
{
	// Token: 0x06000135 RID: 309 RVA: 0x000091AD File Offset: 0x000073AD
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000091BF File Offset: 0x000073BF
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000091F4 File Offset: 0x000073F4
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

	// Token: 0x06000138 RID: 312 RVA: 0x00009290 File Offset: 0x00007490
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "KeyDark";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000174 RID: 372
	private GameObject Player;

	// Token: 0x04000175 RID: 373
	private float clickTime;

	// Token: 0x04000176 RID: 374
	private bool ClickUI;
}
