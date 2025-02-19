using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class FlappyGameManager : MonoBehaviour
{
    static FlappyGameManager gameManager;

    public static FlappyGameManager Instance { get { return gameManager; } }

    public int currentScore = 0;
    private int bestScore = 0;

    public bool startgame = false;
    public bool checkRestartGame = false;

    FlappyPlayer player;
    FlappyUIManager uiManager;
    BgLooper bgLooper;
    Obstacle obstacle;
    public FlappyUIManager UIManager { get { return uiManager; } }

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<FlappyUIManager>();
        player = FindObjectOfType<FlappyPlayer>();
        bgLooper = FindObjectOfType<BgLooper>();
        obstacle = FindObjectOfType<Obstacle>();
    }

    private void Start()
    {
        uiManager.UpdateScore(0);
    }

    public void GameOver()
    {
        startgame = false;        
        uiManager.SetRestart(currentScore, bestScore);
        uiManager.UpdateScore(0);
    }

    public void RestartGame()
    {   
        player.ResetPlayer();  // 플레이어 초기화
        uiManager.HideEndpanel();  // 게임 오버 패널 숨기기
        obstacle.transform.position = new Vector3(12, 0, 0);
        ResetBackground();
        bgLooper.RandomDeploy();
    }

    

    public void AddScore(int score)
    {
        if (!startgame) return;
        else
        {
            currentScore += score;
            if (bestScore < currentScore) bestScore = currentScore;
            uiManager.UpdateScore(currentScore);
        }
    }

    void ResetBackground()
    {
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        GameObject[] tgrounds = GameObject.FindGameObjectsWithTag("Topground");
        GameObject[] bgrounds = GameObject.FindGameObjectsWithTag("Bottomground");

        if (backgrounds.Length == 0) return;
        if (tgrounds.Length == 0) return; 
        if (bgrounds.Length == 0) return;

        // 첫 번째 배경의 가로 길이를 기준으로 사용
        float bgWidth = backgrounds[0].GetComponent<BoxCollider2D>().size.x;
        float gWidth = tgrounds[0].GetComponent <BoxCollider2D>().size.x;
        float bWidth = bgrounds[0].GetComponent<BoxCollider2D>().size.x;

        // 첫 번째 배경을 기준으로 X 좌표를 배치
        backgrounds[0].transform.position = Vector3.zero;
        tgrounds[0].transform.position = new Vector3(0, 3.9f, 0);
        bgrounds[0].transform.position = new Vector3(0, -3.8f, 0);
        float bgstartX = backgrounds[0].transform.position.x;
        float gstartX = tgrounds[0].transform.position.x;
        float bstartX = bgrounds[0].transform.position.x;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position = new Vector3(bgstartX + (bgWidth * i), 0, 0);
        }

        for (int i = 0; i < tgrounds.Length; i++)
        {
            tgrounds[i].transform.position = new Vector3(gstartX + (gWidth * i), 3.8f, 0);
        }

        for (int i = 0;i < bgrounds.Length; i++)
        {
            bgrounds[i].transform.position = new Vector3(gstartX + (gWidth * i), -3.8f, 0);
        }
    }

}
