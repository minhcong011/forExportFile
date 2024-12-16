using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutorial : MonoBehaviour
{
    [SerializeField] private ParticleSystem circleWayEf;

    private void Update()
    {
    }
    public void PlayCircleWayEf()
    {
        circleWayEf.Stop();
        circleWayEf.Play();
    }
}
