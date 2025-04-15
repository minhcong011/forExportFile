using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AndroidNativeCore;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunSceneController : SingletonBase<GunSceneController>
{
    [Header("Menu")]
    [SerializeField] private GunSO gunSO;
    [SerializeField] private GunType gunType;
    [SerializeField] private GameObject stunGunItemPref;
    [SerializeField] private GameObject machineGunItemPref;
    [SerializeField] private GameObject gunItemHolder;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject stunInGameUI;
    [SerializeField] private GameObject machineInGameUI;

    [Header("InGame")]
    [SerializeField] private GameObject stunGunHolder;
    [SerializeField] private GameObject machineGunHolder;
    [SerializeField] private Sprite[] bgSprites;
    [SerializeField] private Sprite bgSpritesDefault;
    [SerializeField] private Image bgImage;

    private Flash flash;

    private GameObject currentGun;
    private void Start()
    {
        Application.targetFrameRate = 60;
        gunType = GunStage.gunType;
        CreateGunItems();
    }
    private void CreateGunItems()
    {
        GunSheet[] gunSheets = null;
        switch (gunType)
        {
            case GunType.Stun:
                {
                    gunSheets = gunSO.stunGuns;
                    break;
                }
            case GunType.Machine:
                {
                    gunSheets = gunSO.machineGuns;
                    break;
                }
            case GunType.Special:
                {
                    gunSheets = gunSO.specialGuns;
                    break;
                }
        }
        foreach(GunSheet gunSheet in gunSheets)
        {
            CreateGunItem(gunSheet);
        }
    }
    private void CreateGunItem(GunSheet gunSheet)
    {
        GameObject gunItemPref = null;
        switch (gunType)
        {
            case GunType.Stun:
                {
                    gunItemPref = stunGunItemPref;
                    break;
                }
            case GunType.Machine: case GunType.Special:
                {
                    gunItemPref = machineGunItemPref;
                    break;
                }
        }
        GameObject newGunItem = Instantiate(gunItemPref);
        newGunItem.transform.SetParent(gunItemHolder.transform, false);
        newGunItem.GetComponent<GunItem>().Set(gunSheet);
    }
    public void ActiveShootStatus(bool active)
    {
        Debug.Log("Tap tap 1");
        if (active)
        {
            currentGun.GetComponent<IGunShoot>().ActiveShootStatus(true);
        }
        else
        {
            currentGun.GetComponent<IGunShoot>().ActiveShootStatus(false);
        }
    }
    public void LoadGun(GunSheet gunSheet)
    {
        menuUI.SetActive(false);
        GetInGameUI().SetActive(true);

        currentGun = Instantiate(gunSheet.gunPref);
        if(gunType == GunType.Stun)
        {
            StunGunInGame.Instance.currentStunGun = currentGun.GetComponent<StunGun>();
        }
        if (gunType == GunType.Machine || gunType == GunType.Special)
        {
            MachineGunInGame.Instance.currentGun = currentGun.GetComponent<MachineGun>();
        }

        bgImage.sprite = bgSprites[Random.Range(0, bgSprites.Length)];
        currentGun.transform.SetParent(GetGunHolder().transform, false);
    }
    private GameObject GetInGameUI()
    {
        switch (gunType)
        {
            case GunType.Stun: return stunInGameUI;
            case GunType.Machine: case GunType.Special: return machineInGameUI;
        }
        return null;
    }
    private GameObject GetGunHolder()
    {
        switch (gunType)
        {
            case GunType.Stun: return stunGunHolder;
            case GunType.Machine: case GunType.Special: return machineGunHolder;
        }
        return null;
    }
    public void DestroyCurrentGun()
    {
        bgImage.sprite = bgSpritesDefault;
        Destroy(currentGun);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
