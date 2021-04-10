using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonDirectionalLighting : DirectionalLighting
{
    public override void Intensity()
    {
        _intensityLevel = Vector3.Dot(_lightSource.transform.forward, Vector3.down);
        _intensityLevel = Mathf.Clamp01(_intensityLevel);
        _lightSource.intensity = (1 - _intensityLevel) * _baseIntensity + 0.05f;
    }
}
