using System;
using System.Collections;
using UnityEngine;

public class AgentStamina : MonoBehaviour
{
    [SerializeField] private int _staminaInitialValue;
    [SerializeField] private float _staminaRegenSpeed;
    [SerializeField] private float _staminaRegenAmount;

    private float _stamina;
    private Action<float> _staminaValue;
    private float _lastTimeSinceStaminaChange;

    public float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = Mathf.Clamp(value, 0, _staminaInitialValue);
            _staminaValue?.Invoke(_stamina / _staminaInitialValue);
        }
    }

    public Action<float> StaminaValue { get => _staminaValue; set => _staminaValue = value; }

    private void Awake()
    {
        Stamina = _staminaInitialValue;
    }

    private void Update()
    {
        StaminaRegeneration();
    }

    private void StaminaRegeneration()
    {
        if (Stamina < _staminaInitialValue && (_lastTimeSinceStaminaChange + _staminaRegenSpeed) < Time.time)
        {
            Stamina += _staminaRegenAmount;
        }
    }

    public void AddToStamina(float amount)
    {
        Stamina += amount;
    }

    public void ReduceStamina(float amount)
    {
        Stamina -= amount;
        _lastTimeSinceStaminaChange = Time.time;
    }
}
