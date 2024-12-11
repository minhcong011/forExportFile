using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
	public Canvas canvas;

	private Vector2 startPos;

	private void Start()
	{
		startPos = base.gameObject.transform.position;
	}

	public void Drop()
	{
		base.gameObject.transform.position = startPos;
	}

	public void DragHandler(BaseEventData data)
	{
		PointerEventData pointerEventData = (PointerEventData)data;
		RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out var localPoint);
		base.transform.position = canvas.transform.TransformPoint(localPoint);
	}
}
