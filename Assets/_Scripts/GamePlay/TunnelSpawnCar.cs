using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TunnelSpawnCar : MonoBehaviour
{
    [SerializeField] private List<GameObject> carHolders;
    [SerializeField] private Transform exitPos;
    [SerializeField] private float speedMoveCar;
    [SerializeField] private TextMeshPro amountCarInHolderText;

    private List<GameObject> carsForward = new();
    private void Start()
    {
        amountCarInHolderText.text = carHolders.Count.ToString();
        StartCoroutine(MoveCarToOutMap());
    }
    private IEnumerator MoveCarToOutMap()
    {
        while (!ColorManager.Instance.finishGetColor) yield return null;
        foreach(GameObject car in carHolders)
        {
            car.transform.position = new Vector3(-100,0,0);
        }
    }
    private void SpawnCar()
    {
        if (carHolders.Count == 0) return;
        GameObject carToSpawn = carHolders[Random.Range(0, carHolders.Count)];
        carHolders.Remove(carToSpawn);
        amountCarInHolderText.text = carHolders.Count.ToString();
        StartCoroutine(MoveCarToExitPos(carToSpawn.transform));
    }
    IEnumerator MoveCarToExitPos(Transform car)
    {
        car.transform.position = transform.position;
        car.LookAt(exitPos);
        while(Vector3.Distance(car.position, exitPos.position) > 0.1f)
        {
            car.position = Vector3.MoveTowards(car.position, exitPos.position, speedMoveCar * Time.deltaTime);
            yield return null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            carsForward.Remove(other.gameObject);
            if(carsForward.Count == 0)  SpawnCar();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && !carsForward.Contains(other.gameObject))
        {
            carsForward.Add(other.gameObject);
        }
    }
}
