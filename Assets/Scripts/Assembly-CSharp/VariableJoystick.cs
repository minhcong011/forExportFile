using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000092 RID: 146
public class VariableJoystick : Joystick
{
	// Token: 0x06000295 RID: 661 RVA: 0x00011CB1 File Offset: 0x0000FEB1
	private void Start()
	{
		if (this.isFixed)
		{
			this.OnFixed();
			return;
		}
		this.OnFloat();
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00011CC8 File Offset: 0x0000FEC8
	public void ChangeFixed(bool joystickFixed)
	{
		if (joystickFixed)
		{
			this.OnFixed();
		}
		else
		{
			this.OnFloat();
		}
		this.isFixed = joystickFixed;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00011CE4 File Offset: 0x0000FEE4
	public override void OnDrag(PointerEventData eventData)
	{
		Vector2 a = eventData.position - this.joystickCenter;
		this.inputVector = ((a.magnitude > this.background.sizeDelta.x / 2f) ? a.normalized : (a / (this.background.sizeDelta.x / 2f)));
		base.ClampJoystick();
		this.handle.anchoredPosition = this.inputVector * this.background.sizeDelta.x / 2f * this.handleLimit;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00011D90 File Offset: 0x0000FF90
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!this.isFixed)
		{
			this.background.gameObject.SetActive(true);
			this.background.position = eventData.position;
			this.handle.anchoredPosition = Vector2.zero;
			this.joystickCenter = eventData.position;
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00011DE8 File Offset: 0x0000FFE8
	public override void OnPointerUp(PointerEventData eventData)
	{
		if (!this.isFixed)
		{
			this.background.gameObject.SetActive(false);
		}
		this.inputVector = Vector2.zero;
		this.handle.anchoredPosition = Vector2.zero;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00011E1E File Offset: 0x0001001E
	private void OnFixed()
	{
		this.joystickCenter = this.fixedScreenPosition;
		this.background.gameObject.SetActive(true);
		this.handle.anchoredPosition = Vector2.zero;
		this.background.anchoredPosition = this.fixedScreenPosition;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00011E5E File Offset: 0x0001005E
	private void OnFloat()
	{
		this.handle.anchoredPosition = Vector2.zero;
		this.background.gameObject.SetActive(false);
	}

	// Token: 0x04000305 RID: 773
	[Header("Variable Joystick Options")]
	public bool isFixed = true;

	// Token: 0x04000306 RID: 774
	public Vector2 fixedScreenPosition;

	// Token: 0x04000307 RID: 775
	private Vector2 joystickCenter = Vector2.zero;
}
