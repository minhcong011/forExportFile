using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCarGift : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI giftTitle;
    [SerializeField] private Image giftIcon;
    [SerializeField] private TextMeshProUGUI giftLevel;
    [SerializeField] private GameObject finishCollectIcon;
    public void Set(int giftLevel,string title, Sprite icon)
    {
        if (giftLevel <= GameCache.GC.giftLevel) finishCollectIcon.SetActive(true);
        else
        {
            this.giftLevel.text = giftLevel.ToString();
        }
        giftTitle.text = title;
        giftIcon.sprite = icon;
    }
}
