using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000091 RID: 145
public class FloatingJoystick : Joystick
{
	// Token: 0x06000290 RID: 656 RVA: 0x00011B6E File Offset: 0x0000FD6E
	private void Start()
	{
		this.background.gameObject.SetActive(false);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00011B84 File Offset: 0x0000FD84
	public override void OnDrag(PointerEventData eventData)
	{
		Vector2 a = eventData.position - this.joystickCenter;
		this.inputVector = ((a.magnitude > this.background.sizeDelta.x / 2f) ? a.normalized : (a / (this.background.sizeDelta.x / 2f)));
		base.ClampJoystick();
		this.handle.anchoredPosition = this.inputVector * this.background.sizeDelta.x / 2f * this.handleLimit;
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00011C30 File Offset: 0x0000FE30
	public override void OnPointerDown(PointerEventData eventData)
	{
		this.background.gameObject.SetActive(true);
		this.background.position = eventData.position;
		this.handle.anchoredPosition = Vector2.zero;
		this.joystickCenter = eventData.position;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00011C80 File Offset: 0x0000FE80
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.background.gameObject.SetActive(false);
		this.inputVector = Vector2.zero;
	}

	// Token: 0x04000304 RID: 772
	private Vector2 joystickCenter = Vector2.zero;
}
