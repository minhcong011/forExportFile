using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShip : MonoBehaviour
{
    public List<Sprite> enemyShipes = new List<Sprite>();
    public List<Sprite> pirates = new List<Sprite>();

    public Sprite GetRandomShip()
    {
        return enemyShipes[Random.Range(0, enemyShipes.Count)];
    }

    public Sprite GetRandomPirate()
    {
        return pirates[Random.Range(0, pirates.Count)];
    }

    public Sprite GetIndexPirate(int index)
    {
        if(index >= pirates.Count)
        {
            return null;
        }

        return pirates[index];
       
    }
}
