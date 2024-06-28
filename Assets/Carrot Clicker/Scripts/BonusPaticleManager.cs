using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPaticleManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private CarrotManager carrotManager;
    [SerializeField] private GameObject bonusParticlePrefab;

    private void Awake()
    {
        InputManager.onCarrotClickedPosition += CarrotClickedCallback;
    }
    private void OnDestroy()
    {
        InputManager.onCarrotClickedPosition -= CarrotClickedCallback;
    }

    private void CarrotClickedCallback(Vector2 clickedPosition)
    {
        GameObject bonusParticleInstance = Instantiate(bonusParticlePrefab, clickedPosition, Quaternion.identity, transform);

        bonusParticleInstance.GetComponent<BonusParticle>().Configure(carrotManager.GetCurrentMultiplier());

        Destroy(bonusParticleInstance, 1);
    }
}
