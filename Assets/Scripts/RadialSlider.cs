// dnSpy decompiler from Assembly-CSharp.dll class: RadialSlider
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		base.StartCoroutine("TrackPointer");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		base.StopCoroutine("TrackPointer");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.isPointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		this.isPointerDown = false;
	}

	private IEnumerator TrackPointer()
	{
		GraphicRaycaster ray = base.GetComponentInParent<GraphicRaycaster>();
		StandaloneInputModule input = UnityEngine.Object.FindObjectOfType<StandaloneInputModule>();
		Text text = base.GetComponentInChildren<Text>();
		if (ray != null && input != null)
		{
			while (Application.isPlaying)
			{
				if (this.isPointerDown)
				{
					Vector2 vector;
					RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, UnityEngine.Input.mousePosition, ray.eventCamera, out vector);
					float num = (Mathf.Atan2(-vector.y, vector.x) * 180f / 3.14159274f + 180f) / 360f;
					base.GetComponent<Image>().fillAmount = num;
					text.text = ((int)(num * 360f)).ToString();
					this.img.localEulerAngles = new Vector3(0f, 0f, num * -360f);
				}
				yield return 0;
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("Could not find GraphicRaycaster and/or StandaloneInputModule");
		}
		yield break;
	}

	private bool isPointerDown;

	public Transform img;
}
