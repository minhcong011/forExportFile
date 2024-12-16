using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml;
using UnityEngine.SceneManagement;
using System.Drawing;

public class CarManager : SingletonBase<CarManager>
{
    [SerializeField] private List<CarHolder> carHolders;
    [SerializeField] private Transform[] targetPos;
    [SerializeField] private Transform exitPos;
    [SerializeField] private Transform center;
    [SerializeField] private GameObject vipHolder;
    public GameObject[] tutorialCar;

    private List<GameObject> cars;
    public List<CarController> carControllers;

    private List<CarController> carInHolders = new();

    private bool isCheckSManColor;

    private int amountCarCollectPerLevel;

    private bool finishGetCar;

    public bool FinishGetCar { get { return finishGetCar; } }

    public Dictionary<GameObject, CarController> carControllderDictionary = new Dictionary<GameObject, CarController>();

    public List<CarController> carIsMove = new();
    private void Start()
    {
        cars = new(GameObject.FindGameObjectsWithTag("Car"));
        GetCarControllerComponent();
        SortCarListWithCenterDistance();
        SetCarHolderDailyMode();
    }
    private void SetCarHolderDailyMode()
    {
        if(GameModeController.gameMode == GameMode.Daily)
        {
            List<CarHolder> tmp = new (carHolders);
            foreach(CarHolder carHolder in tmp)
            {
                if (carHolder.isLook)
                {
                    carHolder.gameObject.SetActive(false);
                    carHolders.Remove(carHolder);
                }
            }
        }
    }
    public void UnlockHolderReceiveBooster()
    {
        foreach (CarHolder holder in carHolders)
        {
            if (holder.isLook)
            {
                holder.Unlock();
                break;
            }
        }
    }
    private void GetCarControllerComponent()
    {
        foreach(GameObject car in cars)
        {
            CarController carController = car.GetComponent<CarController>();
            carControllers.Add(carController);
            carControllderDictionary.Add(car, carController);
        }
    }
    private void SortCarListWithCenterDistance()
    {
        foreach (CarController carController in carControllers)
        {
            carController.GetNumCarForward();
        }
        carControllers.Sort((car1, car2) => car1.GetNumCarBlockToSort().CompareTo(car2.GetNumCarBlockToSort()));
        finishGetCar = true;
    }
    public CarController GetRandomCarHigherNumCarForward(int level)
    {
        return carControllers[Random.Range(0, Mathf.Clamp(level * 5, (level - 1) * 5, carControllers.Count))];
    }
    public int GetCarControllerCount()
    {
        return carControllers.Count;
    }
    public IEnumerator MoveCarToHolder(CarController car, int side)
    {
        switch (side)
        {
            case 1:
                {
                    yield return car.MoveToPos(targetPos[1].position);
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    yield return car.MoveToPos(targetPos[2].position);
                    break;
                }
            case 4:
                {
                    yield return car.MoveToPos(targetPos[3].position);
                    yield return car.MoveToPos(targetPos[2].position);
                    break;
                }
            case 5:
                {
                    yield return car.MoveToPos(targetPos[0].position);
                    yield return car.MoveToPos(targetPos[1].position);
                    break;
                }
        }
        yield return PutCarToHolder(car);
    }
    public CarHolder GetCarHolderAvailable()
    {
        foreach (CarHolder holder in carHolders)
        {
            if (!holder.IsChoose && !holder.isLook) return holder;
        }
        return null;
    }
    public IEnumerator PutCarToHolder(CarController car)
    {
        yield return car.MoveToPos(car.CurrentHolder.entrancePos.position);
        yield return car.MoveToPos(car.CurrentHolder.transform.position);
        car.OpenRoof();
        car.UpScale();
        if (car.IsCarInConveyorBetl) car.ConveyorBetlParent.RemoveCar(car.gameObject);
        car.CurrentHolder.IsHaveCar = true;
        carInHolders.Add(car);
        carIsMove.Remove(car);
        StartCoroutine(CheckSManColor());
    }
    public IEnumerator CheckSManColor()
    {
        if (isCheckSManColor)
        {
            yield return new WaitForSeconds(1f);
            if (isCheckSManColor) yield break;
        }
        isCheckSManColor = true;
        GamePlayUIManager.Instance.CanUseBooster = false;
        CarController carAvaible;
        int lineRemoveSMan = 0;
        while (!SManManager.Instance.SmanWhenStartFinishMove) yield return null;
        do
        {
            carAvaible = null;
            SManController firtSMan;
            if (GameModeController.gameMode == GameMode.Nomal)
            {
                firtSMan = SManManager.Instance.GetFirtSMan();
                if (firtSMan == null) yield break;
                foreach (CarController car in carInHolders)
                {
                    if (!car.GetSeatsIsFull() && firtSMan.ColorID == car.ColorID)
                    {
                        carAvaible = car;
                        break;
                    }
                }
                yield return RemoveSMan();
            }
            else
            {
                List<Coroutine> coroutinues = new();
                for(int i = 0; i < 3; i++)
                {
                    bool sManAvailble = false;
                    firtSMan = SManManager.Instance.GetFirtSMan(i);
                    if (firtSMan != null)
                    {
                        foreach (CarController car in carInHolders)
                        {
                            if (!car.GetSeatsIsFull() && firtSMan.ColorID == car.ColorID)
                            {
                                carAvaible = car;
                                lineRemoveSMan = i;
                                sManAvailble = true;
                                break;
                            }
                        }
                    }
                    if (sManAvailble)
                    {
                        Coroutine coroutine = StartCoroutine(RemoveSMan());
                        coroutinues.Add(coroutine);
                    }
                }
                foreach (Coroutine coroutine in coroutinues)
                {
                    yield return coroutine;
                }
            }
            IEnumerator RemoveSMan()
            {
                if (carAvaible != null)
                {
                    StartCoroutine(PutSManToCar(carAvaible, firtSMan));
                    if (GameModeController.gameMode == GameMode.Nomal) yield return SManManager.Instance.RemoveFirtSMan();
                    else yield return SManManager.Instance.RemoveFirtSMan(lineRemoveSMan);
                }
            }
        } while (carAvaible != null);
        isCheckSManColor = false;
        StartCoroutine(CheckLostGame());
        GamePlayUIManager.Instance.CanUseBooster = true;
    } 
    private IEnumerator PutSManToCar(CarController carToPut, SManController sManToPut)
    {
        if (carToPut.GetSeatsIsFull()) yield break ;
        yield return carToPut.PutSManToSeats(sManToPut);
    }
    public IEnumerator MoveCarToExitPos(CarController carToMove)
    {
        carToMove.CloseRoof();
        carInHolders.Remove(carToMove);
        cars.Remove(carToMove.gameObject);
        carControllers.Remove(carToMove);
        CarHolder carCurrentHolder = carToMove.CurrentHolder;
        yield return carToMove.MoveToPos(carCurrentHolder.entrancePos.position);
        yield return carToMove.MoveToPos(exitPos.position);
        carToMove.DisableSmokeEf();

        if(GameCache.GC.carCollectID == carToMove.CarId)
        {
            amountCarCollectPerLevel++;
        }

        carToMove.gameObject.SetActive(false);

        CheckWin();
    }
    private void CheckWin()
    {
        if(cars.Count == 0)
        {
            AudioManager.Instance.PlaySound("win");
            GameCache.GC.amountCarCollectIncrease += amountCarCollectPerLevel;
            if (GameModeController.gameMode == GameMode.Nomal)
            {
                GamePlayUIManager.Instance.ShowCarRewardPanel();
                GameCache.GC.countWinStreak++;
            }
            else if(GameModeController.gameMode == GameMode.Daily)
            {
                GameCache.GC.finishPlayDailyGame = true;
                GameCache.GC.finishGetStarDailyGame = true;
                DailyLevelDB.Instance.IncreaseUserCountryScore();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public bool CheckCarHolderNotAvailble()
    {
        foreach (CarHolder carHolder in carHolders)
        {
            if (!carHolder.IsChoose && !carHolder.isLook)
            {
                return false;
            }
        }
        return true;
    }
    private IEnumerator CheckLostGame()
    {
        while (SManManager.Instance.sManMoveToCar.Count > 0)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(0.5f);
        foreach (CarHolder carHolder in carHolders)
        {
            if(carHolder.gameObject.activeSelf)
            if (!carHolder.IsHaveCar && !carHolder.isLook)
            {
                yield break;
            }
        }
        Debug.Log("LostGame" + SManManager.Instance.sManMoveToCar.Count);
        if (GameModeController.gameMode == GameMode.Nomal) GamePlayUIManager.Instance.ShowBuyReceivedBoosterPanel();
        if (GameModeController.gameMode == GameMode.Daily)
        {
            GameCache.GC.finishPlayDailyGame = true;
            GamePlayUIManager.Instance.ShowBuyReceivePlayDailyGame();
        }
        GameController.Instance.LostGame();
       
    }
    public List<int> GetCarColorInHolder()
    {
        if (carInHolders.Count == 0) return null;
        List<int> carColor = new();
        foreach (CarController car in carInHolders)
        {
            if (!carColor.Contains(car.ColorID)) carColor.Add(car.ColorID);
        }
        return carColor;
    }
    public List<CarController> GetCarsInHolder()
    {
        if (carInHolders.Count == 0) return null;
        List<CarController> cars = new();
        foreach (CarController car in carInHolders)
        {
            cars.Add(car);
        }
        return cars;
    }
    public int GetSeatsCanUseInHolder(int colorID)
    {
        int count = 0;
        foreach(CarController car in carInHolders)
        {
            if (car.ColorID == colorID) count += car.GetSeatsCanUse();
        }
        if (carInHolders.Count == 0) count = 1;
        return count;
    }
    public int GetNumCarControllderInRange(int begin, int end, int colorID)
    {
        int count = 0;
        for(int i = begin; i < Mathf.Clamp(end, 0, carControllers.Count); i++)
        {
            if (carControllers[i].ColorID == colorID)
            {
                count++;
            }
        }
        return count;
    }
    public List<CarController> GetCarsInPakingLot()
    {
        List<CarController> cars = new();
        foreach(CarController car in carControllers)
        {
            if (!car.FinishMove) cars.Add(car);
        }
        return cars;
    }
    public void DeActiveVipHolder()
    {
        vipHolder.SetActive(false);
    }
}
