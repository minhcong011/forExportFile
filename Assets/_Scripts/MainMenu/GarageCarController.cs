using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageCarController : MonoBehaviour
{
    [SerializeField] private GameObject[] cars;
    public void ShowCar(int amountCar)
    {
        for(int i = 0; i < amountCar; i++)
        {
            cars[i].SetActive(true);
        }
    }
}
