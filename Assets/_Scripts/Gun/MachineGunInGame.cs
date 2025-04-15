using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class MachineGunInGame : SingletonBase<MachineGunInGame>
{
    [SerializeField] private GameObject reloadPanel;
    [SerializeField] private TextMeshProUGUI bulletAmountText;
    private int currentBullet;
    private int maxBullet = 30;

    private bool canShoot;
    public bool CanShoot { get { return canShoot; } }
    public int CurrentBullet
    {
        get
        {
            return currentBullet;
        }
        set
        {
            currentBullet = value;
            bulletAmountText.text = value.ToString();
            currentGun.DecreaseBullet(value);
            if (currentBullet == 0)
            {
                canShoot = false;
                reloadPanel.SetActive(true);
            }
        }
    }
    public MachineGun currentGun;
    private void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
    }
    private void OnEnable()
    {
        canShoot = true;
        CurrentBullet = maxBullet;
    }

    public void Reload()
    {
        currentGun.Reload();
        canShoot = true;
        CurrentBullet = maxBullet;
    }
}
