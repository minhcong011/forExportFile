using UnityEngine;

// Token: 0x0200005A RID: 90
public class wheel1 : MonoBehaviour
{
	// Token: 0x06000194 RID: 404 RVA: 0x0000A681 File Offset: 0x00008881
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000A693 File Offset: 0x00008893
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000A6C8 File Offset: 0x000088C8
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000A718 File Offset: 0x00008918
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "wheel1";
		if (GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text16;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040001A6 RID: 422
	private GameObject Player;

	// Token: 0x040001A7 RID: 423
	private float clickTime;
}
