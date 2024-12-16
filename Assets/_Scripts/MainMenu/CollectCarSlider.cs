using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCarSlider : MonoBehaviour
{
    [SerializeField] private Slider carCollectSlider;
    [SerializeField] private TextMeshProUGUI currentAmountCarText;
    [SerializeField] private float speedIncreaseSlider;
    [SerializeField] private Image nextGiftIcon;
    private void Start()
    {
        SetSliderValueWhenStart();
        StartCoroutine(UpdateSlider());
    }
    private void SetSliderValueWhenStart()
    {
        SetNextGiftIcon();
        carCollectSlider.value = GameCache.GC.amountCarCollect;
        carCollectSlider.maxValue = GameCache.GC.amountCarToTakeGift;
        currentAmountCarText.text = $"{GameCache.GC.amountCarCollect} / {GameCache.GC.amountCarToTakeGift}";
    }
    private void SetNextGiftIcon()
    {
        Sprite icon;
        GiftSheet nextGiftSheet = CollectCarGiftManager.Instance.GetGiftSheet(GameCache.GC.giftLevel);
        if (nextGiftSheet.type == GiftSheet.GiftType.carX2)
        {
            icon = nextGiftSheet.icon[GameCache.GC.carCollectID];
        }
        else
        {
            icon = nextGiftSheet.icon[0];
        }
        nextGiftIcon.sprite = icon;
    }
    private IEnumerator UpdateSlider()
    {
        if (GameCache.GC.amountCarCollectIncrease == 0) yield break;
        int amountIncrease = GameCache.GC.amountCarCollectIncrease;
        int amountIncreaseOneTurn;
        bool needToChangeTurn;
        while (amountIncrease > 0)
        {
            if (amountIncrease > GameCache.GC.amountCarToTakeGift - GameCache.GC.amountCarCollect)
            {
                amountIncreaseOneTurn = GameCache.GC.amountCarToTakeGift - GameCache.GC.amountCarCollect;
                amountIncrease -= amountIncreaseOneTurn;
                needToChangeTurn = true;
            }
            else
            {
                amountIncreaseOneTurn = amountIncrease;
                amountIncrease = 0;
                needToChangeTurn = false;
            }

            carCollectSlider.maxValue = GameCache.GC.amountCarToTakeGift;
            carCollectSlider.value = GameCache.GC.amountCarCollect;
            GameCache.GC.amountCarCollect += amountIncreaseOneTurn;

            float currentSliderValue = carCollectSlider.value;
            float targetValue = GameCache.GC.amountCarCollect;
            float currentTime = 0;
            while (Mathf.Abs(carCollectSlider.value - targetValue) > 0.1f)
            {
                currentTime += Time.deltaTime;
                carCollectSlider.value = Mathf.Lerp(currentSliderValue, targetValue, currentTime/speedIncreaseSlider);
                currentAmountCarText.text = $"{Mathf.Round(carCollectSlider.value)} / {carCollectSlider.maxValue}";
                yield return null;
            }
            if (needToChangeTurn)
            {
                CollectGift();
                GameCache.GC.amountCarToTakeGift *= 2;
                GameCache.GC.amountCarCollect = 0;
                GameCache.GC.giftLevel++;
                SetNextGiftIcon();
            }
        }
        GameCache.GC.amountCarCollectIncrease = 0;
    }
    private void CollectGift()
    {
        GiftSheet giftToCollect = CollectCarGiftManager.Instance.GetGiftSheet(GameCache.GC.giftLevel);
        switch (giftToCollect.type)
        {
            case GiftSheet.GiftType.coin:
                {
                    GameCache.GC.coin += giftToCollect.amount;
                    break;
                }
            case GiftSheet.GiftType.sortBooster:
                {
                    GameCache.GC.sortBoosterAmount++;
                    break;
                }
            case GiftSheet.GiftType.vipBooster:
                {
                    GameCache.GC.vipSlotBoosterAmount++;
                    break;
                }
            case GiftSheet.GiftType.shuffleBooster:
                {
                    GameCache.GC.shuffleBoosterAmount++;
                    break;
                }
            case GiftSheet.GiftType.carX2:
                {
                    GameCache.GC.x2CollectCarBoosterTime += giftToCollect.amount * 60;
                    break;
                }
        }
    }
}
