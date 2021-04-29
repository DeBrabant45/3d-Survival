using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    public bool _landingTrigger = false;
    public float _delay = 0;

    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        _landingTrigger = false;
        _delay = 0.2f;
        controllerReference.AgentAnimations.ResetTriggerForAnimation("land");
        controllerReference.Movement.HandleJump();
    }

    public override void HandleMenuInput()
    {
        base.HandleMenuInput();
        controllerReference.TransitionToState(controllerReference.menuState);
    }

    public override void Update()
    {
        base.Update();
        if(_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }
        if (controllerReference.Movement.CharacterIsGrounded())
        {
            if (_landingTrigger == false)
            {
                _landingTrigger = true;
                controllerReference.AgentAnimations.SetTriggerForAnimation("land");
            }
            if (controllerReference.Movement.HasCompletedJumping())
            {
                controllerReference.AgentAnimations.ResetTriggerForAnimation("fall");
                controllerReference.TransitionToState(controllerReference.movementState);
            }
        }
    }
}
