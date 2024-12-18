using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float topPosY, middlePosy;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup CanvasGroup;

    public void Pause()
    { 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        PausePanelIntro();
}

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public async void Resume()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        CanvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosy, tweenDuration).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        CanvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
