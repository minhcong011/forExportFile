using UnityEngine;

// Token: 0x0200003C RID: 60
public class Zone1 : MonoBehaviour
{
	// Token: 0x06000103 RID: 259 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x000083A0 File Offset: 0x000065A0
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && HumanPa.CameraPosition && VariblesGlobal.Camera01 == 0)
		{
			if (GameObject.Find("IcoCamera") == null)
			{
				GameObject gameObject = Object.Instantiate(Resources.Load("UI/IcoCamera")) as GameObject;
				gameObject.name = "IcoCamera";
				gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
			HumanPa.State2 = 1;
		}
	}
}
