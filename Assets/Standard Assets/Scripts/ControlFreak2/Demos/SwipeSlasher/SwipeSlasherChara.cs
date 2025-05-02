// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.SwipeSlasher.SwipeSlasherChara
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2.Demos.SwipeSlasher
{
	public class SwipeSlasherChara : MonoBehaviour
	{
		private void OnEnable()
		{
			this.animator = base.GetComponent<Animator>();
		}

		private void Start()
		{
			for (SwipeSlasherChara.ActionType actionType = SwipeSlasherChara.ActionType.LEFT_STAB; actionType <= SwipeSlasherChara.ActionType.DODGE_RIGHT; actionType++)
			{
				Graphic actionGraphic = this.GetActionGraphic(actionType);
				if (actionGraphic != null)
				{
					Color color = actionGraphic.color;
					color.a = 0f;
					actionGraphic.color = color;
				}
			}
		}

		private Graphic GetActionGraphic(SwipeSlasherChara.ActionType a)
		{
			switch (a)
			{
			case SwipeSlasherChara.ActionType.LEFT_STAB:
				return this.graphicLeftStab;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_U:
				return this.graphicLeftSlashU;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_R:
				return this.graphicLeftSlashR;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_D:
				return this.graphicLeftSlashD;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_L:
				return this.graphicLeftSlashL;
			case SwipeSlasherChara.ActionType.RIGHT_STAB:
				return this.graphicRightStab;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_U:
				return this.graphicRightSlashU;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_R:
				return this.graphicRightSlashR;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_D:
				return this.graphicRightSlashD;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_L:
				return this.graphicRightSlashL;
			case SwipeSlasherChara.ActionType.DODGE_LEFT:
				return this.graphicDodgeLeft;
			case SwipeSlasherChara.ActionType.DODGE_RIGHT:
				return this.graphicDodgeRight;
			default:
				return null;
			}
		}

		private void SetActionTriggerState(SwipeSlasherChara.ActionType action)
		{
			if (this.animator == null)
			{
				return;
			}
			string text = null;
			switch (action)
			{
			case SwipeSlasherChara.ActionType.LEFT_STAB:
				text = this.animLeftStab;
				break;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_U:
				text = this.animLeftSlashU;
				break;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_R:
				text = this.animLeftSlashR;
				break;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_D:
				text = this.animLeftSlashD;
				break;
			case SwipeSlasherChara.ActionType.LEFT_SLASH_L:
				text = this.animLeftSlashL;
				break;
			case SwipeSlasherChara.ActionType.RIGHT_STAB:
				text = this.animRightStab;
				break;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_U:
				text = this.animRightSlashU;
				break;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_R:
				text = this.animRightSlashR;
				break;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_D:
				text = this.animRightSlashD;
				break;
			case SwipeSlasherChara.ActionType.RIGHT_SLASH_L:
				text = this.animRightSlashL;
				break;
			case SwipeSlasherChara.ActionType.DODGE_LEFT:
				text = this.animDodgeLeft;
				break;
			case SwipeSlasherChara.ActionType.DODGE_RIGHT:
				text = this.animDodgeRight;
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.animator.SetTrigger(text);
			}
		}

		public void ExecuteAction(SwipeSlasherChara.ActionType action)
		{
			Graphic actionGraphic = this.GetActionGraphic(action);
			if (action == SwipeSlasherChara.ActionType.LEFT_STAB || action == SwipeSlasherChara.ActionType.RIGHT_STAB)
			{
				this.PlaySound(this.soundStab);
			}
			else if (action == SwipeSlasherChara.ActionType.DODGE_LEFT || action == SwipeSlasherChara.ActionType.DODGE_RIGHT)
			{
				this.PlaySound(this.soundDodge);
			}
			else
			{
				this.PlaySound(this.soundSlash);
			}
			if (this.animator != null)
			{
				this.SetActionTriggerState(action);
			}
			if (actionGraphic != null)
			{
				Color color = actionGraphic.color;
				color.a = 1f;
				actionGraphic.color = color;
				actionGraphic.CrossFadeAlpha(1f, 0f, true);
				actionGraphic.CrossFadeAlpha(0f, this.fadeDur, true);
			}
		}

		private void PlaySound(AudioClip clip)
		{
			if (clip != null)
			{
				AudioSource.PlayClipAtPoint(clip, Vector3.zero);
			}
		}

		public AudioClip soundDodge;

		public AudioClip soundSlash;

		public AudioClip soundStab;

		public Graphic graphicLeftStab;

		public Graphic graphicLeftSlashU;

		public Graphic graphicLeftSlashR;

		public Graphic graphicLeftSlashD;

		public Graphic graphicLeftSlashL;

		public Graphic graphicRightStab;

		public Graphic graphicRightSlashU;

		public Graphic graphicRightSlashR;

		public Graphic graphicRightSlashD;

		public Graphic graphicRightSlashL;

		public Graphic graphicDodgeLeft;

		public Graphic graphicDodgeRight;

		public float fadeDur = 0.5f;

		private Animator animator;

		public string animLeftStab = "Left-Stab";

		public string animLeftSlashU = "Left-Slash-U";

		public string animLeftSlashR = "Left-Slash-R";

		public string animLeftSlashD = "Left-Slash-D";

		public string animLeftSlashL = "Left-Slash-L";

		public string animRightStab = "Right-Stab";

		public string animRightSlashU = "Right-Slash-U";

		public string animRightSlashR = "Right-Slash-R";

		public string animRightSlashD = "Right-Slash-D";

		public string animRightSlashL = "Right-Slash-L";

		public string animDodgeLeft = "Dodge-Left";

		public string animDodgeRight = "Dodge-Right";

		public enum ActionType
		{
			LEFT_STAB,
			LEFT_SLASH_U,
			LEFT_SLASH_R,
			LEFT_SLASH_D,
			LEFT_SLASH_L,
			RIGHT_STAB,
			RIGHT_SLASH_U,
			RIGHT_SLASH_R,
			RIGHT_SLASH_D,
			RIGHT_SLASH_L,
			DODGE_LEFT,
			DODGE_RIGHT
		}
	}
}
