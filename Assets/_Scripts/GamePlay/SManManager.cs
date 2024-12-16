using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SManManager : SingletonBase<SManManager>
{
    public SManWaitingLine nomalModeWaitingLine;
    [SerializeField] private SManWaitingLine[] dailyModeWaitingLines;
    [SerializeField] private GameObject dailyModeWaitingLineParent;
    [SerializeField] private GameObject carVipHolder;


    private int numSManRemain;
    public int test;
    public int NumSManRemain { get { return numSManRemain; } set { numSManRemain = value; } }

    public List<SManController> sManMoveToCar = new();

    [SerializeField] private GameMode gameMode;

    public Dictionary<CarController, int> seatsCanSpawnInCar = new();

    private int currentLineCheck = 0;

    private bool smanWhenStartFinishMove;
    public bool SmanWhenStartFinishMove { get { return smanWhenStartFinishMove; } set { smanWhenStartFinishMove = value; } }
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        StartCoroutine(WaitGetColor());
    }
    public void SetSManWithShufferItem()
    {
        nomalModeWaitingLine.ResetSMan();
    }
    public void ChangeSeatsCanSpawnInCar(CarController car, int amount)
    {
        //if (seatsCanSpawnInCar[car] == 0) ChangeRandomSeatsCanSpawnInCar(amount);
        seatsCanSpawnInCar[car] += amount;
    }
    public CarController GetCarCanSpawn(int colorID)
    {
        foreach (KeyValuePair<CarController, int> kvp in seatsCanSpawnInCar)
        {
            if (kvp.Value > 0 && kvp.Key.ColorID == colorID)
            {
                return kvp.Key;
            }
        }
        return null;
    }
    private void ChangeRandomSeatsCanSpawnInCar(int amount)
    {
        CarController carToChange = null;
        foreach (KeyValuePair<CarController, int> kvp in seatsCanSpawnInCar)
        {
            if (kvp.Value != 0)
            {
                carToChange = kvp.Key;
                break;
            }
        }
        seatsCanSpawnInCar[carToChange] += amount;
    }
    public void ResetSeatsCanSpawnInCar()
    {
        seatsCanSpawnInCar.Clear();
        foreach (CarController carController in CarManager.Instance.carControllers)
        {
            seatsCanSpawnInCar.Add(carController, carController.GetSeatsCanUse());
        }
    }
    public void BalanceSeatsCanSpawnWithSMan()
    {
        nomalModeWaitingLine.BalanceSManAfterUseBooster();
    }
    IEnumerator WaitGetColor()
    {
        while (!ColorManager.Instance.finishGetColor) yield return new WaitForSeconds(0.3f);
        gameMode = GameModeController.gameMode;
        ResetSeatsCanSpawnInCar();
        numSManRemain = ColorManager.Instance.GetMaxNumberSManSpawn();
        GamePlayUIManager.Instance.UpdateAmountSManRemainText();
        if (gameMode == GameMode.Nomal) nomalModeWaitingLine.gameObject.SetActive(true);
        else dailyModeWaitingLineParent.SetActive(true);
        StartCoroutine(SpawnSManWhenStart());
    }
    IEnumerator SpawnSManWhenStart()
    {
        if (gameMode == GameMode.Nomal)
        {
            StartCoroutine(nomalModeWaitingLine.SpawnSManWhenStart());
        }
        else
        {
            foreach (SManWaitingLine sManWaitingLine in dailyModeWaitingLines)
            {
                yield return sManWaitingLine.SpawnSManWhenStart();
            }
        }
    }
    public SManController GetFirtSMan()
    {
        return nomalModeWaitingLine.GetFirtSMan();
    }
    public SManController GetFirtSMan(int lineID)
    {
        return dailyModeWaitingLines[lineID].GetFirtSMan();
    }
    public IEnumerator RemoveFirtSMan()
    {
        yield return nomalModeWaitingLine.RemoveFirtSMan();
    }
    public IEnumerator RemoveFirtSMan(int lineID)
    {
        yield return dailyModeWaitingLines[lineID].RemoveFirtSMan();
    }
}
