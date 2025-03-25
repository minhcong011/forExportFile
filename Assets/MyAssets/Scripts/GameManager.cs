using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using DG.Tweening.Core.Easing;

public class GameManager : SingletonBase<GameManager>
{
    public int timePerMoveable;
    public Slider timeSlider;
    public float levelTime;
    public GameObject retryPanel, winPanel, startPanel, commingSoonPanel;
    public Text levelText, moveLimitText, levelTimeText;
    private bool isWin, isRetry;
    public int levelNo;
    public int skipLevelCoinAmount;
    public Animator toastAnim;
    public Gradient red, pink, blue, green, yellow;
    public int moveLimitValue;
    [HideInInspector] public bool isDragging;
    public GameObject moveLimitTextObj;


    private float currentTime;

    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        levelNo = PlayerPrefs.GetInt("Level", 1);
        levelText.text = "LEVEL " + levelNo;
        if (levelNo < 11)
            moveLimitTextObj.SetActive(false);
    }
    public void AddTime(int amount)
    {
        levelTime += amount;
    }
    public void SetLevelTime(int countMoveable)
    {
        int time = timePerMoveable * countMoveable;
        levelTime = Mathf.CeilToInt(time / 10f) * 10;
        timeSlider.maxValue = levelTime;
        timeSlider.value = levelTime;
    }
    private void Start()
    {
        FindFirstObjectByType<LevelLoader>().LoadLevel(levelNo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Next();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetInt("Level", levelNo - 1);
            SceneManager.LoadScene(1);
        }
    }

    public void Retry()
    {
        if (isWin)
            return;

        isRetry = true;
        Debug.Log("falskdjfaksjdflasjdlf");
        Invoke(nameof(InvokeRetryGame), 0.5f);
    }

    private void InvokeRetryGame()
    {
        AudioManager.Instance.Play("Retry");
        retryPanel.SetActive(true);
    }

    public void Reload()
    {
        PlayClickSound();
        AdsManager.Instance.ShowInterstitialAd();
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        PlayClickSound();
        StartCoroutine(LevelTimeCountDown());
        startPanel.SetActive(false);
    }

    public void Win()
    {
        if (isRetry)
            return;

        isWin = true;
        Invoke(nameof(InvokeWinGame), 1);
    }

    private void InvokeWinGame()
    {
        AudioManager.Instance.Play("Win");

        if (SceneManager.GetActiveScene().buildIndex < 225)
        {
            winPanel.SetActive(true);
            //winPanel.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), .5f);
        }
        else
        {
            commingSoonPanel.SetActive(true);
            commingSoonPanel.transform.DOScale(new Vector3(1, 1, 1), .5f);
        }
    }
    public void Revice()
    {
        levelTime += 15;
        SetMoveLimit(moveLimitValue + 5);
        StartCoroutine(LevelTimeCountDown());
        retryPanel.SetActive(false);
        isRetry = false;
    }
    public void Next()
    {
        PlayClickSound();
        AdsManager.Instance.ShowInterstitialAd();

        PlayerPrefs.SetInt("Level", levelNo + 1);
        SceneManager.LoadScene(1);
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void ShowRewardVideo()
    {
        AdsManager.Instance.ShowRewardedAd(RewardAdsType.AddCoin);
    }

    public void SkipLevel()
    {
        PlayClickSound();
        AdsManager.Instance.ShowInterstitialAd();

        CoinsManager coinsManager = FindFirstObjectByType<CoinsManager>();

        if (coinsManager.collectedCoins >= skipLevelCoinAmount)
        {
            coinsManager.LessCoins(skipLevelCoinAmount);
            Next();
            AudioManager.Instance.Play("Reward");
        }
        else
        {
            toastAnim.SetTrigger("Toast");
        }
    }

    public void DecreseMoveLimitText()
    {
        if(moveLimitValue == 0)
            return;

        moveLimitValue--;
        moveLimitText.text = "Moves " + moveLimitValue;

        if (moveLimitValue == 0)
        {
            if(FindObjectsByType<MovableItem>(FindObjectsSortMode.None).Length == 1)
                Invoke(nameof(Retry), 2);
            else
            {
                Retry();
            }
        }
    }

    public void SetMoveLimit(int moveLimit)
    {
        moveLimitValue = moveLimit;
        moveLimitText.text = "Moves " + moveLimit;
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.Play("Click");
    }

    IEnumerator LevelTimeCountDown()
    {
        while(levelTime > 0)
        {
            timeSlider.value = levelTime;
            levelTime -= Time.deltaTime;
            yield return null;
        }

        Retry();
    }
    public void CheckWin()
    {
        if (FindFirstObjectByType<MovableItem>())
            Debug.Log("NotYet");
        else
            Win();
    }
}

