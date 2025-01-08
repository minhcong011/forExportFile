using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetAnimationController : SingletonBase<MeetAnimationController>
{
    [SerializeField] private Animator animator;

    public void PlayIdle()
    {
        if (GameCache.GC.currentWeaponType == WeaponType.Knife) return;
        animator.Play("Idle");
    }

    public void PlayDeath()
    {
        if (GameCache.GC.currentWeaponType == WeaponType.Knife) return;
        animator.Play("Death");
    }
    public void PlayHit()
    {
        if (GameCache.GC.currentWeaponType == WeaponType.Knife) return;
        animator.Play("Hit1");
    }
    public void Play(string name)
    {
        if (GameCache.GC.currentWeaponType == WeaponType.Knife) return;
        animator.Play(name);
    }
}
