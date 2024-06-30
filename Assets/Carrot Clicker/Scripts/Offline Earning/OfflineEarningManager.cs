using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OfflineEarningUI))]
public class OfflineEarningManager : MonoBehaviour
{
    [Header("Elements")]
    private OfflineEarningUI offlineEarningUI;

    [Header("Settings")]
    [SerializeField] private int maxOfflineSeconds;
    private DateTime lastDateTime;

    private void Awake()
    {
        offlineEarningUI = GetComponent<OfflineEarningUI>();
        
    }

    private void Start()
    {

        if (LoadLastDateTime())
            CalculateOfflineSeconds();
        else
        {
            Debug.LogWarning("Can't PArse Time");
        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (LoadLastDateTime())
                CalculateOfflineSeconds();
            else
            {
                Debug.LogWarning("Can't PArse Time");
            }
        }
            
        SaveCurrentDateTime();
    }



    //private void OnApplicationQuit()
    //{
    //    SaveCurrentDateTime();
    //}

    private void CalculateOfflineSeconds()
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(lastDateTime);

        int offlineSeconds = (int)timeSpan.TotalSeconds;
        offlineSeconds = Mathf.Min(offlineSeconds, maxOfflineSeconds);

        CalculateOfflineEarning(offlineSeconds);
    }

    private void CalculateOfflineEarning(int offlineSeconds)
    {
        if (UpgradeManager.instance == null)
        {
            LeanTween.delayedCall(Time.deltaTime, () => CalculateOfflineEarning(offlineSeconds));
            return;
        }

        double offlineEarnings = offlineSeconds * UpgradeManager.instance.GetCarrotsPerSecond();

        //Debug.Log(offlineEarnings);

        if (offlineEarnings <= 0)
            return;

        offlineEarningUI.DisplayPopup(offlineEarnings);
    }

    private bool LoadLastDateTime()
    {
        bool validDateTime = DateTime.TryParse(PlayerPrefs.GetString("LastDateTime"), out lastDateTime);

        return validDateTime;
    }
    private void SaveCurrentDateTime()
    {
        DateTime now = DateTime.Now;

        PlayerPrefs.SetString("LastDateTime", now.ToString());
    }
}
