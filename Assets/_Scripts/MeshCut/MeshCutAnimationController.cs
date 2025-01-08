using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCutAnimationController : MonoBehaviour
{
    public Animator animator;
    public IEnumerator PlayNomalAni()
    {
        animator.Play("SlimeAni");
        yield return new WaitForSeconds(2);
        animator.Play("SlimeAniNomal");
    }
}
