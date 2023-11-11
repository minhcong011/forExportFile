using UnityEngine;

// Token: 0x0200006C RID: 108
public class HelpShkaff : MonoBehaviour
{
	// Token: 0x060001D1 RID: 465 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000B3DC File Offset: 0x000095DC
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text13;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
