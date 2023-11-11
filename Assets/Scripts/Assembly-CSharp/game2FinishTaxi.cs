using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class game2FinishTaxi : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x0000410A File Offset: 0x0000230A
	private void Start()
	{
		this.Xpos = base.transform.position.x;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00004124 File Offset: 0x00002324
	private void Update()
	{
		this.Xpos += this.Speed * Time.deltaTime;
		base.transform.position = new Vector3(this.Xpos, base.transform.position.y, base.transform.position.z);
	}

	// Token: 0x04000094 RID: 148
	private float Xpos;

	// Token: 0x04000095 RID: 149
	public float Speed;
}
