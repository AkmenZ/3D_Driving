using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private IOSNotificationHandler iOSNotificationHandler;

    private int energy;

    private const string energyKey = "Energy";
    private const string energyReadyKey = "EnergyReady";

    private void Start() {
        int highScore = PlayerPrefs.GetInt(ScoreHandler.highScoreKey, 0);

        highScoreText.text = $"High Score: {highScore}";

        energy = PlayerPrefs.GetInt(energyKey, maxEnergy);

        if(energy == 0) {
            string energyReadyString = PlayerPrefs.GetString(energyReadyKey, string.Empty);

            if(energyReadyString == string.Empty) return;

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if(DateTime.Now > energyReady) {
                energy = maxEnergy;

                PlayerPrefs.SetInt(energyKey, energy);
            }
        }

        energyText.text = $"Play: ({energy})";
    }

    public void Play() {

        if(energy < 1) return;

        energy --;

        PlayerPrefs.SetInt(energyKey, energy);

        if(energy == 0) {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(energyReadyKey, energyReady.ToString());

            #if UNITY_IOS
            iOSNotificationHandler.ScheduleNotification(energyRechargeDuration);
            #endif
        }

        SceneManager.LoadScene("Scene_Game");
    }
}
