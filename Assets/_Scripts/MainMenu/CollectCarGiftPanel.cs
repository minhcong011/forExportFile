using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCarGiftPanel : MonoBehaviour
{
    [SerializeField] private Slider giftTreeSlider;
    private void Start()
    {
        giftTreeSlider.maxValue = 20;
        giftTreeSlider.value = GameCache.GC.giftLevel;
    }
}
