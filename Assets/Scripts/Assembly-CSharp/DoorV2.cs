using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class DoorV2 : MonoBehaviour
{
	// Token: 0x060000BE RID: 190 RVA: 0x00006F66 File Offset: 0x00005166
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.AngleX = base.transform.eulerAngles.x;
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00006F90 File Offset: 0x00005190
	private void OnMouseDown()
	{
		Debug.Log("Click");
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f && !this.Lock)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00006FE4 File Offset: 0x000051E4
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && !this.Lock && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x00007039 File Offset: 0x00005239
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bot")
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x00007058 File Offset: 0x00005258
	private void OpenClose()
	{
		this.Open = !this.Open;
		if (this.Open)
		{
			base.transform.eulerAngles = new Vector3(this.AngleX + this.Angle, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
			return;
		}
		base.transform.eulerAngles = new Vector3(this.AngleX, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x04000133 RID: 307
	private GameObject Player;

	// Token: 0x04000134 RID: 308
	public bool Lock;

	// Token: 0x04000135 RID: 309
	public bool Open;

	// Token: 0x04000136 RID: 310
	public float Angle;

	// Token: 0x04000137 RID: 311
	private float AngleX;

	// Token: 0x04000138 RID: 312
	private float clickTime;
}
