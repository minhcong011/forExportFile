using UnityEngine;
using UnityEngine.UI;

public class ButtonShowAds : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowInterAds);
    }
    private void ShowInterAds()
    {
        Debug.Log("a");
        AdsManager.Instance.ShowInterstitialAd();
    }
}
