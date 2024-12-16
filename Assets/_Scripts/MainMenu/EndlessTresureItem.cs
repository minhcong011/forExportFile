using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class EndlessTresureItem : MonoBehaviour
{
    [SerializeField] private bool free;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject giftGp;
    [SerializeField] private GameObject giftPref;
    [SerializeField] private GiftInItemSheet[] giftInItemSheets;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private CodelessIAPButton iapButton;
    [SerializeField] private Button clampBt;
    public GiftInItemSheet[] GiftInItemSheets { get { return giftInItemSheets; } }
    private void Start()
    {
        if (!free) iapButton.enabled = true;
        SetPriceText();
        SetGiftsGp();
    }
    public void Unlock()
    {
        lockIcon.SetActive(false);
        clampBt.interactable = true;
    }
    private void SetGiftsGp()
    {
        foreach(GiftInItemSheet gift in giftInItemSheets)
        {
            SpawnGift(gift);
        }
    }
    private void SpawnGift(GiftInItemSheet giftSheet)
    {
        GameObject newGift = Instantiate(giftPref);
        newGift.transform.SetParent(giftGp.transform, false);
        newGift.GetComponent<EndlessTresureGiftInItem>().Set(giftSheet);
    }
    private void SetPriceText()
    {
        if (free) priceText.text = "FREE";
        else priceText.text = "1.99$";
    }
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "endlesstreasure":
                {
                    ClampGift();
                    break;
                }
        }
    }
    public void ClampGift()
    {
        foreach (GiftInItemSheet gift in giftInItemSheets)
        {
            ClampGameItem.Clamp(gift.type, gift.amount);
        }
        EnglessTresurePanel.Instance.DeleteCurrentItemLevel();
    }
    public void ClampGiftWithFree()
    {
        if (!free) return;
        ClampGift();
    }
}
[Serializable]
public class GiftInItemSheet
{
    public GameItemType type;
    public int amount;
}
