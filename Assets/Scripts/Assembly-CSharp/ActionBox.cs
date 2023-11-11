using UnityEngine;

// Token: 0x02000030 RID: 48
public class ActionBox : MonoBehaviour
{
	// Token: 0x060000C9 RID: 201 RVA: 0x000072E2 File Offset: 0x000054E2
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000072F4 File Offset: 0x000054F4
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			if (VariblesGlobal.SelectObject == "Axe")
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text18;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000739C File Offset: 0x0000559C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000CC RID: 204 RVA: 0x000073EC File Offset: 0x000055EC
	private void TakeObject()
	{
		if (this.SoundKick)
		{
			HumanMa.PositionGo = this.Player.transform.position;
		}
		(Object.Instantiate(Resources.Load("BoxEmu")) as GameObject).transform.position = new Vector3(base.gameObject.transform.position.x + 1.2f, base.gameObject.transform.position.y + 0.6f, base.gameObject.transform.position.z + 1.2f);
		(Object.Instantiate(Resources.Load("Sound/breakwood")) as GameObject).transform.position = base.gameObject.transform.position;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400013C RID: 316
	private GameObject Player;

	// Token: 0x0400013D RID: 317
	private float clickTime;

	// Token: 0x0400013E RID: 318
	public bool SoundKick = true;
}
