using UnityEngine;

// Token: 0x02000058 RID: 88
public class screwObject4 : MonoBehaviour
{
	// Token: 0x0600018A RID: 394 RVA: 0x0000A433 File Offset: 0x00008633
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000A448 File Offset: 0x00008648
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

	// Token: 0x0600018C RID: 396 RVA: 0x0000A4F0 File Offset: 0x000086F0
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000A540 File Offset: 0x00008740
	private void TakeObject()
	{
		VariblesGlobal.Wrench4++;
		if (VariblesGlobal.Wrench4 >= 4)
		{
			GameObject.Find("Dver2").AddComponent<Rigidbody>();
			GameObject.Find("Dver2").GetComponent<Rigidbody>().AddForce(this.Player.transform.forward * 100f);
			GameObject.Find("Dver2").AddComponent<Destroy>().DestroyTimer = 2f;
			Object.Instantiate(Resources.Load("sound/SoundDoorCrash"));
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040001A1 RID: 417
	private GameObject Player;

	// Token: 0x040001A2 RID: 418
	private float clickTime;
}
