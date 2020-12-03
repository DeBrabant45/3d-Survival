using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    private bool _landingTrigger = false;
    private float _delay = 0;

    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        _landingTrigger = false;
        _delay = 0.2f;
        controllerReference.Movement.HandleJump();
    }

    public override void Update()
    {
        base.Update();
        if(_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }
        if(controllerReference.Movement.IsGrounded())
        {
            if(_landingTrigger == false)
            {
                _landingTrigger = true;
                controllerReference.Movement.StartLandingAnimation();
            }
            if(controllerReference.Movement.HasCompletedJumping())
            {
                controllerReference.TransitionToState(controllerReference.movementState);
            }
        }
    }
}
