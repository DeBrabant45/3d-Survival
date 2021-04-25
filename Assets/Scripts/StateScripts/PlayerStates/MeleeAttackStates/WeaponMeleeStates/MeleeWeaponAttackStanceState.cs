using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttackStanceState : AttackStanceState
{
    public override void HandlePrimaryInput()
    {
        controllerReference.AgentAimController.IsAimActive = false;
        if (controllerReference.PlayerStat.Stamina > 0)
        {
            controllerReference.AgentAnimations.SetBoolForAnimation(equippedItem.AttackStance, false);
            controllerReference.TransitionToState(controllerReference.meleeWeaponAttackState);
        }
    }

    public override void HandleSecondaryHeldDownInput()
    {
        controllerReference.TransitionToState(controllerReference.defenseState);
    }
}
