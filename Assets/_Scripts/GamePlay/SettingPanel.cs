using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private GameObject soundOnGp;
    [SerializeField] private GameObject soundOffGp;
    [SerializeField] private GameObject vibrateOnGp;
    [SerializeField] private GameObject vibrateOffGp;
    private void Start()
    {
        UpdateSoundSettingUI();
        UpdateVibrateSettingUI();
    }
    private void OnEnable()
    {
        GameController.Instance.PauseGame();
    }
    private void OnDisable()
    {
        GameController.Instance.PlayGame();
    }
    public void ChangeSoundSetting()
    {
        GameCache.GC.soundOff = !GameCache.GC.soundOff;
        AudioManager.Instance.PlayBGMusic(!GameCache.GC.soundOff);
        UpdateSoundSettingUI();
    }
    private void UpdateSoundSettingUI()
    {
        soundOnGp.SetActive(!GameCache.GC.soundOff);
        soundOffGp.SetActive(GameCache.GC.soundOff);
    }
    public void ChangeVibrateSetting()
    {
        GameCache.GC.vibrateOff = !GameCache.GC.vibrateOff;
        UpdateVibrateSettingUI();
    }
    private void UpdateVibrateSettingUI()
    {
        vibrateOnGp.SetActive(!GameCache.GC.vibrateOff);
        vibrateOffGp.SetActive(GameCache.GC.vibrateOff);
    }
    public void ExitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

