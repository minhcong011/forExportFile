// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.BindingDescriptionList
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public class BindingDescriptionList : List<BindingDescription>
	{
		public BindingDescriptionList(BindingDescription.BindingType typeMask, bool addUnusedBindings, int axisInputSourceMask, BindingDescriptionList.NameFormatter menuNameFormatter) : base(16)
		{
			this.Setup(typeMask, addUnusedBindings, axisInputSourceMask, menuNameFormatter);
		}

		public void Setup(BindingDescription.BindingType typeMask, bool addUnusedBindings, int axisInputSourceMask, BindingDescriptionList.NameFormatter menuNameFormatter)
		{
			this.typeMask = typeMask;
			this.addUnusedBindings = addUnusedBindings;
			this.menuNameFormatter = menuNameFormatter;
			this.axisInputSourceMask = axisInputSourceMask;
		}

		public void Add(InputBindingBase binding, string name, string menuPath, UnityEngine.Object undoObject)
		{
			BindingDescription.BindingType bindingType = (!(binding is AxisBinding)) ? ((!(binding is DigitalBinding)) ? ((!(binding is EmuTouchBinding)) ? ((!(binding is MousePositionBinding)) ? BindingDescription.BindingType.All : BindingDescription.BindingType.MousePos) : BindingDescription.BindingType.EmuTouch) : BindingDescription.BindingType.Digital) : BindingDescription.BindingType.Axis;
			string text = (this.menuNameFormatter == null) ? name : this.menuNameFormatter(binding, name);
			if ((bindingType & this.typeMask) == bindingType && bindingType != BindingDescription.BindingType.All)
			{
				base.Add(new BindingDescription
				{
					type = bindingType,
					name = name,
					nameFormatted = text,
					menuPath = menuPath,
					undoObject = undoObject,
					binding = binding
				});
			}
			menuPath = menuPath + text + "/";
			binding.GetSubBindingDescriptions(this, undoObject, menuPath);
		}

		public void Add(AxisBinding binding, InputRig.InputSource sourceType, string name, string menuPath, UnityEngine.Object undoObject)
		{
			if ((this.typeMask & BindingDescription.BindingType.Axis) == (BindingDescription.BindingType)0)
			{
				return;
			}
			if ((this.axisInputSourceMask & 1 << (int)sourceType) == 0)
			{
				return;
			}
			string nameFormatted = (this.menuNameFormatter == null) ? name : this.menuNameFormatter(binding, name);
			base.Add(new BindingDescription
			{
				type = BindingDescription.BindingType.Axis,
				axisSource = sourceType,
				name = name,
				nameFormatted = nameFormatted,
				menuPath = menuPath,
				undoObject = undoObject,
				binding = binding
			});
		}

		public BindingDescription.BindingType typeMask;

		public bool addUnusedBindings;

		public int axisInputSourceMask;

		public BindingDescriptionList.NameFormatter menuNameFormatter;

		public delegate string NameFormatter(InputBindingBase bind, string name);
	}
}
