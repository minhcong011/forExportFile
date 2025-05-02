// dnSpy decompiler from Assembly-UnityScript.dll class: CrossFade
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class CrossFade : MonoBehaviour
{
	public CrossFade()
	{
		this.fadeTime = 2f;
	}

	public virtual void Update()
	{
		if (UnityEngine.Input.GetKeyDown("space"))
		{
			this.DoFade();
		}
	}

	public virtual void DoFade()
	{
		this.camera2.gameObject.SetActive(true);
		this.inProgress = false;
	}

	public virtual IEnumerator FinalFade()
	{
		return new CrossFade._0024FinalFade_002464(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}

	public Camera camera1;

	public Camera camera2;

	public Camera camera3;

	public float fadeTime;

	private bool inProgress;

	private bool swap;

	[CompilerGenerated]
	[Serializable]
	internal sealed class _0024FinalFade_002464 : List<Coroutine>
	{
		public _0024FinalFade_002464(CrossFade self_)
		{
			this._0024self__002466 = self_;
		}

		//public override IEnumerator<Coroutine> GetEnumerator()
		//{
  //          return null;// new CrossFade._0024FinalFade_002464._0024(this._0024self__002466);
		//}

		internal CrossFade _0024self__002466;
	}
}
