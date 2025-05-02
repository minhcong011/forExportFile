// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.BindingType
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public struct BindingDescription
	{
		public BindingDescription.BindingType type;

		public string name;

		public string nameFormatted;

		public string menuPath;

		public InputBindingBase binding;

		public InputRig.InputSource axisSource;

		public UnityEngine.Object undoObject;

		[Flags]
		public enum BindingType
		{
			Digital = 1,
			Axis = 2,
			EmuTouch = 32,
			MousePos = 64,
			All = 127
		}
	}
}
