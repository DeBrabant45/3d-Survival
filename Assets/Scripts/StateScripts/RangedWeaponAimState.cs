using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.ActivatePistolAimAnimation();
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.DeactivatePistolAimAnimation();
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAnimations.DeactivatePistolAimAnimation();
    }
}
