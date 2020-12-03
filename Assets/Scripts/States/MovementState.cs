using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    private float _fallingDelay = 0;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _fallingDelay = 0.2f;
    }

    public override void HandleCameraDirection(Vector3 input)
    {
        base.HandleCameraDirection(input);
        controllerReference.Movement.HandleMovementDirection(input);
    }

    public override void HandleMovement(Vector2 input)
    {
        base.HandleMovement(input);
        controllerReference.Movement.HandleMovement(input);
    }

    public override void HandleJumpInput()
    {
        controllerReference.TransitionToState(controllerReference.jumpState);
    }

    public override void Update()
    {
        base.Update();
        HandleMovement(controllerReference.Input.MovementInputVector);
        HandleCameraDirection(controllerReference.Input.MovementDirectionVector);
        if(controllerReference.Movement.CharacterIsGrounded() == false)
        {
            if(_fallingDelay > 0)
            {
                _fallingDelay -= Time.deltaTime;
                return;
            }
            controllerReference.TransitionToState(controllerReference.fallingState);
        }
    }
}
