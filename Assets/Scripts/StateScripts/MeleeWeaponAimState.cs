using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.ActivateSwordAimAnimation();
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.DeactivateSwordAimAnimation();
        controllerReference.TransitionToState(controllerReference.meleeAttackState);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAnimations.DeactivateSwordAimAnimation();
    }
}
