using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000050 RID: 80
public class Pistol : MonoBehaviour
{
	// Token: 0x06000162 RID: 354 RVA: 0x00009A09 File Offset: 0x00007C09
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00009A1B File Offset: 0x00007C1B
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00009A50 File Offset: 0x00007C50
	private void OnMouseUp()
	{
		this.ClickUI = false;
		if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "IcoDropDown")
		{
			this.ClickUI = true;
		}
		if (Time.time - this.clickTime <= 0.3f && !this.ClickUI && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00009AEC File Offset: 0x00007CEC
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Pistol";
		if (this.StateText == 0)
		{
			this.StateText = 1;
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text3;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400018B RID: 395
	private GameObject Player;

	// Token: 0x0400018C RID: 396
	private float clickTime;

	// Token: 0x0400018D RID: 397
	private bool ClickUI;

	// Token: 0x0400018E RID: 398
	private int StateText;
}
