using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200001D RID: 29
public class FixedButton : MonoBehaviour, IPointerUpHandler, IEventSystemHandler, IPointerDownHandler
{
	// Token: 0x06000070 RID: 112 RVA: 0x00002584 File Offset: 0x00000784
	private void Start()
	{
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00002584 File Offset: 0x00000784
	private void Update()
	{
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00004C37 File Offset: 0x00002E37
	public void OnPointerDown(PointerEventData eventData)
	{
		this.Pressed = true;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004C40 File Offset: 0x00002E40
	public void OnPointerUp(PointerEventData eventData)
	{
		this.Pressed = false;
	}

	// Token: 0x040000CC RID: 204
	[HideInInspector]
	public bool Pressed;
}
