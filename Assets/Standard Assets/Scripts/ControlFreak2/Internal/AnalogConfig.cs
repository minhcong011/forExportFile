// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.AnalogConfig
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class AnalogConfig
	{
		public AnalogConfig()
		{
			this.analogDeadZone = 0.1f;
			this.analogEndZone = 1f;
			this.analogRangeStartValue = 0f;
			this.digitalEnterThresh = 0.5f;
			this.digitalLeaveThresh = 0.2f;
			this.digitalToAnalogPressSpeed = 0.5f;
			this.digitalToAnalogReleaseSpeed = 0.5f;
			this.ramp = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.5f, 0.5f),
				new Keyframe(1f, 1f)
			});
		}

		public float GetAnalogVal(float rawVal)
		{
			float num = Mathf.Abs(rawVal);
			if (num <= this.analogDeadZone)
			{
				return 0f;
			}
			if (num >= this.analogEndZone)
			{
				return (float)((rawVal < 0f) ? -1 : 1);
			}
			float num2 = (num - this.analogDeadZone) / (this.analogEndZone - this.analogDeadZone);
			if (this.useRamp && this.ramp != null)
			{
				num2 = Mathf.Clamp01(this.ramp.Evaluate(num2));
			}
			else
			{
				num2 = Mathf.Lerp(this.analogRangeStartValue, 1f, num2);
			}
			return (rawVal < 0f) ? (-num2) : num2;
		}

		public int GetSignedDigitalVal(float rawVal, int prevDigiVal)
		{
			if (rawVal >= 0f)
			{
				return (rawVal <= ((prevDigiVal != 1) ? this.digitalEnterThresh : this.digitalLeaveThresh)) ? 0 : 1;
			}
			return (rawVal >= ((prevDigiVal != -1) ? (-this.digitalEnterThresh) : (-this.digitalLeaveThresh))) ? 0 : -1;
		}

		public bool GetDigitalVal(float rawVal, bool prevDigiVal)
		{
			return rawVal > ((!prevDigiVal) ? this.digitalEnterThresh : this.digitalLeaveThresh);
		}

		public float AnimateDigitalToAnalog(float curVal, float targetVal, bool pressed)
		{
			return CFUtils.SmoothTowards(curVal, targetVal, ((!pressed) ? this.digitalToAnalogReleaseSpeed : this.digitalToAnalogPressSpeed) * 0.2f, CFUtils.realDeltaTime, 0.001f, 0.75f);
		}

		public float analogDeadZone;

		public float analogEndZone;

		public float analogRangeStartValue;

		public float digitalEnterThresh;

		public float digitalLeaveThresh;

		public float digitalToAnalogPressSpeed;

		public float digitalToAnalogReleaseSpeed;

		public const float DIGITAL_TO_ANALOG_SMOOTHING_TIME = 0.2f;

		public bool useRamp;

		public AnimationCurve ramp;

		private const float MIN_DIGITAL_DEADZONE = 0.05f;

		private const float MAX_DIGITAL_DEADZONE = 0.9f;

		private const float MIN_ANALOG_DEADZONE = 0f;

		private const float MAX_ANALOG_DEADZONE = 0.9f;

		private const float MIN_ANALOG_ENDZONE = 0.1f;

		private const float MAX_ANALOG_ENDZONE = 1f;

		private const float MAX_DIGI_LEAVE_MARGIN = 0.1f;

		private const float MIN_DIGI_LEAVE_THRESH = 0.1f;
	}
}
