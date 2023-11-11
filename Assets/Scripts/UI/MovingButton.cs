using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class MovingButton : MonoBehaviour
{
    [SerializeField] private float delta;
     private float duration;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    private bool isMove = false;
    private float direction = 1;

    public void Start()
    {
        isMove = true;
        direction = Random.Range(0, 2) == 1 ? direction : direction * -1;
        duration = Random.Range(minDuration, maxDuration);
        Moving();
    }

    private void Moving()
    {
        if (!isMove)
        {
            return;
        }
        direction = direction * -1;

        this.transform.DOLocalMoveY(transform.localPosition.y + direction * delta, duration).OnComplete(Moving);
    }

}
