using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField] AgentMovement _movement;
    [SerializeField] PlayerInput _input;

    private void Start()
    {
        _movement = GetComponent<AgentMovement>();
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        _movement.HandleMovement(_input.MovementInputVector);
    }
}
