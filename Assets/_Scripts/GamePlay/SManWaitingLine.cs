using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SManWaitingLine : MonoBehaviour
{
    [SerializeField] private GameObject sManPref;
    [Header("NomalMode")] 
    [SerializeField] private Transform horizontalSpawnStartPos;
    [SerializeField] private Transform horizontalSpawnEndPos;
    [Header("DailyMode")]
    [SerializeField] private Transform lineSpawnPosStart;
    [SerializeField] private Transform lineSpawnPosEnd;

    private const int maxNumSManInWaitingLine = 7;

    private Dictionary<int, int> numSManCommonColor = new();


    public List<SManController> SManFinishSpawnList = new();
    private List<SManController> SManInWaitingLine = new();
    private int amountSManSpawnOneTurn;
    private int currentSManColorID;
    private int oldSManID;

    private int oldSManColorID = 1000;

    private GameMode gameMode;

    List<SManController> sManStartList = new();

    private int currentSManCheck;

    private CarController currentCarToGetColor;

    private int countCarTutorial;

    private Dictionary<CarController, int> seatsCanSpawnInCarAlone;

    private float timeToNextSpawnEmoji;
    private void Start()
    {
        //StartCoroutine(SpawingEmoji());
    }
    public void ResetCarSpawnSManInWaitingLine()
    {
        foreach (SManController sman in SManInWaitingLine)
        {
            sman.carSpawn = null;
        }
    }
    public IEnumerator SpawnSManWhenStart()
    {
        seatsCanSpawnInCarAlone = SManManager.Instance.seatsCanSpawnInCar;
        gameMode = GameModeController.gameMode;
        numSManCommonColor = ColorManager.Instance.numSManCommonColor;
        int countSMan;
        int amountSManSpawn;
        if (gameMode == GameMode.Nomal)
        {
            amountSManSpawn = Mathf.Clamp(SManManager.Instance.NumSManRemain, 0, 7);
        }
        else
        {
            amountSManSpawn = Mathf.Clamp(SManManager.Instance.NumSManRemain, 0, 6);
        }
        countSMan = amountSManSpawn;
        while (countSMan > 0)
        {
            countSMan--;
            SManController newSMan = SpawnNewSManToWaitingLine();
            sManStartList.Add(newSMan);
            yield return null;
        }
        StartCoroutine(MoveSManWhenStart(sManStartList, amountSManSpawn));
    }
    IEnumerator MoveSManWhenStart(List<SManController> sManToMoveList, int amountSManSpawn)
    {
        List<Coroutine> moveCoroutinues = new();
        foreach (SManController sMan in sManToMoveList)
        {
            amountSManSpawn--;
            Coroutine moveCoroutinue = StartCoroutine(MoveSManToPosStep(0, amountSManSpawn, sMan));
            moveCoroutinues.Add(moveCoroutinue);
            yield return new WaitForSeconds(0.1f);
        }
        foreach (Coroutine coroutinue in moveCoroutinues)
        {
            yield return coroutinue;
        }
        SManManager.Instance.SmanWhenStartFinishMove = true;
    }
    public SManController GetFirtSMan()
    {
        if (currentSManCheck == SManFinishSpawnList.Count) return null;
        SManController firtSMan = SManFinishSpawnList[currentSManCheck];
        return firtSMan;
    }
    public IEnumerator RemoveFirtSMan()
    {
        SManInWaitingLine.RemoveAt(0);
        List<Coroutine> coroutines = new List<Coroutine>();
        currentSManCheck++;
        for (int i = currentSManCheck; i < SManFinishSpawnList.Count; i++)
        {
            Coroutine coroutine = StartCoroutine(MoveSManToPosStep(SManFinishSpawnList[i].Step, ++SManFinishSpawnList[i].Step, SManFinishSpawnList[i]));
            coroutines.Add(coroutine);
        }
        foreach (Coroutine coroutine in coroutines)
        {
            yield return coroutine;
        }
        if (numSManCommonColor.Count == 0 && amountSManSpawnOneTurn <= 0)
        {
            yield break;
        }
        SpawnNewSManToWaitingLine();
    }
    private SManController SpawnNewSManToWaitingLine()
    {
        if (amountSManSpawnOneTurn <= 0)
        {
            ResetSpawn();
        }
        if (amountSManSpawnOneTurn == 0) return null;
        SManController newSMan = SpawnNewSManWithColor(currentSManColorID);
        newSMan.carSpawn = currentCarToGetColor;
        amountSManSpawnOneTurn--;
        return newSMan;
    }
    public void ResetSpawn()
    {
        if (GameCache.GC.currentLevel != 1)
        {
            currentSManColorID = GetRandomKey();
            if (numSManCommonColor.ContainsKey(currentSManColorID))
            {
                int maxSpawn = Mathf.Clamp(seatsCanSpawnInCarAlone[currentCarToGetColor], 0, numSManCommonColor[currentSManColorID]);

                amountSManSpawnOneTurn = Mathf.Clamp(UnityEngine.Random.Range(2, 6), 0, maxSpawn);

                seatsCanSpawnInCarAlone[currentCarToGetColor] -= amountSManSpawnOneTurn;
            }
        }
        else if (countCarTutorial < 4)
        {
            CarController carToGetColor = CarManager.Instance.tutorialCar[countCarTutorial].GetComponent<CarController>();
            currentSManColorID = carToGetColor.ColorID;
            amountSManSpawnOneTurn = carToGetColor.GetSeatsCanSpawn();
            countCarTutorial++;
        }
    }
    private int GetRandomKey()
    {
        int randomID = 0;
        List<CarController> carControllers = new(CarManager.Instance.carControllers);
        while (true)
        {
            currentCarToGetColor = GetRandomCarHigherNumCarForward(carControllers);
            randomID = currentCarToGetColor.ColorID;
            if (numSManCommonColor.ContainsKey(randomID) && oldSManID != randomID)
            {
                if (seatsCanSpawnInCarAlone[currentCarToGetColor] > 0)
                {
                    break;
                }
            }
            if (carControllers.Count == 0) break;
        }
        if (carControllers.Count == 0)
        {
            foreach (KeyValuePair<CarController, int> kvp in seatsCanSpawnInCarAlone)
            {
                if (kvp.Value > 0)
                {
                    currentCarToGetColor = kvp.Key;
                    randomID = currentCarToGetColor.ColorID;
                    break;
                }
            }
        }
        oldSManID = randomID;
        return randomID;
    }
    private CarController GetRandomCarHigherNumCarForward(List<CarController> carControllers)
    {
        int randomID = UnityEngine.Random.Range(0, Mathf.Clamp(3, 0, carControllers.Count));
        CarController carRand = carControllers[randomID];
        carControllers.Remove(carRand);
        return carRand;
    }
    private SManController SpawnNewSManWithColor(int colorID)
    {
        GameObject newSMan = ObjectPooling.Instance.CreateObject(sManPref);
        SManController sManController = newSMan.GetComponent<SManController>();

        sManController.SetColor(colorID);
        if (gameMode == GameMode.Nomal) newSMan.transform.position = horizontalSpawnStartPos.position;
        else newSMan.transform.position = lineSpawnPosStart.position;

        SManFinishSpawnList.Add(sManController);
        SManInWaitingLine.Add(sManController);
        DecreaseNumSManCommonColor(colorID);

        return sManController;
    }
    private Vector3 GetMovePos(float numStep)
    {
        if (gameMode == GameMode.Nomal)
        {
                return Vector3.Lerp(horizontalSpawnStartPos.position, horizontalSpawnEndPos.position, numStep / 6);
        }
        else
        {
            return Vector3.Lerp(lineSpawnPosStart.position, lineSpawnPosEnd.position, numStep / 6);
        }
    }
    public IEnumerator MoveSManToPosStep(int currentStep, int nextStep, SManController sManToMove)
    {
        while (currentStep <= nextStep)
        {
            yield return sManToMove.MoveToPos(GetMovePos(currentStep));
            sManToMove.Step = nextStep;
            currentStep++;
        }
    }
    public Dictionary<int, int> GetNumSManFnishSpawn()
    {
        Dictionary<int, int> numSManInWaitingLine = new();
        foreach (SManController sMan in SManInWaitingLine)
        {
            if (!numSManInWaitingLine.ContainsKey(sMan.ColorID))
            {
                numSManInWaitingLine.Add(sMan.ColorID, 0);
            }
            numSManInWaitingLine[sMan.ColorID]++;
        }
        return numSManInWaitingLine;
    }
    public void ChangeColorSManWithBooster()
    {
        StartCoroutine(ChangeColorSManInQueueWithItem());
    }
    private void ReturnSManInQueue()
    {
        ReturnSManCanSpawnInQueueSpawn();

        foreach (SManController sMan in SManInWaitingLine)
        {
            if (!numSManCommonColor.ContainsKey(sMan.ColorID)) numSManCommonColor.Add(sMan.ColorID, 0);
            numSManCommonColor[sMan.ColorID]++;
        }
    }
    private void ReturnSManCanSpawnInQueueSpawn()
    {
        if (!numSManCommonColor.ContainsKey(currentSManColorID)) numSManCommonColor.Add(currentSManColorID, 0);
        numSManCommonColor[currentSManColorID] += amountSManSpawnOneTurn;
    }
    private IEnumerator ChangeColorSManInQueueWithItem()
    {
        yield return RandomSManColorInQueue();
        try
        {
            ChangeColorSManInQueue();
        }
        catch (Exception ex)
        {
            Debug.Log("Error1:" + ex.Message);
            StartCoroutine(ChangeColorSManInQueueWithItem());
            yield break;
        }
        yield return new WaitForSeconds(0.3f);
        GameController.Instance.PlayGame();
        GameController.Instance.IsUseBooster = false;
        try
        {
            ColorManager.Instance.ResetColor();
            ResetSMan();
        }
        catch
        {
            Debug.Log("Error2");
            StartCoroutine(ChangeColorSManInQueueWithItem());
            yield break;
        }
        StartCoroutine(CarManager.Instance.CheckSManColor());
    }
    private void ChangeColorSManInQueue()
    {
        Debug.Log("SmanLenght: " + SManInWaitingLine.Count);
        List<CarController> carInHolder = CarManager.Instance.GetCarsInHolder();
        int countSManFinishChange = 0;
        if (carInHolder != null)
        {
            Debug.Log("CarCount " + carInHolder.Count);
            countSManFinishChange = ChangeSManColorInRange(countSManFinishChange, carInHolder);
        }
        List<CarController> carInParkingLot = CarManager.Instance.GetCarsInPakingLot();
        while (countSManFinishChange < Mathf.Clamp(ColorManager.Instance.GetMaxNumberSManSpawn(), 0, maxNumSManInWaitingLine))
            countSManFinishChange = ChangeSManColorInRange(countSManFinishChange, carInParkingLot);
    }
    private IEnumerator RandomSManColorInQueue()
    {
        GameController.Instance.PauseGame();
        for (int i = 0; i < 5; i++)
        {
            try
            {
                foreach (SManController sMan in SManInWaitingLine)
                {
                    sMan.RandomColor();
                }
            }
            catch
            {
                Debug.Log("Error3");
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    private void ReturnColorIfError()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (SManController sMan in SManInWaitingLine)
            {
                sMan.ReturnColorIfError();
            }
        }
    }
    private int ChangeSManColorInRange(int min, List<CarController> carList)
    {
        int countSManFinishChange = min;
        if (carList.Count == 0) return min;
        for (int i = 0; i < carList.Count; i++)
        {
            int numSManPerColor = carList[i].GetSeatsCanUse();
            for (int j = countSManFinishChange; j < Mathf.Clamp(countSManFinishChange + numSManPerColor, 0, maxNumSManInWaitingLine); j++)
            {
                SManInWaitingLine[j].SetColor(carList[i].ColorID);
                SManInWaitingLine[j].carSpawn = carList[i];
                SManManager.Instance.ChangeSeatsCanSpawnInCar(carList[i], -1);
                //numSManCommonColor[carList[i].ColorID]--;
            }
            countSManFinishChange += numSManPerColor;
            if (countSManFinishChange >= maxNumSManInWaitingLine)
            {
                return countSManFinishChange;
            }
        }
        return countSManFinishChange;
    }
    public void DecreaseSeatsCanSpawnWithId(int colorID)
    {
        Dictionary<CarController, int> tmp = new(seatsCanSpawnInCarAlone);
        foreach (KeyValuePair<CarController, int> kvp in tmp)
        {
            if (kvp.Key.ColorID == colorID && kvp.Value > 0)
            {
                seatsCanSpawnInCarAlone[kvp.Key]--;
                break;
            }
        }
    }
    public void BalanceSManAfterUseBooster()
    {
        foreach (SManController sman in SManInWaitingLine)
        {
            DecreaseSeatsCanSpawnWithId(sman.colorID);
            DecreaseNumSManCommonColor(sman.colorID);
        }
    }
    private void DecreaseNumSManCommonColor(int colorID)
    {
        if (!numSManCommonColor.ContainsKey(colorID))
        {
            amountSManSpawnOneTurn = 0;
            return;
        }
        numSManCommonColor[colorID]--;
        if (numSManCommonColor[colorID] == 0) numSManCommonColor.Remove(colorID);
    }
    public IEnumerator PutSManToVipCar(CarController carToPut)
    {
        List<SManController> sManIsPut = new();
        int countSManMove = 0;
        for (int i = 0; i < SManInWaitingLine.Count; i++)
        {
            if (SManInWaitingLine[i].ColorID == carToPut.ColorID && carToPut.GetSeatsCanUse() != 0)
            {
                StartCoroutine(carToPut.PutSManToSeats(SManInWaitingLine[i]));

                countSManMove++;

                sManIsPut.Add(SManInWaitingLine[i]);
            }
            else if (countSManMove != 0)
            {
                int nextStep = SManInWaitingLine[i].Step + countSManMove;
                StartCoroutine(MoveSManToPosStep(SManInWaitingLine[i].Step, nextStep, SManInWaitingLine[i]));
            }
            yield return null;
        }
        for (int i = carToPut.GetSeatsCanUse(); i > 0; i--)
        {
            SManController newSman = SpawnNewSManWithColor(carToPut.ColorID);
            sManIsPut.Add(newSman);
            StartCoroutine(carToPut.PutSManToSeats(newSman));
            yield return new WaitForSeconds(0.15f);
        }
        foreach (SManController sman in sManIsPut)
        {
            SManFinishSpawnList.Remove(sman);
            SManInWaitingLine.Remove(sman);
        }
        while (SManManager.Instance.sManMoveToCar.Count != 0) yield return null;
        yield return new WaitForSeconds(0.5f);
        ColorManager.Instance.ResetColor();
        ResetSMan();
        for (int i = countSManMove; i > 0; i--)
        {
            SManController newSMan = SpawnNewSManToWaitingLine();
            StartCoroutine(MoveSManToPosStep(0, i - 1, newSMan));
            yield return new WaitForSeconds(0.15f);
        }
        TouchManager.Instance.SetVipBoosterTouch(false);
        StartCoroutine(CarManager.Instance.CheckSManColor());
        GameController.Instance.IsUseBooster = false;
        CarManager.Instance.DeActiveVipHolder();
        GameController.Instance.PlayGame();
    }
    public void ResetSMan()
    {
        SManManager.Instance.ResetSeatsCanSpawnInCar();
        foreach (SManController sman in SManInWaitingLine)
        {
            CarController carTmp = SManManager.Instance.GetCarCanSpawn(sman.colorID);
            sman.carSpawn = carTmp;
            numSManCommonColor[sman.colorID]--;
            seatsCanSpawnInCarAlone[carTmp]--;
        }
        amountSManSpawnOneTurn = 0;
    }
    public int GetNumSManInWaitingLine()
    {
        return SManFinishSpawnList.Count - currentSManCheck;
    }
    public void ReturnSManInWaitingLine()
    {
        foreach (SManController sman in SManInWaitingLine)
        {
            if (!numSManCommonColor.ContainsKey(sman.colorID)) numSManCommonColor.Add(sman.colorID, 0);
            numSManCommonColor[sman.colorID]++;
        }
    }
    public IEnumerator SpawingEmoji()
    {
        RandomNextTimeSpawnEmoji();
        while (GameController.Instance.currentGameStage != GameStage.LostGame)
        {
            timeToNextSpawnEmoji -= Time.deltaTime;
            if(timeToNextSpawnEmoji < 0)
            {
                SpawnEmojiWithRandomSman();
                RandomNextTimeSpawnEmoji();
            }
            yield return null;
        }
    }
    private void RandomNextTimeSpawnEmoji()
    {
        timeToNextSpawnEmoji = UnityEngine.Random.Range(5, 8);
    }
    private void SpawnEmojiWithRandomSman()
    {
        SManController smanToSpawn = SManInWaitingLine[UnityEngine.Random.Range(0, SManInWaitingLine.Count)];
        smanToSpawn.SpawnEmojiOnHead();
    }
}
