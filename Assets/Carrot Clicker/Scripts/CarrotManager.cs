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
    [SerializeField] private double carrotIncrement;

    private void Awake()
    {
        LoadData();

        InputManager.onCarrotClicked += CarrotClickedCallback;
    }

    private void OnDestroy()
    {
        InputManager.onCarrotClicked -= CarrotClickedCallback;
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
    private void SaveData()
    {
        PlayerPrefs.SetString("Carrots", totalCarrotsCount.ToString());
    }
    private void LoadData()
    {
        double.TryParse(PlayerPrefs.GetString("Carrots"), out totalCarrotsCount);

        UpdateCarrotsText();
    }
}
