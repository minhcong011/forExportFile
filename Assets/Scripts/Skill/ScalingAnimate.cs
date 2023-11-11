using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScalingAnimate : MonoBehaviour
{
    bool canDo = true;
    [SerializeField] private float duration;
    private void Start()
    {
        canDo = true;
        Scaling();
    }
    private void Scaling()
    {
        if (!canDo) return;
        Vector3 scaleVec = this.transform.localScale.x == 1 ? Vector3.one * 1.3f : Vector3.one;
        this.transform.DOScale(scaleVec, duration).OnComplete(Scaling);
    }


    private void OnDestroy()
    {
        canDo = false;
    }
}
