using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("Settings")]
    [SerializeField] private int addCarrotsFrequency;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        InvokeRepeating(nameof(AddCarrots), 1, 1f / addCarrotsFrequency);
    }

    private void AddCarrots()
    {
        double totalCarrots = GetCarrotsPerSecond();

        CarrotManager.instance.AddCarrots(totalCarrots / addCarrotsFrequency);
    }

    public double GetCarrotsPerSecond()
    {
        UpgradeSO[] upgrades = ShopManager.instance.GetUpgrades();

        if (upgrades.Length <= 1)
        {
            return 0;
        }

        double totalCarrots = 0;

        for (int i = 0; i < upgrades.Length; i++)
        {
            double upgradeCarrots = upgrades[i].cpsPerLevel * ShopManager.instance.GetUpgradeLevel(i);
            totalCarrots += upgradeCarrots;
        }

        return totalCarrots;
    }
}
