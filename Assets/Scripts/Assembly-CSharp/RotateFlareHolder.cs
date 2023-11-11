using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class RotateFlareHolder : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Update()
	{
		base.transform.Rotate(Vector3.down * Time.deltaTime * 60f);
		base.transform.Rotate(Vector3.left * Time.deltaTime * 30f);
		base.transform.Rotate(Vector3.back * Time.deltaTime * 12f);
	}
}
