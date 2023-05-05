using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : Singleton<CameraManager>
{
    Camera mainCamera;
    protected override void Awake()
    {
        base.Awake();
        mainCamera = GetComponent<Camera>();
    }
    public float CameraSize()
    {
        float screenHeightInUnits = mainCamera.orthographicSize * 2;
        return screenHeightInUnits * Screen.width / Screen.height;
    }
    public float Left()
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
    }
    public float Top()
    { 
        return mainCamera.ViewportToWorldPoint(new Vector3(0f, 1.0f, 0f)).y;
    }
    
}
