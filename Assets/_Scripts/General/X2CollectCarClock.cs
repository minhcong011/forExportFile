using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2CollectCarClock : SingletonBaseDontDestroyOnLoad<X2CollectCarClock>
{
    private void Start()
    {
        StartCoroutine(StartClock());
    }
    IEnumerator StartClock()
    {
        while (true)
        {
            if(GameCache.GC.x2CollectCarBoosterTime == 0)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                GameCache.GC.x2CollectCarBoosterTime -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
