using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class Player3DExample : MonoBehaviour
{
	// Token: 0x06000281 RID: 641 RVA: 0x0001192C File Offset: 0x0000FB2C
	private void Update()
	{
		Vector3 vector = Vector3.right * this.joystick.Horizontal + Vector3.forward * this.joystick.Vertical;
		if (vector != Vector3.zero)
		{
			base.transform.rotation = Quaternion.LookRotation(vector);
			base.transform.Translate(vector * this.moveSpeed * Time.deltaTime, Space.World);
		}
	}

	// Token: 0x040002F7 RID: 759
	public float moveSpeed = 8f;

	// Token: 0x040002F8 RID: 760
	public Joystick joystick;
}
