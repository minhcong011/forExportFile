using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyEggBreakClampPanel : MonoBehaviour
{
    [SerializeField] private Image giftIconImage;
    [SerializeField] private Sprite[] giftIconSprites;

    public void SetGiftIcon(int giftID)
    {
        giftIconImage.sprite = giftIconSprites[giftID];
    }
}
