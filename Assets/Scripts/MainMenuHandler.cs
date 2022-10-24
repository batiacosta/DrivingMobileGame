using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDurationMinutes;

    private int _energy;
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    private int lastScore;

    private void Start()
    {
        lastScore = PlayerPrefs.GetInt(ScoreHandler.HighScoreKey, 0);
        highScoreText.text = $"The highest score is:\n{lastScore}";

        CheckEnergy();

        if (_energy == 0)
        {
            var dateEnergyIsRecharged = GetWhenEnergyIsRecharged();
            if (DateTime.Now > dateEnergyIsRecharged)
            {
                RechargeEnergy();
            }
        }

        playButtonText.text = $"Play ({_energy})";
    }

    private static DateTime GetWhenEnergyIsRecharged()
    {
        //  check when energy should be regenerated
        string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
        if (energyReadyString == string.Empty) ;

        DateTime energyReady = DateTime.Parse(energyReadyString);
        return energyReady;
    }

    private void RechargeEnergy()
    {
        _energy = maxEnergy; //  Refill energy
        PlayerPrefs.SetInt(EnergyKey, _energy);
    }

    private void CheckEnergy()
    {
        _energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
    }

    public void Play()
    {
        if (_energy == 0) return;
        
        _energy--;
        PlayerPrefs.SetInt(EnergyKey, _energy);

        if (_energy == 0)
        {
            DateTime oneMinuteFromNow = DateTime.Now.AddMinutes(energyRechargeDurationMinutes);
            PlayerPrefs.SetString(EnergyReadyKey, oneMinuteFromNow.ToString());
        }
        SceneManager.LoadScene("Level1");

    }
}
