using UnityEngine;

// Token: 0x02000036 RID: 54
public class ladder : MonoBehaviour
{
	// Token: 0x060000E7 RID: 231 RVA: 0x00007C45 File Offset: 0x00005E45
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.CameraLadderDefaultPos = new Vector3(-89.54f, 2.2f, -90.31f);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00007C71 File Offset: 0x00005E71
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00007CA8 File Offset: 0x00005EA8
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00007CF8 File Offset: 0x00005EF8
	private void TakeObject()
	{
		if (this.Player.transform.position.y < 3f)
		{
			Object.Instantiate(Resources.Load("CameraLaddern"), this.CameraLadderDefaultPos, Quaternion.Euler(0f, 90f, 0f));
		}
	}

	// Token: 0x0400014D RID: 333
	private GameObject Player;

	// Token: 0x0400014E RID: 334
	private float clickTime;

	// Token: 0x0400014F RID: 335
	private Vector3 CameraLadderDefaultPos;
}
