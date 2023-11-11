using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovingShip : MonoBehaviour
{
    [SerializeField] private float maxPos;
    [SerializeField] private float minPos;
    private float duration;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    private bool isMove = false;

    public void Start()
    {
        isMove = true;
        Moving();
    }
    private void Moving()
    {
        if (!isMove)
        {
            return;
        }
        duration = Random.Range(minDuration, maxDuration);
        float endvalue = transform.localPosition.y >= maxPos ? minPos : maxPos;
        this.transform.DOLocalMoveY(endvalue, duration).OnComplete(Moving);
    }
}
