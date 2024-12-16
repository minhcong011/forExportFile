using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] garages;
    private void Start()
    {
        ShowGarage();
    }
    private void ShowGarage()
    {
        int level = GameCache.GC.currentLevel - 1;
        int garageId = level / 8;
        int carAmount = level % 8;
        garages[garageId].SetActive(true);
        garages[garageId].GetComponent<GarageCarController>().ShowCar(carAmount);
    }
}
