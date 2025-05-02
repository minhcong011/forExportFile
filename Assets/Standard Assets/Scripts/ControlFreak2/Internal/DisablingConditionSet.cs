// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.DisablingConditionSet
using System;
using System.Collections.Generic;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class DisablingConditionSet
	{
		public DisablingConditionSet(InputRig rig)
		{
			this.rig = rig;
			this.switchList = new List<DisablingConditionSet.DisablingRigSwitch>(32);
			this.mobileModeRelation = DisablingConditionSet.MobileModeRelation.EnabledOnlyInMobileMode;
			this.disableWhenTouchScreenInactive = true;
			this.disableWhenCursorIsUnlocked = false;
		}

		public void SetRig(InputRig rig)
		{
			this.rig = rig;
		}

		public bool IsInEffect()
		{
			return (this.mobileModeRelation != DisablingConditionSet.MobileModeRelation.AlwaysEnabled && ((!CF2Input.IsInMobileMode()) ? (this.mobileModeRelation == DisablingConditionSet.MobileModeRelation.EnabledOnlyInMobileMode) : (this.mobileModeRelation == DisablingConditionSet.MobileModeRelation.DisabledInMobileMode))) || (!(this.rig == null) && ((this.disableWhenTouchScreenInactive && this.rig.AreTouchControlsSleeping()) || (this.disableWhenCursorIsUnlocked && !CFScreen.lockCursor) || this.IsDisabledByRigSwitches()));
		}

		public bool IsDisabledByRigSwitches()
		{
			if (this.rig == null)
			{
				return false;
			}
			for (int i = 0; i < this.switchList.Count; i++)
			{
				if (this.switchList[i].IsInEffect(this.rig))
				{
					return true;
				}
			}
			return false;
		}

		public DisablingConditionSet.MobileModeRelation mobileModeRelation;

		public bool disableWhenTouchScreenInactive;

		public bool disableWhenCursorIsUnlocked;

		public List<DisablingConditionSet.DisablingRigSwitch> switchList;

		[NonSerialized]
		private InputRig rig;

		public enum MobileModeRelation
		{
			EnabledOnlyInMobileMode,
			DisabledInMobileMode,
			AlwaysEnabled
		}

		[Serializable]
		public class DisablingRigSwitch
		{
			public DisablingRigSwitch()
			{
				this.name = string.Empty;
			}

			public DisablingRigSwitch(string name)
			{
				this.name = name;
			}

			public bool IsInEffect(InputRig rig)
			{
				return rig.rigSwitches.GetSwitchState(this.name, ref this.cachedId, this.disableWhenSwitchIsOff) != this.disableWhenSwitchIsOff;
			}

			public string name;

			public bool disableWhenSwitchIsOff;

			private int cachedId;
		}
	}
}
