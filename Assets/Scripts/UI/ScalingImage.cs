using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScalingImage : MonoBehaviour
{
    [SerializeField] private float maxValue;
    [SerializeField] private float minValue;
     private float endValue;
    [SerializeField] private float duration;
    void Start()
    {
        ScalingObject();
    }

    private void ScalingObject()
    {
        endValue = maxValue == endValue ? minValue : maxValue;
        this.transform.DOScale(new Vector3(endValue, endValue, endValue), duration).OnComplete(() => {
            ScalingObject();
        });
    }

}
