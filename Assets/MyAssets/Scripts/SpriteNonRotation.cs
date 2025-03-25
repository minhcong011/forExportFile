using System.Collections;
using UnityEngine;

public class SpriteNonRotation : MonoBehaviour
{
    [SerializeField] private bool font;
    Quaternion initialRotation;

    public bool canReset;
    void Start()
    {
        initialRotation = transform.rotation;
        StartCoroutine(CheckReset());
    }

    public void ResetRotation()
    {
        if(!font)
        transform.eulerAngles = new Vector3(90, 90, 0);
        else transform.eulerAngles = new Vector3(90, 0, 0);
    }
    IEnumerator CheckReset()
    {
        while (!canReset)
        {
            yield return null;
        }
        ResetRotation();
    }
}
