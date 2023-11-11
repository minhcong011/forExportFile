using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour
{
    [Header("Gameplay UI")]
    public Text gamePlayScoreTxt ;
    public Text comboTxt;
    public Transform comboFill;

    [Header("Gameover UI")]
    public Text gameoverScoreTxt;
    public Text bestScoreTxt;


    private int score = 0;
    private int hightScore = 0;
    private const string HSCORE_KEY = "HSCORE_KEY";

    public static ScoreController scoreController;

    private int comboCurrentValue = 0;
    private int maxCombo = 5;
    private int comboCount =1;
    private void Awake()
    {
        scoreController = this;
    }

    private void Start()
    {
        hightScore = PlayerPrefs.GetInt(HSCORE_KEY, 0); 
    }

    public void StartGame()
    {
        score = 0;
        comboCount = 1;

        gamePlayScoreTxt.text = score.ToString();
        comboTxt.text = "x" + comboCount;
        comboFill.localScale = new Vector3(0, 1);
    }

    public void UpdateScore(int value)
    {
        score += value;
        if(score > hightScore)
        {
            hightScore = score;
            PlayerPrefs.SetInt(HSCORE_KEY, hightScore);
        }

        gamePlayScoreTxt.text = score.ToString();
    }

    public void UpdateCombo()
    {
        comboCurrentValue++;
        if(comboCurrentValue == maxCombo)
        {
            comboCount++;
            comboTxt.text = "x" + comboCount;
            //reset
            comboCurrentValue = 0;
            comboFill.DOScaleX(0, 1f);
           
            return;
        }

        comboFill.DOScaleX((float)comboCurrentValue/ (float)maxCombo, 0.5f);
    }

    public void DropCombo()
    {
        comboCurrentValue = 0;
        comboFill.DOScaleX(0, 1f);
        comboCount = 1;

        comboTxt.text = "x" + comboCount;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHightScore()
    {
        return hightScore;
    }


    public void SetScoreGameoverPanel() {
        gameoverScoreTxt.text = score.ToString();
        bestScoreTxt.text = "Best score : " +  GetHightScore().ToString();
    }
}
