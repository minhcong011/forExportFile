using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManger : MonoBehaviour
{
    public GameObject MainPnl,InGamePnl,PausePnl,LevelSelectPnl,LossPanl;

    public GameObject[] Levels;

    public SpriteRenderer NowSprite,NextSprite;

    public Text ScoreText,HighScore,ScoreText1,HighScore1;

    public MergeText mergeText;

    public static UiManger Intence;
    int score;

    private void Awake() 
    {
        Intence = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ShowMain();
        ShowScore(0);

        HighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScore(int i)
    {
        score += i;
        ScoreText.text = score.ToString();
        ScoreText1.text = score.ToString();

        if(score > PlayerPrefs.GetInt("HighScore"))
        PlayerPrefs.SetInt("HighScore",score);
    
    }
    public void ShowMergeText(int i, Vector3 _postion)
    {
        mergeText.gameObject.SetActive(false);
        mergeText.gameObject.SetActive(true);
        mergeText.SetText(i,_postion);
    }











    public void ShowMain()
    {
        PanlOn(MainPnl);
    }

    public void ShowLevel()
    {
        LevelSelectPnl.SetActive(true);
    }


    public void ShowInGame(int i)
    {
        ItemDropController.Instance.lostGame = false;
        foreach (var item in Levels)
        {
            item.SetActive(false);
        }

        Levels[i].SetActive(true);

        PanlOn(InGamePnl);
        if (AdmobManager.instance.ShowInterstitialAd()) return;
        ItemDropController.Instance.PlayGame = true;
    }

    public void ShowPause()
    {
        PanlOn(PausePnl);
        ItemDropController.Instance.PlayGame = false;
    }
    public void UnPuse()
    {
        PanlOn(InGamePnl);
        ItemDropController.Instance.PlayGame = true;
    }
    
    public void UpdateNextItems(Sprite now,Sprite Next )
	{
		NowSprite.sprite = now;
		NextSprite.sprite = Next;
	}

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void ShowLoss()
    {
        AdmobManager.instance.ShowInterstitialAd();
        HighScore1.text = PlayerPrefs.GetInt("HighScore").ToString();

        PanlOn(LossPanl);
        ItemDropController.Instance.PlayGame = false;
        ItemDropController.Instance.lostGame = true;
    }






    void PanlOn(GameObject Pnl)
    {
        MainPnl.SetActive(false);
        InGamePnl.SetActive(false);
        LevelSelectPnl.SetActive(false);
        PausePnl.SetActive(false);
        LossPanl.SetActive(false);


        Pnl.SetActive(true);
    }
}
