using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BulletRotation : MonoBehaviour
{

    [SerializeField] private float endValue;

    [SerializeField] private float maxDuration;
    [SerializeField] private float minDuration;
    private float duration;

    public bool canRotate = true;
    public void StartRotate()
    {
        RotatingObject();
    }

    private void RotatingObject()
    {
        if (!canRotate) return;
        endValue += 1f;
        this.transform.DORotate(new Vector3(0, 0, endValue), duration).OnComplete(RotatingObject);

    }

    private void OnDestroy()
    {
        canRotate = false;
    }
}
