using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day Length in minutes")]
    [SerializeField] private float _targetDayLength = 13f;
    [SerializeField, Range(0f, 1f)] private float _timeOfDay;
    [SerializeField] private int _dayNumber = 0;
    [SerializeField] private int _yearNumber = 0;
    [SerializeField] private int _yearLength = 100;

    [Header("Sun Light")]
    [SerializeField] private Transform _dailyRotation;
    [SerializeField] private Light _sun;
    [SerializeField] private float _sunBaseIntensity = 0.8f;
    [SerializeField] private float _sunVariation = 1.5f;
    [SerializeField] private Gradient _sunColor;
    private float _sunIntensity;

    [Header("Moon Light")]
    [SerializeField] private Light _moon;
    [SerializeField] private Gradient _moonColor;
    [SerializeField] private float _moonBaseIntensity = 0.8f;
    [SerializeField] private float _moonVariation = 1.5f;
    private float _moonIntensity;

    private float _timeScale = 100f;
    public bool Pause = false;
    public Action DayHasPassed;

    public int DayNumber { get => _dayNumber; }
    public float TimeOfDay { get => _timeOfDay; }

    private void Update()
    {
        if (!Pause)
        {
            UpdateTimeScale();
            UpdateTime();
            AdjustSunRotation();
            SunIntensity();
            AdjustSunColor();
            MoonIntensity();
            AdjustMoonColor();
            IsDay();
        }
    }

    public bool IsDay()
    {
        if (_timeOfDay > 0.23f && _timeOfDay < 0.78f)
        {
            return true;
        }
        return false;
    }

    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60);
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400;
        if (_timeOfDay > 1)
        {
            _dayNumber++;
            _timeOfDay -= 1;
            DayHasPassed?.Invoke();
        }
        if (_dayNumber > _yearLength)
        {
            _yearNumber++;
            _dayNumber = 0;
        }
    }

    private void AdjustSunRotation()
    {
        float sunAngle = _timeOfDay * 360f;
        _dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));
    }

    private void SunIntensity()
    {
        _sunIntensity = Vector3.Dot(_sun.transform.forward, Vector3.down);
        _sunIntensity = Mathf.Clamp01(_sunIntensity);
        _sun.intensity = _sunIntensity * _sunVariation * _sunBaseIntensity;
    }

    private void MoonIntensity()
    {
        _moonIntensity = Vector3.Dot(_moon.transform.forward, Vector3.down);
        _moonIntensity = Mathf.Clamp01(_moonIntensity);
        _moon.intensity = (1 - _moonIntensity) * _moonBaseIntensity + 0.05f;
    }

    private void AdjustSunColor()
    {
        _sun.color = _sunColor.Evaluate(_sunIntensity);
    }

    private void AdjustMoonColor()
    {
        _moon.color = _moonColor.Evaluate(_moonIntensity);
    }
}
