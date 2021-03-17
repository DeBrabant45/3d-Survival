using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IHittable
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

    int IHittable.Health => (int)_health;

    private void Awake()
    {
        Health = _healthInitialValue;
        Stamina = _staminaInitialValue;
    }

    public void AddToHealth(float amount)
    {
        Health += amount;
    }

    public void ReduceHealth(float amount)
    {
        Health -= amount;
    }

    public void AddToStamina(float amount)
    {
        Stamina += amount;
    }

    public void ReduceStamina(float amount)
    {
        Stamina -= amount;
    }

    public void GetHit(WeaponItemSO weapon, Vector3 hitpoint)
    {
        ReduceHealth(weapon.GetDamageValue());
    }
}
