using UnityEngine;

// Token: 0x02000044 RID: 68
public class Gvozi : MonoBehaviour
{
	// Token: 0x06000126 RID: 294 RVA: 0x00008EE5 File Offset: 0x000070E5
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00008EF7 File Offset: 0x000070F7
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00008F2C File Offset: 0x0000712C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00008F7C File Offset: 0x0000717C
	private void TakeObject()
	{
		if (GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text45;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		VariblesGlobal.game2_Gvozdi = 1;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400016E RID: 366
	private GameObject Player;

	// Token: 0x0400016F RID: 367
	private float clickTime;
}
