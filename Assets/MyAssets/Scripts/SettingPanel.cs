using System.Security.Policy;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private string supportLink;

    [SerializeField] private GameObject soundOnBt;
    [SerializeField] private GameObject soundOffBt;
    [SerializeField] private GameObject vibrateOnBt;
    [SerializeField] private GameObject vibrateOffBt;
    private void Start()
    {
        if (GameCache.GC.soundOff)
        {
            soundOnBt.SetActive(false);
            soundOffBt.SetActive(true);
        }
        if (GameCache.GC.vibrateOff)
        {
            vibrateOnBt.SetActive(false);
            vibrateOffBt.SetActive(true);
        }
    }
    public void ChangeSoundStatus()
    {
        GameCache.GC.soundOff = !GameCache.GC.soundOff;
    }
    public void ChangeVibrateStatus()
    {
        GameCache.GC.vibrateOff = !GameCache.GC.vibrateOff;
    }
    public void OpenSupportLink()
    {
        Application.OpenURL(supportLink);
    }
}
