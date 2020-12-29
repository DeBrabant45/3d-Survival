using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemSO _itemToSpawn;
    [SerializeField] [Range(1, 100)] private int _count = 1;
    [SerializeField] private bool _singleObject = false;
}
