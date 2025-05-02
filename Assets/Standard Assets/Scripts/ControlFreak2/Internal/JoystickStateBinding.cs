// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.JoystickStateBinding
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class JoystickStateBinding : InputBindingBase
	{
		public JoystickStateBinding(InputBindingBase parent = null) : base(parent)
		{
			this.enabled = false;
			this.horzAxisBinding = new AxisBinding(this);
			this.vertAxisBinding = new AxisBinding(this);
			this.dirBinding = new DirectionBinding(this);
		}

		public void CopyFrom(JoystickStateBinding b)
		{
			if (this.enabled = b.enabled)
			{
				base.Enable();
				this.dirBinding.CopyFrom(b.dirBinding);
				this.horzAxisBinding.CopyFrom(b.horzAxisBinding);
				this.vertAxisBinding.CopyFrom(b.vertAxisBinding);
			}
		}

		public void SyncJoyState(JoystickState state, InputRig rig)
		{
			if (state == null || !this.enabled || rig == null)
			{
				return;
			}
			Vector2 vector = state.GetVector();
			this.horzAxisBinding.SyncFloat(vector.x, InputRig.InputSource.Analog, rig);
			this.vertAxisBinding.SyncFloat(vector.y, InputRig.InputSource.Analog, rig);
			this.dirBinding.SyncDirectionState(state.GetDirState(), rig);
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.enabled && (this.horzAxisBinding.IsBoundToAxis(axisName, rig) || this.vertAxisBinding.IsBoundToAxis(axisName, rig) || this.dirBinding.IsBoundToAxis(axisName, rig));
		}

		protected override bool OnIsBoundToKey(KeyCode keyCode, InputRig rig)
		{
			return this.enabled && (this.horzAxisBinding.IsBoundToKey(keyCode, rig) || this.vertAxisBinding.IsBoundToKey(keyCode, rig) || this.dirBinding.IsBoundToKey(keyCode, rig));
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.horzAxisBinding, InputRig.InputSource.Analog, "Horizontal", parentMenuPath, undoObject);
			descList.Add(this.vertAxisBinding, InputRig.InputSource.Analog, "Vertical", parentMenuPath, undoObject);
			descList.Add(this.dirBinding, "Direction", parentMenuPath, undoObject);
		}

		public AxisBinding horzAxisBinding;

		public AxisBinding vertAxisBinding;

		public DirectionBinding dirBinding;
	}
}
