using UnityEngine;

// Token: 0x02000066 RID: 102
public class game2PaTrigger : MonoBehaviour
{
	// Token: 0x060001BE RID: 446 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000B181 File Offset: 0x00009381
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Bot")
		{
			VariblesGlobal.game2_PosGrandPA = 1;
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000B1AB File Offset: 0x000093AB
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bot")
		{
			VariblesGlobal.game2_PosGrandPA = 1;
			Object.Destroy(base.gameObject);
		}
	}
}
