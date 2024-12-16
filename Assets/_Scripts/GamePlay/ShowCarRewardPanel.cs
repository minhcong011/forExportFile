using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShowCarRewardPanel : MonoBehaviour
{
    [SerializeField] private Image carIcon;

    [SerializeField] SpriteAtlas carIconSprite;
    private void Start()
    {
        //carIcon.sprite = carIconSprite.GetSprite($"{GameCache.GC.currentLevel}");
        GameCache.GC.currentLevel++;
    }
    public void ExitMainMenu()
    {
        StartCoroutine(ExitMainMenuCoroutinue());
    }
    IEnumerator ExitMainMenuCoroutinue()
    {
        if(GameCache.GC.currentLevel >= 10) yield return AdsManager.Instance.ShowInterAync();
        SceneManager.LoadScene("GamePlay");
    }
}
