using UnityEngine;

// Token: 0x0200003D RID: 61
public class Zone2 : MonoBehaviour
{
	// Token: 0x06000106 RID: 262 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00008420 File Offset: 0x00006620
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && HumanPa.CameraPosition)
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
