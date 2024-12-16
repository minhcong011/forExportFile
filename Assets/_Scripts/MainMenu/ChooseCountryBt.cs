using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseCountryBt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countryNameText;
    [SerializeField] private GameObject unSellectBg;
    private int id;

    public void Set(int id, string countryName)
    {
        this.id = id;
        countryNameText.text = countryName;
    }
    public void Sellect()
    {
        SellectCountryPanel.Instance.SetCountryToSellect(id);
        unSellectBg.SetActive(false);
    }
    public void DeSellect()
    {
        unSellectBg.SetActive(true);
    }
}
