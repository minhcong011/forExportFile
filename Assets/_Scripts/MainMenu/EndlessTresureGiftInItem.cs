using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndlessTresureGiftInItem : MonoBehaviour
{
    [SerializeField] private Image giftIcon;
    [SerializeField] private Sprite coinIcon;
    [SerializeField] private Sprite shuffleIcon;
    [SerializeField] private Sprite sortIcon;
    [SerializeField] private Sprite vipSlotIcon;
    [SerializeField] private GiftInItemSheet giftSheet;
    [SerializeField] private TextMeshProUGUI amountText;
    public void Set(GiftInItemSheet giftSheet)
    {
        this.giftSheet = giftSheet;
        switch (giftSheet.type)
        {
            case GameItemType.Coin: giftIcon.sprite = coinIcon; break;
            case GameItemType.Shuffle: giftIcon.sprite = shuffleIcon; break;
            case GameItemType.Sort: giftIcon.sprite = sortIcon; break;
            case GameItemType.VipSlot: giftIcon.sprite = vipSlotIcon; break;    
        }
        amountText.text = giftSheet.amount.ToString();
    }
}
