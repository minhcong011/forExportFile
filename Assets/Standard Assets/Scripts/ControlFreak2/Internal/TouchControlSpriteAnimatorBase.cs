// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TouchControlSpriteAnimatorBase
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2.Internal
{
	[ExecuteInEditMode]
	public abstract class TouchControlSpriteAnimatorBase : TouchControlAnimatorBase
	{
		public TouchControlSpriteAnimatorBase(Type sourceType) : base(sourceType)
		{
			this.spriteNeutral = new SpriteConfig();
			this.spriteNeutral.color = new Color(1f, 1f, 1f, 0.33f);
		}

		protected override void OnInitComponent()
		{
			base.OnInitComponent();
			this.rectTr = base.GetComponent<RectTransform>();
			if (this.image == null)
			{
				this.image = base.GetComponent<Image>();
				if (this.image == null)
				{
					this.image = base.gameObject.AddComponent<Image>();
				}
			}
			if (this.image != null)
			{
				this.image.raycastTarget = false;
			}
			this.canvasGroup = base.gameObject.GetComponent<CanvasGroup>();
			Transform transform = base.transform;
			this.initialTransl = transform.localPosition;
			this.initialScale = transform.localScale;
			this.initialRotation = transform.localRotation;
			this.animOffsetCur = (this.animOffsetStart = (this.extraOffset = Vector2.zero));
			this.animScaleCur = (this.animScaleStart = (this.extraScale = Vector2.one));
			this.animRotationCur = (this.animRotationStart = (this.extraRotation = 0f));
			this.animColorStart = (this.animColorCur = Color.white);
			this.BeginSpriteAnim(this.spriteNeutral, true, true);
		}

		protected override void OnDisableComponent()
		{
			base.transform.localPosition = this.initialTransl;
			base.transform.localScale = this.initialScale;
			base.transform.localRotation = this.initialRotation;
			base.OnDisableComponent();
		}

		protected void BeginSpriteAnim(SpriteConfig spriteConfig, bool skipAnim, bool forceStart = false)
		{
			if (this.curSprite == spriteConfig && !spriteConfig.oneShotState && !skipAnim)
			{
				return;
			}
			if (this.curSprite != null && this.curSprite.oneShotState && !skipAnim && !forceStart)
			{
				this.nextSprite = spriteConfig;
				return;
			}
			this.curSprite = spriteConfig;
			this.spriteAnimElapsed = 0f;
			if (!skipAnim)
			{
				this.animColorStart = this.animColorCur;
			}
			else
			{
				this.animColorStart = (this.animColorCur = spriteConfig.color);
			}
			if (CFUtils.editorStopped)
			{
				return;
			}
			if (!skipAnim)
			{
				this.animOffsetStart = this.animOffsetCur;
				this.animScaleStart = this.animScaleCur;
				this.animRotationStart = this.animRotationCur;
				if (this.curSprite.resetScale)
				{
					this.animScaleStart = Vector2.one;
				}
				if (this.curSprite.resetOffset)
				{
					this.animOffsetStart = Vector2.zero;
				}
				if (this.curSprite.resetRotation)
				{
					this.animRotationStart = 0f;
				}
			}
			else
			{
				this.animOffsetStart = (this.animOffsetCur = spriteConfig.offset);
				this.animScaleStart = (this.animScaleCur = Vector2.one * spriteConfig.scale);
				this.animRotationStart = (this.animRotationCur = spriteConfig.rotation);
				this.ApplySpriteAnimation();
			}
		}

		protected void UpdateSpriteAnimation(bool skipAnim)
		{
			this.spriteAnimElapsed += CFUtils.realDeltaTimeClamped;
			if (this.curSprite.oneShotState && this.spriteAnimElapsed >= this.curSprite.duration)
			{
				this.BeginSpriteAnim((this.nextSprite == null) ? this.spriteNeutral : this.nextSprite, skipAnim, true);
			}
			this.animColorCur = Color.Lerp(this.animColorStart, this.curSprite.color, this.GetAnimLerpFactor((!skipAnim) ? (this.curSprite.colorTransitionFactor * this.curSprite.baseTransitionTime) : 0f));
			this.animColorCur = CFUtils.ScaleColorAlpha(this.animColorCur, this.sourceControl.GetAlpha());
			if (!CFUtils.editorStopped && !base.IsIllegallyAttachedToSource())
			{
				this.animOffsetCur = Vector2.Lerp(this.animOffsetStart, this.curSprite.offset, this.GetAnimLerpFactor((!skipAnim) ? (this.curSprite.offsetTransitionFactor * this.curSprite.baseTransitionTime) : 0f));
				this.animScaleCur = Vector2.Lerp(this.animScaleStart, Vector2.one * this.curSprite.scale, this.GetAnimLerpFactor((!skipAnim) ? (this.curSprite.scaleTransitionFactor * this.curSprite.baseTransitionTime) : 0f));
				this.animRotationCur = Mathf.LerpAngle(this.animRotationStart, this.curSprite.rotation, this.GetAnimLerpFactor((!skipAnim) ? (this.curSprite.rotationTransitionFactor * this.curSprite.baseTransitionTime) : 0f));
			}
			this.ApplySpriteAnimation();
		}

		public void ApplySpriteAnimation()
		{
			if (this.animColorCur.a > 1E-05f != this.image.enabled)
			{
				this.image.enabled = !this.image.enabled;
			}
			Color color = this.animColorCur;
			if (this.canvasGroup != null)
			{
				this.canvasGroup.alpha = this.animColorCur.a;
				color.a = 1f;
			}
			this.image.color = color;
			this.image.sprite = ((!(this.curSprite.sprite == null)) ? this.curSprite.sprite : this.spriteNeutral.sprite);
			if (!CFUtils.editorStopped && !base.IsIllegallyAttachedToSource())
			{
				Rect localRect = this.sourceControl.GetLocalRect();
				base.transform.localPosition = this.initialTransl + (Vector3)Vector2.Scale(localRect.size * 0.5f, this.animOffsetCur + this.extraOffset);
				base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -this.animRotationCur + this.extraRotation)) * this.initialRotation;
				base.transform.localScale = Vector3.Scale(this.initialScale, Vector2.Scale(this.animScaleCur, this.extraScale));
			}
		}

		private float GetAnimLerpFactor(float duration)
		{
			if (this.spriteAnimElapsed > duration || duration < 1E-05f)
			{
				return 1f;
			}
			return this.spriteAnimElapsed / duration;
		}

		protected RectTransform rectTr;

		protected Image image;

		protected CanvasGroup canvasGroup;

		protected Vector3 initialTransl;

		protected Vector3 initialScale;

		protected Quaternion initialRotation;

		public SpriteConfig spriteNeutral;

		[NonSerialized]
		private SpriteConfig curSprite;

		[NonSerialized]
		private SpriteConfig nextSprite;

		private float spriteAnimElapsed;

		protected Vector2 animOffsetStart;

		protected Vector2 animOffsetCur;

		protected Vector2 animScaleStart;

		protected Vector2 animScaleCur;

		protected float animRotationStart;

		protected float animRotationCur;

		protected Color animColorStart;

		protected Color animColorCur;

		protected Vector2 extraOffset;

		protected Vector2 extraScale;

		protected float extraRotation;
	}
}
