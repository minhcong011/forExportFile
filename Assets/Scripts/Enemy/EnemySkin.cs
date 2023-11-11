using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkin : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ship;
    [SerializeField] private SpriteRenderer[] enemyPoints;
    

    public void Set()
    {
        ship.sprite = GameController.controller.GetRandomShip();
        int points = Random.Range(2, 4);

        List<int> pointsList = new List<int>(3) {0,1,2};
        List<int> enemySkin = new List<int>(9);
        for (int i = 0; i < 9; i++)
        {
            enemySkin.Add(i);
        }




        for (int i = 0; i < points; i++)
        {
            int random = pointsList[Random.Range(0, pointsList.Count)];
            pointsList.Remove(random);

            int index = enemySkin[Random.Range(0, enemySkin.Count)];
            enemySkin.Remove(index);

            enemyPoints[random].sprite = GameController.controller.GetIndexPirate(index);
        }
    }


}
