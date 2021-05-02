using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttackStanceState : AttackStanceState
{
    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAimController.IsAimActive = false;
        if (controllerReference.PlayerStat.AgentStamina.Stamina > 0)
        {
            controllerReference.AgentAnimations.SetBoolForAnimation(WeaponItem.AttackStance, false);
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackState);
        }
    }

    public override void HandleSecondaryHeldDownInput()
    {
        controllerReference.TransitionToState(controllerReference.blockStanceState);
    }
}
