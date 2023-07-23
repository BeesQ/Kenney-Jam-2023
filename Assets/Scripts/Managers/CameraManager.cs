using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] public CinemachineVirtualCamera followPlayerCamera;
    [SerializeField] float cameraMinSize = 6f;
    [SerializeField] float cameraMaxSize = 12f;


    private float _cameraSize;
    public float CameraSize
    {
        get { return _cameraSize; }
        set { _cameraSize = value; }
    }

    [SerializeField] private CinemachineConfiner2D _confiner2D;
    public CinemachineConfiner2D Confiner2D
    {
        get { return _confiner2D; }
        set { _confiner2D = value; }
    }



    public void SetNewCameraSize(float size)
    {
        _cameraSize = Mathf.Clamp(size, cameraMinSize, cameraMaxSize);
        followPlayerCamera.m_Lens.OrthographicSize = _cameraSize;
    }
}
