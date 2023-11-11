using UnityEngine;

// Token: 0x0200006B RID: 107
public class HelpSafeZona2 : MonoBehaviour
{
	// Token: 0x060001CE RID: 462 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000B378 File Offset: 0x00009578
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameObject gameObject = Object.Instantiate(Resources.Load("UI/PanelSafeZone")) as GameObject;
			gameObject.name = "PanelSafeZone";
			gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			Object.Destroy(base.gameObject);
		}
	}
}
