using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickFunction : MonoBehaviour
{
    [SerializeField] private string sceneToLoadName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoadName);
    }
}
