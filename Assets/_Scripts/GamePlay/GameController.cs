using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : SingletonBase<GameController>
{
    public GameStage currentGameStage;
    private bool isUseBooster;
    public bool IsUseBooster { get { return isUseBooster; } set { isUseBooster = value; } }

    private void Start()
    {
        Application.targetFrameRate = 60;
        currentGameStage = GameStage.Playing;
    }
    public void SetGameStage(GameStage newGameStage)
    {
        currentGameStage = newGameStage;
    }
    public void Test()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void PlayGame()
    {
        currentGameStage = GameStage.Playing;
    }
    public void PauseGame()
    {
        currentGameStage = GameStage.Pause;
    }
    public void LostGame()
    {
        currentGameStage = GameStage.LostGame;
    }
    public void ExitGamePlay()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene("LoadingGame");
    }
}
public enum GameStage
{
    Pause, Playing, LostGame
}
