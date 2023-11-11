using UnityEngine;

// Token: 0x0200002F RID: 47
public class ActionBochka : MonoBehaviour
{
	// Token: 0x060000C4 RID: 196 RVA: 0x000070F0 File Offset: 0x000052F0
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00007104 File Offset: 0x00005304
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

	// Token: 0x060000C6 RID: 198 RVA: 0x000071AC File Offset: 0x000053AC
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000071FC File Offset: 0x000053FC
	private void TakeObject()
	{
		if (this.SoundKick)
		{
			HumanMa.PositionGo = this.Player.transform.position;
		}
		(Object.Instantiate(Resources.Load("BochkaEmu")) as GameObject).transform.position = new Vector3(base.gameObject.transform.position.x + 1.5f, base.gameObject.transform.position.y + 0.6f, base.gameObject.transform.position.z + 1.5f);
		(Object.Instantiate(Resources.Load("Sound/breakwood")) as GameObject).transform.position = base.gameObject.transform.position;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000139 RID: 313
	private GameObject Player;

	// Token: 0x0400013A RID: 314
	private float clickTime;

	// Token: 0x0400013B RID: 315
	public bool SoundKick = true;
}
