using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _objectDetectionMask;
    [SerializeField] private float _detectionRadius;
    private List<Collider> _collidersList = new List<Collider>();
    private Collider _currentCollider;

    public Collider CurrentCollider { get => _currentCollider; }
    public float DetectionRadius { get => _detectionRadius; }

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
                _currentCollider = null;
            }
            return;
        }

        if(_currentCollider == null)
        {
            _currentCollider = _collidersList[0];
        }
        else if(_collidersList.Contains(_currentCollider) == false)
        {
            _currentCollider = _collidersList[0];
        }
        Debug.Log(_collidersList.Count);
    }
}
