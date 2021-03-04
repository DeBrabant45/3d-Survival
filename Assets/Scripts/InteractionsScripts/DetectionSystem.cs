using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _objectDetectionMask;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private Material _selectionMaterial;
    [SerializeField] private Transform _weaponRaycastStartPosition;
    [SerializeField] private Transform _rangedWeaponRaycastStartPosition;
    [SerializeField] private float _attackDistance = 0.8f;
    [SerializeField] private float _shootingRange = 10f;
    [SerializeField] GameObject _impactEffect;
    private List<Collider> _collidersList = new List<Collider>();
    private Collider _currentCollider;
    private List<Material[]> _currentColliderMaterailsList = new List<Material[]>();
    private Action<Collider, Vector3> _onAttackSuccessful;
    private Action<Collider, Vector3, RaycastHit> _onRangeAttackSuccessful;
    private MaterialHelper _materialHelper = new MaterialHelper();

    public Collider CurrentCollider { get => _currentCollider; }
    public float DetectionRadius { get => _detectionRadius; }
    public Action<Collider, Vector3> OnAttackSuccessful { get => _onAttackSuccessful; set => _onAttackSuccessful = value; }
    public GameObject ImpactEffect { get => _impactEffect; }
    public Action<Collider, Vector3, RaycastHit> OnRangeAttackSuccessful { get => _onRangeAttackSuccessful; set => _onRangeAttackSuccessful = value; }

    public Collider[] DetectObjectsInfront(Vector3 movementDirectionVector)
    {
        return Physics.OverlapSphere(transform.position + movementDirectionVector, _detectionRadius, _objectDetectionMask);
    }

    public void PreformDetection(Vector3 movementDirectionVector)
    {
        var colliders = DetectObjectsInfront(movementDirectionVector);
        _collidersList.Clear();
        foreach (var collider in colliders)
        {
            var pickableItem = collider.GetComponent<IPickable>();
            if(pickableItem != null)
            {
                _collidersList.Add(collider);
            }
        }

        if(_collidersList.Count == 0)
        {
            if(_currentCollider != null)
            {
                _materialHelper.SwapToOriginalMaterial(_currentCollider.gameObject, _currentColliderMaterailsList);
                _currentCollider = null;
            }
            return;
        }

        if(_currentCollider == null)
        {
            _currentCollider = _collidersList[0];
            _materialHelper.SwapToSelectionMaterial(_currentCollider.gameObject, _currentColliderMaterailsList, _selectionMaterial);
        }
        else if(_collidersList.Contains(_currentCollider) == false)
        {
            _materialHelper.SwapToOriginalMaterial(_currentCollider.gameObject, _currentColliderMaterailsList);
            _currentCollider = _collidersList[0];
            _materialHelper.SwapToSelectionMaterial(_currentCollider.gameObject, _currentColliderMaterailsList, _selectionMaterial);
        }
    }

    public void DetectColliderInFront()
    {
        RaycastHit hit;
        if(Physics.SphereCast(_weaponRaycastStartPosition.position, 0.2f, transform.forward, out hit, _attackDistance))
        {
            _onAttackSuccessful?.Invoke(hit.collider, hit.point);
        }
    }

    public void DetectColliderFromRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(_rangedWeaponRaycastStartPosition.transform.position, transform.forward, out hit, _shootingRange))
        {
            _onRangeAttackSuccessful?.Invoke(hit.collider, hit.point, hit);
        }
    }
}
