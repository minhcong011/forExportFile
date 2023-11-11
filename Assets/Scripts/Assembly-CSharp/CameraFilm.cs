using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CameraFilm : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x00002586 File Offset: 0x00000786
	private void Start()
	{
		this.StartPos = base.transform.position.y;
		this.PromoSphere = GameObject.Find("PromoSphere");
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000025B0 File Offset: 0x000007B0
	private void Update()
	{
		this.StartPos += 0.1f * Time.deltaTime;
		base.transform.position = new Vector3(base.transform.position.x, this.StartPos, base.transform.position.z);
		this.Timer1 -= Time.deltaTime;
		if (this.Timer1 < 0f)
		{
			this.PromoSphere.transform.position = new Vector3(this.PromoSphere.transform.position.x + 0.1f * Time.deltaTime, this.PromoSphere.transform.position.y + 0.1f * Time.deltaTime, this.PromoSphere.transform.position.z);
		}
		base.transform.LookAt(this.PromoSphere.transform.position);
	}

	// Token: 0x0400000F RID: 15
	private float StartPos;

	// Token: 0x04000010 RID: 16
	private float Timer1;

	// Token: 0x04000011 RID: 17
	private GameObject Player;

	// Token: 0x04000012 RID: 18
	private GameObject PromoSphere;
}
