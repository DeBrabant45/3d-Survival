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
    private float _stamina;
    private float _health;
    private bool _isBlocking = false;
    private float _blockDistance = 0.8f;

    public Action OnBlockSuccessful { get; set; }
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
        if(_isBlocking == true && IsBlockHitSuccessful() == true)
        {
            OnBlockSuccessful.Invoke();
        }
        else
        {
            ReduceHealth(weapon.GetDamageValue());
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
}
