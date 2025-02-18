using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText == null) Debug.Log("score text is null");
        if (restartText == null) Debug.Log("restart text is null");

        restartText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

}
