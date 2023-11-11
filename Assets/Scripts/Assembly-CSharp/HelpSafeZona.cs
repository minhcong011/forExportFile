using UnityEngine;

// Token: 0x0200006A RID: 106
public class HelpSafeZona : MonoBehaviour
{
	// Token: 0x060001CB RID: 459 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000B2F8 File Offset: 0x000094F8
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text28;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
