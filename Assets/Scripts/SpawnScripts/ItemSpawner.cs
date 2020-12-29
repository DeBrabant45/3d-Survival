using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemSO _itemToSpawn;
    [SerializeField] [Range(1, 100)] private int _count = 1;
    [SerializeField] [Range(0.1f, 50f)] private float _radius = 1;
    [SerializeField] private bool _singleObject = false;
    [SerializeField] private bool _showGizmo = true;
    [SerializeField] private Color _gizmoColor = Color.green;

    public bool SingleObject { get => _singleObject; }
    public ItemSO ItemToSpawn { get => _itemToSpawn; }
    public int Count { get => _count; }
    public float Radius { get => _radius; }

    public void OnDrawGizmos()
    {
        if(_showGizmo && _radius > 0)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
