using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private float score;
    private float scoreMultiplier;

    public const string HighScoreKey = "HighScore";

    void Start()
    {
        score = 100;
        scoreMultiplier = 2;
    }
    void Update()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString();

        if (score <1)
        {
            return;
        }

        score -= Time.deltaTime * scoreMultiplier;
    }

    public void SaveHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
