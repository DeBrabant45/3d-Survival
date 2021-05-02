using System;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    [SerializeField] private int _healthInitialValue;
    [SerializeField] private Action _onHealthAmountEmpty;

    private float _health;
    private Action<int> _healthValue;

    public Action OnHealthAmountEmpty { get => _onHealthAmountEmpty; set => _onHealthAmountEmpty = value; }
    public Action<int> HealthValue { get => _healthValue; set => _healthValue = value; }
    public int HealthInitialValue { get => _healthInitialValue; }

    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _healthInitialValue);
            _healthValue?.Invoke((int)_health);
            if (_health <= 0)
            {
                _onHealthAmountEmpty?.Invoke();
            }
        }
    }

    private void Awake()
    {
        Health = _healthInitialValue;
    }

    public void AddToHealth(float amount)
    {
        Health += amount;
    }

    public void ReduceHealth(float amount)
    {
        Health -= amount;
    }
}
