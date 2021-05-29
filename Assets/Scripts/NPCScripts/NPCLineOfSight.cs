using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCLineOfSight : MonoBehaviour
{
    [SerializeField] float _sightRange;
    [SerializeField] private LayerMask _isPlayer;
    [SerializeField] private Rig _rig;
    [SerializeField] private float _aimDuration = 0.3f;
    bool _isPlayerInSightRange;

    private void Update()
    {
        _isPlayerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _isPlayer);
        SetNPCRigWeight();
    }

    public void SetNPCRigWeight()
    {
        if (_isPlayerInSightRange == true)
        {
            _rig.weight += Time.deltaTime / _aimDuration;
        }
        else
        {
            _rig.weight -= Time.deltaTime / _aimDuration;
        }
    }
}
