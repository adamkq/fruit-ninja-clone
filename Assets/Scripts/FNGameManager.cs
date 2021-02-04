using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FNGameManager : MonoBehaviour
{
    private int highScore;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private int score;

    public Text livesText;
    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = "Best: " + highScore.ToString();
    }

    private void Start()
    {
        IncreaseScore(0);
    }

    private void Update()
    {
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (lives < 1)
        {
            TransitionToGameOver();
        }
    }

    private void TransitionToGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void IncreaseScore(int dScore)
    {
        score += dScore;
        scoreText.text = score.ToString();

        if (score >= highScore)
        {
            highScore = score;
            highScoreText.text = "Best: " + highScore.ToString();
            PlayerPrefs.SetInt("Highscore", highScore);
        }
    }

    public void OnBombHit()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();
    }
}
