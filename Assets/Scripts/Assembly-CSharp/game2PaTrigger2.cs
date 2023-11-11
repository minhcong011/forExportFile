using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class game2PaTrigger2 : MonoBehaviour
{
	// Token: 0x060001C2 RID: 450 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000B1D5 File Offset: 0x000093D5
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && HumanPa.CameraPosition)
		{
			HumanPa.State2 = 1;
		}
	}
}
