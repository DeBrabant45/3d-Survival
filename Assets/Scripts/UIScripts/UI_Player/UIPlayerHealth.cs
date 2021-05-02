using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private Image _heart;
    [SerializeField] private List<Image> _healthObjectContainer;
    [SerializeField] private Sprite _emptyHeart;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private AgentHealth _agentHealth;

    private void Awake()
    {
        SetHealthInitialValue(_agentHealth.HealthInitialValue);
        _agentHealth.HealthValue += SetCurrentHealth;
    }

    public void SetCurrentHealth(int amount)
    {
        SetHealthSprite(amount);
    }

    private void SetHealthSprite(int amount)
    {
        for (int i = 0; i < _healthObjectContainer.Count; i++)
        {
            if (i < amount)
            {
                _healthObjectContainer[i].sprite = _fullHeart;
            }
            else
            {
                _healthObjectContainer[i].sprite = _emptyHeart;
            }
        }
    }

    public void SetHealthInitialValue(int amount)
    {
        DestoryAllChildGameObjects(); 
        ClearHealthContainerList();
        for (int i = 0; i < amount; i++)
        {
            var _healthObject = Instantiate(_heart, transform);
            _healthObjectContainer.Add(_healthObject);
        }
    }

    private void DestoryAllChildGameObjects()
    {
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void ClearHealthContainerList()
    {
        _healthObjectContainer.Clear();
    }
}
