using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("Actions")]
    public static Action onCarrotClicked;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowRaycast();
        }
    }

    private void ThrowRaycast()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hit.collider == null)
        {
            return;
        }

        Debug.Log("Hit Carrot");
        onCarrotClicked?.Invoke();
    }
}
