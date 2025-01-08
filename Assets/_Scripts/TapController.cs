using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : SingletonBase<TapController>
{
    private bool isBlockTap;

    public bool IsBlockTap { set { isBlockTap = value; } }

    float countDownHold = 0;

    void Start()
    {
        StartCoroutine(CheckTap());
    }

    private IEnumerator CheckTap()
    {
        while (true)
        {
            if (!isBlockTap)
            {
                if (Input.touchCount > 0)
                {
                    Debug.Log("touch");
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit[] hits = Physics.RaycastAll(ray);

                        foreach (RaycastHit hit in hits)
                        {
                            if (hit.collider.gameObject.CompareTag("Meet"))
                            {
                                CombatManager.Instance.DamageMeet();
                                AdsManager.Instance.IncreaseCountTapToShowAds();
                                AudioManager.Instance.PlayTapSound();
                                if (GameCache.GC.currentWeaponType == WeaponType.Hand || GameCache.GC.currentWeaponType == WeaponType.Hammer)
                                {
                                    hit.collider.gameObject.GetComponentInChildren<Jellyfier>().TapPressure(hit.point);
                                }
                                if (GameCache.GC.currentWeaponType == WeaponType.Knife)
                                {
                                    CutMeshController.Instance.CreateBlace(hit.point + new Vector3(0, 2, 0));
                                }
                                if (GameCache.GC.currentWeaponType != WeaponType.Fire && GameCache.GC.currentWeaponType != WeaponType.Boom)
                                {
                                    EffectController.Instance.PlayHitEffect(hit.point + new Vector3(0, 1, 0));
                                }
                                Debug.Log("tapMeet");
                                break;
                            }
                        }
                    }

                    if (GameCache.GC.currentWeaponType == WeaponType.Fire || GameCache.GC.currentWeaponType == WeaponType.Boom)
                    {
                        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                        {
                            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                            RaycastHit[] hits = Physics.RaycastAll(ray);

                            foreach (RaycastHit hit in hits)
                            {
                                if (hit.collider.gameObject.CompareTag("Meet"))
                                {
                                    countDownHold += Time.deltaTime;
                                    if (countDownHold > 0.2f)
                                    {
                                        countDownHold = 0;
                                        CombatManager.Instance.DamageMeet();
                                    }
                                    EffectController.Instance.PlayHoldEff(hit.point + new Vector3(0, 1, 0));
                                }
                            }
                        }

                        if (touch.phase == TouchPhase.Ended)
                        {
                            EffectController.Instance.StopPlayHoldEff();
                            AudioManager.Instance.StopFireSound();
                        }
                    }
                }
            }
            yield return null;
        }
    }
}
