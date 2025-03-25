using UnityEngine;
using UnityEngine.UI;

public class BoosterButton : MonoBehaviour
{
    [SerializeField] private Text boosterAmountText;
    [SerializeField] private BoosterType boosterType;
    [SerializeField] private GameObject buyBoosterPanel;
    [SerializeField] private GameObject plusIcon;
    private void Start()
    {
        UpdateBoosterAmountText();
    }
    private int GetBoosterAmount()
    {
        switch (boosterType)
        {
            case BoosterType.AddTime:
                {
                    return GameCache.GC.amountAddTimeBooster;
                }
            case BoosterType.BreakMoveable:
                {
                    return GameCache.GC.amountBreakBooster;
                }
            case BoosterType.Find:
                {
                    return GameCache.GC.amountFindBooster;
                }
        }
        return 0;
    }
    private void DecreaseBoosterAmount()
    {
        switch (boosterType)
        {
            case BoosterType.AddTime:
                {
                    GameCache.GC.amountAddTimeBooster--;
                    break;
                }
            case BoosterType.BreakMoveable:
                {
                    GameCache.GC.amountBreakBooster--;
                    break;
                }
            case BoosterType.Find:
                {
                    GameCache.GC.amountFindBooster--;
                    break;
                }
        }
        UpdateBoosterAmountText();
    }
    public void UpdateBoosterAmountText()
    {
        if (boosterType == BoosterType.WatchAdsCoin) return;
        if (GetBoosterAmount() == 0)
        {
            boosterAmountText.gameObject.SetActive(false);
            plusIcon.SetActive(true);
        }
        else
        {
            boosterAmountText.gameObject.SetActive(true);
            plusIcon.SetActive(false);
            boosterAmountText.text = GetBoosterAmount().ToString();
        }
    }
    public void UseBooster()
    {
        if(boosterType == BoosterType.WatchAdsCoin)
        {
            ShowAdsCoin();
            return;
        }
        if(GetBoosterAmount() == 0)
        {
            buyBoosterPanel.SetActive(true);
            return;
        }
        DecreaseBoosterAmount();
        BoosterManager.Instance.UseBooster(boosterType);
    }
    private void ShowAdsCoin()
    {
        AdsManager.Instance.ShowRewardedAd(RewardAdsType.AddCoin);
    }

}
