using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.Movement.StopMovement();
        controllerReference.AgentAnimations.OnFinishedAttacking += TransitionBack;
        controllerReference.AgentAnimations.TriggerAttackAnimation();
    }

    private void TransitionBack()
    {
        controllerReference.AgentAnimations.OnFinishedAttacking -= TransitionBack;
        controllerReference.TransitionToState(controllerReference.movementState);
    }
}
