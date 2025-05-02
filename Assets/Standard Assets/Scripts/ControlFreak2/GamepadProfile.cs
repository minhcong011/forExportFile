// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.GamepadProfile
using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ControlFreak2
{
	[Serializable]
	public class GamepadProfile
	{
		public GamepadProfile()
		{
			this.name = "New Profile";
			this.joystickIdentifier = "Device Identifier";
			this.profileMode = GamepadProfile.ProfileMode.Normal;
			this.unityVerFrom = "4.7";
			this.unityVerTo = "9.9";
			this.dpad = new GamepadProfile.JoystickSource();
			this.leftStick = new GamepadProfile.JoystickSource();
			this.rightStick = new GamepadProfile.JoystickSource();
			this.keyFaceU = GamepadProfile.KeySource.Empty();
			this.keyFaceR = GamepadProfile.KeySource.Empty();
			this.keyFaceD = GamepadProfile.KeySource.Empty();
			this.keyFaceL = GamepadProfile.KeySource.Empty();
			this.keyStart = GamepadProfile.KeySource.Empty();
			this.keySelect = GamepadProfile.KeySource.Empty();
			this.keyL1 = GamepadProfile.KeySource.Empty();
			this.keyR1 = GamepadProfile.KeySource.Empty();
			this.keyL2 = GamepadProfile.KeySource.Empty();
			this.keyR2 = GamepadProfile.KeySource.Empty();
			this.keyL3 = GamepadProfile.KeySource.Empty();
			this.keyR3 = GamepadProfile.KeySource.Empty();
		}

		public GamepadProfile(string name, string deviceIdentifier, GamepadProfile.ProfileMode profileMode, string unityVerFrom, string unityVerTo, GamepadProfile.JoystickSource leftStick, GamepadProfile.JoystickSource rightStick, GamepadProfile.JoystickSource dpad, GamepadProfile.KeySource keyFaceD, GamepadProfile.KeySource keyFaceR, GamepadProfile.KeySource keyFaceL, GamepadProfile.KeySource keyFaceU, GamepadProfile.KeySource keySelect, GamepadProfile.KeySource keyStart, GamepadProfile.KeySource keyL1, GamepadProfile.KeySource keyR1, GamepadProfile.KeySource keyL2, GamepadProfile.KeySource keyR2, GamepadProfile.KeySource keyL3, GamepadProfile.KeySource keyR3)
		{
			this.name = name;
			this.joystickIdentifier = deviceIdentifier;
			this.profileMode = profileMode;
			this.unityVerFrom = ((!string.IsNullOrEmpty(unityVerFrom)) ? unityVerFrom : "4.3");
			this.unityVerTo = ((!string.IsNullOrEmpty(unityVerTo)) ? unityVerTo : "9.9");
			this.leftStick = ((leftStick == null) ? GamepadProfile.JoystickSource.Empty() : leftStick);
			this.rightStick = ((rightStick == null) ? GamepadProfile.JoystickSource.Empty() : rightStick);
			this.dpad = ((dpad == null) ? GamepadProfile.JoystickSource.Empty() : dpad);
			this.keyFaceU = ((keyFaceU == null) ? GamepadProfile.KeySource.Empty() : keyFaceU);
			this.keyFaceR = ((keyFaceR == null) ? GamepadProfile.KeySource.Empty() : keyFaceR);
			this.keyFaceD = ((keyFaceD == null) ? GamepadProfile.KeySource.Empty() : keyFaceD);
			this.keyFaceL = ((keyFaceL == null) ? GamepadProfile.KeySource.Empty() : keyFaceL);
			this.keyStart = ((keyStart == null) ? GamepadProfile.KeySource.Empty() : keyStart);
			this.keySelect = ((keySelect == null) ? GamepadProfile.KeySource.Empty() : keySelect);
			this.keyL1 = ((keyL1 == null) ? GamepadProfile.KeySource.Empty() : keyL1);
			this.keyR1 = ((keyR1 == null) ? GamepadProfile.KeySource.Empty() : keyR1);
			this.keyL2 = ((keyL2 == null) ? GamepadProfile.KeySource.Empty() : keyL2);
			this.keyR2 = ((keyR2 == null) ? GamepadProfile.KeySource.Empty() : keyR2);
			this.keyL3 = ((keyL3 == null) ? GamepadProfile.KeySource.Empty() : keyL3);
			this.keyR3 = ((keyR3 == null) ? GamepadProfile.KeySource.Empty() : keyR3);
		}

		public bool IsCompatible(string deviceName)
		{
			string unityVersion = Application.unityVersion;
			if (this.unityVerFrom.CompareTo(unityVersion) >= 0 || this.unityVerTo.CompareTo(unityVersion) <= 0)
			{
				return false;
			}
			if (this.profileMode == GamepadProfile.ProfileMode.Normal)
			{
				if (deviceName.IndexOf(this.joystickIdentifier, StringComparison.OrdinalIgnoreCase) < 0)
				{
					return false;
				}
			}
			else if (this.profileMode == GamepadProfile.ProfileMode.Regex && !Regex.IsMatch(deviceName, this.joystickIdentifier, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
			{
				return false;
			}
			return true;
		}

		public void AddSupportedVersion(string unityVer)
		{
			if (string.IsNullOrEmpty(unityVer))
			{
				return;
			}
			if (this.unityVerTo.CompareTo(unityVer) < 0)
			{
				this.unityVerTo = unityVer;
			}
		}

		public GamepadProfile.JoystickSource GetJoystickSource(int id)
		{
			GamepadProfile.JoystickSource result = null;
			if (id != 0)
			{
				if (id != 1)
				{
					if (id == 2)
					{
						result = this.dpad;
					}
				}
				else
				{
					result = this.rightStick;
				}
			}
			else
			{
				result = this.leftStick;
			}
			return result;
		}

		public GamepadProfile.KeySource GetKeySource(int id)
		{
			GamepadProfile.KeySource result = null;
			switch (id)
			{
			case 0:
				result = this.keyFaceD;
				break;
			case 1:
				result = this.keyFaceR;
				break;
			case 2:
				result = this.keyFaceU;
				break;
			case 3:
				result = this.keyFaceL;
				break;
			case 4:
				result = this.keyStart;
				break;
			case 5:
				result = this.keySelect;
				break;
			case 6:
				result = this.keyL1;
				break;
			case 7:
				result = this.keyR1;
				break;
			case 8:
				result = this.keyL2;
				break;
			case 9:
				result = this.keyR2;
				break;
			case 10:
				result = this.keyL3;
				break;
			case 11:
				result = this.keyR3;
				break;
			}
			return result;
		}

		public bool IsDuplicateOf(GamepadProfile profile)
		{
			if (!this.joystickIdentifier.Equals(profile.joystickIdentifier, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			for (int i = 0; i < 12; i++)
			{
				if (!this.GetKeySource(i).IsDuplicateOf(profile.GetKeySource(i)))
				{
					return false;
				}
			}
			for (int j = 0; j < 3; j++)
			{
				if (!this.GetJoystickSource(j).IsDuplicateOf(profile.GetJoystickSource(j)))
				{
					return false;
				}
			}
			return true;
		}

		public const int CAP_DPAD = 1;

		public const int CAP_LEFT_STICK = 2;

		public const int CAP_RIGHT_STICK = 4;

		public const int CAP_START = 8;

		public const int CAP_SELECT = 16;

		public const int CAP_SHOULDER_KEYS = 32;

		public const int CAP_ANALOG_TRIGGERS = 64;

		public const int CAP_PRESSABLE_STICKS = 128;

		public string name;

		public string joystickIdentifier;

		public GamepadProfile.ProfileMode profileMode;

		public string unityVerFrom;

		public string unityVerTo;

		public GamepadProfile.JoystickSource leftStick;

		public GamepadProfile.JoystickSource rightStick;

		public GamepadProfile.JoystickSource dpad;

		public GamepadProfile.KeySource keyFaceU;

		public GamepadProfile.KeySource keyFaceR;

		public GamepadProfile.KeySource keyFaceD;

		public GamepadProfile.KeySource keyFaceL;

		public GamepadProfile.KeySource keyStart;

		public GamepadProfile.KeySource keySelect;

		public GamepadProfile.KeySource keyL1;

		public GamepadProfile.KeySource keyR1;

		public GamepadProfile.KeySource keyL2;

		public GamepadProfile.KeySource keyR2;

		public GamepadProfile.KeySource keyL3;

		public GamepadProfile.KeySource keyR3;

		public enum DeviceType
		{
			Unknown,
			PS3,
			PS4,
			Xbox360,
			XboxOne,
			MOGA,
			OUYA
		}

		public enum ProfileMode
		{
			Normal,
			Regex
		}

		[Serializable]
		public class JoystickSource
		{
			public JoystickSource()
			{
				this.keyD = new GamepadProfile.KeySource();
				this.keyU = new GamepadProfile.KeySource();
				this.keyL = new GamepadProfile.KeySource();
				this.keyR = new GamepadProfile.KeySource();
			}

			public static GamepadProfile.JoystickSource Dpad(int keyU, int keyR, int keyD, int keyL)
			{
				GamepadProfile.JoystickSource joystickSource = new GamepadProfile.JoystickSource();
				joystickSource.keyD.SetKey(keyD);
				joystickSource.keyU.SetKey(keyU);
				joystickSource.keyL.SetKey(keyL);
				joystickSource.keyR.SetKey(keyR);
				return joystickSource;
			}

			public static GamepadProfile.JoystickSource Axes(int horzAxisId, bool horzPositiveRight, int vertAxisId, bool vertPositiveUp)
			{
				GamepadProfile.JoystickSource joystickSource = new GamepadProfile.JoystickSource();
				joystickSource.keyR.SetAxis(horzAxisId, horzPositiveRight);
				joystickSource.keyL.SetAxis(horzAxisId, horzPositiveRight);
				joystickSource.keyU.SetAxis(vertAxisId, vertPositiveUp);
				joystickSource.keyD.SetAxis(vertAxisId, vertPositiveUp);
				return joystickSource;
			}

			public bool IsDuplicateOf(GamepadProfile.JoystickSource a)
			{
				return this.keyD.IsDuplicateOf(a.keyD) && this.keyU.IsDuplicateOf(a.keyU) && this.keyL.IsDuplicateOf(a.keyL) && this.keyR.IsDuplicateOf(a.keyR);
			}

			public static GamepadProfile.JoystickSource Empty()
			{
				return new GamepadProfile.JoystickSource();
			}

			public GamepadProfile.KeySource keyU;

			public GamepadProfile.KeySource keyD;

			public GamepadProfile.KeySource keyR;

			public GamepadProfile.KeySource keyL;
		}

		[Serializable]
		public class KeySource
		{
			public KeySource()
			{
				this.keyId = -1;
				this.axisId = -1;
				this.axisSign = true;
			}

			private KeySource(int keyId, int axisId, bool axisSign)
			{
				this.axisId = axisId;
				this.keyId = keyId;
				this.axisSign = axisSign;
			}

			public bool IsEmpty()
			{
				return this.keyId < 0 && this.axisId < 0;
			}

			public bool IsDuplicateOf(GamepadProfile.KeySource a)
			{
				return this.keyId == a.keyId && this.axisId == a.axisId && this.axisSign == a.axisSign;
			}

			public void Clear()
			{
				this.axisId = -1;
				this.keyId = -1;
				this.axisSign = true;
			}

			public void SetKey(int keyId)
			{
				this.keyId = keyId;
				this.axisId = -1;
				this.axisSign = true;
			}

			public void SetAxis(int axisId, bool axisSign)
			{
				this.keyId = -1;
				this.axisId = axisId;
				this.axisSign = axisSign;
			}

			public static GamepadProfile.KeySource Key(int keyId)
			{
				return new GamepadProfile.KeySource(keyId, -1, true);
			}

			public static GamepadProfile.KeySource PlusAxis(int axisId)
			{
				return new GamepadProfile.KeySource(-1, axisId, true);
			}

			public static GamepadProfile.KeySource MinusAxis(int axisId)
			{
				return new GamepadProfile.KeySource(-1, axisId, false);
			}

			public static GamepadProfile.KeySource KeyAndPlusAxis(int keyId, int axisId)
			{
				return new GamepadProfile.KeySource(keyId, axisId, true);
			}

			public static GamepadProfile.KeySource KeyAndMinusAxis(int keyId, int axisId)
			{
				return new GamepadProfile.KeySource(keyId, axisId, false);
			}

			public static GamepadProfile.KeySource Empty()
			{
				return new GamepadProfile.KeySource(-1, -1, true);
			}

			public int keyId;

			public int axisId;

			public bool axisSign;
		}

		public class GenericProfile : GamepadProfile
		{
			public GenericProfile() : base("Generic Gamepad", string.Empty, GamepadProfile.ProfileMode.Normal, null, null, GamepadProfile.JoystickSource.Axes(0, true, 1, false), GamepadProfile.JoystickSource.Empty(), GamepadProfile.JoystickSource.Empty(), GamepadProfile.KeySource.Key(0), GamepadProfile.KeySource.Key(1), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Empty())
			{
			}

			public GenericProfile(GamepadProfile.JoystickSource leftStick, GamepadProfile.JoystickSource rightStick, GamepadProfile.JoystickSource dpad, GamepadProfile.KeySource keyFaceD, GamepadProfile.KeySource keyFaceR, GamepadProfile.KeySource keyFaceL, GamepadProfile.KeySource keyFaceU, GamepadProfile.KeySource keySelect, GamepadProfile.KeySource keyStart, GamepadProfile.KeySource keyL1, GamepadProfile.KeySource keyR1, GamepadProfile.KeySource keyL2, GamepadProfile.KeySource keyR2, GamepadProfile.KeySource keyL3, GamepadProfile.KeySource keyR3) : base("Generic Gamepad", string.Empty, GamepadProfile.ProfileMode.Normal, null, null, leftStick, rightStick, dpad, keyFaceD, keyFaceR, keyFaceL, keyFaceU, keySelect, keyStart, keyL1, keyR1, keyL2, keyR2, keyL3, keyR3)
			{
			}
		}
	}
}
