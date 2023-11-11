using UnityEngine;

// Token: 0x02000069 RID: 105
public class HelpDiraPol : MonoBehaviour
{
	// Token: 0x060001C8 RID: 456 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000B278 File Offset: 0x00009478
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text22;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
