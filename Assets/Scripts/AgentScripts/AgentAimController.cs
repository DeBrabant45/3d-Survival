using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class AgentAimController : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 5f;
    [SerializeField] private float _aimDuration = 0.3f;
    [SerializeField] private Image _aimCrossHair;
    [SerializeField] private CinemachineFreeLook _playerFollowCamera;
    [SerializeField] private int _cameraZoomInFieldOfView;
    [SerializeField] private int _cameraZoomOutFieldOfView;
    private Camera mainCamera;

    public Image AimCrossHair { get => _aimCrossHair; set => _aimCrossHair = value; }

    private void Start()
    {
        mainCamera = Camera.main;
        _aimCrossHair.enabled = false;
    }

    public void SetZoomInFieldOfView()
    {
        _playerFollowCamera.m_Lens.FieldOfView = _cameraZoomInFieldOfView;
    }    
    
    public void SetZoomOutFieldOfView()
    {
        _playerFollowCamera.m_Lens.FieldOfView = _cameraZoomOutFieldOfView;
    }

    public void SetCameraToMovePlayer()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.deltaTime);
    }
}
