using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinStreakClampPanel : MonoBehaviour
{
    private GiftInItemSheet[] giftsToClamp;
    [SerializeField] private GameObject[] giftUI;
    [SerializeField] private Sprite coinIcon;
    [SerializeField] private Sprite shufferIcon;
    [SerializeField] private Sprite sortIcon;
    [SerializeField] private Sprite vipIcon;
    public void Set(GiftInItemSheet[] giftsToClamp)
    {
        this.giftsToClamp = giftsToClamp;
        for(int i = 0; i < giftsToClamp.Length; i++)
        {
            Sprite giftIcon = null;
            switch (giftsToClamp[i].type)
            {
                case GameItemType.Coin: giftIcon = coinIcon; break;
                case GameItemType.Shuffle: giftIcon = shufferIcon; break;
                case GameItemType.Sort: giftIcon = sortIcon; break;
                case GameItemType.VipSlot: giftIcon = vipIcon; break;
            }
            giftUI[i].SetActive(true);
            giftUI[i].transform.GetChild(0).GetComponent<Image>().sprite = giftIcon;
            giftUI[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = giftsToClamp[i].amount.ToString();
        }
    }
    public void Clamp()
    {
        foreach(GiftInItemSheet gift in giftsToClamp)
        {
            ClampGameItem.Clamp(gift);
        }
    }
}
