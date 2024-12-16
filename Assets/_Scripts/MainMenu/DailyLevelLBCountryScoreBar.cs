using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DailyLevelLBCountryScoreBar : MonoBehaviour
{
    [SerializeField] private SpriteAtlas countryFlagAtlas;
    [SerializeField] private Image countryFlagImage;
    [SerializeField] private TextMeshProUGUI orderText;
    [SerializeField] private TextMeshProUGUI countryCodeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void Set(CountryScoreEntry countryScoreEntry)
    {
        countryFlagImage.gameObject.SetActive(true);
        Sprite[] sprites = new Sprite[countryFlagAtlas.spriteCount];
        countryFlagAtlas.GetSprites(sprites);
        Array.Sort(sprites, (a, b) => string.Compare(a.name, b.name));
        orderText.text = countryScoreEntry.order.ToString();
        countryCodeText.text = CountryInfo.countryCodes[countryScoreEntry.countryID];
        scoreText.text = countryScoreEntry.score.ToString();
        countryFlagImage.sprite = sprites[countryScoreEntry.countryID];
    }
}
