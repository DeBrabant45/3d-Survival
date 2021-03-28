using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnarmedAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.SetBoolForAnimation("meleeUnarmedStance", true);
    }

    public override void HandlePrimaryInput()
    {
        base.HandlePrimaryInput();
        if (controllerReference.PlayerStat.Stamina > 0)
        {
            controllerReference.AgentAnimations.SetBoolForAnimation("meleeUnarmedStance", false);
            controllerReference.TransitionToState(controllerReference.meleeUnarmedAttackState);
        }
    }

    public override void HandleEquipItemInput()
    {
        controllerReference.TransitionToState(controllerReference.movementState);
        controllerReference.AgentAimController.SetZoomOutFieldOfView();
        controllerReference.AgentAimController.AimCrossHair.enabled = false;
        controllerReference.AgentAnimations.SetBoolForAnimation("meleeUnarmedStance", false);
    }
}
