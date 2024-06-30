using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class OfflineEarningPopup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI earningText;
    [SerializeField] private Button claimButton;

    public void Configure(string earningString)
    {
        earningText.text = earningString;
    }

    public Button GetButton()
    {
        return claimButton;
    }
}
