using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingController : MonoBehaviour
{
    private const string SOUNDS_KEY = "soundskey";
    private const string MUSIC_KEY = "musickey";
    public Toggle soundsTg;
    public Toggle musicTg;

    private void Start()
    {
        bool isSoundActive = PlayerPrefs.GetInt(SOUNDS_KEY, 1) == 1 ? true : false;
        bool isMusicActive = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1 ? true : false;
        GameController.controller.MuteEffect(isSoundActive);
        GameController.controller.MuteMusic(isMusicActive);
        soundsTg.isOn = isSoundActive;
        musicTg.isOn = isMusicActive;

        soundsTg.onValueChanged.AddListener(x => {
            PlayerPrefs.SetInt(SOUNDS_KEY, x ? 1 : 0);
            GameController.controller.MuteEffect(x);
        });

        musicTg.onValueChanged.AddListener(x => {
            PlayerPrefs.SetInt(MUSIC_KEY, x ? 1 : 0);
            GameController.controller.MuteMusic(x);
        });

        soundsTg.GetComponent<SliderToggle>().Init();
        musicTg.GetComponent<SliderToggle>().Init();
    }
}
