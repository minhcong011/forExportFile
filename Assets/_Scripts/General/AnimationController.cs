using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {
        if (animator == null) animator = gameObject.GetComponent<Animator>();
    }
    public void Play(string animationName)
    {
        animator.Play(animationName);
    }
}
