using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHelper : MonoBehaviour
{
    [SerializeField] private List<Collider> _collisions = new List<Collider>();
    private Transform _playerTransfrom;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private float _rayCastMaxDistance = 5f;

    public void Initialize(Transform transform)
    {
        _playerTransfrom = transform;
    }

    public void PrepareForMovement()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(_playerTransfrom != null)
        {
            _rigidbody.position = _playerTransfrom.position + _playerTransfrom.forward;
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
                Debug.DrawRay(transform.TransformPoint(topLeftCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
                Debug.DrawRay(transform.TransformPoint(topRightCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
                Debug.DrawRay(transform.TransformPoint(bottomLeftCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
                Debug.DrawRay(transform.TransformPoint(bottomRightCorner) + Vector3.up, Vector3.down * _rayCastMaxDistance, Color.magenta);
                //Check the height difference

                //Change material color - user feedback
            }
        }    
    }

    public void DestroyStructure()
    {
        Destroy(gameObject);
    }
}
