// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.HelpBoxState
using System;

namespace ControlFreak2.Demos
{
	public class HelpBoxState : GameState
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

		public void ShowHelpBox(DemoMainState parentDemoState)
		{
			this.parentDemoState = parentDemoState;
			this.parentDemoState.StartSubState(this);
		}

		public void ExitToMainMenu()
		{
			if (this.parentDemoState != null)
			{
				this.parentDemoState.ExitToMainMenu();
			}
		}

		protected DemoMainState parentDemoState;
	}
}
