// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TiltState
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class TiltState
	{
		public TiltState()
		{
			this.analogRange = new Vector2(25f, 25f);
			this.anglesNeutral = new Vector2(0f, 135f);
			this.calibrated = false;
			this.Reset();
		}

		public void Reset()
		{
			this.calibrated = false;
			this.analogCur = Vector2.zero;
			this.anglesCur = Vector2.zero;
		}

		public void InternalApplyVector(Vector3 v)
		{
			this.nextFrVec += v;
		}

		public static bool IsAvailable()
		{
			return Input.acceleration != Vector3.zero;
		}

		public void Calibate()
		{
			this.setRefAnglesFlag = true;
		}

		public bool IsCalibrated()
		{
			return this.calibrated;
		}

		public void Update()
		{
			this.curVec = this.nextFrVec;
			this.nextFrVec = Vector3.zero;
			float sqrMagnitude = this.curVec.sqrMagnitude;
			if (sqrMagnitude < 1E-06f)
			{
				this.curVec = Vector3.zero;
				this.anglesAbsRaw = this.anglesNeutral;
			}
			else
			{
				if (Mathf.Abs(sqrMagnitude - 1f) > 0.001f)
				{
					this.curVec.Normalize();
				}
				this.anglesAbsRaw.x = Mathf.Atan2(this.curVec.x, Mathf.Max(Mathf.Abs(this.curVec.y), Mathf.Abs(this.curVec.z))) * 57.29578f;
				this.anglesAbsRaw.y = Mathf.Atan2(-this.curVec.y, this.curVec.z) * 57.29578f;
			}
			this.anglesAbsCur = this.anglesAbsRaw;
			if (this.setRefAnglesFlag)
			{
				this.calibrated = true;
				this.setRefAnglesFlag = false;
				this.anglesNeutral = this.anglesAbsRaw;
				this.anglesNeutral.x = 0f;
			}
			this.anglesCur = this.anglesAbsCur - this.anglesNeutral;
			if (!this.calibrated)
			{
				this.anglesCur.y = 0f;
			}
			for (int i = 0; i < 2; i++)
			{
				float num = this.anglesCur[i];
				float num2 = Mathf.Abs(num);
				float num3 = this.analogRange[i];
				this.analogCur[i] = ((num2 < num3) ? (num2 / num3) : 1f) * (float)((num >= 0f) ? 1 : -1);
			}
		}

		public Vector2 GetAngles()
		{
			return this.anglesCur;
		}

		public Vector2 GetAnalog()
		{
			return this.analogCur;
		}

		public Vector2 analogRange;

		private Vector3 curVec;

		private Vector3 nextFrVec;

		private Vector2 anglesNeutral;

		private Vector2 anglesAbsCur;

		private Vector2 anglesAbsRaw;

		private Vector2 anglesCur;

		private Vector2 analogCur;

		private bool calibrated;

		private bool setRefAnglesFlag;
	}
}
