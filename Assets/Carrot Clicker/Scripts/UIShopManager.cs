using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private CircleCollider2D colliderCarrot;

    [Header("Settings")]
    private Vector2 openedPosition;
    private Vector2 closedPosition;

    private void Start()
    {
        openedPosition = Vector2.zero;
        closedPosition = new Vector2(shopPanel.rect.width, 0);

        shopPanel.anchoredPosition = closedPosition;
    }

    public void Open()
    {
        colliderCarrot.enabled = false;
        LeanTween.cancel(shopPanel);
        LeanTween.move(shopPanel, openedPosition, 0.3f).setEase(LeanTweenType.easeInOutSine);
    }
    public void Close()
    {
        colliderCarrot.enabled = true;
        LeanTween.cancel(shopPanel);
        LeanTween.move(shopPanel, closedPosition, 0.3f).setEase(LeanTweenType.easeInOutSine);
    }
}
