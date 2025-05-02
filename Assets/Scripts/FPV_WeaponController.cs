// dnSpy decompiler from Assembly-CSharp.dll class: FPV_WeaponController
using System;
using UnityEngine;

public class FPV_WeaponController : MonoBehaviour
{
	private void Awake()
	{
		for (int i = 0; i < this.attackAnimations.Length; i++)
		{
			this.animation_HAND.AddClip(this.attackAnimations[i], "FIRE_" + i.ToString());
			this.animation_WEAPON.AddClip(this.attackAnimations[i], "FIRE_" + i.ToString());
			this.animation_HAND["FIRE_" + i.ToString()].wrapMode = WrapMode.Once;
			this.animation_WEAPON["FIRE_" + i.ToString()].wrapMode = WrapMode.Once;
			this.animation_HAND["FIRE_" + i.ToString()].layer = 2;
			this.animation_WEAPON["FIRE_" + i.ToString()].layer = 2;
			this.animation_HAND["FIRE_" + i.ToString()].blendMode = AnimationBlendMode.Blend;
			this.animation_WEAPON["FIRE_" + i.ToString()].blendMode = AnimationBlendMode.Blend;
		}
		this.animation_HAND.AddClip(this.idleAnimation, "IDLE");
		this.animation_WEAPON.AddClip(this.idleAnimation, "IDLE");
		this.animation_HAND["IDLE"].wrapMode = WrapMode.Loop;
		this.animation_WEAPON["IDLE"].wrapMode = WrapMode.Loop;
		this.animation_HAND["IDLE"].layer = 1;
		this.animation_WEAPON["IDLE"].layer = 1;
		this.animation_HAND.AddClip(this.drawAnimation, "DRAW");
		this.animation_WEAPON.AddClip(this.drawAnimation, "DRAW");
		this.animation_HAND["DRAW"].wrapMode = WrapMode.Once;
		this.animation_WEAPON["DRAW"].wrapMode = WrapMode.Once;
		this.animation_HAND["DRAW"].layer = 2;
		this.animation_WEAPON["DRAW"].layer = 2;
		this.animation_HAND["DRAW"].blendMode = AnimationBlendMode.Blend;
		this.animation_WEAPON["DRAW"].blendMode = AnimationBlendMode.Blend;
		if (this.reloadAnimation)
		{
			this.animation_HAND.AddClip(this.reloadAnimation, "RELOAD");
			this.animation_WEAPON.AddClip(this.reloadAnimation, "RELOAD");
			this.animation_HAND["RELOAD"].wrapMode = WrapMode.Once;
			this.animation_WEAPON["RELOAD"].wrapMode = WrapMode.Once;
			this.animation_HAND["RELOAD"].layer = 2;
			this.animation_WEAPON["RELOAD"].layer = 2;
			this.animation_HAND["RELOAD"].blendMode = AnimationBlendMode.Blend;
			this.animation_WEAPON["RELOAD"].blendMode = AnimationBlendMode.Blend;
		}
	}

	public void Draw()
	{
		this.animation_HAND.Play("DRAW");
		this.animation_WEAPON.Play("DRAW");
	}

	public void Idle()
	{
		this.animation_HAND.Play("IDLE");
		this.animation_WEAPON.Play("IDLE");
	}

	public void Reload()
	{
		this.animation_HAND.Stop(this.FireAnimationName_last);
		this.animation_WEAPON.Stop(this.FireAnimationName_last);
		this.animation_HAND.Play("RELOAD");
		this.animation_WEAPON.Play("RELOAD");
	}

	public void Attack()
	{
		string text = "FIRE_" + UnityEngine.Random.Range(0, this.attackAnimations.Length).ToString();
		this.animation_HAND.Stop(this.FireAnimationName_last);
		this.animation_WEAPON.Stop(this.FireAnimationName_last);
		this.animation_HAND.Play(text);
		this.animation_WEAPON.Play(text);
		this.FireAnimationName_last = text;
	}

	public Animation animation_HAND;

	public Animation animation_WEAPON;

	public AnimationClip[] attackAnimations;

	public AnimationClip idleAnimation;

	public AnimationClip drawAnimation;

	public AnimationClip reloadAnimation;

	public ParticleSystem[] emittOnAttack;

	public Vector3 normalOffset;

	public float lerp = 1f;

	public float weaponMove_High;

	public float weaponMove_Speed_Run;

	public float weaponMove_Speed_Walk;

	private string FireAnimationName_last;

	private float weaponMoveOffset;

	public Transform _muzzle;

	public Transform bulletOrigin;

	public string bulletName = "Bullet_Tps";
}
