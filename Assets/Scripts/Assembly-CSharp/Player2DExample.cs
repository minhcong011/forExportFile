using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class Player2DExample : MonoBehaviour
{
	// Token: 0x0600027F RID: 639 RVA: 0x00011894 File Offset: 0x0000FA94
	private void Update()
	{
		Vector3 vector = Vector3.right * this.joystick.Horizontal + Vector3.up * this.joystick.Vertical;
		if (vector != Vector3.zero)
		{
			base.transform.rotation = Quaternion.LookRotation(Vector3.forward, vector);
			base.transform.Translate(vector * this.moveSpeed * Time.deltaTime, Space.World);
		}
	}

	// Token: 0x040002F5 RID: 757
	public float moveSpeed = 8f;

	// Token: 0x040002F6 RID: 758
	public Joystick joystick;
}
