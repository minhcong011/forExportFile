// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.DirectionBinding
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class DirectionBinding : InputBindingBase
	{
		public DirectionBinding(InputBindingBase parent = null) : base(parent)
		{
			this.bindDiagonals = true;
			this.dirBindingN = new DigitalBinding(this);
			this.dirBindingAny = new DigitalBinding(this);
			this.dirBindingU = new DigitalBinding(this);
			this.dirBindingUR = new DigitalBinding(this);
			this.dirBindingR = new DigitalBinding(this);
			this.dirBindingDR = new DigitalBinding(this);
			this.dirBindingD = new DigitalBinding(this);
			this.dirBindingDL = new DigitalBinding(this);
			this.dirBindingL = new DigitalBinding(this);
			this.dirBindingUL = new DigitalBinding(this);
		}

		public void CopyFrom(DirectionBinding b)
		{
			if (this.enabled = b.enabled)
			{
				base.Enable();
				this.bindDiagonals = b.bindDiagonals;
				this.bindMode = b.bindMode;
				this.dirBindingN.CopyFrom(b.dirBindingN);
				this.dirBindingAny.CopyFrom(b.dirBindingAny);
				this.dirBindingU.CopyFrom(b.dirBindingU);
				this.dirBindingUR.CopyFrom(b.dirBindingUR);
				this.dirBindingR.CopyFrom(b.dirBindingR);
				this.dirBindingDR.CopyFrom(b.dirBindingDR);
				this.dirBindingD.CopyFrom(b.dirBindingD);
				this.dirBindingDL.CopyFrom(b.dirBindingDL);
				this.dirBindingD.CopyFrom(b.dirBindingL);
				this.dirBindingUL.CopyFrom(b.dirBindingUL);
			}
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.dirBindingN, "Neutral", parentMenuPath, undoObject);
			descList.Add(this.dirBindingAny, "Any Non-Neutral", parentMenuPath, undoObject);
			descList.Add(this.dirBindingU, "Up", parentMenuPath, undoObject);
			if (descList.addUnusedBindings || this.bindDiagonals)
			{
				descList.Add(this.dirBindingUR, "Up-Right", parentMenuPath, undoObject);
			}
			descList.Add(this.dirBindingR, "Right", parentMenuPath, undoObject);
			if (descList.addUnusedBindings || this.bindDiagonals)
			{
				descList.Add(this.dirBindingDR, "Down-Right", parentMenuPath, undoObject);
			}
			descList.Add(this.dirBindingD, "Down", parentMenuPath, undoObject);
			if (descList.addUnusedBindings || this.bindDiagonals)
			{
				descList.Add(this.dirBindingDL, "Down-Left", parentMenuPath, undoObject);
			}
			descList.Add(this.dirBindingL, "Left", parentMenuPath, undoObject);
			if (descList.addUnusedBindings || this.bindDiagonals)
			{
				descList.Add(this.dirBindingUL, "Up-Left", parentMenuPath, undoObject);
			}
		}

		public void SyncDirectionState(DirectionState dirState, InputRig rig)
		{
			switch (this.bindMode)
			{
			case DirectionBinding.BindMode.Normal:
				this.SyncDirRaw(dirState.GetCur(), rig);
				break;
			case DirectionBinding.BindMode.OnChange:
				if (dirState.GetCur() != dirState.GetPrev())
				{
					this.SyncDirRaw(dirState.GetCur(), rig);
				}
				break;
			case DirectionBinding.BindMode.OnRelease:
				if (dirState.GetCur() == Dir.N && dirState.GetPrev() != Dir.N)
				{
					this.SyncDirRaw(dirState.GetPrev(), rig);
				}
				break;
			case DirectionBinding.BindMode.OriginalOnStart:
				if (dirState.GetOriginal() != Dir.N && dirState.GetPrevOriginal() != dirState.GetOriginal())
				{
					this.SyncDirRaw(dirState.GetOriginal(), rig);
				}
				break;
			case DirectionBinding.BindMode.OriginalUntilRelease:
				if (dirState.GetOriginal() != Dir.N)
				{
					this.SyncDirRaw(dirState.GetOriginal(), rig);
				}
				break;
			case DirectionBinding.BindMode.OriginalUntilChange:
				if (dirState.GetOriginal() != Dir.N && dirState.GetOriginal() == dirState.GetCur())
				{
					this.SyncDirRaw(dirState.GetOriginal(), rig);
				}
				break;
			}
		}

		public void SyncDirRaw(Dir dir, InputRig rig)
		{
			if (rig == null || !this.enabled)
			{
				return;
			}
			if (dir != Dir.N)
			{
				this.dirBindingAny.Sync(true, rig, false);
			}
			if (this.bindDiagonals)
			{
				switch (dir)
				{
				case Dir.N:
					this.dirBindingN.Sync(true, rig, false);
					break;
				case Dir.U:
					this.dirBindingU.Sync(true, rig, false);
					break;
				case Dir.UR:
					this.dirBindingUR.Sync(true, rig, false);
					break;
				case Dir.R:
					this.dirBindingR.Sync(true, rig, false);
					break;
				case Dir.DR:
					this.dirBindingDR.Sync(true, rig, false);
					break;
				case Dir.D:
					this.dirBindingD.Sync(true, rig, false);
					break;
				case Dir.DL:
					this.dirBindingDL.Sync(true, rig, false);
					break;
				case Dir.L:
					this.dirBindingL.Sync(true, rig, false);
					break;
				case Dir.UL:
					this.dirBindingUL.Sync(true, rig, false);
					break;
				}
			}
			else
			{
				if (dir == Dir.U || dir == Dir.UL || dir == Dir.UR)
				{
					this.dirBindingU.Sync(true, rig, false);
				}
				if (dir == Dir.R || dir == Dir.UR || dir == Dir.DR)
				{
					this.dirBindingR.Sync(true, rig, false);
				}
				if (dir == Dir.D || dir == Dir.DL || dir == Dir.DR)
				{
					this.dirBindingD.Sync(true, rig, false);
				}
				if (dir == Dir.L || dir == Dir.UL || dir == Dir.DL)
				{
					this.dirBindingL.Sync(true, rig, false);
				}
			}
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.enabled && (this.dirBindingN.IsBoundToAxis(axisName, rig) || this.dirBindingAny.IsBoundToAxis(axisName, rig) || this.dirBindingU.IsBoundToAxis(axisName, rig) || this.dirBindingR.IsBoundToAxis(axisName, rig) || this.dirBindingD.IsBoundToAxis(axisName, rig) || this.dirBindingL.IsBoundToAxis(axisName, rig) || (this.bindDiagonals && (this.dirBindingUR.IsBoundToAxis(axisName, rig) || this.dirBindingDR.IsBoundToAxis(axisName, rig) || this.dirBindingDL.IsBoundToAxis(axisName, rig) || this.dirBindingUL.IsBoundToAxis(axisName, rig))));
		}

		protected override bool OnIsBoundToKey(KeyCode keyCode, InputRig rig)
		{
			return this.enabled && (this.dirBindingN.IsBoundToKey(keyCode, rig) || this.dirBindingAny.IsBoundToKey(keyCode, rig) || this.dirBindingU.IsBoundToKey(keyCode, rig) || this.dirBindingR.IsBoundToKey(keyCode, rig) || this.dirBindingD.IsBoundToKey(keyCode, rig) || this.dirBindingL.IsBoundToKey(keyCode, rig) || (this.bindDiagonals && (this.dirBindingUR.IsBoundToKey(keyCode, rig) || this.dirBindingDR.IsBoundToKey(keyCode, rig) || this.dirBindingDL.IsBoundToKey(keyCode, rig) || this.dirBindingUL.IsBoundToKey(keyCode, rig))));
		}

		public bool bindDiagonals;

		public DirectionBinding.BindMode bindMode;

		public DigitalBinding dirBindingU;

		public DigitalBinding dirBindingUR;

		public DigitalBinding dirBindingR;

		public DigitalBinding dirBindingDR;

		public DigitalBinding dirBindingD;

		public DigitalBinding dirBindingDL;

		public DigitalBinding dirBindingL;

		public DigitalBinding dirBindingUL;

		public DigitalBinding dirBindingN;

		public DigitalBinding dirBindingAny;

		public enum BindMode
		{
			Normal,
			OnChange,
			OnRelease,
			OriginalOnStart,
			OriginalUntilRelease,
			OriginalUntilChange
		}
	}
}
