using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotatingImage : MonoBehaviour
{

    [SerializeField] private float maxDuration;
    [SerializeField] private float minDuration;
    private float duration;
    [SerializeField] private float angleLeft = 0f;
    [SerializeField] private float angleRight = -5f;

    public bool canRotate = true;
    private void Start()
    {
        RotatingObject();
    }

    private void RotatingObject()
    {
        if (!canRotate) return;

      
        float endValue = WrapAngle(this.transform.eulerAngles.z) == angleRight ? angleLeft : angleRight;
        duration = Random.Range(minDuration, maxDuration);
        this.transform.DORotate(new Vector3(0, 0, endValue), duration).OnComplete(() => {
            RotatingObject();
        });
    }

    private void OnDestroy()
    {
        canRotate = false;
    }
    private float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }


}
