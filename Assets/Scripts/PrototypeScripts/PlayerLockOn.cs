using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    [SerializeField] private TargetController _targetController;

    // Update is called once per frame
    void Update()
    {
        if(_targetController.LockedOn)
        {
            var target = _targetController.TargetEnemey;
            Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(targetPosition);
        }
    }
}