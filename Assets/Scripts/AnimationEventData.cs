// dnSpy decompiler from Assembly-CSharp.dll class: AnimationEventData
using System;
using UnityEngine;

[Serializable]
public class AnimationEventData
{
	public AnimationClip motionClip;

	public AnimationEventData.EventData[] events;

	[Serializable]
	public class EventData
	{
		public float eventTime;

		public string functionName;
	}
}
