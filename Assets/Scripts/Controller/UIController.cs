using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

public class UIController : MonoBehaviour
{
    public GameObject lr;
    public float radius;
    public float lineWidth;

    private float minWidth = 0;
    private float maxWidth = 45f;

    public float delay;
    //============================ Menu
    public float showPosY;
    public float hidePoxY;
    public float duration;

    [Header("Main Menu")]
    public Button startBtn;
    public Button skipBtn;
    public Button sharemainmenuBtn;
    public Button shopMainMenuBtn;

    public GameObject mainMenuPanel;

    [Header("Setting")]
    public Button settingBtn;
    public GameObject settingPanel;
    public Button settingBackBtn;

    [Header("Tutorial")]
    public Button tutorialBtn;
    public GameObject tutorialPanel;
    public TutorialController tutorialController;

    [Header("Intro")]
    public RectTransform introPlayer;
    public RectTransform introEnemy;
    public float scaleDuration;

    [Header("Main menu high score")]
    public Text mainmenuScore;

    [Header("Game Over")]
    public Transform gameoverPanel;
    public Button playAgainBtn;
    public Button tutorialOverBtn;
    public Button homeOverBtn;
    public Button settingOverBtn;
    public Button shareGameoverBtn;

    //============================

    //============================ Game play 
    private bool isStartGame = false;
    [Header("Game play")]
    public GameObject playerUI;
    public Image healthFill;

    [Header("shop")]
    public Transform shopPanel;
    public Button backShopBtn;

    private void GameplayUIShow(bool show)
    {
        playerUI.SetActive(show);
    }

    //============================
    private sceneEvent sceneEvent;
    private bool isClickedStartButton = false;
    private void Init()
    {
        shopMainMenuBtn.onClick.AddListener(() => {
            GameController.controller.PlaySound(soundEffect.click);
            mainMenuPanel.transform.DOLocalMoveY(hidePoxY, duration);
            shopPanel.transform.DOLocalMoveY(showPosY, duration);
        });

        backShopBtn.onClick.AddListener(()=> {
            GameController.controller.PlaySound(soundEffect.click);
            mainMenuPanel.transform.DOLocalMoveY(showPosY , duration);
            shopPanel.transform.DOLocalMoveY(hidePoxY, duration);
        });

        playAgainBtn.onClick.AddListener(()=> {
            if (isClickedStartButton) return;
            GameController.controller.PlaySound(soundEffect.click);

            isClickedStartButton = true;
            ZoomIn();
        });

        tutorialOverBtn.onClick.AddListener(() => {
            GameController.controller.PlaySound(soundEffect.click);
            sceneEvent = sceneEvent.gameovermenu; 
            gameoverPanel.transform.DOLocalMoveY(hidePoxY, duration);
            tutorialPanel.transform.DOLocalMoveY(showPosY, duration).OnComplete(() => {
                tutorialController.StartTutorial(this);
            });
        });

        homeOverBtn.onClick.AddListener(() => {
            GameController.controller.PlaySound(soundEffect.click);
            GameController.controller.PlaySound(soundEffect.start);
            GameController.controller.achievement.StartLoading();
            gameoverPanel.transform.DOLocalMoveY(hidePoxY, duration);
            mainMenuPanel.transform.DOLocalMoveY(showPosY, duration);
        });

        settingOverBtn.onClick.AddListener(() => {
            GameController.controller.PlaySound(soundEffect.click);
            sceneEvent = sceneEvent.gameovermenu;
            gameoverPanel.transform.DOLocalMoveY(hidePoxY, duration);
            settingPanel.transform.DOLocalMoveY(showPosY, duration);
        });

        startBtn.onClick.AddListener(() =>
        {
            if (isClickedStartButton) return;
            GameController.controller.PlaySound(soundEffect.click);
            isClickedStartButton = true;
            ZoomIn();
        });

        skipBtn.onClick.AddListener(() => {
            if (isStartGame) return;
            GameController.controller.PlaySound(soundEffect.click);
            isStartGame = true;
 
            skipBtn.gameObject.SetActive(false);

            playerUI.SetActive(true);
            GameplayUIShow(true);

            GameController.controller.StartGame();
            ZoomOut();
        });

        settingBtn.onClick.AddListener(()=> {
            GameController.controller.PlaySound(soundEffect.click);
            sceneEvent = sceneEvent.mainmenu;
            mainMenuPanel.transform.DOLocalMoveY(hidePoxY, duration);
            settingPanel.transform.DOLocalMoveY(showPosY, duration);
        });

        settingBackBtn.onClick.AddListener(()=> {
            GameController.controller.PlaySound(soundEffect.click);
            switch (sceneEvent)
            {
                case sceneEvent.mainmenu:
                    mainMenuPanel.transform.DOLocalMoveY(showPosY, duration);
                    break;
                case sceneEvent.gameovermenu:
                    gameoverPanel.transform.DOLocalMoveY(showPosY, duration);
                    break;
                default:
                    mainMenuPanel.transform.DOLocalMoveY(showPosY, duration);
                    break;
            }
            settingPanel.transform.DOLocalMoveY(hidePoxY, duration);
        });

        tutorialBtn.onClick.AddListener(() => {
            GameController.controller.PlaySound(soundEffect.click);
            sceneEvent = sceneEvent.mainmenu;
            mainMenuPanel.transform.DOLocalMoveY(hidePoxY, duration);
            tutorialPanel.transform.DOLocalMoveY(showPosY,duration).OnComplete(() =>{ tutorialController.StartTutorial(this);
            });
        });

        sharemainmenuBtn.onClick.AddListener(()=> {
            GameController.controller.PlaySound(soundEffect.click);
            Share();
        });
        shareGameoverBtn.onClick.AddListener(() => {
            GameController.controller.PlaySound(soundEffect.click);
            Share();
        });


    }

