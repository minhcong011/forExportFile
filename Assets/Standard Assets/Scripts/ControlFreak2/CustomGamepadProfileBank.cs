// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.CustomGamepadProfileBank
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace ControlFreak2
{
	public class CustomGamepadProfileBank
	{
		public CustomGamepadProfileBank()
		{
			this.profileList = new List<GamepadProfile>(16);
		}

		public int FindDuplicate(GamepadProfile profile)
		{
			for (int i = 0; i < this.profileList.Count; i++)
			{
				if (this.profileList[i].IsDuplicateOf(profile))
				{
					return i;
				}
			}
			return -1;
		}

		public GamepadProfile GetProfileById(int i)
		{
			if (i < 0 || i >= this.profileList.Count)
			{
				return null;
			}
			return this.profileList[i];
		}

		public GamepadProfile GetProfile(string deviceName)
		{
			for (int i = 0; i < this.profileList.Count; i++)
			{
				GamepadProfile gamepadProfile = this.profileList[i];
				if (gamepadProfile.IsCompatible(deviceName))
				{
					return gamepadProfile;
				}
			}
			return null;
		}

		public void GetCompatibleProfiles(string deviceName, List<GamepadProfile> targetList)
		{
			for (int i = 0; i < this.profileList.Count; i++)
			{
				GamepadProfile gamepadProfile = this.profileList[i];
				if (gamepadProfile.IsCompatible(deviceName) && !targetList.Contains(gamepadProfile))
				{
					targetList.Add(gamepadProfile);
				}
			}
		}

		public GamepadProfile AddProfile(GamepadProfile profile)
		{
			if (profile == null)
			{
				return null;
			}
			int num = this.FindDuplicate(profile);
			if (num >= 0)
			{
				GamepadProfile gamepadProfile = this.profileList[num];
				this.profileList.RemoveAt(num);
				this.profileList.Insert(0, gamepadProfile);
				gamepadProfile.AddSupportedVersion(profile.unityVerTo);
				return gamepadProfile;
			}
			this.profileList.Insert(0, profile);
			return profile;
		}

		public void Load()
		{
			this.LoadFromPlayerPrefs();
		}

		public void Save()
		{
			this.SaveToPlayerPrefs();
		}

		private bool LoadFromPlayerPrefs()
		{
			string @string = PlayerPrefs.GetString("CF2GamepadProfiles", null);
			if (@string == null || @string.Length == 0)
			{
				return false;
			}
			TextReader input = new StringReader(@string);
			XmlTextReader stream = new XmlTextReader(input);
			return this.LoadFromStream(stream);
		}

		private bool SaveToPlayerPrefs()
		{
			TextWriter textWriter = new StringWriter();
			XmlTextWriter stream = new XmlTextWriter(textWriter);
			if (!this.SaveToStream(stream))
			{
				return false;
			}
			string value = textWriter.ToString();
			try
			{
				PlayerPrefs.SetString("CF2GamepadProfiles", value);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		private XmlSerializer CreateSerializer()
		{
			return new XmlSerializer(typeof(List<GamepadProfile>), new Type[]
			{
				typeof(GamepadProfile),
				typeof(GamepadProfile.JoystickSource),
				typeof(GamepadProfile.KeySource)
			});
		}

		private bool SaveToStream(XmlTextWriter stream)
		{
			if (stream == null)
			{
				return false;
			}
			try
			{
				XmlSerializer xmlSerializer = this.CreateSerializer();
				xmlSerializer.Serialize(stream, this.profileList);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		private bool LoadFromStream(XmlTextReader stream)
		{
			if (stream == null)
			{
				return false;
			}
			List<GamepadProfile> list = null;
			try
			{
				XmlSerializer xmlSerializer = this.CreateSerializer();
				list = (List<GamepadProfile>)xmlSerializer.Deserialize(stream);
			}
			catch (Exception ex)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"LoadedProfile[",
					i,
					"] : [",
					list[i].name,
					"] {",
					list[i].joystickIdentifier,
					"}!"
				}));
			}
			return true;
		}

		public List<GamepadProfile> profileList;

		public const string GAMEPAD_PROFILE_LIST_KEY = "CF2GamepadProfiles";
	}
}
