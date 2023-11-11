using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000090 RID: 144
public class FixedJoystick : Joystick
{
	// Token: 0x0600028B RID: 651 RVA: 0x00011A5F File Offset: 0x0000FC5F
	private void Start()
	{
		this.joystickPosition = RectTransformUtility.WorldToScreenPoint(this.cam, this.background.position);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00011A80 File Offset: 0x0000FC80
	public override void OnDrag(PointerEventData eventData)
	{
		Vector2 a = eventData.position - this.joystickPosition;
		this.inputVector = ((a.magnitude > this.background.sizeDelta.x / 2f) ? a.normalized : (a / (this.background.sizeDelta.x / 2f)));
		base.ClampJoystick();
		this.handle.anchoredPosition = this.inputVector * this.background.sizeDelta.x / 2f * this.handleLimit;
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00011B2A File Offset: 0x0000FD2A
	public override void OnPointerDown(PointerEventData eventData)
	{
		this.OnDrag(eventData);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00011B33 File Offset: 0x0000FD33
	public override void OnPointerUp(PointerEventData eventData)
	{
		this.inputVector = Vector2.zero;
		this.handle.anchoredPosition = Vector2.zero;
	}

	// Token: 0x04000302 RID: 770
	private Vector2 joystickPosition = Vector2.zero;

	// Token: 0x04000303 RID: 771
	private Camera cam = new Camera();
}
