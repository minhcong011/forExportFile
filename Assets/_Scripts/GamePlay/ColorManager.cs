using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class ColorManager : SingletonBase<ColorManager>
{
    [SerializeField] private List<CarColorSheet> carColorSheets;
    [SerializeField] private Transform center;
    private List<CarColorSheet> carColorSheetsTmp;
    public Dictionary<int, List<CarController>> carCommonColorDictionary = new();
    public Dictionary<int, int> numSManCommonColor = new();
    public Dictionary<int, Material> carColorBase = new();
    public Dictionary<int, CarColorSheet> carColor = new();

    private List<int> numOfColorID = new();

    public bool finishGetColor;
    private void Start()
    {
        carColorSheetsTmp = new(carColorSheets);
        StartCoroutine(InitColor());
    }
    IEnumerator InitColor()
    {
        while (!CarManager.Instance.FinishGetCar)
        {
            yield return null;
        }
        GetCarWithCommonColor();
        GetNumSManCommonColor();
        SetCarColor();
    }
    public void RemoveCarInList(CarController car)
    {
        carCommonColorDictionary[car.ColorID].Remove(car);
    }
    public void ResetColor()
    {
        GetCarWithCommonColor();
        GetNumSManCommonColor();
    }
    private void GetCarWithCommonColor()
    {
        carCommonColorDictionary.Clear();
        numOfColorID.Clear();
        foreach (CarController car in CarManager.Instance.carControllers)
        {
            int carColorID = car.ColorID;
            if (!numOfColorID.Contains(carColorID))
            {
                List<CarController> newCarList = new();
                carCommonColorDictionary.Add(carColorID, newCarList);
                numOfColorID.Add(carColorID);
            }
            carCommonColorDictionary[carColorID].Add(car);
        }
    }
    public void GetNumSManCommonColor()
    {
        numSManCommonColor.Clear();
        foreach (KeyValuePair<int, List<CarController>> kvp in carCommonColorDictionary)
        {
            if (!numSManCommonColor.ContainsKey(kvp.Key))
            {
                numSManCommonColor.Add(kvp.Key, 0);
            }
            GetNumSManPerColor(kvp.Value);
        }
        finishGetColor = true;
    }
    private void GetNumSManPerColor(List<CarController> carList)
    {
        foreach (CarController car in carList)
        {
            if (!numSManCommonColor.ContainsKey(car.ColorID))
            {
                numSManCommonColor.Add(car.ColorID, 0);
            }
            numSManCommonColor[car.ColorID] += car.GetSeatsCanUse();
        }
    }
    private void SetCarColor()
    {
        foreach (KeyValuePair<int, List<CarController>> kvp in carCommonColorDictionary)
        {
            CarColorSheet randColor = GetRandomColorNotDupticate();
            if (!carColorBase.ContainsKey(kvp.Key)) carColorBase.Add(kvp.Key, randColor.colorBase1);
            if (!carColor.ContainsKey(kvp.Key)) carColor.Add(kvp.Key, randColor);
            foreach (CarController car in kvp.Value)
            {
                car.SetColor(randColor);
            }
        }
    }
    public int GetMaxNumberSManSpawn()
    {
        int numSManSpawn = 0;
        foreach (KeyValuePair<int, int> kvp in numSManCommonColor)
        {
            numSManSpawn += kvp.Value;
        }
        return numSManSpawn;
    }
    private CarColorSheet GetRandomColorNotDupticate()
    {
        int randomIndex = Random.Range(0, carColorSheets.Count);
        CarColorSheet randomElement = carColorSheets[randomIndex];
        carColorSheets.RemoveAt(randomIndex);
        return randomElement;
    }
    public void RandomCarColorWithBooster()
    {
        //GameController.Instance.PauseGame();
        //StartCoroutine(RandomCarColorWithBoosterCoroutinue());
        //IEnumerator RandomCarColorWithBoosterCoroutinue()
        //{
        //    yield return RandomCarColorBeforeRefesh();
        //    List<int> keys = new(carCommonColorDictionary.Keys);
        //    List<int> keysIsUse = new();
        //    Dictionary<int, int> currentNumSManCommonColor = SManManager.Instance.nomalModeWaitingLine.GetNumSManFnishSpawn();
        //    Dictionary<int, CarController> numSManCommonColorTmp = new(carCommonColorDictionary);
        //    Debug.Log(numSManCommonColor);
        //    foreach (KeyValuePair<int, CarController> kvp in numSManCommonColorTmp)
        //    {
        //        if (keys.Count == 0 || keysIsUse.Contains(kvp.Key)) break;
        //        int randomKey = kvp.Key;
        //        List<int> keysTmp = new(keys);
        //        while (keysTmp.Count > 0)
        //        {
        //            randomKey = GetRandomKey(keysTmp);
        //            keysTmp.Remove(randomKey);
        //            if (currentNumSManCommonColor.ContainsKey(randomKey) && currentNumSManCommonColor.ContainsKey(kvp.Key))
        //                if (GetNumOfSeatsCarCommonColor(kvp.Key) > currentNumSManCommonColor[randomKey] && GetNumOfSeatsCarCommonColor(randomKey) > currentNumSManCommonColor[kvp.Key] && kvp.Key != randomKey)
        //                {
        //                    keys.Remove(randomKey);
        //                    keys.Remove(kvp.Key);
        //                    keysIsUse.Add(randomKey);
        //                    SwapKeys(carCommonColorDictionary, kvp.Key, randomKey);
        //                    break;
        //                }
        //        }
        //        StartCoroutine(RefreshCarColorAfterRandomWithAds());
        //    }
        //}
        GameController.Instance.PauseGame();
        StartCoroutine(RandomCarColorWithBoosterCoroutinue());
        IEnumerator RandomCarColorWithBoosterCoroutinue()
        {
            yield return RandomCarColorBeforeRefesh();
            try
            {
                List<int> keys = new(carCommonColorDictionary.Keys);
                List<int> keysIsUse = new();
                Dictionary<int, int> currentNumSManCommonColor = SManManager.Instance.nomalModeWaitingLine.GetNumSManFnishSpawn();
                List<int> test = new(carCommonColorDictionary.Keys);
                foreach (int key in test)
                {
                    if (keys.Count == 0 || keysIsUse.Contains(key)) break;
                    int randomKey = key;
                    List<int> keysTmp = new(keys);
                    while (keysTmp.Count > 0)
                    {
                        randomKey = GetRandomKey(keysTmp);
                        keysTmp.Remove(randomKey);
                        if (currentNumSManCommonColor.ContainsKey(randomKey) && currentNumSManCommonColor.ContainsKey(key))
                            if (GetNumOfSeatsCarCommonColor(key) > currentNumSManCommonColor[randomKey] && GetNumOfSeatsCarCommonColor(randomKey) > currentNumSManCommonColor[key] && key != randomKey)
                            {
                                keys.Remove(randomKey);
                                keys.Remove(key);
                                keysIsUse.Add(randomKey);
                                SwapKeys(carCommonColorDictionary, key, randomKey);
                                break;
                            }
                    }
                }
            }
            catch { }
            StartCoroutine(RefreshCarColorAfterRandomWithAds());
        }
    }
    private int GetNumOfSeatsCarCommonColor(int colorID)
    {
        int numOfSeats = 0;
        foreach (CarController carController in carCommonColorDictionary[colorID])
        {
            numOfSeats += carController.GetSeatsCanUse();
        }
        return numOfSeats;
    }
    private IEnumerator RefreshCarColorAfterRandomWithAds()
    {
        var carsListCopy = new Dictionary<int, List<CarController>>(carCommonColorDictionary);

        foreach (KeyValuePair<int, List<CarController>> kvp in carsListCopy)
        {
            foreach (CarController carController in kvp.Value)
            {
                if (!carController.FinishMove)
                {
                    carController.ColorID = kvp.Key;
                    carController.SetColor(carColor[kvp.Key]);
                }
            }
            yield return null;
        }
        ResetColor();
        SManManager.Instance.SetSManWithShufferItem();
        GameController.Instance.PlayGame();
        GameController.Instance.IsUseBooster = false;
    }
    private IEnumerator RandomCarColorBeforeRefesh()
    {
        for (int i = 0; i < 5; i++)
        {
            foreach (KeyValuePair<int, List<CarController>> kvp in carCommonColorDictionary)
            {
                foreach (CarController carController in kvp.Value)
                {
                    if (!carController.FinishMove) carController.RandomColor();
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    public CarColorSheet GetRandomCarColorSheet()
    {
        int rand = Random.Range(0, carColorSheetsTmp.Count);
        return carColorSheetsTmp[rand];
    }
    private int GetRandomKey(List<int> keys)
    {
        int rand = Random.Range(0, keys.Count);
        return keys[rand];
    }
    void SwapKeys<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key1, TKey key2)
    {
        if (dict.ContainsKey(key1) && dict.ContainsKey(key2))
        {
            TValue value1 = dict[key1];
            TValue value2 = dict[key2];

            dict.Remove(key1);
            dict.Remove(key2);

            dict[key1] = value2;
            dict[key2] = value1;
        }
    }
}
