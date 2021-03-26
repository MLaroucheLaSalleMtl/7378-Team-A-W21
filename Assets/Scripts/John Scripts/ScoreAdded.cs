using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAdded : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
    public void GainScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
