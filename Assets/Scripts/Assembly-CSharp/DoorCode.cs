using UnityEngine;

// Token: 0x0200002A RID: 42
public class DoorCode : MonoBehaviour
{
	// Token: 0x060000A8 RID: 168 RVA: 0x00006677 File Offset: 0x00004877
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.AngleY = base.transform.eulerAngles.y;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000669F File Offset: 0x0000489F
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000066D4 File Offset: 0x000048D4
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00006724 File Offset: 0x00004924
	private void OpenClose()
	{
		if (VariblesGlobal.CODEnumberGenerateOpen == "OPEN")
		{
			this.Open = !this.Open;
			if (this.Open)
			{
				base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.AngleY + this.Angle, base.transform.eulerAngles.z);
			}
			else
			{
				base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.AngleY, base.transform.eulerAngles.z);
			}
			(Object.Instantiate(Resources.Load("Sound/SoundDoor2"), base.gameObject.transform.position, Quaternion.identity) as GameObject).transform.position = base.gameObject.transform.position;
		}
	}

	// Token: 0x04000119 RID: 281
	private GameObject Player;

	// Token: 0x0400011A RID: 282
	public bool Lock;

	// Token: 0x0400011B RID: 283
	public string LockNameKey;

	// Token: 0x0400011C RID: 284
	public bool Open;

	// Token: 0x0400011D RID: 285
	public float Angle;

	// Token: 0x0400011E RID: 286
	private float AngleY;

	// Token: 0x0400011F RID: 287
	private float clickTime;
}
