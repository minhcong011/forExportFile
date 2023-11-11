using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMyself : MonoBehaviour
{
    public void Destroymyself()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
