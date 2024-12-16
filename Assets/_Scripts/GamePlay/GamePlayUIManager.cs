using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUIManager : SingletonBase<GamePlayUIManager>
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI amountSManRemainText;
    [SerializeField] private TextMeshProUGUI shuffleBoosterRemainText;
    [SerializeField] private GameObject shufflePlusIcon;
    [SerializeField] private TextMeshProUGUI sortBoosterRemainText;
    [SerializeField] private GameObject sortPlusIcon;
    [SerializeField] private GameObject buyShuffleBoosterPanel;
    [SerializeField] private GameObject buySortBoosterPanel;
    [SerializeField] private GameObject buyReceiveBoosterPanel;
    [SerializeField] private GameObject buyReceivedPlayDailyGame;
    [SerializeField] private GameObject showCarRewardPanel;
    [SerializeField] private GameObject handTuturial;
    [SerializeField] private Vector3 handOffSet;
    [SerializeField] private GameObject[] nomalModeUI;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private GameObject[] tutorialTextContent;
    [SerializeField] private GameObject vipBoosterTutorialText;
    [SerializeField] private GameObject tapEFPref;
    private int countCarTutorial;

    private bool canUseBooster = true;
    public bool CanUseBooster { get { return canUseBooster; } set { canUseBooster = value; } }

    [SerializeField] private TMP_InputField testLevelInput;
    private void Start()
    {
        levelText.text = $"Level {GameCache.GC.currentLevel}";
        SetStartUI();
        UpdateTutorial();
        UpdateBooster();
    }
    public void LoadTestLevel()
    {
       int.TryParse(testLevelInput.text,out GameCache.GC.currentLevel);
        SceneManager.LoadScene("GamePlay");
    }
    public void ShowBuyReceivePlayDailyGame()
    {
        buyReceivedPlayDailyGame.SetActive(true);
    }
    private void SetStartUI()
    {
        if(GameModeController.gameMode != GameMode.Nomal)
        {
            foreach (GameObject ui in nomalModeUI) ui.SetActive(false);
        }
        if (GameCache.GC.currentLevel == 1)
        {
            for (int i = 1; i < nomalModeUI.Length; i++)
            {
                nomalModeUI[i].SetActive(false);
            }
            tutorialText.SetActive(true);
        }
    }
    public void UpdateTutorial()
    {
        if (GameCache.GC.currentLevel != 1) return;
        for(int i = 0; i < tutorialTextContent.Length; i++)
        {
            tutorialTextContent[i].SetActive(i == countCarTutorial);
        }
        if (countCarTutorial < 4)
        {
            if (!handTuturial.activeSelf) handTuturial.SetActive(true);
            handTuturial.transform.position = Camera.main.WorldToScreenPoint(CarManager.Instance.tutorialCar[countCarTutorial].transform.position + handOffSet);
            countCarTutorial++;
        }
        else
        {
            handTuturial.SetActive(false);
            tutorialText.SetActive(false);
        }
    }
    public void UpdateAmountSManRemainText()
    {
        //int amounSManRemain = SManManager.Instance.NumSManRemain;
        //amountSManRemainText.text = amounSManRemain.ToString();
    }
    public void ShowBuyReceivedBoosterPanel()
    {
        buyReceiveBoosterPanel.SetActive(true);
    }
    //1 shuffle
    //2 sort
    public void UseBooster(int boosterID)
    {
        if (SManManager.Instance.sManMoveToCar.Count > 0) return;
        if (CarManager.Instance.carIsMove.Count > 0) return;
        if (GameController.Instance.IsUseBooster) return;
        if (GameController.Instance.currentGameStage != GameStage.Playing) return;
        int boosterRemain = 0;
        switch (boosterID)
        {
            case 1:
                {
                    boosterRemain = GameCache.GC.shuffleBoosterAmount;
                    break;
                }
            case 2:
                {
                    boosterRemain = GameCache.GC.sortBoosterAmount; 
                    break;
                }
        }
        if (boosterRemain <= 0)
        {
            switch (boosterID)
            {
                case 1:
                    {
                        buyShuffleBoosterPanel.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        buySortBoosterPanel.SetActive(true);
                        break;
                    }
            }
        }
        else
        {
            BoosterManager.Instance.UseBooster(boosterID);
            UpdateBooster();
        }
    }
    private void UpdateBooster()
    {
        if (GameCache.GC.shuffleBoosterAmount > 0)
        {
            shuffleBoosterRemainText.text = GameCache.GC.shuffleBoosterAmount.ToString();
            shufflePlusIcon.SetActive(false);
        }
        else shufflePlusIcon.SetActive(true);
        if (GameCache.GC.sortBoosterAmount > 0)
        {
            sortBoosterRemainText.text = GameCache.GC.sortBoosterAmount.ToString();
            sortPlusIcon.SetActive(false);
        }
        else sortPlusIcon.SetActive(true);
    }
    public void ShowCarRewardPanel()
    {
        showCarRewardPanel.SetActive(true);
    }

    public void ShowVipTutorialText(bool isShow)
    {
        vipBoosterTutorialText.SetActive(isShow);
    }

    public void CreateTapEf(Vector3 tapPos)
    {
        GameObject newTapEF = ObjectPooling.Instance.CreateUIObject(tapEFPref);
        newTapEF.transform.position = tapPos;
    }
}
