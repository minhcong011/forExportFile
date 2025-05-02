// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchTrackPadSpriteAnimator
using System;
using ControlFreak2.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	[ExecuteInEditMode]
	public class TouchTrackPadSpriteAnimator : TouchControlSpriteAnimatorBase, ISpriteAnimator
	{
		public TouchTrackPadSpriteAnimator() : base(typeof(TouchTrackPad))
		{
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

		public void SetStateSprite(TouchTrackPadSpriteAnimator.ControlState state, Sprite sprite)
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

		public void SetStateColor(TouchTrackPadSpriteAnimator.ControlState state, Color color)
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

		public SpriteConfig GetStateSpriteConfig(TouchTrackPadSpriteAnimator.ControlState state)
		{
			if (state == TouchTrackPadSpriteAnimator.ControlState.Neutral)
			{
				return this.spriteNeutral;
			}
			if (state != TouchTrackPadSpriteAnimator.ControlState.Pressed)
			{
				return null;
			}
			return this.spritePressed;
		}

		protected override void OnInitComponent()
		{
			base.OnInitComponent();
		}

		protected override void OnUpdateAnimator(bool skipAnim)
		{
			TouchTrackPad touchTrackPad = (TouchTrackPad)this.sourceControl;
			if (touchTrackPad == null || this.image == null)
			{
				return;
			}
			SpriteConfig spriteConfig = null;
			if (touchTrackPad.Pressed() && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spritePressed;
			}
			if (spriteConfig == null || !spriteConfig.enabled)
			{
				spriteConfig = this.spriteNeutral;
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

		public enum ControlState
		{
			Neutral,
			Pressed,
			All
		}
	}
}
