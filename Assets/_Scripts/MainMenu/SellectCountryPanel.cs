using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellectCountryPanel : SingletonBase<SellectCountryPanel>
{
    [SerializeField] private ChooseCountryBt[] chooseCountryBts;
    [SerializeField] private TMP_InputField searchCountryInput;
    [SerializeField] private GameObject ConfirmBtDarkBg;
    [SerializeField] private GameObject leaderboardPanel;
    private int currentCountryIDSellect;
    private bool finishSellect;
    private void Start()
    {
        SetChooseCountryButton();
        searchCountryInput.onValueChanged.AddListener(OnInputChanged);
    }
    void OnInputChanged(string searchString)
    {

        for (int i = 0; i < CountryInfo.countryNames.Length; i++)
        {
            if (CountryInfo.countryNames[i].ToLower().Contains(searchString.ToLower()))
            {
                chooseCountryBts[i].gameObject.SetActive(true);
            }
            else
            {
                chooseCountryBts[i].gameObject.SetActive(false);
            }
        }
    }
    private void SetChooseCountryButton()
    {
        for(int i = 0; i < chooseCountryBts.Length; i++)
        {
            chooseCountryBts[i].Set(i, CountryInfo.countryNames[i]);
        }
    }
    public void SetCountryToSellect(int id)
    {
        chooseCountryBts[currentCountryIDSellect].DeSellect();
        currentCountryIDSellect = id;
        if (!finishSellect)
        {
            finishSellect = true;
            ConfirmBtDarkBg.SetActive(false);
        }
    }
    public void FinishSellectCountry()
    {
        if (!finishSellect) return;
        GameCache.GC.userCountryID = currentCountryIDSellect;
        GameCache.GC.finishChooseCountry = true;
        gameObject.SetActive(false);
        leaderboardPanel.SetActive(true);
    }
}
