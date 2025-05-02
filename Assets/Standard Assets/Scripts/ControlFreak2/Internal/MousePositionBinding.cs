// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.MousePositionBinding
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class MousePositionBinding : InputBindingBase
	{
		public MousePositionBinding(InputBindingBase parent = null) : base(parent)
		{
			this.enabled = false;
			this.priority = 0;
		}

		public MousePositionBinding(int prio, bool enabled, InputBindingBase parent = null) : base(parent)
		{
			this.enabled = enabled;
			this.priority = prio;
		}

		public void CopyFrom(MousePositionBinding b)
		{
			if (b == null)
			{
				return;
			}
			if (this.enabled = b.enabled)
			{
				base.Enable();
				this.priority = b.priority;
			}
		}

		public void SyncPos(Vector2 pos, InputRig rig)
		{
			if (!this.enabled || rig == null)
			{
				return;
			}
			rig.mouseConfig.SetPosition(pos, this.priority);
		}

		protected override bool OnIsEmulatingMousePosition()
		{
			return this.enabled;
		}

		public int priority;
	}
}
