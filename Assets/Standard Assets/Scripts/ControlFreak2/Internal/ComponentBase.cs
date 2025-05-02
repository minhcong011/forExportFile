// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.ComponentBase
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[AddComponentMenu("")]
	[ExecuteInEditMode]
	public abstract class ComponentBase : MonoBehaviour
	{
		public bool IsInitialized
		{
			get
			{
				return this.isReady;
			}
		}

		public bool IsDestroyed
		{
			get
			{
				return this.isDestroyed;
			}
		}

		protected abstract void OnInitComponent();

		protected abstract void OnDestroyComponent();

		protected abstract void OnEnableComponent();

		protected abstract void OnDisableComponent();

		public bool CanBeUsed()
		{
			if (this.isDestroyed)
			{
				return false;
			}
			if (!this.isReady)
			{
				this.isReady = true;
				this.OnInitComponent();
			}
			return true;
		}

		public void Init()
		{
			if (this.isReady)
			{
				return;
			}
			this.isReady = true;
			this.OnInitComponent();
		}

		public void ForceInit()
		{
			this.isReady = true;
			this.OnInitComponent();
		}

		private void OnEnable()
		{
			this.Init();
			this.OnEnableComponent();
		}

		private void OnDisable()
		{
			this.OnDisableComponent();
		}

		private void OnDestroy()
		{
			this.isDestroyed = true;
			this.isReady = false;
			this.OnDestroyComponent();
		}

		[NonSerialized]
		private bool isReady;

		[NonSerialized]
		private bool isDestroyed;
	}
}
