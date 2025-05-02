// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.InputRig
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ControlFreak2.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ControlFreak2
{
	public class InputRig : ComponentBase, IBindingContainer
	{
		public InputRig()
		{
			this.BasicConstructor();
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onSwitchesChanged;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onAddExtraInput;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onAddExtraInputToActiveRig;

		public void OnActivateRig()
		{
			if (this.hideWhenDisactivated)
			{
				this.ShowOrHideTouchControls(true, false);
			}
		}

		public void OnDisactivateRig()
		{
			if (this.disableWhenDisactivated)
			{
				base.gameObject.SetActive(false);
			}
			else if (this.hideWhenDisactivated)
			{
				this.ShowOrHideTouchControls(false, false);
			}
		}

		private void BasicConstructor()
		{
			this.fixedTimeStep = new FixedTimeStepController(120);
			this.ninetyDegTurnMouseDelta = 500f;
			this.ninetyDegTurnTouchSwipeInCm = 4f;
			this.ninetyDegTurnAnalogDuration = 0.75f;
			this.virtualScreenDiameterInches = 4f;
			this.hideWhenDisactivated = true;
			this.hideWhenTouchScreenIsUnused = true;
			this.hideWhenTouchScreenIsUnusedDelay = 10f;
			this.hideWhenGamepadIsActivated = true;
			this.hideWhenGamepadIsActivatedDelay = 5f;
			this.rigSwitches = new InputRig.RigSwitchCollection(this);
			this.InitEmuTouches();
			this.touchControls = new List<TouchControl>(32);
			int length = CFUtils.GetEnumMaxValue(typeof(KeyCode)) + 1;
			this.keysCur = new BitArray(length);
			this.keysPrev = new BitArray(length);
			this.keysNext = new BitArray(length);
			this.keysBlocked = new BitArray(length);
			this.keysMuted = new BitArray(length);
			this.joysticks = new InputRig.VirtualJoystickConfigCollection(this, 1);
			this.axes = new InputRig.AxisConfigCollection(this, 16);
			this.axes.list.Clear();
			this.axes.list.Add(InputRig.AxisConfig.CreateSignedAnalog("Horizontal", KeyCode.D, KeyCode.A));
			this.axes.list.Add(InputRig.AxisConfig.CreateSignedAnalog("Vertical", KeyCode.W, KeyCode.S));
			this.axes.list.Add(InputRig.AxisConfig.CreateDelta("Mouse X", KeyCode.None, KeyCode.None));
			this.axes.list.Add(InputRig.AxisConfig.CreateDelta("Mouse Y", KeyCode.None, KeyCode.None));
			this.axes.list.Add(InputRig.AxisConfig.CreateScrollWheel("Mouse ScrollWheel", KeyCode.None, KeyCode.None));
			this.axes.list.Add(InputRig.AxisConfig.CreateScrollWheel("Mouse ScrollWheel Secondary", KeyCode.None, KeyCode.None));
			this.axes.list.Add(InputRig.AxisConfig.CreateDigital("Fire1", KeyCode.LeftShift));
			this.axes.list.Add(InputRig.AxisConfig.CreateDigital("Fire2", KeyCode.LeftControl));
			this.axes.list.Add(InputRig.AxisConfig.CreateDigital("Jump", KeyCode.Space));
			this.keyboardBlockedCodes = new List<KeyCode>(16);
			this.dpadConfig = new JoystickConfig();
			this.dpadConfig.stickMode = JoystickConfig.StickMode.Digital8;
			this.dpadConfig.analogDeadZone = 0.3f;
			this.dpadConfig.angularMagnet = 0f;
			this.dpadConfig.digitalDetectionMode = JoystickConfig.DigitalDetectionMode.Joystick;
			this.leftStickConfig = new JoystickConfig();
			this.leftStickConfig.stickMode = JoystickConfig.StickMode.Analog;
			this.leftStickConfig.analogDeadZone = 0.3f;
			this.leftStickConfig.analogEndZone = 0.7f;
			this.leftStickConfig.angularMagnet = 0.5f;
			this.leftStickConfig.digitalDetectionMode = JoystickConfig.DigitalDetectionMode.Joystick;
			this.rightStickConfig = new JoystickConfig();
			this.rightStickConfig.stickMode = JoystickConfig.StickMode.Analog;
			this.rightStickConfig.analogDeadZone = 0.3f;
			this.rightStickConfig.analogEndZone = 0.7f;
			this.rightStickConfig.angularMagnet = 0.5f;
			this.rightStickConfig.digitalDetectionMode = JoystickConfig.DigitalDetectionMode.Joystick;
			this.leftTriggerAnalogConfig = new AnalogConfig();
			this.leftTriggerAnalogConfig.analogDeadZone = 0.2f;
			this.rightTriggerAnalogConfig = new AnalogConfig();
			this.rightTriggerAnalogConfig.analogDeadZone = 0.2f;
			this.anyGamepad = new InputRig.GamepadConfig(this);
			this.gamepads = new InputRig.GamepadConfig[4];
			for (int i = 0; i < this.gamepads.Length; i++)
			{
				this.gamepads[i] = new InputRig.GamepadConfig(this);
			}
			this.anyGamepad.enabled = true;
			this.anyGamepad.leftStickStateBinding.horzAxisBinding.AddTarget().SetSingleAxis("Horizontal", false);
			this.anyGamepad.leftStickStateBinding.vertAxisBinding.AddTarget().SetSingleAxis("Vertical", false);
			this.anyGamepad.leftStickStateBinding.enabled = true;
			this.anyGamepad.leftStickStateBinding.horzAxisBinding.enabled = true;
			this.anyGamepad.leftStickStateBinding.vertAxisBinding.enabled = true;
			this.anyGamepad.rightStickStateBinding.horzAxisBinding.AddTarget().SetSingleAxis("Mouse X", false);
			this.anyGamepad.rightStickStateBinding.vertAxisBinding.AddTarget().SetSingleAxis("Mouse Y", false);
			this.anyGamepad.rightStickStateBinding.enabled = true;
			this.anyGamepad.rightStickStateBinding.horzAxisBinding.enabled = true;
			this.anyGamepad.rightStickStateBinding.vertAxisBinding.enabled = true;
			this.anyGamepad.dpadStateBinding.horzAxisBinding.AddTarget().SetSingleAxis("Horizontal", false);
			this.anyGamepad.dpadStateBinding.vertAxisBinding.AddTarget().SetSingleAxis("Vertical", false);
			this.anyGamepad.dpadStateBinding.enabled = true;
			this.anyGamepad.dpadStateBinding.horzAxisBinding.enabled = true;
			this.anyGamepad.dpadStateBinding.vertAxisBinding.enabled = true;
			this.anyGamepad.digiFaceDownBinding.enabled = true;
			this.anyGamepad.digiFaceDownBinding.AddAxis().SetAxis("Fire1", true);
			this.anyGamepad.digiFaceRightBinding.enabled = true;
			this.anyGamepad.digiFaceRightBinding.AddAxis().SetAxis("Jump", true);
			this.anyGamepad.digiFaceLeftBinding.enabled = true;
			this.anyGamepad.digiFaceLeftBinding.AddAxis().SetAxis("Fire2", true);
			this.anyGamepad.digiFaceUpBinding.enabled = true;
			this.anyGamepad.digiFaceUpBinding.AddAxis().SetAxis("Fire3", true);
			this.anyGamepad.digiR1Binding.enabled = true;
			this.anyGamepad.digiR1Binding.AddAxis().SetAxis("Fire1", true);
			this.anyGamepad.digiR2Binding.enabled = true;
			this.anyGamepad.digiR2Binding.AddAxis().SetAxis("Fire1", true);
			this.anyGamepad.digiL1Binding.enabled = true;
			this.anyGamepad.digiL1Binding.AddAxis().SetAxis("Fire2", true);
			this.anyGamepad.digiL2Binding.enabled = true;
			this.anyGamepad.digiL2Binding.AddAxis().SetAxis("Fire2", true);
			this.tilt = new InputRig.TiltConfig(this);
			this.mouseConfig = new InputRig.MouseConfig(this);
			this.scrollWheel = new InputRig.ScrollWheelState(this);
			this.autoInputList = new InputRig.AutomaticInputConfigCollection(this);
		}

		protected override void OnInitComponent()
		{
			this.autoInputList.SetRig(this);
			this.ResetState();
			this.ResetAllSwitches(true);
			this.InvalidateBlockedKeys();
		}

		protected override void OnDestroyComponent()
		{
			if (this.touchControls != null)
			{
				int count = this.touchControls.Count;
				for (int i = 0; i < count; i++)
				{
					TouchControl touchControl = this.touchControls[i];
					if (touchControl != null)
					{
						touchControl.SetRig(null);
					}
				}
			}
		}

		protected override void OnEnableComponent()
		{
			if (this.autoActivate && (CF2Input.activeRig == null || this.overrideActiveRig))
			{
				CF2Input.activeRig = this;
			}
			EventSystem eventSystem = EventSystem.current;
			if (eventSystem == null)
			{
				eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
			}
			if (eventSystem != null)
			{
				MonoBehaviour monoBehaviour = eventSystem.GetComponent("TouchInputModule") as MonoBehaviour;
				if (monoBehaviour != null && monoBehaviour.enabled)
				{
					monoBehaviour.enabled = false;
				}
			}
			this.fixedTimeStep.Reset();
			this.ResetState();
			this.MuteUntilRelease();
			if (CFUtils.editorStopped)
			{
				return;
			}
			CF2Input.onMobileModeChange += this.OnMobileModeChange;
			CFCursor.onLockStateChange += this.OnCursorLockStateChange;
		}

		protected override void OnDisableComponent()
		{
			if (CF2Input.activeRig == this)
			{
				CF2Input.activeRig = null;
			}
			this.ResetState();
			if (CFUtils.editorStopped)
			{
				return;
			}
			CF2Input.onMobileModeChange -= this.OnMobileModeChange;
			CFCursor.onLockStateChange -= this.OnCursorLockStateChange;
		}

		private void OnMobileModeChange()
		{
			this.SyncDisablingConditions(false);
		}

		private void OnCursorLockStateChange()
		{
			this.SyncDisablingConditions(false);
		}

		private void OnApplicationPause(bool paused)
		{
			int count = this.touchControls.Count;
			for (int i = 0; i < count; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl != null)
				{
					touchControl.ReleaseAllTouches();
				}
			}
		}

		private void Update()
		{
			this.fixedTimeStep.Update(CFUtils.realDeltaTimeClamped);
			if (!base.IsInitialized)
			{
				return;
			}
			this.UpdateConfigs();
		}

		public void ResetState()
		{
			if (!base.IsInitialized)
			{
				return;
			}
			this.tilt.Reset();
			this.mouseConfig.Reset();
			this.scrollWheel.Reset();
			this.ResetEmuTouches();
			this.joysticks.ResetAll();
			this.axes.ResetAll();
			this.MuteUntilRelease();
			this.keysCur.SetAll(false);
			this.keysPrev.SetAll(false);
			this.keysNext.SetAll(false);
			this.keysMuted.SetAll(true);
			this.keysCurSomeDown = false;
			this.keysCurSomeOn = false;
			this.keysNextSomeDown = false;
			this.keysNextSomeOn = false;
			this.InvalidateBlockedKeys();
			for (int i = 0; i < this.touchControls.Count; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl != null)
				{
					touchControl.ResetControl();
				}
			}
		}

		public void MuteUntilRelease()
		{
			this.axes.MuteAllUntilRelease();
			this.keysPrev.SetAll(false);
			this.keysCur.SetAll(false);
			this.keysMuted.SetAll(true);
		}

		private void UpdateConfigs()
		{
			this.CheckResolution(false);
			if (!this.touchControlsSleeping)
			{
				float num = -1f;
				if (this.hideWhenTouchScreenIsUnused)
				{
					num = this.hideWhenTouchScreenIsUnusedDelay;
				}
				if (this.hideWhenGamepadIsActivated && GamepadManager.activeManager != null && GamepadManager.activeManager.GetActiveGamepadCount() > 0)
				{
					if (num < 0f)
					{
						num = this.hideWhenGamepadIsActivatedDelay;
					}
					else if (this.hideWhenGamepadIsActivatedDelay < num)
					{
						num = this.hideWhenGamepadIsActivatedDelay;
					}
				}
				if (num > 0f)
				{
					this.elapsedSinceLastTouch += CFUtils.realDeltaTimeClamped;
					if (this.elapsedSinceLastTouch > num)
					{
						this.PutTouchControlsToSleep();
					}
				}
			}
			else if (!this.hideWhenTouchScreenIsUnused && this.hideWhenGamepadIsActivated && GamepadManager.activeManager != null && GamepadManager.activeManager.GetActiveGamepadCount() == 0)
			{
				this.WakeTouchControlsUp();
			}
			this.ApplySwitches(false);
			for (int i = 0; i < this.touchControls.Count; i++)
			{
				this.touchControls[i].UpdateControl();
			}
			this.tilt.Update();
			this.mouseConfig.Update();
			this.scrollWheel.Update();
			this.joysticks.Update(this);
			this.axes.ApplyKeyboardInput();
			if (this == CF2Input.activeRig && InputRig.onAddExtraInputToActiveRig != null)
			{
				InputRig.onAddExtraInputToActiveRig();
			}
			if (this.onAddExtraInput != null)
			{
				this.onAddExtraInput();
			}
			this.autoInputList.Update(this);
			this.axes.Update(this);
			this.UpdateKeyCodes();
			this.UpdateEmuTouches();
		}

		private void UpdateKeyCodes()
		{
			BitArray bitArray = this.keysPrev;
			this.keysPrev = this.keysCur;
			this.keysCur = this.keysNext;
			this.keysNext = bitArray;
			this.keysNext.SetAll(false);
			this.keysMuted.And(this.keysCur);
			this.keysMuted.Not();
			this.keysCur.And(this.keysMuted);
			this.keysMuted.Not();
			this.keysCurSomeDown = this.keysNextSomeDown;
			this.keysCurSomeOn = this.keysNextSomeOn;
			this.keysNextSomeDown = false;
			this.keysNextSomeOn = false;
		}

		private void InvalidateBlockedKeys()
		{
			this.keysBlocked.SetAll(false);
			for (int i = 0; i < this.keyboardBlockedCodes.Count; i++)
			{
				int num = (int)this.keyboardBlockedCodes[i];
				if (num >= 0 && num < this.keysBlocked.Length)
				{
					this.keysBlocked[num] = true;
				}
			}
		}

		public static bool GetSourceKeyState(KeyCode key)
		{
			return key != KeyCode.None && (!CF2Input.IsInMobileMode() || key < KeyCode.Mouse0 || key > KeyCode.Mouse6) && UnityEngine.Input.GetKey(key);
		}

		public static bool IsMouseKeyCode(KeyCode key)
		{
			return key >= KeyCode.Mouse0 && key <= KeyCode.Mouse6;
		}

		public void AddControl(TouchControl c)
		{
			if (!base.CanBeUsed())
			{
				return;
			}
			this.touchControls.Add(c);
		}

		public void RemoveControl(TouchControl c)
		{
			if (!base.CanBeUsed())
			{
				return;
			}
			if (this.touchControls != null)
			{
				this.touchControls.Remove(c);
			}
		}

		public List<TouchControl> GetTouchControls()
		{
			return this.touchControls;
		}

		public TouchControl FindTouchControl(string name)
		{
			return this.touchControls.Find((TouchControl x) => x.name.Equals(name, StringComparison.OrdinalIgnoreCase));
		}

		public void ShowOrHideTouchControls(bool show, bool skipAnim)
		{
			if (this.touchControlsHidden != !show)
			{
				this.touchControlsHidden = !show;
				this.SyncDisablingConditions(skipAnim);
				if ((GamepadManager.activeManager == null || GamepadManager.activeManager.GetActiveGamepadCount() == 0) && show && this.AreTouchControlsSleeping())
				{
					this.WakeTouchControlsUp();
				}
			}
		}

		public bool AreTouchControlsHiddenManually()
		{
			return this.touchControlsHidden;
		}

		public bool AreTouchControlsSleeping()
		{
			return this.touchControlsSleeping;
		}

		public void WakeTouchControlsUp()
		{
			this.elapsedSinceLastTouch = 0f;
			if (this.touchControlsSleeping)
			{
				this.touchControlsSleeping = false;
				this.SyncDisablingConditions(false);
			}
		}

		public void PutTouchControlsToSleep()
		{
			this.touchControlsSleeping = true;
			this.SyncDisablingConditions(false);
		}

		public bool AreTouchControlsVisible()
		{
			return !this.touchControlsSleeping && !this.touchControlsHidden;
		}

		private void SyncDisablingConditions(bool noAnim)
		{
			int count = this.touchControls.Count;
			for (int i = 0; i < count; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl != null)
				{
					touchControl.SyncDisablingConditions(noAnim);
				}
			}
			this.tilt.OnDisablingConditionsChange();
			this.autoInputList.OnDisablingConditionsChange();
		}

		public static bool IsTiltAvailable()
		{
			return TiltState.IsAvailable();
		}

		public bool IsTiltCalibrated()
		{
			return this.tilt.tiltState.IsCalibrated();
		}

		public void CalibrateTilt()
		{
			this.tilt.tiltState.Calibate();
		}

		public void ResetTilt()
		{
			this.tilt.Reset();
		}

		public bool GetSwitchState(string name, ref int cachedId, bool fallbackVal)
		{
			return this.rigSwitches.GetSwitchState(name, ref cachedId, fallbackVal);
		}

		public bool GetSwitchState(string name, bool fallbackVal)
		{
			return this.rigSwitches.GetSwitchState(name, fallbackVal);
		}

		public void SetSwitchState(string name, ref int cachedId, bool state)
		{
			this.rigSwitches.SetSwitchState(name, ref cachedId, state);
		}

		public void SetSwitchState(string name, bool state)
		{
			this.rigSwitches.SetSwitchState(name, state);
		}

		public void SetAllSwitches(bool state)
		{
			this.rigSwitches.SetAll(state);
		}

		public void ResetSwitch(string name, ref int cachedId)
		{
			this.rigSwitches.ResetSwitch(name, ref cachedId);
		}

		public void ResetSwitch(string name)
		{
			this.rigSwitches.ResetSwitch(name);
		}

		public void ResetAllSwitches(bool skipAnim)
		{
			this.rigSwitches.Reset();
			if (skipAnim)
			{
				this.ApplySwitches(true);
			}
		}

		public bool IsSwitchDefined(string name, ref int cachedId)
		{
			return this.rigSwitches.Get(name, ref cachedId) != null;
		}

		public bool IsSwitchDefined(string name)
		{
			return this.rigSwitches.Get(name) != null;
		}

		public void ApplySwitches(bool skipAnim)
		{
			if (this.rigSwitches.changed)
			{
				this.SyncDisablingConditions(skipAnim);
				this.rigSwitches.changed = false;
				if (this.onSwitchesChanged != null)
				{
					this.onSwitchesChanged();
				}
			}
		}

		public void ResetInputAxes()
		{
			this.MuteUntilRelease();
		}

		public bool IsAxisDefined(string name, ref int cachedId)
		{
			return this.axes.Get(name, ref cachedId) != null;
		}

		public bool IsAxisDefined(string name)
		{
			return this.axes.Get(name) != null;
		}

		public float GetAxis(string axisName)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName);
			return (axisConfig == null) ? 0f : axisConfig.GetAnalog();
		}

		public float GetAxis(string axisName, ref int cachedId)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName, ref cachedId);
			return (axisConfig == null) ? 0f : axisConfig.GetAnalog();
		}

		public float GetAxisRaw(string axisName)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName);
			return (axisConfig == null) ? 0f : axisConfig.GetAnalogRaw();
		}

		public float GetAxisRaw(string axisName, ref int cachedId)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName, ref cachedId);
			return (axisConfig == null) ? 0f : axisConfig.GetAnalogRaw();
		}

		public bool GetButton(string axisName)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName);
			return axisConfig != null && axisConfig.GetButton();
		}

		public bool GetButton(string axisName, ref int cachedId)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName, ref cachedId);
			return axisConfig != null && axisConfig.GetButton();
		}

		public bool GetButtonDown(string axisName)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName);
			return axisConfig != null && axisConfig.GetButtonDown();
		}

		public bool GetButtonDown(string axisName, ref int cachedId)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName, ref cachedId);
			return axisConfig != null && axisConfig.GetButtonDown();
		}

		public bool GetButtonUp(string axisName)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName);
			return axisConfig != null && axisConfig.GetButtonUp();
		}

		public bool GetButtonUp(string axisName, ref int cachedId)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(axisName, ref cachedId);
			return axisConfig != null && axisConfig.GetButtonUp();
		}

		public bool GetKey(KeyCode keyCode)
		{
			return keyCode >= KeyCode.None && keyCode < (KeyCode)this.keysCur.Length && (this.keysCur.Get((int)keyCode) || (!this.keysBlocked[(int)keyCode] && (!CF2Input.IsInMobileMode() || !InputRig.IsMouseKeyCode(keyCode)) && UnityEngine.Input.GetKey(keyCode)));
		}

		public bool GetKeyDown(KeyCode keyCode)
		{
			return keyCode >= KeyCode.None && keyCode < (KeyCode)this.keysCur.Length && ((!this.keysPrev.Get((int)keyCode) && this.keysCur.Get((int)keyCode)) || (!this.keysBlocked[(int)keyCode] && (!CF2Input.IsInMobileMode() || !InputRig.IsMouseKeyCode(keyCode)) && UnityEngine.Input.GetKeyDown(keyCode)));
		}

		public bool GetKeyUp(KeyCode keyCode)
		{
			return keyCode >= KeyCode.None && keyCode < (KeyCode)this.keysCur.Length && ((this.keysPrev.Get((int)keyCode) && !this.keysCur.Get((int)keyCode)) || (!this.keysBlocked[(int)keyCode] && (!CF2Input.IsInMobileMode() || !InputRig.IsMouseKeyCode(keyCode)) && UnityEngine.Input.GetKeyUp(keyCode)));
		}

		public bool GetKey(string keyName)
		{
			return this.GetKey(InputRig.NameToKeyCode(keyName));
		}

		public bool GetKeyDown(string keyName)
		{
			return this.GetKeyDown(InputRig.NameToKeyCode(keyName));
		}

		public bool GetKeyUp(string keyName)
		{
			return this.GetKeyUp(InputRig.NameToKeyCode(keyName));
		}

		public bool AnyKey()
		{
			return this.keysCurSomeOn || Input.anyKey;
		}

		public bool AnyKeyDown()
		{
			return this.keysCurSomeDown || Input.anyKeyDown;
		}

		public bool GetMouseButton(int mouseButton)
		{
			return mouseButton >= 0 && mouseButton <= 6 && this.GetKey(KeyCode.Mouse0 + mouseButton);
		}

		public bool GetMouseButtonDown(int mouseButton)
		{
			return mouseButton >= 0 && mouseButton <= 6 && this.GetKeyDown(KeyCode.Mouse0 + mouseButton);
		}

		public bool GetMouseButtonUp(int mouseButton)
		{
			return mouseButton >= 0 && mouseButton <= 6 && this.GetKeyUp(KeyCode.Mouse0 + mouseButton);
		}

		public bool IsAxisAvailableOnMobile(string axisName)
		{
			for (int i = 0; i < this.touchControls.Count; i++)
			{
				if (this.touchControls[i].IsBoundToAxis(axisName, this))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsKeyAvailableOnMobile(KeyCode keyCode)
		{
			for (int i = 0; i < this.touchControls.Count; i++)
			{
				if (this.touchControls[i].IsBoundToKey(keyCode, this))
				{
					return true;
				}
			}
			for (int j = 0; j < this.axes.list.Count; j++)
			{
				InputRig.AxisConfig axisConfig = this.axes.list[j];
				if (axisConfig.DoesAffectKeyCode(keyCode) && this.IsAxisAvailableOnMobile(axisConfig.name))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsTouchEmulatedOnMobile()
		{
			for (int i = 0; i < this.touchControls.Count; i++)
			{
				if (this.touchControls[i].IsEmulatingTouches())
				{
					return true;
				}
			}
			return false;
		}

		public bool IsMousePositionEmulatedOnMobile()
		{
			for (int i = 0; i < this.touchControls.Count; i++)
			{
				if (this.touchControls[i].IsEmulatingMousePosition())
				{
					return true;
				}
			}
			return false;
		}

		public bool IsScrollWheelEmulatedOnMobile()
		{
			if (!this.scrollWheel.vertScrollDeltaBinding.deltaBinding.enabled)
			{
				return false;
			}
			AxisBinding.TargetElem target = this.scrollWheel.vertScrollDeltaBinding.deltaBinding.GetTarget(0);
			return target != null && !target.separateAxes && this.IsAxisAvailableOnMobile(target.singleAxis);
		}

		public bool IsJoystickDefined(string name, ref int cachedId)
		{
			return this.joysticks.Get(name, ref cachedId) != null;
		}

		public bool IsJoystickDefined(string name)
		{
			return this.joysticks.Get(name) != null;
		}

		public JoystickState GetJoystickState(string name, ref int cachedId)
		{
			InputRig.VirtualJoystickConfig virtualJoystickConfig = this.joysticks.Get(name, ref cachedId);
			return (virtualJoystickConfig != null) ? virtualJoystickConfig.joystickState : null;
		}

		public static KeyCode MouseButtonToKey(int mouseButton)
		{
			return (mouseButton < 0 || mouseButton > 6) ? KeyCode.Mouse0 : (KeyCode.Mouse0 + mouseButton);
		}

		public InputRig.AxisConfig GetAxisConfig(string name, ref int cachedId)
		{
			return this.axes.Get(name, ref cachedId);
		}

		public InputRig.AxisConfig GetAxisConfig(string name)
		{
			return this.axes.Get(name);
		}

		public void SetAxis(string name, ref int cachedId, float v, InputRig.InputSource source)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(name, ref cachedId);
			if (axisConfig != null)
			{
				axisConfig.Set(v, source);
			}
		}

		public void SetAxisScroll(string name, ref int cachedId, int v)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(name, ref cachedId);
			if (axisConfig != null)
			{
				axisConfig.SetScrollDelta(v);
			}
		}

		public void SetAxisDigital(string name, ref int cachedId, bool negSide)
		{
			InputRig.AxisConfig axisConfig = this.axes.Get(name, ref cachedId);
			if (axisConfig != null)
			{
				axisConfig.SetSignedDigital(!negSide);
			}
		}

		public void SetJoystickState(string name, ref int joyId, JoystickState state)
		{
			InputRig.VirtualJoystickConfig virtualJoystickConfig = this.joysticks.Get(name, ref joyId);
			if (virtualJoystickConfig != null)
			{
				virtualJoystickConfig.SetState(state);
			}
		}

		public void SetKeyCode(KeyCode keyCode)
		{
			if (keyCode == KeyCode.None)
			{
				return;
			}
			this.keysNext.Set((int)keyCode, true);
			if (!this.keysCur[(int)keyCode])
			{
				this.keysNextSomeDown = true;
			}
			this.keysNextSomeOn = true;
		}

		public bool GetNextFrameKeyState(KeyCode key)
		{
			return this.keysNext[(int)key];
		}

		public void RecalcPixelConversionParams()
		{
			this.CheckResolution(true);
		}

		private void CheckResolution(bool forceRecalc = false)
		{
			float num = CFScreen.dpcm;
			if (forceRecalc || this.storedHorzRes == 0 || this.storedHorzRes != Screen.width || this.storedVertRes != Screen.height || this.storedDPCM < 1f || this.storedDPCM != num)
			{
				this.storedHorzRes = Screen.width;
				this.storedVertRes = Screen.height;
				this.storedDPCM = num;
				if (!Application.isMobilePlatform || num <= 1f)
				{
					num = Mathf.Sqrt((float)(this.storedHorzRes * this.storedHorzRes + this.storedVertRes * this.storedVertRes)) / (this.virtualScreenDiameterInches * 2.54f);
				}
				this.touchPixelsToEmuMouseScale = ((num >= 1f) ? (this.ninetyDegTurnMouseDelta / (this.ninetyDegTurnTouchSwipeInCm * num)) : 0f);
				this.mousePointsToUniversalScale = ((this.ninetyDegTurnMouseDelta != 0f) ? (1f / this.ninetyDegTurnMouseDelta) : 0f);
				this.touchPixelsToUniversalScale = ((num >= 1f) ? (1f / (this.ninetyDegTurnTouchSwipeInCm * num)) : 0f);
				this.analogToEmuMouseScale = this.ninetyDegTurnMouseDelta / this.ninetyDegTurnAnalogDuration;
				this.analogToUniversalScale = 1f / this.ninetyDegTurnAnalogDuration;
				this.analogToRawDeltaScale = this.ninetyDegTurnMouseDelta / this.ninetyDegTurnAnalogDuration;
				this.scrollToEmuMouseScale = this.ninetyDegTurnMouseDelta / (float)this.scrollStepsPerNinetyDegTurn;
				this.scrollToUniversalScale = 1f / (float)this.scrollStepsPerNinetyDegTurn;
			}
		}

		public float TransformMousePointDelta(float mousePoints, InputRig.DeltaTransformMode mode)
		{
			if (mode == InputRig.DeltaTransformMode.EmulateMouse)
			{
				return mousePoints;
			}
			if (mode != InputRig.DeltaTransformMode.Universal)
			{
				return mousePoints;
			}
			return mousePoints * this.mousePointsToUniversalScale;
		}

		public float TransformTouchPixelDelta(float touchPix, InputRig.DeltaTransformMode mode)
		{
			if (mode == InputRig.DeltaTransformMode.EmulateMouse)
			{
				return touchPix * this.touchPixelsToEmuMouseScale;
			}
			if (mode != InputRig.DeltaTransformMode.Universal)
			{
				return touchPix;
			}
			return touchPix * this.touchPixelsToUniversalScale;
		}

		public float TransformNormalizedDelta(float normDelta, InputRig.DeltaTransformMode mode)
		{
			if (mode == InputRig.DeltaTransformMode.EmulateMouse)
			{
				return normDelta * this.ninetyDegTurnMouseDelta;
			}
			if (mode == InputRig.DeltaTransformMode.Universal)
			{
				return normDelta;
			}
			if (mode != InputRig.DeltaTransformMode.Raw)
			{
				return normDelta;
			}
			return normDelta * this.ninetyDegTurnMouseDelta;
		}

		public float TransformScrollDelta(float scrollDelta, InputRig.DeltaTransformMode mode)
		{
			if (mode == InputRig.DeltaTransformMode.EmulateMouse)
			{
				return scrollDelta * this.scrollToEmuMouseScale;
			}
			if (mode == InputRig.DeltaTransformMode.Raw)
			{
				return scrollDelta * this.scrollToEmuMouseScale;
			}
			if (mode != InputRig.DeltaTransformMode.Universal)
			{
				return scrollDelta;
			}
			return scrollDelta * this.scrollToUniversalScale;
		}

		public float TransformAnalogDelta(float analogVal, InputRig.DeltaTransformMode mode)
		{
			if (mode == InputRig.DeltaTransformMode.EmulateMouse)
			{
				return analogVal * this.analogToEmuMouseScale;
			}
			if (mode == InputRig.DeltaTransformMode.Universal)
			{
				return analogVal * this.analogToUniversalScale;
			}
			if (mode != InputRig.DeltaTransformMode.Raw)
			{
				return analogVal;
			}
			return analogVal * this.analogToRawDeltaScale;
		}

		public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			this.mouseConfig.GetSubBindingDescriptions(descList, this, parentMenuPath + "Mouse/");
			this.tilt.GetSubBindingDescriptions(descList, this, parentMenuPath + "Tilt/");
			this.scrollWheel.GetSubBindingDescriptions(descList, this, parentMenuPath + "Mouse Scroll Wheel/");
			this.anyGamepad.GetSubBindingDescriptions(descList, this, parentMenuPath + "Gamepads/Combined Gamepad/");
			for (int i = 0; i < this.gamepads.Length; i++)
			{
				this.gamepads[i].GetSubBindingDescriptions(descList, this, parentMenuPath + "Gamepads/Gamepad " + (i + 1).ToString() + "/");
			}
			for (int j = 0; j < this.joysticks.list.Count; j++)
			{
				this.joysticks.list[j].GetSubBindingDescriptions(descList, this, parentMenuPath + "Virtual Joysticks/Joystick [" + this.joysticks.list[j].name + "]/");
			}
		}

		public bool IsBoundToKey(KeyCode key, InputRig rig)
		{
			if (this.mouseConfig.IsBoundToKey(key, rig) || this.tilt.IsBoundToKey(key, rig) || this.scrollWheel.IsBoundToKey(key, rig) || this.anyGamepad.IsBoundToKey(key, rig))
			{
				return true;
			}
			for (int i = 0; i < this.gamepads.Length; i++)
			{
				if (this.gamepads[i].IsBoundToKey(key, rig))
				{
					return true;
				}
			}
			for (int j = 0; j < this.joysticks.list.Count; j++)
			{
				if (this.joysticks.list[j].IsBoundToKey(key, rig))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsBoundToAxis(string axisName, InputRig rig)
		{
			if (this.mouseConfig.IsBoundToAxis(axisName, rig) || this.tilt.IsBoundToAxis(axisName, rig) || this.scrollWheel.IsBoundToAxis(axisName, rig) || this.anyGamepad.IsBoundToAxis(axisName, rig))
			{
				return true;
			}
			for (int i = 0; i < this.gamepads.Length; i++)
			{
				if (this.gamepads[i].IsBoundToAxis(axisName, rig))
				{
					return true;
				}
			}
			for (int j = 0; j < this.joysticks.list.Count; j++)
			{
				if (this.joysticks.list[j].IsBoundToAxis(axisName, rig))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsEmulatingTouches()
		{
			if (this.mouseConfig.IsEmulatingTouches() || this.tilt.IsEmulatingTouches() || this.scrollWheel.IsEmulatingTouches() || this.anyGamepad.IsEmulatingTouches())
			{
				return true;
			}
			for (int i = 0; i < this.gamepads.Length; i++)
			{
				if (this.gamepads[i].IsEmulatingTouches())
				{
					return true;
				}
			}
			for (int j = 0; j < this.joysticks.list.Count; j++)
			{
				if (this.joysticks.list[j].IsEmulatingTouches())
				{
					return true;
				}
			}
			return false;
		}

		public bool IsEmulatingMousePosition()
		{
			if (this.mouseConfig.IsEmulatingMousePosition() || this.tilt.IsEmulatingMousePosition() || this.scrollWheel.IsEmulatingMousePosition() || this.anyGamepad.IsEmulatingMousePosition())
			{
				return true;
			}
			for (int i = 0; i < this.gamepads.Length; i++)
			{
				if (this.gamepads[i].IsEmulatingMousePosition())
				{
					return true;
				}
			}
			for (int j = 0; j < this.joysticks.list.Count; j++)
			{
				if (this.joysticks.list[j].IsEmulatingMousePosition())
				{
					return true;
				}
			}
			return false;
		}

		private void InitEmuTouches()
		{
			this.emuTouches = new List<InputRig.EmulatedTouchState>(8);
			this.emuTouchesOrdered = new List<InputRig.EmulatedTouchState>(8);
			for (int i = 0; i < 8; i++)
			{
				this.emuTouches.Add(new InputRig.EmulatedTouchState(i));
			}
		}

		public int InternalStartEmuTouch(Vector2 pos)
		{
			for (int i = 0; i < this.emuTouches.Count; i++)
			{
				InputRig.EmulatedTouchState emulatedTouchState = this.emuTouches[i];
				if (!emulatedTouchState.IsUsed())
				{
					emulatedTouchState.Start(pos);
					return i;
				}
			}
			return -1;
		}

		public void InternalEndEmuTouch(int emuTouchId, bool cancel)
		{
			if (emuTouchId < 0 || emuTouchId >= this.emuTouches.Count)
			{
				return;
			}
			this.emuTouches[emuTouchId].EndTouch(cancel);
		}

		public void InternalUpdateEmuTouch(int emuTouchId, Vector2 pos)
		{
			if (emuTouchId < 0 || emuTouchId >= this.emuTouches.Count)
			{
				return;
			}
			this.emuTouches[emuTouchId].UpdatePos(pos);
		}

		public InputRig.Touch[] GetEmuTouchArray()
		{
			if (this.emuOutputTouchesDirty || this.emuOutputTouches == null)
			{
				if (this.emuTouchesOrdered == null || this.emuTouchesOrdered.Count != this.emuOutputTouches.Length)
				{
					this.emuOutputTouches = new InputRig.Touch[this.emuTouchesOrdered.Count];
				}
				for (int i = 0; i < this.emuTouchesOrdered.Count; i++)
				{
					this.emuOutputTouches[i] = this.emuTouchesOrdered[i].outputTouch;
				}
				this.emuOutputTouchesDirty = false;
			}
			return this.emuOutputTouches;
		}

		public int GetEmuTouchCount()
		{
			return this.emuTouchesOrdered.Count;
		}

		public InputRig.Touch GetEmuTouch(int i)
		{
			if (i < 0 || i >= this.emuTouchesOrdered.Count)
			{
				return InputRig.Touch.Dummy;
			}
			return this.emuTouchesOrdered[i].outputTouch;
		}

		private void ResetEmuTouches()
		{
			for (int i = 0; i < this.emuTouches.Count; i++)
			{
				this.emuTouches[i].Reset();
			}
		}

		private void UpdateEmuTouches()
		{
			this.emuTouchesOrdered.Clear();
			for (int i = 0; i < this.emuTouches.Count; i++)
			{
				InputRig.EmulatedTouchState emulatedTouchState = this.emuTouches[i];
				emulatedTouchState.Update();
				if (emulatedTouchState.IsActive())
				{
					this.emuTouchesOrdered.Add(emulatedTouchState);
				}
			}
			this.emuOutputTouchesDirty = true;
		}

		public void SyncEmuTouch(TouchGestureBasicState touch, ref int emuTouchId)
		{
			if (touch == null)
			{
				return;
			}
			if (touch.JustPressedRaw())
			{
				emuTouchId = this.InternalStartEmuTouch(touch.GetStartPos());
			}
			else if (touch.JustReleasedRaw())
			{
				this.InternalEndEmuTouch(emuTouchId, false);
				emuTouchId = -1;
			}
			if (touch.PressedRaw())
			{
				this.InternalUpdateEmuTouch(emuTouchId, touch.GetCurPosRaw());
			}
		}

		public static KeyCode NameToKeyCode(string keyName)
		{
			if (keyName.Length == 1)
			{
				char c = keyName[0];
				if (c >= 'a' && c <= 'z')
				{
					return KeyCode.A + (int)(c - 'a');
				}
				if (c >= 'A' && c <= 'Z')
				{
					return KeyCode.A + (int)(c - 'A');
				}
				if (c >= '0' && c <= '9')
				{
					return KeyCode.Alpha0 + (int)(c - '0');
				}
			}
			StringComparison comparisonType = StringComparison.OrdinalIgnoreCase;
			if (keyName.Equals("enter", comparisonType))
			{
				return KeyCode.Return;
			}
			if (keyName.Equals("return", comparisonType))
			{
				return KeyCode.Return;
			}
			if (keyName.Equals("space", comparisonType))
			{
				return KeyCode.Space;
			}
			if (keyName.Equals("spacebar", comparisonType))
			{
				return KeyCode.Space;
			}
			if (keyName.Equals(" ", comparisonType))
			{
				return KeyCode.Space;
			}
			if (keyName.Equals("esc", comparisonType))
			{
				return KeyCode.Escape;
			}
			if (keyName.Equals("escape", comparisonType))
			{
				return KeyCode.Escape;
			}
			if (keyName.Equals("left", comparisonType))
			{
				return KeyCode.LeftArrow;
			}
			if (keyName.Equals("right", comparisonType))
			{
				return KeyCode.RightArrow;
			}
			if (keyName.Equals("up", comparisonType))
			{
				return KeyCode.UpArrow;
			}
			if (keyName.Equals("down", comparisonType))
			{
				return KeyCode.DownArrow;
			}
			if (keyName.Equals("Left Arrow", comparisonType))
			{
				return KeyCode.LeftArrow;
			}
			if (keyName.Equals("Right Arrow", comparisonType))
			{
				return KeyCode.RightArrow;
			}
			if (keyName.Equals("Up Arrow", comparisonType))
			{
				return KeyCode.UpArrow;
			}
			if (keyName.Equals("Down Arrow", comparisonType))
			{
				return KeyCode.DownArrow;
			}
			if (keyName.Equals("Arrow Left", comparisonType))
			{
				return KeyCode.LeftArrow;
			}
			if (keyName.Equals("Arrow Right", comparisonType))
			{
				return KeyCode.RightArrow;
			}
			if (keyName.Equals("Arrow Up", comparisonType))
			{
				return KeyCode.UpArrow;
			}
			if (keyName.Equals("Arrow Down", comparisonType))
			{
				return KeyCode.DownArrow;
			}
			if (keyName.Equals("Page Down", comparisonType))
			{
				return KeyCode.PageDown;
			}
			if (keyName.Equals("PageDown", comparisonType))
			{
				return KeyCode.PageDown;
			}
			if (keyName.Equals("PgDwn", comparisonType))
			{
				return KeyCode.PageDown;
			}
			if (keyName.Equals("Page Up", comparisonType))
			{
				return KeyCode.PageUp;
			}
			if (keyName.Equals("PageUp", comparisonType))
			{
				return KeyCode.PageUp;
			}
			if (keyName.Equals("PgUp", comparisonType))
			{
				return KeyCode.PageUp;
			}
			if (keyName.Equals("alt", comparisonType))
			{
				return KeyCode.LeftAlt;
			}
			if (keyName.Equals("L alt", comparisonType))
			{
				return KeyCode.LeftAlt;
			}
			if (keyName.Equals("Left alt", comparisonType))
			{
				return KeyCode.LeftAlt;
			}
			if (keyName.Equals("R alt", comparisonType))
			{
				return KeyCode.RightAlt;
			}
			if (keyName.Equals("Right alt", comparisonType))
			{
				return KeyCode.RightAlt;
			}
			if (keyName.Equals("control", comparisonType))
			{
				return KeyCode.LeftControl;
			}
			if (keyName.Equals("L control", comparisonType))
			{
				return KeyCode.LeftControl;
			}
			if (keyName.Equals("Left control", comparisonType))
			{
				return KeyCode.LeftControl;
			}
			if (keyName.Equals("R control", comparisonType))
			{
				return KeyCode.RightControl;
			}
			if (keyName.Equals("Right control", comparisonType))
			{
				return KeyCode.RightControl;
			}
			if (keyName.Equals("ctrl", comparisonType))
			{
				return KeyCode.LeftControl;
			}
			if (keyName.Equals("L ctrl", comparisonType))
			{
				return KeyCode.LeftControl;
			}
			if (keyName.Equals("Left ctrl", comparisonType))
			{
				return KeyCode.LeftControl;
			}
			if (keyName.Equals("R ctrl", comparisonType))
			{
				return KeyCode.RightControl;
			}
			if (keyName.Equals("Right ctrl", comparisonType))
			{
				return KeyCode.RightControl;
			}
			if (keyName.Equals("shift", comparisonType))
			{
				return KeyCode.LeftShift;
			}
			if (keyName.Equals("L shift", comparisonType))
			{
				return KeyCode.LeftShift;
			}
			if (keyName.Equals("Left shift", comparisonType))
			{
				return KeyCode.LeftShift;
			}
			if (keyName.Equals("R shift", comparisonType))
			{
				return KeyCode.RightShift;
			}
			if (keyName.Equals("Right shift", comparisonType))
			{
				return KeyCode.RightShift;
			}
			if (keyName.Equals("Caps Lock", comparisonType))
			{
				return KeyCode.CapsLock;
			}
			if (keyName.Equals("CapsLock", comparisonType))
			{
				return KeyCode.CapsLock;
			}
			if (keyName.Equals("Caps", comparisonType))
			{
				return KeyCode.CapsLock;
			}
			if (keyName.Equals("tab", comparisonType))
			{
				return KeyCode.Tab;
			}
			if (keyName.Equals("/", comparisonType))
			{
				return KeyCode.Backslash;
			}
			if (keyName.Equals("backslash", comparisonType))
			{
				return KeyCode.Backslash;
			}
			if (keyName.Equals("\\", comparisonType))
			{
				return KeyCode.Slash;
			}
			if (keyName.Equals("slash", comparisonType))
			{
				return KeyCode.Slash;
			}
			if (keyName.Equals("[", comparisonType))
			{
				return KeyCode.LeftBracket;
			}
			if (keyName.Equals("]", comparisonType))
			{
				return KeyCode.RightBracket;
			}
			if (keyName.Equals(".", comparisonType))
			{
				return KeyCode.Comma;
			}
			if (keyName.Equals(",", comparisonType))
			{
				return KeyCode.Colon;
			}
			if (keyName.Equals("'", comparisonType))
			{
				return KeyCode.Quote;
			}
			if (keyName.Equals(";", comparisonType))
			{
				return KeyCode.Semicolon;
			}
			if (keyName.Equals("mouse 0", comparisonType))
			{
				return KeyCode.Mouse0;
			}
			if (keyName.Equals("mouse 1", comparisonType))
			{
				return KeyCode.Mouse1;
			}
			if (keyName.Equals("mouse 2", comparisonType))
			{
				return KeyCode.Mouse2;
			}
			if (keyName.Equals("mouse 3", comparisonType))
			{
				return KeyCode.Mouse3;
			}
			if (keyName.Equals("mouse 4", comparisonType))
			{
				return KeyCode.Mouse4;
			}
			if (keyName.Equals("left mouse", comparisonType))
			{
				return KeyCode.Mouse0;
			}
			if (keyName.Equals("right mouse", comparisonType))
			{
				return KeyCode.Mouse1;
			}
			if (keyName.Equals("LMB", comparisonType))
			{
				return KeyCode.Mouse0;
			}
			if (keyName.Equals("RMB", comparisonType))
			{
				return KeyCode.Mouse1;
			}
			if (keyName.Equals("MMB", comparisonType))
			{
				return KeyCode.Mouse2;
			}
			if (keyName.Equals("F1", comparisonType))
			{
				return KeyCode.F1;
			}
			if (keyName.Equals("F2", comparisonType))
			{
				return KeyCode.F2;
			}
			if (keyName.Equals("F3", comparisonType))
			{
				return KeyCode.F3;
			}
			if (keyName.Equals("F4", comparisonType))
			{
				return KeyCode.F4;
			}
			if (keyName.Equals("F5", comparisonType))
			{
				return KeyCode.F5;
			}
			if (keyName.Equals("F6", comparisonType))
			{
				return KeyCode.F6;
			}
			if (keyName.Equals("F7", comparisonType))
			{
				return KeyCode.F7;
			}
			if (keyName.Equals("F8", comparisonType))
			{
				return KeyCode.F8;
			}
			if (keyName.Equals("F9", comparisonType))
			{
				return KeyCode.F9;
			}
			if (keyName.Equals("F10", comparisonType))
			{
				return KeyCode.F10;
			}
			if (keyName.Equals("F11", comparisonType))
			{
				return KeyCode.F11;
			}
			if (keyName.Equals("F12", comparisonType))
			{
				return KeyCode.F12;
			}
			return KeyCode.None;
		}

		public const string CF_EMPTY_AXIS = "cfEmpty";

		public const string CF_SCROLL_WHEEL_X_AXIS = "cfScroll0";

		public const string CF_SCROLL_WHEEL_Y_AXIS = "cfScroll1";

		public const string CF_MOUSE_DELTA_X_AXIS = "cfMouseX";

		public const string CF_MOUSE_DELTA_Y_AXIS = "cfMouseY";

		public const string DEFAULT_LEFT_STICK_NAME = "LeftStick";

		public const string DEFAULT_RIGHT_STICK_NAME = "RightStick";

		public const string DEFAULT_VERT_SCROLL_WHEEL_NAME = "Mouse ScrollWheel";

		public const string DEFAULT_HORZ_SCROLL_WHEEL_NAME = "Mouse ScrollWheel Secondary";

		public bool autoActivate = true;

		public bool overrideActiveRig = true;

		public bool hideWhenDisactivated = true;

		public bool disableWhenDisactivated;

		public bool hideWhenTouchScreenIsUnused = true;

		public float hideWhenTouchScreenIsUnusedDelay;

		public bool hideWhenGamepadIsActivated = true;

		public float hideWhenGamepadIsActivatedDelay;

		public float fingerRadiusInCm = 0.25f;

		public bool swipeOverFromNothing;

		public float controlBaseAlphaAnimDuration = 0.5f;

		public float animatorMaxAnimDuration = 0.2f;

		public float ninetyDegTurnMouseDelta = 500f;

		public float ninetyDegTurnTouchSwipeInCm = 4f;

		public float ninetyDegTurnAnalogDuration = 0.75f;

		public float virtualScreenDiameterInches = 4f;

		public int scrollStepsPerNinetyDegTurn = 10;

		public const float TOUCH_SMOOTHING_MAX_TIME = 0.1f;

		[NonSerialized]
		private float elapsedSinceLastTouch;

		[NonSerialized]
		private bool touchControlsSleeping;

		public InputRig.VirtualJoystickConfigCollection joysticks;

		public InputRig.AxisConfigCollection axes;

		public List<KeyCode> keyboardBlockedCodes;

		[NonSerialized]
		private BitArray keysPrev;

		[NonSerialized]
		private BitArray keysCur;

		[NonSerialized]
		private BitArray keysNext;

		[NonSerialized]
		private BitArray keysMuted;

		[NonSerialized]
		private BitArray keysBlocked;

		[NonSerialized]
		private bool keysNextSomeDown;

		[NonSerialized]
		private bool keysNextSomeOn;

		[NonSerialized]
		private bool keysCurSomeOn;

		[NonSerialized]
		private bool keysCurSomeDown;

		public JoystickConfig dpadConfig;

		public JoystickConfig leftStickConfig;

		public JoystickConfig rightStickConfig;

		public AnalogConfig leftTriggerAnalogConfig;

		public AnalogConfig rightTriggerAnalogConfig;

		public InputRig.GamepadConfig[] gamepads;

		public InputRig.GamepadConfig anyGamepad;

		public InputRig.RigSwitchCollection rigSwitches;

		public InputRig.AutomaticInputConfigCollection autoInputList;

		[NonSerialized]
		private List<TouchControl> touchControls;

		public InputRig.TiltConfig tilt;

		public InputRig.MouseConfig mouseConfig;

		public InputRig.ScrollWheelState scrollWheel;

		private FixedTimeStepController fixedTimeStep;

		private const float MAX_DELTA_TIME = 0.2f;

		private const float DELTA_TIME_SMOOTHING_FACTOR = 0.1f;

		private const float DELTA_TIME_SMOOTHING_TIME = 1f;

		private const int FIXED_FPS = 120;

		private bool touchControlsHidden;

		private float analogToEmuMouseScale;

		private float analogToUniversalScale;

		private float analogToRawDeltaScale;

		private float scrollToEmuMouseScale;

		private float scrollToUniversalScale;

		private float mousePointsToUniversalScale;

		private float touchPixelsToEmuMouseScale;

		private float touchPixelsToUniversalScale;

		private int storedHorzRes;

		private int storedVertRes;

		private float storedDPCM;

		private const int MAX_EMU_TOUCHES = 8;

		private List<InputRig.EmulatedTouchState> emuTouches;

		private List<InputRig.EmulatedTouchState> emuTouchesOrdered;

		private InputRig.Touch[] emuOutputTouches;

		private bool emuOutputTouchesDirty;

		public struct Touch
		{
			static Touch()
			{
				InputRig.Touch.Dummy.phase = TouchPhase.Canceled;
				InputRig.Touch.EmptyArray = new InputRig.Touch[0];
			}

			public Touch(UnityEngine.Touch t)
			{
				this.phase = t.phase;
				this.deltaTime = t.deltaTime;
				this.fingerId = t.fingerId;
				this.tapCount = t.tapCount;
				this.position = t.position;
				this.rawPosition = t.rawPosition;
				this.deltaPosition = t.deltaPosition;
				this.altitudeAngle = 0f;
				this.azimuthAngle = 0f;
				this.maximumPossiblePressure = 1f;
				this.pressure = 1f;
				this.radius = 1f;
				this.radiusVariance = 0f;
				this.type = TouchType.Direct;
			}

			public static InputRig.Touch[] TranslateUnityTouches(UnityEngine.Touch[] tarr)
			{
				if (tarr == null || tarr.Length == 0)
				{
					return InputRig.Touch.EmptyArray;
				}
				if (InputRig.Touch.mTranslatedArray == null || tarr.Length != InputRig.Touch.mTranslatedArray.Length)
				{
					InputRig.Touch.mTranslatedArray = new InputRig.Touch[tarr.Length];
				}
				for (int i = 0; i < tarr.Length; i++)
				{
					InputRig.Touch.mTranslatedArray[i] = new InputRig.Touch(tarr[i]);
				}
				return InputRig.Touch.mTranslatedArray;
			}

			public TouchPhase phase;

			public float deltaTime;

			public int fingerId;

			public int tapCount;

			public Vector2 position;

			public Vector2 rawPosition;

			public Vector2 deltaPosition;

			public float altitudeAngle;

			public float azimuthAngle;

			public float maximumPossiblePressure;

			public float pressure;

			public float radius;

			public float radiusVariance;

			public TouchType type;

			public static InputRig.Touch Dummy = default(InputRig.Touch);

			public static InputRig.Touch[] EmptyArray;

			private static InputRig.Touch[] mTranslatedArray;
		}

		private class EmulatedTouchState
		{
			public EmulatedTouchState()
			{
				this.Reset();
			}

			public EmulatedTouchState(int fingerId)
			{
				this.fingerId = fingerId;
				this.touch = new TouchGestureBasicState();
				this.outputTouch = default(InputRig.Touch);
			}

			public bool IsUsed()
			{
				return this.isUsed;
			}

			public bool IsActive()
			{
				return this.touch.PressedRaw() || this.touch.JustReleasedRaw();
			}

			public void Start(Vector2 pos)
			{
				this.isUsed = true;
				this.updatedThisFrame = true;
				this.touch.OnTouchStart(pos, pos, 0f, false, false, 1f);
			}

			public void UpdatePos(Vector2 pos)
			{
				this.updatedThisFrame = true;
				this.touch.OnTouchMove(pos);
			}

			public void EndTouch(bool cancel)
			{
				this.updatedThisFrame = true;
				this.isUsed = false;
				this.touch.OnTouchEnd(cancel);
			}

			public void Reset()
			{
				this.isUsed = false;
				this.touch.Reset();
				this.SyncOutputTouch();
			}

			private void SyncOutputTouch()
			{
				this.outputTouch.fingerId = this.fingerId;
				this.outputTouch.deltaTime = Time.unscaledDeltaTime;
				this.outputTouch.tapCount = 0;
				if (this.touch.JustPressedRaw())
				{
					this.outputTouch.phase = TouchPhase.Began;
				}
				else if (this.touch.JustReleasedRaw())
				{
					this.outputTouch.phase = TouchPhase.Ended;
				}
				else if (this.touch.PressedRaw())
				{
					this.outputTouch.phase = ((this.touch.GetDeltaVecRaw().sqrMagnitude <= 0.001f) ? TouchPhase.Stationary : TouchPhase.Moved);
				}
				else
				{
					this.outputTouch.phase = TouchPhase.Canceled;
				}
				this.outputTouch.position = (this.outputTouch.rawPosition = this.touch.GetCurPosRaw());
				this.outputTouch.deltaPosition = this.touch.GetDeltaVecRaw();
			}

			public void Update()
			{
				if (this.isUsed && !this.updatedThisFrame)
				{
					this.EndTouch(false);
				}
				this.touch.Update();
				this.SyncOutputTouch();
				this.updatedThisFrame = false;
			}

			private int fingerId;

			public TouchGestureBasicState touch;

			public InputRig.Touch outputTouch;

			private bool isUsed;

			private bool updatedThisFrame;
		}

		[Serializable]
		public class RigSwitchCollection : InputRig.NamedConfigCollection<InputRig.RigSwitch>
		{
			public RigSwitchCollection(InputRig rig) : base(rig, 4)
			{
			}

			public bool changed
			{
				get
				{
					return this._changed;
				}
				set
				{
					this._changed = value;
				}
			}

			public InputRig.RigSwitch Add(string name, bool undoable)
			{
				InputRig.RigSwitch rigSwitch = base.Get(name);
				if (rigSwitch != null)
				{
					return rigSwitch;
				}
				rigSwitch = new InputRig.RigSwitch();
				rigSwitch.name = name;
				this.list.Add(rigSwitch);
				return rigSwitch;
			}

			public void SetSwitchState(string name, ref int cachedId, bool state)
			{
				InputRig.RigSwitch rigSwitch = base.Get(name, ref cachedId);
				if (rigSwitch == null)
				{
					return;
				}
				if (rigSwitch.GetState() != state)
				{
					this.changed = true;
				}
				rigSwitch.SetState(state);
			}

			public void SetSwitchState(string name, bool state)
			{
				int num = 0;
				this.SetSwitchState(name, ref num, state);
			}

			public bool GetSwitchState(string name, ref int cachedId, bool fallbackValue)
			{
				InputRig.RigSwitch rigSwitch = base.Get(name, ref cachedId);
				return (rigSwitch != null) ? rigSwitch.GetState() : fallbackValue;
			}

			public bool GetSwitchState(string name, bool fallbackValue)
			{
				InputRig.RigSwitch rigSwitch = base.Get(name);
				return (rigSwitch != null) ? rigSwitch.GetState() : fallbackValue;
			}

			public bool ToggleSwitchState(string name, ref int cachedId, bool fallbackValue)
			{
				InputRig.RigSwitch rigSwitch = base.Get(name, ref cachedId);
				if (rigSwitch == null)
				{
					return fallbackValue;
				}
				this.changed = true;
				return rigSwitch.ToggleState();
			}

			public bool ToggleSwitchState(string name, bool fallbackValue)
			{
				int num = 0;
				return this.ToggleSwitchState(name, ref num, fallbackValue);
			}

			public void SetAll(bool state)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					InputRig.RigSwitch rigSwitch = this.list[i];
					if (rigSwitch.GetState() != state)
					{
						this.changed = true;
						rigSwitch.SetState(state);
					}
				}
			}

			public void ResetSwitch(string name, ref int cachedId)
			{
				InputRig.RigSwitch rigSwitch = base.Get(name, ref cachedId);
				if (rigSwitch == null)
				{
					return;
				}
				if (rigSwitch.GetState() != rigSwitch.defaultState)
				{
					this.changed = true;
					rigSwitch.SetState(rigSwitch.defaultState);
				}
			}

			public void ResetSwitch(string name)
			{
				int num = 0;
				this.ResetSwitch(name, ref num);
			}

			public void Reset()
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					InputRig.RigSwitch rigSwitch = this.list[i];
					if (rigSwitch.GetState() != rigSwitch.defaultState)
					{
						this.changed = true;
						rigSwitch.SetState(rigSwitch.defaultState);
					}
				}
			}

			public int GetSwitchId(string name)
			{
				int num = -1;
				return (base.Get(name, ref num) != null) ? num : -1;
			}

			private bool _changed;
		}

		[Serializable]
		public class RigSwitch : InputRig.NamedConfigElement
		{
			public RigSwitch()
			{
			}

			public RigSwitch(string name)
			{
				this.name = name;
			}

			public void SetState(bool state)
			{
				this.state = state;
			}

			public bool GetState()
			{
				return this.state;
			}

			public bool ToggleState()
			{
				return this.state = !this.state;
			}

			public override void Reset()
			{
				this.state = this.defaultState;
			}

			public bool defaultState;

			private bool state;
		}

		[Serializable]
		public class GamepadConfig : IBindingContainer
		{
			public GamepadConfig(InputRig rig)
			{
				this.BasicConstructor(rig);
			}

			private void BasicConstructor(InputRig rig)
			{
				this.enabled = false;
				this.digiFaceDownBinding = new DigitalBinding(null);
				this.digiFaceRightBinding = new DigitalBinding(null);
				this.digiFaceLeftBinding = new DigitalBinding(null);
				this.digiFaceUpBinding = new DigitalBinding(null);
				this.digiStartBinding = new DigitalBinding(null);
				this.digiSelectBinding = new DigitalBinding(null);
				this.digiL1Binding = new DigitalBinding(null);
				this.digiR1Binding = new DigitalBinding(null);
				this.digiL2Binding = new DigitalBinding(null);
				this.digiR2Binding = new DigitalBinding(null);
				this.digiL3Binding = new DigitalBinding(null);
				this.digiR3Binding = new DigitalBinding(null);
				this.analogL2Binding = new AxisBinding(null);
				this.analogR2Binding = new AxisBinding(null);
				this.leftStickStateBinding = new JoystickStateBinding(null);
				this.rightStickStateBinding = new JoystickStateBinding(null);
				this.dpadStateBinding = new JoystickStateBinding(null);
			}

			public void SyncGamepad(GamepadManager.Gamepad gamepad, InputRig rig)
			{
				if (!this.enabled || gamepad == null)
				{
					return;
				}
				gamepad.leftStick.SyncJoyState(this.leftStickStateBinding, rig);
				gamepad.rightStick.SyncJoyState(this.rightStickStateBinding, rig);
				gamepad.dpad.SyncJoyState(this.dpadStateBinding, rig);
				gamepad.keys[0].SyncDigital(this.digiFaceDownBinding, rig);
				gamepad.keys[1].SyncDigital(this.digiFaceRightBinding, rig);
				gamepad.keys[3].SyncDigital(this.digiFaceLeftBinding, rig);
				gamepad.keys[2].SyncDigital(this.digiFaceUpBinding, rig);
				gamepad.keys[4].SyncDigital(this.digiStartBinding, rig);
				gamepad.keys[5].SyncDigital(this.digiSelectBinding, rig);
				gamepad.keys[6].SyncDigital(this.digiL1Binding, rig);
				gamepad.keys[7].SyncDigital(this.digiR1Binding, rig);
				gamepad.keys[8].SyncDigital(this.digiL2Binding, rig);
				gamepad.keys[9].SyncDigital(this.digiR2Binding, rig);
				gamepad.keys[10].SyncDigital(this.digiL3Binding, rig);
				gamepad.keys[11].SyncDigital(this.digiR3Binding, rig);
				gamepad.keys[8].SyncAnalog(this.analogL2Binding, rig);
				gamepad.keys[9].SyncAnalog(this.analogR2Binding, rig);
			}

			public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
			{
				descList.Add(this.digiFaceDownBinding, "Bottom Face Button", parentMenuPath, undoObject);
				descList.Add(this.digiFaceRightBinding, "Right Face Button", parentMenuPath, undoObject);
				descList.Add(this.digiFaceUpBinding, "Top Face Button", parentMenuPath, undoObject);
				descList.Add(this.digiFaceLeftBinding, "Left Face Button", parentMenuPath, undoObject);
				descList.Add(this.digiL1Binding, "L1", parentMenuPath, undoObject);
				descList.Add(this.digiR1Binding, "R1", parentMenuPath, undoObject);
				descList.Add(this.digiL2Binding, "L2 (Digital)", parentMenuPath, undoObject);
				descList.Add(this.digiR2Binding, "R2 (Digital)", parentMenuPath, undoObject);
				descList.Add(this.analogL2Binding, "L2 (Analog)", parentMenuPath, undoObject);
				descList.Add(this.analogR2Binding, "R2 (Analog)", parentMenuPath, undoObject);
				descList.Add(this.digiL3Binding, "L3", parentMenuPath, undoObject);
				descList.Add(this.digiR3Binding, "R3", parentMenuPath, undoObject);
				descList.Add(this.digiStartBinding, "Start", parentMenuPath, undoObject);
				descList.Add(this.digiSelectBinding, "Select (Back)", parentMenuPath, undoObject);
				descList.Add(this.dpadStateBinding, "D-Pad State", parentMenuPath, undoObject);
				descList.Add(this.leftStickStateBinding, "Left Stick State", parentMenuPath, undoObject);
				descList.Add(this.rightStickStateBinding, "Right Stick State", parentMenuPath, undoObject);
			}

			public bool IsBoundToKey(KeyCode key, InputRig rig)
			{
				return this.digiFaceDownBinding.IsBoundToKey(key, rig) || this.digiFaceRightBinding.IsBoundToKey(key, rig) || this.digiFaceUpBinding.IsBoundToKey(key, rig) || this.digiFaceLeftBinding.IsBoundToKey(key, rig) || this.digiL1Binding.IsBoundToKey(key, rig) || this.digiR1Binding.IsBoundToKey(key, rig) || this.digiL2Binding.IsBoundToKey(key, rig) || this.digiR2Binding.IsBoundToKey(key, rig) || this.analogL2Binding.IsBoundToKey(key, rig) || this.analogR2Binding.IsBoundToKey(key, rig) || this.digiL3Binding.IsBoundToKey(key, rig) || this.digiR3Binding.IsBoundToKey(key, rig) || this.digiStartBinding.IsBoundToKey(key, rig) || this.digiSelectBinding.IsBoundToKey(key, rig) || this.dpadStateBinding.IsBoundToKey(key, rig) || this.leftStickStateBinding.IsBoundToKey(key, rig) || this.rightStickStateBinding.IsBoundToKey(key, rig);
			}

			public bool IsBoundToAxis(string axisName, InputRig rig)
			{
				return this.digiFaceDownBinding.IsBoundToAxis(axisName, rig) || this.digiFaceRightBinding.IsBoundToAxis(axisName, rig) || this.digiFaceUpBinding.IsBoundToAxis(axisName, rig) || this.digiFaceLeftBinding.IsBoundToAxis(axisName, rig) || this.digiL1Binding.IsBoundToAxis(axisName, rig) || this.digiR1Binding.IsBoundToAxis(axisName, rig) || this.digiL2Binding.IsBoundToAxis(axisName, rig) || this.digiR2Binding.IsBoundToAxis(axisName, rig) || this.analogL2Binding.IsBoundToAxis(axisName, rig) || this.analogR2Binding.IsBoundToAxis(axisName, rig) || this.digiL3Binding.IsBoundToAxis(axisName, rig) || this.digiR3Binding.IsBoundToAxis(axisName, rig) || this.digiStartBinding.IsBoundToAxis(axisName, rig) || this.digiSelectBinding.IsBoundToAxis(axisName, rig) || this.dpadStateBinding.IsBoundToAxis(axisName, rig) || this.leftStickStateBinding.IsBoundToAxis(axisName, rig) || this.rightStickStateBinding.IsBoundToAxis(axisName, rig);
			}

			public bool IsEmulatingTouches()
			{
				return this.digiFaceDownBinding.IsEmulatingTouches() || this.digiFaceRightBinding.IsEmulatingTouches() || this.digiFaceUpBinding.IsEmulatingTouches() || this.digiFaceLeftBinding.IsEmulatingTouches() || this.digiL1Binding.IsEmulatingTouches() || this.digiR1Binding.IsEmulatingTouches() || this.digiL2Binding.IsEmulatingTouches() || this.digiR2Binding.IsEmulatingTouches() || this.analogL2Binding.IsEmulatingTouches() || this.analogR2Binding.IsEmulatingTouches() || this.digiL3Binding.IsEmulatingTouches() || this.digiR3Binding.IsEmulatingTouches() || this.digiStartBinding.IsEmulatingTouches() || this.digiSelectBinding.IsEmulatingTouches() || this.dpadStateBinding.IsEmulatingTouches() || this.leftStickStateBinding.IsEmulatingTouches() || this.rightStickStateBinding.IsEmulatingTouches();
			}

			public bool IsEmulatingMousePosition()
			{
				return this.digiFaceDownBinding.IsEmulatingMousePosition() || this.digiFaceRightBinding.IsEmulatingMousePosition() || this.digiFaceUpBinding.IsEmulatingMousePosition() || this.digiFaceLeftBinding.IsEmulatingMousePosition() || this.digiL1Binding.IsEmulatingMousePosition() || this.digiR1Binding.IsEmulatingMousePosition() || this.digiL2Binding.IsEmulatingMousePosition() || this.digiR2Binding.IsEmulatingMousePosition() || this.analogL2Binding.IsEmulatingMousePosition() || this.analogR2Binding.IsEmulatingMousePosition() || this.digiL3Binding.IsEmulatingMousePosition() || this.digiR3Binding.IsEmulatingMousePosition() || this.digiStartBinding.IsEmulatingMousePosition() || this.digiSelectBinding.IsEmulatingMousePosition() || this.dpadStateBinding.IsEmulatingMousePosition() || this.leftStickStateBinding.IsEmulatingMousePosition() || this.rightStickStateBinding.IsEmulatingMousePosition();
			}

			public bool enabled;

			public DigitalBinding digiFaceDownBinding;

			public DigitalBinding digiFaceRightBinding;

			public DigitalBinding digiFaceLeftBinding;

			public DigitalBinding digiFaceUpBinding;

			public DigitalBinding digiStartBinding;

			public DigitalBinding digiSelectBinding;

			public DigitalBinding digiL1Binding;

			public DigitalBinding digiR1Binding;

			public DigitalBinding digiL2Binding;

			public DigitalBinding digiR2Binding;

			public DigitalBinding digiL3Binding;

			public DigitalBinding digiR3Binding;

			public AxisBinding analogL2Binding;

			public AxisBinding analogR2Binding;

			public JoystickStateBinding leftStickStateBinding;

			public JoystickStateBinding rightStickStateBinding;

			public JoystickStateBinding dpadStateBinding;

			[NonSerialized]
			private JoystickState leftStickState;

			[NonSerialized]
			private JoystickState rigthStickState;

			[NonSerialized]
			private JoystickState dpadState;
		}

		[Serializable]
		public class VirtualJoystickConfig : InputRig.NamedConfigElement, IBindingContainer
		{
			public VirtualJoystickConfig()
			{
				this.BasicConstructor();
			}

			public VirtualJoystickConfig(string targetName, KeyCode keyUp, KeyCode keyRight, KeyCode keyDown, KeyCode keyLeft)
			{
				this.BasicConstructor();
				this.name = targetName;
				this.keyboardUp = keyUp;
				this.keyboardRight = keyRight;
				this.keyboardDown = keyDown;
				this.keyboardLeft = keyLeft;
			}

			private void BasicConstructor()
			{
				this.name = "Joystick";
				this.keyboardUp = KeyCode.W;
				this.keyboardRight = KeyCode.D;
				this.keyboardDown = KeyCode.S;
				this.keyboardLeft = KeyCode.A;
				if (this.joystickConfig == null)
				{
					this.joystickConfig = new JoystickConfig();
				}
				this.joystickState = new JoystickState(this.joystickConfig);
				this.joyStateBinding = new JoystickStateBinding(null);
			}

			public override void Reset()
			{
				this.joystickState.Reset();
			}

			public override void Update(InputRig rig)
			{
				this.joystickState.ApplyDigital(InputRig.GetSourceKeyState(this.keyboardUp), InputRig.GetSourceKeyState(this.keyboardRight), InputRig.GetSourceKeyState(this.keyboardDown), InputRig.GetSourceKeyState(this.keyboardLeft));
				this.joystickState.Update();
				this.joyStateBinding.SyncJoyState(this.joystickState, rig);
			}

			public void SetState(JoystickState state)
			{
				if (this.joystickState != null)
				{
					this.joystickState.ApplyState(state);
				}
			}

			public bool IsBoundToAxis(string axisName, InputRig rig)
			{
				return this.joyStateBinding.IsBoundToAxis(axisName, rig);
			}

			public bool IsBoundToKey(KeyCode key, InputRig rig)
			{
				return this.joyStateBinding.IsBoundToKey(key, rig);
			}

			public bool IsEmulatingTouches()
			{
				return false;
			}

			public bool IsEmulatingMousePosition()
			{
				return false;
			}

			public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
			{
				descList.Add(this.joyStateBinding, "Joystick State", parentMenuPath, undoObject);
			}

			public bool disableOnMobile;

			public JoystickConfig joystickConfig;

			public JoystickState joystickState;

			public KeyCode keyboardUp;

			public KeyCode keyboardRight;

			public KeyCode keyboardDown;

			public KeyCode keyboardLeft;

			public JoystickStateBinding joyStateBinding;
		}

		[Serializable]
		public class VirtualJoystickConfigCollection : InputRig.NamedConfigCollection<InputRig.VirtualJoystickConfig>
		{
			public VirtualJoystickConfigCollection(InputRig rig, int capacity) : base(rig, capacity)
			{
			}

			public InputRig.VirtualJoystickConfig Add(string name, bool undoable)
			{
				InputRig.VirtualJoystickConfig virtualJoystickConfig = base.Get(name);
				if (virtualJoystickConfig != null)
				{
					return virtualJoystickConfig;
				}
				virtualJoystickConfig = new InputRig.VirtualJoystickConfig();
				virtualJoystickConfig.name = name;
				this.list.Add(virtualJoystickConfig);
				return virtualJoystickConfig;
			}
		}

		public enum AxisType
		{
			UnsignedAnalog,
			SignedAnalog,
			Digital,
			ScrollWheel,
			Delta
		}

		public enum InputSource
		{
			Digital,
			Analog,
			MouseDelta,
			TouchDelta,
			NormalizedDelta,
			Scroll
		}

		public enum DeltaTransformMode
		{
			Universal,
			EmulateMouse,
			Raw
		}

		[Serializable]
		public class AxisConfig : InputRig.NamedConfigElement
		{
			public AxisConfig()
			{
				this.name = "Axis";
				this.axisType = InputRig.AxisType.Digital;
				this.deltaMode = InputRig.DeltaTransformMode.EmulateMouse;
				this.keyboardPositive = KeyCode.None;
				this.keyboardNegative = KeyCode.None;
				this.rawSmoothingTime = 0f;
				this.smoothingTime = 0f;
				this.analogToDigitalThresh = 0.125f;
				this.digitalToAnalogAccelTime = 0.25f;
				this.digitalToAnalogDecelTime = 0.25f;
				this.scrollToAnalogSmoothingTime = 0.1f;
				this.scale = 1f;
				this.affectSourceKeys = true;
				this.digitalToScrollRepeatInterval = 0.5f;
				this.digitalToScrollDelay = 1f;
				this.digitalToScrollAutoRepeat = true;
				this.scrollToAnalogStepDuration = 0.1f;
				this.scrollToAnalogSmoothingTime = 0.05f;
				this.scrollToDeltaSmoothingTime = 0.1f;
				this.Reset();
			}

			public bool frDigitalPos { get; protected set; }

			public bool frDigitalNeg { get; protected set; }

			public float frAnalogPos { get; protected set; }

			public float frAnalogNeg { get; protected set; }

			public float frMouseDeltaPos { get; protected set; }

			public float frMouseDeltaNeg { get; protected set; }

			public float frTouchDeltaPos { get; protected set; }

			public float frTouchDeltaNeg { get; protected set; }

			public float frNormalizedDeltaPos { get; protected set; }

			public float frNormalizedDeltaNeg { get; protected set; }

			public int frScrollDelta { get; protected set; }

			public static InputRig.AxisConfig CreateDigital(string name, KeyCode key)
			{
				return new InputRig.AxisConfig
				{
					name = name,
					axisType = InputRig.AxisType.Digital,
					keyboardPositive = key,
					keyboardNegative = KeyCode.None
				};
			}

			public static InputRig.AxisConfig CreateScrollWheel(string name, KeyCode keyPositive, KeyCode keyNegative)
			{
				return new InputRig.AxisConfig
				{
					name = name,
					axisType = InputRig.AxisType.ScrollWheel,
					keyboardPositive = keyPositive,
					keyboardNegative = keyNegative
				};
			}

			public static InputRig.AxisConfig CreateAnalog(string name, KeyCode keyPositive)
			{
				return new InputRig.AxisConfig
				{
					name = name,
					axisType = InputRig.AxisType.UnsignedAnalog,
					keyboardPositive = keyPositive,
					keyboardNegative = KeyCode.None
				};
			}

			public static InputRig.AxisConfig CreateSignedAnalog(string name, KeyCode keyPositive, KeyCode keyNegative)
			{
				return new InputRig.AxisConfig
				{
					name = name,
					axisType = InputRig.AxisType.SignedAnalog,
					keyboardPositive = keyPositive,
					keyboardNegative = keyNegative
				};
			}

			public static InputRig.AxisConfig CreateDelta(string name, KeyCode keyPositive, KeyCode keyNegative)
			{
				return new InputRig.AxisConfig
				{
					name = name,
					axisType = InputRig.AxisType.Delta,
					keyboardPositive = keyPositive,
					keyboardNegative = keyNegative
				};
			}

			public override void Reset()
			{
				this.val = 0f;
				this.valRaw = 0f;
				this.valSmoothVel = 0f;
				this.valRawSmoothVel = 0f;
				this.frAnalogPos = 0f;
				this.frAnalogNeg = 0f;
				this.frDigitalNeg = false;
				this.frDigitalPos = false;
				this.frMouseDeltaPos = 0f;
				this.frMouseDeltaNeg = 0f;
				this.frTouchDeltaPos = 0f;
				this.frTouchDeltaNeg = 0f;
				this.frNormalizedDeltaPos = 0f;
				this.frNormalizedDeltaNeg = 0f;
				this.deltaAccumTarget = 0f;
				this.deltaAccumSmoothCur = 0f;
				this.deltaAccumSmoothPrev = 0f;
				this.deltaAccumRawCur = 0f;
				this.deltaAccumRawPrev = 0f;
				this.digiToScrollPositivePrev = false;
				this.digiToScrollNegativePrev = false;
				this.digiToScrollDelayPhase = true;
				this.scrollElapsedSinceChange = 0f;
			}

			public void MuteUntilRelease()
			{
				this.muteUntilRelease = true;
			}

			public bool IsMuted()
			{
				return this.muteUntilRelease;
			}

			public void ApplyKeyboardInput()
			{
				if (InputRig.GetSourceKeyState(this.keyboardPositive) || InputRig.GetSourceKeyState(this.keyboardPositiveAlt0) || InputRig.GetSourceKeyState(this.keyboardPositiveAlt1) || InputRig.GetSourceKeyState(this.keyboardPositiveAlt2))
				{
					this.SetSignedDigital(true);
				}
				if (InputRig.GetSourceKeyState(this.keyboardNegative) || InputRig.GetSourceKeyState(this.keyboardNegativeAlt0) || InputRig.GetSourceKeyState(this.keyboardNegativeAlt1) || InputRig.GetSourceKeyState(this.keyboardNegativeAlt2))
				{
					this.SetSignedDigital(false);
				}
			}

			public override void Update(InputRig rig)
			{
				this.scrollPrev = this.scrollCur;
				this.digiPrev = this.digiCur;
				this.analogToDigitalNegCur = (this.frAnalogPos + this.frAnalogNeg < -this.analogToDigitalThresh);
				this.analogToDigitalPosCur = (this.frAnalogPos + this.frAnalogNeg > this.analogToDigitalThresh);
				float num = (float)(((!this.frDigitalNeg) ? 0 : -1) + ((!this.frDigitalPos) ? 0 : 1));
				float secondsPerUnit = (!this.frDigitalNeg && !this.frDigitalPos) ? this.digitalToAnalogDecelTime : this.digitalToAnalogAccelTime;
				if (this.snap && Mathf.Abs(num) > 0.1f && num >= 0f != this.digitalToAnalogVal >= 0f)
				{
					this.digitalToAnalogVal = 0f;
				}
				this.digitalToAnalogVal = CFUtils.MoveTowards(this.digitalToAnalogVal, num, secondsPerUnit, CFUtils.realDeltaTime, 0.01f);
				float num2 = CFUtils.ApplyDeltaInput(this.frAnalogNeg + this.frAnalogPos, this.digitalToAnalogVal);
				num2 = Mathf.Clamp(num2, -1f, 1f);
				switch (this.axisType)
				{
				case InputRig.AxisType.UnsignedAnalog:
				case InputRig.AxisType.SignedAnalog:
					if (this.scrollToAnalogTimeRemaining != 0f)
					{
						this.scrollToAnalogTimeRemaining = Mathf.MoveTowards(this.scrollToAnalogTimeRemaining, 0f, CFUtils.realDeltaTimeClamped);
					}
					if (this.frScrollDelta != 0)
					{
						this.scrollToAnalogTimeRemaining += (float)this.frScrollDelta * this.scrollToAnalogStepDuration;
					}
					if (this.scrollToAnalogTimeRemaining != 0f || this.scrollToAnalogValue != 0f)
					{
						this.scrollToAnalogValue = CFUtils.SmoothTowards(this.scrollToAnalogValue, (float)((this.scrollToAnalogTimeRemaining != 0f) ? ((this.scrollToAnalogTimeRemaining >= 0f) ? 1 : -1) : 0), this.scrollToAnalogSmoothingTime, CFUtils.realDeltaTimeClamped, 0.001f, 0.75f);
						num2 = CFUtils.ApplyDeltaInput(num2, this.scrollToAnalogValue);
					}
					this.digiCur = (this.frDigitalNeg || this.frDigitalPos || this.analogToDigitalNegCur || this.analogToDigitalPosCur || this.scrollToAnalogTimeRemaining != 0f);
					if (this.axisType == InputRig.AxisType.UnsignedAnalog && num2 < 0f)
					{
						num2 = 0f;
					}
					this.valRaw = CFUtils.SmoothDamp(this.valRaw, num2, ref this.valRawSmoothVel, this.rawSmoothingTime, CFUtils.realDeltaTime, 0.001f);
					this.val = CFUtils.SmoothDamp(this.val, num2, ref this.valSmoothVel, this.smoothingTime, CFUtils.realDeltaTime, 0.001f);
					break;
				case InputRig.AxisType.Digital:
					this.scrollToDigitalClicksRemaining += this.frScrollDelta;
					if (this.scrollToDigitalClickOn)
					{
						this.scrollToDigitalClickOn = false;
					}
					else if (this.scrollToDigitalClicksRemaining != 0)
					{
						this.scrollToDigitalClickOn = true;
						if (this.scrollToDigitalClicksRemaining > 0)
						{
							this.scrollToDigitalClicksRemaining--;
							this.frDigitalPos = true;
						}
						else
						{
							this.scrollToDigitalClicksRemaining++;
							this.frDigitalNeg = true;
						}
					}
					this.digiCur = (this.frDigitalNeg || this.frDigitalPos || this.analogToDigitalNegCur || this.analogToDigitalPosCur || this.scrollToDigitalClickOn);
					this.val = (this.valRaw = (float)((!this.digiCur) ? 0 : 1));
					break;
				case InputRig.AxisType.ScrollWheel:
				{
					bool flag = this.frDigitalPos || this.analogToDigitalPosCur;
					bool flag2 = this.frDigitalNeg || this.analogToDigitalNegCur;
					int num3 = ((!flag) ? 0 : 1) + ((!flag2) ? 0 : -1);
					if (this.frScrollDelta != 0 || flag != this.digiToScrollPositivePrev || flag2 != this.digiToScrollNegativePrev)
					{
						this.digiToScrollDelayPhase = true;
						this.scrollElapsedSinceChange = 0f;
						if (num3 != 0 && !this.digiToScrollNegativePrev && !this.digiToScrollPositivePrev)
						{
							this.frScrollDelta = CFUtils.ApplyDeltaInputInt(this.frScrollDelta, num3);
						}
					}
					else if (this.digitalToScrollAutoRepeat && num3 != 0)
					{
						this.scrollElapsedSinceChange += CFUtils.realDeltaTimeClamped;
						if (this.scrollElapsedSinceChange > ((!this.digiToScrollDelayPhase) ? this.digitalToScrollRepeatInterval : this.digitalToScrollDelay))
						{
							this.digiToScrollDelayPhase = false;
							this.scrollElapsedSinceChange = 0f;
							this.frScrollDelta = CFUtils.ApplyDeltaInputInt(this.frScrollDelta, num3);
						}
					}
					this.digiToScrollPositivePrev = flag;
					this.digiToScrollNegativePrev = flag2;
					this.scrollCur += this.frScrollDelta;
					this.val = (this.valRaw = (float)(this.scrollCur - this.scrollPrev));
					this.digiCur = (this.frScrollDelta != 0);
					break;
				}
				case InputRig.AxisType.Delta:
				{
					float num4 = CFUtils.ApplyDeltaInput(rig.TransformTouchPixelDelta(this.frTouchDeltaPos + this.frTouchDeltaNeg, this.deltaMode), rig.TransformMousePointDelta(this.frMouseDeltaPos + this.frMouseDeltaNeg, this.deltaMode));
					num4 = CFUtils.ApplyDeltaInput(num4, rig.TransformNormalizedDelta(this.frNormalizedDeltaPos + this.frNormalizedDeltaNeg, this.deltaMode));
					num4 = CFUtils.ApplyDeltaInput(num4, rig.TransformScrollDelta((float)this.frScrollDelta, this.deltaMode));
					for (int i = 0; i < rig.fixedTimeStep.GetFrameSteps(); i++)
					{
						num4 = CFUtils.ApplyDeltaInput(num4, rig.TransformAnalogDelta(num2 * rig.fixedTimeStep.GetDeltaTime(), this.deltaMode));
					}
					this.deltaAccumRawPrev = this.deltaAccumRawCur;
					this.deltaAccumTarget += num4;
					this.deltaAccumSmoothPrev = this.deltaAccumSmoothCur;
					this.deltaAccumSmoothCur = CFUtils.SmoothDamp(this.deltaAccumSmoothCur, this.deltaAccumTarget, ref this.valSmoothVel, this.smoothingTime, CFUtils.realDeltaTime, 1E-05f);
					this.deltaAccumRawPrev = this.deltaAccumRawCur;
					this.deltaAccumRawCur = CFUtils.SmoothDamp(this.deltaAccumRawCur, this.deltaAccumTarget, ref this.valRawSmoothVel, this.rawSmoothingTime, CFUtils.realDeltaTime, 1E-05f);
					this.val = this.deltaAccumSmoothCur - this.deltaAccumSmoothPrev;
					this.valRaw = this.deltaAccumRawCur - this.deltaAccumRawPrev;
					break;
				}
				}
				if (this.muteUntilRelease)
				{
					this.val = 0f;
					this.valRaw = 0f;
					this.valRawSmoothVel = 0f;
					this.valSmoothVel = 0f;
					this.digiCur = false;
					this.digiPrev = false;
					this.digitalToAnalogVal = 0f;
					if (!this.frDigitalNeg && !this.frDigitalPos && !this.analogToDigitalNegCur && !this.analogToDigitalPosCur && this.frScrollDelta == 0)
					{
						this.muteUntilRelease = false;
					}
					if (this.axisType == InputRig.AxisType.Delta)
					{
						this.muteUntilRelease = false;
					}
				}
				if (this.analogToDigitalNegCur || this.frDigitalNeg)
				{
					if (this.affectSourceKeys)
					{
						rig.SetKeyCode(this.affectedKeyNegative);
					}
					else
					{
						rig.SetKeyCode(this.keyboardNegative);
						rig.SetKeyCode(this.keyboardNegativeAlt0);
						rig.SetKeyCode(this.keyboardNegativeAlt1);
						rig.SetKeyCode(this.keyboardNegativeAlt2);
					}
				}
				if (this.analogToDigitalPosCur || this.frDigitalPos)
				{
					if (this.affectSourceKeys)
					{
						rig.SetKeyCode(this.affectedKeyPositive);
					}
					else
					{
						rig.SetKeyCode(this.keyboardPositive);
						rig.SetKeyCode(this.keyboardPositiveAlt0);
						rig.SetKeyCode(this.keyboardPositiveAlt1);
						rig.SetKeyCode(this.keyboardPositiveAlt2);
					}
				}
				this.frAnalogPos = 0f;
				this.frAnalogNeg = 0f;
				this.frMouseDeltaPos = 0f;
				this.frMouseDeltaNeg = 0f;
				this.frTouchDeltaPos = 0f;
				this.frTouchDeltaNeg = 0f;
				this.frNormalizedDeltaPos = 0f;
				this.frNormalizedDeltaNeg = 0f;
				this.frScrollDelta = 0;
				this.frDigitalPos = false;
				this.frDigitalNeg = false;
			}

			public void Set(float v, InputRig.InputSource source)
			{
				switch (source)
				{
				case InputRig.InputSource.Digital:
					if (Mathf.Abs(v) > 0.5f)
					{
						this.SetSignedDigital(v >= 0f);
					}
					return;
				case InputRig.InputSource.Analog:
					this.SetAnalog(v);
					return;
				case InputRig.InputSource.MouseDelta:
					this.SetMouseDelta(v);
					return;
				case InputRig.InputSource.TouchDelta:
					this.SetTouchDelta(v);
					return;
				case InputRig.InputSource.NormalizedDelta:
					this.SetNormalizedDelta(v);
					return;
				case InputRig.InputSource.Scroll:
					this.SetScrollDelta(Mathf.RoundToInt(v));
					return;
				default:
					return;
				}
			}

			public void SetAnalog(float v)
			{
				this.frAnalogPos = CFUtils.ApplyPositveDeltaInput(this.frAnalogPos, v);
				this.frAnalogNeg = CFUtils.ApplyNegativeDeltaInput(this.frAnalogNeg, v);
			}

			public void SetDigital()
			{
				this.frDigitalPos = true;
			}

			public void SetSignedDigital(bool vpositive)
			{
				if (vpositive)
				{
					this.frDigitalPos = true;
				}
				else
				{
					this.frDigitalNeg = true;
				}
			}

			public void SetScrollDelta(int scrollDelta)
			{
				this.frScrollDelta = CFUtils.ApplyDeltaInputInt(this.frScrollDelta, scrollDelta);
			}

			public void SetTouchDelta(float touchDelta)
			{
				this.frTouchDeltaPos = CFUtils.ApplyPositveDeltaInput(this.frTouchDeltaPos, touchDelta);
				this.frTouchDeltaNeg = CFUtils.ApplyNegativeDeltaInput(this.frTouchDeltaNeg, touchDelta);
			}

			public void SetMouseDelta(float mouseDelta)
			{
				this.frMouseDeltaPos = CFUtils.ApplyPositveDeltaInput(this.frMouseDeltaPos, mouseDelta);
				this.frMouseDeltaNeg = CFUtils.ApplyNegativeDeltaInput(this.frMouseDeltaNeg, mouseDelta);
			}

			public void SetNormalizedDelta(float normalizedDelta)
			{
				this.frNormalizedDeltaPos = CFUtils.ApplyPositveDeltaInput(this.frNormalizedDeltaPos, normalizedDelta);
				this.frNormalizedDeltaNeg = CFUtils.ApplyNegativeDeltaInput(this.frNormalizedDeltaNeg, normalizedDelta);
			}

			public float GetAnalog()
			{
				if (this.axisType != InputRig.AxisType.ScrollWheel)
				{
					return this.val * this.scale;
				}
				return this.val;
			}

			public float GetAnalogRaw()
			{
				if (this.axisType != InputRig.AxisType.ScrollWheel)
				{
					return this.valRaw * this.scale;
				}
				return this.valRaw;
			}

			public bool IsControlledByInput()
			{
				return this.frDigitalNeg || this.frDigitalPos || Mathf.Abs(this.frAnalogNeg) > 0.001f || Mathf.Abs(this.frAnalogPos) > 0.001f || Mathf.Abs(this.frMouseDeltaNeg) > 0.001f || Mathf.Abs(this.frMouseDeltaPos) > 0.001f || Mathf.Abs(this.frNormalizedDeltaNeg) > 0.001f || Mathf.Abs(this.frNormalizedDeltaPos) > 0.001f || Mathf.Abs(this.frTouchDeltaNeg) > 0.001f || Mathf.Abs(this.frTouchDeltaPos) > 0.001f || this.frScrollDelta != 0;
			}

			public bool GetButton()
			{
				return this.digiCur;
			}

			public bool GetButtonDown()
			{
				return !this.digiPrev && this.digiCur;
			}

			public bool GetButtonUp()
			{
				return this.digiPrev && !this.digiCur;
			}

			public int GetSupportedInputSourceMask()
			{
				int result = 0;
				switch (this.axisType)
				{
				case InputRig.AxisType.UnsignedAnalog:
				case InputRig.AxisType.SignedAnalog:
					result = 35;
					break;
				case InputRig.AxisType.Digital:
					result = 35;
					break;
				case InputRig.AxisType.ScrollWheel:
					result = 35;
					break;
				case InputRig.AxisType.Delta:
					result = 63;
					break;
				}
				return result;
			}

			public bool DoesSupportInputSource(InputRig.InputSource source)
			{
				return (this.GetSupportedInputSourceMask() & 1 << (int)source) != 0;
			}

			private bool IsSignedAxis()
			{
				return this.axisType == InputRig.AxisType.Delta || this.axisType == InputRig.AxisType.ScrollWheel || this.axisType == InputRig.AxisType.SignedAnalog;
			}

			public bool DoesAffectKeyCode(KeyCode key)
			{
				if (!this.affectSourceKeys)
				{
					if (this.affectedKeyPositive == key || (this.IsSignedAxis() && this.affectedKeyNegative == key))
					{
						return true;
					}
				}
				else
				{
					if (key == this.keyboardPositive || key == this.keyboardPositiveAlt0 || key == this.keyboardPositiveAlt1 || key == this.keyboardPositiveAlt2)
					{
						return true;
					}
					if (this.IsSignedAxis() && (key == this.keyboardNegative || key == this.keyboardNegativeAlt0 || key == this.keyboardNegativeAlt1 || key == this.keyboardNegativeAlt2))
					{
						return true;
					}
				}
				return false;
			}

			public InputRig.AxisType axisType;

			public InputRig.DeltaTransformMode deltaMode;

			public bool snap;

			public float scale;

			public bool affectSourceKeys;

			public KeyCode affectedKeyPositive;

			public KeyCode affectedKeyNegative;

			public KeyCode keyboardPositive;

			public KeyCode keyboardPositiveAlt0;

			public KeyCode keyboardPositiveAlt1;

			public KeyCode keyboardPositiveAlt2;

			public KeyCode keyboardNegative;

			public KeyCode keyboardNegativeAlt0;

			public KeyCode keyboardNegativeAlt1;

			public KeyCode keyboardNegativeAlt2;

			public float analogToDigitalThresh;

			public float rawSmoothingTime;

			public float smoothingTime;

			public float digitalToAnalogAccelTime;

			public float digitalToAnalogDecelTime;

			public bool digitalToScrollAutoRepeat;

			public float digitalToScrollRepeatInterval;

			public float digitalToScrollDelay;

			public float scrollToAnalogSmoothingTime;

			public float scrollToAnalogStepDuration;

			public float scrollToDeltaSmoothingTime;

			private float valRaw;

			private float val;

			private int scrollPrev;

			private int scrollCur;

			private bool digiCur;

			private bool digiPrev;

			private bool muteUntilRelease;

			private float digitalToAnalogVal;

			private bool analogToDigitalNegCur;

			private bool analogToDigitalPosCur;

			private float valSmoothVel;

			private float valRawSmoothVel;

			private float scrollToAnalogTimeRemaining;

			private float scrollToAnalogValue;

			private int scrollToDigitalClicksRemaining;

			private bool scrollToDigitalClickOn;

			private float deltaAccumTarget;

			private float deltaAccumSmoothCur;

			private float deltaAccumSmoothPrev;

			private float deltaAccumRawCur;

			private float deltaAccumRawPrev;

			private float scrollElapsedSinceChange;

			private bool digiToScrollPositivePrev;

			private bool digiToScrollNegativePrev;

			private bool digiToScrollDelayPhase;
		}

		[Serializable]
		public class AxisConfigCollection : InputRig.NamedConfigCollection<InputRig.AxisConfig>
		{
			public AxisConfigCollection(InputRig rig, int capacity) : base(rig, capacity)
			{
			}

			public InputRig.AxisConfig Add(string name, InputRig.AxisType axisType, bool undoable)
			{
				InputRig.AxisConfig axisConfig = base.Get(name);
				if (axisConfig != null)
				{
					return axisConfig;
				}
				axisConfig = new InputRig.AxisConfig();
				axisConfig.name = name;
				axisConfig.axisType = axisType;
				this.list.Add(axisConfig);
				return axisConfig;
			}

			public void ApplyKeyboardInput()
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					this.list[i].ApplyKeyboardInput();
				}
			}

			public void MuteAllUntilRelease()
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					this.list[i].MuteUntilRelease();
				}
			}
		}

		[Serializable]
		public class AutomaticInputConfig : InputRig.NamedConfigElement
		{
			public AutomaticInputConfig()
			{
				this.targetBinding = new DigitalBinding(null);
				this.disabledByConditions = false;
				this.relatedAxisList = new List<InputRig.AutomaticInputConfig.RelatedAxis>();
				this.relatedKeyList = new List<InputRig.AutomaticInputConfig.RelatedKey>();
				this.disablingConditions = new DisablingConditionSet(null);
				this.disablingConditions.mobileModeRelation = DisablingConditionSet.MobileModeRelation.EnabledOnlyInMobileMode;
			}

			public void SetRig(InputRig rig)
			{
				this.disablingConditions.SetRig(rig);
				this.OnDisablingConditionsChange();
			}

			public void OnDisablingConditionsChange()
			{
				this.disabledByConditions = this.disablingConditions.IsInEffect();
			}

			public override void Update(InputRig rig)
			{
				if (this.disabledByConditions)
				{
					return;
				}
				for (int i = 0; i < this.relatedKeyList.Count; i++)
				{
					if (!this.relatedKeyList[i].IsEnabling(rig))
					{
						return;
					}
				}
				for (int j = 0; j < this.relatedAxisList.Count; j++)
				{
					if (!this.relatedAxisList[j].IsEnabling(rig))
					{
						return;
					}
				}
				this.targetBinding.Sync(true, rig, true);
			}

			public DigitalBinding targetBinding;

			public DisablingConditionSet disablingConditions;

			[NonSerialized]
			private bool disabledByConditions;

			public List<InputRig.AutomaticInputConfig.RelatedAxis> relatedAxisList;

			public List<InputRig.AutomaticInputConfig.RelatedKey> relatedKeyList;

			[Serializable]
			public class RelatedAxis
			{
				public RelatedAxis()
				{
					this.axisName = string.Empty;
				}

				public bool IsEnabling(InputRig rig)
				{
					InputRig.AxisConfig axisConfig = rig.GetAxisConfig(this.axisName, ref this.axisId);
					return axisConfig == null || axisConfig.IsControlledByInput() == this.mustBeControlledByInput;
				}

				public string axisName;

				public bool mustBeControlledByInput;

				private int axisId;
			}

			[Serializable]
			public class RelatedKey
			{
				public bool IsEnabling(InputRig rig)
				{
					return this.key == KeyCode.None || rig.GetKey(this.key) == this.mustBeControlledByInput;
				}

				public KeyCode key;

				public bool mustBeControlledByInput;
			}
		}

		[Serializable]
		public class AutomaticInputConfigCollection : InputRig.NamedConfigCollection<InputRig.AutomaticInputConfig>
		{
			public AutomaticInputConfigCollection(InputRig rig) : base(rig, 0)
			{
			}

			public void SetRig(InputRig rig)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					this.list[i].SetRig(rig);
				}
			}

			public InputRig.AutomaticInputConfig Add(string name, bool createUndo)
			{
				InputRig.AutomaticInputConfig automaticInputConfig = new InputRig.AutomaticInputConfig();
				automaticInputConfig.name = name;
				automaticInputConfig.SetRig(this.rig);
				this.list.Add(automaticInputConfig);
				return automaticInputConfig;
			}

			public void OnDisablingConditionsChange()
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					this.list[i].OnDisablingConditionsChange();
				}
			}
		}

		public class NamedConfigElement
		{
			public virtual void Reset()
			{
			}

			public virtual void Update(InputRig rig)
			{
			}

			public string name;
		}

		[Serializable]
		public class NamedConfigCollection<T> where T : InputRig.NamedConfigElement, new()
		{
			public NamedConfigCollection(InputRig rig, int capacity)
			{
				this.rig = rig;
				this.list = new List<T>(capacity);
			}

			public void ResetAll()
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					T t = this.list[i];
					t.Reset();
				}
			}

			public void Update(InputRig rig)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					T t = this.list[i];
					t.Update(rig);
				}
			}

			public T Get(string name, ref int cachedId)
			{
				if (cachedId >= 0 && cachedId < this.list.Count && this.list[cachedId].name == name)
				{
					return this.list[cachedId];
				}
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.list[i].name == name)
					{
						cachedId = i;
						return this.list[i];
					}
				}
				return (T)((object)null);
			}

			public T Get(string name)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.list[i].name == name)
					{
						return this.list[i];
					}
				}
				return (T)((object)null);
			}

			public List<T> list;

			[NonSerialized]
			protected InputRig rig;
		}

		[Serializable]
		public class TiltConfig : IBindingContainer
		{
			public TiltConfig(InputRig rig)
			{
				this.rig = rig;
				this.tiltState = new TiltState();
				this.rollAnalogConfig = new AnalogConfig();
				this.pitchAnalogConfig = new AnalogConfig();
				this.rollAnalogConfig.analogDeadZone = 0.3f;
				this.pitchAnalogConfig.analogDeadZone = 0.3f;
				this.disablingConditions = new DisablingConditionSet(rig);
				this.disablingConditions.disableWhenCursorIsUnlocked = false;
				this.disablingConditions.disableWhenTouchScreenInactive = false;
				this.disablingConditions.mobileModeRelation = DisablingConditionSet.MobileModeRelation.EnabledOnlyInMobileMode;
				this.rollBinding = new AxisBinding("Horizontal", false, null);
				this.pitchBinding = new AxisBinding("Vertical", false, null);
				this.rollLeftBinding = new DigitalBinding(null);
				this.rollRightBinding = new DigitalBinding(null);
				this.pitchForwardBinding = new DigitalBinding(null);
				this.pitchBackwardBinding = new DigitalBinding(null);
			}

			public void Reset()
			{
				this.digitalPitchDirection = 0;
				this.digitalRollDirection = 0;
				this.tiltState.Reset();
				this.OnDisablingConditionsChange();
			}

			public void OnDisablingConditionsChange()
			{
				this.disabledByRigSwitches = this.disablingConditions.IsInEffect();
			}

			public bool IsEnabled()
			{
				return !this.disabledByRigSwitches;
			}

			public void Update()
			{
				this.tiltState.InternalApplyVector(Input.acceleration);
				this.tiltState.Update();
				Vector2 analog = this.tiltState.GetAnalog();
				this.digitalRollDirection = this.rollAnalogConfig.GetSignedDigitalVal(analog.x, this.digitalRollDirection);
				this.digitalPitchDirection = this.pitchAnalogConfig.GetSignedDigitalVal(analog.y, this.digitalPitchDirection);
				if (this.IsEnabled())
				{
					if (this.rollBinding.enabled)
					{
						this.rollBinding.SyncFloat(this.rollAnalogConfig.GetAnalogVal(analog.x), InputRig.InputSource.Analog, this.rig);
					}
					if (this.pitchBinding.enabled)
					{
						this.pitchBinding.SyncFloat(this.pitchAnalogConfig.GetAnalogVal(analog.y), InputRig.InputSource.Analog, this.rig);
					}
					if (this.digitalRollDirection != 0)
					{
						((this.digitalRollDirection >= 0) ? this.rollRightBinding : this.rollLeftBinding).Sync(true, this.rig, false);
					}
					if (this.digitalPitchDirection != 0)
					{
						((this.digitalPitchDirection >= 0) ? this.pitchForwardBinding : this.pitchBackwardBinding).Sync(true, this.rig, false);
					}
				}
			}

			public bool IsBoundToAxis(string axisName, InputRig rig)
			{
				return this.rollBinding.IsBoundToAxis(axisName, rig) || this.pitchBinding.IsBoundToAxis(axisName, rig) || this.rollLeftBinding.IsBoundToAxis(axisName, rig) || this.rollRightBinding.IsBoundToAxis(axisName, rig) || this.pitchForwardBinding.IsBoundToAxis(axisName, rig) || this.pitchBackwardBinding.IsBoundToAxis(axisName, rig);
			}

			public bool IsBoundToKey(KeyCode key, InputRig rig)
			{
				return this.rollBinding.IsBoundToKey(key, rig) || this.pitchBinding.IsBoundToKey(key, rig) || this.rollLeftBinding.IsBoundToKey(key, rig) || this.rollRightBinding.IsBoundToKey(key, rig) || this.pitchForwardBinding.IsBoundToKey(key, rig) || this.pitchBackwardBinding.IsBoundToKey(key, rig);
			}

			public bool IsEmulatingTouches()
			{
				return false;
			}

			public bool IsEmulatingMousePosition()
			{
				return false;
			}

			public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
			{
				descList.Add(this.rollBinding, InputRig.InputSource.Analog, "Roll Angle (Analog)", parentMenuPath, undoObject);
				descList.Add(this.pitchBinding, InputRig.InputSource.Analog, "Pitch Angle (Analog)", parentMenuPath, undoObject);
				descList.Add(this.rollLeftBinding, "Roll Left (Digital)", parentMenuPath, undoObject);
				descList.Add(this.rollRightBinding, "Roll Right (Digital)", parentMenuPath, undoObject);
				descList.Add(this.pitchForwardBinding, "Pitch Forward (Digital)", parentMenuPath, undoObject);
				descList.Add(this.pitchBackwardBinding, "Pitch Backward (Digital)", parentMenuPath, undoObject);
			}

			[NonSerialized]
			private InputRig rig;

			public TiltState tiltState;

			private int digitalRollDirection;

			private int digitalPitchDirection;

			private bool disabledByRigSwitches;

			public DisablingConditionSet disablingConditions;

			public AnalogConfig rollAnalogConfig;

			public AnalogConfig pitchAnalogConfig;

			public AxisBinding rollBinding;

			public AxisBinding pitchBinding;

			public DigitalBinding rollLeftBinding;

			public DigitalBinding rollRightBinding;

			public DigitalBinding pitchForwardBinding;

			public DigitalBinding pitchBackwardBinding;
		}

		[Serializable]
		public class ScrollWheelState : IBindingContainer
		{
			public ScrollWheelState(InputRig rig)
			{
				this.rig = rig;
				this.vertScrollDeltaBinding = new ScrollDeltaBinding("Mouse ScrollWheel", true, null);
				this.horzScrollDeltaBinding = new ScrollDeltaBinding("Mouse ScrollWheel Secondary", true, null);
			}

			public void Reset()
			{
			}

			public void Update()
			{
				Vector2 mouseScrollDelta = Input.mouseScrollDelta;
				this.horzScrollDeltaBinding.SyncScrollDelta(Mathf.RoundToInt(mouseScrollDelta.x), this.rig);
				this.vertScrollDeltaBinding.SyncScrollDelta(Mathf.RoundToInt(mouseScrollDelta.y), this.rig);
			}

			public Vector2 GetDelta()
			{
				Vector2 mouseScrollDelta = Input.mouseScrollDelta;
				if (this.rig == null)
				{
					return mouseScrollDelta;
				}
				if (this.horzScrollDeltaBinding.deltaBinding.enabled)
				{
					mouseScrollDelta.x = this.horzScrollDeltaBinding.deltaBinding.GetAxis(this.rig);
				}
				if (this.vertScrollDeltaBinding.deltaBinding.enabled)
				{
					mouseScrollDelta.y = this.vertScrollDeltaBinding.deltaBinding.GetAxis(this.rig);
				}
				return mouseScrollDelta;
			}

			public bool IsBoundToAxis(string axisName, InputRig rig)
			{
				return this.horzScrollDeltaBinding.IsBoundToAxis(axisName, rig) || this.vertScrollDeltaBinding.IsBoundToAxis(axisName, rig);
			}

			public bool IsBoundToKey(KeyCode key, InputRig rig)
			{
				return this.horzScrollDeltaBinding.IsBoundToKey(key, rig) || this.vertScrollDeltaBinding.IsBoundToKey(key, rig);
			}

			public bool IsEmulatingTouches()
			{
				return false;
			}

			public bool IsEmulatingMousePosition()
			{
				return false;
			}

			public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
			{
				descList.Add(this.vertScrollDeltaBinding, "Vertical/Primary Scroll Wheel Delta", parentMenuPath, undoObject);
				descList.Add(this.horzScrollDeltaBinding, "Horizontal/Secondary Scroll Wheel Delta", parentMenuPath, undoObject);
			}

			private InputRig rig;

			public ScrollDeltaBinding horzScrollDeltaBinding;

			public ScrollDeltaBinding vertScrollDeltaBinding;
		}

		[Serializable]
		public class MouseConfig : IBindingContainer
		{
			public MouseConfig(InputRig rig)
			{
				this.rig = rig;
				this.horzDeltaBinding = new AxisBinding("Mouse X", false, null);
				this.vertDeltaBinding = new AxisBinding("Mouse Y", false, null);
			}

			public void Reset()
			{
			}

			public void SetPosition(Vector2 pos, int prio)
			{
				if (prio > this.frMousePosPrio)
				{
					this.frMousePos = pos;
					this.frMousePosPrio = prio;
				}
			}

			public Vector3 GetPosition()
			{
				return this.mousePos;
			}

			private bool IsMouseDeltaUsable()
			{
				return !CF2Input.IsInMobileMode() || (Input.touchSupported && Input.mousePresent && !Input.simulateMouseWithTouches && CFCursor.lockState == CursorLockMode.Locked);
			}

			private bool IsMousePositionUsable()
			{
				return !CF2Input.IsInMobileMode() || (Input.touchSupported && Input.mousePresent && !Input.simulateMouseWithTouches && CFCursor.lockState != CursorLockMode.Locked);
			}

			public void Update()
			{
				if (this.IsMouseDeltaUsable())
				{
					this.horzDeltaBinding.SyncFloat(UnityEngine.Input.GetAxisRaw("cfMouseX"), InputRig.InputSource.MouseDelta, this.rig);
					this.vertDeltaBinding.SyncFloat(UnityEngine.Input.GetAxisRaw("cfMouseY"), InputRig.InputSource.MouseDelta, this.rig);
				}
				if (this.IsMousePositionUsable())
				{
					this.SetPosition(UnityEngine.Input.mousePosition, -1);
				}
				this.mousePos = this.frMousePos;
				this.frMousePosPrio = -10;
			}

			public bool IsBoundToAxis(string axisName, InputRig rig)
			{
				return this.horzDeltaBinding.IsBoundToAxis(axisName, rig) || this.vertDeltaBinding.IsBoundToAxis(axisName, rig);
			}

			public bool IsBoundToKey(KeyCode key, InputRig rig)
			{
				return this.horzDeltaBinding.IsBoundToKey(key, rig) || this.vertDeltaBinding.IsBoundToKey(key, rig);
			}

			public bool IsEmulatingTouches()
			{
				return false;
			}

			public bool IsEmulatingMousePosition()
			{
				return false;
			}

			public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
			{
				descList.Add(this.horzDeltaBinding, InputRig.InputSource.MouseDelta, "Horz. Mouse Delta", parentMenuPath, undoObject);
				descList.Add(this.vertDeltaBinding, InputRig.InputSource.MouseDelta, "Vert. Mouse Delta", parentMenuPath, undoObject);
			}

			private InputRig rig;

			public AxisBinding horzDeltaBinding;

			public AxisBinding vertDeltaBinding;

			private Vector2 mousePos;

			private Vector2 frMousePos;

			private int frMousePosPrio;
		}
	}
}
