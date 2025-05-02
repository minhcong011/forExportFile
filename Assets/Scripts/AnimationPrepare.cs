// dnSpy decompiler from Assembly-CSharp.dll class: AnimationPrepare
using System;
using UnityEngine;

public class AnimationPrepare : MonoBehaviour
{
	private void Start()
	{
		foreach (AnimationEventData animationEventData in this.animationEvents)
		{
			foreach (AnimationEventData.EventData eventData in animationEventData.events)
			{
				AnimationEvent animationEvent = new AnimationEvent();
				animationEvent.time = eventData.eventTime;
				animationEvent.functionName = eventData.functionName;
				animationEventData.motionClip.AddEvent(animationEvent);
			}
		}
	}

	public AnimationEventData[] animationEvents;
}
