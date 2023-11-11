using UnityEngine;

// Token: 0x02000033 RID: 51
public class CameraHide : MonoBehaviour
{
	// Token: 0x060000D9 RID: 217 RVA: 0x000078F5 File Offset: 0x00005AF5
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.Cam = base.GetComponent<Camera>();
		this.Cam.enabled = false;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00007920 File Offset: 0x00005B20
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			if (VariblesGlobal.MaSee == 0 && VariblesGlobal.PaSee == 0)
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text4;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x000079C4 File Offset: 0x00005BC4
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 4f)
		{
			this.HideMe();
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00007A14 File Offset: 0x00005C14
	private void HideMe()
	{
		if (this.CallGrandMA && Random.Range(0, 3) == 0)
		{
			HumanMa.PositionGo = this.Player.transform.position;
		}
		GameObject gameObject = Object.Instantiate(Resources.Load("UI/icoShowMe")) as GameObject;
		gameObject.name = "icoShowMe";
		gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		gameObject.GetComponent<ShowMe>().Cam = this.Cam;
		Object.Instantiate(Resources.Load("Sound/SoundCabinet"));
		VariblesGlobal.PlayerHide = 1;
		this.Cam.enabled = true;
	}

	// Token: 0x04000146 RID: 326
	private GameObject Player;

	// Token: 0x04000147 RID: 327
	private Camera Cam;

	// Token: 0x04000148 RID: 328
	private float clickTime;

	// Token: 0x04000149 RID: 329
	public bool CallGrandMA = true;
}
