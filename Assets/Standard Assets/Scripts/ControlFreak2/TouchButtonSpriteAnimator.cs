// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchButtonSpriteAnimator
using System;
using ControlFreak2.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	[ExecuteInEditMode]
	public class TouchButtonSpriteAnimator : TouchControlSpriteAnimatorBase, ISpriteAnimator
	{
		public TouchButtonSpriteAnimator() : base(typeof(TouchButton))
		{
			this.spritePressed = new SpriteConfig(true, false, 1.2f);
			this.spriteToggled = new SpriteConfig(false, false, 1.1f);
			this.spriteToggledAndPressed = new SpriteConfig(false, false, 1.3f);
			this.spritePressed.scale = 1.25f;
			this.spriteToggled.scale = 1.1f;
			this.spriteToggledAndPressed.scale = 1.3f;
		}

		public void SetSprite(Sprite sprite)
		{
			this.spriteNeutral.sprite = sprite;
			this.spritePressed.sprite = sprite;
			this.spriteToggled.sprite = sprite;
			this.spriteToggledAndPressed.sprite = sprite;
		}

		public void SetColor(Color color)
		{
			this.spriteNeutral.color = color;
			this.spritePressed.color = color;
			this.spriteToggled.color = color;
			this.spriteToggledAndPressed.color = color;
		}

		public void SetStateSprite(TouchButtonSpriteAnimator.ControlState state, Sprite sprite)
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

		public void SetStateColor(TouchButtonSpriteAnimator.ControlState state, Color color)
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

		public SpriteConfig GetStateSpriteConfig(TouchButtonSpriteAnimator.ControlState state)
		{
			switch (state)
			{
			case TouchButtonSpriteAnimator.ControlState.Neutral:
				return this.spriteNeutral;
			case TouchButtonSpriteAnimator.ControlState.Pressed:
				return this.spritePressed;
			case TouchButtonSpriteAnimator.ControlState.Toggled:
				return this.spriteToggled;
			case TouchButtonSpriteAnimator.ControlState.ToggledAndPressed:
				return this.spriteToggledAndPressed;
			default:
				return null;
			}
		}

		protected override void OnInitComponent()
		{
			base.OnInitComponent();
		}

		protected override void OnUpdateAnimator(bool skipAnim)
		{
			TouchButton touchButton = (TouchButton)this.sourceControl;
			if (touchButton == null || this.image == null)
			{
				return;
			}
			bool flag = touchButton.Pressed();
			bool flag2 = touchButton.Toggled();
			SpriteConfig spriteConfig = null;
			if (flag && flag2 && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteToggledAndPressed;
			}
			if (flag2 && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteToggled;
			}
			if (flag && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spritePressed;
			}
			if (spriteConfig == null || !spriteConfig.enabled)
			{
				spriteConfig = this.spriteNeutral;
			}
			base.BeginSpriteAnim((spriteConfig != null) ? spriteConfig : this.spriteNeutral, false, false);
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
			optimizer.AddSprite(this.spriteToggled.sprite);
			optimizer.AddSprite(this.spriteToggledAndPressed.sprite);
		}

		void ISpriteAnimator.OnSpriteOptimization(ISpriteOptimizer optimizer)
		{
			this.spriteNeutral.sprite = optimizer.GetOptimizedSprite(this.spriteNeutral.sprite);
			this.spritePressed.sprite = optimizer.GetOptimizedSprite(this.spritePressed.sprite);
			this.spriteToggled.sprite = optimizer.GetOptimizedSprite(this.spriteToggled.sprite);
			this.spriteToggledAndPressed.sprite = optimizer.GetOptimizedSprite(this.spriteToggledAndPressed.sprite);
		}

		public SpriteConfig spritePressed;

		public SpriteConfig spriteToggled;

		public SpriteConfig spriteToggledAndPressed;

		public enum ControlState
		{
			Neutral,
			Pressed,
			Toggled,
			ToggledAndPressed,
			All
		}
	}
}
