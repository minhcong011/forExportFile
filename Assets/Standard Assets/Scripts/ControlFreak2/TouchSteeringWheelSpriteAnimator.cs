// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchSteeringWheelSpriteAnimator
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	[RequireComponent(typeof(RectTransform))]
	public class TouchSteeringWheelSpriteAnimator : TouchControlSpriteAnimatorBase, ISpriteAnimator
	{
		public TouchSteeringWheelSpriteAnimator() : base(typeof(TouchSteeringWheel))
		{
			this.rotationRange = 45f;
			this.rotationSmoothingTime = 0.05f;
			this.spritePressed = new SpriteConfig(true, false, 1.2f);
		}

		public void SetSprite(Sprite sprite)
		{
			this.spriteNeutral.sprite = sprite;
			this.spritePressed.sprite = sprite;
		}

		public void SetColor(Color color)
		{
			this.spriteNeutral.color = color;
			this.spritePressed.color = color;
		}

		public void SetStateSprite(TouchSteeringWheelSpriteAnimator.ControlState state, Sprite sprite)
		{
			SpriteConfig stateSpriteConfig = this.GetStateSpriteConfig(state);
			if (stateSpriteConfig == null)
			{
				this.SetSprite(sprite);
			}
			else
			{
				stateSpriteConfig.sprite = sprite;
			}
		}

		public void SetStateColor(TouchSteeringWheelSpriteAnimator.ControlState state, Color color)
		{
			SpriteConfig stateSpriteConfig = this.GetStateSpriteConfig(state);
			if (stateSpriteConfig == null)
			{
				this.SetColor(color);
			}
			else
			{
				stateSpriteConfig.color = color;
			}
		}

		public SpriteConfig GetStateSpriteConfig(TouchSteeringWheelSpriteAnimator.ControlState state)
		{
			if (state == TouchSteeringWheelSpriteAnimator.ControlState.Neutral)
			{
				return this.spriteNeutral;
			}
			if (state != TouchSteeringWheelSpriteAnimator.ControlState.Pressed)
			{
				return null;
			}
			return this.spritePressed;
		}

		protected override void OnInitComponent()
		{
			base.OnInitComponent();
			if (this.image.sprite != null && this.spriteNeutral.sprite == null && this.spritePressed.sprite == null)
			{
				this.spriteNeutral.sprite = this.image.sprite;
				this.spritePressed.sprite = this.image.sprite;
			}
		}

		protected override void OnUpdateAnimator(bool skipAnim)
		{
			if (this.sourceControl == null || this.image == null)
			{
				return;
			}
			TouchSteeringWheel touchSteeringWheel = (TouchSteeringWheel)this.sourceControl;
			SpriteConfig spriteConfig = null;
			if (touchSteeringWheel.Pressed() && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spritePressed;
			}
			if (spriteConfig == null || !spriteConfig.enabled)
			{
				spriteConfig = this.spriteNeutral;
			}
			if (!CFUtils.editorStopped && !base.IsIllegallyAttachedToSource())
			{
				this.extraRotation = CFUtils.SmoothTowardsAngle(this.extraRotation, -(touchSteeringWheel.GetValue() * ((touchSteeringWheel.wheelMode != TouchSteeringWheel.WheelMode.Swipe) ? touchSteeringWheel.maxTurnAngle : this.rotationRange)), this.rotationSmoothingTime, CFUtils.realDeltaTimeClamped, 0.001f, 0.75f);
			}
			else
			{
				this.extraRotation = 0f;
			}
			base.BeginSpriteAnim(spriteConfig, skipAnim, false);
			base.UpdateSpriteAnimation(skipAnim);
		}

		MonoBehaviour ISpriteAnimator.GetComponent()
		{
			return this;
		}

		void ISpriteAnimator.AddUsedSprites(ISpriteOptimizer optimizer)
		{
			optimizer.AddSprite(this.spriteNeutral.sprite);
			optimizer.AddSprite(this.spritePressed.sprite);
		}

		void ISpriteAnimator.OnSpriteOptimization(ISpriteOptimizer optimizer)
		{
			this.spriteNeutral.sprite = optimizer.GetOptimizedSprite(this.spriteNeutral.sprite);
			this.spritePressed.sprite = optimizer.GetOptimizedSprite(this.spritePressed.sprite);
		}

		public SpriteConfig spritePressed;

		public float rotationRange = 45f;

		public float rotationSmoothingTime = 0.1f;

		public enum ControlState
		{
			Neutral,
			Pressed,
			All
		}
	}
}
