using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text lifeCountText;

    LivesManager LivesManager;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        highScoreText.text = "HighScore Test:" + highScore.ToString();
    }

    private void Update()
    {
        LivesManager = GameObject.FindGameObjectWithTag("Lives Handler").GetComponent<LivesManager>();
        int currentLifeCount = PlayerPrefs.GetInt(LivesManager.LifeCountKey, 10);
        lifeCountText.text = "Lives " + currentLifeCount.ToString();
    }

    public void Play()
    {
        LivesManager = GameObject.FindGameObjectWithTag("Lives Handler").GetComponent<LivesManager>();
        int currentLifeCount = PlayerPrefs.GetInt(LivesManager.LifeCountKey, 10);
        if (currentLifeCount <1) { return; }
        SceneManager.LoadScene(2);
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
}
