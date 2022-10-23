using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int lastScore;

    private void Start()
    {
        lastScore = PlayerPrefs.GetInt(ScoreHandler.HighScoreKey, 0);
        highScoreText.text = $"The highest score is:\n{lastScore}";
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
}
