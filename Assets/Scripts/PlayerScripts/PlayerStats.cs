using System;
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
    [SerializeField] private float _staminaRegenSpeed;
    [SerializeField] private float _staminaRegenAmount;
    private BlockAttack _blockAttack;
    private float _stamina;
    private float _health;
    private float _lastTimeSinceStaminaChange;
    private bool _stopCoroutine = false;

    public Action OnTakeDamage { get; set; }
    public UnityEvent OnDeath { get => _onDeath; }
    int IHittable.Health => (int)_health;
    public BlockAttack BlockAttack { get => _blockAttack; }
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
        _blockAttack = GetComponent<BlockAttack>();
    }

    private void Update()
    {
        StaminaRegeneration();
    }

    private void StaminaRegeneration()
    {
        if (Stamina < _staminaInitialValue && Time.time > _lastTimeSinceStaminaChange)
        {
            StartCoroutine("StaminaRegenCoroutine");
            _stopCoroutine = true;
        }
        else if (_stopCoroutine == true)
        {
            StopCoroutine("StaminaRegenCoroutine");
            _stopCoroutine = false;
        }
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
        _lastTimeSinceStaminaChange = Time.time;
    }

    public void GetHit(WeaponItemSO weapon, Vector3 hitpoint)
    {
        if (_blockAttack.IsBlocking == true && _blockAttack.IsBlockHitSuccessful() == true)
        {
            _blockAttack.OnBlockSuccessful.Invoke();
        }
        else
        {
            ReduceHealth(weapon.GetDamageValue());
            OnTakeDamage?.Invoke();
        }
    }

    IEnumerator StaminaRegenCoroutine()
    {
        yield return new WaitForSeconds(_staminaRegenSpeed);
        Stamina += _staminaRegenAmount;
    }
}