   public void TurnOffTutorialPanel()
    {
        tutorialPanel.transform.DOLocalMoveY(hidePoxY, duration);

        switch (sceneEvent)
        {
            case sceneEvent.mainmenu:
                mainMenuPanel.transform.DOLocalMoveY(showPosY, duration);
                break;
            case sceneEvent.gameovermenu:
                gameoverPanel.transform.DOLocalMoveY(showPosY, duration);
                break;
            default:
                mainMenuPanel.transform.DOLocalMoveY(showPosY, duration);
                break;
        }
    }

    private void Start()
    {
        Init();
        mainmenuScore.text = "Best score : "+ GameController.controller.scoreController.GetHightScore().ToString();
    }


    public void ZoomOut()
    {
        StartCoroutine(ChangeSizeOut());
    }
    public void ZoomIn()
    {
        StartCoroutine(ChangeSizeIn());
    }

    private IEnumerator ChangeSizeOut()
    {
        yield return new WaitForSeconds(delay);

        if (lineWidth <= minWidth)
        {
           
            yield break;
        }

        lineWidth -= 0.2f;

        DrawCircle(lr, radius, lineWidth);

        StartCoroutine(ChangeSizeOut());
    }

    private IEnumerator ChangeSizeIn()
    {
        yield return new WaitForSeconds(delay);

        if (lineWidth >= maxWidth)
        {
            mainMenuPanel.transform.DOLocalMoveY(hidePoxY, 1f);
            gameoverPanel.transform.DOLocalMoveY(hidePoxY, duration);

            skipBtn.gameObject.SetActive(true);
            introPlayer.DOScale(1, scaleDuration).OnComplete(() => introPlayer.DOScale(0, scaleDuration));
            yield return new WaitForSeconds(scaleDuration * 0.4f);
            introEnemy.DOScale(1, scaleDuration).OnComplete(() => introEnemy.DOScale(0, scaleDuration).OnComplete(()=> {
                if (isStartGame) return;
                isStartGame = true;
         
                skipBtn.gameObject.SetActive(false);
                playerUI.SetActive(true);
                GameplayUIShow(true);

                GameController.controller.StartGame();
                ZoomOut();
            }));
            yield break;
        }

        lineWidth += 0.2f;

        DrawCircle(lr, radius, lineWidth);

        StartCoroutine(ChangeSizeIn());
    }

    public void DrawCircle(GameObject container, float radius, float lineWidth)
    {
        var segments = 360;
        var line = container.GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }

    public void HealFill(float percent)
    {
        healthFill.DOFillAmount(percent,0.5f);
    }

    public void ShowGameoverPanel()
    {
        GameplayUIShow(false);
        gameoverPanel.DOLocalMoveY(0, 1f);
        ResetValue();
    }

    private void ResetValue()
    {
        isStartGame = false;
        isClickedStartButton = false;
    }

    private void Share()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        string text = GameController.controller.scoreController.GetHightScore() == 0 ? "Join game and defeat me !" : "My best score was " + GameController.controller.scoreController.GetHightScore().ToString();

        new NativeShare().AddFile(filePath)
            .SetSubject("Escape pirates ! ").SetText(text)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }
}

