using UnityEngine;

// Token: 0x02000056 RID: 86
public class screwObject2 : MonoBehaviour
{
	// Token: 0x06000180 RID: 384 RVA: 0x0000A135 File Offset: 0x00008335
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000A148 File Offset: 0x00008348
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

	// Token: 0x06000182 RID: 386 RVA: 0x0000A1F0 File Offset: 0x000083F0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000A240 File Offset: 0x00008440
	private void TakeObject()
	{
		VariblesGlobal.Wrench2++;
		if (VariblesGlobal.Wrench2 >= 4)
		{
			GameObject.Find("Krishka").AddComponent<Rigidbody>();
			GameObject.Find("Krishka").AddComponent<Destroy>().DestroyTimer = 2f;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400019D RID: 413
	private GameObject Player;

	// Token: 0x0400019E RID: 414
	private float clickTime;
}
