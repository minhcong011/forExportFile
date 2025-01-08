using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        if (animator == null)
        {
            Debug.Log("animator error" + gameObject.name);
            return;
        }
        animator.Play(animationName);
    }
    public bool CheckAnimationStage(string name)
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == name;
    }
    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    public void PlaySound(string soundName)
    {
        //AudioManager.Instance.PlaySound(soundName);
    }
    public void DisableParent()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
