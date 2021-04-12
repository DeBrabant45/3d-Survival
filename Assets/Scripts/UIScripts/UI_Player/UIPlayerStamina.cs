using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStamina : MonoBehaviour
{
    [SerializeField] private Image _staminaBar;

    public void SetCurrentStamina(float amount)
    {
        _staminaBar.transform.localScale = new Vector3(Mathf.Clamp01(amount), 1, 1);
    }
}
