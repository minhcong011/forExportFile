using UnityEngine;

// Token: 0x02000028 RID: 40
public class CODE : MonoBehaviour
{
	// Token: 0x0600009C RID: 156 RVA: 0x00006144 File Offset: 0x00004344
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.PositionX = base.gameObject.transform.position.x;
		this.CodeColorRed = GameObject.Find("CodeColorRed");
		this.CodeColorGreen = GameObject.Find("CodeColorGreen");
		this.CodeColorGreen.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000061B0 File Offset: 0x000043B0
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			base.gameObject.transform.position = new Vector3(this.PositionX + 0.015f, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000623C File Offset: 0x0000443C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f)
		{
			if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
			{
				base.gameObject.transform.position = new Vector3(this.PositionX, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
				this.TakeObject();
				return;
			}
		}
		else
		{
			base.gameObject.transform.position = new Vector3(this.PositionX, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00006318 File Offset: 0x00004518
	private void TakeObject()
	{
		if (!this.ActivateCode)
		{
			VariblesGlobal.CODEnumber += this.NameB;
			if (VariblesGlobal.CODEnumber.Length > 4)
			{
				VariblesGlobal.CODEnumber = VariblesGlobal.CODEnumber.Substring(VariblesGlobal.CODEnumber.Length - 4);
			}
			if (VariblesGlobal.CODEnumberGenerate == VariblesGlobal.CODEnumber)
			{
				this.ActivateCode = true;
				this.CodeColorRed.GetComponent<MeshRenderer>().enabled = false;
				this.CodeColorGreen.GetComponent<MeshRenderer>().enabled = true;
				VariblesGlobal.CODEnumberGenerateOpen = "OPEN";
				(Object.Instantiate(Resources.Load("Sound/openCode")) as GameObject).transform.position = base.gameObject.transform.position;
			}
		}
		(Object.Instantiate(Resources.Load("Sound/soundBeep")) as GameObject).transform.position = base.gameObject.transform.position;
	}

	// Token: 0x0400010A RID: 266
	public string NameB;

	// Token: 0x0400010B RID: 267
	private float PositionX;

	// Token: 0x0400010C RID: 268
	private bool ActivateCode;

	// Token: 0x0400010D RID: 269
	private GameObject Player;

	// Token: 0x0400010E RID: 270
	private GameObject CodeColorRed;

	// Token: 0x0400010F RID: 271
	private GameObject CodeColorGreen;

	// Token: 0x04000110 RID: 272
	private float clickTime;
}
