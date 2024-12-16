using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DailyLevelLeaderBoard : MonoBehaviour
{
    [SerializeField] private SpriteAtlas flagAtlas;
    [SerializeField] private DailyLevelLBCountryScoreBar[] dailyLevelLBCountryScores;
    [SerializeField] private DailyLevelLBCountryScoreBar userCountryScoreBar;
    [SerializeField] private TextMeshProUGUI dailyLevelTimeRemainReset;
    [SerializeField] private Image lastChampionFlagIconImage;
    [SerializeField] private TextMeshProUGUI lastChamionCountryCodeText;
    [SerializeField] private GameObject starIcon;
    private void OnEnable()
    {
        StartCoroutine(SetDailyLevelTimeRemainText());
    }
    private void Start()
    {
        StartCoroutine(WaitingDBLoad());
        starIcon.SetActive(GameCache.GC.finishGetStarDailyGame);
    }
    IEnumerator SetDailyLevelTimeRemainText()
    {
        yield return AmericanTimeResetDB.GetTimeRemainString(currentAmericanTime =>
        {
            dailyLevelTimeRemainReset.text = currentAmericanTime;
        });
    }
    IEnumerator WaitingDBLoad()
    {
        while (!DailyLevelDB.Instance.finishLoadDB)
        {
            yield return new WaitForSeconds(0.5f);
        }
        SetLastChamion();
        StartCoroutine(SetLeaderboard());
    }
    private void SetLastChamion()
    {
        lastChamionCountryCodeText.text = CountryInfo.countryCodes[DailyLevelDB.Instance.ChampionID];
        lastChampionFlagIconImage.gameObject.SetActive(true);
        Sprite[] sprites = new Sprite[flagAtlas.spriteCount];
        flagAtlas.GetSprites(sprites);
        Array.Sort(sprites, (a, b) => string.Compare(a.name, b.name));
        lastChampionFlagIconImage.sprite = sprites[DailyLevelDB.Instance.ChampionID];
    }
    private IEnumerator SetLeaderboard()
    {
        userCountryScoreBar.Set(DailyLevelDB.Instance.GetUserCountryScore());
        for (int i = 0; i < DailyLevelDB.Instance.countryScoreEntries.Count; i++)
        {
            dailyLevelLBCountryScores[i].Set(DailyLevelDB.Instance.countryScoreEntries[i]);
            yield return null;
        }
    }
}
[Serializable]
public class CountryScoreEntry
{
    public int score;
    public int countryID;
    public int order;

    public CountryScoreEntry(int score, int countryID, int order)
    {
        this.score = score;
        this.countryID = countryID;
        this.order = order;
    }
}