using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
public class StunGunInGame : SingletonBase<StunGunInGame>
{
    [SerializeField] private GameObject chargerEneryPanel;
    [SerializeField] private Slider enerySlider;

    private float maxEnery = 10;
    private float currentEnery;

    private bool isShoot;
    public bool IsShoot
    {
        set
        {
            isShoot = value;
        }
    }
    public StunGun currentStunGun;
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
        StartCoroutine(DecreaseEneryCoroutinue());
        Recharger();
    }
    public void Recharger()
    {
        currentEnery = maxEnery;
        enerySlider.maxValue = maxEnery;
        enerySlider.value = maxEnery;
    }
    IEnumerator DecreaseEneryCoroutinue()
    {
        while (true)
        {
            if (isShoot)
            {
                currentEnery -= Time.deltaTime;
                enerySlider.value = currentEnery;
                if (currentEnery <= 0)
                {
                    chargerEneryPanel.SetActive(true);
                    currentStunGun.ActiveShootStatus(false);
                }
            }
            yield return null;
        }
    }
}
