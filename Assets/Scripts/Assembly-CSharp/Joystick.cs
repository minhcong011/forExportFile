using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200008E RID: 142
public class Joystick : MonoBehaviour, IDragHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000283 RID: 643 RVA: 0x000119BC File Offset: 0x0000FBBC
	public float Horizontal
	{
		get
		{
			return this.inputVector.x;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000284 RID: 644 RVA: 0x000119C9 File Offset: 0x0000FBC9
	public float Vertical
	{
		get
		{
			return this.inputVector.y;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000285 RID: 645 RVA: 0x000119D6 File Offset: 0x0000FBD6
	public Vector2 Direction
	{
		get
		{
			return new Vector2(this.Horizontal, this.Vertical);
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00002584 File Offset: 0x00000784
	public virtual void OnDrag(PointerEventData eventData)
	{
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00002584 File Offset: 0x00000784
	public virtual void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00002584 File Offset: 0x00000784
	public virtual void OnPointerUp(PointerEventData eventData)
	{
	}

	// Token: 0x06000289 RID: 649 RVA: 0x000119EC File Offset: 0x0000FBEC
	protected void ClampJoystick()
	{
		if (this.joystickMode == JoystickMode.Horizontal)
		{
			this.inputVector = new Vector2(this.inputVector.x, 0f);
		}
		if (this.joystickMode == JoystickMode.Vertical)
		{
			this.inputVector = new Vector2(0f, this.inputVector.y);
		}
	}

	// Token: 0x040002F9 RID: 761
	[Header("Options")]
	[Range(0f, 2f)]
	public float handleLimit = 1f;

	// Token: 0x040002FA RID: 762
	public JoystickMode joystickMode;

	// Token: 0x040002FB RID: 763
	public Vector2 inputVector = Vector2.zero;

	// Token: 0x040002FC RID: 764
	[Header("Components")]
	public RectTransform background;

	// Token: 0x040002FD RID: 765
	public RectTransform handle;
}
