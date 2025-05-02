// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.InputBindingBase
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public abstract class InputBindingBase : IBindingContainer
	{
		public InputBindingBase(InputBindingBase parent)
		{
			this.enabled = false;
			this.parent = parent;
		}

		public InputBindingBase GetParent()
		{
			return this.parent;
		}

		public void Enable()
		{
			for (InputBindingBase inputBindingBase = this; inputBindingBase != null; inputBindingBase = inputBindingBase.parent)
			{
				inputBindingBase.enabled = true;
			}
		}

		public bool IsEnabledInHierarchy()
		{
			InputBindingBase inputBindingBase = this;
			while (inputBindingBase.enabled)
			{
				if ((inputBindingBase = inputBindingBase.parent) == null)
				{
					return true;
				}
			}
			return false;
		}

		public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			this.OnGetSubBindingDescriptions(descList, undoObject, parentMenuPath);
		}

		protected virtual void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
		}

		public bool IsBoundToKey(KeyCode key, InputRig rig)
		{
			return this.OnIsBoundToKey(key, rig);
		}

		public bool IsBoundToAxis(string axisName, InputRig rig)
		{
			return this.OnIsBoundToAxis(axisName, rig);
		}

		public bool IsEmulatingTouches()
		{
			return this.OnIsEmulatingTouches();
		}

		public bool IsEmulatingMousePosition()
		{
			return this.OnIsEmulatingMousePosition();
		}

		protected virtual bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			return false;
		}

		protected virtual bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return false;
		}

		protected virtual bool OnIsEmulatingTouches()
		{
			return false;
		}

		protected virtual bool OnIsEmulatingMousePosition()
		{
			return false;
		}

		public bool enabled;

		[NonSerialized]
		private InputBindingBase parent;
	}
}
