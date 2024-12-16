using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUp : MonoBehaviour
{
    [SerializeField] private GameObject sManPref;
    [Header("NomalMode")]
    [SerializeField] private Transform verticalSpawnStartPos;
    [SerializeField] private Transform verticalSpawnEndPos;
    [SerializeField] private Transform horizontalSpawnStartPos;
    [SerializeField] private Transform horizontalSpawnEndPos;
    [Header("DailyMode")]
    [SerializeField] private Transform line1SpawnPosStart;
    [SerializeField] private Transform line1SpawnPosEnd;
    [SerializeField] private Transform line2SpawnPosStart;
    [SerializeField] private Transform line2SpawnPosEnd;
    [SerializeField] private Transform line3SpawnPosStart;
    [SerializeField] private Transform line3SpawnPosEnd;

    [SerializeField] private GameObject carVipHolder;

    private Dictionary<int, int> numSManCommonColor = new();

    private int numberSManSpawn;
    public int NumberSManSpawn { get { return numberSManSpawn; } set { numberSManSpawn = value; } }

    private List<SManController> sManList = new();

    private int amountSManSpawnOneTurn;
    private int currentSManColorID;

    private int oldSManColorID = 1000;

    public List<SManController> sManMoveToCar = new();

    [SerializeField] private bool dailyMode;
    private void Start()
    {
        numberSManSpawn = ColorManager.Instance.GetMaxNumberSManSpawn();
        //numSManCommonColor = ColorManager.Instance.numSManCommonColor;
        //numberSManSpawn = ColorManager.Instance.GetMaxNumberSManSpawn();
        //StartCoroutine(SpawnSManWhenStart());
        //GamePlayUIManager.Instance.UpdateAmountSManRemainText();
    }
    private IEnumerator SpawnSManWhenStart()
    {
        int countSMan = Mathf.Clamp(numberSManSpawn, 0, 23);
        while (countSMan > 0)
        {
            SManController newSMan = SpawnNewSManToWaitingLine();
            countSMan--;
            StartCoroutine(MoveSManToPosStep(0, countSMan, newSMan));
            yield return new WaitForSeconds(0.1f);
        }
    }
    private SManController SpawnNewSManToWaitingLine()
    {
        if (amountSManSpawnOneTurn == 0)
        {
            ResetSpawn();
        }

        SManController newSMan = SpawnNewSManWithColor(currentSManColorID);

        amountSManSpawnOneTurn--;

        return newSMan;
    }
    private SManController SpawnNewSManWithColor(int colorID)
    {
        GameObject newSMan = ObjectPooling.Instance.CreateObject(sManPref);
        SManController sManController = newSMan.GetComponent<SManController>();

        sManController.SetColor(colorID);

        newSMan.transform.position = verticalSpawnStartPos.position;

        sManList.Add(sManController);

        DecreaseNumSManCommonColor(colorID);

        numberSManSpawn--;

        return sManController;
    }
    private void DecreaseNumSManCommonColor(int colorID)
    {
        numSManCommonColor[colorID]--;
        if (numSManCommonColor[colorID] == 0) numSManCommonColor.Remove(colorID);
    }
    private Vector3 GetMovePos(float numStep)
    {
        if (numStep <= 8)
        {
            return Vector3.Lerp(verticalSpawnStartPos.position, verticalSpawnEndPos.position, numStep / 8);
        }
        else
        {
            float newNumStep = numStep - 8;
            return Vector3.Lerp(horizontalSpawnStartPos.position, horizontalSpawnEndPos.position, newNumStep / 15);
        }
    }
    IEnumerator MoveSManToPosStep(int currentStep, int nextStep, SManController sManToMove)
    {
        while (currentStep <= nextStep)
        {
            yield return sManToMove.MoveToPos(GetMovePos(currentStep));
            sManToMove.Step = nextStep;
            currentStep++;
        }
    }
    public SManController GetFirtSMan()
    {
        if (sManList.Count == 0) return null;
        SManController firtSMan = sManList[0];
        return firtSMan;
    }
    public IEnumerator RemoveFirtSMan()
    {
        sManList.RemoveAt(0);

        List<Coroutine> coroutines = new List<Coroutine>();

        foreach (SManController sMan in sManList)
        {
            Coroutine coroutine = StartCoroutine(MoveSManToPosStep(sMan.Step, ++sMan.Step, sMan));
            coroutines.Add(coroutine);
        }

        foreach (Coroutine coroutine in coroutines)
        {
            yield return coroutine;
        }

        if (numSManCommonColor.Count != 0)
        {
            SpawnNewSManToWaitingLine();
        }
    }
    private int GetRandomKey(Dictionary<int, int> dictionary)
    {
        List<int> keys = new List<int>(dictionary.Keys);

        int randomIndex;
        if (keys.Count > 1)
        {
            do
            {
                randomIndex = Random.Range(0, keys.Count);
            } while (randomIndex == oldSManColorID);
            oldSManColorID = randomIndex;
        }
        else
        {
            randomIndex = 0;
        }
        return keys[randomIndex];
    }
    public Dictionary<int, int> GetNumSManFnishSpawn()
    {
        Dictionary<int, int> currentNumSManCommonColor = new();
        List<int> idIsUse = new();
        foreach (SManController sMan in sManList)
        {
            if (!idIsUse.Contains(sMan.ColorID))
            {
                idIsUse.Add(sMan.ColorID);
                currentNumSManCommonColor.Add(sMan.ColorID, 0);
            }
            currentNumSManCommonColor[sMan.ColorID]++;
        }
        return currentNumSManCommonColor;
    }
    public void ResetSpawn()
    {
        currentSManColorID = GetRandomKey(numSManCommonColor);
        amountSManSpawnOneTurn = Mathf.Clamp(numSManCommonColor[currentSManColorID], 0, Random.Range(1, 6));
    }
    public void ChangeColorSManWithBooster()
    {
        ReturnSManInQueue();
        StartCoroutine(ChangeColorSManInQueueWithItem());
    }
    private void ReturnSManInQueue()
    {
        foreach (SManController sMan in sManList)
        {
            if (!numSManCommonColor.ContainsKey(sMan.ColorID)) numSManCommonColor.Add(sMan.ColorID, 0);
            numSManCommonColor[sMan.ColorID]++;
        }
    }
    private IEnumerator ChangeColorSManInQueueWithItem()
    {
        yield return RandomSManColorInQueue();
        List<int> carColorIDInHolder = CarManager.Instance.GetCarColorInHolder();
        int countSManFinishChange = 0;
        if (carColorIDInHolder != null)
        {
            countSManFinishChange = ChangeSManColorInRange(countSManFinishChange, carColorIDInHolder);
        }
        List<int> carColorIDInParkingLot = new(numSManCommonColor.Keys);
        ChangeSManColorInRange(countSManFinishChange, carColorIDInParkingLot);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(CarManager.Instance.CheckSManColor());
    }
    private IEnumerator RandomSManColorInQueue()
    {
        GameController.Instance.PauseGame();
        for (int i = 0; i < 5; i++)
        {
            foreach (SManController sMan in sManList)
            {
                sMan.RandomColor();
            }
            yield return new WaitForSeconds(0.3f);
        }
        GameController.Instance.PlayGame();
    }
    private int ChangeSManColorInRange(int min, List<int> colorIDList)
    {
        if (min >= 23) return 0;
        int countSManFinishChange = min;
        if (colorIDList.Count == 0) return min;
        for (int i = 0; i < colorIDList.Count; i++)
        {
            int numSManPerColor = Mathf.Clamp(numSManCommonColor[colorIDList[i]], 0, 10);
            for (int j = countSManFinishChange; j < Mathf.Clamp(countSManFinishChange + numSManPerColor, 0, 23); j++)
            {
                sManList[j].SetColor(colorIDList[i]);
                DecreaseNumSManCommonColor(colorIDList[i]);
            }
            countSManFinishChange += numSManPerColor;
            if (countSManFinishChange >= 23)
            {
                return countSManFinishChange;
            }
        }
        return countSManFinishChange;
    }
    public int GetNumSManRemain()
    {
        return sManList.Count + numberSManSpawn;
    }
    public IEnumerator PutSManToVipCar(CarController carToPut)
    {
        List<SManController> sManIsPut = new();
        int countSManMove = 0;
        int countSManMoveOneSpan = 0;
        bool sManAvailbleColor = false;
        for (int i = 0; i < sManList.Count; i++)
        {
            if (sManList[i].ColorID == carToPut.ColorID && carToPut.NumOfSeats != countSManMove)
            {
                if (!sManAvailbleColor)
                {
                    countSManMoveOneSpan = 0;
                    sManAvailbleColor = true;
                }
                StartCoroutine(carToPut.PutSManToSeats(sManList[i]));

                countSManMoveOneSpan++;
                countSManMove++;

                sManIsPut.Add(sManList[i]);
            }
            else if (countSManMoveOneSpan != 0)
            {
                int nextStep = sManList[i].Step + countSManMoveOneSpan;
                StartCoroutine(MoveSManToPosStep(sManList[i].Step, nextStep, sManList[i]));
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = carToPut.CurrentSeatsIsUse; i < carToPut.NumOfSeats; i++)
        {
            SManController newSman = SpawnNewSManWithColor(carToPut.ColorID);
            sManMoveToCar.Add(newSman);
            sManIsPut.Add(newSman);
            StartCoroutine(carToPut.PutSManToSeats(newSman));
            yield return new WaitForSeconds(0.15f);
        }
        for (int i = countSManMove; i >= 0; i--)
        {
            SManController newSMan = SpawnNewSManToWaitingLine();
            StartCoroutine(MoveSManToPosStep(0, i, newSMan));
            yield return new WaitForSeconds(0.15f);
        }
        foreach (SManController sman in sManIsPut)
        {
            sManList.Remove(sman);
        }
        while (sManMoveToCar.Count != 0)
        {
            yield return null;
        }
        GameController.Instance.PlayGame();
        carVipHolder.SetActive(false);
    }
}
