using UnityEngine;

// Token: 0x02000029 RID: 41
public class Door : MonoBehaviour
{
	// Token: 0x060000A1 RID: 161 RVA: 0x0000640D File Offset: 0x0000460D
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.AngleY = base.transform.eulerAngles.y;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00006438 File Offset: 0x00004638
	private void OnMouseDown()
	{
		if (this.Lock)
		{
			if (!(this.LockNameKey == VariblesGlobal.SelectObject))
			{
				Object.Instantiate(Resources.Load("Sound/doorClose"));
				return;
			}
			if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
			{
				this.clickTime = Time.time;
				this.Lock = false;
				return;
			}
		}
		else if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000064DC File Offset: 0x000046DC
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00006529 File Offset: 0x00004729
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bot" && !this.Open)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00006550 File Offset: 0x00004750
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bot" && !this.Open)
		{
			this.OpenClose();
		}
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00006578 File Offset: 0x00004778
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

	// Token: 0x04000111 RID: 273
	private GameObject Player;

	// Token: 0x04000112 RID: 274
	public bool Lock;

	// Token: 0x04000113 RID: 275
	public string LockNameKey;

	// Token: 0x04000114 RID: 276
	public bool Open;

	// Token: 0x04000115 RID: 277
	public float Angle;

	// Token: 0x04000116 RID: 278
	private float AngleY;

	// Token: 0x04000117 RID: 279
	private float clickTime;

	// Token: 0x04000118 RID: 280
	public string typeGO;
}
