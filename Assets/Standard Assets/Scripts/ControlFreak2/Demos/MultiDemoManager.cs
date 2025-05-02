// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.MultiDemoManager
using System;

namespace ControlFreak2.Demos
{
	public class MultiDemoManager : GameState
	{
		public void EnterMainMenu()
		{
			base.StartSubState(this.mainMenuState);
		}

		private void Start()
		{
			this.OnStartState(null);
		}

		private void Update()
		{
			if (base.IsRunning())
			{
				this.OnUpdateState();
			}
		}

		protected override void OnStartState(GameState parentState)
		{
			base.OnStartState(parentState);
			base.gameObject.SetActive(true);
			this.EnterMainMenu();
		}

		protected override void OnExitState()
		{
			base.OnExitState();
			base.gameObject.SetActive(false);
		}

		private const string MAIN_MENU_SCENE_NAME = "CF2-Multi-Demo-Manager";

		public DemoMainState mainMenuState;
	}
}
