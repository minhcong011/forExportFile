using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class RadioAchiv : MonoBehaviour
{
	// Token: 0x06000167 RID: 359 RVA: 0x00009B73 File Offset: 0x00007D73
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
		this.AudioSource1 = base.GetComponent<AudioSource>();
		this.AudioSource1.volume = 0f;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00009BA1 File Offset: 0x00007DA1
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00009BD5 File Offset: 0x00007DD5
	private void OnMouseUp()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00009C04 File Offset: 0x00007E04
	private void TakeObject()
	{
		if (VariblesGlobal.Achievement4 == 0)
		{
			VariblesGlobal.Achievement4 = 1;
			SaveLoadG.SaveData();
		}
		if (this.AudioSource1.volume == 0f)
		{
			this.AudioSource1.volume = 1f;
			return;
		}
		this.AudioSource1.volume = 0f;
	}

	// Token: 0x0400018F RID: 399
	private GameObject Player;

	// Token: 0x04000190 RID: 400
	private float clickTime;

	// Token: 0x04000191 RID: 401
	private AudioSource AudioSource1;
}
