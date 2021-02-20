using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Include This

public class TargetController : MonoBehaviour
{

    private Camera _mainCam; 
    private EnemyInView _targetEnemey; 
    private Image _image;    
    private bool _lockedOn;    
    private int _lockedEnemy;

    public static List<EnemyInView> nearByEnemies = new List<EnemyInView>();

    public bool LockedOn { get => _lockedOn; }
    public EnemyInView TargetEnemey { get => _targetEnemey; set => _targetEnemey = value; }

    void Start()
    {
        _mainCam = Camera.main;
        _image = gameObject.GetComponent<Image>();
        _lockedOn = false;
        _lockedEnemy = 0;
    }

    void Update()
    {

        //Press Space Key To Lock On
        if (Input.GetKeyDown(KeyCode.L) && !_lockedOn)
        {
            if (nearByEnemies.Count >= 1)
            {
                _lockedOn = true;
                _image.enabled = true;

                //Lock On To First Enemy In List By Default
                _lockedEnemy = 0;
                _targetEnemey = nearByEnemies[_lockedEnemy];
            }
        }
        //Turn Off Lock On When L Is Pressed Or No More Enemies Are In The List
        else if ((Input.GetKeyDown(KeyCode.L) && _lockedOn) || nearByEnemies.Count == 0)
        {
            _lockedOn = false;
            _image.enabled = false;
            _lockedEnemy = 0;
            _targetEnemey = null;
        }

        //Press X To Switch Targets
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_lockedEnemy == nearByEnemies.Count - 1)
            {
                //If End Of List Has Been Reached, Start Over
                _lockedEnemy = 0;
                _targetEnemey = nearByEnemies[_lockedEnemy];
            }
            else
            {
                //Move To Next Enemy In List
                _lockedEnemy++;
                _targetEnemey = nearByEnemies[_lockedEnemy];
            }
        }

        if (_lockedOn)
        {
            _targetEnemey = nearByEnemies[_lockedEnemy];
            //Determine Crosshair Location Based On The Current Target
            gameObject.transform.position = _mainCam.WorldToScreenPoint(_targetEnemey.transform.position);
            //Rotate Crosshair
            gameObject.transform.Rotate(new Vector3(0, 0, -1));
        }
    }
}
