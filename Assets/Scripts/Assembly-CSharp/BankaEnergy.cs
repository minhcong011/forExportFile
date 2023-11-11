using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000040 RID: 64
public class BankaEnergy : MonoBehaviour
{
	// Token: 0x06000112 RID: 274 RVA: 0x00008A98 File Offset: 0x00006C98
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00008AAA File Offset: 0x00006CAA
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00008AE0 File Offset: 0x00006CE0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00008B30 File Offset: 0x00006D30
	private void TakeObject()
	{
		VariblesGlobal.BankaEnergy++;
		if (GameObject.Find("TextBankaEnergy") != null)
		{
			GameObject.Find("TextBankaEnergy").GetComponent<Text>().text = string.Concat(VariblesGlobal.BankaEnergy);
		}
		if (GameObject.Find("infobox") == null && this.StateMessage == 0)
		{
			this.StateMessage = 1;
			VariblesGlobal.infoboxText = VariblesGlobalText.Text57;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		if (GameObject.Find("IcoBankaEnergy") == null)
		{
			GameObject gameObject2 = Object.Instantiate(Resources.Load("UI/IcoBankaEnergy")) as GameObject;
			gameObject2.name = "IcoBankaEnergy";
			gameObject2.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000163 RID: 355
	private GameObject Player;

	// Token: 0x04000164 RID: 356
	private float clickTime;

	// Token: 0x04000165 RID: 357
	private int StateMessage;
}
