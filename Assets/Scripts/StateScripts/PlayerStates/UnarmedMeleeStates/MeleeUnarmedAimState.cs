using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnarmedAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.ActivateUnarmedStanceAnimation();
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.DeactivateUnarmedStanceAnimation();
        controllerReference.TransitionToState(controllerReference.meleeUnarmedAttackOne);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAnimations.DeactivateUnarmedStanceAnimation();
    }
}
