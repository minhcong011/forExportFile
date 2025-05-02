// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.SwipeSlasher.SwipeSlasherUserControl
using System;
using UnityEngine;

namespace ControlFreak2.Demos.SwipeSlasher
{
	public class SwipeSlasherUserControl : MonoBehaviour
	{
		private void OnEnable()
		{
			if (this.character == null)
			{
				this.character = base.GetComponent<SwipeSlasherChara>();
			}
		}

		private void Update()
		{
			if (this.character == null)
			{
				return;
			}
			if (CF2Input.GetButtonDown("Left-Stab"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_STAB);
			}
			else if (CF2Input.GetButtonDown("Left-Slash-U"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_U);
			}
			else if (CF2Input.GetButtonDown("Left-Slash-R"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_R);
			}
			else if (CF2Input.GetButtonDown("Left-Slash-L"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_L);
			}
			else if (CF2Input.GetButtonDown("Left-Slash-D"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.LEFT_SLASH_D);
			}
			else if (CF2Input.GetButtonDown("Right-Stab"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_STAB);
			}
			else if (CF2Input.GetButtonDown("Right-Slash-U"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_U);
			}
			else if (CF2Input.GetButtonDown("Right-Slash-R"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_R);
			}
			else if (CF2Input.GetButtonDown("Right-Slash-L"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_L);
			}
			else if (CF2Input.GetButtonDown("Right-Slash-D"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.RIGHT_SLASH_D);
			}
			else if (CF2Input.GetButtonDown("Dodge-Right"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.DODGE_RIGHT);
			}
			else if (CF2Input.GetButtonDown("Dodge-Left"))
			{
				this.character.ExecuteAction(SwipeSlasherChara.ActionType.DODGE_LEFT);
			}
		}

		public SwipeSlasherChara character;
	}
}
