using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEmissions : MonoBehaviour
{
    [SerializeField] private GameObject _agentModel;
    [SerializeField, Range(0, 1)] private float _hurtFeedbackTime = 0.2f;

    private MaterialHelper _materialHelper = new MaterialHelper();

    public void StartHurtCoroutine()
    {
        StartCoroutine(HurtCoroutine());
    }

    private IEnumerator HurtCoroutine()
    {
        _materialHelper.EnableEmission(_agentModel, Color.red);
        yield return new WaitForSeconds(_hurtFeedbackTime);
        _materialHelper.DisableEmission(_agentModel);
    }

    public void DisablePlayerEmissions()
    {
        StopCoroutine(HurtCoroutine());
        _materialHelper.DisableEmission(_agentModel);
    }
}
