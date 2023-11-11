using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TutorialController : MonoBehaviour
{
    public float movingShipDuration;
    public float duration;

    public float showPointXMotherShip;
    public float showPointXPirateShip;

    private Vector3 startPosMotherShip;
    private Vector3 startPosPrirateShip; 

    public Transform motherShip;
    public Transform pirateShip;

    public Text descriptionTxt1, descriptionTxt2;
    public Image curve;
    public GameObject mainMenuPanel;

    public float delay; 
    private void Start()
    {
        startPosMotherShip = motherShip.localPosition;
        startPosPrirateShip = pirateShip.localPosition;
    }
    private UIController uiController;
    public IEnumerator BacktoMainmenuEvent()
    {
        yield return new WaitForSeconds(delay);

        motherShip.DOLocalMoveY(motherShip.localPosition.y - 10, duration).OnComplete(()=> {
            motherShip.gameObject.SetActive(false);
        });
        pirateShip.DOLocalMoveY(motherShip.localPosition.y - 10, duration).OnComplete(()=> {
            pirateShip.gameObject.SetActive(false);
        });

        curve.DOFade(0, duration);
        descriptionTxt1.DOFade(0, duration);
        descriptionTxt2.DOFade(0, duration).OnComplete(()=> {
            uiController.TurnOffTutorialPanel();
        });

      
    }

    public void StartTutorial(UIController uIController)
    {
        this.uiController = uIController;
        motherShip.localPosition = startPosMotherShip;
        pirateShip.localPosition = startPosPrirateShip;
        motherShip.gameObject.SetActive(true);
        pirateShip.gameObject.SetActive(true);

        motherShip.DOLocalMoveX(showPointXMotherShip, movingShipDuration).OnComplete(() => {
            pirateShip.DOLocalMoveX(showPointXPirateShip, movingShipDuration).OnComplete(()=> {
                curve.DOFade(1, duration).OnComplete(()=> {
                    descriptionTxt1.DOFade(1, duration);
                    descriptionTxt2.DOFade(1, duration).OnComplete(()=> {
                        StartCoroutine(BacktoMainmenuEvent());
                    });
                });
            });
        });
    }
}
