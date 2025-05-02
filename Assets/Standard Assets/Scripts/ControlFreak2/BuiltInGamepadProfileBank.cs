// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.BuiltInGamepadProfileBank
using System;
using UnityEngine;

namespace ControlFreak2
{
	public abstract class BuiltInGamepadProfileBank
	{
		public static GamepadProfile GetProfile(string deviceName)
		{
			BuiltInGamepadProfileBank builtInGamepadProfileBank = BuiltInGamepadProfileBank.Inst();
			return (builtInGamepadProfileBank != null) ? builtInGamepadProfileBank.FindProfile(deviceName) : null;
		}

		public static GamepadProfile GetGenericProfile()
		{
			BuiltInGamepadProfileBank builtInGamepadProfileBank = BuiltInGamepadProfileBank.Inst();
			return (builtInGamepadProfileBank != null) ? builtInGamepadProfileBank.GetInternalGenericProfile() : null;
		}

		protected virtual GamepadProfile GetInternalGenericProfile()
		{
			if (this.genericProfile == null)
			{
				this.genericProfile = new GamepadProfile.GenericProfile();
			}
			return this.genericProfile;
		}

		protected virtual GamepadProfile FindProfile(string deviceName)
		{
			return this.FindInternalProfile(deviceName);
		}

		protected GamepadProfile FindInternalProfile(string deviceName)
		{
			if (this.profiles == null || this.profiles.Length == 0)
			{
				return null;
			}
			for (int i = 0; i < this.profiles.Length; i++)
			{
				if (this.profiles[i] != null)
				{
					if (this.profiles[i].IsCompatible(deviceName))
					{
						return this.profiles[i];
					}
				}
			}
			return null;
		}

		private static BuiltInGamepadProfileBank Inst()
		{
			if (BuiltInGamepadProfileBank.mInst != null)
			{
				return BuiltInGamepadProfileBank.mInst;
			}
			RuntimePlatform platform = Application.platform;
			switch (platform)
			{
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXPlayer:
				BuiltInGamepadProfileBank.mInst = new BuiltInGamepadProfileBankOSX();
				goto IL_D4;
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
				break;
			default:
				switch (platform)
				{
				case RuntimePlatform.WebGLPlayer:
					BuiltInGamepadProfileBank.mInst = new BuiltInGamepadProfileBankWebGL();
					goto IL_D4;
				case RuntimePlatform.MetroPlayerX86:
				case RuntimePlatform.MetroPlayerX64:
				case RuntimePlatform.MetroPlayerARM:
					break;
				default:
					if (platform != RuntimePlatform.tvOS)
					{
						goto IL_D4;
					}
					goto IL_A7;
				}
				break;
			case RuntimePlatform.IPhonePlayer:
				goto IL_A7;
			case RuntimePlatform.Android:
				BuiltInGamepadProfileBank.mInst = new BuiltInGamepadProfileBankAndroid();
				goto IL_D4;
			case RuntimePlatform.LinuxPlayer:
				BuiltInGamepadProfileBank.mInst = new BuiltInGamepadProfileBankLinux();
				goto IL_D4;
			}
			BuiltInGamepadProfileBank.mInst = new BuiltInGamepadProfileBankWin();
			goto IL_D4;
			IL_A7:
			BuiltInGamepadProfileBank.mInst = new BuiltInGamepadProfileBankIOS();
			IL_D4:
			return BuiltInGamepadProfileBank.mInst;
		}

		private static BuiltInGamepadProfileBank mInst;

		protected GamepadProfile[] profiles;

		protected GamepadProfile genericProfile;
	}
}
