using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class game2MostDeathTtrigger : MonoBehaviour
{
	// Token: 0x060001BB RID: 443 RVA: 0x0000B0FB File Offset: 0x000092FB
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000B11C File Offset: 0x0000931C
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.Player.GetComponent<CharacterController>().enabled = false;
			this.Player.transform.position = GameObject.Find("SpawnPoint").transform.position;
			this.Player.GetComponent<CharacterController>().enabled = true;
		}
	}

	// Token: 0x040001C6 RID: 454
	private GameObject Player;

	// Token: 0x040001C7 RID: 455
	private float Timer;
}
