using UnityEngine;

// Token: 0x02000057 RID: 87
public class screwObject3 : MonoBehaviour
{
	// Token: 0x06000185 RID: 389 RVA: 0x0000A295 File Offset: 0x00008495
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000A2A8 File Offset: 0x000084A8
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

	// Token: 0x06000187 RID: 391 RVA: 0x0000A350 File Offset: 0x00008550
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000A3A0 File Offset: 0x000085A0
	private void TakeObject()
	{
		VariblesGlobal.Wrench3++;
		if (VariblesGlobal.Wrench3 >= 4)
		{
			GameObject.Find("Dver").AddComponent<Rigidbody>();
			GameObject.Find("Dver").GetComponent<Rigidbody>().AddForce(this.Player.transform.forward * 100f);
			GameObject.Find("Dver").AddComponent<Destroy>().DestroyTimer = 2f;
			Object.Instantiate(Resources.Load("sound/SoundDoorCrash"));
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400019F RID: 415
	private GameObject Player;

	// Token: 0x040001A0 RID: 416
	private float clickTime;
}
