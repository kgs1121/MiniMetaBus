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
    public GameObject RemoveScore;
    public GameObject ScoreBoard;
    public GameObject Endpanel;
    public GameObject startPanel; // ��ŸƮ ��ư & ������ ��ư�� �ִ� UI �г�
    public TextMeshProUGUI[] bestScoreTexts;  // UI�� �ְ� ���� �ؽ�Ʈ �迭


    FlappyGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FlappyGameManager.Instance;

        if (scoreText == null) Debug.Log("score text is null");
        if (Endpanel == null) Debug.Log("restart text is null");

        // ó���� ���� ���� ����
        Time.timeScale = 0;

        ScoreBoard.SetActive(false);
        Endpanel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false); // ���� �г� �����
        gameManager.RestartGame();

        // ���� �ð� �ڿ� ���� ���� ���¸� true�� ���� (���� �߰��� ���۵ǵ���)
        StartCoroutine(StartGameAfterDelay(0.1f));  // ������ �� ���� ����

        gameManager.currentScore = 0;
        UpdateScore(0);
        Time.timeScale = 1;
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.startgame = true;  // ������ �� ���� ���� ���·� ����
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainScene"); // ��Ÿ������ ���ư���
    }


    public void SetRestart(int score, int bestScore)  // ���ӿ��� �г� ����
    {
        if (score > bestScore) bestScore = score;
        PanelscoreText.text = score.ToString();
        BestScoreText.text = bestScore.ToString();
        Endpanel.gameObject.SetActive(true);
    }

    public void SetScoreList()
    {
        List<int> bestScores = gameManager.bestScorelist; // �ְ� ���� ����Ʈ ��������
        int scoreCount = bestScores.Count;

        for (int i = 0; i < bestScoreTexts.Length; i++)
        {
            if (i < scoreCount)
            {
                bestScoreTexts[i].text = bestScores[i].ToString(); // ���� ����
            }
            else
            {
                bestScoreTexts[i].text = "-"; // ������ �����ϸ� �� �� ǥ��
            }
        }
    }


    public void RemovedScore()
    {
        gameManager.bestScorelist.Clear();
        gameManager.SaveBestScore(0);
        gameManager.LoadBestScores();
    }



    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void HideEndpanel()
    {
        Endpanel.gameObject.SetActive(false);
    }

    public void LookScoreBoard()
    {
        gameManager.LoadBestScores();
        SetScoreList();
        ScoreBoard.gameObject.SetActive(true);
    }


    public void Startpanel()
    {
        Endpanel.SetActive(false);
        ScoreBoard.SetActive(false);
        startPanel.SetActive(true);
    }

}
