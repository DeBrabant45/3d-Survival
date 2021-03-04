using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementHelper : MonoBehaviour
{
    [SerializeField] private List<Collider> _collisions = new List<Collider>();
    private Transform _playerTransfrom;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private float _rayCastMaxDistance = 5f;
    private LayerMask _layerMask;
    private float _maxHeightDifference = 0.3f;
    private List<Material[]> _objectMaterials = new List<Material[]>();
    private MaterialHelper _materialHelper;
    private Material m_material;

    private void Start()
    {
        _layerMask.value = 1 << LayerMask.NameToLayer("Ground");
        _materialHelper = new MaterialHelper();
    }

    public void Initialize(Transform transform)
    {
        _playerTransfrom = transform;
    }

    public void PrepareForMovement()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _materialHelper.SwapToSelectionMaterial(gameObject, _objectMaterials, ItemSpawnManager.Instance.TransparentMaterial);
        m_material = GetComponent<Renderer>().material;
    }

    public void PrepareForPlacement()
    {
        _materialHelper.SwapToOriginalMaterial(gameObject, _objectMaterials);
    }

    private void FixedUpdate()
    {
        if(_playerTransfrom != null)
        {
            var positionToMove = _playerTransfrom.position + _playerTransfrom.forward;
            _rigidbody.position = positionToMove;
            _rigidbody.MoveRotation(Quaternion.LookRotation(_playerTransfrom.forward));
            if(_collisions.Count == 0)
            {
                //find corners of box collider
                Vector3 bottomCenter = new Vector3(_boxCollider.center.x, _boxCollider.center.y - _boxCollider.size.y / 2f, _boxCollider.center.z);
                Vector3 topLeftCorner = bottomCenter + new Vector3(-_boxCollider.size.x / 2f, 0, _boxCollider.size.z / 2f);
                Vector3 topRightCorner = bottomCenter + new Vector3(_boxCollider.size.x / 2f, 0, _boxCollider.size.z / 2f);
                Vector3 bottomLeftCorner = bottomCenter + new Vector3(-_boxCollider.size.x / 2f, 0, -_boxCollider.size.z / 2f);
                Vector3 bottomRightCorner = bottomCenter + new Vector3(_boxCollider.size.x / 2f, 0, -_boxCollider.size.z / 2f);
                //Shoot rays from those points
                ShootRaycastDetectionFromObject(topLeftCorner, topRightCorner, bottomLeftCorner, bottomRightCorner, positionToMove);
                //Check the height difference

                //Change material color - user feedback
            }
        }    
    }

    private void ShootRaycastDetectionFromObject(Vector3 topLeftCorner, Vector3 topRightCorner, Vector3 bottomLeftCorner, Vector3 bottomRightCorner, Vector3 positionToMove)
    {
        Debug.DrawRay(transform.TransformPoint(topLeftCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
        Debug.DrawRay(transform.TransformPoint(topRightCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
        Debug.DrawRay(transform.TransformPoint(bottomLeftCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
        Debug.DrawRay(transform.TransformPoint(bottomRightCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);

        RaycastHit hit1, hit2, hit3, hit4;
        bool result1 = Physics.Raycast(transform.TransformPoint(topLeftCorner) + Vector3.up, Vector3.down, out hit1, _rayCastMaxDistance, _layerMask);
        bool result2 = Physics.Raycast(transform.TransformPoint(topRightCorner) + Vector3.up, Vector3.down, out hit2, _rayCastMaxDistance, _layerMask);
        bool result3 = Physics.Raycast(transform.TransformPoint(bottomLeftCorner) + Vector3.up, Vector3.down, out hit3, _rayCastMaxDistance, _layerMask);
        bool result4 = Physics.Raycast(transform.TransformPoint(bottomRightCorner) + Vector3.up, Vector3.down, out hit4, _rayCastMaxDistance, _layerMask);

        if (result1 && result2 && result3 && result4)
        {
            float[] heightValuesList = { hit1.point.y, hit2.point.y, hit3.point.y, hit4.point.y };
            var min = heightValuesList.Min();
            var max = heightValuesList.Max();
            if (max - min > _maxHeightDifference)
            {
                Debug.Log("Not even ground");
                ChangeMaterialColor(Color.red);
            }
            else
            {
                Debug.Log("Even ground");
                ChangeMaterialColor(Color.green);
                _rigidbody.position = new Vector3(positionToMove.x, (max + min) / 2f, positionToMove.z);
            }
        }
    }

    private void ChangeMaterialColor(Color color)
    {
        m_material.SetColor("Color_Shader", color);
    }

    public void DestroyStructure()
    {
        Destroy(gameObject);
    }
}
