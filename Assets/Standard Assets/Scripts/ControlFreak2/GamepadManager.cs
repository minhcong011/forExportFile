// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.GamepadManager
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	public class GamepadManager : ComponentBase
	{
		public GamepadManager()
		{
			this.customProfileBank = new CustomGamepadProfileBank();
			this.dontDestroyOnLoad = false;
			this.connectionCheckInterval = 1f;
			this.elapsedSinceLastConnectionCheck = 10000f;
			this.fallbackDpadConfig = new JoystickConfig();
			this.fallbackDpadConfig.stickMode = JoystickConfig.StickMode.Digital8;
			this.fallbackDpadConfig.analogDeadZone = 0.3f;
			this.fallbackLeftStickConfig = new JoystickConfig();
			this.fallbackLeftStickConfig.stickMode = JoystickConfig.StickMode.Analog;
			this.fallbackLeftStickConfig.analogDeadZone = 0.3f;
			this.fallbackRightStickConfig = new JoystickConfig();
			this.fallbackRightStickConfig.stickMode = JoystickConfig.StickMode.Analog;
			this.fallbackRightStickConfig.analogDeadZone = 0.3f;
			this.fallbackLeftTriggerAnalogConfig = new AnalogConfig();
			this.fallbackLeftTriggerAnalogConfig.analogDeadZone = 0.2f;
			this.fallbackRightTriggerAnalogConfig = new AnalogConfig();
			this.fallbackRightTriggerAnalogConfig.analogDeadZone = 0.2f;
			this.gamepadsCombined = new GamepadManager.Gamepad(this);
			this.freeGamepads = new List<GamepadManager.Gamepad>(4);
			this.activeGamepads = new List<GamepadManager.Gamepad>(4);
			this.connectedGamepads = new List<GamepadManager.Gamepad>(4);
			this.activeGamepadNum = 0;
			for (int i = 0; i < 4; i++)
			{
				this.freeGamepads.Add(new GamepadManager.Gamepad(this));
			}
		}

		public static GamepadManager activeManager { get; private set; }

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GamepadManager.OnDisconnectionCallback onGamepadDisconnected;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GamepadManager.OnDisconnectionCallback onGamepadDisactivated;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GamepadManager.OnConnectionChnageCallback onGamepadConnected;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GamepadManager.OnConnectionChnageCallback onGamepadActivated;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onChange;

		public static string GetJoyAxisName(int joyId, int axisId)
		{
			if (joyId < 0 || joyId >= 4 || axisId < 0 || axisId >= 10)
			{
				return string.Empty;
			}
			return string.Concat(new object[]
			{
				"cfJ",
				joyId,
				string.Empty,
				axisId
			});
		}

		public static KeyCode GetJoyKeyCode(int joyId, int keyId)
		{
			KeyCode keyCode;
			switch (joyId)
			{
			case 0:
				keyCode = KeyCode.Joystick1Button0;
				break;
			case 1:
				keyCode = KeyCode.Joystick2Button0;
				break;
			case 2:
				keyCode = KeyCode.Joystick3Button0;
				break;
			case 3:
				keyCode = KeyCode.Joystick4Button0;
				break;
			default:
				return KeyCode.None;
			}
			if (keyId < 0 || keyId >= 20)
			{
				return KeyCode.None;
			}
			return keyCode + keyId;
		}

		protected override void OnInitComponent()
		{
			this.customProfileBank.Load();
		}

		protected override void OnDestroyComponent()
		{
		}

		protected override void OnEnableComponent()
		{
			if (GamepadManager.activeManager == null)
			{
				this.SetAsMain();
				if (this.dontDestroyOnLoad && !CFUtils.editorStopped)
				{
					UnityEngine.Object.DontDestroyOnLoad(this);
				}
			}
			else if (GamepadManager.activeManager != this && !CFUtils.editorStopped)
			{
				base.enabled = false;
			}
		}

		public bool IsMain()
		{
			return GamepadManager.activeManager == this;
		}

		public void SetAsMain()
		{
			base.enabled = true;
			if (this.IsMain())
			{
				return;
			}
			if (GamepadManager.activeManager != null)
			{
				GamepadManager.activeManager.RemoveAsMain();
			}
			GamepadManager.activeManager = this;
			CF2Input.onActiveRigChange += this.OnActiveRigChange;
			this.OnActiveRigChange();
		}

		public void RemoveAsMain()
		{
			if (GamepadManager.activeManager == this)
			{
				GamepadManager.activeManager = null;
			}
			CF2Input.onActiveRigChange -= this.OnActiveRigChange;
		}

		public CustomGamepadProfileBank GetCustomProfileBank()
		{
			return this.customProfileBank;
		}

		protected override void OnDisableComponent()
		{
			for (int i = 0; i < this.activeGamepads.Count; i++)
			{
				GamepadManager.Gamepad gamepad = this.activeGamepads[i];
				if (gamepad != null)
				{
					this.DisconnectGamepad(gamepad, GamepadManager.DisconnectionReason.ManagerDisabled);
				}
			}
			if (this.IsMain())
			{
				if (GamepadManager.onChange != null)
				{
					GamepadManager.onChange();
				}
				this.RemoveAsMain();
			}
		}

		private void OnActiveRigChange()
		{
			this.gamepadsCombined.SetRig(CF2Input.activeRig);
			for (int i = 0; i < this.freeGamepads.Count; i++)
			{
				this.freeGamepads[i].SetRig(CF2Input.activeRig);
			}
			for (int j = 0; j < this.connectedGamepads.Count; j++)
			{
				this.connectedGamepads[j].SetRig(CF2Input.activeRig);
			}
		}

		private void Update()
		{
			this.CheckJoystickConnections();
			for (int i = 0; i < this.connectedGamepads.Count; i++)
			{
				this.connectedGamepads[i].Update();
			}
			for (int j = 0; j < this.activeGamepads.Count; j++)
			{
				GamepadManager.Gamepad gamepad = this.activeGamepads[j];
				if (gamepad != null && !gamepad.IsBlocked())
				{
					this.gamepadsCombined.ApplyGamepadState(gamepad);
				}
			}
			this.gamepadsCombined.Update();
			InputRig activeRig = CF2Input.activeRig;
			if (activeRig != null)
			{
				this.ApplyToRig(activeRig);
			}
		}

		private void ApplyToRig(InputRig rig)
		{
			if (rig == null)
			{
				return;
			}
			for (int i = 0; i < this.activeGamepads.Count; i++)
			{
				GamepadManager.Gamepad gamepad = this.activeGamepads[i];
				if (gamepad != null && !gamepad.IsBlocked())
				{
					if (rig.gamepads.Length > i)
					{
						rig.gamepads[i].SyncGamepad(gamepad, rig);
					}
				}
			}
			rig.anyGamepad.SyncGamepad(this.gamepadsCombined, rig);
		}

		private void CheckJoystickConnections()
		{
			this.CheckUnactivatedGamepads();
			if ((this.elapsedSinceLastConnectionCheck += Time.unscaledDeltaTime) < this.connectionCheckInterval)
			{
				return;
			}
			this.ConnectJoysticks();
		}

		private string GetConnectedDeviceName(int internalJoyId)
		{
			if (this.deviceConnections == null || internalJoyId < 0 || internalJoyId >= this.deviceConnections.Length || this.deviceConnections[internalJoyId] == null || this.deviceConnections[internalJoyId].Length == 0)
			{
				return string.Empty;
			}
			return this.deviceConnections[internalJoyId];
		}

		private GamepadManager.Gamepad AddConnectedGamepad()
		{
			if (this.freeGamepads.Count <= 0)
			{
				return null;
			}
			GamepadManager.Gamepad gamepad = this.freeGamepads[this.freeGamepads.Count - 1];
			this.freeGamepads.RemoveAt(this.freeGamepads.Count - 1);
			this.connectedGamepads.Add(gamepad);
			return gamepad;
		}

		private GamepadManager.Gamepad FindDisconnectedGamepad(int internalJoyId, string deviceName)
		{
			GamepadManager.Gamepad gamepad = null;
			GamepadManager.Gamepad gamepad2 = null;
			for (int i = 0; i < this.connectedGamepads.Count; i++)
			{
				GamepadManager.Gamepad gamepad3 = this.connectedGamepads[i];
				if (!gamepad3.IsConnected())
				{
					if (gamepad == null)
					{
						gamepad = gamepad3;
					}
					if (gamepad3.GetInternalJoyName() == deviceName)
					{
						if (internalJoyId == gamepad3.GetInternalJoyId())
						{
							return gamepad3;
						}
						if (gamepad2 == null)
						{
							gamepad2 = gamepad3;
						}
					}
				}
			}
			return (gamepad2 == null) ? gamepad : gamepad2;
		}

		private void ActivateGamepad(GamepadManager.Gamepad g)
		{
			if (g == null)
			{
				return;
			}
			int num = -1;
			int slot = g.GetSlot();
			if (slot >= 0 && slot < this.activeGamepads.Count && this.activeGamepads[slot] == null)
			{
				this.activeGamepads[slot] = g;
				num = slot;
			}
			if (num < 0)
			{
				for (int i = 0; i < this.activeGamepads.Count; i++)
				{
					if (this.activeGamepads[i] == null)
					{
						this.activeGamepads[i] = g;
						num = i;
						break;
					}
				}
				if (num < 0)
				{
					num = this.activeGamepads.Count;
					this.activeGamepads.Add(g);
				}
			}
			if (num < 0)
			{
				return;
			}
			g.OnActivate(num);
			this.CountActiveGamepads();
			if (this.IsMain() && GamepadManager.onGamepadActivated != null)
			{
				GamepadManager.onGamepadActivated(g);
			}
		}

		private void DisactivateGamepad(GamepadManager.Gamepad g, GamepadManager.DisconnectionReason reason)
		{
			if (g == null || !g.IsActivated())
			{
				return;
			}
			if (this.IsMain() && GamepadManager.onGamepadDisactivated != null)
			{
				GamepadManager.onGamepadDisactivated(g, reason);
			}
			g.OnDisactivate();
			int num = this.activeGamepads.IndexOf(g);
			if (num < 0)
			{
				return;
			}
			this.activeGamepads[num] = null;
			this.CountActiveGamepads();
		}

		private void CountActiveGamepads()
		{
			this.activeGamepadNum = 0;
			for (int i = 0; i < this.activeGamepads.Count; i++)
			{
				if (this.activeGamepads[i] != null)
				{
					this.activeGamepadNum++;
				}
			}
		}

		private void DisconnectGamepad(GamepadManager.Gamepad g, GamepadManager.DisconnectionReason reason)
		{
			if (g.IsActivated())
			{
				this.DisactivateGamepad(g, reason);
			}
			if (this.IsMain() && GamepadManager.onGamepadDisconnected != null)
			{
				GamepadManager.onGamepadDisconnected(g, reason);
			}
			g.OnDisconnect();
		}

		private void ConnectJoysticks()
		{
			this.elapsedSinceLastConnectionCheck = 0f;
			this.deviceConnections = Input.GetJoystickNames();
			bool flag = false;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.connectedGamepads.Count; i++)
			{
				GamepadManager.Gamepad gamepad = this.connectedGamepads[i];
				if (!gamepad.IsConnected())
				{
					num2 |= 1 << i;
				}
				else
				{
					string connectedDeviceName = this.GetConnectedDeviceName(gamepad.GetInternalJoyId());
					if (connectedDeviceName == gamepad.GetInternalJoyName())
					{
						num |= 1 << gamepad.GetInternalJoyId();
					}
					else
					{
						flag = true;
						this.DisconnectGamepad(gamepad, GamepadManager.DisconnectionReason.Disconnection);
					}
				}
			}
			for (int j = 0; j < 4; j++)
			{
				string connectedDeviceName2 = this.GetConnectedDeviceName(j);
				if (!string.IsNullOrEmpty(connectedDeviceName2) && (num & 1 << j) == 0)
				{
					GamepadManager.Gamepad gamepad2 = this.FindDisconnectedGamepad(j, connectedDeviceName2);
					if (gamepad2 == null)
					{
						gamepad2 = this.AddConnectedGamepad();
					}
					if (gamepad2 != null)
					{
						flag = true;
						gamepad2.ConnectToJoy(j, connectedDeviceName2);
						GamepadProfile profileForDevice = this.GetProfileForDevice(connectedDeviceName2);
						if (profileForDevice != null)
						{
							gamepad2.SetProfile(profileForDevice);
						}
						if (this.IsMain() && GamepadManager.onGamepadConnected != null)
						{
							GamepadManager.onGamepadConnected(gamepad2);
						}
					}
				}
			}
			if (flag && this.IsMain() && GamepadManager.onChange != null)
			{
				GamepadManager.onChange();
			}
		}

		private void CheckUnactivatedGamepads()
		{
			bool flag = false;
			for (int i = 0; i < this.connectedGamepads.Count; i++)
			{
				GamepadManager.Gamepad gamepad = this.connectedGamepads[i];
				if (gamepad.IsConnected() && gamepad.IsSupported() && !gamepad.IsBlocked() && !gamepad.IsActivated() && gamepad.AnyInternalKeyOrAxisPressed())
				{
					flag = true;
					this.ActivateGamepad(gamepad);
				}
			}
			if (this.IsMain() && flag && GamepadManager.onChange != null)
			{
				GamepadManager.onChange();
			}
		}

		public int GetGamepadSlotCount()
		{
			return this.activeGamepads.Count;
		}

		public int GetActiveGamepadCount()
		{
			return this.activeGamepadNum;
		}

		public GamepadManager.Gamepad GetGamepadAtSlot(int slot)
		{
			return (slot >= 0 && slot < this.activeGamepads.Count) ? this.activeGamepads[slot] : null;
		}

		public GamepadManager.Gamepad GetCombinedGamepad()
		{
			return this.gamepadsCombined;
		}

		public int GetConnectedGamepadCount()
		{
			return this.connectedGamepads.Count;
		}

		public GamepadManager.Gamepad GetConnectedGamepad(int index)
		{
			return (index >= 0 && index < this.connectedGamepads.Count) ? this.connectedGamepads[index] : null;
		}

		public void DisactivateGamepads()
		{
			for (int i = 0; i < this.activeGamepads.Count; i++)
			{
				GamepadManager.Gamepad gamepad = this.activeGamepads[i];
				if (gamepad != null)
				{
					this.DisactivateGamepad(gamepad, GamepadManager.DisconnectionReason.MassDisactivation);
				}
			}
			if (this.IsMain() && GamepadManager.onChange != null)
			{
				GamepadManager.onChange();
			}
		}

		private GamepadProfile GetProfileForDevice(string joyDevice)
		{
			GamepadProfile gamepadProfile = null;
			if (this.customProfileBank != null)
			{
				gamepadProfile = this.customProfileBank.GetProfile(joyDevice);
			}
			if (gamepadProfile == null)
			{
				gamepadProfile = BuiltInGamepadProfileBank.GetProfile(joyDevice);
			}
			if (gamepadProfile == null)
			{
				gamepadProfile = BuiltInGamepadProfileBank.GetGenericProfile();
			}
			return gamepadProfile;
		}

		public const int MAX_JOYSTICKS = 4;

		public const int MAX_INTERNAL_AXES = 10;

		public const int MAX_INTERNAL_KEYS = 20;

		public const int GamepadStickCount = 3;

		public const int GamepadKeyCount = 12;

		private CustomGamepadProfileBank customProfileBank;

		public bool dontDestroyOnLoad;

		public float connectionCheckInterval;

		private float elapsedSinceLastConnectionCheck;

		[NonSerialized]
		private JoystickConfig fallbackDpadConfig;

		[NonSerialized]
		private JoystickConfig fallbackLeftStickConfig;

		[NonSerialized]
		private JoystickConfig fallbackRightStickConfig;

		[NonSerialized]
		private AnalogConfig fallbackLeftTriggerAnalogConfig;

		[NonSerialized]
		private AnalogConfig fallbackRightTriggerAnalogConfig;

		private List<GamepadManager.Gamepad> activeGamepads;

		private List<GamepadManager.Gamepad> freeGamepads;

		private List<GamepadManager.Gamepad> connectedGamepads;

		private GamepadManager.Gamepad gamepadsCombined;

		private int activeGamepadNum;

		private string[] deviceConnections;

		public delegate void OnConnectionChnageCallback(GamepadManager.Gamepad gamepad);

		public delegate void OnDisconnectionCallback(GamepadManager.Gamepad gamepad, GamepadManager.DisconnectionReason reason);

		public enum GamepadStick
		{
			LeftAnalog,
			RightAnalog,
			Dpad
		}

		public enum GamepadKey
		{
			FaceBottom,
			FaceRight,
			FaceTop,
			FaceLeft,
			Start,
			Select,
			L1,
			R1,
			L2,
			R2,
			L3,
			R3
		}

		public enum DisconnectionReason
		{
			MassDisactivation,
			Disactivation,
			Disconnection,
			ManagerDisabled
		}

		public class Gamepad
		{
			public Gamepad(GamepadManager manager)
			{
				this.manager = manager;
				this.internalAxes = new GamepadManager.Gamepad.InternalAxisState[10];
				this.internalKeys = new GamepadManager.Gamepad.InternalKeyState[20];
				for (int i = 0; i < this.internalAxes.Length; i++)
				{
					this.internalAxes[i] = new GamepadManager.Gamepad.InternalAxisState();
				}
				for (int j = 0; j < this.internalKeys.Length; j++)
				{
					this.internalKeys[j] = new GamepadManager.Gamepad.InternalKeyState();
				}
				this.leftStick = new GamepadManager.Gamepad.JoyState(this, manager.fallbackLeftStickConfig);
				this.rightStick = new GamepadManager.Gamepad.JoyState(this, manager.fallbackRightStickConfig);
				this.dpad = new GamepadManager.Gamepad.JoyState(this, manager.fallbackDpadConfig);
				this.keys = new GamepadManager.Gamepad.KeyState[12];
				for (int k = 0; k < this.keys.Length; k++)
				{
					if (k == 6 || k == 8)
					{
						this.keys[k] = new GamepadManager.Gamepad.KeyState(this, true, manager.fallbackLeftTriggerAnalogConfig);
					}
					else if (k == 7 || k == 9)
					{
						this.keys[k] = new GamepadManager.Gamepad.KeyState(this, true, manager.fallbackRightTriggerAnalogConfig);
					}
					else
					{
						this.keys[k] = new GamepadManager.Gamepad.KeyState(this, false, null);
					}
				}
				this.OnDisconnect();
			}

			public bool IsConnected()
			{
				return this.isConnected;
			}

			public bool IsActivated()
			{
				return this.isActivated;
			}

			public bool IsSupported()
			{
				return this.profile != null;
			}

			public int GetSlot()
			{
				return this.slot;
			}

			public string GetInternalJoyName()
			{
				return this.internalJoyName;
			}

			public int GetInternalJoyId()
			{
				return this.internalJoyId;
			}

			public string GetProfileName()
			{
				return (this.profile == null) ? string.Empty : this.profile.name;
			}

			public void Block(bool block)
			{
				this.isBlocked = block;
			}

			public bool IsBlocked()
			{
				return this.isBlocked;
			}

			public void SetRig(InputRig rig)
			{
				this.leftStick.SetConfig((!(rig != null)) ? this.manager.fallbackLeftStickConfig : rig.leftStickConfig);
				this.rightStick.SetConfig((!(rig != null)) ? this.manager.fallbackRightStickConfig : rig.rightStickConfig);
				this.dpad.SetConfig((!(rig != null)) ? this.manager.fallbackDpadConfig : rig.dpadConfig);
				this.keys[6].SetConfig((!(rig != null)) ? this.manager.fallbackLeftTriggerAnalogConfig : rig.leftTriggerAnalogConfig);
				this.keys[8].SetConfig((!(rig != null)) ? this.manager.fallbackLeftTriggerAnalogConfig : rig.leftTriggerAnalogConfig);
				this.keys[7].SetConfig((!(rig != null)) ? this.manager.fallbackRightTriggerAnalogConfig : rig.rightTriggerAnalogConfig);
				this.keys[9].SetConfig((!(rig != null)) ? this.manager.fallbackRightTriggerAnalogConfig : rig.rightTriggerAnalogConfig);
			}

			public void ConnectToJoy(int internalJoyId, string joyName)
			{
				this.profile = null;
				this.isActivated = false;
				this.profile = null;
				this.isBlocked = false;
				if (internalJoyId >= 0)
				{
					this.internalJoyId = internalJoyId;
					this.internalJoyName = joyName;
					this.isConnected = true;
				}
				else
				{
					this.isConnected = false;
				}
				for (int i = 0; i < this.internalAxes.Length; i++)
				{
					this.internalAxes[i].ConnectToHardwareAxis(internalJoyId, i);
				}
				for (int j = 0; j < this.internalKeys.Length; j++)
				{
					this.internalKeys[j].ConnectToHardwareKey(internalJoyId, j);
				}
				this.Reset();
			}

			public void OnDisconnect()
			{
				this.ConnectToJoy(-1, string.Empty);
			}

			public void OnActivate(int slot)
			{
				this.slot = slot;
				this.isActivated = true;
				this.Reset();
			}

			public void OnDisactivate()
			{
				this.isActivated = false;
				this.Reset();
			}

			public GamepadProfile GetProfile()
			{
				return this.profile;
			}

			public void SetProfile(GamepadProfile profile)
			{
				this.profile = profile;
				this.leftStick.ConnectToJoy((profile == null) ? null : profile.leftStick);
				this.rightStick.ConnectToJoy((profile == null) ? null : profile.rightStick);
				this.dpad.ConnectToJoy((profile == null) ? null : profile.dpad);
				for (int i = 0; i < this.keys.Length; i++)
				{
					this.keys[i].ConnectToJoy((profile == null) ? null : profile.GetKeySource(i));
				}
				this.Reset();
			}

			public void Reset()
			{
				this.leftStick.Reset();
				this.rightStick.Reset();
				this.dpad.Reset();
				for (int i = 0; i < this.keys.Length; i++)
				{
					this.keys[i].Reset();
				}
				for (int j = 0; j < this.internalAxes.Length; j++)
				{
					this.internalAxes[j].Reset();
				}
				for (int k = 0; k < this.internalKeys.Length; k++)
				{
					this.internalKeys[k].Reset();
				}
			}

			public bool GetKey(GamepadManager.GamepadKey key)
			{
				return this.keys[(int)key].GetDigital();
			}

			public bool GetKeyDown(GamepadManager.GamepadKey key)
			{
				return this.keys[(int)key].GetDigitalDown();
			}

			public bool GetKeyUp(GamepadManager.GamepadKey key)
			{
				return this.keys[(int)key].GetDigitalUp();
			}

			public float GetKeyAnalog(GamepadManager.GamepadKey key)
			{
				return this.keys[(int)key].GetAnalog();
			}

			public Vector2 GetStickVec(GamepadManager.GamepadStick s)
			{
				return this.GetStick(s).GetVector();
			}

			public Dir GetStickDir8(GamepadManager.GamepadStick s)
			{
				return this.GetStick(s).GetDir8();
			}

			public Dir GetStickDir4(GamepadManager.GamepadStick s)
			{
				return this.GetStick(s).GetDir4();
			}

			public Dir GetStickDir(GamepadManager.GamepadStick s)
			{
				return this.GetStick(s).GetDir();
			}

			public JoystickState GetStick(GamepadManager.GamepadStick s)
			{
				if (s == GamepadManager.GamepadStick.LeftAnalog)
				{
					return this.leftStick.state;
				}
				if (s != GamepadManager.GamepadStick.RightAnalog)
				{
					return this.dpad.state;
				}
				return this.rightStick.state;
			}

			public bool GetInternalKeyState(int keyId)
			{
				return keyId >= 0 && keyId < this.internalKeys.Length && this.internalKeys[keyId].GetState();
			}

			public float GetInternalAxisAnalog(int axisId)
			{
				return (axisId < 0 || axisId >= this.internalAxes.Length) ? 0f : this.internalAxes[axisId].GetVal();
			}

			public int GetInternalAxisDigital(int axisId)
			{
				return (axisId < 0 || axisId >= this.internalAxes.Length) ? 0 : this.internalAxes[axisId].GetDigital();
			}

			public bool IsInternalAxisFullyAnalog(int axisId)
			{
				return axisId >= 0 && axisId < this.internalAxes.Length && this.internalAxes[axisId].IsFullyAnalog();
			}

			public bool AnyInternalKeyOrAxisPressed()
			{
				return this.AnyInternalKeyPressed() || this.AnyInternalAxisPressed();
			}

			public bool AnyInternalKeyPressed()
			{
				for (int i = 0; i < this.internalKeys.Length; i++)
				{
					if (this.internalKeys[i].GetState())
					{
						return true;
					}
				}
				return false;
			}

			public bool AnyInternalAxisPressed()
			{
				for (int i = 0; i < this.internalAxes.Length; i++)
				{
					if (this.internalAxes[i].GetDigital() != 0)
					{
						return true;
					}
				}
				return false;
			}

			public int GetPressedInternalKey(int start = 0)
			{
				for (int i = start; i < this.internalKeys.Length; i++)
				{
					if (this.internalKeys[i].GetState())
					{
						return i;
					}
				}
				return -1;
			}

			public int GetPressedInternalAxis(out bool positiveSide, int start = 0)
			{
				for (int i = start; i < this.internalAxes.Length; i++)
				{
					int digital = this.internalAxes[i].GetDigital();
					if (digital != 0)
					{
						positiveSide = (digital > 0);
						return i;
					}
				}
				positiveSide = false;
				return -1;
			}

			public void Update()
			{
				if (this.IsConnected())
				{
					for (int i = 0; i < this.internalKeys.Length; i++)
					{
						this.internalKeys[i].Update();
					}
					for (int j = 0; j < this.internalAxes.Length; j++)
					{
						this.internalAxes[j].Update();
					}
				}
				this.leftStick.Update();
				this.rightStick.Update();
				this.dpad.Update();
				for (int k = 0; k < this.keys.Length; k++)
				{
					this.keys[k].Update();
				}
			}

			public void ApplyGamepadState(GamepadManager.Gamepad g)
			{
				if (g == null)
				{
					return;
				}
				this.leftStick.state.ApplyState(g.leftStick.state);
				this.rightStick.state.ApplyState(g.rightStick.state);
				this.dpad.state.ApplyState(g.dpad.state);
				for (int i = 0; i < this.keys.Length; i++)
				{
					this.keys[i].SetDigital(g.keys[i].GetDigitalRaw());
					this.keys[i].SetAnalog(g.keys[i].GetAnalogRaw());
				}
			}

			private GamepadManager manager;

			private GamepadProfile profile;

			private bool isActivated;

			private bool isConnected;

			private bool isBlocked;

			private string internalJoyName;

			private int internalJoyId;

			private int slot;

			public GamepadManager.Gamepad.JoyState leftStick;

			public GamepadManager.Gamepad.JoyState rightStick;

			public GamepadManager.Gamepad.JoyState dpad;

			public GamepadManager.Gamepad.KeyState[] keys;

			private GamepadManager.Gamepad.InternalAxisState[] internalAxes;

			private GamepadManager.Gamepad.InternalKeyState[] internalKeys;

			public class JoyState
			{
				public JoyState(GamepadManager.Gamepad gamepad, JoystickConfig config)
				{
					this.gamepad = gamepad;
					this.state = new JoystickState(config);
					this.isConnectedToHardware = false;
				}

				public void SetConfig(JoystickConfig config)
				{
					this.state.SetConfig(config);
				}

				public void Reset()
				{
					this.state.Reset();
				}

				public void Update()
				{
					this.ReadHardwareState();
					this.state.Update();
				}

				private float GetCompositeAnalogVal(int posAxisId, bool posFlip, int negAxisId, bool negFlip)
				{
					if (posAxisId >= 0 && posAxisId == negAxisId)
					{
						float internalAxisAnalog = this.gamepad.GetInternalAxisAnalog(posAxisId);
						return (!posFlip) ? internalAxisAnalog : (-internalAxisAnalog);
					}
					float num = this.gamepad.GetInternalAxisAnalog(posAxisId);
					if (posFlip)
					{
						num = -num;
					}
					float num2 = this.gamepad.GetInternalAxisAnalog(negAxisId);
					if (negFlip)
					{
						num2 = -num2;
					}
					return Mathf.Clamp(num, 0f, 1f) + Mathf.Clamp(num2, -1f, 0f);
				}

				private void ReadHardwareState()
				{
					if (!this.isConnectedToHardware)
					{
						return;
					}
					Vector2 zero = Vector2.zero;
					zero.x = this.GetCompositeAnalogVal(this.srcAxisIdR, this.srcAxisFlipR, this.srcAxisIdL, this.srcAxisFlipL);
					zero.y = this.GetCompositeAnalogVal(this.srcAxisIdU, this.srcAxisFlipU, this.srcAxisIdD, this.srcAxisFlipD);
					bool digiU = this.srcKeyU >= 0 && this.gamepad.GetInternalKeyState(this.srcKeyU);
					bool digiD = this.srcKeyD >= 0 && this.gamepad.GetInternalKeyState(this.srcKeyD);
					bool digiR = this.srcKeyR >= 0 && this.gamepad.GetInternalKeyState(this.srcKeyR);
					bool digiL = this.srcKeyL >= 0 && this.gamepad.GetInternalKeyState(this.srcKeyL);
					this.state.ApplyClampedVec(zero, JoystickConfig.ClampMode.Square);
					this.state.ApplyDigital(digiU, digiR, digiD, digiL);
				}

				public void ConnectToJoy(GamepadProfile.JoystickSource joySrc)
				{
					this.isConnectedToHardware = (joySrc != null);
					if (joySrc != null)
					{
						this.srcAxisIdL = joySrc.keyL.axisId;
						this.srcAxisFlipL = !joySrc.keyL.axisSign;
						this.srcAxisIdR = joySrc.keyR.axisId;
						this.srcAxisFlipR = !joySrc.keyR.axisSign;
						this.srcAxisIdU = joySrc.keyU.axisId;
						this.srcAxisFlipU = !joySrc.keyU.axisSign;
						this.srcAxisIdD = joySrc.keyD.axisId;
						this.srcAxisFlipD = !joySrc.keyD.axisSign;
						this.srcKeyL = joySrc.keyL.keyId;
						this.srcKeyR = joySrc.keyR.keyId;
						this.srcKeyU = joySrc.keyU.keyId;
						this.srcKeyD = joySrc.keyD.keyId;
					}
					this.Reset();
				}

				public void SyncJoyState(JoystickStateBinding bind, InputRig rig)
				{
					bind.SyncJoyState(this.state, rig);
				}

				private GamepadManager.Gamepad gamepad;

				public JoystickState state;

				private bool isConnectedToHardware;

				private int srcAxisIdL;

				private int srcAxisIdR;

				private int srcAxisIdU;

				private int srcAxisIdD;

				private bool srcAxisFlipL;

				private bool srcAxisFlipR;

				private bool srcAxisFlipU;

				private bool srcAxisFlipD;

				private int srcKeyU;

				private int srcKeyR;

				private int srcKeyD;

				private int srcKeyL;
			}

			public class KeyState
			{
				public KeyState(GamepadManager.Gamepad gamepad, bool isTrigger, AnalogConfig analogConfig = null)
				{
					this.gamepad = gamepad;
					this.isTrigger = isTrigger;
					this.analogConfig = analogConfig;
					this.isConnectedToHardware = false;
					this.Reset();
				}

				public void SetConfig(AnalogConfig config)
				{
					this.analogConfig = config;
				}

				public void SetDigital(bool state)
				{
					if (state)
					{
						this.frDigital = true;
					}
				}

				public void SetAnalog(float val)
				{
					if (val > this.frAnalog)
					{
						this.frAnalog = Mathf.Clamp01(val);
					}
				}

				public bool GetDigitalRaw()
				{
					return this.digiCurRaw;
				}

				public bool GetDigital()
				{
					return this.digiCur;
				}

				public bool GetDigitalDown()
				{
					return this.digiCur && !this.digiPrev;
				}

				public bool GetDigitalUp()
				{
					return !this.digiCur && this.digiPrev;
				}

				public float GetAnalog()
				{
					return this.analogCur;
				}

				public float GetAnalogRaw()
				{
					return this.analogRawCur;
				}

				public bool IsTrigger()
				{
					return this.isTrigger;
				}

				public void Reset()
				{
					this.analogCur = 0f;
					this.analogRawCur = 0f;
					this.digiCur = false;
					this.digiPrev = false;
					this.digiCurRaw = false;
					this.frAnalog = 0f;
					this.frDigital = false;
				}

				public void Update()
				{
					this.ReadHardwareState();
					this.digiPrev = this.digiCur;
					this.digiCurRaw = this.frDigital;
					this.analogRawCur = this.frAnalog;
					this.frAnalog = 0f;
					this.frDigital = false;
					if (this.analogConfig != null)
					{
						this.analogCur = this.analogConfig.GetAnalogVal((!this.digiCurRaw) ? this.analogRawCur : 1f);
						this.digiCur = (this.digiCurRaw || this.analogConfig.GetDigitalVal(this.analogRawCur, this.digiPrev));
					}
					else
					{
						this.analogCur = ((!this.digiCurRaw) ? this.analogRawCur : 1f);
						this.digiCur = (this.digiCurRaw || this.analogRawCur > 0.5f);
					}
				}

				private void ReadHardwareState()
				{
					if (!this.isConnectedToHardware)
					{
						return;
					}
					bool digital = this.srcKeyId >= 0 && this.gamepad.GetInternalKeyState(this.srcKeyId);
					float num = 0f;
					if (this.srcAxisId >= 0)
					{
						num = this.gamepad.GetInternalAxisAnalog(this.srcAxisId);
						num = Mathf.Clamp01((!this.srcAxisSign) ? (-num) : num);
					}
					this.SetAnalog(num);
					this.SetDigital(digital);
				}

				public void ConnectToJoy(GamepadProfile.KeySource src)
				{
					this.isConnectedToHardware = (src != null);
					this.srcAxisId = -1;
					this.srcKeyId = -1;
					if (src != null)
					{
						this.srcKeyId = src.keyId;
						this.srcAxisId = src.axisId;
						this.srcAxisSign = src.axisSign;
					}
					this.Reset();
				}

				public void SyncDigital(DigitalBinding bind, InputRig rig)
				{
					if (this.GetDigital())
					{
						bind.Sync(true, rig, false);
					}
				}

				public void SyncAnalog(AxisBinding bind, InputRig rig)
				{
					bind.SyncFloat(this.GetAnalog(), InputRig.InputSource.Analog, rig);
					bind.SyncFloat((float)((!this.GetDigitalRaw()) ? 0 : 1), InputRig.InputSource.Digital, rig);
				}

				private GamepadManager.Gamepad gamepad;

				private bool isTrigger;

				private AnalogConfig analogConfig;

				public bool isConnectedToHardware;

				private float frAnalog;

				private bool frDigital;

				private float analogCur;

				private float analogRawCur;

				private bool digiCurRaw;

				private bool digiCur;

				private bool digiPrev;

				private int srcAxisId;

				private int srcKeyId;

				private bool srcAxisSign;
			}

			private class InternalKeyState
			{
				public InternalKeyState()
				{
					this.isReadyToUse = false;
					this.stateCur = false;
					this.stateCur = false;
					this.key = KeyCode.None;
				}

				public bool GetState()
				{
					return this.isReadyToUse && this.stateCur;
				}

				public void Reset()
				{
					this.stateCur = (this.statePrev = false);
					this.isReadyToUse = false;
				}

				public void Update()
				{
					this.statePrev = this.stateCur;
					this.ReadHardwareState();
					if (!this.isReadyToUse && !this.stateCur)
					{
						this.isReadyToUse = true;
					}
				}

				private void ReadHardwareState()
				{
					this.stateCur = (this.key != KeyCode.None && UnityEngine.Input.GetKey(this.key));
				}

				public void ConnectToHardwareKey(int joyId, int keyId)
				{
					this.key = GamepadManager.GetJoyKeyCode(joyId, keyId);
					this.Reset();
				}

				private KeyCode key;

				public bool stateCur;

				public bool statePrev;

				public bool isReadyToUse;
			}

			private class InternalAxisState
			{
				public InternalAxisState()
				{
					this.isReadyToUse = false;
					this.valCur = (this.valPrev = 0f);
					this.axisName = string.Empty;
					this.trueAnalogRange = false;
				}

				public float GetVal()
				{
					return (!this.isReadyToUse) ? 0f : this.valCur;
				}

				public int GetDigital()
				{
					return (!this.isReadyToUse || Mathf.Abs(this.valCur) <= 0.5f) ? 0 : ((this.valCur <= 0f) ? -1 : 1);
				}

				public void Reset()
				{
					this.valCur = (this.valPrev = 0f);
					this.isReadyToUse = false;
					this.trueAnalogRange = false;
				}

				public void Update()
				{
					this.valPrev = this.valCur;
					this.ReadHardwareState();
					if (!this.isReadyToUse && Mathf.Abs(this.valCur) < 0.1f)
					{
						this.isReadyToUse = true;
					}
				}

				public void SetVal(float v)
				{
					this.valCur = v;
					if (!this.trueAnalogRange && Mathf.Abs(v) > 0.01f && Mathf.Abs(v) < 0.99f)
					{
						this.trueAnalogRange = true;
					}
				}

				private void ReadHardwareState()
				{
					this.SetVal((!string.IsNullOrEmpty(this.axisName)) ? UnityEngine.Input.GetAxisRaw(this.axisName) : 0f);
				}

				public void ConnectToHardwareAxis(int joyId, int axisId)
				{
					this.axisName = GamepadManager.GetJoyAxisName(joyId, axisId);
					this.Reset();
				}

				public bool IsFullyAnalog()
				{
					return this.trueAnalogRange;
				}

				private string axisName;

				public float valCur;

				public float valPrev;

				public bool isReadyToUse;

				public bool trueAnalogRange;
			}
		}
	}
}
