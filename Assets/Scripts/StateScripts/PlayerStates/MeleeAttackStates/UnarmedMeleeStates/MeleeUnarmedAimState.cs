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
        if (controllerReference.PlayerStat.Stamina > 0)
        {
            controllerReference.AgentAnimations.IsMeleeUnarmedStanceAnimationActive(false);
            controllerReference.TransitionToState(controllerReference.meleeUnarmedAttackOne);
        }
    }

    public override void HandleEquipItemInput()
    {
        controllerReference.TransitionToState(controllerReference.movementState);
        controllerReference.AgentAimController.SetZoomOutFieldOfView();
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
        controllerReference.AgentAnimations.IsMeleeUnarmedStanceAnimationActive(false);
    }
}
