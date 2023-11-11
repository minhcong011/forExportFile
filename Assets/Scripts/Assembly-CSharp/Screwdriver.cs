using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000054 RID: 84
public class Screwdriver : MonoBehaviour
{
	// Token: 0x06000176 RID: 374 RVA: 0x00009E79 File Offset: 0x00008079
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00009E8B File Offset: 0x0000808B
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00009EC0 File Offset: 0x000080C0
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

	// Token: 0x06000179 RID: 377 RVA: 0x00009F5C File Offset: 0x0000815C
	private void TakeObject()
	{
		if (GameObject.Find("infobox") == null)
		{
			VariblesGlobal.infoboxText = VariblesGlobalText.Text21;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		VariblesGlobal.SelectObject = "Screwdriver";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000198 RID: 408
	private GameObject Player;

	// Token: 0x04000199 RID: 409
	private float clickTime;

	// Token: 0x0400019A RID: 410
	private bool ClickUI;
}
