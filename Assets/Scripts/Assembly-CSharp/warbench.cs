using UnityEngine;

// Token: 0x0200003B RID: 59
public class warbench : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x000081CC File Offset: 0x000063CC
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.gm_pistol = GameObject.Find("gm_Pistol");
		this.PosPistol = this.gm_pistol.transform.position;
		this.gm_pistol.transform.position = new Vector3(this.PosPistol.x, this.PosPistol.y - 100f, this.PosPistol.z);
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000824B File Offset: 0x0000644B
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00008280 File Offset: 0x00006480
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000082D0 File Offset: 0x000064D0
	private void TakeObject()
	{
		if (this.State == 0)
		{
			if (VariblesGlobal.PartPistol >= 2)
			{
				(Object.Instantiate(Resources.Load("Sound/metalhit")) as GameObject).transform.position = base.gameObject.transform.position;
				this.gm_pistol.transform.position = this.PosPistol;
				this.State = 1;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text11;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x04000157 RID: 343
	private GameObject Player;

	// Token: 0x04000158 RID: 344
	private GameObject gm_pistol;

	// Token: 0x04000159 RID: 345
	private Vector3 PosPistol;

	// Token: 0x0400015A RID: 346
	private float clickTime;

	// Token: 0x0400015B RID: 347
	private int State;
}
