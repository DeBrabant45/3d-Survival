using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _objectDetectionMask;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private Material _selectionMaterial;
    private List<Collider> _collidersList = new List<Collider>();
    private Collider _currentCollider;
    private List<Material[]> _currentColliderMaterailsList = new List<Material[]>();

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
                SwapToOriginalMaterial();
                _currentCollider = null;
            }
            return;
        }

        if(_currentCollider == null)
        {
            _currentCollider = _collidersList[0];
            SwapToSelectionMaterial();
        }
        else if(_collidersList.Contains(_currentCollider) == false)
        {
            SwapToOriginalMaterial();
            _currentCollider = _collidersList[0];
            SwapToSelectionMaterial();
        }
    }

    private void SwapToSelectionMaterial()
    {
        _currentColliderMaterailsList.Clear();
        if (_currentCollider.transform.childCount > 0)
        {
            foreach (Transform child in _currentCollider.transform)
            {
                PrepareRendererToSwapMaterials();
            }
        }
        else
        {
            PrepareRendererToSwapMaterials();
        }
    }

    private void PrepareRendererToSwapMaterials()
    {
        var renderer = _currentCollider.GetComponent<Renderer>();
        _currentColliderMaterailsList.Add(renderer.sharedMaterials);
        SwapMaterials(renderer);
    }

    private void SwapMaterials(Renderer renderer)
    {
        Material[] matArray = new Material[renderer.materials.Length];
        for (int i = 0; i < matArray.Length; i++)
        {
            matArray[i] = _selectionMaterial;
        }
        renderer.materials = matArray;
    }

    private void SwapToOriginalMaterial()
    {
        if(_currentColliderMaterailsList.Count > 1)
        {
            for (int i = 0; i < _currentColliderMaterailsList.Count; i++)
            {
                var renderer = _currentCollider.transform.GetChild(i).GetComponent<Renderer>();
                renderer.materials = _currentColliderMaterailsList[i];
            }
        }
        else
        {
            var renderer = _currentCollider.GetComponent<Renderer>();
            renderer.materials = _currentColliderMaterailsList[0];
        }
    }
}
