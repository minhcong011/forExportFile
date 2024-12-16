using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStreakChest : MonoBehaviour
{
    [SerializeField] private int chestID;
    [SerializeField] private int amountWinStreakToCollect;
    [SerializeField] private GiftInItemSheet[] giftBeCollects;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject openImage;
    private bool isCanClamp;

    public bool IsCanClamp { get => isCanClamp; set => isCanClamp = value; }
    public int AmountWinStreakToCollect { get => amountWinStreakToCollect;}

    public void ClampChest()
    {
        if (IsCanClamp)
        {
            WinStreakPanel.Instance.ClampChest(giftBeCollects);
            SetFinishClamp();
            GameCache.GC.finishCollectWinStreakChest[chestID] = true;
        }
    }
    public void SetFinishClamp()
    {
        lockImage.SetActive(false);
        openImage.SetActive(true);
    }
}
