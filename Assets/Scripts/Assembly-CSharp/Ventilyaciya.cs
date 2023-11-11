using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class Ventilyaciya : MonoBehaviour
{
	// Token: 0x060001D4 RID: 468 RVA: 0x0000B45B File Offset: 0x0000965B
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			VariblesGlobal.tCanvasUpDown = 1;
		}
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000B475 File Offset: 0x00009675
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			VariblesGlobal.tCanvasUpDown = 0;
		}
	}
}
