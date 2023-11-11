using UnityEngine;

// Token: 0x02000045 RID: 69
public class jerrycan : MonoBehaviour
{
	// Token: 0x0600012B RID: 299 RVA: 0x00008FEF File Offset: 0x000071EF
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00009001 File Offset: 0x00007201
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00009038 File Offset: 0x00007238
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00009088 File Offset: 0x00007288
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "jerrycan";
		if (GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text16;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000170 RID: 368
	private GameObject Player;

	// Token: 0x04000171 RID: 369
	private float clickTime;
}
