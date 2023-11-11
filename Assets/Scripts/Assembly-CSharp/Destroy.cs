using UnityEngine;

// Token: 0x02000010 RID: 16
public class Destroy : MonoBehaviour
{
	// Token: 0x06000037 RID: 55 RVA: 0x00002BE4 File Offset: 0x00000DE4
	private void Update()
	{
		this.DestroyTimer -= Time.deltaTime;
		if (this.DestroyTimer <= 0f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400005E RID: 94
	public float DestroyTimer;
}
