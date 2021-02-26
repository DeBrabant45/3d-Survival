using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAimState : AimState
{
    public override void EnterState(AgentController controller)
    {
        base.EnterState(controller);
        controllerReference.AgentAnimations.IsMeleeWeaponStanceAnimationActive(true);
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAnimations.IsMeleeWeaponStanceAnimationActive(false);
        controllerReference.TransitionToState(controllerReference.meleeWeaponAttackOne);
    }

    public override void HandleAimInput()
    {
        base.HandleAimInput();
        controllerReference.AgentAnimations.IsMeleeWeaponStanceAnimationActive(false);
    }
}
