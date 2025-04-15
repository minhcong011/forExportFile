using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSettingPanel : MonoBehaviour
{
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle vibrateToggle;
    [SerializeField] private Toggle flashToggle;

    private void Start()
    {
        soundToggle.onValueChanged.AddListener(ChangeSound);

        if (GameCache.GC.soundOff) soundToggle.isOn = false;
        if (GameCache.GC.vibrateOff) vibrateToggle.isOn = false;
    }
    public void ChangeSound(bool active)
    {
        GameCache.GC.soundOff = !active;
    }
    public void ChangeVibrate(bool active)
    {
        GameCache.GC.vibrateOff = !active;
    }
}
