// dnSpy decompiler from Assembly-CSharp.dll class: CharacterRotation
using System;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Vector2 deltaPosition = UnityEngine.Input.GetTouch(0).deltaPosition;
			base.transform.Rotate(new Vector3(0f, -1f * deltaPosition.x * this.speed * 0.8f * Time.deltaTime, 0f));
		}
		else
		{
			base.transform.Rotate(new Vector3(0f, -1f * this.speed * 0.1f * Time.deltaTime, 0f));
		}
	}

	public float speed = 2f;
}
