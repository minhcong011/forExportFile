using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class IcoCamera : MonoBehaviour
{
	// Token: 0x06000213 RID: 531 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
	private void Update()
	{
		base.GetComponent<RectTransform>().sizeDelta = new Vector2(this.Size, this.Size);
		if (this.Size <= 140f)
		{
			this.SizePlus = 70f * Time.deltaTime;
		}
		if (this.Size >= 180f)
		{
			this.SizePlus = -70f * Time.deltaTime;
		}
		this.Size += this.SizePlus;
	}

	// Token: 0x04000248 RID: 584
	private int State;

	// Token: 0x04000249 RID: 585
	private float Size = 140f;

	// Token: 0x0400024A RID: 586
	private float SizePlus;
}
