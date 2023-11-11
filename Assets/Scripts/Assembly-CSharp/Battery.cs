using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000041 RID: 65
public class Battery : MonoBehaviour
{
	// Token: 0x06000117 RID: 279 RVA: 0x00008C38 File Offset: 0x00006E38
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00008C4A File Offset: 0x00006E4A
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00008C80 File Offset: 0x00006E80
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00008CD0 File Offset: 0x00006ED0
	private void TakeObject()
	{
		VariblesGlobal.Ammo++;
		if (GameObject.Find("IcoBulletText") != null)
		{
			GameObject.Find("IcoBulletText").GetComponent<Text>().text = string.Concat(VariblesGlobal.Ammo);
		}
		if (GameObject.Find("infobox") == null && this.StateMessage == 0)
		{
			this.StateMessage = 1;
			VariblesGlobal.infoboxText = VariblesGlobalText.Text2;
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
			gameObject.name = "infobox";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000166 RID: 358
	private GameObject Player;

	// Token: 0x04000167 RID: 359
	private float clickTime;

	// Token: 0x04000168 RID: 360
	private int StateMessage;
}
