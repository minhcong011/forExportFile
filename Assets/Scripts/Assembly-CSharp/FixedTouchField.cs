using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200001E RID: 30
public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	// Token: 0x06000075 RID: 117 RVA: 0x00004C4C File Offset: 0x00002E4C
	private void Start()
	{
		Color color = base.GetComponent<Image>().color;
		color.a = 0f;
		base.gameObject.GetComponent<Image>().color = color;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00004C84 File Offset: 0x00002E84
	private void Update()
	{
		if (!this.Pressed)
		{
			this.TouchDist = default(Vector2);
			return;
		}
		if (this.PointerId >= 0 && this.PointerId < Input.touches.Length)
		{
			this.TouchDist = Input.touches[this.PointerId].position - this.PointerOld;
			this.PointerOld = Input.touches[this.PointerId].position;
			return;
		}
		this.TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - this.PointerOld;
		this.PointerOld = Input.mousePosition;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00004D3D File Offset: 0x00002F3D
	public void OnPointerDown(PointerEventData eventData)
	{
		this.Pressed = true;
		this.PointerId = eventData.pointerId;
		this.PointerOld = eventData.position;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00004D5E File Offset: 0x00002F5E
	public void OnPointerUp(PointerEventData eventData)
	{
		this.Pressed = false;
	}

	// Token: 0x040000CD RID: 205
	[HideInInspector]
	public Vector2 TouchDist;

	// Token: 0x040000CE RID: 206
	[HideInInspector]
	public Vector2 PointerOld;

	// Token: 0x040000CF RID: 207
	[HideInInspector]
	protected int PointerId;

	// Token: 0x040000D0 RID: 208
	[HideInInspector]
	public bool Pressed;

	// Token: 0x040000D1 RID: 209
	private float TimeLeft = 5f;
}
