// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.DigitalBinding
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[Serializable]
	public class DigitalBinding : InputBindingBase
	{
		public DigitalBinding(InputBindingBase parent = null) : base(parent)
		{
			this.BasicConstructor();
		}

		public DigitalBinding(KeyCode key, bool bindToAxis, string axisName, bool axisNegSide, bool enabled, InputBindingBase parent = null) : base(parent)
		{
			this.BasicConstructor();
			if (enabled)
			{
				base.Enable();
			}
			if (key != KeyCode.None)
			{
				this.AddKey(key);
			}
			if (!string.IsNullOrEmpty(axisName))
			{
				this.AddAxis().SetAxis(axisName, !axisNegSide);
			}
		}

		public DigitalBinding(KeyCode key, string axisName, bool enabled, InputBindingBase parent = null) : base(parent)
		{
			this.BasicConstructor();
			if (enabled)
			{
				base.Enable();
			}
			if (key != KeyCode.None)
			{
				this.AddKey(key);
			}
			if (!string.IsNullOrEmpty(axisName))
			{
				this.AddAxis().SetAxis(axisName, true);
			}
		}

		public DigitalBinding(KeyCode key, bool enabled, InputBindingBase parent = null) : base(parent)
		{
			this.BasicConstructor();
			if (enabled)
			{
				base.Enable();
			}
			if (key != KeyCode.None)
			{
				this.AddKey(key);
			}
		}

		private void BasicConstructor()
		{
			this.enabled = false;
			this.keyList = new List<KeyCode>(1);
			this.axisList = new List<DigitalBinding.AxisElem>(1);
		}

		public void CopyFrom(DigitalBinding b)
		{
			if (b == null)
			{
				return;
			}
			if (this.enabled = b.enabled)
			{
				base.Enable();
				if (this.axisList.Count != b.axisList.Count)
				{
					this.axisList.Clear();
					for (int i = 0; i < b.axisList.Count; i++)
					{
						this.AddAxis();
					}
				}
				for (int j = 0; j < b.axisList.Count; j++)
				{
					this.axisList[j].CopyFrom(b.axisList[j]);
				}
				if (this.keyList.Count != b.keyList.Count)
				{
					this.keyList.Clear();
					for (int k = 0; k < b.keyList.Count; k++)
					{
						this.AddKey(b.keyList[k]);
					}
				}
				else
				{
					for (int l = 0; l < b.keyList.Count; l++)
					{
						this.keyList[l] = b.keyList[l];
					}
				}
			}
		}

		public void Sync(bool state, InputRig rig, bool skipIfTargetIsMuted = false)
		{
			if (!state || rig == null || !this.enabled)
			{
				return;
			}
			for (int i = 0; i < this.keyList.Count; i++)
			{
				rig.SetKeyCode(this.keyList[i]);
			}
			for (int j = 0; j < this.axisList.Count; j++)
			{
				DigitalBinding.AxisElem axisElem = this.axisList[j];
				axisElem.ApplyToRig(rig, skipIfTargetIsMuted);
			}
		}

		public void Clear()
		{
			this.ClearKeys();
			this.ClearAxes();
		}

		public void ClearKeys()
		{
			this.keyList.Clear();
		}

		public void ClearAxes()
		{
			this.axisList.Clear();
		}

		public void AddKey(KeyCode code)
		{
			this.keyList.Add(code);
		}

		public void RemoveLastKey()
		{
			if (this.keyList.Count > 0)
			{
				this.keyList.RemoveAt(this.keyList.Count - 1);
			}
		}

		public void ReplaceKey(int keyElemId, KeyCode key)
		{
			if (keyElemId < 0 || keyElemId >= this.keyList.Count)
			{
				return;
			}
			this.keyList[keyElemId] = key;
		}

		public DigitalBinding.AxisElem AddAxis()
		{
			DigitalBinding.AxisElem axisElem = new DigitalBinding.AxisElem();
			this.axisList.Add(axisElem);
			return axisElem;
		}

		public DigitalBinding.AxisElem GetAxisElem(int id)
		{
			return (id >= 0 && id < this.axisList.Count) ? this.axisList[id] : null;
		}

		public void RemoveLastAxis()
		{
			if (this.axisList.Count > 0)
			{
				this.axisList.RemoveAt(this.axisList.Count - 1);
			}
		}

		protected override bool OnIsBoundToKey(KeyCode keycode, InputRig rig)
		{
			return this.enabled && this.keyList.Count > 0 && this.keyList.IndexOf(keycode) >= 0;
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.enabled && this.axisList.Count > 0 && this.axisList.FindIndex((DigitalBinding.AxisElem x) => x.axisName == axisName) >= 0;
		}

		public List<DigitalBinding.AxisElem> axisList;

		public List<KeyCode> keyList;

		[Serializable]
		public class AxisElem
		{
			public AxisElem()
			{
				this.axisPositiveSide = true;
				this.axisName = string.Empty;
			}

			private bool OnIsBoundToAxis(string axisName, InputRig rig)
			{
				return this.axisName == axisName;
			}

			public void SetAxis(string axisName, bool positiveSide)
			{
				this.axisName = axisName;
				this.axisPositiveSide = positiveSide;
			}

			public void CopyFrom(DigitalBinding.AxisElem b)
			{
				this.axisName = b.axisName;
				this.axisPositiveSide = b.axisPositiveSide;
			}

			public void ApplyToRig(InputRig rig, bool skipIfTargetIsMuted)
			{
				InputRig.AxisConfig axisConfig = rig.GetAxisConfig(this.axisName, ref this.cachedAxisId);
				if (axisConfig == null)
				{
					return;
				}
				if (!skipIfTargetIsMuted || !axisConfig.IsMuted())
				{
					axisConfig.SetSignedDigital(this.axisPositiveSide);
				}
			}

			public string axisName;

			public bool axisPositiveSide;

			[NonSerialized]
			private int cachedAxisId;
		}
	}
}
