using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInView : MonoBehaviour
{

    private Camera _mainCam;      
    bool _addOnlyOnce;

    void Start()
    {
        _mainCam = Camera.main;
        _addOnlyOnce = true;
    }

    void Update()
    {

        //First Create A Vector3 With Dimensions Based On The Camera's Viewport
        Vector3 enemyPosition = _mainCam.WorldToViewportPoint(gameObject.transform.position);

        //If The X And Y Values Are Between 0 And 1, The Enemy Is On Screen
        bool onScreen = enemyPosition.z > 0 && enemyPosition.x > 0 && enemyPosition.x < 1 && enemyPosition.y > 0 && enemyPosition.y < 1;

        //If The Enemy Is On Screen Add It To The List Of Nearby Enemies Only Once
        if (onScreen && _addOnlyOnce)
        {
            _addOnlyOnce = false;
            TargetController.nearByEnemies.Add(this);
        }
    }
}