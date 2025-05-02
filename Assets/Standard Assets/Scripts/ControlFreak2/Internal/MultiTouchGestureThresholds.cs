// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.MultiTouchGestureThresholds
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class MultiTouchGestureThresholds : TouchGestureThresholds
	{
		public MultiTouchGestureThresholds()
		{
			this.twistMinDistCm = 0.5f;
			this._twistMinDistPx = 2f;
			this.twistAngleThresh = 2f;
			this.twistDigitalThresh = 25f;
			this.twistAnalogRange = 45f;
			this.twistDeltaRange = 90f;
			this.pinchAnalogRangeCm = 1.5f;
			this._pinchAnalogRangePx = 30f;
			this.pinchDeltaRangeCm = 2.5f;
			this._pinchDeltaRangePx = 60f;
			this.pinchDistThreshCm = 0.2f;
			this._pinchDistThreshPx = 2f;
			this.pinchDigitalThreshCm = 1f;
			this._pinchDigitalThreshPx = 20f;
			this.pinchScrollStepCm = 0.5f;
			this._pinchScrollStepPx = 10f;
			this.twistScrollStep = 30f;
			this.multiFingerTapMaxFingerDistCm = 3f;
			this._multiFingerTapMaxFingerDistPx = 90f;
		}

		public float pinchDistThreshPx
		{
			get
			{
				return this._pinchDistThreshPx;
			}
		}

		public float pinchDistThreshPxSq
		{
			get
			{
				return this._pinchDistThreshPx * this._pinchDistThreshPx;
			}
		}

		public float pinchAnalogRangePx
		{
			get
			{
				return this._pinchAnalogRangePx;
			}
		}

		public float pinchAnalogRangePxSq
		{
			get
			{
				return this._pinchAnalogRangePx * this._pinchAnalogRangePx;
			}
		}

		public float pinchDeltaRangePx
		{
			get
			{
				return this._pinchDeltaRangePx;
			}
		}

		public float pinchDeltaRangePxSq
		{
			get
			{
				return this._pinchDeltaRangePx * this._pinchDeltaRangePx;
			}
		}

		public float pinchDigitalThreshPx
		{
			get
			{
				return this._pinchDigitalThreshPx;
			}
		}

		public float pinchDigitalThreshPxSq
		{
			get
			{
				return this._pinchDigitalThreshPx * this._pinchDigitalThreshPx;
			}
		}

		public float pinchScrollStepPx
		{
			get
			{
				return this._pinchScrollStepPx;
			}
		}

		public float pinchScrollStepPxSq
		{
			get
			{
				return this._pinchScrollStepPx * this._pinchScrollStepPx;
			}
		}

		public float twistMinDistPx
		{
			get
			{
				return this._twistMinDistPx;
			}
		}

		public float twistMinDistPxSq
		{
			get
			{
				return this._twistMinDistPx * this._twistMinDistPx;
			}
		}

		public float multiFingerTapMaxFingerDistPx
		{
			get
			{
				return this._multiFingerTapMaxFingerDistPx;
			}
		}

		public float multiFingerTapMaxFingerDistPxSq
		{
			get
			{
				return this._multiFingerTapMaxFingerDistPx * this._multiFingerTapMaxFingerDistPx;
			}
		}

		protected override void OnRecalc(float dpcm)
		{
			base.OnRecalc(dpcm);
			this._pinchDistThreshPx = Mathf.Max(2f, this.pinchDistThreshCm * dpcm);
			this._pinchDigitalThreshPx = Mathf.Max(2f, this.pinchDigitalThreshCm * dpcm);
			this._twistMinDistPx = Mathf.Max(2f, this.twistMinDistCm * dpcm);
			this._pinchAnalogRangePx = Mathf.Max(2f, this.pinchAnalogRangeCm * dpcm);
			this._pinchDeltaRangePx = Mathf.Max(2f, this.pinchDeltaRangeCm * dpcm);
			this._multiFingerTapMaxFingerDistPx = Mathf.Max(2f, this.multiFingerTapMaxFingerDistCm * dpcm);
			this._pinchScrollStepPx = Mathf.Max(2f, this.pinchScrollStepCm * dpcm);
		}

		public float pinchDistThreshCm;

		public float pinchAnalogRangeCm;

		public float pinchDeltaRangeCm;

		public float pinchDigitalThreshCm;

		public float pinchScrollStepCm;

		public float pinchScrollMagnet;

		public float twistMinDistCm;

		public float twistAngleThresh;

		public float twistAnalogRange;

		public float twistDeltaRange;

		public float twistDigitalThresh;

		public float twistScrollStep;

		public float twistScrollMagnet;

		public float multiFingerTapMaxFingerDistCm;

		private float _pinchDistThreshPx;

		private float _pinchAnalogRangePx;

		private float _pinchDeltaRangePx;

		private float _pinchDigitalThreshPx;

		private float _pinchScrollStepPx;

		private float _twistMinDistPx;

		private float _multiFingerTapMaxFingerDistPx;
	}
}
