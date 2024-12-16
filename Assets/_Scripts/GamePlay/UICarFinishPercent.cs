using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UICarFinishPercent : SingletonBase<UICarFinishPercent>
{
    [SerializeField] private SpriteAtlas CarIconAtlas;
    [SerializeField] private SpriteAtlas CarIconDisableAtlas;
    [SerializeField] private Image CarIconImage;
    [SerializeField] private Image CarIconDisableImage;
    [SerializeField] private Slider carSlider;
    [SerializeField] private TextMeshProUGUI carFinishPercentText;

    private float maxSManCanSpawn;
    private void Start()
    {
        StartCoroutine(WaitGetColor());
        carSlider.maxValue = 100;
        SetCarIcon();
    }
    IEnumerator WaitGetColor()
    {
        while (!ColorManager.Instance.finishGetColor) yield return null;
        maxSManCanSpawn = ColorManager.Instance.GetMaxNumberSManSpawn();
    }
    private void SetCarIcon()
    {
        CarIconImage.sprite = CarIconAtlas.GetSprite($"{GameCache.GC.currentLevel}");
        CarIconDisableImage.sprite = CarIconDisableAtlas.GetSprite($"{GameCache.GC.currentLevel}");
    }
    public void UpdateSlider()
    {
        float numSManRemain = SManManager.Instance.NumSManRemain;
        float percentFinish = (maxSManCanSpawn - numSManRemain) / maxSManCanSpawn * 100;
        carSlider.value = percentFinish;
        carFinishPercentText.text = $"{(int)percentFinish}%";
    }
}
