using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentAimController : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 5f;
    [SerializeField] private float _aimDuration = 0.3f;
    [SerializeField] private Cinemachine.AxisState _xAxis;
    [SerializeField] private Cinemachine.AxisState _yAxis;
    [SerializeField] private Transform _cameraLookAt;
    [SerializeField] private Image _aimCrossHair;
    private Camera mainCamera;

    public Image AimCrossHair { get => _aimCrossHair; set => _aimCrossHair = value; }

    private void Start()
    {
        mainCamera = Camera.main;
        _aimCrossHair.enabled = false;
        SetXAxisValues();
        SetYAxisValues();
    }

    private void SetXAxisValues()
    {
        _xAxis.m_InputAxisName = "Mouse X";
        _xAxis.m_MaxSpeed = 100;
        _xAxis.m_AccelTime = 0.02f;
        _xAxis.m_DecelTime = 0.02f;
        _xAxis.m_MaxValue = 180;
        _xAxis.m_MinValue = -180;
        _xAxis.m_Wrap = true;
    }    
    
    private void SetYAxisValues()
    {
        _yAxis.m_InputAxisName = "Mouse Y";
        _yAxis.m_MaxSpeed = 100;
        _yAxis.m_AccelTime = 0.02f;
        _yAxis.m_DecelTime = 0.02f;
        _yAxis.m_MaxValue = 90;
        _yAxis.m_MinValue = -90;
        _yAxis.m_Wrap = false;
    }

    public void SetCameraToMovePlayer()
    {
        _xAxis.Update(Time.fixedDeltaTime);
        _yAxis.Update(Time.fixedDeltaTime);
        _cameraLookAt.eulerAngles = new Vector3(_yAxis.Value, _xAxis.Value, 0);
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), _turnSpeed * Time.deltaTime);
    }
}
