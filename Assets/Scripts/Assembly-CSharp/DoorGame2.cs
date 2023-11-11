using UnityEngine;

// Token: 0x0200002B RID: 43
public class DoorGame2 : MonoBehaviour
{
	// Token: 0x060000AD RID: 173 RVA: 0x00006814 File Offset: 0x00004A14
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.AngleY = base.transform.eulerAngles.y;
		if (this.Open)
		{
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, this.AngleY + this.Angle, base.transform.eulerAngles.z);
		}
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000688C File Offset: 0x00004A8C
	private void OnMouseDown()
	{
		if (VariblesGlobal.game2_OpenDoor == 1)
		{
			if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
			{
				this.clickTime = Time.time;
				this.Lock = false;
				return;
			}
		}
		else
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text50;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			Object.Instantiate(Resources.Load("Sound/doorClose"));
		}
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00006940 File Offset: 0x00004B40
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000698D File Offset: 0x00004B8D
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bot" && !this.Open)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000069B4 File Offset: 0x00004BB4
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bot" && !this.Open)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000069DC File Offset: 0x00004BDC
	private void OpenClose()
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
		if (this.typeGO == "door")
		{
			(Object.Instantiate(Resources.Load("Sound/SoundDoor2"), base.gameObject.transform.position, Quaternion.identity) as GameObject).transform.position = base.gameObject.transform.position;
			return;
		}
		Object.Instantiate(Resources.Load("Sound/doorOpen"));
	}

	// Token: 0x04000120 RID: 288
	private GameObject Player;

	// Token: 0x04000121 RID: 289
	public bool Lock;

	// Token: 0x04000122 RID: 290
	public string LockNameKey;

	// Token: 0x04000123 RID: 291
	public bool Open;

	// Token: 0x04000124 RID: 292
	public float Angle;

	// Token: 0x04000125 RID: 293
	private float AngleY;

	// Token: 0x04000126 RID: 294
	private float clickTime;

	// Token: 0x04000127 RID: 295
	public string typeGO;
}
