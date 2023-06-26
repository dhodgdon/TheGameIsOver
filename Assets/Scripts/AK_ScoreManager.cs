using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AK_ScoreManager : MonoBehaviour
{
    public static AK_ScoreManager instance;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    private int score = 0;
    private int highscore = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // scoreText.text = score.ToString() + " POINTS";
        // highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public void AddToScore()
    {
        score += 1000;
        scoreText.text = score.ToString() + " POINTS";
    }
}