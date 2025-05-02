// dnSpy decompiler from Assembly-CSharp.dll class: AimControllerCanvas
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AimControllerCanvas : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
{
	public void OnBeginDrag(PointerEventData eventData)
	{
		Canvas canvas = AimControllerCanvas.FindInParents<Canvas>(base.gameObject);
		if (canvas == null)
		{
			return;
		}
		if (this.dragOnPlane)
		{
			this.TouchPlane = (base.transform as RectTransform);
		}
		else
		{
			this.TouchPlane = (canvas.transform as RectTransform);
		}
		this.SetDraggedPosition(eventData);
		this.controllerPositionNext = new Vector2(eventData.position.x, eventData.position.y);
		this.controllerPositionTemp = this.controllerPositionNext;
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.SetDraggedPosition(eventData);
		this.controllerPositionNext = new Vector2(eventData.position.x, eventData.position.y);
		Vector2 vector = this.controllerPositionNext - this.controllerPositionTemp;
		this.rotationX = this.rotationXtemp + vector.x * this.sensitivity * Time.deltaTime;
		this.rotationY = this.rotationYtemp + vector.y * this.sensitivity * Time.deltaTime;
		if (this.rotationX >= 360f)
		{
			this.rotationX = 0f;
			this.rotationXtemp = 0f;
		}
		if (this.rotationX <= -360f)
		{
			this.rotationX = 0f;
			this.rotationXtemp = 0f;
		}
		this.rotationX = AimControllerCanvas.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
		this.rotationY = AimControllerCanvas.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
		this.rotationXtemp = this.rotationX;
		this.rotationYtemp = this.rotationY;
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public void myDragPlace(bool e)
	{
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.SetDraggedPosition(eventData);
		this.controllerPositionNext = new Vector2(eventData.position.x, eventData.position.y);
		this.controllerPositionTemp = this.controllerPositionNext;
	}

	private void SetDraggedPosition(PointerEventData data)
	{
		if (this.dragOnPlane && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
		{
			this.TouchPlane = (data.pointerEnter.transform as RectTransform);
		}
		RectTransform component = this.TouchPlane.GetComponent<RectTransform>();
		Vector3 vector;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.TouchPlane, data.position, data.pressEventCamera, out vector))
		{
			this.PointerPosInfo.text = string.Concat(new object[]
			{
				"X: ",
				data.position.x,
				"\nY: ",
				data.position.y
			});
			this.DragPosition = new Vector2(data.position.x, data.position.y);
		}
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

	public bool dragOnPlane = true;

	public RectTransform TouchPlane;

	public Text PointerPosInfo;

	public Vector2 DragPosition;

	public Vector2 controllerPositionNext;

	public Vector2 controllerPositionTemp;

	public float rotationX;

	public float rotationXtemp;

	public float rotationY;

	public float rotationYtemp;

	public float sensitivity;

	public float minimumX;

	public float maximumX;

	public float minimumY;

	public float maximumY;
}
