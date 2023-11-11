using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class MoveScript : MonoBehaviour
{
	// Token: 0x060001F3 RID: 499 RVA: 0x0000E977 File Offset: 0x0000CB77
	private void Start()
	{
		this.MoveJoystick = GameObject.Find("Fixed Joystick");
		this.JumpButton = GameObject.Find("JoyButton");
		this.TouchField = GameObject.Find("TouchField");
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000E9AC File Offset: 0x0000CBAC
	private void Update()
	{
		FirstPersonController component = base.GetComponent<FirstPersonController>();
		if (VariblesGlobal.PlayerHide == 0)
		{
			component.RunAxis = this.MoveJoystick.GetComponent<FixedJoystick>().inputVector;
			component.m_MouseLook.LookAxis = this.TouchField.GetComponent<FixedTouchField>().TouchDist;
			return;
		}
		component.RunAxis = new Vector2(0f, 0f);
		component.m_MouseLook.LookAxis = this.TouchField.GetComponent<FixedTouchField>().TouchDist;
	}

	// Token: 0x04000231 RID: 561
	private GameObject MoveJoystick;

	// Token: 0x04000232 RID: 562
	private GameObject JumpButton;

	// Token: 0x04000233 RID: 563
	private GameObject TouchField;

	// Token: 0x04000234 RID: 564
	private int State;

	// Token: 0x04000235 RID: 565
	private float TimerAchiev;
}
