using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPlayAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.enabled = false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
}
