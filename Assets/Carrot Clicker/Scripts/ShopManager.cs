using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header(" Elements ")]
    [SerializeField] private UpgradeButton upgradeButton;
    [SerializeField] private Transform upgradeButtonParent;

    [Header("Data")]
    [SerializeField] private UpgradeSO[] upgrades;

    [Header("Actions")]
    public static Action<int> onUpgradePurchased;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnButtons();
    }

    private void SpawnButtons()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            SpawnButton(i);
        } 
    }
    private void SpawnButton(int index)
    {
        UpgradeButton upgradeButtonInstance = Instantiate(upgradeButton, upgradeButtonParent);

        UpgradeSO upgrade = upgrades[index];

        int upgradeLevel = GetUpgradeLevel(index);

        Sprite icon = upgrade.icon;
        string title = upgrade.title;
        string subtitle = string.Format("lvl {0} (+{1} Cps)", upgradeLevel, upgrade.cpsPerLevel);
        string price = DoubleUtilities.ToScientificNotation(upgrade.GetPrice(upgradeLevel));

        upgradeButtonInstance.Configure(icon, title, subtitle, price);

        upgradeButtonInstance.GetButton().onClick.AddListener(() => UpgradeButtonClickCallback(index));
    }

    private void UpgradeButtonClickCallback(int upgradeIndex)
    {
        if (CarrotManager.instance.TryPurchase(GetUpgradePrice(upgradeIndex)))
        {
            AudioManager.instance.Play("Buy");
            IncreaseUpgradeLevel(upgradeIndex);
        }
        else
            AudioManager.instance.Play("Poor");
            //Debug.Log("Cant'");
    }

    private void IncreaseUpgradeLevel(int upgradeIndex)
    {
        int currentUpgradeLevel = GetUpgradeLevel(upgradeIndex);
        currentUpgradeLevel++;

        SaveUprgadeLevel(upgradeIndex, currentUpgradeLevel);

        UpdateVisuals(upgradeIndex);

        onUpgradePurchased?.Invoke(upgradeIndex);
    }

    private void UpdateVisuals(int upgradeIndex)
    {
        UpgradeButton upgradeButton = upgradeButtonParent.GetChild(upgradeIndex).GetComponent<UpgradeButton>();

        UpgradeSO upgrade = upgrades[upgradeIndex];

        int upgradeLevel = GetUpgradeLevel(upgradeIndex);


        string subtitle = string.Format("lvl {0} (+{1} Cps)", upgradeLevel, upgrade.cpsPerLevel);
        string price = DoubleUtilities.ToScientificNotation(upgrade.GetPrice(upgradeLevel));

        upgradeButton.UpdateVisuals(subtitle, price);
    }

    private double GetUpgradePrice(int upgradeIndex)
    {
        return upgrades[upgradeIndex].GetPrice(GetUpgradeLevel(upgradeIndex));
    }

    public int GetUpgradeLevel(int upgradeLevel)
    {
        return PlayerPrefs.GetInt("Upgrade" + upgradeLevel);
    }

    private void SaveUprgadeLevel(int upgradeIndex, int upgradeLevel)
    {
        PlayerPrefs.SetInt("Upgrade" + upgradeIndex, upgradeLevel);
    }

    public UpgradeSO[] GetUpgrades()
    {
        return upgrades;
    }
}
