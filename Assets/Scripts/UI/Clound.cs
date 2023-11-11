using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Clound : MonoBehaviour
{
    [SerializeField] private float startPosX;
    [SerializeField] private float endPosX;

    [SerializeField] private float minPosY;
    [SerializeField] private float maxPosY;

    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;
    private float duration;
    [SerializeField] private float delay ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStartMove());
    }

    private IEnumerator DelayStartMove()
    {
        yield return new WaitForSeconds(delay);

        Moving();
    }

    private void Moving()
    {
        this.transform.localPosition = new Vector3(startPosX, Random.Range(minPosY, maxPosY));
        duration = Random.Range(minDuration, maxDuration);
      
       this.transform.DOLocalMoveX(endPosX, duration).OnComplete(Moving);
    }

}
