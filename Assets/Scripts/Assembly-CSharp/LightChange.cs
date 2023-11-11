using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class LightChange : MonoBehaviour
{
	// Token: 0x060001AD RID: 429 RVA: 0x0000AB78 File Offset: 0x00008D78
	private void Update()
	{
		this.lerpedColor = Color.Lerp(Color.gray, Color.black, Mathf.PingPong(Time.time, 4f));
		base.GetComponent<Light>().color = this.lerpedColor;
	}

	// Token: 0x040001B2 RID: 434
	private Color lerpedColor = Color.white;
}
