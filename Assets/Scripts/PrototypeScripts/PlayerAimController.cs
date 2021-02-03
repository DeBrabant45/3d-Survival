using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    public Transform cameraLookAt;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }
}
