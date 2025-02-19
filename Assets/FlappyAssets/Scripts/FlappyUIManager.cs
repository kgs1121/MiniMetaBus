using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI PanelscoreText;
    public TextMeshProUGUI BestScoreText;
    public GameObject Endpanel;
    public GameObject startPanel; // 스타트 버튼 & 나가기 버튼이 있는 UI 패널
    

    FlappyGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FlappyGameManager.Instance;

        if (scoreText == null) Debug.Log("score text is null");
        if (Endpanel == null) Debug.Log("restart text is null");

        // 처음엔 게임 정지 상태
        Time.timeScale = 0;

        Endpanel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false); // 시작 패널 숨기기
        gameManager.RestartGame();

        // 일정 시간 뒤에 게임 시작 상태를 true로 설정 (점수 추가가 시작되도록)
        StartCoroutine(StartGameAfterDelay(0.1f));  // 딜레이 후 게임 시작

        gameManager.currentScore = 0;
        Time.timeScale = 1;
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.startgame = true;  // 딜레이 후 게임 시작 상태로 설정
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene"); // 메타버스로 돌아가기
    }


    public void SetRestart(int score, int bestScore)
    {
        if (score > bestScore) bestScore = score;
        PanelscoreText.text = score.ToString();
        BestScoreText.text = bestScore.ToString();
        Endpanel.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void HideEndpanel()
    {
        Endpanel.gameObject.SetActive(false);
    }


}
