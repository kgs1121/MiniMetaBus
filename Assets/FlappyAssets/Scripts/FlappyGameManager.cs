using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class FlappyGameManager : MonoBehaviour
{
    static FlappyGameManager gameManager;

    public static FlappyGameManager Instance { get { return gameManager; } }

    private int currentScore = 0;

    FlappyUIManager uiManager;
    public FlappyUIManager UIManager { get { return uiManager; } }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<FlappyUIManager>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
    }


    public void GameOver()
    {
        //Debug.Log("Game Over");
        uiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        //Debug.Log("Score: " + currentScore);
        uiManager.UpdateScore(currentScore);
    }


}
