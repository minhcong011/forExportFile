// dnSpy decompiler from Assembly-CSharp.dll class: EnemyBadgeScaler
using System;
using UnityEngine;

public class EnemyBadgeScaler : MonoBehaviour
{
	private void Awake()
	{
		this.scale = base.GetComponent<TweenScale>();
	}

	private void OnEnable()
	{
		this.scale.PlayForward();
		if (this.pos != null)
		{
			this.pos.PlayForward();
		}
		if (this.shouldDisable)
		{
			base.Invoke("makeSelfDisable", 1f);
		}
	}

	private void makeSelfDisable()
	{
		base.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		this.scale.ResetToBeginning();
		if (this.pos != null)
		{
			this.pos.ResetToBeginning();
		}
	}

	private TweenScale scale;

	public TweenPosition pos;

	public bool shouldDisable;
}
