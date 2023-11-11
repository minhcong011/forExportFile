using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionController : MonoBehaviour
{

    [SerializeField] private GameObject explotionBoom;

    public void CreateExplosion(Vector3 position)
    {
        GameObject clone = Instantiate(explotionBoom, this.transform);
        clone.transform.position = position ;
    }

}
