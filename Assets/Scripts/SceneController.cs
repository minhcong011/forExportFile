using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        //AdmobAds2 ads = gameObject.GetComponent<AdmobAds2>();

        //// Check if ads are ready
        //if (ads != null && ads.IsInterstitialAdReady())
        //{
        //    ads.ShowInterstitialAd(OnAdClosed);  // Show the ad and call OnAdClosed when the ad is finished
        //}
        //else
        //{
            LoadNextScene();  // If ad is not ready, directly proceed to the next level
        //}
    }


    private void OnAdClosed()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
