using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAimState : MovementState
{
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
        controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, true);
        controllerReference.AgentAimController.IsHandsConstraintActive = true;
    }

    public override void HandlePrimaryInput()
    {
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackState);
    }

    public override void HandleSecondaryUpInput()
    {
        controllerReference.AgentAimController.IsHandsConstraintActive = false;
        controllerReference.AgentAnimations.SetBoolForAnimation(((RangedWeaponItemSO)WeaponItem).WeaponAimAnimation, false);
        controllerReference.TransitionToState(controllerReference.rangedWeaponAttackStanceState);
    }
}
