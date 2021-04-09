using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDayNightCycle : MonoBehaviour
{
    [SerializeField] private Text _currentDayValue;
    [SerializeField] private Image _sunIcon;
    [SerializeField] private Image _moonIcon;
    [SerializeField] private DayNightCycle _dayNightCycle;

    // Start is called before the first frame update
    void Start()
    {
        _dayNightCycle.DayHasPassed += SetCurretDayNumber;
    }

    private void SetCurretDayNumber()
    {
        _currentDayValue.text = _dayNightCycle.DayNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(_dayNightCycle.IsDay() == false)
        {
            _moonIcon.enabled = true;
            _sunIcon.enabled = false;
        }
        else
        {
            _sunIcon.enabled = true;
            _moonIcon.enabled = false;
        }
    }
}
