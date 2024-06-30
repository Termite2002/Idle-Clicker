using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGlow : MonoBehaviour
{
    private void Start()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 360f, 4f).setLoopClamp();
    }

}
