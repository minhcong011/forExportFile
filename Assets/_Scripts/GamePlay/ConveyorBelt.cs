using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorBelt : SingletonBase<ConveyorBelt>
{
    [SerializeField] private float speedMoveCar;
    [SerializeField] private float spawnCarDelay;
    [SerializeField] private Transform startMovePos;
    [SerializeField] private Transform endMovePos;
    [SerializeField] private GameObject carMoveHolderPref;

    [SerializeField] private List<GameObject> carSpawns;

    [SerializeField] private GameObject trashHolder;

    [SerializeField] private GameObject ground;

    private List<GameObject> moveCarHolders = new();
    private Dictionary<GameObject, Coroutine> moveCarHolderCoroutinueDic = new();
    private bool stopMove;

    public bool StopMove { get => stopMove; set => stopMove = value; }

    private int currentCarSpawnIndex;

    private void Start()
    {
        StartCoroutine(MoveGround());
        StartCoroutine(PutCarsToMoveHolder());
    }
    //IEnumerator MovingConveyorBelt()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < moveCarHolders.Count; i++)
    //        {
    //            SpawnCarToStarPos(moveCarHolders[i]);
    //            yield return new WaitForSeconds(spawnCarDelay);
    //            while (stopMove) yield return null;
    //        }
    //        yield return null;
    //    }
    //}
    private IEnumerator PutCarsToMoveHolder()
    {
        while (!ColorManager.Instance.finishGetColor) yield return null;
        for (int i = 0; i < carSpawns.Count; i++)
        {
            CarController carController = CarManager.Instance.carControllderDictionary[carSpawns[i]];
            GameObject newCarMoveHolder = ObjectPooling.Instance.CreateObject(carMoveHolderPref);
            carSpawns[i].transform.SetParent(newCarMoveHolder.transform, true);
            carSpawns[i].transform.position = newCarMoveHolder.transform.position;
            carController.IsCarInConveyorBetl = true;
            carController.ConveyorBetlParent = this;
            moveCarHolders.Add(newCarMoveHolder);

        }
        PutMoveHoldersToOutMap();
        SpawnCarWhenStart();
    }
    private void SpawnCarWhenStart()
    {
        for(float i = 0; i < 7; i++)
        {
            GameObject carToSpawn = SpawnCarToStarPos();
            carToSpawn.transform.position = Vector3.Lerp(startMovePos.position, endMovePos.position, i / 7f);
        }
    }
    private GameObject SpawnCarToStarPos()
    {
        GameObject moveHolder = moveCarHolders[currentCarSpawnIndex];
        currentCarSpawnIndex++;
        moveHolder.transform.position = startMovePos.position;
        if (!moveCarHolderCoroutinueDic.ContainsKey(moveHolder)) moveCarHolderCoroutinueDic.Add(moveHolder, null);
        moveCarHolderCoroutinueDic[moveHolder] = StartCoroutine(MovingMoveHolder(moveHolder));
        if (currentCarSpawnIndex >= moveCarHolders.Count) currentCarSpawnIndex = 0;
        return moveHolder;
    }
    private IEnumerator MovingMoveHolder(GameObject moveHolder)
    {
        while (Vector3.Distance(moveHolder.transform.position, endMovePos.position) > 0.1f)
        {
            if (!stopMove) moveHolder.transform.position = Vector3.MoveTowards(moveHolder.transform.position, endMovePos.position, speedMoveCar * Time.deltaTime);
            yield return null;
        }
        SpawnCarToStarPos();
        PutMoveCarHolderToOutMap(moveHolder);
    }
    private void PutMoveHoldersToOutMap()
    {
        foreach (GameObject moveHolder in  moveCarHolders)
        {
            PutMoveCarHolderToOutMap(moveHolder);
        }
    }
    private void PutMoveCarHolderToOutMap(GameObject moveHolder)
    {
        moveHolder.transform.position = new Vector3(-100, 0, 100);
        if(moveCarHolderCoroutinueDic.ContainsKey(moveHolder)) StopCoroutine(moveCarHolderCoroutinueDic[moveHolder]);
    }
    public void RemoveCar(GameObject carToRemove)
    {
        carToRemove.transform.SetParent(trashHolder.transform);
    }
    private IEnumerator MoveGround()
    {
        Vector3 targetPos = new Vector3(7, 0, 0.2691f);
        Vector3 startPos = ground.transform.localPosition;
        Debug.Log(startPos);
        while (ground.activeSelf)
        {
            ground.transform.localPosition = Vector3.MoveTowards(ground.transform.localPosition, targetPos, speedMoveCar * Time.deltaTime);
            if (Vector3.Distance(ground.transform.localPosition, targetPos) < 0.1f) ground.transform.localPosition = startPos;
            yield return null;
        }
    }
}
