using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyLevelPanel : MonoBehaviour
{
    [SerializeField] private GameObject chooseCountryPanel;
    [SerializeField] private GameObject leaderboardPanel;
    private void Start()
    {
        chooseCountryPanel.SetActive(!GameCache.GC.finishChooseCountry);
        leaderboardPanel.SetActive(GameCache.GC.finishChooseCountry);
    }
}
