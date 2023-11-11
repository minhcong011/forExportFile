using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class ActionWood : MonoBehaviour
{
	// Token: 0x060000D4 RID: 212 RVA: 0x000077F7 File Offset: 0x000059F7
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.set_wood = GameObject.Find("set_wood");
		this.set_wood.SetActive(false);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00007828 File Offset: 0x00005A28
	private void OnMouseDown()
	{
		Debug.Log("CLICK");
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f && VariblesGlobal.SelectObject == "Wood")
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00007884 File Offset: 0x00005A84
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000078D1 File Offset: 0x00005AD1
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "";
		base.GetComponent<MeshRenderer>().enabled = false;
		this.set_wood.SetActive(true);
	}

	// Token: 0x04000143 RID: 323
	private GameObject Player;

	// Token: 0x04000144 RID: 324
	private GameObject set_wood;

	// Token: 0x04000145 RID: 325
	private float clickTime;
}
