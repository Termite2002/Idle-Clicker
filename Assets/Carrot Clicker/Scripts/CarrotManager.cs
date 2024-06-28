using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarrotManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI carrotsText;


    [Header(" Data ")]
    [SerializeField] private double totalCarrotsCount;
    [SerializeField] private int frenzyModeMultiplier;
    private int carrotIncrement;

    private void Awake()
    {
        LoadData();

        carrotIncrement = 1;

        InputManager.onCarrotClicked += CarrotClickedCallback;

        Carrot.onFrenzyModeStarted += FrenzyModeStartedCallback;
        Carrot.onFrenzyModeStopped += FrenzyModeStoppedCallback;
    }


    private void OnDestroy()
    {
        InputManager.onCarrotClicked -= CarrotClickedCallback;

        Carrot.onFrenzyModeStarted -= FrenzyModeStartedCallback;
        Carrot.onFrenzyModeStopped -= FrenzyModeStoppedCallback;
    }

    private void CarrotClickedCallback()
    {
        totalCarrotsCount += carrotIncrement;

        UpdateCarrotsText();

        SaveData();
    }
    private void UpdateCarrotsText()
    {
        carrotsText.text = totalCarrotsCount + " Carrots!";
    }

    private void FrenzyModeStartedCallback()
    {
        carrotIncrement = frenzyModeMultiplier;
    }
    private void FrenzyModeStoppedCallback()
    {
        carrotIncrement = 1;
    }
    private void SaveData()
    {
        PlayerPrefs.SetString("Carrots", totalCarrotsCount.ToString());
    }
    private void LoadData()
    {
        double.TryParse(PlayerPrefs.GetString("Carrots"), out totalCarrotsCount);

        UpdateCarrotsText();
    }

    public int GetCurrentMultiplier()
    {
        return carrotIncrement;
    }
}
