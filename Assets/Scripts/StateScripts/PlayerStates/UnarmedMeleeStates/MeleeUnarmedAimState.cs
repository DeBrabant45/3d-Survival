using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnarmedAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.IsMeleeUnarmedStanceAnimationActive(true);
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.IsMeleeUnarmedStanceAnimationActive(false);
        controllerReference.TransitionToState(controllerReference.meleeUnarmedAttackOne);
    }

    public override void HandleEquipItemInput()
    {
        base.HandleEquipItemInput();
        controllerReference.AgentAnimations.IsMeleeUnarmedStanceAnimationActive(false);
    }
}
