using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class NoRender : MonoBehaviour
{
	// Token: 0x060001AF RID: 431 RVA: 0x00008391 File Offset: 0x00006591
	private void Start()
	{
		base.GetComponent<MeshRenderer>().enabled = false;
	}
}
