using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IHittable, IBlockable
{
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private UI_PlayerStats _uIPlayerStats;
    [SerializeField] private int _healthInitialValue;
    [SerializeField] private int _staminaInitialValue;
    [SerializeField] private Transform _blockRaycastStartPosition;
    [SerializeField] private float _staminaRegenSpeed;
    [SerializeField] private float _staminaRegenAmount;
    private float _stamina;
    private float _health;
    private bool _isBlocking = false;
    private float _blockDistance = 0.8f;
    private float _lastTimeSinceStaminaChange;
    bool _stopCoroutine = false;

    public Action OnBlockSuccessful { get; set; }
    public Action OnTakeDamage { get; set; }
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

    public bool IsBlocking { get => _isBlocking; set => _isBlocking = value; }

    public int BlockLevel => throw new NotImplementedException();

    private void Awake()
    {
        Health = _healthInitialValue;
        Stamina = _staminaInitialValue;
    }

    private void Update()
    {
        SaminaRegeneration();
    }

    private void SaminaRegeneration()
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
        if(_isBlocking == true && IsBlockHitSuccessful() == true)
        {
            OnBlockSuccessful.Invoke();
        }
        else
        {
            ReduceHealth(weapon.GetDamageValue());
            OnTakeDamage?.Invoke();
        }
    }

    public bool IsBlockHitSuccessful()
    {
        RaycastHit hit;
        if (Physics.SphereCast(_blockRaycastStartPosition.position, 0.2f, transform.forward, out hit, _blockDistance))
        {
            return true;
        }
        return false;
    }

    IEnumerator StaminaRegenCoroutine()
    {
        yield return new WaitForSeconds(_staminaRegenSpeed);
        Stamina += _staminaRegenAmount;
    }
}
