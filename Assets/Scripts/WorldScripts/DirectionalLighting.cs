using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionalLighting : MonoBehaviour
{
    [Header("Directional Light settings")]
    [SerializeField] protected Light _lightSource;
    [SerializeField] protected float _baseIntensity = 0.8f;
    [SerializeField] protected float _lightVariation = 1.5f;
    [SerializeField] protected Gradient _lightColor;
    protected float _intensityLevel;

    private void Start()
    {
        _lightSource = GetComponent<Light>();
    }

    private void Update()
    {
        Intensity();
        AdjustColor();
    }

    public virtual void Intensity()
    {
        _intensityLevel = Vector3.Dot(_lightSource.transform.forward, Vector3.down);
        _intensityLevel = Mathf.Clamp01(_intensityLevel);
        _lightSource.intensity = _intensityLevel * _lightVariation * _baseIntensity;
    }

    public virtual void AdjustColor()
    {
        _lightSource.color = _lightColor.Evaluate(_intensityLevel);
    }
}
