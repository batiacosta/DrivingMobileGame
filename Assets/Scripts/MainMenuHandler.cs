using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDurationMinutes;
    [SerializeField] private AndroidNotificationsHandler androidNotificationsHandler;
    [SerializeField] private iOSNotificationHandler iOSNotificationsHandler;
    [SerializeField] private Button playButton;

    private int _energy = 0;
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    private int lastScore;

    private void Start()
    {
        OnAplicationFocus(true);
    }

    private void OnAplicationFocus(bool hasFocus)
    {
        if(!hasFocus) return;
        CancelInvoke();
        
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
            else
            {
                playButton.interactable = false;
                Invoke(nameof(RechargeEnergy), (dateEnergyIsRecharged - DateTime.Now).Seconds);
            }
        }
        UpdatePlayButtoText();
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
        _energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, _energy);
        playButton.interactable = true;
        UpdatePlayButtoText();
    }

    private void UpdatePlayButtoText()
    {
        playButtonText.text = $"Play ({_energy})";
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
            #if UNITY_ANDROID
                androidNotificationsHandler.ScheduleNotification(oneMinuteFromNow);
            #elif UNITY_IOS
            iOSNotificationsHandler.ScheduleNotification(energyRechargeDurationMinutes);
            #endif
            
            
        }
        SceneManager.LoadScene("Level1");

    }
}
