// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.EmuTouchBinding
using System;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class EmuTouchBinding : InputBindingBase
	{
		public EmuTouchBinding(InputBindingBase parent = null) : base(parent)
		{
			this.enabled = false;
			this.emuTouchId = -1;
		}

		public void CopyFrom(EmuTouchBinding b)
		{
			if (b == null)
			{
				return;
			}
			if (this.enabled = b.enabled)
			{
				base.Enable();
			}
		}

		public void SyncState(TouchGestureBasicState touchState, InputRig rig)
		{
			if (!this.enabled || rig == null)
			{
				return;
			}
			rig.SyncEmuTouch(touchState, ref this.emuTouchId);
		}

		protected override bool OnIsEmulatingTouches()
		{
			return this.enabled;
		}

		private int emuTouchId;
	}
}
