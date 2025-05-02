// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.DemoMainState
using System;
using UnityEngine;

namespace ControlFreak2.Demos
{
	public class DemoMainState : GameState
	{
		protected override void OnStartState(GameState parentState)
		{
			base.OnStartState(parentState);
			base.gameObject.SetActive(true);
		}

		protected override void OnExitState()
		{
			base.OnExitState();
			base.gameObject.SetActive(false);
		}

		protected override void OnUpdateState()
		{
			if (this.helpBox != null && !this.helpBox.IsRunning() && this.helpKey != KeyCode.None && CF2Input.GetKeyDown(this.helpKey))
			{
				this.ShowHelpBox();
			}
			base.OnUpdateState();
		}

		public void ExitToMainMenu()
		{
			if (this.multiDemoManager != null)
			{
				this.multiDemoManager.EnterMainMenu();
			}
			else
			{
				Application.Quit();
			}
		}

		public void ShowHelpBox()
		{
			if (this.helpBox != null)
			{
				this.helpBox.ShowHelpBox(this);
			}
		}

		public MultiDemoManager multiDemoManager;

		public HelpBoxState helpBox;

		public KeyCode helpKey = KeyCode.Escape;
	}
}
