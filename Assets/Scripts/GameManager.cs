using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelWin;

    public GameObject gameOverPanel;
    public GameObject winLevelPanel;
    public GameObject highScoreText;

    public static int currentLevelIndex;
    public static int noOfPassingRings;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ringScoreText;

    public Slider progressBar;

    public int highestScore;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
    }


    private void Start()
    {
       
        Time.timeScale = 1;
        noOfPassingRings = 0;
        gameOver = false;
        levelWin = false;
        UpdateHighScoreUI();

    }
    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

          
            if (score > highestScore)
            {
                highestScore = score;
                PlayerPrefs.SetInt("HighestScore", highestScore);
                PlayerPrefs.Save();
                UpdateHighScoreUI();

            }
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }

        //slider Update
        int progress = noOfPassingRings * 100 / FindObjectOfType<HellixManager>().noOfRings;
        progressBar.value = progress;

        
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();
        if (levelWin)
        {
            winLevelPanel.SetActive(true);
            ResetHighScore();
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene(0);
            }
        }
    }
    private void ResetHighScore()
    {
        highestScore = 0;
        PlayerPrefs.SetInt("HighestScore", highestScore);
        UpdateHighScoreUI();
    }

    private void UpdateHighScoreUI()
    {
        highScoreText.GetComponent<TextMeshProUGUI>().text= "HIGH SCORE \n" + highestScore.ToString();
        
    }

    public void GameScore(int ringScore)
    {
      score += ringScore;
      scoreText.text = score.ToString();
      Invoke("HideRingScoreText", 1f);   
    }

    void HideRingScoreText()
    {
        ringScoreText.text = "";
    }
}
