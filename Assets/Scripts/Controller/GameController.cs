
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{

    public static GameController controller;
    private void Awake()
    {
        controller = this;
        //loading.transform.position = Vector3.zero;
    }
    //========================================

    public Transform startPos;

    public Transform endPos;

    public MotherShip motherShip;
    public float duration =1f;

    public UIController uiController;
    public SkillController skillController;
    public EnemiesController enemiesController;
    public ScoreController scoreController;
    public EmoteController emoteController;
    public ExplotionController explotionController;
    public Achievement achievement;
    public SoundController soundController;
    public SkinShip skinShip;
    public AdsController adsController;
    public ShopController shopController;

    public bool isGameover = false;

    public GameObject loading;
    
   

    private void Start()
    {
        soundController.PlaySound(soundEffect.start);
        adsController.ShowInterstitialAd();
    }

    public void PlaySound(soundEffect soundEffect)
    {
        soundController.PlaySound(soundEffect);
    }

    public void MotherShipTakeDame()
    {
        motherShip.TakeDame(50f);
    }
    public void StartGame()
    {
        motherShip.ResetHealth();
        skillController.UpdateText();
        UpdateHealthUI(1);
        motherShip.transform.position = startPos.position;

        enemiesController.ResetWave();
        scoreController.StartGame();
        isGameover = false; 

        motherShip.transform.DOMove(endPos.position, duration).OnComplete(()=> {
            soundController.PlaySound(soundEffect.gameplay);

            enemiesController.SpawnWave();
            skillController.StartDropSkill();
            motherShip.ReadyToShoot();
        }) ; 
    }
    public ISkill GetPlayerSkill()
    {
        return motherShip.GetComponent<ISkill>();
    }
    public void UpdateHealth()
    {
        skillController.UpdateHealth(true);
    }
    public void UpdateRocket()
    {
        skillController.UpdateRocket(true);
    }
    public void UpdateGatlingGun()
    {
        skillController.UpdateGatlingGun(true);
    }
    public void UpdateSkill(Skill skill , int index)
    {
        switch (skill)
        {
            case Skill.heal:
                skillController.UpdateHealth(index);
                break;
            case Skill.gatling:
                skillController.UpdateGatlingGun(index);
                break;
            case Skill.rocket:
                skillController.UpdateRocket(index);
                break;
        }
    }


    public void UpdateHealthUI(float percent)
    {
        uiController.HealFill(percent);
    }
    public void CreateEmote(Vector3 position)
    {
        emoteController.CreateEmote(position);
    }
    public void CreateExpolotion(Vector3 position)
    {
        explotionController.CreateExplosion(position);
    }

    public void GameoverEvent()
    {
        isGameover = true;

        adsController.ShowInterstitialAd();

        soundController.PlaySound(soundEffect.gameover);
        motherShip.DisablePath();
        scoreController.SetScoreGameoverPanel();
        enemiesController.DeleteAllShip();
        uiController.ShowGameoverPanel();
    }

    public void UpdateScore(int value)
    {
        scoreController.UpdateScore(value);
    }

    public void UpdateCombo()
    {
        scoreController.UpdateCombo();
    }

    public void DropCombo()
    {
        scoreController.DropCombo();
    }
    public void SetArchivement(int bossID, bool isPassed)
    {
        achievement.SetArchivement(bossID, isPassed);
    }

    public void MuteEffect(bool mute)
    {
        soundController.MuteEffect(mute);
    }
    public void MuteMusic(bool mute)
    {
        soundController.MuteMusic(mute);
    }
    public Sprite GetRandomShip()
    {
        return skinShip.GetRandomShip();
    }
    public Sprite GetRandomPirate()
    {
        return skinShip.GetRandomPirate();
    }

    public Sprite GetIndexPirate(int index)
    {
        return skinShip.GetIndexPirate(index);
    }

    public bool CheckMotherShipHeal()
    {
        return motherShip.CanHealth();
    }

    public RuntimeAnimatorController GetAnimatorController()
    {
         return enemiesController.GetAnimatorController();
    }

    public void ShowRewardAd()
    {
        adsController.ShowRewardedAd();
    }

    public int GetSkillCount(Skill skill)
    {
        switch (skill)
        {
            case Skill.heal:
               return skillController.GetHealthCount();
            case Skill.gatling:
                return skillController.GetGatlingCount();
            case Skill.rocket:
                return skillController.GetRocketCount();
             default:
                return 0;
        }
    }

    public void OpenPopupReward()
    {
        shopController.SetChooseItem(true);
        shopController.OpenPopupReward();
    }


    public void EnableLoadingPanel(bool enable)
    {
        loading.SetActive(enable);
    }
}
