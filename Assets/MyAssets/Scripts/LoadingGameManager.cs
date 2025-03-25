using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGameManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(WaitLoadGameData());
    }
    IEnumerator WaitLoadGameData()
    {
        while (!GameDataSave.Instance.FinishLoad) yield return null;
        StartCoroutine(LoadSceneAsync("Game"));
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        while (loadingSlider.value < 0.3f)
        {
            loadingSlider.value += Time.deltaTime * 0.2f;
            yield return null;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progress > 0.3f) loadingSlider.value = progress;
            yield return null;
        }
    }
}
