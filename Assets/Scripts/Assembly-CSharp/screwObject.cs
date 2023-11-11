using UnityEngine;

// Token: 0x02000055 RID: 85
public class screwObject : MonoBehaviour
{
	// Token: 0x0600017B RID: 379 RVA: 0x00009FD4 File Offset: 0x000081D4
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00009FE8 File Offset: 0x000081E8
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			if (VariblesGlobal.SelectObject == "wrench01")
			{
				this.clickTime = Time.time;
				return;
			}
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text17;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000A090 File Offset: 0x00008290
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000A0E0 File Offset: 0x000082E0
	private void TakeObject()
	{
		VariblesGlobal.Wrench++;
		if (VariblesGlobal.Wrench >= 4)
		{
			GameObject.Find("Krishka").AddComponent<Rigidbody>();
			GameObject.Find("Krishka").AddComponent<Destroy>().DestroyTimer = 2f;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400019B RID: 411
	private GameObject Player;

	// Token: 0x0400019C RID: 412
	private float clickTime;
}
