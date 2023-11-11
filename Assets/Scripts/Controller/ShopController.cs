using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopController : MonoBehaviour
{
    public Button getItem;

    public GameObject popupReward;

    public Button gatling;
    public Button rocket;
    public Button heal;



    private bool canChoose = false;

    private void Awake()
    {
        getItem.onClick.AddListener(()=> {
            GameController.controller.PlaySound(soundEffect.click);
            GameController.controller.ShowRewardAd();
        });

        gatling.onClick.AddListener(() => {
            if (!canChoose) return;
            canChoose = false;
            rocket.gameObject.SetActive(false);
            heal.gameObject.SetActive(false);
            GameController.controller.UpdateSkill(Skill.gatling, 2);

            StartCoroutine(ClosePopup());
        });

        rocket.onClick.AddListener(()=> {
            if (!canChoose) return;
            canChoose = false;
            gatling.gameObject.SetActive(false);
            heal.gameObject.SetActive(false);
            GameController.controller.UpdateSkill(Skill.rocket, 2);

            StartCoroutine(ClosePopup());
        });

        heal.onClick.AddListener(()=> {
            if (!canChoose) return;
            canChoose = false;
            gatling.gameObject.SetActive(false);
            rocket.gameObject.SetActive(false);
            GameController.controller.UpdateSkill(Skill.heal, 2);

            StartCoroutine(ClosePopup());
        });
    }

    public void OpenPopupReward()
    {
        gatling.gameObject.SetActive(true);
        rocket.gameObject.SetActive(true);
        heal.gameObject.SetActive(true);


        popupReward.SetActive(true);
    }

    private IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(1f);
        popupReward.SetActive(false);
    }




    public void SetChooseItem(bool canChoose)
    {
        this.canChoose = canChoose;
    }

}
