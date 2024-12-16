using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelDesign : MonoBehaviour
{
    [SerializeField] private GameObject[] levelDesigns;
    [SerializeField] private bool test;
    private void Awake()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
#if UNITY_EDITOR
        if (test)
        {
            GameCache.GC.currentLevel = 2;
            return;
        }
#endif
        if(GameModeController.gameMode == GameMode.Nomal) levelDesigns[GameCache.GC.currentLevel - 1].SetActive(true);
        if (GameModeController.gameMode == GameMode.Daily) levelDesigns[Random.Range(1, levelDesigns.Length)].SetActive(true);
    }
}
