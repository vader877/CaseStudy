using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static float FOVMax = 100;
    public static float FOVMin = 45;
    public static float FOVIncrease = 5;
    public static float FOVTarget = 45;
    public static float FOVChangeSpeed = 0.5F;

    public static void IncreaseFOV(int times)
    {
        //Camera cam = Camera.main;
        if (Camera.main.fieldOfView < FOVMax)
        {
            FOVTarget += FOVIncrease * times;
        }
    }

    public static void DecreaseFOV(int times)
    {
        if (Camera.main.fieldOfView > FOVMin)
        {
            FOVTarget -= FOVIncrease * times;
        }
    }

    public void FixedUpdate()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, FOVTarget, FOVChangeSpeed);

    }

}
