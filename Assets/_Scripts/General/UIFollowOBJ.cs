using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowOBJ : MonoBehaviour
{
    [SerializeField] private bool isCanFollowAllTime;
    [SerializeField] private Transform objToFollow;
    [SerializeField] private Vector3 offSet;
    private void Start()
    {
        StartCoroutine(FollowingOBJ());
    }
    private IEnumerator FollowingOBJ()
    {
        transform.position = Camera.main.WorldToScreenPoint(objToFollow.position + offSet);
        if (!isCanFollowAllTime) yield break;
        while (true)
        {
            transform.position = Camera.main.WorldToScreenPoint(objToFollow.position + offSet);
            yield return null;
        }
    }
}
