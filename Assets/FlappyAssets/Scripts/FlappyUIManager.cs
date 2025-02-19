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
    public GameObject startPanel; // ��ŸƮ ��ư & ������ ��ư�� �ִ� UI �г�
    

    FlappyGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FlappyGameManager.Instance;

        if (scoreText == null) Debug.Log("score text is null");
        if (Endpanel == null) Debug.Log("restart text is null");

        // ó���� ���� ���� ����
        Time.timeScale = 0;

        Endpanel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false); // ���� �г� �����
        gameManager.RestartGame();

        // ���� �ð� �ڿ� ���� ���� ���¸� true�� ���� (���� �߰��� ���۵ǵ���)
        StartCoroutine(StartGameAfterDelay(0.1f));  // ������ �� ���� ����

        gameManager.currentScore = 0;
        Time.timeScale = 1;
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.startgame = true;  // ������ �� ���� ���� ���·� ����
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene"); // ��Ÿ������ ���ư���
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
