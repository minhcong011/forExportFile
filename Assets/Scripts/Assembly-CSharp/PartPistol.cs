using UnityEngine;

// Token: 0x02000038 RID: 56
public class PartPistol : MonoBehaviour
{
	// Token: 0x060000F1 RID: 241 RVA: 0x00007EA2 File Offset: 0x000060A2
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00007EB4 File Offset: 0x000060B4
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00007EE8 File Offset: 0x000060E8
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00007F38 File Offset: 0x00006138
	private void TakeObject()
	{
		VariblesGlobal.PartPistol++;
		if (VariblesGlobal.PartPistol == 1 && GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text9;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		if (VariblesGlobal.PartPistol == 2 && GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text10;
			GameObject gameObject2 = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject2.name = "infobox";
			gameObject2.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000153 RID: 339
	private GameObject Player;

	// Token: 0x04000154 RID: 340
	private float clickTime;
}
