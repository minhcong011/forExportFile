// dnSpy decompiler from Assembly-CSharp.dll class: UITwist
using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class UITwist : MonoBehaviour
{
	public void OnEnable()
	{
		EasyTouch.On_Twist += this.On_Twist;
	}

	public void OnDestroy()
	{
		EasyTouch.On_Twist -= this.On_Twist;
	}

	private void On_Twist(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.Rotate(new Vector3(0f, 0f, gesture.twistAngle));
		}
	}
}
