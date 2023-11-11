
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class OceanWater : MonoBehaviour
{
    [SerializeField] private float delta;
    private float duration;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    private bool isMove = false;
    public float direction = 1;

    private void Start()
    {
        isMove = true;
        direction = Random.Range(0, 2) == 1 ? direction : direction * -1;
        duration = Random.Range(minDuration, maxDuration);
        Moving();
    }

    private void Moving()
    {
        if(!isMove)
        {
            return;
        }
        direction = direction * -1;

        this.transform.DOMoveX(transform.position.x + direction * delta, duration).OnComplete(Moving);
    }

}
