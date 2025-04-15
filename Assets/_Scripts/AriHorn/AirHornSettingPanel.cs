using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirHornSettingPanel : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private Color[] colors;
    [SerializeField] private GameObject chooseBorder;
    [SerializeField] private GameObject[] chooseButtons;

    public void SetBgColor(int id)
    {
        bg.color = colors[id];
        chooseBorder.transform.position = chooseButtons[id].transform.position;
    }
}
