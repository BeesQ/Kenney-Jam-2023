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

    private void Start()
    {
        //_confiner2D = Camera.main.GetComponent<CinemachineConfiner2D>();
        //Debug.Log(_confiner2D);
    }

    private void Update()
    {
        var distance = PointBetweenManager.Instance.GetLongestDistanceBetweenPlayers();
        var newSize = distance * 0.3f * 2 * (9f / 16f) + 0.5f;

        //if (distance > 20)
        //{
        //    newSize = distance / 3;
        //}
        //else if (distance > 8)
        //{
        //    newSize = distance / 2;
        //}
        //else if (distance > 6)
        //{
        //    newSize = distance;
        //}
        //Debug.Log("Distance: " + distance);
        //Debug.Log("Size: " + newSize);
        //SetNewCameraSize(newSize);
    }

    public void SetNewCameraSize(float size)
    {
        _cameraSize = Mathf.Clamp(size, cameraMinSize, cameraMaxSize);
        followPlayerCamera.m_Lens.OrthographicSize = _cameraSize;
    }
}
