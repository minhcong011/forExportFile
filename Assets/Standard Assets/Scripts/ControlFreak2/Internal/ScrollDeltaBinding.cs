// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.ScrollDeltaBinding
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class ScrollDeltaBinding : InputBindingBase
	{
		public ScrollDeltaBinding(InputBindingBase parent = null) : base(parent)
		{
			this.deltaBinding = new AxisBinding("Mouse ScrollWheel", false, this);
			this.positiveDigitalBinding = new DigitalBinding(this);
			this.negativeDigitalBinding = new DigitalBinding(this);
		}

		public ScrollDeltaBinding(string axisName, bool enabled = false, InputBindingBase parent = null) : base(parent)
		{
			this.enabled = enabled;
			this.deltaBinding = new AxisBinding(axisName, enabled, this);
			this.positiveDigitalBinding = new DigitalBinding(this);
			this.negativeDigitalBinding = new DigitalBinding(this);
		}

		public void CopyFrom(ScrollDeltaBinding b)
		{
			if (this.enabled = b.enabled)
			{
				base.Enable();
				this.deltaBinding.CopyFrom(b.deltaBinding);
				this.positiveDigitalBinding.CopyFrom(b.positiveDigitalBinding);
				this.negativeDigitalBinding.CopyFrom(b.negativeDigitalBinding);
			}
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.deltaBinding, InputRig.InputSource.Scroll, "Delta Binding", parentMenuPath, undoObject);
			descList.Add(this.positiveDigitalBinding, "Positive Digital", parentMenuPath, undoObject);
			descList.Add(this.negativeDigitalBinding, "Negative Digital", parentMenuPath, undoObject);
		}

		public void SyncScrollDelta(int delta, InputRig rig)
		{
			if (rig == null || !this.enabled)
			{
				return;
			}
			this.deltaBinding.SyncScroll(delta, rig);
			if (delta != 0)
			{
				if (delta > 0)
				{
					this.positiveDigitalBinding.Sync(true, rig, false);
				}
				else
				{
					this.negativeDigitalBinding.Sync(true, rig, false);
				}
			}
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.enabled && (this.deltaBinding.IsBoundToAxis(axisName, rig) || this.positiveDigitalBinding.IsBoundToAxis(axisName, rig) || this.negativeDigitalBinding.IsBoundToAxis(axisName, rig));
		}

		protected override bool OnIsBoundToKey(KeyCode keyCode, InputRig rig)
		{
			return this.enabled && (this.deltaBinding.IsBoundToKey(keyCode, rig) || this.positiveDigitalBinding.IsBoundToKey(keyCode, rig) || this.negativeDigitalBinding.IsBoundToKey(keyCode, rig));
		}

		public AxisBinding deltaBinding;

		public DigitalBinding positiveDigitalBinding;

		public DigitalBinding negativeDigitalBinding;
	}
}
