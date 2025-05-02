// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.IBindingContainer
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public interface IBindingContainer
	{
		void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath);

		bool IsBoundToKey(KeyCode key, InputRig rig);

		bool IsBoundToAxis(string axisName, InputRig rig);

		bool IsEmulatingTouches();

		bool IsEmulatingMousePosition();
	}
}
