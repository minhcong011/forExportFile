using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class bmwx5 : MonoBehaviour
{
	// Token: 0x06000044 RID: 68 RVA: 0x00002584 File Offset: 0x00000784
	private void Start()
	{
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000039A4 File Offset: 0x00001BA4
	private void Update()
	{
		if (this.Speed > 60f)
		{
			this.Speed = 60f;
			return;
		}
		this.Speed += 3f * Time.deltaTime;
		base.transform.position = new Vector3(base.transform.position.x + this.Speed * Time.deltaTime, base.transform.position.y, base.transform.position.z);
	}

	// Token: 0x0400007B RID: 123
	private float Speed;
}
