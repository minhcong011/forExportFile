using UnityEngine;

// Token: 0x0200005B RID: 91
public class wheel2 : MonoBehaviour
{
	// Token: 0x06000199 RID: 409 RVA: 0x0000A790 File Offset: 0x00008990
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000A7A2 File Offset: 0x000089A2
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000A7D8 File Offset: 0x000089D8
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000A828 File Offset: 0x00008A28
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "wheel2";
		if (GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text16;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040001A8 RID: 424
	private GameObject Player;

	// Token: 0x040001A9 RID: 425
	private float clickTime;
}
