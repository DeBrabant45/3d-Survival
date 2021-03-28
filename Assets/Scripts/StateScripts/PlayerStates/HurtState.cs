using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.SetTriggerForAnimation("hurtPlayer"); 
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger += ReturnBackToState;
    }

    public void ReturnBackToState()
    {
        controllerReference.AgentAnimations.OnAnimationFunctionTrigger -= ReturnBackToState;
        controllerReference.TransitionToState(controllerReference.PreviousState);
    }
}
