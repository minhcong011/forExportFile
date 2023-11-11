using UnityEngine;

// Token: 0x02000037 RID: 55
public class ladderBroken : MonoBehaviour
{
	// Token: 0x060000EC RID: 236 RVA: 0x00007D4B File Offset: 0x00005F4B
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.set_ladderLong = GameObject.Find("ladderLong811u82");
		this.set_ladderLong.SetActive(false);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00007D7C File Offset: 0x00005F7C
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			if (VariblesGlobal.SelectObject == "Molotok" && VariblesGlobal.game2_Gvozdi > 0)
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text44;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00007E2C File Offset: 0x0000602C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00007E79 File Offset: 0x00006079
	private void TakeObject()
	{
		Object.Instantiate(Resources.Load("Sound/SoundComplete"));
		this.set_ladderLong.SetActive(true);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000150 RID: 336
	private GameObject Player;

	// Token: 0x04000151 RID: 337
	private GameObject set_ladderLong;

	// Token: 0x04000152 RID: 338
	private float clickTime;
}
