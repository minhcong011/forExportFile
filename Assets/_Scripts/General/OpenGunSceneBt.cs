using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGunSceneBt : MonoBehaviour
{
    [SerializeField] private GunType gunType;

    public void OpenScene()
    {
        GunStage.gunType = gunType;
        SceneManager.LoadScene("GunScene");
    }
}
