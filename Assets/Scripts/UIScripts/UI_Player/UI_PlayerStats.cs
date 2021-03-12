using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStats : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _staminaBar;

    public void SetStamina(float amount)
    {
        _staminaBar.transform.localScale = new Vector3(Mathf.Clamp01(amount), 1, 1);
    }

    public void SetHealth(float amount)
    {
        _healthBar.transform.localScale = new Vector3(Mathf.Clamp01(amount), 1, 1);
    }
}
