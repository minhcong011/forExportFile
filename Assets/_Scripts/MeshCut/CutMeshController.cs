using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutMeshController : SingletonBase<CutMeshController>
{
    [SerializeField] private GameObject blacePref;
    // Start is called before the first frame update
    public void CreateBlace(Vector3 spawnPos)
    {
        float randRotation = Random.Range(0, 360);
        GameObject newBlace = ObjectPooling.Instance.CreateObject(blacePref);
        newBlace.transform.position = spawnPos;
        newBlace.transform.eulerAngles = new Vector3(90, 0, randRotation);

    }
}
