using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("Actions")]
    public static Action onCarrotClicked;
    public static Action<Vector2> onCarrotClickedPosition;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ThrowRaycast();
        //}

        if (Input.touchCount > 0)
        {
            ManageTouches();
        }
    }

    private void ManageTouches()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                ThrowRaycast(touch.position);
            }
        }
    }

    private void ThrowRaycast(Vector2 touchPosition)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touchPosition));

        if (hit.collider == null)
        {
            return;
        }

        //Debug.Log("Hit Carrot");
        onCarrotClicked?.Invoke();
        onCarrotClickedPosition?.Invoke(hit.point);
    }


}
