using UnityEngine;

// Token: 0x0200003A RID: 58
public class WallBreak : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x00008016 File Offset: 0x00006216
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00008028 File Offset: 0x00006228
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			if (VariblesGlobal.SelectObject == "Molotok")
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text15;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x000080D0 File Offset: 0x000062D0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00008120 File Offset: 0x00006320
	private void TakeObject()
	{
		Object.Instantiate(Resources.Load("Sound/wallBreak"));
		HumanMa.PositionGo = this.Player.transform.position;
		(Object.Instantiate(Resources.Load("WallEmu")) as GameObject).transform.position = new Vector3(base.gameObject.transform.position.x + 0.26f, base.gameObject.transform.position.y + 1.5f, base.gameObject.transform.position.z);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000155 RID: 341
	private GameObject Player;

	// Token: 0x04000156 RID: 342
	private float clickTime;
}
