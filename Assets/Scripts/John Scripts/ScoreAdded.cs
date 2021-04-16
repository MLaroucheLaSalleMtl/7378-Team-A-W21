using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAdded : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] private Text scoreText;

    public Text ScoreText { get => scoreText; set => scoreText = value; }

    private void Update()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }

    private void Start()
    {
        ScoreText = scoreText;
        ScoreText.text = "Score: " + currentScore.ToString();
    }

    public void GainScore(int score)
    {
        currentScore += score;
        ScoreText.text = "Score: " + currentScore.ToString();
    }
}
