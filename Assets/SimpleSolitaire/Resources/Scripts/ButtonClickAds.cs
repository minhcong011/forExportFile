using SimpleSolitaire.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickAds : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowAds);
    }
    public void ShowAds()
    {
        InterVideoAds.Instance.ShowInterstitial();
    }
}
