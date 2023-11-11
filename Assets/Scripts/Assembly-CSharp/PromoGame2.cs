using UnityEngine;

// Token: 0x02000062 RID: 98
public class PromoGame2 : MonoBehaviour
{
	// Token: 0x060001B3 RID: 435 RVA: 0x0000AC74 File Offset: 0x00008E74
	private void Start()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 3f, base.transform.position.z);
		this.Player = GameObject.Find("FPSController");
		this.PromoSphere = GameObject.Find("PromoSphere");
		this.Ypos = base.transform.position.y;
		this.Xpos = base.transform.position.x;
		this.Zpos = base.transform.position.z;
		this.YYpos = this.PromoSphere.transform.position.y;
		this.Yrot = 90f;
		base.transform.rotation = Quaternion.Euler(0f, this.Yrot, 0f);
		VariblesGlobal.PauseGame = true;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000AD78 File Offset: 0x00008F78
	private void Update()
	{
		if (this.State1 == 0)
		{
			this.TimeLeft1 -= Time.deltaTime;
			if (this.TimeLeft1 > 0f)
			{
				if (base.transform.position.y > 1.8f)
				{
					this.Ypos -= 0.9f * Time.deltaTime;
				}
			}
			else
			{
				this.State1 = 1;
				this.TimeLeft2 = 6f;
				this.State2 = 1;
			}
		}
		if (this.State2 == 1)
		{
			this.TimeLeft2 -= Time.deltaTime;
			if (this.TimeLeft2 > 0f)
			{
				if (this.SpeedX > 1f)
				{
					this.SpeedX -= 0.9f * Time.deltaTime;
					this.Zpos += 0.9f * Time.deltaTime;
				}
				this.Ypos += 0.2f * Time.deltaTime;
			}
			else
			{
				this.State3 = 1;
				this.State2 = 0;
				this.TimerLeft3 = 7f;
			}
		}
		if (this.State3 == 1)
		{
			this.TimerLeft3 -= Time.deltaTime;
			if (this.TimerLeft3 > 0f)
			{
				this.YYpos += 1.5f * Time.deltaTime;
				this.PromoSphere.transform.position = new Vector3(this.PromoSphere.transform.position.x, this.YYpos, this.PromoSphere.transform.position.z);
			}
			else
			{
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/Chapter2")) as GameObject;
				gameObject.name = "Chapter2";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
				Object.Destroy(GameObject.Find("PanelBlock"));
				Object.Destroy(base.gameObject);
			}
			if (this.TimerLeft3 < 1f)
			{
				base.GetComponent<AudioSource>().volume -= Time.deltaTime;
				VariblesGlobal.PauseGame = false;
			}
		}
		this.Xpos += this.SpeedX * Time.deltaTime;
		base.transform.LookAt(this.PromoSphere.transform.position);
		base.transform.position = new Vector3(this.Xpos, this.Ypos, this.Zpos);
	}

	// Token: 0x040001B4 RID: 436
	private GameObject Player;

	// Token: 0x040001B5 RID: 437
	private GameObject PromoSphere;

	// Token: 0x040001B6 RID: 438
	private float Ypos;

	// Token: 0x040001B7 RID: 439
	private float Xpos;

	// Token: 0x040001B8 RID: 440
	private float Zpos;

	// Token: 0x040001B9 RID: 441
	private float YYpos;

	// Token: 0x040001BA RID: 442
	private float Yrot;

	// Token: 0x040001BB RID: 443
	private float SpeedX = 5f;

	// Token: 0x040001BC RID: 444
	private float TimeLeft1 = 4f;

	// Token: 0x040001BD RID: 445
	private float TimeLeft2;

	// Token: 0x040001BE RID: 446
	private float TimerLeft3;

	// Token: 0x040001BF RID: 447
	private int State1;

	// Token: 0x040001C0 RID: 448
	private int State2;

	// Token: 0x040001C1 RID: 449
	private int State3;
}
