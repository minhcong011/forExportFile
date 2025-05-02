// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.GamepadInputModule
using System;
using ControlFreak2.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ControlFreak2
{
	public class GamepadInputModule : BaseInputModule
	{
		public override bool IsModuleSupported()
		{
			return true;
		}

		private bool JustPressedSubmitButton()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
			{
				return true;
			}
			GamepadManager activeManager = GamepadManager.activeManager;
			return activeManager != null && (activeManager.GetCombinedGamepad().GetKeyDown(this.submitGamepadButton) || activeManager.GetCombinedGamepad().GetKeyDown(this.submitGamepadButtonAlt));
		}

		private bool JustPressedCancelButton()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				return true;
			}
			GamepadManager activeManager = GamepadManager.activeManager;
			return activeManager != null && (activeManager.GetCombinedGamepad().GetKeyDown(this.cancelGamepadButton) || activeManager.GetCombinedGamepad().GetKeyDown(this.cancelGamepadButtonAlt));
		}

		private MoveDirection JustPressedDirectionKey()
		{
			bool flag = UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow);
			bool flag2 = UnityEngine.Input.GetKeyDown(KeyCode.RightArrow);
			bool flag3 = UnityEngine.Input.GetKeyDown(KeyCode.UpArrow);
			bool flag4 = UnityEngine.Input.GetKeyDown(KeyCode.DownArrow);
			GamepadManager activeManager = GamepadManager.activeManager;
			if (activeManager != null)
			{
				flag |= (activeManager.GetCombinedGamepad().leftStick.state.JustPressedDir4(Dir.L) || activeManager.GetCombinedGamepad().dpad.state.JustPressedDir4(Dir.L));
				flag2 |= (activeManager.GetCombinedGamepad().leftStick.state.JustPressedDir4(Dir.R) || activeManager.GetCombinedGamepad().dpad.state.JustPressedDir4(Dir.R));
				flag3 |= (activeManager.GetCombinedGamepad().leftStick.state.JustPressedDir4(Dir.U) || activeManager.GetCombinedGamepad().dpad.state.JustPressedDir4(Dir.U));
				flag4 |= (activeManager.GetCombinedGamepad().leftStick.state.JustPressedDir4(Dir.D) || activeManager.GetCombinedGamepad().dpad.state.JustPressedDir4(Dir.D));
			}
			if (flag && flag2)
			{
				flag2 = (flag = false);
			}
			if (flag3 && flag4)
			{
				flag4 = (flag3 = false);
			}
			return (!flag3) ? ((!flag4) ? ((!flag) ? ((!flag2) ? MoveDirection.None : MoveDirection.Right) : MoveDirection.Left) : MoveDirection.Down) : MoveDirection.Up;
		}

		public override bool ShouldActivateModule()
		{
			return base.ShouldActivateModule() && (this.JustPressedCancelButton() || this.JustPressedSubmitButton() || this.JustPressedDirectionKey() != MoveDirection.None);
		}

		public override void ActivateModule()
		{
			base.ActivateModule();
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject != null)
			{
				base.eventSystem.SetSelectedGameObject(null, this.GetBaseEventData());
				base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
				this.SendMoveEventToSelectedObject();
			}
			else
			{
				if (gameObject == null)
				{
					gameObject = base.eventSystem.firstSelectedGameObject;
				}
				else
				{
					gameObject = this.FindFirstSelectableInScene();
				}
				if (gameObject != null)
				{
					base.eventSystem.SetSelectedGameObject(null, this.GetBaseEventData());
					base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
				}
			}
		}

		private GameObject FindFirstSelectableInScene()
		{
			Selectable selectable = null;
			foreach (Selectable selectable2 in (Selectable[])UnityEngine.Object.FindObjectsOfType(typeof(Selectable)))
			{
				if (selectable2.navigation.mode != Navigation.Mode.None)
				{
					selectable = selectable2;
				}
			}
			return (!(selectable != null)) ? null : selectable.gameObject;
		}

		public override void DeactivateModule()
		{
			base.DeactivateModule();
		}

		public override void Process()
		{
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
		}

		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			GamepadManager activeManager = GamepadManager.activeManager;
			if (activeManager == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (this.JustPressedSubmitButton())
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			if (this.JustPressedCancelButton())
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		protected bool SendMoveEventToSelectedObject()
		{
			GamepadManager activeManager = GamepadManager.activeManager;
			if (activeManager == null)
			{
				return false;
			}
			JoystickState stick = activeManager.GetCombinedGamepad().GetStick(GamepadManager.GamepadStick.LeftAnalog);
			JoystickState stick2 = activeManager.GetCombinedGamepad().GetStick(GamepadManager.GamepadStick.Dpad);
			Dir dir = Dir.N;
			if (stick.JustReleasedDir4(Dir.N))
			{
				dir = stick.GetDir4();
			}
			else if (stick2.JustReleasedDir4(Dir.N))
			{
				dir = stick2.GetDir4();
			}
			Vector2 vector = CFUtils.DirToVector(dir, false);
			Vector2 vector2 = new Vector2((!Input.GetKeyDown(KeyCode.RightArrow)) ? ((!Input.GetKeyDown(KeyCode.LeftArrow)) ? 0f : -1f) : 1f, (!Input.GetKeyDown(KeyCode.UpArrow)) ? ((!Input.GetKeyDown(KeyCode.DownArrow)) ? 0f : -1f) : 1f);
			vector.x = CFUtils.ApplyDeltaInput(vector.x, vector2.x);
			vector.y = CFUtils.ApplyDeltaInput(vector.y, vector2.y);
			if (vector.sqrMagnitude < 1E-05f)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(vector.x, vector.y, 0.3f);
			ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
			if (base.eventSystem.currentSelectedGameObject != null)
			{
				base.eventSystem.firstSelectedGameObject = base.eventSystem.currentSelectedGameObject;
			}
			return axisEventData.used;
		}

		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		public GamepadManager.GamepadKey submitGamepadButton;

		public GamepadManager.GamepadKey submitGamepadButtonAlt = GamepadManager.GamepadKey.Start;

		public GamepadManager.GamepadKey cancelGamepadButton = GamepadManager.GamepadKey.FaceRight;

		public GamepadManager.GamepadKey cancelGamepadButtonAlt = GamepadManager.GamepadKey.Select;
	}
}
