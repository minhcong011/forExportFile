using UnityEngine;

// Token: 0x02000014 RID: 20
public class CameraFinish : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x00002584 File Offset: 0x00000784
	private void Start()
	{
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003A30 File Offset: 0x00001C30
	private void Update()
	{
		this.Time1 -= Time.deltaTime;
		this.Time2 -= Time.deltaTime;
		if (this.Time2 < 0f & this.State2 == 0)
		{
			this.State2 = 1;
			Object.Instantiate(Resources.Load("Sound/SoundBoxCrash"), base.gameObject.transform.position, Quaternion.identity);
		}
		if (this.Time2 < -0.5f & this.State3 == 0)
		{
			this.State3 = 1;
			Object.Instantiate(Resources.Load("Sound/SoundBoxCrash2"), base.gameObject.transform.position, Quaternion.identity);
		}
		if (this.Time1 <= 0f)
		{
			Quaternion b = Quaternion.Euler(0f, this.Angle, 0f);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * this.Speed);
			if (base.transform.rotation.y < 0.5f && this.State == 0)
			{
				this.Angle = 95f;
				this.Speed = 0.9f;
				this.State = 1;
			}
			if (base.transform.rotation.y > 0.735f && this.State == 1)
			{
				this.Angle = 90f;
				this.Speed = 0.9f;
				this.State = 2;
			}
		}
	}

	// Token: 0x0400007C RID: 124
	private float Angle;

	// Token: 0x0400007D RID: 125
	private float Speed = 0.3f;

	// Token: 0x0400007E RID: 126
	private int State;

	// Token: 0x0400007F RID: 127
	private int State2;

	// Token: 0x04000080 RID: 128
	private int State3;

	// Token: 0x04000081 RID: 129
	private float Time1 = 3f;

	// Token: 0x04000082 RID: 130
	private float Time2 = 1.1f;
}
