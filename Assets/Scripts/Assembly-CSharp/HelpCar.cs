using UnityEngine;

// Token: 0x02000068 RID: 104
public class HelpCar : MonoBehaviour
{
	// Token: 0x060001C5 RID: 453 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000B1F8 File Offset: 0x000093F8
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (GameObject.Find("infobox") == null)
			{
				VariblesGlobal.infoboxText = VariblesGlobalText.Text14;
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/infobox")) as GameObject;
				gameObject.name = "infobox";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
