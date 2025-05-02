// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.AxisBinding
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class AxisBinding : InputBindingBase
	{
		public AxisBinding(InputBindingBase parent = null) : base(parent)
		{
			this.BasicConstructor();
		}

		public AxisBinding(string singleName, bool enabled, InputBindingBase parent = null) : base(parent)
		{
			this.BasicConstructor();
			this.AddTarget().SetSingleAxis(singleName, false);
			if (enabled)
			{
				base.Enable();
			}
		}

		private void BasicConstructor()
		{
			this.enabled = false;
			this.targetList = new List<AxisBinding.TargetElem>(1);
		}

		public void CopyFrom(AxisBinding b)
		{
			if (this.enabled = b.enabled)
			{
				base.Enable();
				if (this.targetList.Count != b.targetList.Count)
				{
					this.targetList.Clear();
					for (int i = 0; i < b.targetList.Count; i++)
					{
						this.AddTarget();
					}
				}
				for (int j = 0; j < b.targetList.Count; j++)
				{
					this.targetList[j].CopyFrom(b.targetList[j]);
				}
			}
		}

		public void Clear()
		{
			this.targetList.Clear();
		}

		public AxisBinding.TargetElem AddTarget()
		{
			AxisBinding.TargetElem targetElem = new AxisBinding.TargetElem();
			this.targetList.Add(targetElem);
			return targetElem;
		}

		public void RemoveLastTarget()
		{
			if (this.targetList.Count > 0)
			{
				this.targetList.RemoveAt(this.targetList.Count - 1);
			}
		}

		public AxisBinding.TargetElem GetTarget(int axisElemId)
		{
			return (axisElemId >= 0 && axisElemId < this.targetList.Count) ? this.targetList[axisElemId] : null;
		}

		public void SyncFloat(float val, InputRig.InputSource source, InputRig rig)
		{
			if (rig == null || !this.enabled)
			{
				return;
			}
			for (int i = 0; i < this.targetList.Count; i++)
			{
				this.targetList[i].SyncFloat(val, source, rig);
			}
		}

		public void SyncScroll(int val, InputRig rig)
		{
			if (rig == null || !this.enabled)
			{
				return;
			}
			for (int i = 0; i < this.targetList.Count; i++)
			{
				this.targetList[i].SyncScroll(val, rig);
			}
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			if (!this.enabled)
			{
				return false;
			}
			for (int i = 0; i < this.targetList.Count; i++)
			{
				if (this.targetList[i].IsBoundToAxis(axisName))
				{
					return true;
				}
			}
			return false;
		}

		protected override bool OnIsBoundToKey(KeyCode keycode, InputRig rig)
		{
			return false;
		}

		public float GetAxis(InputRig rig)
		{
			if (!this.enabled)
			{
				return 0f;
			}
			if (rig == null)
			{
				rig = CF2Input.activeRig;
			}
			if (rig == null)
			{
				return 0f;
			}
			if (this.targetList == null || this.targetList.Count == 0)
			{
				return 0f;
			}
			return this.targetList[0].GetAxis(rig);
		}

		public List<AxisBinding.TargetElem> targetList;

		[Serializable]
		public class TargetElem
		{
			public TargetElem()
			{
				this.separateAxes = false;
				this.singleAxis = string.Empty;
				this.reverseSingleAxis = false;
				this.positiveAxis = string.Empty;
				this.negativeAxis = string.Empty;
				this.positiveAxisAsPositive = true;
				this.negativeAxisAsPositive = true;
				this.singleAxisId = 0;
				this.positiveAxisId = 0;
				this.negativeAxisId = 0;
			}

			public void SetSingleAxis(string name, bool flip)
			{
				this.separateAxes = false;
				this.singleAxis = name;
				this.reverseSingleAxis = flip;
			}

			public void SetSeparateAxis(string name, bool positiveSide, bool asPositive)
			{
				this.separateAxes = true;
				if (positiveSide)
				{
					this.positiveAxis = name;
					this.positiveAxisAsPositive = asPositive;
				}
				else
				{
					this.negativeAxis = name;
					this.negativeAxisAsPositive = asPositive;
				}
			}

			public void SyncFloat(float val, InputRig.InputSource source, InputRig rig)
			{
				if (this.separateAxes)
				{
					if (val >= 0f)
					{
						rig.SetAxis(this.positiveAxis, ref this.positiveAxisId, (!this.positiveAxisAsPositive) ? (-val) : val, source);
					}
					else
					{
						rig.SetAxis(this.negativeAxis, ref this.negativeAxisId, (!this.negativeAxisAsPositive) ? val : (-val), source);
					}
				}
				else
				{
					rig.SetAxis(this.singleAxis, ref this.singleAxisId, (!this.reverseSingleAxis) ? val : (-val), source);
				}
			}

			public void SyncScroll(int val, InputRig rig)
			{
				if (!this.separateAxes)
				{
					rig.SetAxisScroll(this.singleAxis, ref this.singleAxisId, (!this.reverseSingleAxis) ? val : (-val));
				}
			}

			public float GetAxis(InputRig rig)
			{
				if (rig == null)
				{
					return 0f;
				}
				if (this.separateAxes)
				{
					return ((!this.positiveAxisAsPositive) ? -1f : 1f) * rig.GetAxis(this.positiveAxis, ref this.positiveAxisId) - ((!this.negativeAxisAsPositive) ? -1f : 1f) * rig.GetAxis(this.negativeAxis, ref this.negativeAxisId);
				}
				return rig.GetAxis(this.singleAxis, ref this.singleAxisId);
			}

			public bool IsBoundToAxis(string axisName)
			{
				return (!this.separateAxes) ? (this.singleAxis == axisName) : (this.positiveAxis == axisName || this.negativeAxis == axisName);
			}

			public void CopyFrom(AxisBinding.TargetElem elem)
			{
				this.separateAxes = elem.separateAxes;
				this.singleAxis = elem.singleAxis;
				this.reverseSingleAxis = elem.reverseSingleAxis;
				this.positiveAxis = elem.positiveAxis;
				this.positiveAxisAsPositive = elem.positiveAxisAsPositive;
				this.negativeAxis = elem.negativeAxis;
				this.negativeAxisAsPositive = elem.negativeAxisAsPositive;
			}

			public bool separateAxes;

			public string singleAxis;

			public bool reverseSingleAxis;

			public string positiveAxis;

			public string negativeAxis;

			public bool positiveAxisAsPositive;

			public bool negativeAxisAsPositive;

			private int singleAxisId;

			private int positiveAxisId;

			private int negativeAxisId;
		}
	}
}
