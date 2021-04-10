using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDirectionalLighting : DirectionalLighting
{
    public override void Intensity()
    {
        _intensityLevel = Vector3.Dot(_lightSource.transform.forward, Vector3.down);
        _intensityLevel = Mathf.Clamp01(_intensityLevel);
        _lightSource.intensity = _intensityLevel * _lightVariation * _baseIntensity;
    }
}
