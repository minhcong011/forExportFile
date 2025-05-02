// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.BuiltInGamepadProfileBankIOS
using System;

namespace ControlFreak2
{
	public class BuiltInGamepadProfileBankIOS : BuiltInGamepadProfileBank
	{
		public BuiltInGamepadProfileBankIOS()
		{
			this.genericProfile = new GamepadProfile("MFi Controller", "MFi Controller", GamepadProfile.ProfileMode.Normal, null, null, GamepadProfile.JoystickSource.Axes(0, true, 1, false), GamepadProfile.JoystickSource.Axes(2, true, 3, false), GamepadProfile.JoystickSource.Dpad(4, 5, 6, 7), GamepadProfile.KeySource.Key(14), GamepadProfile.KeySource.Key(13), GamepadProfile.KeySource.Key(15), GamepadProfile.KeySource.Key(12), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Key(0), GamepadProfile.KeySource.KeyAndPlusAxis(8, 8), GamepadProfile.KeySource.KeyAndPlusAxis(9, 9), GamepadProfile.KeySource.KeyAndPlusAxis(10, 10), GamepadProfile.KeySource.KeyAndPlusAxis(11, 11), GamepadProfile.KeySource.Key(-1), GamepadProfile.KeySource.Key(-1));
			this.profiles = new GamepadProfile[]
			{
				new GamepadProfile("Startus XL", "Startus XL", GamepadProfile.ProfileMode.Normal, null, null, GamepadProfile.JoystickSource.Axes(0, true, 1, false), GamepadProfile.JoystickSource.Axes(2, true, 3, false), GamepadProfile.JoystickSource.Dpad(4, 5, 6, 7), GamepadProfile.KeySource.Key(14), GamepadProfile.KeySource.Key(13), GamepadProfile.KeySource.Key(15), GamepadProfile.KeySource.Key(12), GamepadProfile.KeySource.Empty(), GamepadProfile.KeySource.Key(-1), GamepadProfile.KeySource.KeyAndPlusAxis(8, 8), GamepadProfile.KeySource.KeyAndPlusAxis(9, 9), GamepadProfile.KeySource.Key(10), GamepadProfile.KeySource.Key(11), GamepadProfile.KeySource.Key(-1), GamepadProfile.KeySource.Key(-1))
			};
		}
	}
}
