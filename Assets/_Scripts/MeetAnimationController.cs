using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetAnimationController : SingletonBase<MeetAnimationController>
{
    [SerializeField] private Animator animator;

    public void PlayIdle()
    {
        animator.Play("Idle");
    }

    public void PlayDeath()
    {
        animator.Play("Death");
    }
    public void PlayHit()
    {
        animator.Play("Hit1");
    }
    public void Play(string name)
    {
        animator.Play(name);
    }
}
