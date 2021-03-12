using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private UI_PlayerStats _uIPlayerStats;
    [SerializeField] private int _healthInitialValue;
    [SerializeField] private int _staminaInitialValue;
    private float _stamina;
    private float _health;

    public UnityEvent OnDeath { get => _onDeath; }
    public float Stamina 
    { 
        get => _stamina;
        set
        {
            _stamina = Mathf.Clamp(value, 0, _staminaInitialValue);
            _uIPlayerStats.SetStamina(_stamina / _staminaInitialValue);
        }
    }

    public float Health 
    { 
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _healthInitialValue);
            _uIPlayerStats.SetHealth(_health / _healthInitialValue);
            if(_health <= 0)
            {
                Debug.Log("Player Has died");
                _onDeath.Invoke();
            }
        }
    }

    private void Awake()
    {
        Health = _healthInitialValue;
        Stamina = _staminaInitialValue;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Health -= 40;
            Stamina -= 10;
        }
    }
}
