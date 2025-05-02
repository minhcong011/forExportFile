// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TouchGestureThresholds
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class TouchGestureThresholds
	{
		public TouchGestureThresholds()
		{
			this.storedDpi = -1f;
			this.tapMoveThreshCm = 0.1f;
			this._tapMoveThreshPx = 10f;
			this.tapPosThreshCm = 0.5f;
			this._tapPosThreshPx = 30f;
			this.dragThreshCm = 0.25f;
			this._dragThreshPx = 10f;
			this.swipeSegLenCm = 0.5f;
			this._swipeSegLenPx = 10f;
			this.swipeJoystickRadCm = 1.5f;
			this._swipeJoystickRadPx = 15f;
			this.scrollMagnetFactor = 0.1f;
			this.scrollThreshCm = 0.5f;
			this._scrollThreshPx = 30f;
			this.tapMaxDur = 0.3f;
			this.multiTapMaxTimeGap = 0.4f;
			this.longPressMinTime = 0.5f;
			this.longTapMaxDuration = 1f;
		}

		public float tapMoveThreshPx
		{
			get
			{
				return this._tapMoveThreshPx;
			}
		}

		public float tapMoveThreshPxSq
		{
			get
			{
				return this._tapMoveThreshPx * this._tapMoveThreshPx;
			}
		}

		public float tapPosThreshPx
		{
			get
			{
				return this._tapPosThreshPx;
			}
		}

		public float tapPosThreshPxSq
		{
			get
			{
				return this._tapPosThreshPx * this._tapPosThreshPx;
			}
		}

		public float dragThreshPx
		{
			get
			{
				return this._dragThreshPx;
			}
		}

		public float dragThreshPxSq
		{
			get
			{
				return this._dragThreshPx * this._dragThreshPx;
			}
		}

		public float swipeSegLenPx
		{
			get
			{
				return this._swipeSegLenPx;
			}
		}

		public float swipeSegLenPxSq
		{
			get
			{
				return this._swipeSegLenPx * this._swipeSegLenPx;
			}
		}

		public float swipeJoystickRadPx
		{
			get
			{
				return this._swipeJoystickRadPx;
			}
		}

		public float swipeJoystickRadPxSq
		{
			get
			{
				return this._swipeJoystickRadPx * this._swipeJoystickRadPx;
			}
		}

		public float scrollThreshPx
		{
			get
			{
				return this._scrollThreshPx;
			}
		}

		public float scrollThreshPxSq
		{
			get
			{
				return this._scrollThreshPx * this._scrollThreshPx;
			}
		}

		public void Recalc(float dpi)
		{
			if (dpi <= 0.0001f)
			{
				dpi = 96f;
			}
			if (this.storedDpi == dpi)
			{
				return;
			}
			this.storedDpi = dpi;
			this.OnRecalc(dpi / 2.54f);
		}

		protected virtual void OnRecalc(float dpcm)
		{
			this._tapMoveThreshPx = Mathf.Max(2f, this.tapMoveThreshCm * dpcm);
			this._tapPosThreshPx = Mathf.Max(2f, this.tapPosThreshCm * dpcm);
			this._dragThreshPx = Mathf.Max(2f, this.dragThreshCm * dpcm);
			this._swipeSegLenPx = Mathf.Max(2f, this.swipeSegLenCm * dpcm);
			this._swipeJoystickRadPx = Mathf.Max(2f, this.swipeJoystickRadCm * dpcm);
			this._scrollThreshPx = Mathf.Max(2f, this.scrollThreshCm * dpcm);
		}

		public float tapMoveThreshCm;

		public float tapPosThreshCm;

		public float dragThreshCm;

		public float scrollThreshCm;

		public float scrollMagnetFactor;

		public float swipeSegLenCm;

		public float swipeJoystickRadCm;

		public float tapMaxDur;

		public float multiTapMaxTimeGap;

		public float longPressMinTime;

		public float longTapMaxDuration;

		private float _tapMoveThreshPx;

		private float _tapPosThreshPx;

		private float _dragThreshPx;

		private float _swipeSegLenPx;

		private float _swipeJoystickRadPx;

		private float _scrollThreshPx;

		private float storedDpi;
	}
}
