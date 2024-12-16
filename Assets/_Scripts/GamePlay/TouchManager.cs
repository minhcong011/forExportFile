using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : SingletonBase<TouchManager>
{
    private bool vipBoosterTouch = false;
    private int countTutorialCar;

    private void Update()
    {
        CheckTouch();
    }
    public void SetVipBoosterTouch(bool vipBoosterTouch)
    {
        this.vipBoosterTouch = vipBoosterTouch;
    }
    private void CheckTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended) GamePlayUIManager.Instance.CreateTapEf(touch.position);
            if (GameController.Instance.currentGameStage != GameStage.Playing) return;
            if (GameController.Instance.IsUseBooster && !vipBoosterTouch) return;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit[] hits = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform != null)
                    {
                        HandleTouch(hit.transform);
                    }
                }
            }
        }
    }

    private void HandleTouch(Transform touchedTransform)
    {
        switch (touchedTransform.gameObject.tag)
        {
            case "Car":
                AudioManager.Instance.PlaySound("DragonPop");
                if (GameCache.GC.currentLevel != 1)
                {
                    if (vipBoosterTouch)
                    {
                        StartCoroutine(HeliVipBooster.Instance.PutCarToVipHolder(touchedTransform));
                        GamePlayUIManager.Instance.ShowVipTutorialText(false);
                        vipBoosterTouch = false;
                    }
                    else
                    {
                        CarController carController = CarManager.Instance.carControllderDictionary[touchedTransform.gameObject];
                        if (carController.IsCarInConveyorBetl)
                        {
                            carController.ConveyorBetlParent.StopMove = true;
                        }
                        carController.StartMove();
                    }
                }
                else
                {
                    if (touchedTransform.gameObject == CarManager.Instance.tutorialCar[countTutorialCar])
                    {
                        CarManager.Instance.carControllderDictionary[touchedTransform.gameObject].StartMove();
                        countTutorialCar++;
                        GamePlayUIManager.Instance.UpdateTutorial();
                    }
                }
                break;

            case "CarHolder":
                CarHolder carHolderIsTouch = touchedTransform.GetComponent<CarHolder>();
                if (carHolderIsTouch.isLook == true)
                    AdsManager.Instance.ShowRewardAdsUnlockCarHolder(carHolderIsTouch);
                break;

            case "Heli":
                if (!vipBoosterTouch)
                    HeliVipBooster.Instance.CheckBoosterAvailble();
                break;
        }
    }
}
