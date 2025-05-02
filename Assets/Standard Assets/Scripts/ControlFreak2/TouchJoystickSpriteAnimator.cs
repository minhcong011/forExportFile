// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchJoystickSpriteAnimator
using System;
using ControlFreak2.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	[ExecuteInEditMode]
	public class TouchJoystickSpriteAnimator : TouchControlSpriteAnimatorBase, ISpriteAnimator
	{
		public TouchJoystickSpriteAnimator() : base(typeof(TouchJoystick))
		{
			this.animateTransl = false;
			this.moveScale = new Vector2(0.5f, 0.5f);
			this.rotationMode = TouchJoystickSpriteAnimator.RotationMode.Disabled;
			this.rotationSmoothingTime = 0.01f;
			this.simpleRotationRange = 45f;
			this.lastSafeCompassAngle = 0f;
			this.spriteNeutralPressed = new SpriteConfig(true, false, 1.2f);
			this.spriteUp = new SpriteConfig(false, false, 1.2f);
			this.spriteUpRight = new SpriteConfig(false, false, 1.2f);
			this.spriteRight = new SpriteConfig(false, false, 1.2f);
			this.spriteDownRight = new SpriteConfig(false, false, 1.2f);
			this.spriteDown = new SpriteConfig(false, false, 1.2f);
			this.spriteDownLeft = new SpriteConfig(false, false, 1.2f);
			this.spriteLeft = new SpriteConfig(false, false, 1.2f);
			this.spriteUpLeft = new SpriteConfig(false, false, 1.2f);
		}

		public void SetSprite(Sprite sprite)
		{
			this.spriteNeutral.sprite = sprite;
			this.spriteNeutralPressed.sprite = sprite;
			this.spriteUp.sprite = sprite;
			this.spriteUpRight.sprite = sprite;
			this.spriteRight.sprite = sprite;
			this.spriteDownRight.sprite = sprite;
			this.spriteDown.sprite = sprite;
			this.spriteDownLeft.sprite = sprite;
			this.spriteLeft.sprite = sprite;
			this.spriteUpLeft.sprite = sprite;
		}

		public void SetColor(Color color)
		{
			this.spriteNeutral.color = color;
			this.spriteNeutralPressed.color = color;
			this.spriteUp.color = color;
			this.spriteUpRight.color = color;
			this.spriteRight.color = color;
			this.spriteDownRight.color = color;
			this.spriteDown.color = color;
			this.spriteDownLeft.color = color;
			this.spriteLeft.color = color;
			this.spriteUpLeft.color = color;
		}

		public void SetStateSprite(TouchJoystickSpriteAnimator.ControlState state, Sprite sprite)
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

		public void SetStateColor(TouchJoystickSpriteAnimator.ControlState state, Color color)
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

		public SpriteConfig GetStateSpriteConfig(TouchJoystickSpriteAnimator.ControlState state)
		{
			switch (state)
			{
			case TouchJoystickSpriteAnimator.ControlState.Neutral:
				return this.spriteNeutral;
			case TouchJoystickSpriteAnimator.ControlState.NeutralPressed:
				return this.spriteNeutralPressed;
			case TouchJoystickSpriteAnimator.ControlState.U:
				return this.spriteUp;
			case TouchJoystickSpriteAnimator.ControlState.UR:
				return this.spriteUpRight;
			case TouchJoystickSpriteAnimator.ControlState.R:
				return this.spriteRight;
			case TouchJoystickSpriteAnimator.ControlState.DR:
				return this.spriteDownRight;
			case TouchJoystickSpriteAnimator.ControlState.D:
				return this.spriteDown;
			case TouchJoystickSpriteAnimator.ControlState.DL:
				return this.spriteDownLeft;
			case TouchJoystickSpriteAnimator.ControlState.L:
				return this.spriteLeft;
			case TouchJoystickSpriteAnimator.ControlState.UL:
				return this.spriteUpLeft;
			default:
				return null;
			}
		}

		protected override void OnInitComponent()
		{
			base.OnInitComponent();
			if (this.image != null && this.image.sprite != null && this.spriteNeutral.sprite == null)
			{
				this.spriteNeutral.sprite = this.image.sprite;
				this.spriteNeutralPressed.sprite = this.image.sprite;
				this.spriteUp.sprite = this.image.sprite;
				this.spriteUpRight.sprite = this.image.sprite;
				this.spriteRight.sprite = this.image.sprite;
				this.spriteDownRight.sprite = this.image.sprite;
				this.spriteDown.sprite = this.image.sprite;
				this.spriteDownLeft.sprite = this.image.sprite;
				this.spriteLeft.sprite = this.image.sprite;
				this.spriteUpLeft.sprite = this.image.sprite;
			}
		}

		protected override void OnUpdateAnimator(bool skipAnim)
		{
			TouchJoystick touchJoystick = (TouchJoystick)this.sourceControl;
			if (touchJoystick == null || this.image == null)
			{
				return;
			}
			JoystickState state = touchJoystick.GetState();
			SpriteConfig spriteConfig = null;
			if (this.spriteMode == TouchJoystickSpriteAnimator.SpriteMode.FourWay || this.spriteMode == TouchJoystickSpriteAnimator.SpriteMode.EightWay)
			{
				Dir dir = Dir.N;
				if (this.spriteMode == TouchJoystickSpriteAnimator.SpriteMode.FourWay)
				{
					dir = state.GetDir4();
				}
				else if (this.spriteMode == TouchJoystickSpriteAnimator.SpriteMode.EightWay)
				{
					dir = state.GetDir4();
				}
				switch (dir)
				{
				case Dir.U:
					spriteConfig = this.spriteUp;
					break;
				case Dir.UR:
					spriteConfig = this.spriteUpRight;
					break;
				case Dir.R:
					spriteConfig = this.spriteRight;
					break;
				case Dir.DR:
					spriteConfig = this.spriteDownRight;
					break;
				case Dir.D:
					spriteConfig = this.spriteDown;
					break;
				case Dir.DL:
					spriteConfig = this.spriteDownLeft;
					break;
				case Dir.L:
					spriteConfig = this.spriteLeft;
					break;
				case Dir.UL:
					spriteConfig = this.spriteUpLeft;
					break;
				}
			}
			if (touchJoystick.Pressed() && (spriteConfig == null || !spriteConfig.enabled))
			{
				spriteConfig = this.spriteNeutralPressed;
			}
			if (spriteConfig == null || !spriteConfig.enabled)
			{
				spriteConfig = this.spriteNeutral;
			}
			if (!CFUtils.editorStopped && !base.IsIllegallyAttachedToSource())
			{
				Vector2 vectorEx = state.GetVectorEx(touchJoystick.shape == TouchControl.Shape.Rectangle || touchJoystick.shape == TouchControl.Shape.Square);
				if (this.animateTransl)
				{
					this.extraOffset = CFUtils.SmoothTowardsVec2(this.extraOffset, Vector2.Scale(vectorEx, this.moveScale), this.translationSmoothingTime, CFUtils.realDeltaTimeClamped, 0.0001f, 0.75f);
				}
				else
				{
					this.extraOffset = Vector2.zero;
				}
				if (this.rotationMode != TouchJoystickSpriteAnimator.RotationMode.Disabled)
				{
					float b;
					if (touchJoystick.Pressed())
					{
						Vector2 vector = state.GetVector();
						if (this.rotationMode == TouchJoystickSpriteAnimator.RotationMode.Compass)
						{
							if (vector.sqrMagnitude > 0.0001f)
							{
								this.lastSafeCompassAngle = state.GetAngle();
							}
							b = -this.lastSafeCompassAngle;
						}
						else
						{
							b = ((this.rotationMode != TouchJoystickSpriteAnimator.RotationMode.SimpleHorizontal) ? vector.y : vector.x) * -this.simpleRotationRange;
						}
					}
					else
					{
						this.lastSafeCompassAngle = 0f;
						b = 0f;
					}
					this.extraRotation = CFUtils.SmoothTowardsAngle(this.extraRotation, b, this.rotationSmoothingTime, CFUtils.realDeltaTimeClamped, 0.0001f, 0.75f);
				}
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
			optimizer.AddSprite(this.spriteNeutralPressed.sprite);
			optimizer.AddSprite(this.spriteUp.sprite);
			optimizer.AddSprite(this.spriteUpRight.sprite);
			optimizer.AddSprite(this.spriteRight.sprite);
			optimizer.AddSprite(this.spriteDownRight.sprite);
			optimizer.AddSprite(this.spriteDown.sprite);
			optimizer.AddSprite(this.spriteDownLeft.sprite);
			optimizer.AddSprite(this.spriteLeft.sprite);
			optimizer.AddSprite(this.spriteUpLeft.sprite);
		}

		void ISpriteAnimator.OnSpriteOptimization(ISpriteOptimizer optimizer)
		{
			this.spriteNeutral.sprite = optimizer.GetOptimizedSprite(this.spriteNeutral.sprite);
			this.spriteNeutralPressed.sprite = optimizer.GetOptimizedSprite(this.spriteNeutralPressed.sprite);
			this.spriteUp.sprite = optimizer.GetOptimizedSprite(this.spriteUp.sprite);
			this.spriteUpRight.sprite = optimizer.GetOptimizedSprite(this.spriteUpRight.sprite);
			this.spriteRight.sprite = optimizer.GetOptimizedSprite(this.spriteRight.sprite);
			this.spriteDownRight.sprite = optimizer.GetOptimizedSprite(this.spriteDownRight.sprite);
			this.spriteDown.sprite = optimizer.GetOptimizedSprite(this.spriteDown.sprite);
			this.spriteDownLeft.sprite = optimizer.GetOptimizedSprite(this.spriteDownLeft.sprite);
			this.spriteLeft.sprite = optimizer.GetOptimizedSprite(this.spriteLeft.sprite);
			this.spriteUpLeft.sprite = optimizer.GetOptimizedSprite(this.spriteUpLeft.sprite);
		}

		public TouchJoystickSpriteAnimator.SpriteMode spriteMode;

		public SpriteConfig spriteNeutralPressed;

		public SpriteConfig spriteUp;

		public SpriteConfig spriteUpRight;

		public SpriteConfig spriteRight;

		public SpriteConfig spriteDownRight;

		public SpriteConfig spriteDown;

		public SpriteConfig spriteDownLeft;

		public SpriteConfig spriteLeft;

		public SpriteConfig spriteUpLeft;

		public bool animateTransl;

		public Vector2 moveScale;

		public float translationSmoothingTime = 0.1f;

		public TouchJoystickSpriteAnimator.RotationMode rotationMode;

		public float simpleRotationRange;

		public float rotationSmoothingTime = 0.1f;

		private float lastSafeCompassAngle;

		public enum SpriteMode
		{
			Simple,
			FourWay,
			EightWay
		}

		public enum RotationMode
		{
			Disabled,
			SimpleHorizontal,
			SimpleVertical,
			Compass
		}

		public enum ControlState
		{
			Neutral,
			NeutralPressed,
			U,
			UR,
			R,
			DR,
			D,
			DL,
			L,
			UL,
			All
		}
	}
}
