using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunItem : MonoBehaviour
{
    [SerializeField] private Image gunIconImg;
    [SerializeField] private GameObject lockGp;
    [SerializeField] private TextMeshProUGUI gunName;

    private GunSheet gunSheet;
    public void Set(GunSheet gunSheet)
    {
        this.gunSheet = gunSheet;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (!gunSheet.IsUnlock) lockGp.SetActive(true);
        gunIconImg.sprite = gunSheet.gunIcon;
        if(gunName) gunName.text = gunSheet.gunName;
    }
    public void UnlockWithAds()
    {

    }
    public void SellectGun()
    {
        GunSceneController.Instance.LoadGun(gunSheet);
    }
}
