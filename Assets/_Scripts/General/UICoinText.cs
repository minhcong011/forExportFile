using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoinText : SingletonBase<UICoinText>
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject iapPanel;
    private void Start()
    {
        UpdateCoinText();
    }
    public void UpdateCoinText()
    {
        coinText.text = GameCache.GC.coin.ToString();
    }
    public void ShowIAPPanel()
    {
        iapPanel.SetActive(true);
    }
}
