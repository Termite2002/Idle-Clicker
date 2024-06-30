using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineEarningUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private OfflineEarningPopup popup;
    [SerializeField] private GameObject panelPopup;


    private Vector3 initialScale;

    public void DisplayPopup(double earning)
    {
        popup.Configure(DoubleUtilities.ToScientificNotation(earning));

        popup.GetButton().onClick.AddListener(() => ClaimEarning(earning));

        popup.gameObject.SetActive(true);

        // Anim
        initialScale = panelPopup.transform.localScale;
        panelPopup.transform.localScale = Vector3.zero;
        LeanTween.scale(panelPopup, initialScale, 0.3f).setEaseOutBack();
    }

    private void ClaimEarning(double earning)
    {
        AudioManager.instance.Play("Buy");

        LeanTween.scale(panelPopup, Vector3.zero, 0.3f).setEaseInBack()
            .setOnComplete(() => popup.gameObject.SetActive(false));
        
        CarrotManager.instance.AddCarrots(earning);
    }
}
