using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day Length in minutes")]
    [SerializeField] private float _targetDayLength = 13f;
    [SerializeField, Range(0f, 1f)] private float _timeOfDay;
    [SerializeField] private int _dayNumber = 0;
    [SerializeField] private int _yearNumber = 0;
    [SerializeField] private int _yearLength = 100;

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
}
