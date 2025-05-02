// dnSpy decompiler from Assembly-CSharp.dll class: DragHandeler
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
{
	public void OnBeginDrag(PointerEventData eventData)
	{
		Canvas canvas = DragHandeler.FindInParents<Canvas>(base.gameObject);
		if (canvas == null)
		{
			return;
		}
		this.DragBegin.text = eventData.dragging.ToString();
		if (this.m_DraggingIcon == null)
		{
			this.m_DraggingIcon = new GameObject("icon");
		}
		this.m_DraggingIcon.transform.SetParent(canvas.transform, false);
		this.m_DraggingIcon.transform.SetAsLastSibling();
		if (this.m_DraggingIcon.GetComponent<Image>() == null)
		{
			Image image = this.m_DraggingIcon.AddComponent<Image>();
			image.sprite = base.GetComponent<Image>().sprite;
			image.SetNativeSize();
		}
		this.m_DraggingIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, 50f);
		if (this.dragOnSurfaces)
		{
			this.m_DraggingPlane = (base.transform as RectTransform);
		}
		else
		{
			this.m_DraggingPlane = (canvas.transform as RectTransform);
		}
		this.SetDraggedPosition(eventData);
	}

	public void OnDrag(PointerEventData data)
	{
		if (this.m_DraggingIcon != null)
		{
			this.SetDraggedPosition(data);
		}
		this.Draging.text = data.dragging.ToString();
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (this.dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
		{
			this.m_DraggingPlane = (data.pointerEnter.transform as RectTransform);
		}
		RectTransform component = this.m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 position;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_DraggingPlane, data.position, data.pressEventCamera, out position))
		{
			component.position = position;
			component.rotation = Quaternion.Euler(0f, 0f, 15f);
		}
		this.PointerPosInfo.text = string.Concat(new object[]
		{
			"X: ",
			data.position.x,
			"\nY: ",
			data.position.y
		});
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.DragEnd.text = eventData.dragging.ToString();
		this.m_DraggingIcon.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, 0f);
	}

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return (T)((object)null);
		}
		T component = go.GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		Transform parent = go.transform.parent;
		while (parent != null && component == null)
		{
			component = parent.gameObject.GetComponent<T>();
			parent = parent.parent;
		}
		return component;
	}

	public bool dragOnSurfaces = true;

	public Text PointerPosInfo;

	public Text DragBegin;

	public Text DragEnd;

	public Text Draging;

	private GameObject m_DraggingIcon;

	private RectTransform m_DraggingPlane;
}
