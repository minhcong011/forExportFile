using UnityEngine;

// Token: 0x0200004E RID: 78
public class meatbig : MonoBehaviour
{
	// Token: 0x06000158 RID: 344 RVA: 0x000097E5 File Offset: 0x000079E5
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000097F8 File Offset: 0x000079F8
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			if (VariblesGlobal.SelectObject == "Knife")
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text49;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x000098A0 File Offset: 0x00007AA0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000098F0 File Offset: 0x00007AF0
	private void TakeObject()
	{
		Object.Instantiate(Resources.Load("Sound/SoundPut"));
		GameObject.Find("gm_meat").transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 1f, base.transform.position.z);
	}

	// Token: 0x04000187 RID: 391
	private GameObject Player;

	// Token: 0x04000188 RID: 392
	private float clickTime;
}
