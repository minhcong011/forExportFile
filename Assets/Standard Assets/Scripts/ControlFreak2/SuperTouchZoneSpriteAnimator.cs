// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.SuperTouchZoneSpriteAnimator
using System;
using ControlFreak2.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	[ExecuteInEditMode]
	public class SuperTouchZoneSpriteAnimator : TouchControlSpriteAnimatorBase, ISpriteAnimator
	{
		public SuperTouchZoneSpriteAnimator() : base(typeof(SuperTouchZone))
		{
			this.spriteRawPress = new SpriteConfig(true, false, 1.2f);
			this.spriteNormalPress = new SpriteConfig(false, false, 1.2f);
			this.spriteLongPress = new SpriteConfig(false, false, 1.2f);
			this.spriteTap = new SpriteConfig(false, true, 1.2f);
			this.spriteDoubleTap = new SpriteConfig(false, true, 1.2f);
			this.spriteLongTap = new SpriteConfig(false, true, 1.2f);
			this.spriteNormalScrollU = new SpriteConfig(false, true, 1.2f);
			this.spriteNormalScrollR = new SpriteConfig(false, true, 1.2f);
			this.spriteNormalScrollD = new SpriteConfig(false, true, 1.2f);
			this.spriteNormalScrollL = new SpriteConfig(false, true, 1.2f);
			this.spriteLongScrollU = new SpriteConfig(false, true, 1.2f);
			this.spriteLongScrollR = new SpriteConfig(false, true, 1.2f);
			this.spriteLongScrollD = new SpriteConfig(false, true, 1.2f);
			this.spriteLongScrollL = new SpriteConfig(false, true, 1.2f);
		}

		public void SetSprite(Sprite sprite)
		{
			for (SuperTouchZoneSpriteAnimator.ControlState controlState = SuperTouchZoneSpriteAnimator.ControlState.Neutral; controlState < SuperTouchZoneSpriteAnimator.ControlState.All; controlState++)
			{
				this.GetStateSpriteConfig(controlState).sprite = sprite;
			}
		}

		public void SetColor(Color color)
		{
			for (SuperTouchZoneSpriteAnimator.ControlState controlState = SuperTouchZoneSpriteAnimator.ControlState.Neutral; controlState < SuperTouchZoneSpriteAnimator.ControlState.All; controlState++)
			{
				this.GetStateSpriteConfig(controlState).color = color;
			}
		}

		public void SetStateSprite(SuperTouchZoneSpriteAnimator.ControlState state, Sprite sprite)
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

		public void SetStateColor(SuperTouchZoneSpriteAnimator.ControlState state, Color color)
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

		public SpriteConfig GetStateSpriteConfig(SuperTouchZoneSpriteAnimator.ControlState state)
		{
			switch (state)
			{
			case SuperTouchZoneSpriteAnimator.ControlState.Neutral:
				return this.spriteNeutral;
			case SuperTouchZoneSpriteAnimator.ControlState.RawPress:
				return this.spriteRawPress;
			case SuperTouchZoneSpriteAnimator.ControlState.NormalPress:
				return this.spriteNormalPress;
			case SuperTouchZoneSpriteAnimator.ControlState.LongPress:
				return this.spriteLongPress;
			case SuperTouchZoneSpriteAnimator.ControlState.Tap:
				return this.spriteTap;
			case SuperTouchZoneSpriteAnimator.ControlState.DoubleTap:
				return this.spriteDoubleTap;
			case SuperTouchZoneSpriteAnimator.ControlState.LongTap:
				return this.spriteLongTap;
			case SuperTouchZoneSpriteAnimator.ControlState.NormalScrollU:
				return this.spriteNormalScrollU;
			case SuperTouchZoneSpriteAnimator.ControlState.NormalScrollR:
				return this.spriteNormalScrollR;
			case SuperTouchZoneSpriteAnimator.ControlState.NormalScrollD:
				return this.spriteNormalScrollD;
			case SuperTouchZoneSpriteAnimator.ControlState.NormalScrollL:
				return this.spriteNormalScrollL;
			case SuperTouchZoneSpriteAnimator.ControlState.LongScrollU:
				return this.spriteLongScrollU;
			case SuperTouchZoneSpriteAnimator.ControlState.LongScrollR:
				return this.spriteLongScrollR;
			case SuperTouchZoneSpriteAnimator.ControlState.LongScrollD:
				return this.spriteLongScrollD;
			case SuperTouchZoneSpriteAnimator.ControlState.LongScrollL:
				return this.spriteLongScrollL;
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
			SuperTouchZone superTouchZone = (SuperTouchZone)this.sourceControl;
			if (superTouchZone == null || this.image == null)
			{
				return;
			}
			Vector2 scrollDelta = superTouchZone.GetScrollDelta(1);
			SpriteConfig spriteConfig = null;
			if ((superTouchZone.JustTapped(1, 2) || superTouchZone.JustTapped(2, 2) || superTouchZone.JustTapped(3, 2)) && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteDoubleTap;
			}
			if ((superTouchZone.JustTapped(1, 1) || superTouchZone.JustTapped(2, 1) || superTouchZone.JustTapped(3, 1)) && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteTap;
			}
			if ((superTouchZone.JustLongTapped(1) || superTouchZone.JustLongTapped(2) || superTouchZone.JustLongTapped(3)) && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteLongTap;
			}
			if ((superTouchZone.PressedLong(1) || superTouchZone.PressedLong(2) || superTouchZone.PressedLong(3)) && (spriteConfig == null || !spriteConfig.enabled))
			{
				if (scrollDelta.x > 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteLongScrollR;
				}
				if (scrollDelta.x < 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteLongScrollL;
				}
				if (scrollDelta.y > 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteLongScrollU;
				}
				if (scrollDelta.y < 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteLongScrollD;
				}
				if (spriteConfig == null || !spriteConfig.enabled)
				{
					spriteConfig = this.spriteLongPress;
				}
			}
			else if ((superTouchZone.PressedNormal(1) || superTouchZone.PressedNormal(2) || superTouchZone.PressedNormal(3)) && (spriteConfig == null || !spriteConfig.enabled))
			{
				if (scrollDelta.x > 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteNormalScrollR;
				}
				if (scrollDelta.x < 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteNormalScrollL;
				}
				if (scrollDelta.y > 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteNormalScrollU;
				}
				if (scrollDelta.y < 0f && (spriteConfig == null || !spriteConfig.enabled))
				{
					spriteConfig = this.spriteNormalScrollD;
				}
				if (spriteConfig == null || !spriteConfig.enabled)
				{
					spriteConfig = this.spriteNormalPress;
				}
			}
			if ((superTouchZone.PressedRaw(1) || superTouchZone.PressedRaw(2) || superTouchZone.PressedRaw(3)) && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteRawPress;
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
			for (SuperTouchZoneSpriteAnimator.ControlState controlState = SuperTouchZoneSpriteAnimator.ControlState.Neutral; controlState < SuperTouchZoneSpriteAnimator.ControlState.All; controlState++)
			{
				optimizer.AddSprite(this.GetStateSpriteConfig(controlState).sprite);
			}
		}

		void ISpriteAnimator.OnSpriteOptimization(ISpriteOptimizer optimizer)
		{
			for (SuperTouchZoneSpriteAnimator.ControlState controlState = SuperTouchZoneSpriteAnimator.ControlState.Neutral; controlState < SuperTouchZoneSpriteAnimator.ControlState.All; controlState++)
			{
				SpriteConfig stateSpriteConfig = this.GetStateSpriteConfig(controlState);
				stateSpriteConfig.sprite = optimizer.GetOptimizedSprite(stateSpriteConfig.sprite);
			}
		}

		public SpriteConfig spriteRawPress;

		public SpriteConfig spriteNormalPress;

		public SpriteConfig spriteLongPress;

		public SpriteConfig spriteTap;

		public SpriteConfig spriteDoubleTap;

		public SpriteConfig spriteLongTap;

		public SpriteConfig spriteNormalScrollU;

		public SpriteConfig spriteNormalScrollR;

		public SpriteConfig spriteNormalScrollD;

		public SpriteConfig spriteNormalScrollL;

		public SpriteConfig spriteLongScrollU;

		public SpriteConfig spriteLongScrollR;

		public SpriteConfig spriteLongScrollD;

		public SpriteConfig spriteLongScrollL;

		public const SuperTouchZoneSpriteAnimator.ControlState ControlStateFirst = SuperTouchZoneSpriteAnimator.ControlState.Neutral;

		public const SuperTouchZoneSpriteAnimator.ControlState ControlStateCount = SuperTouchZoneSpriteAnimator.ControlState.All;

		public enum ControlState
		{
			Neutral,
			RawPress,
			NormalPress,
			LongPress,
			Tap,
			DoubleTap,
			LongTap,
			NormalScrollU,
			NormalScrollR,
			NormalScrollD,
			NormalScrollL,
			LongScrollU,
			LongScrollR,
			LongScrollD,
			LongScrollL,
			All
		}
	}
}
