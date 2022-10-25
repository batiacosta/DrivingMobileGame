using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreMultiplier;

    private float _score;

    public const string HighScoreKey = "HighScore";

    private void Update()
    {
        _score += Time.deltaTime * scoreMultiplier;

        SetScoreUI();
    }

    private void SetScoreUI()
    {
        var score = Mathf.FloorToInt(_score);
        scoreText.text = score.ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (_score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(_score));
        }
    }
}
