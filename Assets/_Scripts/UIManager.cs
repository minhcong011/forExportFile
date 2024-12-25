
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField] private GameObject removeAdsBT;
    [SerializeField] private Slider currentXPSlider;
    [SerializeField] private Slider currentHpSlider;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    private void Start()
    {
        removeAdsBT.SetActive(!GameCache.GC.blockAds);
        UpdateXpSlider();
        UpdateHPSlider();
        UpdateLevelText();
    }
    public void UpdateHPSlider()
    {
        currentHpSlider.maxValue = GameCache.GC.maxHp;
        currentHpSlider.value = GameCache.GC.currentHp;
    }
    public void UpdateXpSlider()
    {
        currentXPSlider.maxValue = GameCache.GC.maxXP;
        currentXPSlider.value = GameCache.GC.currentXP;
    }
    public void UpdateLevelText()
    {
        currentLevelText.text = $"Level {GameCache.GC.currentLevel}";
    }
}
