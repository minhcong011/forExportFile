using UnityEngine;

// Token: 0x0200002D RID: 45
public class DoorSafe : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00006CF0 File Offset: 0x00004EF0
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.AngleY = base.transform.eulerAngles.y;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00006D18 File Offset: 0x00004F18
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			if (VariblesGlobal.SelectObject == "Screwdriver")
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text12;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00006DC0 File Offset: 0x00004FC0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00006E10 File Offset: 0x00005010
	private void OpenClose()
	{
		this.Open = !this.Open;
		if (this.Open)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.AngleY + this.Angle, base.transform.eulerAngles.z);
			base.transform.position = new Vector3(base.transform.position.x + 1.34f, base.transform.position.y, base.transform.position.z);
		}
		else
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.AngleY, base.transform.eulerAngles.z);
			base.transform.position = new Vector3(base.transform.position.x - 1.34f, base.transform.position.y, base.transform.position.z);
		}
		(Object.Instantiate(Resources.Load("Sound/safeclick")) as GameObject).transform.position = base.gameObject.transform.position;
	}

	// Token: 0x0400012D RID: 301
	private GameObject Player;

	// Token: 0x0400012E RID: 302
	public bool Lock;

	// Token: 0x0400012F RID: 303
	public bool Open;

	// Token: 0x04000130 RID: 304
	public float Angle;

	// Token: 0x04000131 RID: 305
	private float AngleY;

	// Token: 0x04000132 RID: 306
	private float clickTime;
}
