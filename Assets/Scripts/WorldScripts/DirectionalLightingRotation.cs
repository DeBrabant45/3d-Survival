using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightingRotation : MonoBehaviour
{
    [SerializeField] private TimeCycle _timeCycle;

    private void Update()
    {
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        float lightAngle = _timeCycle.TimeOfDay * 360f;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, lightAngle));
    }
}
